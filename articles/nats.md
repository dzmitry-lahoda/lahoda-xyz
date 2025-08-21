# Overview

This document describes archival (and replication, and DA - so it is not main purpose kind of) solution via NATS.

## Problem

We need to be sure that if we committed action (and snap hash for it) to `rollman`, that action and any one before it are in `highly durable`📍 DA.
We have at least `10Mbit/second of many small messages`📍 and for legal reasons need `long term storage`📍 too, latency should be good too.
Also we need `snap` storage replica to allow start rolling from that in case of `app` data loss to restart `app` within SLA.
Solution should have high security.

## Solution

Using NATS cloud provider (locally it is Go OSS and easy to run).

Re-execution is not target of this solution.

Some things marked as:
🧩 - optional things, sometimes just possibilities, not critical
📍 - critical and very important things
⏳ - must be done, but later

## 📍 Terms and constraints

`da_period =  2 * max(fault proof time, rollup revision time)`

`mirror_period = max(6 month, da_period)` ℹ️ can be reduced as soon as we will get `Archiver` done

`sla_period = min(app SLA of being off, 1 hour) / 2`

`max_action_message_size = 4kb`

`max_da_latency = 99.9% under 50ms` ℹ️ time it takes to remote ACK message after local journaled ACK.

`da_durability = 99.9999%` 
ℹ️ in general during full flow we should be able to have such durability over whole flow.
For example, local app node + NATS file replica 3 may achieve this for hours, and if mirror added - for months.


Above is simplification, more precise percentiles definitions can be elaborated later.

Price discovery for solution is out of scope of this document.


### Detailed description

We need:
1. publish and wait for ACK before committing to rollman for that action
   ℹ️ users view does not need to wait for `(remote) replicated ACK`, they're fine with `(local) journaled ACK`
2. manage archiving and clean up

All of next assumes relevant cloud configuration is done to make things happen.
We expect NATS configured to use TLS and accounts (consumers, leaf nodes, admins) to use ED25519 keys
(as supported by NATS NKeys). 

#### Streaming configuration

Specifically `stream` with `max_action_message_size` and dedup period equal clean up equal to `da_period`,
to consume versioned `subject`s.
And relevant replication and archival parameters outlined below.

- Stream retention discarding old messages
  - All streams disabled with deletion via API.
  - Subject prefix is `action.*`
- 📍 Replication factor is 3 with file persist within `da_period` and same size dedup window, no compression
  - replicas get fastest storage and network latency possible
  - ℹ️ replicas automatically failover if 1 node lost and have high availability (NATS RAFT leader chosen)
  - only authorized keys can read from replicas
  - 📍 2 mirror streams (file based with compression) streams in 2 regions with `mirror_period` retention
    - mirror streams have semi permissionless rate-limited read      
    - 🧩 within time one of sources must be self-hosted as leaf node
      - in theory active validators may provide their own leaf nodes mirrors as needed
    - `Archiver` consumer will replicate mirror into object storage

#### Publisher and Subjects

It is just app using nats-rs.

- ⭘ Registers `solution_revision_id` key in `subject`s KV, which is u16.
  - ♦️ Checks if self is latest action subject according to `compatibility_key` 
    - ⎇ If not, fails ◉ 
    - ⎇ If yes, adds record with `solution_revision_id`
      - Value is `compatibility_key` consisting from:
        - `rollup_revision_id` u16 
        - `state_version` is u16 number
        - `engine_version` first two u16 numbers from crate version
      - Adds reverse map with key being `compatibility_key` 
◼

When sending action message, 
⭘ - Gets its `solution_revision_id`
   - Checks latest message in `action_stream`, and starts reading from journal for that
   - Synchronously linearly publishes messages (publish and wait for ACK).🧩 NATS can do async - can improve later.
    - Set `message id` header equal to `solution_revision_id.action_id` and `action.{solution_revision_id}` as subject
        - Here we have exactly once delivery 📍 of action from journal to stream
        - This is how we ensure consumers can switch to proper version of app and data as needed.
    - After messages are ACKed by stream, it is ACKed to `rollman` 📍.
◼

##### Aggregates

- Updates or inserts `rollup_revision_id` value under `action_id` key into canonical KV
  - NATS `revision_id` equals exactly `rollup_revision_id`, conflict resolved via CAS
  - `TTL` and `dedup` windows are `da_period`
  - Metadata enriched with NATS stream `sequence_id`, ℹ️ that's how we map `action_id <-> sequence_id` in stream to start consuming 📍.

##### 🧩 Snap store

We should publish snaps periodically into `Object Store` with compression enabled.

Publish snap each `sla_period` under `rollup_revision_id.action_id` with metadata of `compatibility_key` and `hash` of state.

Keep files for `da_period`.

ℹ️ This is how we start fast after local data loss. Also good for debug.

##### ⏳ Archiver

`Archiver` is consumer which consumes messages weeks before mirror stream messages are evicted, after `da_period` passed.

It consumes and confirms batches of messages and stores them in AWS Glacier(or NATS Object storage with compression, depending on price vs SLA).

Using NATS Execution Engine to host Go code like that makes solution to be trivial (from template).

#### Done

So given above we got fast confirmation for DA and archival replication.


#### 🧩 Improvements

There could be various consumers, like user signature verifiers, re-execution validators, and indexers these are out of scope of this document.

Consumer of mirrors can update its `view` and provide current state to readers, need `receipt` stream for that.

NATS describes data types via JSON Schema and AsyncAPI, can do same for app WS. 

Attach at least one leaf node in custom location to ensure better DA.

### Notes

According to quick research, solution is identical to what we will do and get if we go with Kafka or start with custom replication and attach batching to object storage.
Describing why we are doing NATS and not others is out of scope of this document.

Guesstimate of overhead of using generic NATS solution instead of tied exactly to our problem would be 2x of network traffic and storage per action. And some increase of latency (but not so much).

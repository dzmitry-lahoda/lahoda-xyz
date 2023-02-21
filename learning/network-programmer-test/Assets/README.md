# Overview

## How to use

- start server, start clients by buttones
- send flash to server from any of client
- as soon as flash happens on any of clients, counter increments

## How it works

- Client connects and sends pings.
- If there was flash, clients sends it many times.
- Run flashes are marked as handled .
- Old flashes removed
- Client plays flashes several ticks old
- Client runs several ticks above server
- Server cleans old flashes
- Client mass send all flashes to all client many times

## Hot it does not work

- no fancy drawing to make flash:)
- not circular buffer used, just slow imitation on collections
- latency is not averaged/varinaced/weigheted and subject to jitter
- all data is sent to all clients all the time, no delta/diff/per consumer optimizations
- no handling of edge case like multi tick updates or disconnects or other errors
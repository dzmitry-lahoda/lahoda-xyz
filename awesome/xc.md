# Awesome XC

Awesome cross chain languages and protocols (Polkadot XCM and Cosmos ICS-999).

## XCM standard and specs

- [XCM Part II: Versioning and Compatibility](https://medium.com/polkadot-network/xcm-part-ii-versioning-and-compatibility-b313fc257b83)
- [XCM Part III: Execution and Error Management](https://medium.com/polkadot-network/xcm-part-iii-execution-and-error-management-ceb8155dd166)
- [Polkadot Cross-Consensus Message (XCM) Format (specification)](https://github.com/paritytech/xcm-format/blob/master/README.md)
- [XCM: The Cross-Consensus Message Format](https://medium.com/polkadot-network/xcm-the-cross-consensus-message-format-3b77b1373392)
- [The XCM executor(reference source)](https://github.com/paritytech/polkadot/blob/master/xcm/xcm-executor/src/lib.rs)
- [Cross-Consensus Message Format (XCM)](https://wiki.polkadot.network/docs/learn-xcm)
- [pallet-xcm(official public extrinsics for users)](https://github.com/paritytech/polkadot/blob/master/xcm/pallet-xcm/src/lib.rs)
- [Gavin Wood: XCM V3 | Polkadot Decoded 2022(video)](https://www.youtube.com/watch?v=ccfhYX3AimU)

## Bridges

[Demystifying Trustless Bridges in the Polkadot Ecosystem | Polkadot Decoded](https://www.youtube.com/watch?v=HN2MQryGA14) #video #dotsama #parity #2023

## Assets/Registries

[Polkadot XCM Global Asset Registry (GAR) for Polkadot and Kusama Ecosystem](https://github.com/colorfulnotion/xcm-global-registry) #dotsama #assets #registry


## Multihop solutions

[packet-forward-middleware](https://github.com/strangelove-ventures/packet-forward-middleware) #ibc #ics-20 #trasfer #memo #hack


## Multichain turing complete lanaguages

[ICS-999](https://github.com/larry0x/ics999) #cosmos #ibc #cosmwasm

## Indexers/Monitoring/Alerts/Aggregators

[Subscum](subscan.io) #xcm #redis #myssql

[Polkaholic](https://polkaholic.io/) #xcm [#oss](https://github.com/colorfulnotion/polkaholic) #bigquery #mysql

## Modules

### Dotsama

[pallet-ismp](https://github.com/polytope-labs/substrate-ismp) #bridge #trustless #messaging

### IBC

[Introducing the Callbacks Middleware: Compose Smart Contracts and Modules with IBC](https://medium.com/the-interchain-foundation/introducing-the-callbacks-middleware-compose-smart-contracts-and-modules-with-ibc-6f3fb527e44a)
 
## Configuration

- [XCM Config & Pallet-XCM | Polkadot Deep Dives](https://www.youtube.com/watch?v=bFMvWmU1pYI) #video #runtime #configuration #design #substrate


# Overview

To run, any chain needs:

- genesis state (to be shared with cluster of nodes which form consensus)
- runtime to execute (logic of)
- bootstrap networking information (which nodes to connect)
- and, either relay setup or what relay to use
- and other useful setup like logging, metrics
- private keys in keystore 
- logical network connectivity allowance (bridges)

There are guides and tools which help to make life easier

## How tos

- https://docs.substrate.io/tutorials/get-started/
- https://docs.substrate.io/tutorials/connect-relay-and-parachains/
- https://github.com/paritytech/devops-guide

## Tools

- https://github.com/open-web3-stack/parachain-launch
- https://github.com/paritytech/polkadot-launch/
- https://github.com/paritytech/zombienet
- https://www.youtube.com/watch?v=9BPGQOM7d2E
- https://github.com/paritytech/polkadot-introspector 
- https://github.com/maxsam4/fork-off-substrate
- [chopsticks](https://github.com/AcalaNetwork/chopsticks) - [smoldot](https://github.com/paritytech/smoldot) light client Substrate runtime host with fork and upgrade capabilities running in JavaScript VM

## Test chains

- https://wiki.polkadot.network/docs/learn-common-goods
- https://github.com/paritytech/cumulus/tree/master/zombienet



# Overview

Resources which allows to grasp XCM(P)('Cross Chain Message (Passing)').  

## Basics

[Moonbeam: Cross-Consensus Messaging (XCM)](https://docs.moonbeam.network/builders/xcm/overview/)

[Polkadot's Cross-chain Message Passing Protocol: Shared Security and Polkadot's Design(video)](https://www.youtube.com/watch?v=XU6dAAQD9UE)

[How XCM will actually be used with XCMP](https://forum.polkadot.network/t/how-xcm-will-actually-be-used-with-xcmp/190)

[Tokens transfer scenarios](https://github.com/open-web3-stack/open-runtime-module-library/pull/856)]

## How to setup XCMP basics

- [Polkadot XCM Cross-Chain Asset Transfer Demo](https://medium.com/oak-blockchain/polkadot-xcm-cross-chain-asset-transfer-demo-53aa9a2e97a7)
- [Tutorial Polkadot Cross-Chain Message Passing (XCMP) demo with ping pallet](https://medium.com/oak-blockchain/tutorial-polkadot-cross-chain-message-passing-xcmp-demo-with-ping-pallet-f53397158ab4)
- [XCMP overview](https://research.web3.foundation/en/latest/polkadot/XCMP/index.html)



## XCMP design

- [Gavin Wood: XCM V3: Building a Heterogeneous Sharded Ecosystem | Polkadot Decoded 2022(video)](https://www.youtube.com/watch?v=ccfhYX3AimU)
- [Substrate Builders Program Office Hours: XCM AMA(video)](https://www.youtube.com/watch?v=cS8GvPGMLS0)
- [Shawn Tabrizi: XCM - The Backbone Of A Multichain Future | Polkadot Decoded 2022(video)](https://www.youtube.com/watch?v=2tmspefsygQ)
- [Sub0 Online: Getting Started with XCM - Your First Cross Chain Messages](https://www.youtube.com/watch?v=5cgq5jOZx9g)
- [How can I transfer assets using XCM?](https://substrate.stackexchange.com/questions/37/how-can-i-transfer-assets-using-xcm)
- [Sub0.1: Gavin Wood presents Polkadot's cross-chain messaging (XCMP) scheme(2020)](https://www.youtube.com/watch?v=wrA9vlPjVPE)
- [https://research.web3.foundation/en/latest/polkadot/XCMP/Opening_closing%20XCMP%20Channel.html](https://research.web3.foundation/en/latest/polkadot/XCMP/Opening_closing%20XCMP%20Channel.html)
- [Bill Laboon of Polkadot discusses Parachains and XCMP](https://www.youtube.com/watch?v=P_yLrFfmLrU) #video #2021
- <https://blog.quarkslab.com/resources/2022-02-27-xcmv2-audit/21-12-908-REP.pdf>
- [Polkadot Implementers Guide Messaging Overview](https://github.com/paritytech/polkadot/blob/master/roadmap/implementers-guide/src/messaging.md)
- [Polkadot’s Messaging Scheme(article)](https://medium.com/web3foundation/polkadots-messaging-scheme-b1ec560908b7)
- [Polkadot: XCM v3 | Polkadot Deep Dives](https://www.youtube.com/watch?v=MMIPNR3SuB4) #video #2023


## HRMP

- [How to open Dotsama XCMP HRMP channel](./How%20to%20open%20Dotsama%20XCMP%20HRMP%20channel.md)
- [StackExchange: Substrate and Polkadot: How to open HRMP channels between parachains?](https://substrate.stackexchange.com/questions/5445/how-to-open-hrmp-channels-between-parachains)
- [Substrate How to Guides: Parachains: Add HRMP channels](https://docs.substrate.io/reference/how-to-guides/parachains/add-hrmp-channels/)

## Cross chain assets

<https://polkadot.network/blog/statemint-becomes-first-common-good-parachain-on-polkadot/>

## Real-world usage

- [Substrate Seminar: Extending XCM to the off chain world](https://www.youtube.com/watch?v=5mspUoK1aIE)
- [Moonbeam XCM Interoperability](https://docs.moonbeam.network/builders/xcm/)
- [Statemine XCMP configuration](https://github.com/paritytech/cumulus/blob/master/parachains/runtimes/assets/statemine/src/xcm_config.rs)
- [Bridging parachains to other ecosystems with Phala's SubBridge](https://www.youtube.com/watch?v=K5i_BpuZAnE)
- [XBI, an XCM-based binary interface for smart contracts | Substrate Seminar(video)](https://www.youtube.com/watch?v=9nBGSMs8XM8)
- [Moonbeam: Moonbeam and XCM: Our Experience and What's to Come | Sub0 2022](https://www.youtube.com/watch?v=Lg2xyNuCBxA)

## Solutions/integrations/tools

- [Moonbeam: Moonbeam and XCM: Our Experience and What's to Come | Sub0 2022](https://www.youtube.com/watch?v=Lg2xyNuCBxA) #video #tools #monitoring #fees
- [Subscan XCM (explorer/indexer)](https://picasso.subscan.io/xcm_dashboard)
- ORML + Cumulus, which does not support out of box access to all XMP and as of now opinionated implementations instructions.
- <https://www.youtube.com/watch?v=92w8rVXB5q8> - extends and enhances to support more `Transact` patterns
- [Substrate Utilities(decoder/encoder)](https://www.shawntabrizi.com/substrate-js-utilities)
- [Account Format Transform](https://polkadot.subscan.io/tools/ss58_transform)
- [A set of scripts to help XCM initialization, asset registration and chanel set up](https://github.com/PureStake/xcm-tools)

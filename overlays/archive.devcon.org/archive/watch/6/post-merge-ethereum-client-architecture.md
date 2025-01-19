# Overview


This document is overlay over https://archive.devcon.org/archive/watch/6/post-merge-ethereum-client-architecture/

# Execution client

With regard to data, syncs, prunes it.

Transactions.

geth for example.

# Consensus Client

Blocks, attestations, slashings, exists

Named Beacon node. 
Prysm for example.


# Interaction

Consensus calls Execution via EngineAPI

Validator calls Consensus.

Consensus calls MEV-boost.

# Validator Client

May or may not be part of Consensus client.

# External signer

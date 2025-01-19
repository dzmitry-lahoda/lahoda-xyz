Low level blockchain is peer discovery, block propagation, consensus, transaction finalization.


CommetBFT has strict fork accountability. What is strict fork account ability?

Validator confirm valid blocks and reject invalid by signature, and penalized when not able to do so.

Block creator collect signatures until finalized set and broadcasts block.

Delegators either vote or validator votes for them. Only delegated stake can vote.

Consensus establishes canonical set of blocks with well ordered set of transactions.


Application Checks Transaction to be well formed, if not it is not propagated.

Consesnsus delivers blocks information to execution layer.

Chain initialized from genesis.

Application hash is hash of application state after executing and committing block.

Modules receive begin and end block.

## Interchain Security

Interchain security provider chain validator is obliged to run separate validator of consumer chain.

Consumer chain knows which validators securing provider chains via IBC.

Consumer chain tells misbehavior to provider so that validators are slashed on provider chain.
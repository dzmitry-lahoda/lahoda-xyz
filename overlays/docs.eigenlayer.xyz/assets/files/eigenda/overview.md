

Is data availiability store securedd by EignenLayer validators.

It is decenralized transient storage.

The purpose of this to store L2 rollup transactions, until these finaled on L1.


Inspired by Danksharding.

Blob writes are registered on L1, subject **operators** to slashing risk.

Allows only DA metadata and accountability process to happen on L1.

Consist of three components.
Operators, Disperser, Retriever.



Dispenser Reed-Solomon blob,  calculates KZG commitment KC, and generates proof of KZG commitment of each chunk.
It sends chunks, KC and proofs to Operators.

Operators are trusted staked. 
They store data as per request if data was:
- payed fees and treshhold of stake used to secure
- provided data verifiers againsk KZG commitment and proof
In case of success check, O signs message wit KZG commitment and chunk index,
and send send these to Disperser

Dispenser uploads signatures to L1.

Retriever requestred fro BlobChunks, vderify chunks are accruent, reconstuct origina blob.
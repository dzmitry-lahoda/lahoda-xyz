
Nomt is merkle trie.

Any index to become hash. 
If use have natural keys index, natural key will be hashes,
and key stored as trie octets.

Any operation accpanied on it gives:
- read witness
- write witness
- readwrite wittness

Each commit gives you:
- all witnesses
- delta

Delta can be used to destuctively rollback into the past to previous commit.

Wittnesses and wittness data can be used:
- to prove changes made by commit in ZK
- make logical replication of storage
- prove to other computer any specific trie path update

These opeation do not need full access to data nor load whole NOMT storage into prover.

NOMT cannot non destuctoively rollback into any point of time.
It will be O(number of commits) to do so.

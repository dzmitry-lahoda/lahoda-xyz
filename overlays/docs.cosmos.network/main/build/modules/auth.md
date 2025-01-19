
Auth defines Transaction and Account structures, and authenticates transactions.

Fees general purpose censorship of transactions of no economic value when validators are disinterested in use of network and identities of users.

$$
fees = \ceil (gas_limit * gas_price)
$$

Validator must check if any other validator accepts transaction fee before including tx into pool or gossiping.

Min gas prices up to fluctuation as market moves.

Auth contains public key, account number, sequence number per account and coins.

Sequence number against replay protection of deleted accounts.

Sequence number for replay protection.

Ante handler is decorator(interceptor) which checks transactions or throws them out of mempool.
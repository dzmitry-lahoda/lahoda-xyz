data Rollup = ZK | Optimistic(Optimistic)

-- waits for fraud window
data Optimistic = {}

data OptimisticFinalityDelay = Hour | Week

fraud(r:Optimistic) = 
  raise "return fraud proof if any"

data Transactopm = {}

batch (r: Rollup) =
  raise "batch and return transaction to eth"
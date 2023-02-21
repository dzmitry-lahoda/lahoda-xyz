divides d n = rem n d  == 0

ldf k n | divides k n = k
        | k^2 > n     = n
        | otherwise   = ldf (k+1) n
        
ld n = ldf 2 n

div2  = divides 2 

prime0 n | n <= 1   = error "not a postiove number"
         | n == 1   = False
         | otherwise = ld n == n 
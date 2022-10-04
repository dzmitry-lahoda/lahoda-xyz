


import Data.List
import Text.Read

-- | f :: a -> b
-- | g :: B ->  C

-- | fg = g . f

id :: a -> a
id x = x

undefinedf x  = undefined

fact n = product [1..n]

someToNothing _ = ()


singleon1 () = 1

main = do
 putStr "Hello \n" 
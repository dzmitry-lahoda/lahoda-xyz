


import Data.Char
import Data.List
import Text.Read
import Data.Bits

-- | f :: a -> b
-- | g :: B ->  C

-- | fg = g . f
id :: a -> a
id x = x
undefinedf x  = undefined
fact n = product [1..n]
someToNothing _ = ()
singleton42 () = 42
getAnythingAndReturnNothing _ = ()
gdc a 0 = a
gdc 0 b = b
gdc a b | b == 0    = a 
        | a == 0    = b 
        | a > b     = gdc b (rem a b)
        | otherwise = gdc b (rem b a)


class MyMonoid m where 
    mempty  :: m
    mappend :: m -> m -> m

-- instance MyMonoid String where
--     mempty  = ""
--     mappend = (++) 

composition f g = f . g
add2 x = x + 2
mul2 x = x * 2
aIdentity x = x
idF x f = composition aIdentity f x
fId x f = composition f aIdentity x
mp = composition add2 mul2 
primes = divisionPrime [2..]
-- example:
-- divisionPrime [2, 3, 4, 5, ..] 
--   2 : divisionPrime [all elements not divided by 2 ] 
--     3: divisionPrime [all elements not divided by 3] 
--       5: divisionPrime [all elements not divided by 5]
--         .. 
--   2: [3, 5, ..]  
-- [2, 3, 5, ..]  
divisionPrime (p : xs) = p : divisionPrime [ x | x <- xs, mod x p > 0]
mp3 = mp 3
gdc42_13 = gdc 42 36 

shiftC x n = chr (mod (((ord x) - ord 'a') + n) 26 + ord 'a')

caesar encrypted = [ [shift x n | x <- encrypted]  | n <- [0 .. 26]]

shiftN n a = (chr (((((ord a) - (ord 'a')) + n) `mod` 26) + ord 'a'))
shiftNBack n a = (chr (((((ord a) - (ord 'a')) - n) `mod` 26) + ord 'a'))
shiftN19 = shiftN 19   
shiftNBack19 = shiftNBack 19   
exampleshiftN = shiftN19 'a' == 'b'
exampleshiftNBack19 = shiftNBack19 'b' == 'a'
boolXor a b = a /= b

percent 0 = 1000
percent n = 1.05 * percent (n - 1)

percentExample = percent 10 == 2000.0 

generator = [3^n `mod` 42 | n <- [1..] ]

doPrint = print "Hello!"

main = do
 putStr "Hello \n"
 print gdc42_13
 print (percent 20) 
 print mp3
 print ((xor (xor (42 :: Int) 13) 13) == 42)
 print ('a')
 print (shiftN19 'a')
 print (shiftNBack19 (shiftN19 'a'))
 print (take 20 primes)
 print (idF 1 add2 == fId 1 add2)
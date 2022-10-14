


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


singleton42 () = 42

getAnythingAndReturnNothing _ = ()

gdc a 0 = a
gdc 0 b = b

gdc a b | b == 0    = a 
        | a == 0    = b 
        | a > b     = gdc b (rem a b)
        | otherwise = gdc b (rem b a)

composition f g = f . g

add2 x = x + 2
mul2 x = x * 2

aIdentity x = x
idF x f = composition aIdentity f x
fId x f = composition f aIdentity x

mp = composition add2 mul2 

mp3 = mp 3

gdc42_13 = gdc 42 36 

main = do
 putStr "Hello \n"
 print gdc42_13 
 print mp3 
 print (idF 1 add2 == fId 1 add2)
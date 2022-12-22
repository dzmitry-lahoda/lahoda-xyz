# RSA


Let $m$ to be message, $e$ to be exponent, $N$ some number, then $c$ is defined:

$$ m^e \mod N \equiv c $$

, which is easy to calculate.


While backward

$$c^d \mod N \equiv m$$ 

is hard, until you know special $d$.

So $d$ - is decryption private key.

$e$ - is encryption public key.

And the number $d$ allows to decrypt:

$$c^d \mod N = (m^e \mod N)^d \mod N \equiv (m^e)^d \mod N = m^(e * d) \mod N = m$$
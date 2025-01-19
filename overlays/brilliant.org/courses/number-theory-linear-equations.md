

It tells there are Diophantine equations. In these we are interested only in integer solutions.

Example, integer solution x^2 + x^2 = 5^5 (on circle line).

There are specific class is linear integer equations.

Than it showed how to solve ax + by = c via Euclidian algorithm.

It proved, trivial, that gdc(a,b)=d => d|c. Via Bezout lemma.

They showed how billiard ball bouncing can help to come up to conclusion.


## Division

Usual division tells us that `c * 1/c = 1`. 
So number multiplied by its inverse give 1.

Let have `s = 1/c mod n`.
Multiply by c, and get

`sc = c * 1/c mod n` =>

`sc = 1 mod n`

By theorem `gdc(sc, n) = 1` =>

`gdc(c, n) = 1`

So if solution exists as `s` it is called `multiplicative inverse`.

If `s` is `multiplicative inverse` of `c`.

Let `c a = b mod n` => 

`(s * c) * a = s b mod n` =>

`s * a = b mod n`

Use definition of mod to make multiplier and module coprime.

**Divide multiplier and module by GDC, and then invert multiplier**


## Recursive sequences
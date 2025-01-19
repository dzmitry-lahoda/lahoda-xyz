Language is set of 01 strings.
NP language statement can be verified in polynomial time.
With Completeness, true claims have short proofs.
And Soundness, false states has no proofs.

## Interactive

Verifier can accept false proofs with negligible probability.

**Example: 2 color paper and blind verifier**

Prover wants to prove he has 2 color page.
He sends page to blind Verifier.
He tosses count and flips page in case of head.
Gives back to prover. 
And ask prover if page was flipped or not.
If color is 1 or 3, repeating, will show that he lied.

**Example: quadratic residue**

{ (N, y) : E x   y = x ** 2 mod N }

(N,y) are part of quadratic residue language.


1. P: chose r : 1 <= r <= N, gdc(r, N) = 1.
2. P: send to V $ s = r ^ 2 mod n $  
3. If P sends sqrt(s) and sqrt (s y mod N), V will convinced (N, y).
4. V flips a coin. if heads ask z = r, else z = r sqrt(y) mod N.
5. So if it heads, he accepts z^2 = s mod N, else z^2 = s y mod N  


## Zero Knowledge 



V simulates his view, and real and simulated views are indistinguishable.


Intertive Protocol PV is ZK if for Language E Sim V x e L so that next are poly time indistuinguishable:

1. view_v (P,V)[x] = 

2. Sim (x, 1^lambda) 

and is complete, sound and ZK

V is possibly malicus (binzantine, non hones) verifier.

### 3 coloring
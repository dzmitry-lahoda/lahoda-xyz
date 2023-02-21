# Diffie-hellman

Is based on fact  that if $ a^n mod b = k$, it is easy to calculated forward. 
But finding $n$ if you do not know it hard.


```haskell
alice_private_number = 123 
bob_private_number = 42 
base_public = 17 
modulo_public = 666 

alice_mix_public = (base_public ^ alice_private_number) `mod` modulo_public
bob_mix_public   = (base_public ^ bob_private_number) `mod` modulo_public

alice_shared = (bob_mix_public ^ alice_private_number) `mod` modulo_public
bob_shared = (alice_mix_public ^ bob_private_number) `mod` modulo_public
shared = bob_shared == alice_shared
```
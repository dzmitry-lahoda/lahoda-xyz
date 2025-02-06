inductive Even
  | zero
  | addTwo : Even -> Even

def Even.plus : Even -> Even -> Even
   | Even.zero, b => b
   | Even.addTwo a, b => Even.plus a (Even.addTwo b)

instance : Add Even := ⟨Even.plus⟩

instance : OfNat Even n where
  ofNat :=
    let rec natPlusTwo (n:Nat) : Even :=
      match n with
      | 0 => Even.zero
      | n + 1 => Even.addTwo (natPlusTwo n)
    natPlusTwo n

def Even.toNat : Even -> Nat
  | Even.zero => 0
  | Even.addTwo n => 2 + n.toNat

instance : ToString Even where
  toString := toString ∘ Even.toNat

#check Even.zero
#check Even.zero + Even.zero

def a : Even := 2

#eval a
#eval a + a

#eval a +  (a+1)

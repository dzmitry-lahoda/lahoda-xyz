

class IsEven (n: Nat) where
  proof: n % 2 = 0

instance : IsEven 0 where
  proof := rfl

instance : IsEven n where
  proof :=
    let rec pr :=
      match n % 2 with
        | 0 => rfl
        | n + 2 => pr n
        | 1 => failure "asd"
    pr
structure EvenNat where
  val: Nat
  isEven: val % 2 = 0

instance [IsEven n] : OfNat EvenNat n where
  ofNat := { val := n, isEven := IsEven.proof  }

instance : ToString EvenNat where
  toString e := toString e.val


-- def one : EvenNat := 1 -- fails

def two : EvenNat := 2

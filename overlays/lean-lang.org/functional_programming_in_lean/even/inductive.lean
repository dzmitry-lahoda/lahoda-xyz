inductive EvenInductive : Nat -> Type where
  | zero: EvenInductive Nat.zero
  | addTwo {n: Nat}: EvenInductive n -> EvenInductive (n+2)

instance {n:Nat} : OfNat EvenInductive n where
  ofNat :=
    let rec aux (n: Nat) : EvenInductive n :=
      match n with
      | 0 => EvenInductive.zero
      | n + 1 => aux n
    aux n

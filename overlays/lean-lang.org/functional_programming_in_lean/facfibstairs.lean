

-- def fac_top_down(n: Nat): Nat :=
--  n * fac_top_down (Nat.pred n)

-- bottom up?


def fac : Nat -> Nat
  | 0 => 1
  | n + 1=> (n + 1) * fac n

#eval fac 4

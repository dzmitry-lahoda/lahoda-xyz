structure Pos where
  succ::
  pred: Nat

partial def Pos.plus (a:Pos) (b:Pos) : Pos :=
  match b.pred with
  | 0 => Pos.succ (Nat.succ a.pred)
  | Nat.succ n => Pos.plus (Pos.succ (Nat.succ a.pred)) (Pos.succ n)

instance : Add Pos where
  add := Pos.plus

instance : OfNat Pos (n+1) where
  ofNat := Pos.succ n

instance : ToString Pos where
  toString p := toString (p.pred + 1)

def Pos.one := 1

def Pos.mul: Pos → Pos → Pos
  | Pos.succ 0, b => b + b
  | Pos.succ (a+1), b => (Pos.mul (Pos.succ a) b) + b

instance : Mul Pos where
  mul := Pos.mul

#eval Pos.plus 13 11

#eval Pos.plus 3 3

def pp: Pos := 1

def ppp: Pos := 33333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333
def zzz: Pos := 22222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222

#eval pp
#eval ppp

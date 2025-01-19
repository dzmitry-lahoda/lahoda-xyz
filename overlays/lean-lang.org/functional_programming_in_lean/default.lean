def maximum (n: Nat) k := if n < k then k else n

def bigger (n: Nat): Nat -> Nat := maximum n

def Str: Type := String

def NatNum: Type := Nat

def nat_num: NatNum := (42 : Nat)

abbrev NN := Nat

abbrev Size := Nat
abbrev Price := Nat


structure Limits (α : Type) (β: Type) where
  size: α
  price: β
deriving Repr



def Limits.reduceSize (l: Limits Nat Nat) (n: Size) : Limits Nat Nat :=
  { l with size := l.size - n}

structure Order where
  limit: Limits Nat Nat
deriving Repr

inductive FillMode where
  | fillOrKill : FillMode
  | immediateOrCancel : FillMode
  | postOnly : FillMode
  | limit : FillMode


def is_immediate_or_cancel(f:FillMode): Bool :=
  match f with
    | FillMode.immediateOrCancel | FillMode.fillOrKill  => true
    | _ => false


def some_list := [1,2,3]

def more_list := List.cons 1 some_list

inductive Side where
  | buy : Side
  | sell : Side

def side := Side.buy

def x: Int := [].head!

def distributedProductOverSums (α: Type) (β: Type) (γ: Type) (i: α × (β ⊕ γ )) : (α × β) ⊕ (α × γ) :=
 match i with
  | (a, Sum.inl b) => Sum.inl (a, b)
  | (a, Sum.inr b) => Sum.inr (a, b)


-- this is same
def propositonAndTheorem : (2 + 3 = 5) := rfl
-- as this
def Proposition : Prop := 2 + 3 = 5
theorem justtheorem : Proposition := by
  rfl
-- ad as this
theorem justtheorem2 : 2 + 3 = 5 := by
  simp

theorem HelloWorld: "Hello".append "World" == "HelloWorld" := by rfl

theorem xxx: 5 < 17 := by
  simp


def refactor1 (x: Nat) (y) :=
  y/ x + y + x - 2

def refactor2 (x: Nat) (y) :=
  y + x + y/ x - 2

-- why cannot prove
-- theorem xxxx : refactor1 = refactor2 := by
--   simp


#eval Nat.succ 42

#eval maximum 42 13

#eval ({ size := 42, price := 13: Limits Nat Nat }).reduceSize 2

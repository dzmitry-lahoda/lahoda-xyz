
let ap (z:int list) = z
let ai (z:list<int>) = ap z
let aii (z:list<int>) = z |> ai
type X = int seq

type bs<'a> = Bs of 'a
type 'a bp = Bp of 'a

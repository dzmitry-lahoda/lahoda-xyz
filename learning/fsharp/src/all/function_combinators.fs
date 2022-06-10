
// TODO:
let Q x y z = y (x z)  // z is of type 'a and return value is of type c'
let Q1 (z:float) = int z + 1 // round
let Q2 (z:int) = z.ToString()

let Q3 = Q Q1 Q2

let S x y z = x z (y z) // Starling


let rec Y f x = f (Y f) x  // Y or Sage combinator


let a1 f1 x =  (int (round (f1 (float x)))) 



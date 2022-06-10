
module P =
  type T = {A:string; B:string} with
    member this.AB = 
      (+) this.A this.B

  let create a b = 
      {A=a;B=b} 
    
  // partial
  type T with 
    member ignore.FooBar = 1

// extenstion
module P2 =
  open P
  type T with 
    member me.FizzBuzz = "2"
  type P.T with 
    member me.FizzBuzz2 = 2.0    

// prelude
[<AutoOpen>]
module P3 =
  type P.T with
    member _.X = "123"

open P2
let p = P.create "1" "2"
p.AB
p.FooBar
p.FizzBuzz
p.FizzBuzz2
p.X

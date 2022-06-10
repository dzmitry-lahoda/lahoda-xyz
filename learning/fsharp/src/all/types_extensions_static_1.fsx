
module P =
  type T = {A:string; B:string} with
    member this.AB = 
      (+) this.A this.B

  type T with 
     static member create a b = 
       {A=a;B=b} 


open P
let p = T.create "1" "2"



// unit / zero arguemnts 
let s0 = System.Guid.NewGuid
let sUnit = System.Guid.NewGuid ()
let a() = 1

type QWE = {asd:int}
// pattern match in parameter
let f {asd=z} = z * 2

(* fails
let b = a unit
let unitEquals = () = unit
let s0Error = System.Guid.NewGuid unit
*)

let addConfusingTuple (x,y) = x + y

// error 
// addConfusingTuple 1 2 

let (~%%) (s:string) = s.ToCharArray()
// unary operator
let result = %% "hello"
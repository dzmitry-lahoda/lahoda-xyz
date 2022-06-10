module Tests3

open Expecto


// decompose on let binding
type A = {B:int;C:float}
let a = {B=1;C=1.0}
let {C=c} = a

type R<'a, 'b> =
    | S of 'a
    | F of 'b

type E =
    | NotFound of string
    | Auth of string * System.Exception


let file action path = 
    try
      use file = new System.IO.StreamReader(path:string)
      let result = action file
      S result
    with
     | :? System.IO.FileNotFoundException as ex
       -> F (NotFound path)


[<Tests>]
let tests =
  testList "samples" [    
    test "Exception wrapper" {
      let r = file (fun x -> x.ReadLine() |> ignore) "any random sring.foo bar baz"
      match r with
       | F (NotFound path) -> ()
       | _ -> Expect.equal "1" "2" "failt"
    }
  ]

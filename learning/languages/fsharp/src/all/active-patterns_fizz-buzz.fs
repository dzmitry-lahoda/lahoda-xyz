module fluent_shape
open Expecto


[<EntryPoint>]
let main argv =

    let (|Mo3|_|) x = if x % 3 = 0 then Some Mo3 else None 
    let (|Mo5|_|) x = if x % 5 = 0 then Some Mo5 else None

    let fb x =
      match x with 
      | Mo3 & Mo5 _ -> printfn "FizzBuzz"
      | Mo3 -> printfn "Fizz"
      | Mo5 -> printfn "Buzz"
      | _ -> printfn "Any"

    0

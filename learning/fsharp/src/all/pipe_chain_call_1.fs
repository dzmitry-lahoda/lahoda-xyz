

// |>
let (|||) x f = f x 
let (|>>) x f = f x

let a (b:int) (c:string) = b.ToString() + c |> System.Single.Parse

let z r t y = 
    if (y <> 3) then raise (System.Exception("Foo!"))
    r + t + y

let q = 3 |> z 1 2



let b = 1 |> a
let c = b "2"

let d = a <| 1

let w = 1 + 1 |> a <| "a" + "b"
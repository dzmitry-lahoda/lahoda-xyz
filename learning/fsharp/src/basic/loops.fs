
module System


/// Imitates "for loop" using recursion
let rec forLoop body times =
    if times <= 0 then
        ()
    else
        body()
        forLoop body (times - 1)


/// Imitates "while loop" using recursion
let rec whileLoop predicate body =
    if predicate() then 
        body()
        whileLoop predicate body
    else
        ()

let rec (!) x =
    if x <= 1 then 1
    else x * !(x - 1)

// was able to use ~ prefix in earlier versions of F#
let (=!=) (str:string) (contains:string) =
    str.Contains contains

let f a = a+a
let x = 2
let g = (*)

printfn "The result of sprintf is %s" <| sprintf "(%d, %d)" 1 2;;

[ [1]; []; [4;5;6]; [3;4]; []; []; []; [9] ] |> List.filter (List.isEmpty >> not) |> ignore;;
module Tests5

[<NoComparison; NoEquality>]
type CustomerAccount = {Id : string}

let x = { Id = "Joe"}

//The type 'CustomerAccount' does not support the operator '=='
//let f = x == x

let sum1 l = List.reduce (fun sum x -> sum + x) l
let sum2 = List.reduce (+)
let sum3 = 0 |> List.fold (+)
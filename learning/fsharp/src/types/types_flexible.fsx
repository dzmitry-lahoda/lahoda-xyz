


open System

let iterate (f : unit -> int seq) =
    for a in f() do printfn "%d" a

// contravariance alike
let iterate2 (f : unit -> #seq<int>) =
    for a in f() do printfn "%d" a

iterate (fun () -> [1] :> int seq)

iterate2 (fun () -> [1])



 
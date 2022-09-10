


let a = [ 1; 2; 3 ]
let b = [ '1'; '2'; '3' ]

let c = List.zip a b

printfn "%b" (c.[0] = (1,'1'))
printfn "%b" (c.[2] = (3,'3'))

let d = List.filter (fun x -> x < 2) a

printfn "%b" (d.Length = 1)
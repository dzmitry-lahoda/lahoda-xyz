



let f a b c = if a then b else c
let g a b c = if a then b() else c()


f true (printfn "a") (printfn "b") // eager
g true (fun ()-> printfn "a") (fun ()-> printfn "b")
let l = f true (lazy (printfn "a")) (lazy (printfn "b")) // very bad
l.Force()
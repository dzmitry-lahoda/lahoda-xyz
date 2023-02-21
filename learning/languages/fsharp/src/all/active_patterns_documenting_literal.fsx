

// wrapper 
let (|A|) (a:string) = "a" + a
let (|B|) (A a) = ("b", a)

let c (B b) =
  let (q,w) = b
  printfn "%s" q
  printfn "%s"w


let q = c "123"
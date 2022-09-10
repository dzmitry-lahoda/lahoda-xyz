//// what maximal number each of these similar funcions will output?

let t1() = 
  let mutable v1 = 0
  let seq1 = 
    seq {
      for i = 1 to 3 do
        v1 <- v1 + 1
        yield v1
    }
  printfn "v1 = %d" v1
  seq1 |> Seq.length |> printfn "seq1 length %d" 
  printfn "v1 = %d" v1
  seq1 |> Seq.length |> printfn  "seq1 length %d" 
  printfn "v1 = %d" v1

let t2() =
  let mutable v2 = 0
  let seq2() = 
    seq {
      for i = 1 to 3 do
        v2 <- v2 + 1
        yield v2
    }

  printfn "v2 = %d" v2
  seq2() |> Seq.length |> printfn "seq2 length %d" 
  printfn "v2 = %d" v2
  seq2() |> Seq.length |> printfn  "seq2 length %d" 
  printfn "v2 = %d" v2
 
let t3() =
  let mutable v3 = 0
  let seq33() = 
    seq {
      for i = 1 to 3 do
        v3 <- v3 + 1
        yield v3
    }
  let seq3 = seq33()
  
  printfn "v3 = %d" v3
  seq3 |> Seq.length |> printfn "seq3 length %d" 
  printfn "v3 = %d" v3
  seq3 |> Seq.length |> printfn  "seq3 length %d" 
  printfn "v3 = %d" v3
  
let t4() =
  let mutable v4 = 0
  let seq44 = 
    lazy seq {
      for i = 1 to 3 do
        v4 <- v4 + 1
        yield v4
    }
  let seq4 = seq44.Value
  printfn "v4 = %d" v4
  seq4 |> Seq.length |> printfn "seq4 length %d" 
  printfn "v4 = %d" v4
  seq4 |> Seq.length |> printfn  "seq4 length %d" 
  printfn "v4 = %d" v4

t1()
t2()
t3()
t4()
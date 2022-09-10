

let a1 (filename:string) = new System.IO.StreamReader(filename)
let a2 filename = new System.IO.StreamReader(filename:string)
let a3 filename = new System.IO.StreamReader(path=filename)

let filename = "666"
let a4 = new System.IO.StreamReader(filename)


(*
let afails filename = new System.IO.StreamReader(filename)
*)
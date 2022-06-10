

// creating arrays

let a1 = [|1; 2; 3|]

let a2 = [|
    1
    2
    3
|]

// from expression
let a3  = [| for i in 1 .. 3 -> i|]
let a33 = [|1..3|]
let a4:int array = Array.zeroCreate 3
a4.[0] <- 1
a4.[1] <- 2
a4.[2] <- 3

let a5 = Array.create 3 1
let a6 = Array.init 3 (fun index -> index + 1)

// slice array
let b1 = a1.[..2]
let b2 = a2.[1..]
let b3 = a3.[1..2]
let b4 = Array.sub a4 1 2


// access
Array.set a4 1 1
let c4 = Array.get a4 2
if c4 <> 3 then failwithf "%A" c4

// concat into new
let d1 = Array.append a1 a2

let d2c = Array.choose (fun elem -> 
    if elem % 2 = 0 then Some(float elem) else None
 ) 
let d2 = d2c a1
    
if (d2.Length <> 1) then failwithf "Wrong filter %A" d2

let s1 = Array.collect (fun elem -> [|1..elem|]) a1
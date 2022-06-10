

let sum a b  = a + b

let sum2 a =
    let sum3 b = a + b
    sum3

// curried into the same
let a = sum 1 2
let b = sum2 1 2
let d = (sum 1) 2
let e = (sum2 1) 2
let f = (+) 1 2
let g = ((+) 1) 2




//z = 'c
// y = 'c -> 'a
// x = 'a -> 'b 
// ret = 'b
let F x y z = x (y z)
  
let id x = x
let compose f g x = g ( f x)
let (>>>) f g x = g (f x)

let add1 x = x + 1
let divide2 x = x / 2

let addDivide x =  divide2 (add1 x)
let composeAddDivide x = compose add1 divide2 x
let addDivide1 = addDivide 1
let addDivide2 = composeAddDivide 1

let a x = x * 2.0
let b x = x > 1.0

let c1 x =
    let y = a x
    b y

let c2 = compose a b



let c3 = a >> b


let c4 =  (+) 1 >> (*) 2
let rep r = r >> r

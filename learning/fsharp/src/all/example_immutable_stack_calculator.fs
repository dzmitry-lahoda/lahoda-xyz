

type Stack = StackContent of float list

// pushes value onto stack
//let push stack value= 
    //let (StackContent content) = stack
    //let updated = value::content
    //StackContent updated
let EMPTY = StackContent []

// use stack first to produce DSL
let push value (StackContent content) = StackContent (value::content)     // via pattern matchig

let one = push 1.0 EMPTY

let two = push 2.0 one

let ONE = push 1.0
let TWO = push 2.0
let THREE = push 3.0
let FOUR = push 4.0
let FIVE = push 5.0

let operation = EMPTY |> ONE |> FOUR

let pop (StackContent content) =
    match content with
    | top::rest -> (top, StackContent rest)
    | [] -> failwith "Stack underflow"

let OP op stack =
    let (x,stack') = pop stack
    let (y,stack'') = pop stack'
    push (op x  y) stack''

let MUL = OP (*)  
let ADD = OP (+)  
let DIV= OP (/)  
let DUP stack = 
  let (x,_) = pop stack
  push x stack

let SWAP stack = 
    let x,s = pop stack  
    let y,s' = pop s
    push y (push x s') 

let SHOW stack= 
  let (x,_) = pop stack
  printfn "%f" x
  stack

let SQUARE = DUP >> MUL

let SUM_UP_TO = 
  DUP 
  >> ONE >> ADD >> MUL
  >> TWO >> SWAP >> DIV 
  >> SHOW
  
let calc = EMPTY |> ONE |> TWO |> ADD

EMPTY |> FIVE |> SUM_UP_TO



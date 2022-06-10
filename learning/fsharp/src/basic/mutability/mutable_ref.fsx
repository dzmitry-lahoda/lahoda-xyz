


// update value of first with value of second.
// bind second to same as first
let inline updateConnect e1 e2 = fun () -> let ev = !e1 in ev:=!(!e2); e2:=ev
let inline update e1 e2 = fun () -> let ev = !e1 in ev:=!(!e2); e2:=ev


let a = ref 1
let b = ref 2
let c = ref 3
let aa = ref a
let bb = ref b
let cc = ref c

let uc1 = updateConnect aa bb
uc1()

let uc2 = updateConnect aa cc
uc2()

let aaa = !(!aa)
let bbb = !(!bb)
let ccc = !(!cc)



type A() = 
  member val a = "a" with get, set 

 
let mutable a = new A()
a.a <- "b"

// is mutable inside
let b = new A()
b.a <- "b"



open System.IO

type A() = 
  let mutable a = "a"
  member val AutoGetProperty = "b" //readonly
  member val AutoGetSetProperty = "c" with get, set //"c" is evaulated once, if changed value - still the same
  member this.GetOnlyProperty with get() = "a"
  member this.SetOnlyProperty with set v = a <- v
  member this.GetSetProperty 
    with get() = a
    and set v = a <-v


let mutable a = new A()
a.AutoGetSetProperty <- "e"
printfn "%s" (a.AutoGetSetProperty)


 

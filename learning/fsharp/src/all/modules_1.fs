module TopLevel

 do printfn "S tatic init was called"

 module ChildModule = 
    let a() =1s
 
 [<RequireQualifiedAccess>]
 module CannotOpen =
    let a() = 2.0   
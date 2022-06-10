// See the 'F# Tutorial' project for more help.
// Learn more about F# at http://fsharp.net

open operators

[<EntryPoint>]
let main argv = 
  let x = new operators.X()
  printfn "b = %s" (x.null_value |?? x.b )
  printfn "null %s" (x.null_value |? x.b )
  printfn "null %s" (x.b |? x.null_value)
  printfn "b = %s" (trywith x.c x.b) 
  printfn "null %s" (trywith x.d x.b )
  printfn "b = %s" (trywithnull x.d x.b)
  
  0 // return an integer exit code


let a = 
  {new System.IDisposable with
    member this.Dispose() =
        printfn "Disposed"
  }

a.Dispose()    

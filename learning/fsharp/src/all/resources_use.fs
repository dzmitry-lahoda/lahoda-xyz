


let r = { new System.IDisposable with 
                member this.Dispose() = printfn "disposed"
         }

let b x = System.Console.WriteLine(x.GetType())

using (r) b
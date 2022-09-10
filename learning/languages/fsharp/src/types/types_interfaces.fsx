
type x() =
  interface System.IDisposable with
    member this.Dispose() = ()

let y = new x()
(y :> System.IDisposable).Dispose()
let dispose (z:System.IDisposable) = z.Dispose()
dispose y
let dodo =
    use z = new x()
    ()
(*
y.Dispose()
*)

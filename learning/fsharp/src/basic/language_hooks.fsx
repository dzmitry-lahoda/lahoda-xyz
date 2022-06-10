
type Wrapped<'a> = Wrapped of 'a


type wrapBuilder() =
  member this.Bind(wrapped:Wrapped<'a>, func: 'a-> 'b Wrapped) =
    System.Console.WriteLine("Bind " + wrapped.ToString())
    match wrapped with
    | Wrapped(inner) -> func(inner)
  member this.Return inner = 
    System.Console.WriteLine("Return " + inner.ToString())
    Wrapped(inner)

let wrap = new wrapBuilder()

let x = 
    wrap {
        System.Console.WriteLine("Before Bind")
        let! z = Wrapped(1)
        let result = z + 1
        System.Console.WriteLine("Before Return")
        return z
    }
x


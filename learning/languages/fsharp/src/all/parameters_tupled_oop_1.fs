


type A = {a:string;b:float} with
    member this.B foo bar = this.b * foo - bar 
    member this.C(foo,bar) = this.b * foo - bar 
    member this.D(foo,?bar) = this.b * foo - defaultArg bar 13.0
    // overload
    member this.C(foo) = this.b * foo
    member this.P = printfn "%s" (this.a)


let x = {a= "a"; b = 1.0}
// named parameters
let q = x.C(bar =3.0, foo = 2.0)
let w = x.C(foo = 2.0, bar =3.0)
// optional
let e  = x.D(foo = 2.0)

// not type inference
//let q a = a.P;

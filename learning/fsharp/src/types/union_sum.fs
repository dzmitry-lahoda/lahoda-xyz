namespace types
open System

module union_sum =


    [<ReflectedDefinitionAttribute>]
    type SumUnionTaggedReflected = 
        | A of int
        | B of string

    type SumUnionTagged = 
        | A of int
        | B of string    
        | D of namedCase : double

    [<StructAttribute>]
    type SumUnionTaggedStruct = 
        | A of A : int
        | B of B : string    
        | C of C : byte

    let test() =
        let b = A 1
        let c = SumUnionTaggedReflected.A 1
        let z = D 2.0
        ()
namespace PersonNamespace


type PersonType = {Name:string; Surname:string}

// better works with C#
module Person =
   let create name surname = {Name=name; Surname = surname}
   let print {Name=name;Surname=surname} = printfn "%s" name
   let printName {PersonType.Name=name;} = printfn "%s" name

module Example =

   let person = Person.create "joe" "doe"
   Person.print person


// inline person, more F# centic
module Persom2 =
   type T = {N:string;S:string}
   let create name surname = {N=name; S = surname}
   let print {T.N=name;} = printfn "%s" name

module Do =
  let c = Persom2.create "1" "2"
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open FsCheck
open Swensen.Unquote
open FsCheck
open generating_data
let mod5 x = x % 5

let modIsLessThan5 x =
    x < 5

let modIsLessThan4 x =
    x < 4

let generateTests() =
  let  goodProperty num =
    mod5 num |> modIsLessThan5
  Check.QuickThrowOnFailure goodProperty

  try
    let badProperty num =
      mod5 num |> modIsLessThan4
    Check.QuickThrowOnFailure badProperty
    with
      | :? System.Exception as ex -> printfn "%s" (ex.GetType().FullName + ex.Message)
   


let unqouteTest() =
    2 =! 2
    test <@ 2 = 2 @>
    try
      2 =! 3
    with
      | :? Swensen.Unquote.AssertionFailedException as ex -> printfn "%s" (ex.GetType().FullName + ex.Message)


[<EntryPoint>]
let main argv = 
    Tests.runTestsInAssembly defaultConfig argv |> ignore
    generatorTests()
    //unqouteTest()
    //generateTests()
    printfn "%A" argv
    0 // return an integer exit code

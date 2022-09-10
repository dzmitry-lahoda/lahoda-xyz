module All
open Expecto

[<EntryPoint>]
let main argv =
    runTestsInAssembly defaultConfig argv
    //runTestsWithArgs defaultConfig args tests
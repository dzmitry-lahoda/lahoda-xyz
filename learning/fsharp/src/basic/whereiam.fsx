/// outputs location and name of script
printfn "Application Path:"
let path =  System.IO.Path.Combine(__SOURCE_DIRECTORY__,__SOURCE_FILE__)
printfn "%s" path

printfn ""
printfn "Directory:"
printfn "%s" __SOURCE_DIRECTORY__

printfn ""
printfn "Filename:"
printfn "%s" __SOURCE_FILE__




// run 
// fsi runscript.fsx

fsi.ShowProperties <- true

printfn "first argument is %s" (fsi.CommandLineArgs.[0])

// hangs
//fsi.EventLoop.Invoke(fun () -> printfn "Invoke"; 1)


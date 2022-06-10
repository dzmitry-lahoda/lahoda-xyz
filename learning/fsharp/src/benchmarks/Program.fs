open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Jobs
open BenchmarkDotNet.Running
open BenchmarkDotNet.Validators
open BenchmarkDotNet.Exporters
open BenchmarkDotNet.Environments
open System.Reflection
open BenchmarkDotNet.Configs

type SleepMarks () =
    
    member val public donotcare = false with get, set

    member val startInt32 = 1_000_000
    member val startInt64 = 1_000_000
    
    [<Params(100, 100000)>]
    member val public loop = 0 with get, set
    
    [<Benchmark>]
    member this.ForToInt () = 
        let up = this.startInt32 + this.loop  
        for i = this.startInt32 to up do
            this.donotcare <- i % 2 = 0

    [<Benchmark>]
    member this.ForInInt () = 
        let up = this.startInt32 + this.loop  
        for i in this.startInt32 .. up do
            this.donotcare <- i % 2 = 0

let config =
     ManualConfig
            .Create(DefaultConfig.Instance)
            .With(Job.ShortRun.With(Runtime.Core))
            .With(MemoryDiagnoser.Default)
            .With(MarkdownExporter.GitHub)
            .With(ExecutionValidator.FailOnError)

let defaultSwitch () =
        Assembly.GetExecutingAssembly().GetTypes() |> Array.filter (fun t ->
            t.GetMethods ()|> Array.exists (fun m ->
                m.GetCustomAttributes (typeof<BenchmarkAttribute>, false) <> [||] ))
        |> BenchmarkSwitcher


[<EntryPoint>]
let main argv =
    defaultSwitch().Run(argv,config) |> ignore
    // BenchmarkRunner.Run<SleepMarks>(config) |> ignore
    0 // return an integer exit code
open Expecto
open System
open System.IO

// open Microsoft.FSharp.Control.CommonExtensions
// open System.IO

// [<EntryPoint>]
// let main argv =
//     use stream = new MemoryStream()
//     do stream.WriteByte 1uy
//     do stream.WriteByte 2uy
//     let memory = Memory<byte>.Empty
//     let (read:Async<byte[]>) = stream.AsyncRead 1
//     let asyncRead = async {
//                         let (read1:Async<byte[]>) = stream.AsyncRead 1
//                         let! (read2:byte[]) = stream.AsyncRead 1
//                         return (read1, read2)
//                         }

let createTimerAndObserbable ms =
    let t = new System.Timers.Timer(float ms)
    t.AutoReset <- true 
    let observable = t.Elapsed
    let a = async {
        t.Start()
        do! Async.Sleep 5000
        t.Stop()
    }

    (observable, a)

type FizzBuzzEvent = {label:int; time:DateTime}

let atMoment (earlier, later) =
    let {label=_; time = t1} = earlier
    let {label=_; time = t2} = later
    t2.Subtract(t1).Milliseconds < 50

[<EntryPoint>]
let main argv =
    Tests.runTestsInAssembly defaultConfig argv

    let t1, a1 = createTimerAndObserbable 300
    let t2, a2 = createTimerAndObserbable 500

    let e1 = t1 |> Observable.map (fun _ -> {label = 3; time = DateTime.Now});
    let e2 = t2 |> Observable.map (fun _ -> {label = 5; time = DateTime.Now});

    let both = Observable.merge e1 e2
    let pairwise = both |> Observable.pairwise 
    let atMoment, notAt = pairwise |> Observable.partition atMoment
    let fizz, buzz = notAt |> Observable.map (fun (ev1, _) -> ev1) |> Observable.partition (fun x -> x.label = 3)

    both |> Observable.subscribe (fun x -> printfn "Combined %A" (x.label, x.time.Second, x.time.Millisecond))
    atMoment |> Observable.subscribe (fun x -> printfn "FizzBuzz")
    fizz |> Observable.subscribe (fun x -> printfn "Fizz")
    buzz |> Observable.subscribe (fun x -> printfn "Buzz")

    [a1;a2] |> Async.Parallel |> Async.RunSynchronously |> ignore
    0
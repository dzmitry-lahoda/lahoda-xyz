module AsyncClassTests
open Expecto
open System.Collections.Concurrent
open System.Threading
open System.Timers
open System.IO


let dumpEnv message =
  printfn "ThreadId = %d;Message = %s" (Thread.CurrentThread.ManagedThreadId) message

let childWithSleep time (items:ConcurrentQueue<string>)  = async {
        dumpEnv("before sleep in childWithSleep")
        do! Async.Sleep time
        dumpEnv("after sleep in childWithSleep")
        items.Enqueue("childWithSleep")       
    }

let parentWithChild items = async {
        let! child = Async.StartChild (childWithSleep 1000 items)
        dumpEnv("before sleep in parentWithChild")
        do! Async.Sleep 100
        dumpEnv("after sleep in parentWithChild")
        items.Enqueue("parentWithChild")
        do! child
        return ()              
    }

let catch func = 
  try 
    func() |> Result.Ok

  with 
    | e -> e |> Result.Error

type CartMessage =
 | Add of string
 | Remove of string
 | Pay of decimal

// TODO: make cart and shop to be different mail boxes
type Shop = {Items:string list; Payed: decimal}

let updateShop shop msg =
    match msg with
    | Add x -> {shop with Items = x :: shop.Items}
    | Pay x -> {shop with Payed = x}
    | _ -> shop


let ignoreAgent = MailboxProcessor<obj>(fun inbox -> 
  let loop() = async {
    return ()
  }
  loop()
)

module MasterSlave = 
  type Reply = MailboxProcessor<SlaveMessage> -> unit
  and Action = unit -> Async<unit>
  and MasterMessage = 
    | Start
    | Stop
    | Done of MailboxProcessor<SlaveMessage>
  and SlaveMessage = 
    | Start of Reply * Action
    | Stop
    
  let slaveFactory() = MailboxProcessor<SlaveMessage>(fun inbox -> 
    let rec loop() = async {
      let! message = inbox.Receive()
      match message with 
      | Stop ->
        return ()
      | Start (reply,act) ->
          do! act()
          reply(inbox)
          return! loop()
    }
    loop()
  )

  type MasterState = {inFlightMaximum:int; current:int; slaves:MailboxProcessor<SlaveMessage> list}

  let master slaveCount action = MailboxProcessor<MasterMessage>(fun inbox -> 
    let rec loop(state) = async {
      let! message = inbox.Receive()
      match message with 
      | MasterMessage.Start ->
         if List.length state.slaves < state.inFlightMaximum then
            let slave = slaveFactory()
            inbox.Post(MasterMessage.Start)
            return! loop({state with slaves = List.append state.slaves [slave] } )
         else 
            let doneMessage(s) = inbox.Post(Done s)
            state.slaves |> List.iter (fun x -> x.Start(); x.Post(SlaveMessage.Start (doneMessage, action)))
            return! loop({state with current = List.length state.slaves} )
      | Done s ->
          let doneMessage(s) = inbox.Post(Done s)
          s.Post(SlaveMessage.Start (doneMessage, action))
          return! loop(state)
      | MasterMessage.Stop -> 
        state.slaves |> List.iter (fun x -> x.Post(SlaveMessage.Stop);)
        return ()
      | _ ->
        return! loop(state)
    }

    loop({inFlightMaximum = slaveCount; current = 0; slaves = []})
  )

let cart = MailboxProcessor<CartMessage>.Start (fun inbox -> // inbox is channel
    let rec loop shop = async {
        let! message = inbox.Receive()
     
        let shop' = updateShop shop message        
        return! loop shop'
    }
    loop({Items=[]; Payed =0m})
)


[<Tests>]
let tests =
  testList "samples" [    

    testAsync "MasterSlaves" {
      let printAsync() = async {
        do! Async.Sleep(100)
        printfn "Did print Async from zlave"
      }

      let m = MasterSlave.master 4 printAsync
      m.Start()
      m.Post (MasterSlave.MasterMessage.Start)
      do! Async.Sleep 1000
      m.Post(MasterSlave.MasterMessage.Stop)
      
    }

    test "Actors" {
      cart.Post (Add "Brain")
      cart.Post (Add "Heart")     
      // TODO: pass counter agent and pass into actor so that can receive large payment and send notification      
    }

    test "parent child" {
      let items = ConcurrentQueue<string>()
      Async.RunSynchronously (parentWithChild items)
      use t = new System.Threading.CancellationTokenSource()
      Async.Start (parentWithChild (ConcurrentQueue<string>()), t.Token)
      t.Cancel()
      [parentWithChild (ConcurrentQueue<string>()); parentWithChild (ConcurrentQueue<string>())] |> Async.Parallel |> Async.RunSynchronously |> ignore
    }


    testAsync "timer" {
        use t = new Timer(1000.0)
        let a = Async.AwaitEvent (t.Elapsed) 
        let run() = Async.RunSynchronously (a, 2000, CancellationToken.None)
        Expect.isError (catch run) "Timeout"
        return ()
    }

    testAsync "read bytes" {
     
        let printTotalFileBytes =
            async {
                let! data = File.ReadAllBytesAsync(System.Reflection.Assembly.GetExecutingAssembly().Location) |> Async.AwaitTask
                return data
            }   
        let computation1 = printTotalFileBytes |> Async.RunSynchronously
        let! computation2 = printTotalFileBytes
        
        Expect.sequenceEqual computation1 computation2 "Read"
          
    }        
  ]

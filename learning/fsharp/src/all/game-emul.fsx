// plauyerB shoots PlayerA
// player A looses point

type Player = {health:int; id:int}


type GameActions =
| Shoot of int
| Heal of int

type GameEvent = { action:GameActions }

type GameState = {
   players: Player list
}


let consume gameState gameEvent =
  match gameEvent with
  | Shoot pid ->
    // find payer with id, updatre minues 1 health, return new list with update state
    let ps = gameState.players
    let p = ps |> List.find (fun x -> x.id = pid)
   
    let u = { p with health = p.health - 1}
    let pss = ps |> List.filter ( fun x -> x.id <> pid)
   
    { gameState with players = u :: pss}
   
  | Heal hpid -> gameState
   
   
let gameState = { players = [ {health = 20; id = 1 }; {health = 20; id = 2 }; ] }

let ga = [Shoot 2; Shoot 2;Shoot 1]


let lastState = List.fold consume gameState ga

printfn "%A" lastState
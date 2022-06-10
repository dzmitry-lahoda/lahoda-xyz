module Shop
open Expecto

type CartItem = string

type EmptyCart = NoItems

type ActiveCart = {UnpaidItems : CartItem list}
type PayedCart = {PaidItems : CartItem list; Amount:decimal}

type Cart = 
 | Empty of EmptyCart 
 | Active of ActiveCart
 | Payed of PayedCart


let addToEmptyState item =
    Cart.Active {UnpaidItems = [item]}

let addToActiveState cart item = 
  let n = item :: cart.UnpaidItems
  Cart.Active { UnpaidItems = n}
   
let removeFromActiveState cart item = 
  let n = cart.UnpaidItems |> List.filter (fun x -> x <> item)
  match n with 
    | [] -> Cart.Empty NoItems
    | _ -> Cart.Active { cart with UnpaidItems = n }

let payForActiveState cart amount= 
    Cart.Payed { PaidItems = cart.UnpaidItems; Amount = amount }


type EmptyCart with 
  member this.Add = addToEmptyState

type ActiveCart with
  member this.Add = addToActiveState this
  member this.Remove = removeFromActiveState  this
  member this.Pay = payForActiveState  this


let addItemToCart cart item =
    match cart with 
    | Empty x -> x.Add item
    | Active x -> x.Add item
    | Payed x -> 
      printfn "ERROR: card was payed for"
      cart

let removeItemFromCart cart item =
    match cart with 
    | Empty x ->
      printfn "ERROR: remove from emtpty"
      cart
    | Active x -> x.Remove item
    | Payed x ->
      printfn "ERROR: remove from payed"
      cart

let displayCart cart item =
    match cart with 
    | Empty x ->
      printfn "empty"
      cart
    | Active x -> 
      printfn "Unpaid %A" x.UnpaidItems
      cart
    | Payed x ->
      printfn "Paid %A of %f" x.PaidItems x.Amount
      cart

type Cart with 
  static member NewCart = Cart.Empty NoItems
  member x.Add = addItemToCart x
  member x.Remove = removeItemFromCart x
  member x.Display = displayCart x

[<Tests>]
let tests =
  testList "samples" [    
    test "Shop" {
      let cart = Cart.NewCart
      let cartAPaid =
        match cart with 
         | Empty _ | Payed _ -> None
         | Active x -> Some (x.Pay 213m)
      Expect.equal None cartAPaid "Cannot pay for emtpty"
    }
  ]

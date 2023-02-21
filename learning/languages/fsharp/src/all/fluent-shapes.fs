module fluent_shape
open Expecto

type FluentShape = {
    color:string;
    label:string;
    onClick: FluentShape -> FluentShape
}

let defaultShape = { color=""; label=""; onClick = fun shape -> shape }

let click shape =
  shape.onClick shape

let display shape =
  printfn "Label =%s Color=%s" shape.label shape.color
  shape

let setColor color shape =
  { shape with FluentShape.color = color}

let setLabel label shape =
  { shape with FluentShape.label = label}

let appendClickAction action shape = 
   { shape with FluentShape.onClick = shape.onClick >> action }

[<EntryPoint>]
let main argv =
    let setRedBox = setLabel "box" >> setColor "red"
    let changeColorOnClick color = appendClickAction (setColor color)
    let blueBox = defaultShape |> setRedBox
    blueBox 
    |> display
    |> setColor "orange"
    |> display
    |> appendClickAction (setColor "white" >> setColor "cone")
    |> click
    |> display
    let setColorAndDisplay color = setColor color >> display 
    
    let show = ["red"; "black"; "white"]
               |> List.map setColorAndDisplay
               |> List.reduce (>>)
    
    let slow x = 
        printfn "Before"
        let r  = x + 1 
        printfn "After"
        r
    let cache x = 
      let mutable d = None
      let c()  = 
        match d with
        | None -> 
          let r = slow x
          d <- Some r
          r
        | Some ss-> ss
      c
    
    let z = cache 1
    do z()
    do z()

    defaultShape |> show
    0
    

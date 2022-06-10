#r "WindowsBase.dll"
#r "PresentationCore.dll"
#r "PresentationFramework.dll"

open System
open System.Windows
open System.Windows.Controls

let list = seq {for i=1 to 10 do yield i*i}

let stackPanel = 
  let sp = StackPanel()
  
  let createTb (i:int) = 
    let tb = TextBlock()
    tb.Text <- sprintf "* %d" i
    tb
  let add x = sp.Children.Add x |> ignore
  Seq.mapi (fun i  idx -> createTb(i)) list |> Seq.iter add

let w = new Window()

//w.Content <- stackPanel
let tb = new TextBlock()
tb.Text <- "Fun"
w.Content <- tb
w.Show()

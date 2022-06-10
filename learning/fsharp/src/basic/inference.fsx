
open System
open System.IO


let test = 
  use file = new FileStream("C:/text", FileMode.OpenOrCreate)
  use reader = new StreamReader(file)

  ()
module Tests2

open Expecto

let rec movingaverage data = 
  match data with 
  | [] -> []
  | x::y::other ->
    let avg = (x + y)/2.0
    avg :: movingaverage (y::other)
  | [_] -> []      


[<Tests>]
let tests =
  testList "samples" [    
    test "Averaged" {
      Expect.equal (movingaverage [3.0;2.0]) [2.5] "Averaged"      
    }
  ]

module Tests

open Expecto

let rec quicksort data = 
  match data with 
  | [] -> []
  | first::other ->
    let lesser = other |> List.filter (fun x -> x < first) |> quicksort
    let larger = other |> List.filter (fun x -> x >= first) |> quicksort
    List.concat [lesser; [first]; larger]


[<Tests>]
let tests =
  testList "samples" [    
    test "Sorted" {
      Expect.equal (quicksort [3;2]) [2;3] "sorted"      
    }
  ]

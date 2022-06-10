module Tests4

open Expecto

[<Measure>]
type second

[<Measure>]
type meters

[<Measure>]
type lightYears =
  static member toMeters(lightYears:float<lightYears>) : float<meters> = 
      lightYears * 1500000.0<meters/lightYears>

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
    test "Units of measure" {
      let ly = 2.0<lightYears>
      //'float' does not match the type 'float<lightYears
      //let s = 2.0<lightYears> + 1.0
      let ss = 2.0<lightYears> + 2.0<_>
      let ms = lightYears.toMeters ly
      Expect.equal ms 3000000.0<meters> "measured"      
    }
  ]

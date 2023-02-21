
// arrays 2d, 3d, 4d, etc

let a = array2D 
                [
                    [1; 2]
                    [3; 4]
                ]

if a.[0, 1] <> 2 then failwith "WFT?"                

let a2 = Array2D.create 2 2 0

let a3 = Array2D.init 2 2 (fun i j -> i * j)

a3.[1, 1] <- 4

// slicing
// matrix[row start .. row finish, columnet start .. column finish]
let column1 = a.[1, *]
if column1.[0] <> 3 then failwithf "%A" column1.[0]
if column1.[1] <> 4 then failwithf "%A" column1.[1] 


let cell = a.[1..1,1..1]
if cell.[0,0] <> 4 then failwithf "WTF?"
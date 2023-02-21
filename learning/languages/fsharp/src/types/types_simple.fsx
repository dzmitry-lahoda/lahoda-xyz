type a = {b:int; c:char}
let aa = {b=1; c='a'}

printfn "%b" (aa.GetType() = typeof<a>)

type b = 
    | C of int
    | D of char


type Brec =
    | C of a
    | CC of a list

let al = [aa]

let breci = C aa
let breci2 = CC al

type StringLimited =
    | String32 of string
    | String64 of string


let stringId (s:string)= 
    let l = s.Length
    match l with
      | 32 -> String32 s |> Some
      | 64 -> String64 s |> Some
      | _ -> None

let guid =  System.Guid.NewGuid().ToString("N") |> stringId

let guid2 =  System.Guid.NewGuid().ToString("N") + System.Guid.NewGuid().ToString("N") |> stringId

let guid3 =  "not_id" |> stringId

type dummy() = let unitField = ()
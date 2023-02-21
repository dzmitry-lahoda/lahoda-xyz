


type A = B of string

let v = B "123"

let check =
    let (B vd) = v
    if (v.ToString() <> "B \"123\"" || vd <> "123") then 
        "Ops!"|> failwith 



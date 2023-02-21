module Tailcall

let count s =
    let rec countN n s =
        match s with 
        | [] -> n
        | h::t ->  countN (n+1) s
    countN 0
module operators

 
  //http://en.wikipedia.org/wiki/Null_coalescing_operator#F.23
  let (|?) lhs rhs = (if lhs <> null then rhs else lhs)

  let (|??) lhs rhs = (if lhs <> null then lhs else rhs)

  //tries left if fails does right
  let trywith<'T when 'T:null and 'T:equality> (lhs:unit->'T) (rhs:'T)  =
    try 
     lhs()
    with
     | ex -> rhs
  
  // tries left, when fails or returns null, does right
  let trywithnull<'T when 'T:null and 'T:equality> (lhs:unit->'T) (rhs:'T)  =
    try 
      let bind = lhs ()
      if bind <> null then 
        bind 
      else
        rhs
    with
     | ex -> rhs
  
  type public X() = 
    member x.null_value = 
      let str:string = null
      str

    member x.b =
       "b"
    member x.c () =
      1 / 0 |> ignore 
      "c"
    member x.d () =
      let str:string = null
      str

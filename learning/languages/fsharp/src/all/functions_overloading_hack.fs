type WTF = WTF with 
  static member inline ($) (c:char, _) = System.Console.Write c
  static member inline ($) (c:string, _) = System.Console.Write c
  static member inline ($) (c:int, _) = System.Console.Write c

let inline write x = x $ WTF

write "1"
write '1'
write 1
//write 1.0

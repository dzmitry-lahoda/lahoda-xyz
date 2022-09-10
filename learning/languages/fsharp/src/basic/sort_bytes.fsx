open System
let inline (++) (array:array<int>) i = array.[int(i)] <- array.[int(i)] + 1
let bytes = Array.zeroCreate<byte> (10 * 1000 * 1000)
System.Random().NextBytes(bytes)
let counter = Array.zeroCreate<int> (int(Byte.MaxValue)+1)
let s = System.Diagnostics.Stopwatch()
s.Start()
for b in bytes do counter ++ b
s.Stop()
s.Elapsed

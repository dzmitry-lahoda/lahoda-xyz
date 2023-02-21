``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.706 (1803/April2018Update/Redstone4)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host] : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT
  Core   : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                     Method |         N |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |
|--------------------------- |---------- |---------:|----------:|----------:|---------:|------:|--------:|
|            RawArrayIndexer | 100000000 | 187.0 ms |  3.715 ms |  4.129 ms | 186.3 ms |  1.00 |    0.00 |
|           ArrayWrapIndexer | 100000000 | 207.9 ms |  4.621 ms | 13.626 ms | 206.1 ms |  1.12 |    0.07 |
|          MemoryWrapIndexer | 100000000 | 703.1 ms | 14.650 ms | 32.156 ms | 695.3 ms |  3.80 |    0.18 |
|        ArrayViaSpanIndexer | 100000000 | 191.4 ms |  5.072 ms | 14.875 ms | 186.6 ms |  1.01 |    0.05 |
| ArrayWithParamsSpanIndexer | 100000000 | 181.1 ms |  3.724 ms |  8.096 ms | 178.5 ms |  0.99 |    0.06 |
|   ArrayViaParamSpanIndexer | 100000000 | 185.2 ms |  3.442 ms |  3.826 ms | 184.3 ms |  0.99 |    0.03 |
|              MemoryIndexer | 100000000 | 640.8 ms | 12.803 ms | 17.948 ms | 635.5 ms |  3.44 |    0.14 |
|          MemorySpanIndexer | 100000000 | 174.9 ms |  2.155 ms |  1.800 ms | 174.8 ms |  0.93 |    0.02 |

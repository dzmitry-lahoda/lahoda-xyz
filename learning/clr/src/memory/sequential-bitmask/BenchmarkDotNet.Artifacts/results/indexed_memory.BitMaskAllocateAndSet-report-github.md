``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.437 (1809/October2018Update/Redstone5)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.203
  [Host] : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  Core   : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|                                Method |       N |      Mean |     Error |    StdDev | Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 |   Allocated |
|-------------------------------------- |-------- |----------:|----------:|----------:|------:|--------:|-----------:|------:|------:|------------:|
|       thread_usnafe_shared_bool_array | 2000000 |  65.26 ms | 1.1189 ms | 0.9919 ms |  1.00 |    0.00 |          - |     - |     - |           - |
|            bool_create_each_timearray | 2000000 |  95.19 ms | 1.5403 ms | 1.4408 ms |  1.46 |    0.02 | 55000.0000 |     - |     - | 176000000 B |
| bool_create_one_local_for_N_timearray | 2000000 |  82.77 ms | 1.0555 ms | 0.9357 ms |  1.27 |    0.03 |          - |     - |     - |        88 B |
|      bool_array_per_thread_with_check | 2000000 |  65.27 ms | 0.7393 ms | 0.6554 ms |  1.00 |    0.02 |          - |     - |     - |           - |
|                      bool_stack_array | 2000000 |  66.74 ms | 0.9414 ms | 0.8345 ms |  1.02 |    0.02 |          - |     - |     - |           - |
|                write_from_bits_number | 2000000 |  33.44 ms | 0.6593 ms | 0.7054 ms |  0.51 |    0.02 |          - |     - |     - |           - |
|                 write_from_bits_array | 2000000 | 161.31 ms | 1.7131 ms | 1.6024 ms |  2.47 |    0.04 |          - |     - |     - |           - |
|                    u64BitsWriteBitRef | 2000000 | 229.96 ms | 3.3062 ms | 2.9309 ms |  3.52 |    0.07 |          - |     - |     - |           - |
|                           u64OneByOne | 2000000 |  79.97 ms | 1.5463 ms | 1.5880 ms |  1.22 |    0.04 |          - |     - |     - |           - |
|                    u64BitsWriteBitSet | 2000000 | 237.76 ms | 3.4114 ms | 3.1911 ms |  3.64 |    0.08 |          - |     - |     - |           - |

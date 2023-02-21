``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.437 (1809/October2018Update/Redstone5)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.203
  [Host] : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  Core   : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|             Method |      N |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |------- |----------:|----------:|----------:|------:|--------:|
|         bool_array | 100000 |  4.371 ms | 0.1171 ms | 0.3321 ms |  1.00 |    0.00 |
| u64BitsWriteBitRef | 100000 | 11.844 ms | 0.2352 ms | 0.5162 ms |  2.70 |    0.26 |

``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17134.523 (1803/April2018Update/Redstone4)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host]   : .NET Core 3.0.0-preview-27122-01 (CoreCLR 4.6.27121.03, CoreFX 4.7.18.57103), 64bit RyuJIT DEBUG
  ShortRun : .NET Core 3.0.0-preview-27122-01 (CoreCLR 4.6.27121.03, CoreFX 4.7.18.57103), 64bit RyuJIT

Job=ShortRun  Runtime=Core  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
|   Method |   loop |         Mean |         Error |        StdDev | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------- |------- |-------------:|--------------:|--------------:|------------:|------------:|------------:|--------------------:|
| **ForToInt** |    **100** |     **97.45 ns** |      **7.730 ns** |     **0.4237 ns** |           **-** |           **-** |           **-** |                   **-** |
| ForInInt |    100 |     93.87 ns |      9.948 ns |     0.5453 ns |           - |           - |           - |                   - |
| **ForToInt** | **100000** | **84,826.74 ns** | **15,020.401 ns** |   **823.3189 ns** |           **-** |           **-** |           **-** |                   **-** |
| ForInInt | 100000 | 85,488.12 ns | 20,994.615 ns | 1,150.7857 ns |           - |           - |           - |                   - |

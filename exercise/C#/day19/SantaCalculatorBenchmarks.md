
BenchmarkDotNet v0.14.0, macOS Sequoia 15.1.1 (24B91) [Darwin 24.1.0]
Apple M2 Max, 1 CPU, 12 logical and 12 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 8.0.11 (8.0.1124.51707), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.11 (8.0.1124.51707), Arm64 RyuJIT AdvSIMD


 Method                            | NumberOfReindeers | Mean       | Error     | StdDev    |
---------------------------------- |------------------ |-----------:|----------:|----------:|
 **CalculateTotalDistanceRecursively** | **15**                | **14.4293 ns** | **0.0453 ns** | **0.0401 ns** |
 CalculateTotalDistanceInALoop     | 15                |  4.9196 ns | 0.0268 ns | 0.0251 ns |
 CalculateTotalDistanceUsingLinq   | 15                | 22.3654 ns | 0.0481 ns | 0.0402 ns |
 CalculateTotalDistanceDirectly    | 15                |  0.4038 ns | 0.0060 ns | 0.0053 ns |
 **CalculateTotalDistanceRecursively** | **20**                | **21.9543 ns** | **0.0950 ns** | **0.0889 ns** |
 CalculateTotalDistanceInALoop     | 20                |  6.2849 ns | 0.0321 ns | 0.0301 ns |
 CalculateTotalDistanceUsingLinq   | 20                | 29.9476 ns | 0.1238 ns | 0.1158 ns |
 CalculateTotalDistanceDirectly    | 20                |  0.4023 ns | 0.0038 ns | 0.0036 ns |
 **CalculateTotalDistanceRecursively** | **25**                | **29.8053 ns** | **0.1616 ns** | **0.1512 ns** |
 CalculateTotalDistanceInALoop     | 25                |  7.6619 ns | 0.0546 ns | 0.0511 ns |
 CalculateTotalDistanceUsingLinq   | 25                | 37.5325 ns | 0.1077 ns | 0.1007 ns |
 CalculateTotalDistanceDirectly    | 25                |  0.4040 ns | 0.0057 ns | 0.0048 ns |
 **CalculateTotalDistanceRecursively** | **30**                | **37.5308 ns** | **0.2518 ns** | **0.2232 ns** |
 CalculateTotalDistanceInALoop     | 30                |  9.0281 ns | 0.0288 ns | 0.0269 ns |
 CalculateTotalDistanceUsingLinq   | 30                | 44.0825 ns | 0.1579 ns | 0.1477 ns |
 CalculateTotalDistanceDirectly    | 30                |  0.4042 ns | 0.0051 ns | 0.0048 ns |

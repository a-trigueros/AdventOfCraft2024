// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Travel;

var summary = BenchmarkRunner.Run<SantaCalculatorBenchmarks>();

[HtmlExporter]
[MarkdownExporter]
public class SantaCalculatorBenchmarks
{
    [Params(15, 20, 25, 30)]
    public int NumberOfReindeers {get;set;}
    
    [Benchmark]
    public int CalculateTotalDistanceRecursively() => 
        SantaTravelCalculator.CalculateTotalDistanceRecursively(NumberOfReindeers);
    
    [Benchmark]
    public int CalculateTotalDistanceInALoop() => 
        SantaTravelCalculator.CalculateTotalDistanceInALoop(NumberOfReindeers);
    
    [Benchmark]
    public int CalculateTotalDistanceUsingLinq() => 
        SantaTravelCalculator.CalculateTotalDistanceUsingLinq(NumberOfReindeers);

    [Benchmark]
    public int CalculateTotalDistanceDirectly() => 
        SantaTravelCalculator.CalculateTotalDistanceDirectly(NumberOfReindeers);
}
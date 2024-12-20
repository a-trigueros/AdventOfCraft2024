using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace Reindeer.Web.Tests;

public class XunitOutput(ITestOutputHelper output) : IOutput
{
    public void WriteLine(string line) => output.WriteLine(line);
}
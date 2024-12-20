using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using Reindeer.Web.Tests.Fixtures;
using Xunit.Abstractions;

namespace Reindeer.Web.Tests;

[Collection(ReindeerApiCollection.Name)]
public class ReindeerProducerShould(ReindeerApiFixture fixture, ITestOutputHelper output)
{
    [Fact]
    public void HaveAValidContract()
    {
        var config = new PactVerifierConfig
        {
            Outputters = new List<IOutput>
            {
                new XunitOutput(output),
            },
        };
        string pactPath = Path.Combine("..", "..", "..", "pacts", "Reindeer API Consumer-Reindeer API.json");
        using var pactVerifier = new PactVerifier("Reindeer API", config);
        pactVerifier
            .WithHttpEndpoint(fixture.ServerUri)
            .WithFileSource(new FileInfo(pactPath))
            .Verify();
    }
}
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using PactNet;
using Reindeer.Web.Service;

namespace Reindeer.Web.Tests;

public class ReindeerConsumerShould
{
    static readonly Guid PetarId = Guid.Parse("40F9D24D-D3E0-4596-ADC5-B4936FF84B19");
    private readonly IPactBuilderV4 _pactBuilder = Pact.V4("Reindeer API Consumer", "Reindeer API").WithHttpInteractions();
    
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    
    [Fact]
    public async Task LoadReinderThatExists()
    {
        _pactBuilder.UponReceiving("Get reinder")
            .Given($"Reinder with id {PetarId}")
            .WithRequest(HttpMethod.Get, $"/reindeer/{PetarId}")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new
            {
                Id = PetarId,
                Name = "Petar",
                Color = 1
            }, JsonSerializerOptions);

        await _pactBuilder.VerifyAsync(async context =>
        {
            using var client = new HttpClient();
            client.BaseAddress = context.MockServerUri;
            var response = await client.GetAsync($"/reindeer/{PetarId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ReindeerResult>();
            result.Should().NotBeNull();
            result!.Id.Should().Be(PetarId);
            result.Name.Should().Be("Petar");
            result.Color.Should().Be(ReindeerColor.Black);
        });
    }
    
    public class Fail
    {
        private readonly IPactBuilderV4 _pactBuilder = Pact.V4("Reindeer API Consumer", "Reindeer API").WithHttpInteractions();

        [Fact]
        public async Task ToLoadNonExistingReindeer()
        {
            var randomId = Guid.NewGuid();
            _pactBuilder.UponReceiving("Get reinder")
                .Given($"A random reinder")
                .WithRequest(HttpMethod.Get, $"/reindeer/{randomId}")
                .WillRespond()
                .WithStatus(HttpStatusCode.NotFound);
            
            await _pactBuilder.VerifyAsync(async context =>
            {
                using var client = new HttpClient();
                client.BaseAddress = context.MockServerUri;
                var response = await client.GetAsync($"/reindeer/{randomId}");
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            });

        }

        [Fact]
        public async Task ToCreateAnAlreadyExistingReindeer()
        {
            var request = new ReindeerToCreateRequest("Petar", ReindeerColor.Purple);
            
            _pactBuilder.UponReceiving("Fail to create an existing reindeer")
                .Given($"Reindeer with name {request.Name} and color {request.Color}")
                .WithRequest(HttpMethod.Post, "/reindeer")
                .WithJsonBody(request, JsonSerializerOptions)
                .WillRespond()
                .WithStatus(HttpStatusCode.Conflict);
            
            await _pactBuilder.VerifyAsync(async context =>
            {
                using var client = new HttpClient();
                client.BaseAddress = context.MockServerUri;
                
                var response = await client.PostAsync("/reindeer", JsonContent.Create(request,options: JsonSerializerOptions));
                response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            });
        }
    }
}
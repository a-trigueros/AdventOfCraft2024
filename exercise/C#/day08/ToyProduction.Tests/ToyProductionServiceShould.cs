using ToyProduction.Domain;
using ToyProduction.Services;
using ToyProduction.Tests.Doubles;
using Xunit;

namespace ToyProduction.Tests;

public class ToyProductionServiceShould
{
    private const string ToyName = "Train";
    
    [Fact]
    public void StartProductionWhenToyUnassigned()
    {
        var toy = new Toy(ToyName, State.Unassigned);
        var repository = InMemoryToyRepository.Initialize(r => r.Save(toy));
        var service = new ToyProductionService(repository);

        service.StartProduction(ToyName);

        repository.ShoulHaveSearched(ToyName)
            .ShouldHaveSaved(toy);
    }

    public class Fail
    {
        [Fact]
        public void ToStartProductionWhenItDoesNotExists()
        {
            var repository = new InMemoryToyRepository();
            var service = new ToyProductionService(repository);
            service.StartProduction(ToyName);
            
            repository.ShoulHaveSearched(ToyName)
                .ShouldNotHaveSaved();
        }
        
        [Theory]
        [InlineData(State.InProduction)]
        [InlineData(State.Completed)]
        public void ToStartProduction(State state)
        {
            var toy = new Toy(ToyName, state);
            var repository = InMemoryToyRepository.Initialize(r => r.Save(toy));
            var service = new ToyProductionService(repository);
            
            service.StartProduction(ToyName);
            
            repository.ShoulHaveSearched(ToyName)
                .ShouldNotHaveSaved();
        }
    }
}
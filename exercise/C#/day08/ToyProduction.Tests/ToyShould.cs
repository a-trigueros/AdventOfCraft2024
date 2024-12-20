using ToyProduction.Domain;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using Xunit;

namespace ToyProduction.Tests;

public class ToyShould
{
    [Fact]
    public void BeAbleToStartBeingProducedWhenUnassigned()
    {
        var toy = new Toy("Train", State.Unassigned);
        toy.CanStartProduction().Should().BeTrue();
    }
    
    [Theory]    
    [InlineData(State.InProduction)]
    [InlineData(State.Completed)]
    public void NotBeAbleToStartBeingProduced(State status)
    {
        var toy = new Toy("Train", status);
        toy.CanStartProduction().Should().BeFalse();
    }

    [Fact]
    public void StartProductionWhenUnassigned()
    {
        var toy = new Toy("Train", State.Unassigned);
        toy.StartProduction().Should().BeSome();
    }

    public class Fail
    {
        [Theory]
        [InlineData(State.InProduction)]
        [InlineData(State.Completed)]
        public void ToStartProduction(State state)
        {
            var toy = new Toy("Train", state);
            toy.StartProduction().Should().BeNone();
        }
    }
}
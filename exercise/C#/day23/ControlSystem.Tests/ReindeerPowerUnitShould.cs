using ControlSystem.Core;
using ControlSystem.External;
using FluentAssertions;

namespace ControlSystem.Tests;

public class ReindeerPowerUnitShould
{
    
    Reindeer LikeDancer = new("Dancer", 2, 8);
    
    [Fact]
    public void BeCreatedWithBasicAmplifierIfNotSpecified()
    {
        var reindeerPowerUnit = new ReindeerPowerUnit(LikeDancer);
        var reference = LikeDancer.GetMagicPower();

        reindeerPowerUnit.HarnessMagicPower().Should().Be(reference);
    }
    
    
    [Theory]
    [InlineData(AmplifierType.Basic)]
    [InlineData(AmplifierType.Blessed)]
    [InlineData(AmplifierType.Divine)]
    public void BeCreatedWithTheSpecifiedAmplifier(AmplifierType amplifierType)
    {
        var reindeerPowerUnit = new ReindeerPowerUnit(LikeDancer, amplifierType);
        var reference = LikeDancer.GetMagicPower() * amplifierType.GetMultiplier();

        reindeerPowerUnit.HarnessMagicPower().Should().Be(reference);
    }
}
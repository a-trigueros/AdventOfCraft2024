using ControlSystem.Core;
using ControlSystem.External;
using FluentAssertions;

namespace ControlSystem.Tests;

public class ReindeerPowerUnitShould
{
    private readonly Reindeer _likeDancer = new("Dancer", 2, 8);
    
    [Fact]
    public void BeCreatedWithBasicAmplifierIfNotSpecified()
    {
        var reindeerPowerUnit = new ReindeerPowerUnit(_likeDancer);
        var reference = _likeDancer.GetMagicPower();

        reindeerPowerUnit.HarnessableMagicPower().Should().Be(reference);
    }
    
    
    [Theory]
    [InlineData(AmplifierType.Basic)]
    [InlineData(AmplifierType.Blessed)]
    [InlineData(AmplifierType.Divine)]
    public void BeCreatedWithTheSpecifiedAmplifier(AmplifierType amplifierType)
    {
        var reindeerPowerUnit = new ReindeerPowerUnit(_likeDancer, amplifierType);
        var reference = _likeDancer.GetMagicPower() * amplifierType.GetMultiplier();

        reindeerPowerUnit.HarnessableMagicPower().Should().Be(reference);
    }
}
using ControlSystem.Core;
using FsCheck;
using FsCheck.Xunit;

namespace ControlSystem.Tests;

public class MagicPowerAmplifierShould
{
    private static Arbitrary<float> PositiveValue() =>
        Arb.Default.Float().Generator.Where(x => x is > 0 and < 10_000_000).Select(x => (float)x)
        .ToArbitrary();

    [Property]
    public Property KeepOutputAsIsIfIsBasic() => 
        Prop.ForAll(PositiveValue(), ShouldKeepItsValue);

    private static bool ShouldKeepItsValue(float input)
    {
        var amplifier = new MagicPowerAmplifier(AmplifierType.Basic);
        var output = amplifier.Amplify(input);
        return Math.Abs(output - input) < float.Epsilon;
    }
    
    [Property]
    public Property MultiplyOutputByTwoIfIsBlessed() => 
        Prop.ForAll(PositiveValue(), ShouldMultiplyByTwo);

    private bool ShouldMultiplyByTwo(float input)
    {
        var amplifier = new MagicPowerAmplifier(AmplifierType.Blessed);
        var output = amplifier.Amplify(input);
        return Math.Abs(output - input * 2) < float.Epsilon;
    }

    [Property]
    public Property MultiplyOutputByThreeIfIsDivine() => 
        Prop.ForAll(PositiveValue(), ShouldMultiplyByThree);

    private bool ShouldMultiplyByThree(float input)
    {
        var amplifier = new MagicPowerAmplifier(AmplifierType.Divine);
        var output = amplifier.Amplify(input);
        return Math.Abs(output - input * 3) < float.Epsilon;
    }
}
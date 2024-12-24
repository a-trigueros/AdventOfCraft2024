using ControlSystem.External;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace ControlSystem.Core;

public class ReindeerTeam
{
    private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;

    private int _blessedAmplifiersAvailable = 2;
    private int _divineAmplifiersAvailable = 1;

    public ReindeerTeam()
    {
        _reindeerPowerUnits = new MagicStable().GetAllReindeers()
            .OrderByDescending(x => x.GetMagicPower())
            .Select(AttachPowerUnitToHealthyReindeer).ToList();
    }

    private ReindeerPowerUnit AttachPowerUnitToHealthyReindeer(Reindeer reindeer) =>
        new(reindeer, TakeAmplifierType(reindeer));


    private AmplifierType TakeAmplifierType(Reindeer reindeer)
    {
        if (reindeer.Sick)
        {
            return AmplifierType.Basic;
        }

        if (_divineAmplifiersAvailable > 0)
        {
            _divineAmplifiersAvailable--;
            return AmplifierType.Divine;
        }

        if (_blessedAmplifiersAvailable > 0)
        {
            _blessedAmplifiersAvailable--;
            return AmplifierType.Blessed;
        }

        return AmplifierType.Basic;
    }

    public Either<Error, Unit> HarnessMagicPower(int requiredMagicPower)
    {
        var harnessableMagicPower = _reindeerPowerUnits.Sum(x => x.HarnessableMagicPower());

        if (harnessableMagicPower < requiredMagicPower)
            return Error.New(new ReindeersNeedRestException());
        
        _reindeerPowerUnits.ForEach(x => x.HarnessMagicPower());
        return unit;
    }

    public void Rest()
    {
        foreach (var reindeerPowerUnit in _reindeerPowerUnits)
        {
            reindeerPowerUnit.Reindeer.TimesHarnessing = 0;
        }
    }
}
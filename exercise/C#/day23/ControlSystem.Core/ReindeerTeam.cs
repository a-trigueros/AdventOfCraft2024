using ControlSystem.External;

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

    private ReindeerPowerUnit AttachPowerUnitToHealthyReindeer(Reindeer reindeer) => new(reindeer, TakeAmplifierType(reindeer));
        
        
    private AmplifierType TakeAmplifierType(Reindeer reindeer)
    {
        if (reindeer.Sick)
        {
            return AmplifierType.Basic;
        }
            
        if(_divineAmplifiersAvailable > 0)
        {
            _divineAmplifiersAvailable--;
            return AmplifierType.Divine;
        }

        if(_blessedAmplifiersAvailable > 0)
        {
            _blessedAmplifiersAvailable--;
            return AmplifierType.Blessed;
        }

        return AmplifierType.Basic;
    }
        
    public void HarnessMagicPower(int requiredMagicPower)
    {
        var controlMagicPower = _reindeerPowerUnits.Sum(x => x.HarnessableMagicPower());

        if (controlMagicPower < requiredMagicPower)
            throw new ReindeersNeedRestException();
        _reindeerPowerUnits.ForEach(x => x.HarnessMagicPower());
    }

    public void Rest()
    {
        foreach (var reindeerPowerUnit in _reindeerPowerUnits)
        {
            reindeerPowerUnit.Reindeer.TimesHarnessing = 0;
        }
    }
}
using ControlSystem.External;

namespace ControlSystem.Core
{
    public class System
    {
        private const int XmasSpirit = 40;
        private readonly Dashboard _dashboard;
        private readonly MagicStable _magicStable = new();
        private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;
        public SleighEngineStatus Status { get; private set; }
        public SleighAction Action { get; private set; }

        
        private int _blessedAmplifiersAvailable = 2;
        private int _divineAmplifiersAvailable = 1;

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

        
        public System()
        {
            _dashboard = new Dashboard();
            _reindeerPowerUnits = BringAllReindeers();
        }

        private List<ReindeerPowerUnit> BringAllReindeers() =>
            _magicStable.GetAllReindeers().OrderByDescending(x => x.GetMagicPower()).Select(AttachPowerUnit).ToList();

        private ReindeerPowerUnit AttachPowerUnit(Reindeer reindeer) => new(reindeer, TakeAmplifierType(reindeer));

        public void StartSystem()
        {
            _dashboard.DisplayStatus("Starting the sleigh...");
            Status = SleighEngineStatus.On;
            _dashboard.DisplayStatus("System ready.");
        }

        public void Ascend()
        {
            if (Status != SleighEngineStatus.On)
            {
                throw new SleighNotStartedException();
            }

            var controlMagicPower = _reindeerPowerUnits.Sum(x => x.HarnessMagicPower());

            if (!(controlMagicPower >= XmasSpirit))
                throw new ReindeersNeedRestException();
            
            _dashboard.DisplayStatus("Ascending...");
            Action = SleighAction.Flying;
        }

        public void Descend()
        {
            if (Status == SleighEngineStatus.On)
            {
                _dashboard.DisplayStatus("Descending...");
                Action = SleighAction.Hovering;
            }
            else throw new SleighNotStartedException();
        }

        public void Park()
        {
            if (Status == SleighEngineStatus.On)
            {
                _dashboard.DisplayStatus("Parking...");

                foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                {
                    reindeerPowerUnit.Reindeer.TimesHarnessing = 0;
                }

                Action = SleighAction.Parked;
            }
            else throw new SleighNotStartedException();
        }

        public void StopSystem()
        {
            _dashboard.DisplayStatus("Stopping the sleigh...");
            Status = SleighEngineStatus.Off;
            _dashboard.DisplayStatus("System shutdown.");
        }
    }
}
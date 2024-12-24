namespace ControlSystem.Core
{
    public class System
    {
        private const int XmasSpirit = 40;
        private readonly Dashboard _dashboard = new();
        private readonly ReindeerTeam _reindeerTeam = new();
        public SleighEngineStatus Status { get; private set; }
        public SleighAction Action { get; private set; }
        

        public void StartSystem()
        {
            _dashboard.DisplayStatus("Starting the sleigh...");
            Status = SleighEngineStatus.On;
            _dashboard.DisplayStatus("System ready.");
        }

        public void Ascend()
        {
            EnsureIsStarted();

            _reindeerTeam.HarnessMagicPower(XmasSpirit);
            _dashboard.DisplayStatus("Ascending...");
            Action = SleighAction.Flying;
        }

        public void Descend()
        {
            EnsureIsStarted();
            _dashboard.DisplayStatus("Descending...");
            Action = SleighAction.Hovering;
        }

        public void Park()
        {
            EnsureIsStarted();
            _dashboard.DisplayStatus("Parking...");

            _reindeerTeam.Rest();

            Action = SleighAction.Parked;
        }

        private void EnsureIsStarted()
        {
            if (Status != SleighEngineStatus.On)
                throw new SleighNotStartedException();
        }

        public void StopSystem()
        {
            _dashboard.DisplayStatus("Stopping the sleigh...");
            Status = SleighEngineStatus.Off;
            _dashboard.DisplayStatus("System shutdown.");
        }
    }
}
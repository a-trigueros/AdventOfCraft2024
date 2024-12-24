using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

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

        public Either<Error, Unit> Ascend() =>
            EnsureIsStarted()
                .Bind(_ => _reindeerTeam.HarnessMagicPower(XmasSpirit))
                .Do(_ => _dashboard.DisplayStatus("Ascending..."))
                .Do(_ => Action = SleighAction.Flying);

        public Either<Error, Unit> Descend() =>
            EnsureIsStarted()
                .Do(_ => _dashboard.DisplayStatus("Descending..."))
                .Do(_ => Action = SleighAction.Hovering);

        public void Park() =>
            EnsureIsStarted()
                .Do(_ => _dashboard.DisplayStatus("Parking..."))
                .Do(_ => _reindeerTeam.Rest())
                .Do(_ => Action = SleighAction.Parked);

        private Either<Error, Unit> EnsureIsStarted() =>
            !Status.IsStarted() 
                ? Error.New(new SleighNotStartedException()) 
                : unit;

        public void StopSystem()
        {
            _dashboard.DisplayStatus("Stopping the sleigh...");
            Status = SleighEngineStatus.Off;
            _dashboard.DisplayStatus("System shutdown.");
        }
    }
}
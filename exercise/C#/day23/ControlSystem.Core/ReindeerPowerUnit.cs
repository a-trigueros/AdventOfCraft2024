using ControlSystem.External;

namespace ControlSystem.Core
{
    public class ReindeerPowerUnit(Reindeer reindeer, AmplifierType amplifierType = AmplifierType.Basic)
    {
        public Reindeer Reindeer => reindeer;
        private readonly MagicPowerAmplifier _amplifier = new(amplifierType);


        public float HarnessMagicPower()
        {
            if (!Reindeer.NeedsRest())
            {
                Reindeer.TimesHarnessing++;
                return _amplifier.Amplify(Reindeer.GetMagicPower());
            }

            return 0;
        }

        public float CheckMagicPower()
        {
            return Reindeer.GetMagicPower();
        }
    }
}
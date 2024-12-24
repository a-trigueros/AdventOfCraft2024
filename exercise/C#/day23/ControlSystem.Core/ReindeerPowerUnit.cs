using ControlSystem.External;

namespace ControlSystem.Core
{
    public class ReindeerPowerUnit(Reindeer reindeer, AmplifierType amplifierType = AmplifierType.Basic)
    {
        public Reindeer Reindeer => reindeer;
        private readonly MagicPowerAmplifier _amplifier = new(amplifierType);


        public void HarnessMagicPower()
        {
            if (!Reindeer.NeedsRest())
            {
                Reindeer.TimesHarnessing++;
            }
        }

        public float HarnessableMagicPower()
        {
            Reindeer.TimesHarnessing++;
            var retVal =  _amplifier.Amplify(Reindeer.GetMagicPower());
            Reindeer.TimesHarnessing--;
            return retVal;
        }
    }
}
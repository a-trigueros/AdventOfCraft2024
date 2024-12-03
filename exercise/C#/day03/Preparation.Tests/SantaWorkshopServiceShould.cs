using FsCheck;
using FsCheck.Xunit;

namespace Preparation.Tests
{
    record GiftProperties(string name, double weight, string color, string material);

    public class SantaWorkshopServiceShould
    {
        private const string RecommendedAge = "recommendedAge";
        private readonly SantaWorkshopService _service = new();

        public static Gen<int> RecommandedAgeGen= Gen.Choose(1, 150);
        
        private static Gen<string> NameGen = Arb.Generate<string>();
        private static Gen<double> UnderWeightGen = Arb.Generate<double>().Where(x => x <= 5);
        private static Gen<double> OverWeightGen = Arb.Generate<double>().Where(x => x > 5);
        private static Gen<string> ColorGen = Gen.Elements("Red", "Blue", "Green", "Golden");
        private static Gen<string> MaterialGen = Gen.Elements("Plastic", "Metal", "Cotton", "Wood");

        private static Gen<GiftProperties> ValidGiftGen =
            from name in NameGen
            from weight in UnderWeightGen
            from color in ColorGen
            from material in MaterialGen
            select new GiftProperties(name, weight, color, material);

        private static Gen<GiftProperties> OverwheightGiftGen =
            from name in NameGen
            from weight in OverWeightGen
            from color in ColorGen
            from material in MaterialGen
            select new GiftProperties(name, weight, color, material);


        [Property]
        public Property PrepareGiftWithValidToy() =>
            Prop.ForAll(ValidGiftGen.ToArbitrary(), ShouldAddGift);

        private bool ShouldAddGift(GiftProperties props)
        {
            try
            {
                var (name, weight, color, material) = props;
                _service.PrepareGift(name, weight, color, material);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Property]
        public Property RetrieveAttributeOnGift() =>
            Prop.ForAll(ValidGiftGen.ToArbitrary(), 
                RecommandedAgeGen.ToArbitrary(), 
                ShouldSetAndRetrieveAttribute);

        private bool ShouldSetAndRetrieveAttribute(GiftProperties props, int recommandedAge)
        {
            var (name, weight, color, material) = props;
            var gift = _service.PrepareGift(name, weight, color, material);
            gift.AddAttribute(RecommendedAge, recommandedAge.ToString());
            return gift.RecommendedAge() == recommandedAge;
        }

        [Property]
        public Property FailsToPrepareForATooHeavyGift()
        => Prop.ForAll(OverwheightGiftGen.ToArbitrary(), ShouldThrowException);

        private bool ShouldThrowException(GiftProperties obj)
        {
            try
            {
                var(name, weight, color, material) = obj;
                _service.PrepareGift(name, weight, color, material);
                return false;
            }
            catch (ArgumentException e) when(e.Message == "Gift is too heavy for Santa's sleigh")
            {
                return true;
            }
        }
    }
}
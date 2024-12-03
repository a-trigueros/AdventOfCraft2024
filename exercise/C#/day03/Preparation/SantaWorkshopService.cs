namespace Preparation
{
    public class SantaWorkshopService
    {
        private readonly List<Gift> _preparedGifts = new();

        public Gift PrepareGift(string giftName, double weight, string color, string material)
        {
            AssertIsNotOverweight(weight);

            var gift = new Gift(giftName, weight, color, material);
            _preparedGifts.Add(gift);

            return gift;
        }

        private static void AssertIsNotOverweight(double weight)
        {
            if (weight > 5)
            {
                throw new ArgumentException("Gift is too heavy for Santa's sleigh");
            }
        }
    }
}
namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public SleighReport LoadGiftsInSleigh(params Child[] children)
    {
        var list = new SleighReport();
        foreach (var child in children)
        {
            var gift = wishList.IdentifyGift(child);
            if (gift.IsLeft)
            {
                list.Add(child, (string)gift.Case);
                continue;
            }
            var manufactured = factory.FindManufacturedGift((Gift)gift.Case);
            if (manufactured is null)
            {
                list.Add(child, "Missing gift: Gift wasn't manufactured!");
                continue;
            };
            var finalGift = inventory.PickUpGift(manufactured.BarCode);
            if (finalGift is null)
            {
                list.Add(child, "Missing gift: The gift has probably been misplaced by the elves!");
                continue;
            };
            list.Add(child, $"Gift: {finalGift.Name} has been loaded!");
        }
        return list;
    }
}
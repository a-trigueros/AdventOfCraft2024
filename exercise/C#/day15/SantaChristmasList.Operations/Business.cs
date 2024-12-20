namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public SleighReport LoadGiftsInSleigh(params Child[] children)
    {
        var list = new SleighReport();
        foreach (var child in children)
        {
            
            var tempResult = 
                from gift in wishList.IdentifyGift(child)
                from manufactured in factory.FindManufacturedGift(gift)
                select manufactured;
            if (tempResult.IsLeft)
            {
                list.Add(child, (string)tempResult.Case);
                continue;
            }
            var finalGift = inventory.PickUpGift(((ManufacturedGift)tempResult.Case).BarCode);
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
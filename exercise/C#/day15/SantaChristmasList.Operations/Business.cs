namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public SleighReport LoadGiftsInSleigh(params Child[] children)
    {
        var list = new SleighReport();
        foreach (var child in children)
        {
            ( 
                from gift in wishList.IdentifyGift(child)
                from manufactured in factory.FindManufacturedGift(gift)
                from finalGift in  inventory.PickUpGift(manufactured.BarCode)
                select finalGift
                ).Match(
                Left: error => list.Add(child, error),
                Right: finalGift => list.Add(child, $"Gift: {finalGift.Name} has been loaded!")
            );
        }
        return list;
    }
}
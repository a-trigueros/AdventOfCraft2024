using LanguageExt;

namespace SantaChristmasList.Operations;

public class Factory : Dictionary<Gift, ManufacturedGift>
{
    public Either<string, ManufacturedGift> FindManufacturedGift(Gift gift)
    {
        return ContainsKey(gift) 
            ? this[gift] 
            : "Missing gift: Gift wasn't manufactured!";
    }
}

public class Inventory : Dictionary<string, Gift>
{
    public Gift PickUpGift(string barCode)
    {
        return ContainsKey(barCode) ? this[barCode] : null;
    }
}

public class WishList : Dictionary<Child, Gift>
{
    public Either<string, Gift> IdentifyGift(Child child)
    {
        return ContainsKey(child) 
            ? this[child] 
            : "Missing gift: Child wasn't nice this year!";
    }
}
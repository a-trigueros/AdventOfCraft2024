using LanguageExt;

namespace SantaChristmasList.Operations;

public class Factory : Dictionary<Gift, ManufacturedGift>
{
    public const string MissingGiftMessage = "Missing gift: Gift wasn't manufactured!";
    public Either<string, ManufacturedGift> FindManufacturedGift(Gift gift)
    {
        return ContainsKey(gift)
            ? this[gift]
            : MissingGiftMessage;
    }
}

public class Inventory : Dictionary<string, Gift>
{
    public const string MissingGiftMessage = "Missing gift: The gift has probably been misplaced by the elves!";
    public Either<string, Gift> PickUpGift(string barCode)
    {
        return ContainsKey(barCode)
            ? this[barCode]
            : MissingGiftMessage;
    }
}

public class WishList : Dictionary<Child, Gift>
{
    public const string MissingGiftMessage = "Missing gift: Child wasn't nice this year!";
    public Either<string, Gift> IdentifyGift(Child child)
    {
        return ContainsKey(child)
            ? this[child]
            : MissingGiftMessage;
    }
}
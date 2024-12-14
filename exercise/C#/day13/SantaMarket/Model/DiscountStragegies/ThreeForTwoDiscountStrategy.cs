namespace SantaMarket.Model.DiscountStragegies;

public class ThreeForTwoDiscountStrategy(ISantamarketCatalog catalog) : IComputeDiscount
{
    public bool IsValid(Offer offer, int quantity) 
        => offer.OfferType == SpecialOfferType.ThreeForTwo && quantity > 2;

    public Discount Compute(Offer offer, Product product, double quantity)
    {
        int quantityAsInt = (int)quantity;
        var unitPrice = catalog.GetUnitPrice(product);
        var discountAmount = quantity * unitPrice -
                          ((quantityAsInt / 3 * 2 * unitPrice) + (quantityAsInt % 3) * unitPrice);
        return new Discount(product, "3 for 2", -discountAmount);
    }
}
namespace SantaMarket.Model.DiscountStragegies;

public class TenPercentDiscountStrategy(ISantamarketCatalog catalog) : IComputeDiscount
{
    public bool IsValid(Offer offer, int _) => offer.OfferType == SpecialOfferType.TenPercentDiscount;
    public Discount Compute(Offer offer, Product product, double quantity)
    {
        var unitPrice = catalog.GetUnitPrice(product);
        return new Discount(product, offer.Argument + "% off",
        -quantity * unitPrice * offer.Argument / 100.0);
    }
}
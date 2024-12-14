namespace SantaMarket.Model.DiscountStragegies;

public interface IComputeDiscount
{
    bool IsValid(Offer offer, int quantity);
    Discount Compute(Offer offer, Product product, double quantity);
}
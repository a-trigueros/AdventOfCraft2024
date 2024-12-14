using SantaMarket.Model.DiscountStragegies;

namespace SantaMarket.Model
{
    public class ShoppingSleigh
    {
        private readonly List<ProductQuantity> _items = [];
        private readonly Dictionary<Product, double> _productQuantities = new();

        public IReadOnlyList<ProductQuantity> Items() => _items.AsReadOnly();

        public void AddItem(Product product) => AddItemQuantity(product, 1.0);

        public IReadOnlyDictionary<Product, double> ProductQuantities() => _productQuantities.AsReadOnly();

        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                _productQuantities[product] += quantity;
            }
            else
            {
                _productQuantities[product] = quantity;
            }
        }

        
        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, ISantamarketCatalog catalog)
        {
            var threeForTwoDiscountStrategy = new ThreeForTwoDiscountStrategy(catalog);
            var tenPercentStrategy = new TenPercentDiscountStrategy(catalog);
            
            foreach (var product in ProductQuantities().Keys)
            {
                var quantity = _productQuantities[product];
                if (offers.ContainsKey(product))
                {
                    var offer = offers[product];
                    var unitPrice = catalog.GetUnitPrice(product);
                    var quantityAsInt = (int) quantity;
                    Discount? discount = null;
                    var x = offer.OfferType == SpecialOfferType.ThreeForTwo ? 3 : 1;

                    if (offer.OfferType == SpecialOfferType.TwoForAmount && quantityAsInt >= 2)
                    {
                        var total = offer.Argument * (quantityAsInt / 2) + (quantityAsInt % 2) * unitPrice;
                        discount = new Discount(product, "2 for " + offer.Argument, -(unitPrice * quantity - total));
                    }

                    if(threeForTwoDiscountStrategy.IsValid(offer, quantityAsInt))
                    {
                        discount = threeForTwoDiscountStrategy.Compute(offer, product, quantity);
                    }

                    if(tenPercentStrategy.IsValid(offer, quantityAsInt))
                    {
                        discount = tenPercentStrategy.Compute(offer, product, quantity);
                    }

                    if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
                    {
                        var discountTotal = unitPrice * quantity -
                                            (offer.Argument * (quantityAsInt / 5) + (quantityAsInt % 5) * unitPrice);
                        discount = new Discount(product, "5 for " + offer.Argument, -discountTotal);
                    }

                    if (discount != null)
                    {
                        receipt.AddDiscount(discount);
                    }
                }
            }
        }
    }
}
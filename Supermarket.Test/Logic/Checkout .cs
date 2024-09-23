namespace Supermarket.Test.Logic;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, (int UnitPrice, (int Quantity, int Price)[] SpecialPrices)> _pricingRules;
    private readonly Dictionary<string, int> _scannedItems;

    public Checkout(Dictionary<string, (int UnitPrice, (int Quantity, int Price)[] SpecialPrices)> pricingRules)
    {
        _pricingRules = pricingRules;
        _scannedItems = new Dictionary<string, int>();
    }

    public void Scan(string item)
    {
        if (_pricingRules.ContainsKey(item))
        {
            if (_scannedItems.ContainsKey(item))
            {
                _scannedItems[item]++;
            }
            else
            {
                _scannedItems[item] = 1;
            }
        }
    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;

        foreach (var item in _scannedItems)
        {
            var sku = item.Key;
            var quantity = item.Value;
            var (unitPrice, specialPrices) = _pricingRules[sku];

            // Apply special prices first
            int specialPriceTotal = 0;
            foreach (var (offerQuantity, offerPrice) in specialPrices.OrderByDescending(o => o.Quantity))
            {
                while (quantity >= offerQuantity)
                {
                    specialPriceTotal += offerPrice;
                    quantity -= offerQuantity;
                }
            }

            // Add remaining items at unit price
            totalPrice += specialPriceTotal + (quantity * unitPrice);
        }

        return totalPrice;
    }
}

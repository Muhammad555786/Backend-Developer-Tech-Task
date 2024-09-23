namespace Supermarket.Test.Logic;

public interface ICheckout
{
    int GetTotalPrice();
    void Scan(string item);
}
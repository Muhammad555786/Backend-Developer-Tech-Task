using Supermarket.Test.Logic;

namespace Supermarket.Test;

public class CheckoutTests
{
    private ICheckout _checkout;

    public CheckoutTests()
    {
        var pricingRules = new Dictionary<string, (int, (int, int)[])>
        {
            { "A", (50, new[] { (3, 130) }) },
            { "B", (30, new[] { (2, 45) }) },
            { "C", (20, new (int, int)[] { }) },
            { "D", (15, new (int, int)[] { }) }
        };

        _checkout = new Checkout(pricingRules);
    }

    [Fact]
    public void Test_A50Equal()
    {
        _checkout.Scan("A");
        Assert.Equal(50, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_AA100Equal()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        Assert.Equal(100, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_AAA130Equal()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        Assert.Equal(130, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_B30Equal()
    {
        _checkout.Scan("B");
        Assert.Equal(30, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_BB45Equal()
    {
        _checkout.Scan("B");
        _checkout.Scan("B");
        Assert.Equal(45, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_BBB75Equal()
    {
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("B");
        Assert.Equal(75, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_ABB95()
    {
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        Assert.Equal(95, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_CD35Equal()
    {
        _checkout.Scan("C");
        _checkout.Scan("D");
        Assert.Equal(35, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_AAABBCD155NotEqual()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("C");
        _checkout.Scan("D");
        Assert.NotEqual(155, _checkout.GetTotalPrice());
    }

    [Fact]
    public void Test_AAABBCD210Equal()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("C");
        _checkout.Scan("D");
        Assert.Equal(210, _checkout.GetTotalPrice());
    }
}


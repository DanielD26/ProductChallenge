namespace Tests;

using API;
using Xunit;

public class UnitTest1
{
    [Theory]
    [InlineData(5, 2.4)]
    [InlineData(10, 1.5)]
    [InlineData(2, 5.7)]
    // Test for calculate total
    public void TotalCostTest(int q, float price) {
        var order = new Order().calcTotal(q, price);
        var result = q * price;
        Assert.Equal(order, result);
    }
    // Test for GST
    [Theory]
    [InlineData(5, 2.4)]
    [InlineData(10, 1.5)]
    [InlineData(2, 5.7)]
    public void gstTest(int q, float price) {
        var order = new Order().gst(q, price);
        var result = (q * price)/10;
        Assert.Equal(order, result);
    }
}
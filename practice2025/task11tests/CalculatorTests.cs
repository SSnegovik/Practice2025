using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldReturnCorrectSum()
    {
        dynamic calc = Calculator.Create();
        int result = calc.Add(5, 3);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Minus_ShouldReturnCorrectDifference()
    {
        dynamic calc = Calculator.Create();
        int result = calc.Minus(10, 4);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Mul_ShouldReturnCorrectProduct()
    {
        dynamic calc = Calculator.Create();
        int result = calc.Mul(3, 7);
        Assert.Equal(21, result);
    }

    [Fact]
    public void Div_ShouldReturnCorrectQuotient()
    {
        dynamic calc = Calculator.Create();
        int result = calc.Div(10, 2);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Div_ByZero_ShouldThrowException()
    {
        dynamic calc = Calculator.Create();
        Assert.Throws<DivideByZeroException>(() => calc.Div(10, 0));
    }
}
using System.ComponentModel;
using System.Reflection;
using Xunit;

#nullable disable

public class AttributeReflectionTests
{
    [Fact]
    public void Class_HasDisplayNameAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Пример класса", attribute.DisplayName);
    }

    [Fact]
    public void Method_HasDisplayNameAttribute()
    {
        var method = typeof(SampleClass).GetMethod("TestMethod");
        var attribute = method.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Тестовый метод", attribute.DisplayName);
    }

    [Fact]
    public void Property_HasDisplayNameAttribute()
    {
        var prop = typeof(SampleClass).GetProperty("Number");
        var attribute = prop.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Числовое свойство", attribute.DisplayName);
    }

    [Fact]
    public void Class_HasVersionAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<VersionAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal(1, attribute.Major);
        Assert.Equal(0, attribute.Minor);
    }

    [Fact]
    public void PrintTypeInfo_Test()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        var expectedOutput = "Имя класса: Пример класса\r\n" +
                            "Версия класса: 1.0\r\n" +
                            "get_Number - Имя метода: нет\r\n" +
                            "TestMethod - Имя метода: Тестовый метод\r\n" +
                            "Number - Имя свойства: Числовое свойство\r\n";

        ReflectionHelper.PrintTypeInfo(typeof(SampleClass));

        Assert.Equal(expectedOutput, output.ToString());
    }
}

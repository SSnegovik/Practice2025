using System.Reflection;

using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; }

    public DisplayNameAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    public int Major { get; }
    public int Minor { get; }

    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}

[DisplayNameAttribute("Пример класса")]
[VersionAttribute(1, 0)]
public class SampleClass
{
    [DisplayNameAttribute("Числовое свойство")]
    public int Number { get; }

    [DisplayNameAttribute("Тестовый метод")]
    public void TestMethod() { }
}


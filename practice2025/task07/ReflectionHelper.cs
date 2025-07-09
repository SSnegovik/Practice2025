using System.Reflection;

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayNameAttr = type.GetCustomAttribute<DisplayNameAttribute>();
        if (displayNameAttr != null)
        {
            Console.WriteLine($"Имя класса: {displayNameAttr.DisplayName}");
        }

        var versionAttribute = type.GetCustomAttribute<VersionAttribute>();
        if (versionAttribute != null)
        {
            Console.WriteLine($"Версия класса: {versionAttribute.Major}.{versionAttribute.Minor}");
        }

        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            var methodName = method.GetCustomAttribute<DisplayNameAttribute>();
            Console.WriteLine($"{method.Name} - Имя метода: {(methodName?.DisplayName ?? "нет")}");
        }

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            var propertyName = property.GetCustomAttribute<DisplayNameAttribute>();
            Console.WriteLine($"{property.Name} - Имя свойства: {(propertyName?.DisplayName ?? "нет")}");
        }
    }
}
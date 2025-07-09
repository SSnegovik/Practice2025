using System;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Укажите путь к сборке!");
            return;
        }

        string assemblyPath = args[0];
        Assembly assembly = Assembly.LoadFrom(assemblyPath);

        foreach (Type type in assembly.GetTypes())
        {
            Console.WriteLine($"=== Класс: {type.Name} ===");
            Console.WriteLine("\nАтрибуты:");
            foreach (var attribute in type.GetCustomAttributes())
            {
                if (attribute is DisplayNameAttribute dna)
                    Console.WriteLine($"  [DisplayName(\"{dna.DisplayName}\")]");
                else if (attribute is VersionAttribute va)
                    Console.WriteLine($"  [Version({va.Major}, {va.Minor})]");
                else
                    Console.WriteLine("  Нет");
            }

            Console.WriteLine("\nМетоды:");
            foreach (var method in type.GetMethods())
            {
                if (method.IsSpecialName) continue;
                Console.Write($"  {method.ReturnType.Name} {method.Name}(");
                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                }
                Console.Write(")");
                foreach (var attribute in method.GetCustomAttributes())
                {
                    if (attribute is DisplayNameAttribute dna)
                        Console.Write($" [DisplayName(\"{dna.DisplayName}\")]");
                }

                Console.WriteLine();
                
            }

            Console.WriteLine("\nКонструкторы:");
            foreach (var constructor in type.GetConstructors())
            {
                Console.Write($" {type.Name} (");
                ParameterInfo[] parameters = constructor.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                }
                Console.WriteLine(")");
            }
            Console.WriteLine();
        }
    }
}
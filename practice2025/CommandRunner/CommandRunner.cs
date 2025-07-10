using System;
using System.Linq;
using System.Reflection;

#nullable disable

namespace CommandRunner
{
    class CommandRunner
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Введите путь до dll");
                return;
            }

            try
            {
                var assembly = Assembly.LoadFrom(args[0]);
                var commandTypes = assembly.GetTypes()
                    .Where(t => typeof(ICommand).IsAssignableFrom(t));

                foreach (var type in commandTypes)
                {
                    ExecuteCommand(type);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ExecuteCommand(Type commandType)
        {
            try
            {
                Console.WriteLine($"Выполнени:{commandType.Name}");

                var constructor = commandType.GetConstructors().First();
                var parameters = constructor.GetParameters()
                    .Select(p => GetDefaultParameterValue(p.ParameterType, p.Position))
                    .ToArray();

                var command = (ICommand)Activator.CreateInstance(commandType, parameters);
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка выполнения: {commandType.Name}: {ex.Message}");
            }
        }

        static object GetDefaultParameterValue(Type type, int position)
        {
            return type == typeof(string)
                ? (position == 0 ? "." : "*.txt")
                : Activator.CreateInstance(type);
        }
    }
}
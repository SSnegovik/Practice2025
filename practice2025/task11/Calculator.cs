using System;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

#nullable disable

public class Calculator
{
    public static dynamic Create()
    {
        string code = @"
            public class Calculator
            {
                public int Add(int a, int b) => a + b;
                public int Minus(int a, int b) => a - b;
                public int Mul(int a, int b) => a * b;
                public int Div(int a, int b) => a / b;
            }";

        var compilation = CSharpCompilation.Create("CalculatorAssembly")
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
            )
            .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code));

        using var ms = new System.IO.MemoryStream();
        var emitResult = compilation.Emit(ms);

        ms.Seek(0, System.IO.SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());
        var type = assembly.GetType("Calculator");
        return Activator.CreateInstance(type);
    }
}

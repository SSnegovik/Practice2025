using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ScottPlot;

class Program
{
    static void Main()
    {
        double a = -100;
        double b = 100;
        Func<double, double> f = Math.Sin;

        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        double requiredAccuracy = 1e-4;

        Console.WriteLine("Значение с очень мелким шагом 1e-7...");
        double refValue = DefiniteIntegral.CalculateDefiniteIntegral(a, b, f, 1e-7);
        Console.WriteLine($"Значение интеграла: {refValue:F8}");

        double chosenStep = 0;
        double chosenStepError = double.MaxValue;

        foreach (var step in steps)
        {
            double val = DefiniteIntegral.CalculateDefiniteIntegral(a, b, f, step);
            double abs = Math.Abs(val - refValue);

            Console.WriteLine($"Шаг: {step:E}, Интеграл: {val:F8}");

            if (abs <= requiredAccuracy && (step < chosenStep || chosenStep == 0))
            {
                chosenStep = step;
                chosenStepError = abs;
            }
        }

        Console.WriteLine($"\nМинимальный шаг: {chosenStep:E}, с {chosenStepError:E}\n");

        int maxThreads = Environment.ProcessorCount * 2;
        int repeats = 3;

        var threadCounts = new List<int>();
        var times = new List<double>();

        for (int threads = 1; threads <= maxThreads; threads++)
        {
            double totalTime = 0;
            for (int i = 0; i < repeats; i++)
            {
                var sw = Stopwatch.StartNew();
                double res = DefiniteIntegral.Solve(a, b, f, chosenStep, threads);
                sw.Stop();
                totalTime += sw.Elapsed.TotalMilliseconds;
            }
            double avgTime = totalTime / repeats;
            threadCounts.Add(threads);
            times.Add(avgTime);
            Console.WriteLine($"Потоки: {threads}, Среднее время: {avgTime:F2} ms");
        }

        int bestThreads = threadCounts[times.IndexOf(times.Min())];
        double bestTime = times.Min();

        Console.WriteLine($"\nОптимальное число потоков: {bestThreads}, время: {bestTime:F2} ms\n");

        double singleThreadTime = 0;
        int singleThreadRepeats = 5;
        for (int i = 0; i < singleThreadRepeats; i++)
        {
            var sw = Stopwatch.StartNew();
            double res = DefiniteIntegral.CalculateDefiniteIntegral(a, b, f, chosenStep);
            sw.Stop();
            singleThreadTime += sw.Elapsed.TotalMilliseconds;
        }
        singleThreadTime /= singleThreadRepeats;
        Console.WriteLine($"Однопоточная реализация, среднее время: {singleThreadTime:F2} ms\n");

        double speedup = singleThreadTime / bestTime;
        double percentDiff = (speedup - 1) * 100;

        Console.WriteLine($"Ускорение: {speedup:F2}x ({percentDiff:F2}%)");

        if (percentDiff < 15)
        {
            Console.WriteLine("Разница менее 15%, рекомендуется оптимизировать многопоточную реализацию.");
        }

        using (var sw = new StreamWriter("result.txt"))
        {
            sw.WriteLine("Отчет по вычислению определенного интеграла sin(x) на [-100, 100]");
            sw.WriteLine($"Значение (шаг 1e-7): {refValue:F8}");
            sw.WriteLine($"Выбран минимальный шаг: {chosenStep:E} с {chosenStepError:E}");
            sw.WriteLine($"Оптимальное число потоков: {bestThreads}");
            sw.WriteLine($"Время оптимальной многопоточной реализации: {bestTime:F2} ms");
            sw.WriteLine($"Время однопоточной реализации: {singleThreadTime:F2} ms");
            sw.WriteLine($"Ускорение многопоточной версии относительно однопоточной: {speedup:F2}x ({percentDiff:F2}%)");
        }

        var plt = new ScottPlot.Plot();
        double[] xs = threadCounts.Select(x => (double)x).ToArray();
        double[] ys = times.ToArray();

        var scatter = plt.Add.Scatter(xs, ys);
        scatter.MarkerSize = 7;
        scatter.MarkerShape = MarkerShape.FilledCircle;
        scatter.LineWidth = 2;

        plt.Title("Зависимость времени решения интеграла от числа потоков");
        plt.XLabel("Количество потоков");
        plt.YLabel("Время выполнения, мс");
        plt.Axes.SetLimits(null, null, 0, null);

        plt.SavePng("chart.png", 800, 600);

        Console.WriteLine("График сохранён в файл chart.png");
        Console.WriteLine("Отчет записан в файл result.txt");
    }
}



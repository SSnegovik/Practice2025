using System;
using System.Threading;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        if (threadsNumber <= 0)
            throw new ArgumentException("Число должно быть положительным", nameof(threadsNumber));

        double total = 0.0;
        double segmentLength = (b - a) / threadsNumber;
        var threads = new Thread[threadsNumber];
        var barrier = new Barrier(threadsNumber + 1);

        for (int i = 0; i < threadsNumber; i++)
        {
            double start = a + i * segmentLength;
            double end = (i == threadsNumber - 1) ? b : start + segmentLength;

            threads[i] = new Thread(() =>
            {
                double partialSum = CalculateDefiniteIntegral(start, end, function, step);
                Add(ref total, partialSum);
                barrier.SignalAndWait();
            });

            threads[i].Start();
        }

        barrier.SignalAndWait();

        return total;
    }

    public static double CalculateDefiniteIntegral(double a, double b, Func<double, double> function, double step)
    {
        double sum = 0.0;
        double x = a;

        while (x < b)
        {
            double nextX = Math.Min(x + step, b);
            sum += (function(x) + function(nextX)) * (nextX - x) / 2;
            x = nextX;
        }

        return sum;
    }

    private static void Add(ref double location, double value)
    {
        double newCurrentValue = location;
        while (true)
        {
            double currentValue = newCurrentValue;
            double newValue = currentValue + value;
            newCurrentValue = Interlocked.CompareExchange(ref location, newValue, currentValue);
            if (newCurrentValue == currentValue)
                break;
        }
    }
}

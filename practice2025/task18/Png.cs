using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

public class ChartLogger
{
    private readonly string csvPath;
    private readonly string pngPath;

    public ChartLogger(string csvPath, string pngPath)
    {
        this.csvPath = csvPath;
        this.pngPath = pngPath;

        File.WriteAllText(csvPath, "Time,Command,StepsLeft\n");
    }

    public void Log(string commandName, int stepsLeft)
    {
        string line = $"{DateTime.Now:HH:mm:ss.fff},{commandName},{stepsLeft}\n";
        File.AppendAllText(csvPath, line);
    }

    [Obsolete]
    public void RenderChart()
    {
        var lines = File.ReadAllLines(csvPath);
        var data = new Dictionary<string, List<(double time, double value)>>();

        DateTime startTime = DateTime.MinValue;

        foreach (var line in lines[1..])
        {
            var parts = line.Split(',');
            if (parts.Length != 3) continue;

            string timeStr = parts[0];
            string cmd = parts[1];
            int stepsLeft = int.Parse(parts[2]);

            DateTime time = DateTime.ParseExact(timeStr, "HH:mm:ss.fff", null);
            if (startTime == DateTime.MinValue)
                startTime = time;

            double seconds = (time - startTime).TotalSeconds;

            if (!data.ContainsKey(cmd))
                data[cmd] = new List<(double, double)>();

            data[cmd].Add((seconds, stepsLeft));
        }

        var plt = new ScottPlot.Plot();

        foreach (var kvp in data)
        {
            var xs = new List<double>();
            var ys = new List<double>();

            foreach (var point in kvp.Value)
            {
                xs.Add(point.time);
                ys.Add(point.value);
            }

            var line = plt.Add.Scatter(xs, ys);
            line.Label = "Task1";
        }

        plt.Title("Выполнение команд");
        plt.XLabel("Время (секунды)");
        plt.YLabel("Оставшиеся шаги");
        plt.Legend.IsVisible = true;
        plt.Legend.Location = ScottPlot.Alignment.UpperRight;
        plt.Axes.SetLimits(null, null, 0, null);
        plt.SavePng(pngPath, 800, 600);
    }
}

using System;

class Program
{
    static void Main()
    {
        string reportPath = "report.txt";
        string chartCsv = "chart.csv";
        string chartPng = "chart.png";

        var chartLogger = new ChartLogger(chartCsv, chartPng);

        File.WriteAllText(reportPath, "=== Отчет выполнения команд ===\n");

        var scheduler = new SimpleScheduler();
        var worker = new WorkerThread(scheduler);

        scheduler.Add(new LongCommand("Task1", 5, reportPath, chartLogger));
        scheduler.Add(new LongCommand("Task2", 7, reportPath, chartLogger));

        worker.Start();

        Console.WriteLine("Нажмите Enter для остановки...");
        Console.ReadLine();

        worker.Stop();

        chartLogger.RenderChart();

        Console.WriteLine("Готово. График сохранён в chart.png");
    }
}

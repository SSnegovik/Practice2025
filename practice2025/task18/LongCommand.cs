public class LongCommand : ICommand
{
    private int stepsLeft;
    private readonly string name;
    private readonly string reportPath;
    private readonly ChartLogger logger;

    public LongCommand(string name, int steps, string reportPath, ChartLogger logger)
    {
        this.name = name;
        this.stepsLeft = steps;
        this.reportPath = reportPath;
        this.logger = logger;

        File.AppendAllText(reportPath, $"{name} с {steps} шагами\n");
    }

    public bool Execute()
    {
        Thread.Sleep(100);
        stepsLeft--;

        File.AppendAllText(reportPath,
            $"[{DateTime.Now:HH:mm:ss.fff}] {name} шаг выполнен. Осталось шагов: {stepsLeft}\n");

        logger.Log(name, stepsLeft);

        return stepsLeft <= 0;
    }
}



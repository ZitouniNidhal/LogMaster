using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the log file path:");
            string filePath = Console.ReadLine();

            LogFile logFile = new LogFile(filePath);
            logFile.LoadFile();

            Console.WriteLine($"Loaded {logFile.GetLineCount()} lines.");
            
            LogParser parser = new LogParser("yyyy-MM-dd HH:mm:ss");
            LogStatistics stats = new LogStatistics();
            stats.CalculateStats(logFile.Lines);

            Console.WriteLine($"Total Errors: {stats.ErrorCount}");
            Console.WriteLine($"Total Warnings: {stats.WarningCount}");

            AnomalyDetector detector = new AnomalyDetector(0.1);
            var anomalies = detector.DetectAnomalies(logFile.Lines);
            Console.WriteLine($"Detected {anomalies.Count} anomalies.");

            LogExporter exporter = new LogExporter("ExportedLogs");
            exporter.ExportToCSV(logFile.Lines, "ExportedLogs.csv");
            Console.WriteLine("Logs exported successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

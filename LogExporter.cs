using System;
using System.Collections.Generic;
using System.IO;
namespace LogMasterAnalyzer{
public class LogExporter
{
    public enum ExportFormat { PDF, CSV, TXT }
    public string ExportPath { get; set; }

    public LogExporter(string exportPath)
    {
        ExportPath = exportPath;
    }

    public void ExportLogs(List<string> logs, ExportFormat format, bool overwrite = false)
    {
        string filePath = GetFilePath(format);

        if (File.Exists(filePath) && !overwrite)
        {
            Console.WriteLine($"File {filePath} already exists. Use overwrite option to replace it.");
            return;
        }

        try
        {
            switch (format)
            {
                case ExportFormat.CSV:
                    LogExporter.ExportToCSV(logs, filePath);
                    break;
                case ExportFormat.TXT:
                    ExportToTXT(logs, filePath);
                    break;
                case ExportFormat.PDF:
                    Console.WriteLine("PDF export is not implemented yet.");
                    break;
                default:
                    throw new NotSupportedException("Unsupported export format.");
            }
            Console.WriteLine($"Logs exported successfully to {filePath}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting logs: {ex.Message}");
        }
    }

    public static void ExportToCSV(List<string> logs, string filePath)
    {
        File.WriteAllLines(filePath, logs);
    }

    private static void ExportToTXT(List<string> logs, string filePath)
    {
        File.WriteAllLines(filePath, logs);
    }

    private string GetFilePath(ExportFormat format)
    {
        return $"{ExportPath}.{format.ToString().ToLower()}";
    }
}
}
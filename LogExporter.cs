using System;
using System.Collections.Generic;
using System.IO;

public class LogExporter
{
    public enum ExportFormat { PDF, CSV, TXT }
    public string ExportPath { get; set; }

    public LogExporter(string exportPath)
    {
        ExportPath = exportPath;
    }

    public void ExportToCSV(List<string> logs)
    {
        File.WriteAllLines($"{ExportPath}.csv", logs);
    }

    public void ExportToTXT(List<string> logs)
    {
        File.WriteAllLines($"{ExportPath}.txt", logs);
    }
}

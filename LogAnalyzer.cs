
using System;
using System.IO;
using Newtonsoft.Json; // Make sure to install Newtonsoft.Json via NuGet
using System.Xml.Linq;
using System.Linq;

namespace LogMasterAnalyzer
{
    public class LogAnalyzer
    {
        public void AnalyzeLogs(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();

            switch (fileExtension)
            {
                case ".txt":
                case ".log":
                    AnalyzeTextLogs(filePath);
                    break;
                case ".json":
                    AnalyzeJsonLogs(filePath);
                    break;
                case ".xml":
                    AnalyzeXmlLogs(filePath);
                    break;
                default:
                    throw new NotSupportedException("Unsupported file format.");
            }
        }

        private static void AnalyzeTextLogs(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int infoCount = 0, warningCount = 0, errorCount = 0;

            foreach (var line in lines)
            {
                if (line.Contains("[Info]")) infoCount++;
                else if (line.Contains("[Warning]")) warningCount++;
                else if (line.Contains("[Error]")) errorCount++;
            }

            LogAnalyzer.DisplayStatistics(lines.Length, infoCount, warningCount, errorCount);
        }

        private void AnalyzeJsonLogs(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            dynamic logs = JsonConvert.DeserializeObject(jsonData);
            int infoCount = 0, warningCount = 0, errorCount = 0;

            foreach (var log in logs)
            {
                if (log.level == "Info") infoCount++;
                else if (log.level == "Warning") warningCount++;
                else if (log.level == "Error") errorCount++;
            }

            LogAnalyzer.DisplayStatistics(logs.Count, infoCount, warningCount, errorCount);
        }

        private void AnalyzeXmlLogs(string filePath)
        {
            XDocument xmlDoc = XDocument.Load(filePath);
            int infoCount = 0, warningCount = 0, errorCount = 0;

            foreach (var log in xmlDoc.Descendants("log"))
            {
                string level = log.Element("level")?.Value;
                if (level == "Info") infoCount++;
                else if (level == "Warning") warningCount++;
                else if (level == "Error") errorCount++;
            }

            LogAnalyzer.DisplayStatistics(xmlDoc.Descendants().Count(), infoCount, warningCount, errorCount);
        }

        private static void DisplayStatistics(int totalLines, int infoCount, int warningCount, int errorCount)
        {
            Console.WriteLine($"File statistics:");
            Console.WriteLine($"- Total lines: {totalLines}");
            Console.WriteLine($"- [Info] messages: {infoCount}");
            Console.WriteLine($"- [Warning] messages: {warningCount}");
            Console.WriteLine($"- [Error] messages: {errorCount}");
        }
    }
}
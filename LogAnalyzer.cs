using System;



namespace LogMasterAnalyzer
{
    public  class LogAnalyzer
    {
        public  void AnalyzeLogs(string[] lines)
        {
            int infoCount = 0, warningCount = 0, errorCount = 0;

            foreach (var line in lines)
            {
                if (line.Contains("[Info]")) infoCount++;
                else if (line.Contains("[Warning]")) warningCount++;
                else if (line.Contains("[Error]")) errorCount++;
            }

            Console.WriteLine($"File statistics:");
            Console.WriteLine($"- Total lines: {lines.Length}");
            Console.WriteLine($"- [Info] messages: {infoCount}");
            Console.WriteLine($"- [Warning] messages: {warningCount}");
            Console.WriteLine($"- [Error] messages: {errorCount}");
        }
    }
}


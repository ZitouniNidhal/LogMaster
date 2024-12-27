using System;
using System.Collections.Generic;

public class LogStatistics
{
    public int TotalLines { get; private set; }
    public int ErrorCount { get; private set; }
    public int WarningCount { get; private set; }
    public Dictionary<int, int> FrequencyByHour { get; private set; }

    public LogStatistics()
    {
        FrequencyByHour = new Dictionary<int, int>();
    }

    public void CalculateStats(List<string> logs)
    {
        TotalLines = logs.Count;
        foreach (var line in logs)
        {
            if (line.Contains("[Error]")) ErrorCount++;
            if (line.Contains("[Warning]")) WarningCount++;

            var parser = new LogParser("yyyy-MM-dd HH:mm:ss");
            var parsed = parser.ParseLine(line);
            if (parsed.ContainsKey("Timestamp"))
            {
                var timestamp = DateTime.Parse(parsed["Timestamp"]);
                int hour = timestamp.Hour;
                if (!FrequencyByHour.ContainsKey(hour))
                {
                    FrequencyByHour[hour] = 0;
                }
                FrequencyByHour[hour]++;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LogParser
{
    public string DateFormat { get; set; }
    public Dictionary<string, string> RegexPatterns { get; set; }

    public LogParser(string dateFormat)
    {
        DateFormat = dateFormat;
        RegexPatterns = new Dictionary<string, string>();
    }

    public Dictionary<string, string> ParseLine(string line)
    {
        // Extract example: timestamp, level, message
        var result = new Dictionary<string, string>();
        var regex = new Regex(@"(?<Timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\s+\[(?<Level>\w+)]\s+(?<Message>.+)");
        var match = regex.Match(line);

        if (match.Success)
        {
            result["Timestamp"] = match.Groups["Timestamp"].Value;
            result["Level"] = match.Groups["Level"].Value;
            result["Message"] = match.Groups["Message"].Value;
        }

        return result;
    }

    public List<string> FilterByDate(List<string> logs, DateTime start, DateTime end)
    {
        var filteredLogs = new List<string>();
        foreach (var line in logs)
        {
            var parsed = ParseLine(line);
            if (parsed.ContainsKey("Timestamp"))
            {
                DateTime timestamp = DateTime.Parse(parsed["Timestamp"]);
                if (timestamp >= start && timestamp <= end)
                {
                    filteredLogs.Add(line);
                }
            }
        }
        return filteredLogs;
    }
}

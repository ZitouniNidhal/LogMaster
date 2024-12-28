using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LogParser
{
    public string DateFormat { get; set; }
    public Dictionary<string, string> RegexPatterns { get; private set; }

    public LogParser(string dateFormat)
    {
        DateFormat = dateFormat;
        RegexPatterns = new Dictionary<string, string>();
    }

    public Dictionary<string, string> ParseLine(string line)
    {
        var result = new Dictionary<string, string>();
        string defaultPattern = @"(?<Timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\s+\[(?<Level>\w+)]\s+(?<Message>.+)";
        string pattern = RegexPatterns.ContainsKey("Default") ? RegexPatterns["Default"] : defaultPattern;

        var regex = new Regex(pattern);
        var match = regex.Match(line);

        if (match.Success)
        {
            foreach (var groupName in regex.GetGroupNames())
            {
                if (match.Groups[groupName].Success && groupName != "0")
                {
                    result[groupName] = match.Groups[groupName].Value;
                }
            }
        }

        return result;
    }

    public List<string> FilterByDate(List<string> logs, DateTime start, DateTime end)
    {
        var filteredLogs = new List<string>();

        foreach (var line in logs)
        {
            var parsed = ParseLine(line);
            if (parsed.TryGetValue("Timestamp", out var timestampStr))
            {
                if (DateTime.TryParse(timestampStr, out DateTime timestamp) && timestamp >= start && timestamp <= end)
                {
                    filteredLogs.Add(line);
                }
            }
        }

        return filteredLogs;
    }

    public List<string> FilterByLevel(List<string> logs, string level)
    {
        var filteredLogs = new List<string>();

        foreach (var line in logs)
        {
            var parsed = ParseLine(line);
            if (parsed.TryGetValue("Level", out var logLevel) && logLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
            {
                filteredLogs.Add(line);
            }
        }

        return filteredLogs;
    }

    public List<string> SearchMessages(List<string> logs, string keyword)
    {
        var matchingLogs = new List<string>();

        foreach (var line in logs)
        {
            var parsed = ParseLine(line);
            if (parsed.TryGetValue("Message", out var message) && message.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                matchingLogs.Add(line);
            }
        }

        return matchingLogs;
    }

    public List<Dictionary<string, string>> ParseMultipleLogs(List<string> logs)
    {
        var parsedLogs = new List<Dictionary<string, string>>();

        foreach (var line in logs)
        {
            var parsed = ParseLine(line);
            if (parsed.Count > 0)
            {
                parsedLogs.Add(parsed);
            }
        }

        return parsedLogs;
    }

    public void AddRegexPattern(string key, string pattern)
    {
        if (!RegexPatterns.ContainsKey(key))
        {
            RegexPatterns[key] = pattern;
        }
    }

    public void RemoveRegexPattern(string key)
    {
        if (RegexPatterns.ContainsKey(key))
        {
            RegexPatterns.Remove(key);
        }
    }

    public string FormatLog(Dictionary<string, string> log)
    {
        if (log.ContainsKey("Timestamp") && log.ContainsKey("Level") && log.ContainsKey("Message"))
        {
            return $"{log["Timestamp"]} [{log["Level"]}] {log["Message"]}";
        }

        return string.Join(" | ", log.Values);
    }
}

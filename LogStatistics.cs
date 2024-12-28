using System;
using System.Collections.Generic;

    public class LogStatistics
    {
    public int TotalLines { get; private set; }
    public int ErrorCount { get; private set; }
    public int WarningCount { get; private set; }
    public Dictionary<int, int> FrequencyByHour { get; private set; }

    private readonly LogParser _parser;

    public LogStatistics()
    {
        FrequencyByHour = new Dictionary<int, int>();
        _parser = new LogParser("yyyy-MM-dd HH:mm:ss");
    }

    public void CalculateStats(IEnumerable<string> logs)
    {
        ResetStats(); // Réinitialiser les statistiques avant de calculer

        foreach (var line in logs)
        {
            if (line.Contains("[Error]", StringComparison.OrdinalIgnoreCase)) ErrorCount++;
            if (line.Contains("[Warning]", StringComparison.OrdinalIgnoreCase)) WarningCount++;

            try
            {
                var parsed = _parser.ParseLine(line);
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
            catch (FormatException)
            {
                // Gérer les erreurs de format de date si nécessaire
            }
            catch (Exception ex)
            {
                // Gérer d'autres exceptions si nécessaire
                Console.WriteLine($"Error parsing line: {ex.Message}");
            }
        }
    }

    private void ResetStats()
    {
        TotalLines = 0;
        ErrorCount = 0;
        WarningCount = 0;
        FrequencyByHour.Clear();
    }
}

using System;
using System.Collections.Generic;

public class AnomalyDetector
{
    public double Threshold { get; set; }
    public List<string> Anomalies { get; private set; }

    public AnomalyDetector(double threshold)
    {
        Threshold = threshold;
        Anomalies = new List<string>();
    }

    public List<string> DetectAnomalies(List<string> logs)
    {
        foreach (var line in logs)
        {
            if (line.Contains("[Critical]") || line.Contains("[Fatal]"))
            {
                Anomalies.Add(line);
            }
        }
        return Anomalies;
    }
}

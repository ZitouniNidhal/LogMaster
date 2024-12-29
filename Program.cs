using System;
using System.IO;



namespace LogMasterAnalyzer
{
static class Program
{
  [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to LogMaster Analyzer!");

            // Use FileSelector to choose a file
            FileSelector fileSelector = new FileSelector();
            string filePath = fileSelector.SelectLogFile();

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("No file selected. Exiting application.");
                return;
            }

            Console.WriteLine($"Selected file: {filePath}");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file does not exist.");
                return;
            }

            Console.WriteLine("Loading and analyzing the file...");
            string[] lines = File.ReadAllLines(filePath);

            // Use LogAnalyzer to analyze the file
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            logAnalyzer.AnalyzeLogs(lines);

            Console.WriteLine("Analysis complete.");
        }
    }
}
using System;
using System.IO;
using System.Windows.Forms;

namespace LogMasterAnalyzer
{
/*static class Program
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
    }*/
    using System;
using System.Windows.Forms;

namespace LogMasterAnalyzer
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
      {
    try
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());  // Lance la fenêtre principale (MainForm)
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Une erreur est survenue : {ex.Message}\n\n{ex.StackTrace}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
    }
}

}
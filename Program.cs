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


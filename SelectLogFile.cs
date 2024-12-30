using System.Windows.Forms;

namespace LogMasterAnalyzer
{
    public class FileSelector
    {
        public string SelectLogFile()
        {
            #if WINDOWS
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            // Modifier le filtre pour inclure tous les types de fichiers
                            openFileDialog.Filter = "Tous les fichiers (*.*)|*.*"; // Sélectionne tous les fichiers
                            openFileDialog.Title = "Sélectionner un fichier de log";
            
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                return openFileDialog.FileName;
                            }
                        }
            #endif
            
                        return string.Empty;
        }
    }
}
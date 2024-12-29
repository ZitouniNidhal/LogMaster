using System.Windows.Forms;


namespace LogMasterAnalyzer
{
    

    public  class FileSelector
    {
        public string SelectLogFile()
        {
            #if WINDOWS
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = "Text Files (*.txt;*.log)|*.txt;*.log|All Files (*.*)|*.*";
                            openFileDialog.Title = "Select a log file";
            
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
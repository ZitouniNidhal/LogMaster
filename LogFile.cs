using System;
using System.Collections.Generic;
using System.IO;
namespace LogMasterAnalyzer
{
public class LogFile
{
    public string FilePath { get; set; }
    public long FileSize { get; private set; }
    public List<string> Lines { get; private set; }
    public DateTime LastModified { get; private set; }

    public  LogFile(string filePath)
    {
        FilePath = filePath;
        Lines = new List<string>();
    }

    public void LoadFile()
    {
        if (File.Exists(FilePath))
        {
            Lines = new List<string>(File.ReadAllLines(FilePath));
            FileSize = new FileInfo(FilePath).Length;
            LastModified = File.GetLastWriteTime(FilePath);
        }
        else
        {
            throw new FileNotFoundException("Log file not found.");
        }
    }

    public int GetLineCount()
    {
        return Lines?.Count ?? 0;
    }

    public List<string> Search(string keyword)
    {
        return Lines.FindAll(line => line.Contains(keyword));
    }
}
}

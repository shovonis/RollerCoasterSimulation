using System;
using System;
using System.IO;

public class Logger
{
    private static string logFileName = "logs/system.log";

    public static void Log( string logLevel, string logText)
    {
        using (StreamWriter writer = File.AppendText(logFileName))
        {
            string logTime = DateTime.Now.ToString("yyyy.MM.dd HHHH:mm:ss:fff");
            writer.WriteLine("[" + logLevel + "][" + logTime + "]::" + logText);
        }
    }
}
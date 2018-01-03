using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;

/// <summary>
/// Summary description for LogFile
/// </summary>
public class LogFile
{
    public static string strPath = AppDomain.CurrentDomain.BaseDirectory;
    public static string strLogFilePath = strPath + @"Log\Log.txt";

    public static void WriteToLog(string eventname, Exception msg)
    {
        try
        {
            if (!File.Exists(strLogFilePath))
            {
                File.Create(strLogFilePath).Close();
            }
            using (StreamWriter w = File.AppendText(strLogFilePath))
            {
                w.Write("\rLog Date & Time : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                w.Write("\rEvent Name : ");
                w.WriteLine("{0}", eventname);
                string err = "Error Message : " + msg.ToString();
                w.WriteLine(err);
                w.WriteLine();
                w.WriteLine();

                w.Flush();
                w.Close();
            }
        }
        catch { }
    }
}
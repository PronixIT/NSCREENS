using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Summary description for SendLogFile
/// </summary>
public class SendLogFile
{
    public static string strPath = AppDomain.CurrentDomain.BaseDirectory;
    public static string strLogFilePath = strPath + @"Log\Error.txt";

    public static void SendMail()
    {
        try
        {
            MailMessage mail = new MailMessage();

            mail.To.Add("nscreens.eluru@gmail.com");
            mail.From = new MailAddress("nscreens.eluru@gmail.com");
            mail.Subject = "Bhavanthi_LogFile";
            mail.Body = "Error";
            mail.IsBodyHtml = true;

            string s = AppDomain.CurrentDomain.BaseDirectory + @"Log\Log.txt";
            string t = AppDomain.CurrentDomain.BaseDirectory + @"Log\Error.txt"; //"D:/Error.txt"; 
            if (File.Exists(t))
            {
                mail.Attachments.Clear();
                File.Delete(t);
            }
            File.Copy(s, t);

            mail.Attachments.Add(new Attachment(t));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.EnableSsl = true;

            //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            smtp.Credentials = new System.Net.NetworkCredential("nscreens.eluru@gmail.com", "9885908149");//Or your Smtp Email ID and Password

            //smtp.Send(mail);
            ((IDisposable)mail).Dispose();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
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
public class SendEmail
{
    public static string File;
    public static string htmltemplatePath = AppDomain.CurrentDomain.BaseDirectory;
    public static string strhtmltemplate = htmltemplatePath + "@RegisterTemplate.html";

    public static void SendMailRegistration(string body,string EmailId)
    {
        try
        {
            //MailMessage mail = new MailMessage();
            ////string body=createEmailBody("test", "test", "test");
            //mail.To.Add(EmailId);
            //mail.From = new MailAddress("nscreens.eluru@gmail.com");
            //mail.Subject = "SIMPLE REGISTER";
            //mail.Body = "test";
            //mail.IsBodyHtml = true;         
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            ////smtp.Port = 587;     
            //smtp.Credentials = new System.Net.NetworkCredential("nscreens.eluru@gmail.com", "9885908149");//Or your Smtp Email ID and Password
            //smtp.EnableSsl = true;
            ////smtp.UseDefaultCredentials = true;


            //smtp.Send(mail);
            //((IDisposable)mail).Dispose();


        

            string fromAddress = "nscreens.eluru@gmail.com";
            string mailPassword = "9885908149";       // Mail id password from where mail will be sent.
            string messageBody = body;// "Write the body of the message here.";


            // Create smtp connection.
            SmtpClient client = new SmtpClient();
            client.Port = 587;//outgoing port for the mail.
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);


            // Fill the mail form.
            var send_mail = new MailMessage();

            send_mail.IsBodyHtml = true;
            //address from where mail will be sent.
            send_mail.From = new MailAddress("nscreens.eluru@gmail.com");
            //address to which mail will be sent.           
            send_mail.To.Add(new MailAddress(EmailId));
            //subject of the mail.
            send_mail.Subject = "NScreens";

            send_mail.Body = messageBody;
            client.Send(send_mail);
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            //ShowNotification("API", dispErrorMsg, NotificationType.error);
            //SendLogFile.SendMail();
        }
    }

    public static string createEmailBody(string Name,string userName, string MobileNo, string EmailId, string Password,string FileName)
    {

        string body = string.Empty;
        //using streamreader for reading my htmltemplate   

        using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+FileName))
        {

            body = reader.ReadToEnd();

        }

        body = body.Replace("UserName", userName); //replacing the required things  

        body = body.Replace("Name", Name);

        body = body.Replace("MobileNo", MobileNo);

        body = body.Replace("Passwords", Password);

        SendMailRegistration(body, EmailId);

        return "";

    }  
}
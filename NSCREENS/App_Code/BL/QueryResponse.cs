using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Services;


public class QueryResponse
{
    public static string output = "Process Completed";


    [WebMethod]
    public static string ResponseProcess(string transid, string opmsg, string bal, string status, string optransid)
    {
       
        try
        {


          DataSet objDataSet2 = MasterCode.ExcuteCommonResponse(optransid, opmsg, "", "0", bal, transid, status, "", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt"));

           
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
           

        }
        return output;

    }

   
}
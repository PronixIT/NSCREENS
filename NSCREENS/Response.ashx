<%@ WebHandler Language="C#" Class="Response" %>

using System;
using System.Web;
using System.Diagnostics;

public class Response : IHttpHandler
{


    public void ProcessRequest(HttpContext context)
    {
        string transid, output, optransid, opmsg, bal, status;
        transid = context.Request.QueryString["transid"];
        opmsg = context.Request.QueryString["opmsg"];
        bal = context.Request.QueryString["bal"];
        status = context.Request.QueryString["status"];
        optransid = context.Request.QueryString["optransid"];

        try
        {
            output = QueryResponse.ResponseProcess(transid, opmsg, bal, status, optransid);
        }
        catch (Exception Ex)
        {
            output = "Server Error Please wait";
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
        }

        string rOput = context.Request.Params["callback"];
        rOput += "" + output + "";
        context.Response.Write(rOput);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
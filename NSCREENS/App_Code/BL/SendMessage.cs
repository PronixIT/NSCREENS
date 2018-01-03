using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SendMessage
/// </summary>
public class SendMessage:Connection
{
	public SendMessage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _SendFrom;
    private string _SendTo;
    private string _Message;
    private DateTime _CreatedDate;
    private string _UserId;
    private string _Subject;

    public string Subject
    {
        get { return _Subject; }
        set { _Subject = value; }
    }

    public string UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    public DateTime CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }

    public string Message
    {
        get { return _Message; }
        set { _Message = value; }
    }

    public string SendTo
    {
        get { return _SendTo; }
        set { _SendTo = value; }
    }

    public string SendFrom
    {
        get { return _SendFrom; }
        set { _SendFrom = value; }
    }

    public static DataSet Send_Message(SendMessage objSendMessage)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[5];

            objSqlParameter[0] = new SqlParameter("@Message", objSendMessage.Message);
            objSqlParameter[1] = new SqlParameter("@SendFrom", objSendMessage.SendFrom);
            objSqlParameter[2] = new SqlParameter("@SendTo", objSendMessage.SendTo);
            objSqlParameter[3] = new SqlParameter("@CreatedDate", objSendMessage.CreatedDate);
            objSqlParameter[4] = new SqlParameter("@Subject", objSendMessage.Subject);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_SendMessage", objSqlParameter);
        }
        catch(Exception Ex)
        {
            throw Ex;
        }
    }

}
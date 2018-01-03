using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Admin_Channel
/// </summary>
public class Admin_Channel:Connection
{
	public Admin_Channel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _ChannelName;
    private int _ChannelId;
    private string _DumpChannelName;
    private int _UserId;
    private DateTime _CreatedDate;
    private bool _Isactive;
    private string _Description;

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }

    public bool Isactive
    {
        get { return _Isactive; }
        set { _Isactive = value; }
    }

    public DateTime CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }
    
    public int UserId
    {
      get { return _UserId; }
      set { _UserId = value; }
    }

    public string DumpChannelName
    {
        get { return _DumpChannelName; }
        set { _DumpChannelName = value; }
    }

    public int ChannelId
    {
        get { return _ChannelId; }
        set { _ChannelId = value; }
    }

    public string ChannelName
    {
        get { return _ChannelName; }
        set { _ChannelName = value; }
    }

    public static DataSet Channel_Send_To_DB(Admin_Channel objAdmin_Channel)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[7];

            objSqlParameter[0] = new SqlParameter("@ChannelName", objAdmin_Channel.ChannelName);
            objSqlParameter[1] = new SqlParameter("@ChannelId", objAdmin_Channel.ChannelId);
            objSqlParameter[2] = new SqlParameter("@DumpChannelName", objAdmin_Channel.DumpChannelName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_Channel.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_Channel.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Channel.UserId);
            objSqlParameter[6] = new SqlParameter("@Description", objAdmin_Channel.Description);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Channel", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
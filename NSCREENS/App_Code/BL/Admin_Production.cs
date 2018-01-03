using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Admin_Production
/// </summary>
public class Admin_Production:Connection
{
	public Admin_Production()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _ProductionName;
    private int _ProductionId;
    private string _DumpProductionName;
    private int _UserId;
    private DateTime _CreatedDate;
    private bool _Isactive;
    private string _Description;
    private string _Img;

    public string Img
    {
        get { return _Img; }
        set { _Img = value; }
    }

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

    public string DumpProductionName
    {
        get { return _DumpProductionName; }
        set { _DumpProductionName = value; }
    }

    public int ProductionId
    {
        get { return _ProductionId; }
        set { _ProductionId = value; }
    }

    public string ProductionName
    {
        get { return _ProductionName; }
        set { _ProductionName = value; }
    }

    public static DataSet Production_Send_To_DB(Admin_Production objAdmin_Production)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[8];

            objSqlParameter[0] = new SqlParameter("@ChannelName", objAdmin_Production.ProductionName);
            objSqlParameter[1] = new SqlParameter("@ChannelId", objAdmin_Production.ProductionId);
            objSqlParameter[2] = new SqlParameter("@DumpChannelName", objAdmin_Production.DumpProductionName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_Production.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_Production.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Production.UserId);
            objSqlParameter[6] = new SqlParameter("@Description", objAdmin_Production.Description);
            objSqlParameter[7] = new SqlParameter("@Img", objAdmin_Production.Img);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Channel", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
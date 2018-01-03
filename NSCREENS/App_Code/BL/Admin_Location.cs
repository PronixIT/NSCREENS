using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Admin_Location
/// </summary>
public class Admin_Location:Connection
{
	public Admin_Location()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _CityId;
    private string _Location_Name;
    private string _DumpLocation;
    private DateTime _CreatedDate;
    private int _UserId;
    private bool _Isactive;
    private int _DumpCityId;
    private int _LocationID;

    public int LocationID
    {
        get { return _LocationID; }
        set { _LocationID = value; }
    }

    public int DumpCityId
    {
        get { return _DumpCityId; }
        set { _DumpCityId = value; }
    }

    public bool Isactive
    {
        get { return _Isactive; }
        set { _Isactive = value; }
    }

    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    public DateTime CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }

    public string DumpLocation
    {
        get { return _DumpLocation; }
        set { _DumpLocation = value; }
    }

    public string Location_Name
    {
        get { return _Location_Name; }
        set { _Location_Name = value; }
    }

    public int CityId
    {
        get { return _CityId; }
        set { _CityId = value; }
    }

    public static DataSet Location_Send_To_DB(Admin_Location objAdmin_Location)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[8];

            objSqlParameter[0] = new SqlParameter("@CityId", objAdmin_Location.CityId);
            objSqlParameter[1] = new SqlParameter("@Location_Name", objAdmin_Location.Location_Name);
            objSqlParameter[2] = new SqlParameter("@DumpLocation", objAdmin_Location.DumpLocation);
            objSqlParameter[3] = new SqlParameter("@LocationID", objAdmin_Location.LocationID);
            objSqlParameter[4] = new SqlParameter("@Isactive", objAdmin_Location.Isactive);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Location.UserId);
            objSqlParameter[6] = new SqlParameter("@CreatedDate", objAdmin_Location.CreatedDate);
            objSqlParameter[7] = new SqlParameter("@DumpCityId", objAdmin_Location.DumpCityId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Location", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
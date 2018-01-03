using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Admin_User
/// </summary>
public class Admin_User:Connection
{
	public Admin_User()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _Name;
    private string _Mobile;
    private string _EmailId;
    private string _Address;
    private int _UserId;
    private DateTime _CreatedDate;
    private DateTime _DOB;
    private string _Img;
    private int _RegisterId;
    private string _DumpEmailId;
    private int _CityId;
    private string _Gender;
    private string _DumpCityId;

    public string DumpCityId
    {
        get { return _DumpCityId; }
        set { _DumpCityId = value; }
    }

    public string Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }

    public int CityId
    {
        get { return _CityId; }
        set { _CityId = value; }
    }

    public string DumpEmailId
    {
        get { return _DumpEmailId; }
        set { _DumpEmailId = value; }
    }

    public int RegisterId
    {
        get { return _RegisterId; }
        set { _RegisterId = value; }
    }

    public string Img
    {
        get { return _Img; }
        set { _Img = value; }
    }

    public DateTime DOB
    {
        get { return _DOB; }
        set { _DOB = value; }
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

    public string Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

    public string EmailId
    {
        get { return _EmailId; }
        set { _EmailId = value; }
    }

    public string Mobile
    {
        get { return _Mobile; }
        set { _Mobile = value; }
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public static DataSet Send_User_To_DB(Admin_User objAdmin_User)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[13];

            objSqlParameter[0] = new SqlParameter("@Name", objAdmin_User.Name);
            objSqlParameter[1] = new SqlParameter("@Mobile", objAdmin_User.Mobile);
            objSqlParameter[2] = new SqlParameter("@Address", objAdmin_User.Address);
            objSqlParameter[3] = new SqlParameter("@EmailId", objAdmin_User.EmailId);
            objSqlParameter[4] = new SqlParameter("@DOB", objAdmin_User.DOB);
            objSqlParameter[5] = new SqlParameter("@Img", objAdmin_User.Img);
            objSqlParameter[6] = new SqlParameter("@RegisterId", objAdmin_User.RegisterId);
            objSqlParameter[7] = new SqlParameter("@DumpEmailId", objAdmin_User.DumpEmailId);
            objSqlParameter[8] = new SqlParameter("@CityId", objAdmin_User.CityId);
            objSqlParameter[9] = new SqlParameter("@UserId", objAdmin_User.UserId);
            objSqlParameter[10] = new SqlParameter("@CreatedDate", objAdmin_User.CreatedDate);
            objSqlParameter[11] = new SqlParameter("@Gender", objAdmin_User.Gender);
            objSqlParameter[12] = new SqlParameter("@DumpCityId", objAdmin_User.DumpCityId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Register_user", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
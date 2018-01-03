using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for User_Comments
/// </summary>
public class User_Comments:Connection
{
	public User_Comments()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _Name;
    private string _Email;
    private string _Phone;
    private string _Message;
    private string _UserId;
    private DateTime _CreatedDate;
    private int _ShortFilm;

    public int ShortFilm
    {
        get { return _ShortFilm; }
        set { _ShortFilm = value; }
    }

    public DateTime CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }

    public string UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    public string Message
    {
        get { return _Message; }
        set { _Message = value; }
    }

    public string Phone
    {
        get { return _Phone; }
        set { _Phone = value; }
    }

    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public static DataSet Send_To_DB(User_Comments objUser_Comments)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[7];

            objSqlParameter[0] = new SqlParameter("@Name", objUser_Comments.Name);
            objSqlParameter[1] = new SqlParameter("@Phone", objUser_Comments.Phone);
            objSqlParameter[2] = new SqlParameter("@Email", objUser_Comments.Email);
            objSqlParameter[3] = new SqlParameter("@Message", objUser_Comments.Message);
            objSqlParameter[4] = new SqlParameter("@UserId", objUser_Comments.UserId);
            objSqlParameter[5] = new SqlParameter("@CreatedDate", objUser_Comments.CreatedDate);
            objSqlParameter[6] = new SqlParameter("@ShortFilm", objUser_Comments.ShortFilm);

            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_Comments", objSqlParameter);
        }
        catch(Exception Ex)
        {
            throw Ex;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Admin_Category
/// </summary>
public class Admin_Category:Connection
{
	public Admin_Category()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _CategoryName;
    private int _CategoryId;
    private string _DumpCategoryName;
    private bool _Isactive;
    private DateTime _CreatedDate;
    private int _UserId;

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

    public bool Isactive
    {
        get { return _Isactive; }
        set { _Isactive = value; }
    }

    public string DumpCategoryName
    {
        get { return _DumpCategoryName; }
        set { _DumpCategoryName = value; }
    }

    public int CategoryId
    {
        get { return _CategoryId; }
        set { _CategoryId = value; }
    }

    public string CategoryName
    {
        get { return _CategoryName; }
        set { _CategoryName = value; }
    }

    public static DataSet Category_Send_To_DB(Admin_Category objAdmin_Category)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[6];

            objSqlParameter[0] = new SqlParameter("@CategoryName", objAdmin_Category.CategoryName);
            objSqlParameter[1] = new SqlParameter("@CategoryId", objAdmin_Category.CategoryId);
            objSqlParameter[2] = new SqlParameter("@DumpCategoryName", objAdmin_Category.DumpCategoryName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_Category.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_Category.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Category.UserId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Category", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
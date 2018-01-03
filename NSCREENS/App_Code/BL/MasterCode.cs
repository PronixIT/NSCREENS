using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Master
/// </summary>
public class MasterCode:Connection
{
    #region Private Variables

    private string _UserName;
    private string _Password;
    private string _IPAddress;
    private DateTime _DateTime;
    private int _UserId;
    private string _IsActive;
    private string _OldPassword;
    private string _NewPassword;
    private int _UserCode;
    private string _UserRights;

    #endregion

    #region Public Properties

    public string UserRights
    {
        get { return _UserRights; }
        set { _UserRights = value; }
    }

    public int UserCode
    {
        get { return _UserCode; }
        set { _UserCode = value; }
    }

    public string NewPassword
    {
        get { return _NewPassword; }
        set { _NewPassword = value; }
    }

    public string OldPassword
    {
        get { return _OldPassword; }
        set { _OldPassword = value; }
    }

    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }

    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    public DateTime DateTime
    {
        get { return _DateTime; }
        set { _DateTime = value; }
    }

    public string IPAddress
    {
        get { return _IPAddress; }
        set { _IPAddress = value; }
    }

    public string Password
    {
        get { return _Password; }
        set { _Password = value; }
    }

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }

    #endregion

    #region Public Methods

    public static DataSet SendDate(DateTime DisplayDate,string AddColoum)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[2];

            objSqlParameter[0] = new SqlParameter("@Date", DisplayDate);
            objSqlParameter[1] = new SqlParameter("@AddColoum", AddColoum);

           return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Bill_Display", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public DataSet GetStaffDetailsById()
    {
        SqlParameter p = new SqlParameter("@UserId", this._UserId);
        p.DbType = DbType.Int32;
        return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_GetUserDetailsById", p);
    }

    public static DataSet RetrieveQuery(string Query)
    {
        try
        {
            return SqlHelper.ExecuteDataSet(con, CommandType.Text, Query, (SqlParameter[])null);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet ExecuteStoredProcedure(string ProcedureName)
    {
        try
        {
            return SqlHelper.ExecuteDataSet(con,CommandType.StoredProcedure,ProcedureName,(SqlParameter[])null);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet ExcuteProcedureWithSingleParameter(int Parameter1,string ProcedureName,string LogOutDatetime)
    {
        try
        {
            SqlParameter[] objSqlParameter=new SqlParameter[2];

            objSqlParameter[0] = new SqlParameter("@Parameter1", Parameter1);
            objSqlParameter[1] = new SqlParameter("@LogOutDatetime", LogOutDatetime);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, ProcedureName, objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet InsertLoginDetails(MasterCode objMasterCode)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[5];

            objSqlParameter[0] = new SqlParameter("@UserName", objMasterCode.UserName);
            objSqlParameter[1] = new SqlParameter("@Password", objMasterCode.Password);
            objSqlParameter[2] = new SqlParameter("@IPAddress", objMasterCode.IPAddress);
            objSqlParameter[3] = new SqlParameter("@DateTime", objMasterCode.DateTime);
            objSqlParameter[4] = new SqlParameter("@UserId", objMasterCode.UserId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "SP_Login_InsertLogDetails", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public DataSet GetUserLoginDetails()
    {
        SqlParameter[] p = new SqlParameter[3];

        p[0] = new SqlParameter("@UserName", this._UserName);
        p[0].DbType = DbType.String;

        p[1] = new SqlParameter("@Password", this._Password);
        p[1].DbType = DbType.String;

        p[2] = new SqlParameter("@UserCode", this._UserCode);
        p[2].DbType = DbType.String;

        return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_GetUserLoginDetails", p);
    }

    public static int InsertFail(MasterCode objMasterCode)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[4];

            objSqlParameter[0] = new SqlParameter("@UserName", objMasterCode.UserName);
            objSqlParameter[1] = new SqlParameter("@Password", objMasterCode.Password);
            objSqlParameter[2] = new SqlParameter("@IPAddress", objMasterCode.IPAddress);
            objSqlParameter[3] = new SqlParameter("@DateTime", objMasterCode.DateTime);

            return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "Sp_Login_InsertFail", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public int Block()
    {
        SqlParameter[] objSqlParameter = new SqlParameter[2];

        objSqlParameter[0] = new SqlParameter("@IsActive", this._IsActive);
        objSqlParameter[0].DbType = DbType.String;

        objSqlParameter[1] = new SqlParameter("@UserName", this._UserName);
        objSqlParameter[0].DbType = DbType.String;

        return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "Sp_Login_SetBlock", objSqlParameter);
    }

    public DataSet GetCount(string Username, string UserId, DateTime Date)
    {
        return SqlHelper.ExecuteDataset(con, CommandType.Text, "select UserName from tbl_LoginFailed where Username='" + Username + "' and IPAddress='" + UserId + "' and DateTime='" + Date + "'", (SqlParameter[])null);
    }

    public static DataSet ChangePassword(MasterCode objMasterCode)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[3];

            objSqlParameter[0] = new SqlParameter("@UserName", objMasterCode.UserName);
            objSqlParameter[1] = new SqlParameter("@OldPassword", objMasterCode.OldPassword);
            objSqlParameter[2] = new SqlParameter("@NewPassword", objMasterCode.NewPassword);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Login_ChangePassword", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet ActiveBolckedUsers(MasterCode objMasterCode)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[1];

            objSqlParameter[0] = new SqlParameter("@UserName", objMasterCode.UserName);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Update_ActiveBolckedUsers", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public DataSet RetrieveUP(string emailid)
    {
        return SqlHelper.ExecuteDataset(con, CommandType.Text, "select * from tblUsers where EmployeeDetailsID in (select EmployeeId from tbl_EmployeeDetails where PrivateEmail='" + emailid + "')", (SqlParameter[])null);
    }

    public DataSet GetUnameBname()
    {
        return SqlHelper.ExecuteDataset(con, CommandType.Text, "select User_Id,Username from tbl_user", (SqlParameter[])null);
    }

    public DataSet GetUserType()
    {
        return SqlHelper.ExecuteDataset(con, CommandType.Text, "select distinct UserType from tblUsers", (SqlParameter[])null);
    }

    public DataSet GetStaffDetails(int Uid)
    {
        return SqlHelper.ExecuteDataset(con, CommandType.Text, "select * from tblUsers where Id=" + Uid, (SqlParameter[])null);
    }

    public DataSet GetEmployeeRights(string UserType)
    {
        return SqlHelper.ExecuteDataset(con, CommandType.Text, "select distinct UserRights from tblUsers where UserType='" + UserType + "'", (SqlParameter[])null);
    }

    public int UpdateRights(string Uid, int id)
    {
        return SqlHelper.ExecuteNonQuery(con, CommandType.Text, "update tblUsers set UserRights='" + Uid + "' where Id=" + id, (SqlParameter[])null);
    }

    public int UpdateRightsForUser(string Uid, string UserType)
    {
        return SqlHelper.ExecuteNonQuery(con, CommandType.Text, "update tblUsers set UserRights='" + Uid + "' where UserType='" + UserType.ToString() + "'", (SqlParameter[])null);
    }

    public DataSet GetBlockDetails()
    {
        return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_GetBlockDetails", (SqlParameter[])null);
    }

    public int DeleteUser(string UserName)
    {
        return SqlHelper.ExecuteNonQuery(con, CommandType.Text, "Delete from tblLoginFailed where UserName='" + UserName + "'", (SqlParameter[])null);
    }

    public int UpdateRightsANDActiveUser()
    {
        SqlParameter[] p = new SqlParameter[2];

        p[0] = new SqlParameter("@UserId", this._UserId);
        p[0].DbType = DbType.Int32;
        p[1] = new SqlParameter("@UserRights", this._UserRights);
        p[1].DbType = DbType.String;

        return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "Sp_UpdateRightsANDActiveUser", p);
    }

    public static DataSet ExcuteOneParameter(string Parameter, string Procedure, string Parameter1)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[2];

            objSqlParameter[0] = new SqlParameter("@Parameter", Parameter);
            objSqlParameter[1] = new SqlParameter("@Parameter1", Parameter1);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, Procedure, objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet ExcuteCommonResponse(string @optransid, string @opmsg, string @MobileNo, string @amt, string @bal, string @transid, string @status, string @ResponseCode, string @Date)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[9];

            objSqlParameter[0] = new SqlParameter("@optransid", @optransid);
            objSqlParameter[1] = new SqlParameter("@opmsg", @opmsg);
            objSqlParameter[2] = new SqlParameter("@MobileNo", @MobileNo);
            objSqlParameter[3] = new SqlParameter("@amt", @amt);
            objSqlParameter[4] = new SqlParameter("@bal", @bal);
            objSqlParameter[5] = new SqlParameter("@transid", @transid);
            objSqlParameter[6] = new SqlParameter("@status", @status);
            objSqlParameter[7] = new SqlParameter("@ResponseCode", @ResponseCode);
            objSqlParameter[8] = new SqlParameter("@Date", @Date);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "SP_CommonResponse", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion
}
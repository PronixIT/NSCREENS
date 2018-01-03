using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for Recharge
/// </summary>
public class Recharge:Connection
{
	public Recharge()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _CreatedDate;
    private string _MobileNo;
    private decimal _Amount;
    private string _OperatorCode;
    private int _UserId;
    private string _Area;
    private string _RechargeTypeIds;
    private string _TTType;
    private string _Days;
    private string _Description;

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }

    public string Days
    {
        get { return _Days; }
        set { _Days = value; }
    }

    public string TTType
    {
        get { return _TTType; }
        set { _TTType = value; }
    }
    public string RechargeTypeIds
    {
        get { return _RechargeTypeIds; }
        set { _RechargeTypeIds = value; }
    }

    public string Area
    {
        get { return _Area; }
        set { _Area = value; }
    }

    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    public string OperatorCode
    {
        get { return _OperatorCode; }
        set { _OperatorCode = value; }
    }

    public decimal Amount
    {
        get { return _Amount; }
        set { _Amount = value; }
    }

    public string MobileNo
    {
        get { return _MobileNo; }
        set { _MobileNo = value; }
    }

    public string CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }

    public static DataSet Send_Data_To_DB(Recharge objRecharge)
    {
        try
        {
            SqlParameter[] objSqlParameter=new SqlParameter[6];

            objSqlParameter[0] = new SqlParameter("@Amount", objRecharge.Amount);
            objSqlParameter[1] = new SqlParameter("@OperatorCode", objRecharge.OperatorCode);
            objSqlParameter[2] = new SqlParameter("@CreatedDate", objRecharge.CreatedDate);
            objSqlParameter[3] = new SqlParameter("@UserId", objRecharge.UserId);
            objSqlParameter[4] = new SqlParameter("@Area", objRecharge.Area);
            objSqlParameter[5] = new SqlParameter("@MobileNo", objRecharge.MobileNo);

            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_Recharge", objSqlParameter);
        }
        catch(Exception Ex)
        {
           throw Ex;
        }
    }

    public static DataSet Recharge_Data_To_DB(Recharge objRecharge)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[9];

            objSqlParameter[0] = new SqlParameter("@Amount", objRecharge.Amount);
            objSqlParameter[1] = new SqlParameter("@OperatorCode", objRecharge.OperatorCode);
            objSqlParameter[2] = new SqlParameter("@CreatedDate", objRecharge.CreatedDate);
            objSqlParameter[3] = new SqlParameter("@UserId", objRecharge.UserId);
            objSqlParameter[4] = new SqlParameter("@Area", objRecharge.Area);
            objSqlParameter[5] = new SqlParameter("@RechargeTypeIds", objRecharge.RechargeTypeIds);
            objSqlParameter[6] = new SqlParameter("@TTType", objRecharge.TTType);
            objSqlParameter[7] = new SqlParameter("@Days", objRecharge.Days);
            objSqlParameter[8] = new SqlParameter("@Description", objRecharge.Description);

            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_Offers", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static string Recharge_Bal(string MobileNo, decimal amt, string rcode, string transid)
    {
        try
        {
            string output = "";

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.simpleapi.in/recharge.ashx?uid=7893123555&pwd=12345&mobileno=" + MobileNo + "&amt=" + amt + "&rcode=" + rcode + "&transid=" + transid + "");
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                string text = sr.ReadToEnd();
                output = text;
                DataSet objDataSet1 = MasterCode.RetrieveQuery("update Request set  ResponseCode= '" + text.ToString() + "' where ClientTXT='" + transid + "'");
            }

            return output;//SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Sp_Offers", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
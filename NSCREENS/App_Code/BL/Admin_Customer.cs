using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Admin_Customer
/// </summary>
public class Admin_Customer : Connection
{
    public Admin_Customer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _FirstName;
    private string _Lastname;
    private string _MobileNo;
    private int _CityId;
    private string _PostedBy;
    private int _UserId;
    private DateTime _CreatedDate;
    private bool _Isactive;
    private string _EmailId;
    private int _NatureofJob;
    private int _VendorId;
    private int _CustomerId;
    private string _DumpMobileNo;
    private string _Description;
    private string _BillingAddress;
    private string _DeliveryAddress;
    private int _LocationId;
    private string _Designation;

    public string Designation
    {
        get { return _Designation; }
        set { _Designation = value; }
    }

    public int LocationId
    {
        get { return _LocationId; }
        set { _LocationId = value; }
    }

    public string DeliveryAddress
    {
        get { return _DeliveryAddress; }
        set { _DeliveryAddress = value; }
    }

    public string BillingAddress
    {
        get { return _BillingAddress; }
        set { _BillingAddress = value; }
    }

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }

    public string DumpMobileNo
    {
        get { return _DumpMobileNo; }
        set { _DumpMobileNo = value; }
    }

    public int CustomerId
    {
        get { return _CustomerId; }
        set { _CustomerId = value; }
    }

    public int VendorId
    {
        get { return _VendorId; }
        set { _VendorId = value; }
    }

    public int NatureofJob
    {
        get { return _NatureofJob; }
        set { _NatureofJob = value; }
    }

    public string EmailId
    {
        get { return _EmailId; }
        set { _EmailId = value; }
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

    public string PostedBy
    {
        get { return _PostedBy; }
        set { _PostedBy = value; }
    }

    public int CityId
    {
        get { return _CityId; }
        set { _CityId = value; }
    }

    public string MobileNo
    {
        get { return _MobileNo; }
        set { _MobileNo = value; }
    }

    public string Lastname
    {
        get { return _Lastname; }
        set { _Lastname = value; }
    }

    public string FirstName
    {
        get { return _FirstName; }
        set { _FirstName = value; }
    }

    public static DataSet Send_Customer_To_DB(Admin_Customer objAdmin_Customer)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[11];

            objSqlParameter[0] = new SqlParameter("@FirstName", objAdmin_Customer.FirstName);
            objSqlParameter[1] = new SqlParameter("@Lastname", objAdmin_Customer.Lastname);
            objSqlParameter[2] = new SqlParameter("@MobileNo", objAdmin_Customer.MobileNo);
            objSqlParameter[3] = new SqlParameter("@CityId", objAdmin_Customer.CityId);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_Customer.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Customer.UserId);
            objSqlParameter[6] = new SqlParameter("@EmailId", objAdmin_Customer.EmailId);
            objSqlParameter[7] = new SqlParameter("@CustomerId", objAdmin_Customer.CustomerId);
            objSqlParameter[8] = new SqlParameter("@DumpMobileNo", objAdmin_Customer.DumpMobileNo);
            objSqlParameter[9] = new SqlParameter("@BillingAddress", objAdmin_Customer.BillingAddress);
            objSqlParameter[10] = new SqlParameter("@DeliveryAddress", objAdmin_Customer.DeliveryAddress);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Customer", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Send_Vendor_To_DB(Admin_Customer objAdmin_Customer)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[11];

            objSqlParameter[0] = new SqlParameter("@FirstName", objAdmin_Customer.FirstName);
            objSqlParameter[1] = new SqlParameter("@Lastname", objAdmin_Customer.Lastname);
            objSqlParameter[2] = new SqlParameter("@MobileNo", objAdmin_Customer.MobileNo);
            objSqlParameter[3] = new SqlParameter("@LocationId", objAdmin_Customer.LocationId);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_Customer.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Customer.UserId);
            objSqlParameter[6] = new SqlParameter("@EmailId", objAdmin_Customer.EmailId);
            objSqlParameter[7] = new SqlParameter("@NatureofJob", objAdmin_Customer.NatureofJob);
            objSqlParameter[8] = new SqlParameter("@VendorId", objAdmin_Customer.VendorId);
            objSqlParameter[9] = new SqlParameter("@DumpMobileNo", objAdmin_Customer.DumpMobileNo);
            objSqlParameter[10] = new SqlParameter("@Description", objAdmin_Customer.Description);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Vendor", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Send_Employee_To_DB(Admin_Customer objAdmin_Customer)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[11];

            objSqlParameter[0] = new SqlParameter("@FirstName", objAdmin_Customer.FirstName);
            objSqlParameter[1] = new SqlParameter("@Lastname", objAdmin_Customer.Lastname);
            objSqlParameter[2] = new SqlParameter("@MobileNo", objAdmin_Customer.MobileNo);
            objSqlParameter[3] = new SqlParameter("@LocationId", objAdmin_Customer.LocationId);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_Customer.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_Customer.UserId);
            objSqlParameter[6] = new SqlParameter("@EmailId", objAdmin_Customer.EmailId);
            objSqlParameter[7] = new SqlParameter("@CustomerId", objAdmin_Customer.CustomerId);
            objSqlParameter[8] = new SqlParameter("@DumpMobileNo", objAdmin_Customer.DumpMobileNo);
            objSqlParameter[9] = new SqlParameter("@Designation", objAdmin_Customer.Designation);
            objSqlParameter[10] = new SqlParameter("@Address", objAdmin_Customer.DeliveryAddress);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Employee", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
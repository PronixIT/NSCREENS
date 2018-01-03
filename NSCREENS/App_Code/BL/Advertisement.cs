using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Advertisement
/// </summary>
public class Advertisement : Connection
{
    public Advertisement()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _Title;
    private string _Tag;
    private DateTime _StartDate;
    private DateTime _EndDate;
    private string _NoofVisits;
    private decimal _Budget;
    private string _PromoCode;
    private string _Description;
    private string _Video;
    private int _UserId;
    private bool _Isactive;
    private DateTime _CreatedDate;
    private string _ShortFilmId;
    private string _Image;
    private int _AdvertisementId;
    private string _CityId;
    private string _Gender;
    private string _Agefrom;
    private string _Ageto;
    private string _UpComming;

    public string UpComming
    {
        get { return _UpComming; }
        set { _UpComming = value; }
    }

    public string Ageto
    {
        get { return _Ageto; }
        set { _Ageto = value; }
    }

    public string Agefrom
    {
        get { return _Agefrom; }
        set { _Agefrom = value; }
    }

    public string Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }

    public string CityId
    {
        get { return _CityId; }
        set { _CityId = value; }
    }

    public int AdvertisementId
    {
        get { return _AdvertisementId; }
        set { _AdvertisementId = value; }
    }

    public string Image
    {
        get { return _Image; }
        set { _Image = value; }
    }

    public string ShortFilmId
    {
        get { return _ShortFilmId; }
        set { _ShortFilmId = value; }
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

    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    public string Video
    {
        get { return _Video; }
        set { _Video = value; }
    }

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }

    public string PromoCode
    {
        get { return _PromoCode; }
        set { _PromoCode = value; }
    }

    public decimal Budget
    {
        get { return _Budget; }
        set { _Budget = value; }
    }

    public string NoofVisits
    {
        get { return _NoofVisits; }
        set { _NoofVisits = value; }
    }

    public DateTime EndDate
    {
        get { return _EndDate; }
        set { _EndDate = value; }
    }

    public DateTime StartDate
    {
        get { return _StartDate; }
        set { _StartDate = value; }
    }

    public string Tag
    {
        get { return _Tag; }
        set { _Tag = value; }
    }

    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }

    public static DataSet Advertisement_Send_To_DB(Advertisement objAdvertisement)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[19];

            objSqlParameter[0] = new SqlParameter("@Title", objAdvertisement.Title);
            objSqlParameter[1] = new SqlParameter("@Tag", objAdvertisement.Tag);
            objSqlParameter[2] = new SqlParameter("@StartDate", objAdvertisement.StartDate);
            objSqlParameter[3] = new SqlParameter("@EndDate", objAdvertisement.EndDate);
            objSqlParameter[4] = new SqlParameter("@NoofVisits", objAdvertisement.NoofVisits);
            objSqlParameter[5] = new SqlParameter("@Budget", objAdvertisement.Budget);
            objSqlParameter[6] = new SqlParameter("@PromoCode", objAdvertisement.PromoCode);
            objSqlParameter[7] = new SqlParameter("@Description", objAdvertisement.Description);
            objSqlParameter[8] = new SqlParameter("@Video", objAdvertisement.Video);
            objSqlParameter[9] = new SqlParameter("@UserId", objAdvertisement.UserId);
            objSqlParameter[10] = new SqlParameter("@CreatedDate", objAdvertisement.CreatedDate);
            objSqlParameter[11] = new SqlParameter("@ShortFilmId", objAdvertisement.ShortFilmId);
            objSqlParameter[12] = new SqlParameter("@Image", objAdvertisement.Image);
            objSqlParameter[13] = new SqlParameter("@AdvertisementId", objAdvertisement.AdvertisementId);
            objSqlParameter[14] = new SqlParameter("@CityId", objAdvertisement.CityId);
            objSqlParameter[15] = new SqlParameter("@Gender", objAdvertisement.Gender);
            objSqlParameter[16] = new SqlParameter("@Agefrom", objAdvertisement.Agefrom);
            objSqlParameter[17] = new SqlParameter("@Ageto", objAdvertisement.Ageto);
            objSqlParameter[18] = new SqlParameter("@UpComming", objAdvertisement.UpComming);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Advertisement", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
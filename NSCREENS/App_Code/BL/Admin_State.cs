using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Admin_State
/// </summary>
public class Admin_State : Connection
{
    #region Private Variables

    private int _StateId;
    private string _StateName;
    private string _DumpState;
    private int _DumpSateId;

    private int _DistrictId;
    private string _DistrictName;
    private string _DumpDistrict;

    private bool _Isactive;
    private int _UserId;

    private DateTime _CreatedDate;

    private string _DumpCity;
    private int _CityId;
    private string _CityName;
    private int _DumpDistrictId;
    private string _CityCode;

    private string _Route;
    private int _RouteId;
    private string _DumpRoute;
    private int _DumpCityId;

    private int _CoveredAreaId;
    private string _CoveredAreaName;
    private string _DumpCoveredArea;
    private int _DumpCoveredAreaId;
    private string _CountryName;
    private int _CountryId;
    private string _DumpCountryName;
    private int _DumpCountryId;

    private string _ArtistName;
    private int _ArtistId;
    private string _DumpArtistName;

    private string _TitleName;
    private int _TitleId;
    private string _DumpTitleName;

    private string _Languages;
    private string _Tag;

    private string _LanguageName;
    private int _LanguageId;
    private string _DumpLanguageName;

    private string _Description;
    private string _Img;
    private string _Gender;
    private string _Interestarea;

    private string _AId;

    private string _LocationId;
    private string _ContactInformation;

    private string _LanguesIds;

    private string _DumpLanguesIds;
    private string _DumpTag;

    public string DumpTag
    {
        get { return _DumpTag; }
        set { _DumpTag = value; }
    }

    public string DumpLanguesIds
    {
        get { return _DumpLanguesIds; }
        set { _DumpLanguesIds = value; }
    }

    public string LanguesIds
    {
        get { return _LanguesIds; }
        set { _LanguesIds = value; }
    }

    public string ContactInformation
    {
        get { return _ContactInformation; }
        set { _ContactInformation = value; }
    }

    public string LocationId
    {
        get { return _LocationId; }
        set { _LocationId = value; }
    }

    public string AId
    {
        get { return _AId; }
        set { _AId = value; }
    }

    public string Interestarea
    {
        get { return _Interestarea; }
        set { _Interestarea = value; }
    }

    public string Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }

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

    public string DumpLanguageName
    {
        get { return _DumpLanguageName; }
        set { _DumpLanguageName = value; }
    }

    public int LanguageId
    {
        get { return _LanguageId; }
        set { _LanguageId = value; }
    }

    public string LanguageName
    {
        get { return _LanguageName; }
        set { _LanguageName = value; }
    }

    public string Tag
    {
        get { return _Tag; }
        set { _Tag = value; }
    }

    public string Languages
    {
        get { return _Languages; }
        set { _Languages = value; }
    }

    public string DumpTitleName
    {
        get { return _DumpTitleName; }
        set { _DumpTitleName = value; }
    }

    public int TitleId
    {
        get { return _TitleId; }
        set { _TitleId = value; }
    }
    public string TitleName
    {
        get { return _TitleName; }
        set { _TitleName = value; }
    }

    public string DumpArtistName
    {
        get { return _DumpArtistName; }
        set { _DumpArtistName = value; }
    }

    public int ArtistId
    {
        get { return _ArtistId; }
        set { _ArtistId = value; }
    }

    public string ArtistName
    {
        get { return _ArtistName; }
        set { _ArtistName = value; }
    }

    public int DumpCountryId
    {
        get { return _DumpCountryId; }
        set { _DumpCountryId = value; }
    }

    public string DumpCountryName
    {
        get { return _DumpCountryName; }
        set { _DumpCountryName = value; }
    }

    public int CountryId
    {
        get { return _CountryId; }
        set { _CountryId = value; }
    }

    public string CountryName
    {
        get { return _CountryName; }
        set { _CountryName = value; }
    }

    public int CoveredAreaId
    {
        get { return _CoveredAreaId; }
        set { _CoveredAreaId = value; }
    }

    public string CoveredAreaName
    {
        get { return _CoveredAreaName; }
        set { _CoveredAreaName = value; }
    }

    public string DumpCoveredArea
    {
        get { return _DumpCoveredArea; }
        set { _DumpCoveredArea = value; }
    }

    public int DumpCoveredAreaId
    {
        get { return _DumpCoveredAreaId; }
        set { _DumpCoveredAreaId = value; }
    }

    public int DumpCityId
    {
        get { return _DumpCityId; }
        set { _DumpCityId = value; }
    }

    public string DumpRoute
    {
        get { return _DumpRoute; }
        set { _DumpRoute = value; }
    }

    public int RouteId
    {
        get { return _RouteId; }
        set { _RouteId = value; }
    }

    public string Route
    {
        get { return _Route; }
        set { _Route = value; }
    }

    public string CityCode
    {
        get { return _CityCode; }
        set { _CityCode = value; }
    }

    #endregion

    #region Public Properties

    public int DumpDistrictId
    {
        get { return _DumpDistrictId; }
        set { _DumpDistrictId = value; }
    }

    public string CityName
    {
        get { return _CityName; }
        set { _CityName = value; }
    }

    public int CityId
    {
        get { return _CityId; }
        set { _CityId = value; }
    }

    public string DumpCity
    {
        get { return _DumpCity; }
        set { _DumpCity = value; }
    }

    public int DumpSateId
    {
        get { return _DumpSateId; }
        set { _DumpSateId = value; }
    }

    public DateTime CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }

    public int StateId
    {
        get { return _StateId; }
        set { _StateId = value; }
    }

    public string StateName
    {
        get { return _StateName; }
        set { _StateName = value; }
    }

    public string DumpState
    {
        get { return _DumpState; }
        set { _DumpState = value; }
    }


    public int DistrictId
    {
        get { return _DistrictId; }
        set { _DistrictId = value; }
    }

    public string DistrictName
    {
        get { return _DistrictName; }
        set { _DistrictName = value; }
    }

    public string DumpDistrict
    {
        get { return _DumpDistrict; }
        set { _DumpDistrict = value; }
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

    #endregion

    public static DataSet State_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[8];

            objSqlParameter[0] = new SqlParameter("@StateName", objAdmin_State.StateName);
            objSqlParameter[1] = new SqlParameter("@StateId", objAdmin_State.StateId);
            objSqlParameter[2] = new SqlParameter("@DumpState", objAdmin_State.DumpState);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);
            objSqlParameter[6] = new SqlParameter("@CountryId", objAdmin_State.CountryId);
            objSqlParameter[7] = new SqlParameter("@DumpCountryId", objAdmin_State.DumpCountryId);

            objSqlParameter[6] = new SqlParameter("@CountryId", objAdmin_State.CountryId);
            objSqlParameter[7] = new SqlParameter("@DumpCountryId", objAdmin_State.DumpCountryId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_State", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Country_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[6];

            objSqlParameter[0] = new SqlParameter("@CountryName", objAdmin_State.CountryName);
            objSqlParameter[1] = new SqlParameter("@CountryId", objAdmin_State.CountryId);
            objSqlParameter[2] = new SqlParameter("@DumpCountryName", objAdmin_State.DumpCountryName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Country", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Artist_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[6];

            objSqlParameter[0] = new SqlParameter("@ArtistName", objAdmin_State.ArtistName);
            objSqlParameter[1] = new SqlParameter("@ArtistId", objAdmin_State.ArtistId);
            objSqlParameter[2] = new SqlParameter("@DumpArtistName", objAdmin_State.DumpArtistName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Artist", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Artist_Send_To_DB_Details(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[14];

            objSqlParameter[0] = new SqlParameter("@ArtistName", objAdmin_State.ArtistName);
            objSqlParameter[1] = new SqlParameter("@ArtistId", objAdmin_State.ArtistId);
            objSqlParameter[2] = new SqlParameter("@DumpArtistName", objAdmin_State.DumpArtistName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);

            objSqlParameter[6] = new SqlParameter("@Description", objAdmin_State.Description);
            objSqlParameter[7] = new SqlParameter("@Img", objAdmin_State.Img);
            objSqlParameter[8] = new SqlParameter("@Gender", objAdmin_State.Gender);
            objSqlParameter[9] = new SqlParameter("@Interestarea", objAdmin_State.Interestarea);
            objSqlParameter[10] = new SqlParameter("@AId", objAdmin_State.AId);

            objSqlParameter[11] = new SqlParameter("@ContactInformation", objAdmin_State.ContactInformation);
            objSqlParameter[12] = new SqlParameter("@LocationId", objAdmin_State.LocationId);
            objSqlParameter[13] = new SqlParameter("@LanguageId", objAdmin_State.LanguesIds);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Artist_Details", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Title_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[10];

            objSqlParameter[0] = new SqlParameter("@TitleName", objAdmin_State.TitleName);
            objSqlParameter[1] = new SqlParameter("@TitleId", objAdmin_State.TitleId);
            objSqlParameter[2] = new SqlParameter("@DumpTitleName", objAdmin_State.DumpTitleName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);

            objSqlParameter[6] = new SqlParameter("@Tag", objAdmin_State.Tag);
            objSqlParameter[7] = new SqlParameter("@Languages", objAdmin_State.Languages);
            objSqlParameter[8] = new SqlParameter("@DumpLanguesIds", objAdmin_State.DumpLanguesIds);
            objSqlParameter[9] = new SqlParameter("@DumpTag", objAdmin_State.DumpTag);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_New_Register", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet District_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[8];

            objSqlParameter[0] = new SqlParameter("@StateId", objAdmin_State.StateId);
            objSqlParameter[1] = new SqlParameter("@DistrictName", objAdmin_State.DistrictName);
            objSqlParameter[2] = new SqlParameter("@DumpDistrict", objAdmin_State.DumpDistrict);
            objSqlParameter[3] = new SqlParameter("@DistrictId", objAdmin_State.DistrictId);
            objSqlParameter[4] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);
            objSqlParameter[6] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[7] = new SqlParameter("@DumpSateId", objAdmin_State.DumpSateId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_District", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet City_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[8];

            objSqlParameter[0] = new SqlParameter("@DistrictId", objAdmin_State.DistrictId);
            objSqlParameter[1] = new SqlParameter("@CityName", objAdmin_State.CityName);
            objSqlParameter[2] = new SqlParameter("@DumpCity", objAdmin_State.DumpCity);
            objSqlParameter[3] = new SqlParameter("@CityId", objAdmin_State.CityId);
            objSqlParameter[4] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);
            objSqlParameter[6] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[7] = new SqlParameter("@DumpDistrictId", objAdmin_State.DumpDistrictId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_City", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Route_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[8];

            objSqlParameter[0] = new SqlParameter("@CityId", objAdmin_State.CityId);
            objSqlParameter[1] = new SqlParameter("@Route", objAdmin_State.Route);
            objSqlParameter[2] = new SqlParameter("@DumpRoute", objAdmin_State.DumpRoute);
            objSqlParameter[3] = new SqlParameter("@RouteId", objAdmin_State.RouteId);
            objSqlParameter[4] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);
            objSqlParameter[6] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[7] = new SqlParameter("@DumpCityId", objAdmin_State.DumpCityId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Route", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet CoveredArea_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[6];

            objSqlParameter[0] = new SqlParameter("@CoveredAreaName", objAdmin_State.CoveredAreaName);
            objSqlParameter[1] = new SqlParameter("@CoveredAreaId", objAdmin_State.CoveredAreaId);
            objSqlParameter[2] = new SqlParameter("@DumpCoveredArea", objAdmin_State.DumpCoveredArea);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_CoveredArea", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Language_Send_To_DB(Admin_State objAdmin_State)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[6];

            objSqlParameter[0] = new SqlParameter("@LanguageName", objAdmin_State.LanguageName);
            objSqlParameter[1] = new SqlParameter("@LanguageId", objAdmin_State.LanguageId);
            objSqlParameter[2] = new SqlParameter("@DumpLanguageName", objAdmin_State.DumpLanguageName);
            objSqlParameter[3] = new SqlParameter("@Isactive", objAdmin_State.Isactive);
            objSqlParameter[4] = new SqlParameter("@CreatedDate", objAdmin_State.CreatedDate);
            objSqlParameter[5] = new SqlParameter("@UserId", objAdmin_State.UserId);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Admin_Language", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
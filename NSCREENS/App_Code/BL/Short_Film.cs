using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Short_Film
/// </summary>
public class Short_Film:Connection
{
	public Short_Film()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _Title;
    private string _Tag;
    private string _Hero;
    private string _Heroine;
    private string _Technician;
    private string _Category;
    private string _Language;
    private int _Channel;
    private string _Description;
    private string _Video;
    private int _UserId;
    private DateTime _CreatedDate;
    private string _ShortIds;
    private string _Image;
    private int _Short_film_Id;
    private decimal _Duration;
    private string _Trailer;
    private string _Available;

    public string Available
    {
        get { return _Available; }
        set { _Available = value; }
    }

    public string Trailer
    {
        get { return _Trailer; }
        set { _Trailer = value; }
    }

    public decimal Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
    }

    public int Short_film_Id
    {
        get { return _Short_film_Id; }
        set { _Short_film_Id = value; }
    }

    public string Image
    {
        get { return _Image; }
        set { _Image = value; }
    }

    public string ShortIds
    {
        get { return _ShortIds; }
        set { _ShortIds = value; }
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

    public int Channel
    {
        get { return _Channel; }
        set { _Channel = value; }
    }

    public string Language
    {
        get { return _Language; }
        set { _Language = value; }
    }

    public string Category
    {
        get { return _Category; }
        set { _Category = value; }
    }

    public string Technician
    {
        get { return _Technician; }
        set { _Technician = value; }
    }

    public string Heroine
    {
        get { return _Heroine; }
        set { _Heroine = value; }
    }

    public string Hero
    {
        get { return _Hero; }
        set { _Hero = value; }
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

    public static DataSet Short_Film_Send_To_DB(Short_Film objShort_Film,DataTable Artist)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[16];

            objSqlParameter[0] = new SqlParameter("@Title", objShort_Film.Title);
            objSqlParameter[1] = new SqlParameter("@Tag", objShort_Film.Tag);

            objSqlParameter[2] = new SqlParameter("@Image", objShort_Film.Image);
            objSqlParameter[3] = new SqlParameter("@Short_film_Id", objShort_Film.Short_film_Id);
            objSqlParameter[4] = new SqlParameter("@Artist", Artist);

            objSqlParameter[5] = new SqlParameter("@Category", objShort_Film.Category);
            objSqlParameter[6] = new SqlParameter("@Language", objShort_Film.Language);
            objSqlParameter[7] = new SqlParameter("@Description", objShort_Film.Description);

            objSqlParameter[8] = new SqlParameter("@Channel", objShort_Film.Channel);
            objSqlParameter[9] = new SqlParameter("@Video", objShort_Film.Video);
            objSqlParameter[10] = new SqlParameter("@UserId", objShort_Film.UserId);
            objSqlParameter[11] = new SqlParameter("@CreatedDate", objShort_Film.CreatedDate);
            objSqlParameter[12] = new SqlParameter("@ShortIds", objShort_Film.ShortIds);
            objSqlParameter[13] = new SqlParameter("@Duration", objShort_Film.Duration);
            objSqlParameter[14] = new SqlParameter("@Trailer", objShort_Film.Trailer);
            objSqlParameter[15] = new SqlParameter("@Available", objShort_Film.Available);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Short_Film", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public static DataSet Trailer_Send_To_DB(Short_Film objShort_Film, DataTable Artist)
    {
        try
        {
            SqlParameter[] objSqlParameter = new SqlParameter[5];

            objSqlParameter[0] = new SqlParameter("@Short_film_Id", objShort_Film.Short_film_Id);
            objSqlParameter[1] = new SqlParameter("@UserId", objShort_Film.UserId);
            objSqlParameter[2] = new SqlParameter("@CreatedDate", objShort_Film.CreatedDate);
            objSqlParameter[3] = new SqlParameter("@Trailer", objShort_Film.Trailer);
            objSqlParameter[4] = new SqlParameter("@Language", objShort_Film.Language);

            return SqlHelper.ExecuteDataSet(con, CommandType.StoredProcedure, "Sp_Trailer", objSqlParameter);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
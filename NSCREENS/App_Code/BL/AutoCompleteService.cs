using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

/// <summary>
/// Summary description for AutoCompleteService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
[System.Web.Script.Services.ScriptService]
public class AutoCompleteService : System.Web.Services.WebService
{
    [WebMethod]
    public List<string> GetAutoCompleteData(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager
                    .ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand("Select Title,('../Video_Images/'+LS.Short_film_Image) as Short_film_Image,(Select Language_Name from tbl_admin_language AL where AL.Language_Id=LS.Language)as Language from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true' and Title Like @SearchText + '%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["Title"].ToString() + "$" + dr["Short_film_Image"].ToString() + "@" + dr["Language"].ToString());
                }
                return result;
            }
        }
    }



    [WebMethod]
    public List<string> GetArtists(string Artistname)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager
                    .ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand("Select Artist_Details_Id,Name as Title,'../Artist_Photo/'+Photo as Short_film_Image,Artist_Name as Language from tbl_Artist_Details AD join tbl_admin_Artist AA on AA.Artist_Id=AD.Interest_Areas where AD.Isactive='true' and Name Like @SearchText + '%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", Artistname);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //result.Add(dr["Name"].ToString() + "$" + dr["Photo"].ToString() + "@" + dr["Artist_Name"].ToString());
                    result.Add(dr["Title"].ToString() + "$" + dr["Short_film_Image"].ToString() + "@" + dr["Language"].ToString());
                }
                return result;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmHome : System.Web.UI.Page
{
    public void Display_List(ListView lst, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                lst.DataSource = objDataSet;
                lst.DataBind();
            }
            else
            {
                lst.DataSource = "";
                lst.DataBind();
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                String myUrl = Request.RawUrl.ToString();
                var result = Path.GetFileName(myUrl);
                String Folder = Path.GetDirectoryName(myUrl);
                string[] SplitOffer = Folder.Split('\\');
                for (int i = 0; i < SplitOffer.Length; i++)
                    if (i == 1)
                        Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                if (Session["SearchFilm"] != null)
                {
                    this.Master.SearchBox = Session["SearchFilm"].ToString();
                    Display_List(lstRecentVideos, "Select top 12 LS.Short_Film_Id,Title,Tag,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' Order by LS.Lan_Short_film_Id desc");

                    if (Session["SearchFilm"].ToString() == "")
                        Display_List(lstAllVideos, "Select SF.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id  where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' Order by Title");
                    else
                        Display_List(lstAllVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,Tag from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true' and LS.Isactive='True' and Title Like '%" + Session["SearchFilm"].ToString() + "%' Order by Title");

                    recent.Visible = false;

                    Session["SearchFilm"] = null;
                }
                else
                {
                    recent.Visible = true;

                    string LId = Request.QueryString["Id"];
                    string RId = Request.QueryString["RId"];
                    if (LId != null)
                    {
                        Session["LId"] = LId;
                        Display_List(lstRecentVideos, "Select top 12 Lan_Short_film_Id,LS.Short_film_Id,Tag,Title,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and LS.Language=" + LId + " Order by LS.Lan_Short_film_Id desc");
                        Display_List(lstAllVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and LS.Language=" + LId + " Order by Title");
                    }
                    else if (RId != null)
                    {
                        if (Session["LId"] == null)
                        {
                            Display_List(lstRecentVideos, "Select top 12 Lan_Short_film_Id,LS.Short_film_Id,Tag,Title,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and Category Like '%," + RId + ",%' Order by Lan_Short_film_Id desc");
                            Display_List(lstAllVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and Category Like '%," + RId + ",%' Order by Title");
                        }
                        else
                        {
                            Display_List(lstRecentVideos, "Select top 12 Lan_Short_film_Id,LS.Short_film_Id,Tag,Title,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and Category Like '%," + RId + ",%' and LS.Language=" + Session["LId"].ToString() + " Order by Lan_Short_film_Id desc");
                            Display_List(lstAllVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and Category Like '%," + RId + ",%' and LS.Language=" + Session["LId"].ToString() + " Order by Title");
                        }
                    }
                    else
                    {
                        Session["LId"] = null;
                        Display_List(lstRecentVideos, "Select top 12 LS.Short_Film_Id,Title,Tag,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' Order by LS.Lan_Short_film_Id desc");
                        Display_List(lstAllVideos, "Select LS.Short_Film_Id,SF.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id  where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' Order by Title");
                    }
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lstRecentVideos_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (lstAllVideos.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            string LId = Request.QueryString["Id"];
            string RId = Request.QueryString["RId"];
            if (LId != null)
                Display_List(lstAllVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and LS.Language Like '%," + LId + ",%' Order by Title");
            else if (RId != null)
                Display_List(lstAllVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and Category Like '%," + RId + ",%' Order by Title");
            else
                Display_List(lstAllVideos, "Select SF.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id  where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' Order by Title");
        }
        catch (Exception Ex)
        {

        }
    }
}
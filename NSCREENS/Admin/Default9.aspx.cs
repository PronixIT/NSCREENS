using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Default9 : System.Web.UI.Page
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
            Display_List(lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max))+'&shortfilm=' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Description from tbl_Advertisement where Isactive='True' and Status='Approve'");
        }
        catch(Exception Ex)
        {

        }
    }

    protected void play_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            ListViewItem lst = (ListViewItem)lnk.NamingContainer;

            string Short = Request.QueryString["shortfilm"];
            string ShrUserId = Request.QueryString["userId"];
            if (Short == null)
            {
                //ShowNotification("Advertisement", "Please Select Short Film..!", NotificationType.error);
            }
            else
            {
                if (ShrUserId == null)
                    Response.Redirect((lst.FindControl("lblURLPlay") as Label).Text, false);
                else
                    Response.Redirect((lst.FindControl("lblURLPlay") as Label).Text + "&userId=" + ShrUserId, false);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
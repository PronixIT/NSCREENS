using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MyProfile : System.Web.UI.Page
{
    public void DropDownList(DropDownList ddl, string DataValueField, string DataTextField, string Query, string DisplayText)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = objDataSet;
                ddl.DataValueField = DataValueField;
                ddl.DataTextField = DataTextField;
                ddl.DataBind();
                if (DisplayText != "")
                    ddl.Items.Insert(0, new ListItem(" -- " + DisplayText + " -- ", "0"));
            }
            else
            {
                if (ddl.Items.Count > 0)
                    ddl.Items.Clear();
                if (DisplayText != "")
                    ddl.Items.Insert(0, new ListItem(" -- " + DisplayText + " -- ", "0"));
                else
                    ddl.Items.Insert(0, new ListItem(" -- Select -- ", "0"));
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void Display_List(GridView gv, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = objDataSet;
                gv.DataBind();
            }
            else
            {
                Display_List(gvArtist, "select null as Id,null as Artist_Id,''as Artist,'' as Name,''as NameId,'' as Short_Artist_Id");
            }
        }
        catch (Exception Ex)
        {

        }
    }

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

                string UserId = "";
                if (Request.QueryString["ProductionId"] != null)
                {
                    DataSet DataSetUser = MasterCode.RetrieveQuery("select CreatedById from tbl_Admin_Channel where Channel_Id=" + Request.QueryString["ProductionId"]);
                    if (DataSetUser.Tables[0].Rows.Count > 0)
                        UserId = DataSetUser.Tables[0].Rows[0][0].ToString();
                }
                else
                    UserId = Session["UserId"].ToString();

                lblTopName.Text = Session["Name"].ToString();

                DataSet objDataSet = MasterCode.RetrieveQuery("select Channel_Name,Description,'~/ProductionImg/'+Img as Img from tbl_admin_channel where Isactive='true' and CreatedById=" + UserId);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    img.ImageUrl = objDataSet.Tables[0].Rows[0]["Img"].ToString();
                    lblProductionName.Text = objDataSet.Tables[0].Rows[0][0].ToString();
                    lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                }

                if (UserId == Session["UserId"].ToString())
                {
                    sidemenu.Visible = true;
                    lnkDescription.Visible = true;
                    aImage.Visible = true;
                    Display_List(lstRecentVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration,cast('true' as bit) as own,cast('false' as bit) as other,(Select Language_Name from tbl_admin_language TA where TA.Language_Id=LS.Language)as Language_Name,LS.Language from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and LS.CreatedById=" + UserId + " Order by Lan_Short_film_Id desc");
                }
                else
                {
                    sidemenu.Visible = false;
                    lnkDescription.Visible = false;
                    aImage.Visible = false;
                    Display_List(lstRecentVideos, "Select Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration,cast('false' as bit) as own,cast('true' as bit) as other,(Select Language_Name from tbl_admin_language AL where AL.Language_Id=LS.Language)as LanguageName,LS.Language from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id  where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true' and LS.CreatedById=" + UserId + " Order by Short_film_Id desc");
                }

                //Display_List(lstUpcomingVideo, "Select Title_Id,Title_Name,Tag,'../Video_Images/'+Image as Short_film_Image,'frmSingle.aspx?Trailer='+cast(Title_Id as varchar(max)) as VideoTrailer,(Select Language_Name from tbl_admin_language where Language_Id in (Select val from fn_String_To_Table(Languages,',',1)))as Language_Name from tbl_Register_Title where Isactive='True' and CreatedById=" + UserId + " and Title_Name not in (Select Title from tbl_Short_film where Short_film_Id in (Select distinct Short_Film_Id from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) Order by Title_Id desc");
                Display_List(lstUpcomingVideo, "Select Title_Id,Title_Name,Tag,'../Video_Images/'+Image as Short_film_Image,'frmSingle.aspx?Trailer='+cast(Title_Id as varchar(max)) as VideoTrailer,(Select Language_Name from tbl_admin_language where Language_Id in (Select val from fn_String_To_Table(Languages,',',1)))as Language_Name from tbl_Register_Title where Isactive='True' and CreatedById=" + UserId + " and Title_Id not in (Select Title_Id from tbl_Register_Title where Title_Name in (Select Title from tbl_Short_film where Short_film_Id in (Select distinct Short_Film_Id from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) and Languages in (Select ','+Language+',' from tbl_Language_Short_FilmId where Isactive='true' and Publish='true') and Tag in (Select Tags from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) Order by Title_Id desc ");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnShortFilms_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("frmAddShortfilm.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("frmTrailerUpload.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lstRecentVideos_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            lblShortId.Text = (e.Item.FindControl("lblShort_film_Id") as Label).Text;
            lblLangId.Text = (e.Item.FindControl("lblLanguageIDs") as Label).Text;

            DataSet objDataSet = MasterCode.RetrieveQuery("Select Description from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + lblShortId.Text + ")");
            if (objDataSet.Tables[0].Rows.Count > 0)
                txtDescriptionArtist.Text = objDataSet.Tables[0].Rows[0][0].ToString();

            Display_List(gvArtist, "Select Short_Artist_Id,AD.Name as NameId,Artist_Details_Id as Name,Interest_Areas as Artist_Id,(Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=AD.Interest_Areas)as Artist from tbl_Artist_Details AD join tbl_Short_Artist SA on AD.Artist_Details_Id=SA.Artist_Id where Language_Short_Id=" + (e.Item.FindControl("lblShort_film_Id") as Label).Text);

            DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlArtist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList((gvArtist.FooterRow.FindControl("ddlAName") as DropDownList), "Artist_Details_Id", "Name", "SELECT Artist_Details_Id,Name FROM tbl_Artist_Details where Interest_Areas=" + (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).SelectedValue.ToString() + " Order by Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkAddArtist_Click(object sender, EventArgs e)
    {
        try
        {
            //DropDownList(ddlUpdateInterestArea, "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objButton = sender as LinkButton;
            GridViewRow Row = (GridViewRow)objButton.NamingContainer;

            DataSet objDataSet = MasterCode.RetrieveQuery("Delete from tbl_Short_Artist where Short_Artist_Id=" + (Row.FindControl("lblShort_Artist_Id") as Label).Text);
            Display_List(gvArtist, "Select Short_Artist_Id,AD.Name as NameId,Artist_Details_Id as Name,Interest_Areas as Artist_Id,(Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=AD.Interest_Areas)as Artist from tbl_Artist_Details AD join tbl_Short_Artist SA on AD.Artist_Details_Id=SA.Artist_Id where Short_Film_Id=" + lblShortId.Text);
            DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;
            if ((Row.FindControl("ddlArtist") as DropDownList).SelectedIndex != 0)
            {
                string ff = (Row.FindControl("ddlArtist") as DropDownList).SelectedValue.ToString();

                DataSet objDataSet = MasterCode.RetrieveQuery("insert into tbl_Short_Artist(Artist_Id,Short_Film_Id,Language_Short_Id) values(" + (Row.FindControl("ddlAName") as DropDownList).SelectedValue.ToString() + "," + lblShortId.Text + "," + lblShortId.Text + ")");
                Display_List(gvArtist, "Select Short_Artist_Id,AD.Name as NameId,Artist_Details_Id as Name,Interest_Areas as Artist_Id,(Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=AD.Interest_Areas)as Artist from tbl_Artist_Details AD join tbl_Short_Artist SA on AD.Artist_Details_Id=SA.Artist_Id where Short_Film_Id=" + lblShortId.Text);
                DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDescriptionArtist.Text.Trim() != "")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Short_film set Description='" + txtDescriptionArtist.Text.Trim() + "' where Short_film_Id=" + lblShortId.Text);

                string Image1 = fupImage.FileName;
                if (Image1 != "")
                    fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Shortfilm_" + lblShortId.Text + ".jpg");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void Upload(object sender, EventArgs e)
    {
        try
        {
            string Image1 = fup.FileName;
            if (Image1 != "")
                fup.SaveAs(Server.MapPath(img.ImageUrl));
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkDescription_Click(object sender, EventArgs e)
    {
        try
        {
            txtUpdateDescription.Text = lblDescription.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupDescription()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUpdateDescription.Text.Trim() != "")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_admin_channel set Description='" + txtUpdateDescription.Text.Trim() + "' where CreatedById=" + Session["UserId"].ToString());
                lblDescription.Text = txtUpdateDescription.Text.Trim();
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmUploadShortfilm : System.Web.UI.Page
{
    DataTable objDataTable = new DataTable();

    #region Private & Enum Methods

    enum NotificationType
    {
        info,
        success,
        error
    }

    private void ShowNotification(string title, string msg, NotificationType nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

    #endregion

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomerName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Select Title from tbl_Short_film where Title Like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> Title = new List<string>();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                    while (sdr.Read())
                        Title.Add(sdr["Title"].ToString());

                conn.Close();
                return Title;
            }
        }
    }

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

    public void CheckBoxList(ListBox cbl, string DataValueField, string DataTextField, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                cbl.DataSource = objDataSet;
                cbl.DataValueField = DataValueField;
                cbl.DataTextField = DataTextField;
                cbl.DataBind();
            }
            else
            {
                cbl.DataSource = "";
                cbl.DataBind();
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
                gv.DataSource = "";
                gv.DataBind();
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void Clear_Short_Film()
    {
        lblShortFilmId.Text = "";
        txtTitle.Text = "";
        txtTag.Text = "";
        txtDescription.Text = "";
        txtTitle.Text = "";
        lstCategory.ClearSelection();
        ddlProduction.SelectedIndex = 0;
        lstLanguage.ClearSelection();
        lblTitleAvailable.Text = "";

        gvArtist.DataSource = "";
        gvArtist.DataBind();
    }

    public void Send_Artist_Data(string AId, string Artist_Name, int Artist_Id, string DumpArtist_Name, bool Isactive, string Description, string Img, string Gender, string Interestarea)
    {
        try
        {
            if (Artist_Name != "" && Description != "" && Interestarea != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.ArtistName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Artist_Name.ToLower());
                    objAdmin_State.ArtistId = Artist_Id;
                    objAdmin_State.DumpArtistName = DumpArtist_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdmin_State.Description = Description;
                    objAdmin_State.Img = Img;
                    if (Gender == "true")
                        objAdmin_State.Gender = "Male";
                    else
                        objAdmin_State.Gender = "Female";
                    objAdmin_State.Interestarea = Interestarea;
                    objAdmin_State.AId = AId;

                    DataSet objDataSet = Admin_State.Artist_Send_To_DB_Details(objAdmin_State);
                    if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) >= 0)
                    {
                        if (Artist_Id == 0)
                        {
                            if (Img != "")
                                fupupdatePhoto.SaveAs(Server.MapPath("~/Artist_Photo/") + "Ar" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                            ShowNotification("Artist", "Inserted Successfully..", NotificationType.success);
                            txtUpdateArtist.Text = "";
                            txtUpdateDescription.Text = "";
                            ddlUpdateInterestArea.SelectedIndex = 0;
                        }
                        else
                        {
                            ShowNotification("Artist", "Updated Successfully..", NotificationType.success);
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Artist", "Artist Already Existed..!", NotificationType.error);
                        if (Artist_Id == 0)
                            txtUpdateArtist.Focus();
                        else
                            txtUpdateArtist.Focus();
                    }
                    else
                        ShowNotification("Artist", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Artist", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Artist", "Please fill all Fields..!", NotificationType.error);
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
                if (Session["UserId"] != null)
                {
                    String myUrl = Request.RawUrl.ToString();
                    var result = Path.GetFileName(myUrl);
                    String Folder = Path.GetDirectoryName(myUrl);
                    string[] SplitOffer = Folder.Split('\\');
                    for (int i = 0; i < SplitOffer.Length; i++)
                        if (i == 1)
                            Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                    DataSet obj = MasterCode.RetrieveQuery("Select count(*) from tbl_admin_channel where Isactive='True' and CreatedById=" + Session["UserId"].ToString());
                    if (Convert.ToInt32(obj.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");
                        CheckBoxList(lstCategory, "Category_Id", "Category_Name", "select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name");

                        DropDownList(ddlProduction, "Channel_Id", "Channel_Name", "Select Channel_Id,Channel_Name from tbl_admin_channel where Isactive='True' and CreatedById=" + Session["UserId"].ToString() + " Order by Channel_Name", "Select");
                        if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                            Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film SF where Isactive='True' Order by Short_film_Id desc");
                        else
                            Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

                        DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                    }
                    else
                        Response.Redirect("frmProduction.aspx", false);
                }
                else
                    ShowNotification("Trailer", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "handleFileSelect()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtURLTRailer.Text.Trim() != "")
            {
                Short_Film objShort_Film = new Short_Film();

                objShort_Film.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                objShort_Film.UserId = Convert.ToInt32(Session["UserId"].ToString());

                objShort_Film.Short_film_Id = Convert.ToInt32(lblShortFilmId.Text.Trim());

                string Url1 = txtURLTRailer.Text.Trim();
                string[] SplitUrl1 = Url1.Split('/');
                string NewUrl1 = "";
                if (SplitUrl1.Length != 0)
                    for (int i = 0; i < SplitUrl1.Length; i++)
                        if (i == 1)
                            NewUrl1 = NewUrl1 + "//player." + SplitUrl1[i].ToString();
                        else if (i == 3)
                            NewUrl1 = NewUrl1 + "/video/" + SplitUrl1[i].ToString();
                        else
                            NewUrl1 = NewUrl1 + SplitUrl1[i].ToString();

                objShort_Film.Trailer = NewUrl1;

                DataSet objDataSet = Short_Film.Trailer_Send_To_DB(objShort_Film, (DataTable)ViewState["Artist"]);
                if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                {
                    ShowNotification("Trailer", "inserted Successfully..", NotificationType.success);

                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film SF where Isactive='True' Order by Short_film_Id desc");
                    else
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

                    Clear_Short_Film();
                }
            }
            else
            {
                ShowNotification("Trailer", "Please Select Trailer..!", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Short_Film();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void gvShortFilm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                ViewState["Artist"] = null;
                int index = Convert.ToInt32(e.CommandArgument);
                DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Short_film_Id,Title,Tag,Category,Language,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,Visits,ShortIds,Short_film_Image FROM tbl_Short_film where Short_film_Id=" + (gvShortFilm.Rows[index].FindControl("lblGridAdvertisement_Id") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    DataSet objDataSetArtist = MasterCode.RetrieveQuery("Select Short_Artist_Id,Artist_Id as ArtistId,(Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=SA.Artist_Id)as Artist,Name from tbl_Short_Artist SA where Isactive='true' and Short_Film_Id=" + objDataSet.Tables[0].Rows[0]["Short_film_Id"].ToString());
                    if (objDataSetArtist.Tables[0].Rows.Count > 0)
                    {
                        gvArtist.DataSource = objDataSetArtist.Tables[0];
                        gvArtist.DataBind();
                    }
                    else
                    {
                        gvArtist.DataSource = "";
                        gvArtist.DataBind();
                    }

                    lblShortFilmId.Text = objDataSet.Tables[0].Rows[0]["Short_film_Id"].ToString();
                    txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();

                    txtTag.Enabled = false;

                    lstLanguage.ClearSelection();
                    string[] Split = (objDataSet.Tables[0].Rows[0]["Language"].ToString()).Split(',');
                    if (Split.Length > 0)
                        for (int i = 0; i < Split.Length; i++)
                            foreach (ListItem li in lstLanguage.Items)
                                if (li.Value == Split[i])
                                    li.Selected = true;

                    lstLanguage.Enabled = false;

                    txtDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                    txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();

                    lstCategory.ClearSelection();
                    string[] SplitCate = (objDataSet.Tables[0].Rows[0]["Category"].ToString()).Split(',');
                    if (SplitCate.Length > 0)
                        for (int i = 0; i < SplitCate.Length; i++)
                            foreach (ListItem li in lstCategory.Items)
                                if (li.Value == SplitCate[i])
                                    li.Selected = true;

                    lstCategory.Enabled = false;

                    ddlProduction.ClearSelection();
                    if (ddlProduction.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Channel"].ToString()) != null)
                        ddlProduction.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Channel"].ToString()).Selected = true;

                    txtDescription.Focus();
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (fupupdatePhoto.FileName != "")
                Send_Artist_Data(ddlUpdateInterestArea.SelectedValue.ToString(), txtUpdateArtist.Text.Trim(), 0, "", true, txtUpdateDescription.Text.Trim(), fupupdatePhoto.FileName, (rdbUpdateMale.Checked).ToString(), ddlUpdateInterestArea.SelectedValue.ToString());
            else
                ShowNotification("Artist", "Please Select Photo..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void txtTitle_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Short_film_Id,Title,Tag,Category,Language,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,Visits,ShortIds,Short_film_Image FROM tbl_Short_film where Title='" + txtTitle.Text.Trim() + "'");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                DataSet objDataSetArtist = MasterCode.RetrieveQuery("Select Short_Artist_Id,Artist_Id as ArtistId,(Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=SA.Artist_Id)as Artist,Name from tbl_Short_Artist SA where Isactive='true' and Short_Film_Id=" + objDataSet.Tables[0].Rows[0]["Short_film_Id"].ToString());
                if (objDataSetArtist.Tables[0].Rows.Count > 0)
                {
                    gvArtist.DataSource = objDataSetArtist.Tables[0];
                    gvArtist.DataBind();
                }
                else
                {
                    gvArtist.DataSource = "";
                    gvArtist.DataBind();
                }

                lblShortFilmId.Text = objDataSet.Tables[0].Rows[0]["Short_film_Id"].ToString();
                txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();

                txtTag.Enabled = false;

                lstLanguage.ClearSelection();
                string[] Split = (objDataSet.Tables[0].Rows[0]["Language"].ToString()).Split(',');
                if (Split.Length > 0)
                    for (int i = 0; i < Split.Length; i++)
                        foreach (ListItem li in lstLanguage.Items)
                            if (li.Value == Split[i])
                                li.Selected = true;

                lstLanguage.Enabled = false;

                txtDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();

                lstCategory.ClearSelection();
                string[] SplitCate = (objDataSet.Tables[0].Rows[0]["Category"].ToString()).Split(',');
                if (SplitCate.Length > 0)
                    for (int i = 0; i < SplitCate.Length; i++)
                        foreach (ListItem li in lstCategory.Items)
                            if (li.Value == SplitCate[i])
                                li.Selected = true;

                lstCategory.Enabled = false;

                ddlProduction.ClearSelection();
                if (ddlProduction.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Channel"].ToString()) != null)
                    ddlProduction.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Channel"].ToString()).Selected = true;

                txtDescription.Focus();
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
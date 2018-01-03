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

public partial class Admin_frmAddShortfilm : System.Web.UI.Page
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
                cmd.CommandText = "Select distinct Title_Name+case when Tag='' then'' else '_' end+Tag as Title_Name from tbl_Register_Title where Title_Name Like @SearchText + '%'";
                // Title_Name not in (Select Title from tbl_Short_film where Isactive='true') and
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> Title = new List<string>();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                    while (sdr.Read())
                        Title.Add(sdr["Title_Name"].ToString());

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
        txtTitle.Text = "";
        txtTag.Text = "";
        txtDescription.Text = "";
        txtTitle.Text = "";
        lstCategory.ClearSelection();
        ddlProduction.SelectedIndex = 0;
        ddlLanguage.SelectedIndex = 0;
        lblTitleAvailable.Text = "";

        ViewState["Artist"] = null;
        Display_List(gvArtist, "select null as Id,null as ArtistId,''as Artist,'' as Name,''as NameId");

        DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
    }

    public void Send_Artist_Data(string AId, string Artist_Name, int Artist_Id, string DumpArtist_Name, bool Isactive, string Description, string Img, string Gender, string Interestarea, int LocationId, string ContactInformation, string LanguagesIds)
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
                    if (Gender == "True")
                        objAdmin_State.Gender = "Male";
                    else
                        objAdmin_State.Gender = "Female";
                    objAdmin_State.Interestarea = Interestarea;
                    objAdmin_State.AId = AId;

                    objAdmin_State.ContactInformation = ContactInformation;
                    objAdmin_State.LocationId = LocationId.ToString();
                    objAdmin_State.LanguesIds = LanguagesIds;

                    DataSet objDataSet = Admin_State.Artist_Send_To_DB_Details(objAdmin_State);
                    if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) >= 0)
                    {
                        if (Artist_Id == 0)
                        {
                            if (Img != "")
                                fupupdatePhoto.SaveAs(Server.MapPath("~/Artist_Photo/") + "Ar" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");


                            DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

                            (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).ClearSelection();
                            if ((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).Items.FindByValue(ddlUpdateInterestArea.SelectedValue.ToString()) != null)
                                (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).Items.FindByValue(ddlUpdateInterestArea.SelectedValue.ToString()).Selected = true;

                            //DropDownList((gvArtist.FooterRow.FindControl("ddlAName") as DropDownList), "Artist_Details_Id", "Name", "SELECT Artist_Details_Id,Name FROM tbl_Artist_Details where Interest_Areas=" + (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).SelectedValue.ToString() + " Order by Name", "Select");

                            CheckBoxList((gvArtist.FooterRow.FindControl("ddlAName") as ListBox), "Artist_Details_Id", "Name", "SELECT Artist_Details_Id,Name+'$'+'../Artist_Photo/'+Photo as Name FROM tbl_Artist_Details where Interest_Areas=" + (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).SelectedValue.ToString() + " Order by Name");

                            (gvArtist.FooterRow.FindControl("ddlAName") as ListBox).ClearSelection();
                            string sss = (Artist_Name + "$../Artist_Photo/" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");
                            if ((gvArtist.FooterRow.FindControl("ddlAName") as ListBox).Items.FindByText((Artist_Name + "$../Artist_Photo/Ar" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg")) != null)
                                (gvArtist.FooterRow.FindControl("ddlAName") as ListBox).Items.FindByText((Artist_Name + "$../Artist_Photo/Ar" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg")).Selected = true;

                            ShowNotification("Artist", "Inserted Successfully..", NotificationType.success);
                            txtUpdateArtist.Text = "";
                            txtUpdateDescription.Text = "";
                            ddlUpdateInterestArea.SelectedIndex = 0;
                            ddlLocation.SelectedIndex = 0;
                            txtContactInformation.Text = "";
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
                    txtURL.Text = "https://vimeo.com/237181336";

                    String myUrl = Request.RawUrl.ToString();
                    var result = Path.GetFileName(myUrl);
                    String Folder = Path.GetDirectoryName(myUrl);
                    string[] SplitOffer = Folder.Split('\\');
                    for (int i = 0; i < SplitOffer.Length; i++)
                        if (i == 1)
                            Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                    DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' Order by City_Name", "Select");

                    DataSet obj = MasterCode.RetrieveQuery("Select count(*) from tbl_admin_channel where Isactive='True' and CreatedById=" + Session["UserId"].ToString());
                    if (Convert.ToInt32(obj.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        DropDownList(ddlLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name", "Select");

                        CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");
                        CheckBoxList(lstCategory, "Category_Id", "Category_Name", "select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name");

                        //DropDownList(ddlCategory, "Category_Id", "Category_Name", "select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name", "Select");
                        DropDownList(ddlProduction, "Channel_Id", "Channel_Name", "Select Channel_Id,Channel_Name from tbl_admin_channel where Isactive='True' and CreatedById=" + Session["UserId"].ToString() + " Order by Channel_Name", "");
                        if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                            Display_List(gvShortFilm, "SELECT top 10 LS.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,case when LS.Status='Approve' then 'Approved' else 'Unapproved' end as Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(LS.Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film   SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' Order by LS.Short_film_Id desc");
                        else
                            Display_List(gvShortFilm, "SELECT top 10 LS.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,case when LS.Status='Approve' then 'Approved' else 'Unapproved' end as Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(LS.Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film   SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()) + " Order by LS.Short_film_Id desc");

                        Display_List(gvArtist, "select null as Id,null as ArtistId,''as Artist,'' as Name,''as NameId");

                        DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                    }
                    else
                        Response.Redirect("frmProduction.aspx", false);
                }
                else
                    ShowNotification("Short Film", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);

            }
            //if (txtURL.Text.Trim() != "")
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "divPopup();", true);

        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            string CategoryIds = ",";
            for (int i = 0; i < lstCategory.Items.Count; i++)
                if (lstCategory.Items[i].Selected)
                    CategoryIds = CategoryIds + lstCategory.Items[i].Value + ",";

            //string Video = fudUploadVideo.FileName;
            string Image = fupImage.FileName;
            if (txtTitle.Text.Trim() != "" && CategoryIds != "" && txtDescription.Text.Trim() != "" && Image != "" && ddlLanguage.SelectedIndex != 0)
            {
                if (txtURL.Text.Trim() != "" || txtURLTRailer.Text.Trim() != "")
                {
                    Short_Film objShort_Film = new Short_Film();

                    //string FileExt = Path.GetExtension(fudUploadVideo.FileName);
                    string[] Split123 = (txtTitle.Text.Trim()).Split('_');
                    objShort_Film.Title = Split123[0].ToString();
                    objShort_Film.Tag = txtTag.Text.Trim();
                    objShort_Film.Category = CategoryIds;
                    objShort_Film.Language = ddlLanguage.SelectedValue.ToString();
                    objShort_Film.Channel = Convert.ToInt32(ddlProduction.SelectedValue.ToString());

                    string Url = txtURL.Text.Trim();
                    string[] SplitUrl = Url.Split('/');
                    string NewUrl = "";
                    if (SplitUrl.Length != 0)
                        for (int i = 0; i < SplitUrl.Length; i++)
                            if (i == 1)
                                NewUrl = NewUrl + "//player." + SplitUrl[i].ToString();
                            else if (i == 3)
                                NewUrl = NewUrl + "/video/" + SplitUrl[i].ToString();
                            else
                                NewUrl = NewUrl + SplitUrl[i].ToString();

                    objShort_Film.Video = NewUrl;
                    objShort_Film.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objShort_Film.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objShort_Film.Description = txtDescription.Text.Trim();
                    objShort_Film.ShortIds = "";
                    objShort_Film.Image = Image;
                    objShort_Film.Short_film_Id = 0;
                    objShort_Film.Duration = 0;

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
                    objShort_Film.Available = lblAvailable.Text;

                    //lblAvailable.Text

                    DataSet objDataSet = Short_Film.Short_Film_Send_To_DB(objShort_Film, (DataTable)ViewState["Artist"]);
                    if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                    {
                        //if (Video != "")
                        //    fudUploadVideo.SaveAs(Server.MapPath("~/Videos/") + "Shortfilm_" + objDataSet.Tables[0].Rows[0][0].ToString() + FileExt);

                        if (Image != "")
                            fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Shortfilm_" + objDataSet.Tables[0].Rows[0][2].ToString() + ".jpg");

                        if (lblAvailable.Text == "1")
                            if (Image != "")
                                fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Reg" + objDataSet.Tables[1].Rows[0][0].ToString() + ".jpg");

                        ShowNotification("Short Film", "Your film is uploaded successfully and we will inform you shortly once it got approved", NotificationType.success);

                        if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                            Display_List(gvShortFilm, "SELECT top 10 LS.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,case when LS.Status='Approve' then 'Approved' else 'Unapproved' end as Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(LS.Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film   SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' Order by LS.Short_film_Id desc");
                        else
                            Display_List(gvShortFilm, "SELECT top 10 LS.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,case when LS.Status='Approve' then 'Approved' else 'Unapproved' end as Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(LS.Language,',',1))FOR XML PATH (''))as Language FROM tbl_Short_film   SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()) + " Order by LS.Short_film_Id desc");

                        Clear_Short_Film();
                    }
                }
                else
                {
                    ShowNotification("Short Film", "Please Select Video..!", NotificationType.error);
                }
            }
            else
            {
                ShowNotification("Short Film", "Please Fill All Fields..!", NotificationType.error);
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
                string dd = (Row.FindControl("ddlAName") as ListBox).SelectedItem.ToString();
                if ((Row.FindControl("ddlAName") as ListBox).SelectedItem.ToString() != "" && (Row.FindControl("ddlArtist") as DropDownList).SelectedIndex != 0)
                {
                    if (ViewState["Artist"] == null)
                    {
                        objDataTable.Columns.Add("Id", typeof(Int32));
                        objDataTable.Columns.Add("ArtistId", typeof(Int32));
                        objDataTable.Columns.Add("NameId", typeof(String));
                        objDataTable.Columns.Add("Artist", typeof(String));
                        objDataTable.Columns.Add("Name", typeof(String));
                        objDataTable.Columns["Id"].AutoIncrementSeed = 1;
                        objDataTable.Columns["Id"].AutoIncrement = true;
                    }
                    else
                        objDataTable = (DataTable)ViewState["Artist"];

                    DataRow objDataRow = objDataTable.NewRow();

                    string Art = (Row.FindControl("ddlAName") as ListBox).SelectedItem.ToString();

                    string[] Split = Art.Split('$');

                    objDataRow["ArtistId"] = (Row.FindControl("ddlArtist") as DropDownList).SelectedValue;
                    objDataRow["Artist"] = (Row.FindControl("ddlArtist") as DropDownList).SelectedItem;
                    objDataRow["NameId"] = Split[0].ToString();
                    objDataRow["Name"] = (Row.FindControl("ddlAName") as ListBox).SelectedValue;

                    objDataTable.Rows.Add(objDataRow);

                    ViewState["Artist"] = objDataTable;

                    gvArtist.DataSource = (DataTable)ViewState["Artist"];
                    gvArtist.DataBind();

                    DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                }
            }
            else if ((Row.FindControl("txtAName") as TextBox).Text != "")
            {
                if ((Row.FindControl("txtAName") as TextBox).Text != "" && (Row.FindControl("ddlArtist") as DropDownList).SelectedIndex != 0)
                {
                    if (ViewState["Artist"] == null)
                    {
                        objDataTable.Columns.Add("Id", typeof(Int32));
                        objDataTable.Columns.Add("ArtistId", typeof(Int32));
                        objDataTable.Columns.Add("NameId", typeof(String));
                        objDataTable.Columns.Add("Artist", typeof(String));
                        objDataTable.Columns.Add("Name", typeof(String));
                        objDataTable.Columns["Id"].AutoIncrementSeed = 1;
                        objDataTable.Columns["Id"].AutoIncrement = true;
                    }
                    else
                        objDataTable = (DataTable)ViewState["Artist"];

                    DataRow objDataRow = objDataTable.NewRow();

                    string Art = (Row.FindControl("ddlAName") as ListBox).SelectedItem.ToString();

                    string[] Split = Art.Split('$');

                    objDataRow["ArtistId"] = (Row.FindControl("ddlArtist") as DropDownList).SelectedValue;
                    objDataRow["Artist"] = (Row.FindControl("ddlArtist") as DropDownList).SelectedItem;
                    objDataRow["NameId"] = Split[0].ToString();
                    objDataRow["Name"] = (Row.FindControl("ddlAName") as ListBox).SelectedValue;

                    objDataTable.Rows.Add(objDataRow);

                    ViewState["Artist"] = objDataTable;

                    gvArtist.DataSource = (DataTable)ViewState["Artist"];
                    gvArtist.DataBind();

                    DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                }
            }
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

            string ArtistId = (Row.FindControl("lblId") as Label).Text;

            DataTable objDataTable = new DataTable();
            objDataTable = (DataTable)ViewState["Artist"];
            if (objDataTable != null)
            {
                DataRow[] drr = objDataTable.Select("Id='" + (Row.FindControl("lblId") as Label).Text + "'");

                for (int i = 0; i < drr.Length; i++)
                    objDataTable.Rows.Remove(drr[i]);

                objDataTable.AcceptChanges();
                ViewState["Artist"] = objDataTable;
                if (objDataTable.Rows.Count > 0)
                {
                    gvArtist.DataSource = (DataTable)ViewState["Artist"];
                    gvArtist.DataBind();
                }
                else
                {
                    Display_List(gvArtist, "select null as Id,null as ArtistId,''as Artist,'' as Name,''as NameId");
                    DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void txtTitle_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtTitle.Text.Trim() != "" && ddlLanguage.SelectedIndex != 0)
            {
                string[] Split123 = (txtTitle.Text.Trim()).Split('_');
                //DataSet objDataSet = MasterCode.RetrieveQuery("Select COUNT(*) from tbl_Short_film where Title='" + txtTitle.Text.Trim() + "'");
                DataSet objDataSet = MasterCode.RetrieveQuery("Select Count(*) from tbl_Language_Short_FilmId where language=" + ddlLanguage.SelectedValue.ToString() + " and Short_Film_Id in (Select Short_film_Id from tbl_Short_film where Title='" + Split123[0] + "')");
                DataSet objDataSet1 = MasterCode.RetrieveQuery("select COUNT(*) from tbl_Register_Title where Isactive='True' and Title_Name='" + Split123[0] + "' and Languages Like '%," + ddlLanguage.SelectedValue + ",%'");
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) == 0 && Convert.ToInt32(objDataSet1.Tables[0].Rows[0][0].ToString()) == 0)
                {
                    DataSet objDataSet122 = MasterCode.RetrieveQuery("select Title_Name,Tag,Languages from tbl_Register_Title where Title_Name='" + Split123[0] + "' and Languages Like '%," + ddlLanguage.SelectedValue + ",%' and createdById=" + Session["UserId"].ToString());
                    if (objDataSet122.Tables[0].Rows.Count > 0)
                    {
                        txtTag.Text = objDataSet122.Tables[0].Rows[0]["Tag"].ToString();
                        lblRegId.Text = objDataSet122.Tables[0].Rows[0]["Trailer_Id"].ToString();
                    }

                    txtTag.Enabled = true;
                    ddlLanguage.Enabled = true;

                    lblTitleAvailable.Text = "Available";
                    lblAvailable.Text = "1";
                    lblTitleAvailable.CssClass = "label label-success Status_Success";
                }
                else
                {
                    #region Check
                    DataSet objDataSet12 = MasterCode.RetrieveQuery("select COUNT(*) from tbl_Register_Title where Isactive='True' and Title_Name='" + Split123[0] + "' and Languages Like '%," + ddlLanguage.SelectedValue + ",%' and createdById=" + Session["UserId"].ToString());
                    if (Convert.ToInt32(objDataSet12.Tables[0].Rows[0][0].ToString()) >= 1)
                    {
                        if (Split123.Length == 2)
                        {
                            DataSet objDataSet59 = MasterCode.RetrieveQuery("Select Count(*) from tbl_Language_Short_FilmId where language=" + ddlLanguage.SelectedValue.ToString() + " and Short_Film_Id in (Select Short_film_Id from tbl_Short_film where Title='" + Split123[0] + "') and Tags='" + Split123[1].ToString() + "'");
                            if (Convert.ToInt32(objDataSet59.Tables[0].Rows[0][0].ToString()) == 0)
                            {
                                DataSet objDataSet122 = MasterCode.RetrieveQuery("select Title_Id,Title_Name,Tag,Languages from tbl_Register_Title where Title_Name='" + Split123[0] + "' and Languages Like '%," + ddlLanguage.SelectedValue + ",%' and createdById=" + Session["UserId"].ToString() + " and Tag='" + Split123[1].ToString() + "'");
                                if (objDataSet122.Tables[0].Rows.Count > 0)
                                {
                                    txtTag.Text = objDataSet122.Tables[0].Rows[0]["Tag"].ToString();
                                    lblRegId.Text = objDataSet122.Tables[0].Rows[0]["Title_Id"].ToString();
                                }
                                lblTitleAvailable.Text = "Available";
                                lblAvailable.Text = "1";
                                lblTitleAvailable.CssClass = "label label-success Status_Success";
                            }
                            else
                            {
                                txtTag.Text = "";
                                txtTitle.Text = "";
                                lblTitleAvailable.Text = "Unavailable";
                                ddlLanguage.SelectedIndex = 0;
                                lblAvailable.Text = "0";
                                lblRegId.Text = "0";
                                lblTitleAvailable.CssClass = "label label-danger Status_Warning";
                            }
                        }
                        else
                        {
                            DataSet objDataSet59 = MasterCode.RetrieveQuery("Select Count(*) from tbl_Language_Short_FilmId where language=" + ddlLanguage.SelectedValue.ToString() + " and Short_Film_Id in (Select Short_film_Id from tbl_Short_film where Title='" + Split123[0] + "') and Tags=''");
                            if (Convert.ToInt32(objDataSet59.Tables[0].Rows[0][0].ToString()) == 0)
                            {
                                DataSet objDataSet122 = MasterCode.RetrieveQuery("select Title_Id,Title_Name,Tag,Languages from tbl_Register_Title where Title_Name='" + Split123[0] + "' and Languages Like '%," + ddlLanguage.SelectedValue + ",%' and createdById=" + Session["UserId"].ToString() + "");
                                if (objDataSet122.Tables[0].Rows.Count > 0)
                                {
                                    txtTag.Text = objDataSet122.Tables[0].Rows[0]["Tag"].ToString();
                                    lblRegId.Text = objDataSet122.Tables[0].Rows[0]["Title_Id"].ToString();
                                }
                                lblTitleAvailable.Text = "Available";
                                lblAvailable.Text = "1";
                                lblTitleAvailable.CssClass = "label label-success Status_Success";
                            }
                            else
                            {
                                txtTag.Text = "";
                                txtTitle.Text = "";
                                lblTitleAvailable.Text = "Unavailable";
                                ddlLanguage.SelectedIndex = 0;
                                lblAvailable.Text = "0";
                                lblRegId.Text = "0";
                                lblTitleAvailable.CssClass = "label label-danger Status_Warning";
                            }
                        }
                    }
                    else
                    {
                        txtTag.Text = "";
                        txtTitle.Text = "";
                        lblTitleAvailable.Text = "Unavailable";
                        ddlLanguage.SelectedIndex = 0;
                        lblAvailable.Text = "0";
                        lblRegId.Text = "0";
                        lblTitleAvailable.CssClass = "label label-danger Status_Warning";
                    }
                    #endregion
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlArtist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //DropDownList((gvArtist.FooterRow.FindControl("ddlAName") as DropDownList), "Artist_Details_Id", "Name", "SELECT Artist_Details_Id,Name FROM tbl_Artist_Details where Interest_Areas=" + (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).SelectedValue.ToString() + " Order by Name", "Select");
            CheckBoxList((gvArtist.FooterRow.FindControl("ddlAName") as ListBox), "Artist_Details_Id", "Name", "SELECT Artist_Details_Id,Name+'$'+'../Artist_Photo/'+Photo as Name FROM tbl_Artist_Details where Interest_Areas=" + (gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList).SelectedValue.ToString() + " Order by Name");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkAddArtist_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlUpdateInterestArea, "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string LanguesIds = ",";
            for (int i = 0; i < lstLanguage.Items.Count; i++)
                if (lstLanguage.Items[i].Selected)
                    LanguesIds = LanguesIds + lstLanguage.Items[i].Value + ",";

            if (fupupdatePhoto.FileName != "" && LanguesIds != "")
                Send_Artist_Data(ddlUpdateInterestArea.SelectedValue.ToString(), txtUpdateArtist.Text.Trim(), 0, "", true, txtUpdateDescription.Text.Trim(), fupupdatePhoto.FileName, (rdbUpdateMale.Checked).ToString(), ddlUpdateInterestArea.SelectedValue.ToString(), Convert.ToInt32(ddlLocation.SelectedValue.ToString()), txtContactInformation.Text.Trim(), LanguesIds);
            else
                ShowNotification("Artist", "Please Select Photo..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }
}
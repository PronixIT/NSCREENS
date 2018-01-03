using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

public partial class Admin_frmAddAdvertisement : System.Web.UI.Page
{
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

    public void Clear_Advertisement()
    {
        CheckBoxList(ddlSearchTitle, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image as Title from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true' and LS.Isactive='True' Order by Title");

        txtTitle.Text = "";
        txtTag.Text = "";
        ddlDay.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlEndDay.SelectedIndex = 0;
        ddlEndMonth.SelectedIndex = 0;
        ddlEndYear.SelectedIndex = 0;
        txtNoofVisits.Text = "";
        txtBudget.Text = "";
        txtPromoCode.Text = "";
        txtDescription.Text = "";

        ddlSearchTitle.ClearSelection();
        lblAddSht.Text = "";

        lstCity.ClearSelection();

        rdbMale.Checked = true;

        ddlAgefrom.SelectedIndex = 0;
        ddlAgeTo.SelectedIndex = 0;

        gvSearchFilm.DataSource = "";
        gvSearchFilm.DataBind();

        gdvUpComming.DataSource = "";
        gdvUpComming.DataBind();

        lblUploadImage.Text = "";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtURL.Text = "https://vimeo.com/237181336";

                String myUrl = Request.RawUrl.ToString();
                var result = Path.GetFileName(myUrl);
                String Folder = Path.GetDirectoryName(myUrl);
                string[] SplitOffer = Folder.Split('\\');
                for (int i = 0; i < SplitOffer.Length; i++)
                    if (i == 1)
                        Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                txtPromoCode.Text = this.Master.PromoCode;

                int Years = DateTime.Now.AddHours(Connection.SetHours).Year;

                for (int i = 0; i < 40; i++)
                {
                    ddlYear.Items.Insert(i + 1, new ListItem((Years + i).ToString(), (i + 1).ToString()));
                    ddlEndYear.Items.Insert(i + 1, new ListItem((Years + i).ToString(), (i + 1).ToString()));
                }

                //if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                //DropDownList(ddlSearchTitle, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image as Title from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true' and LS.Isactive='True' Order by Title", "Select");
                CheckBoxList(ddlSearchTitle, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image+'$'+L.Language_Name as Title from tbl_Short_film SF Join tbl_admin_language L on L.Language_Id=SF.Language  join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true'  and LS.Isactive='True' Order by Title");
                CheckBoxList(lstUpcomming, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image+'$'+L.Language_Name as Title from tbl_Short_film SF Join tbl_admin_language L on L.Language_Id=SF.Language  join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='false'  and LS.Isactive='True' Order by Title");
                //CheckBoxList(lstUpcomming, "Title_Id", "Title", "Select Title_Id,Title_Name+'$'+'../Video_Images/'+Image+'$'+(Select Language_Name from tbl_admin_language where Language_Id in (Select val from fn_String_To_Table(Languages,',',1)))as Title from tbl_Register_Title where Isactive='True' and Title_Name not in (Select Title from tbl_Short_film where Short_film_Id in (Select distinct Short_Film_Id from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) Order by Title_Name");
                //else
                //    DropDownList(ddlSearchTitle, "Short_film_Id", "Title", "Select Short_film_Id,Title from tbl_Short_film where Status='Approve' and Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()) + " Order by Title", "Select");


                string CountryIds = "";
                for (int i = 0; i < lstCountry.Items.Count; i++)
                    if (lstCountry.Items[i].Selected)
                        CountryIds = CountryIds + lstCountry.Items[i].Value + ",";

                string StateIds = "";
                for (int i = 0; i < lstState.Items.Count; i++)
                    if (lstState.Items[i].Selected)
                        StateIds = StateIds + lstState.Items[i].Value + ",";

                string DistrictIds = "";
                for (int i = 0; i < lstDistrict.Items.Count; i++)
                    if (lstDistrict.Items[i].Selected)
                        DistrictIds = DistrictIds + lstDistrict.Items[i].Value + ",";

                CheckBoxList(lstCountry, "Country_Id", "Country_Name", "Select Country_Id,Country_Name from tbl_admin_country where Isactive='true' order by Country_Name");
                CheckBoxList(lstState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' and Country_Id=" + CountryIds + " Order by State_Name");
                CheckBoxList(lstDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + StateIds + " Order by District_Name");
                CheckBoxList(lstCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + DistrictIds + " order by City_Name");

                if (Session["UserId"] != null)
                {
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,case when Status='Approve' then 'Approved' else 'Unapproved' end as Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True'");
                    else
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,case when Status='Approve' then 'Approved' else 'Unapproved' end as Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
                }
                else
                    ShowNotification("Advertisement", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            //if (txtURL.Text.Trim() != "")
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "divPopup();", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            //string Video = fudUploadVideo.FileName;
            string Image = fupImage.FileName;

            string CityIdIds = ",";
            for (int i = 0; i < lstCity.Items.Count; i++)
                if (lstCity.Items[i].Selected)
                    CityIdIds = CityIdIds + lstCity.Items[i].Value + ",";

            if (txtTitle.Text.Trim() != "" && txtTag.Text.Trim() != "" && ((ddlDay.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && ddlYear.SelectedIndex != 0 && ddlEndDay.SelectedIndex != 0 && ddlEndMonth.SelectedIndex != 0 && ddlEndYear.SelectedIndex != 0) || rdbViews.Checked) && txtNoofVisits.Text.Trim() != "" && txtBudget.Text.Trim() != "" && txtDescription.Text.Trim() != "" && Image != "" && ddlAgeTo.SelectedIndex != 0)
            {
                Advertisement objAdvertisement = new Advertisement();

                //string FileExt = Path.GetExtension(fudUploadVideo.FileName);

                string ShortId = ",";
                for (int i = 0; i < gvSearchFilm.Rows.Count; i++)
                    ShortId = ShortId + (gvSearchFilm.Rows[i].FindControl("lblGridAdvertisement_Id") as Label).Text + ",";
                //if ((gvSearchFilm.Rows[i].FindControl("rdbSelect") as RadioButton).Checked == true)

                //string UpId = ",";
                for (int i = 0; i < gdvUpComming.Rows.Count; i++)
                    ShortId = ShortId + (gdvUpComming.Rows[i].FindControl("lblGridAdvertisement_Id") as Label).Text + ",";


                objAdvertisement.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtTitle.Text.Trim());
                objAdvertisement.Tag = txtTag.Text.Trim();
                if (rdbViews.Checked)
                {
                    objAdvertisement.StartDate = Convert.ToDateTime((DateTime.Now.AddHours(Connection.SetHours).ToShortDateString()));
                    objAdvertisement.EndDate = Convert.ToDateTime((DateTime.Now.AddYears(20).ToShortDateString()));
                }
                else
                {
                    objAdvertisement.StartDate = Convert.ToDateTime(ddlMonth.SelectedValue.ToString() + "/" + ddlDay.SelectedValue.ToString() + "/" + ddlYear.SelectedItem.ToString());
                    objAdvertisement.EndDate = Convert.ToDateTime(ddlEndMonth.SelectedValue.ToString() + "/" + ddlEndDay.SelectedValue.ToString() + "/" + ddlEndYear.SelectedItem.ToString());
                }
                objAdvertisement.NoofVisits = txtNoofVisits.Text.Trim();
                objAdvertisement.Budget = Convert.ToDecimal(txtBudget.Text.Trim());
                objAdvertisement.PromoCode = txtPromoCode.Text.Trim();
                objAdvertisement.Description = txtDescription.Text.Trim();

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

                objAdvertisement.Video = NewUrl;
                objAdvertisement.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objAdvertisement.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                objAdvertisement.ShortFilmId = ShortId;
                objAdvertisement.Image = Image;
                objAdvertisement.CityId = CityIdIds;

                string Gender = "";
                if (rdbAll.Checked)
                    Gender = "ALL";
                else if (rdbMale.Checked)
                    Gender = "Male";
                else
                    Gender = "Female";

                objAdvertisement.Gender = Gender;
                objAdvertisement.Agefrom = ddlAgefrom.SelectedItem.ToString();
                objAdvertisement.Ageto = ddlAgeTo.SelectedItem.ToString();



                objAdvertisement.UpComming = "";//UpId;

                DataSet objDataSet = Advertisement.Advertisement_Send_To_DB(objAdvertisement);
                if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                {
                    if (Image != "")
                        fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Advatizments_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    ShowNotification("Advertisement", "Your advertisement is uploaded successfully and we will inform you shortly once it got approved", NotificationType.success);

                    Clear_Advertisement();
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,case when Status='Approve' then 'Approved' else 'Unapproved' end as Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True'");
                    else
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,case when Status='Approve' then 'Approved' else 'Unapproved' end as Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
                }
                else
                    ShowNotification("Advertisement", "not inserted..", NotificationType.error);
            }
            else
            {
                ShowNotification("Advertisement", "Please Fill All Fields..!", NotificationType.error);
            }



            //if (!string.IsNullOrEmpty(file.ThumbnailLink))
            //{
            //    rowThumbnail.Visible = true;
            //    imgThumbnail.ImageUrl = file.ThumbnailLink;
            //}
        }
        catch (Exception Ex)
        {
            ShowNotification("Advertisement", "Invalid Date..", NotificationType.error);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Advertisement();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void gvadvertisement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        try
        {
            //string AddColums = "";
            //if (ddlSearchTitle.SelectedIndex != 0)
            //    AddColums = AddColums + " Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + ddlSearchTitle.SelectedValue.ToString() + ") and ";
            //if (ddlSearchCategory.SelectedIndex != 0)
            //    AddColums = AddColums + " Category Like '%" + ddlSearchCategory.SelectedValue.ToString() + "%' and ";
            //if (ddlSearchChannel.SelectedIndex != 0)
            //    AddColums = AddColums + " Channel=" + ddlSearchChannel.SelectedValue.ToString() + " and ";

            //if (AddColums != "")
            //{
            //    AddColums = AddColums.Remove(AddColums.Length - 4, 4);

            //    //if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
            //    Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,(Select Language_Name from tbl_admin_Language L where L.Language_Id=LS.Language) as Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where Isactive='True' and " + AddColums);
            //    //else
            //    //Display_List(gvSearchFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()) + " and " + AddColums);
            //}

            if (lblAddSht.Text != "")
                lblAddSht.Text = lblAddSht.Text + ",";

            lblAddSht.Text = lblAddSht.Text + ddlSearchTitle.SelectedValue.ToString() + ",";
            if (lblAddSht.Text != "")
            {
                lblAddSht.Text = lblAddSht.Text.Remove(lblAddSht.Text.Length - 1, 1);

                Display_List(gvSearchFilm, "SELECT Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,Category,(Select Language_Name from tbl_admin_Language L where L.Language_Id=LS.Language) as Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and Lan_Short_film_Id in (" + lblAddSht.Text + ")");
                CheckBoxList(ddlSearchTitle, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image+'$'+L.Language_Name as Title from tbl_Short_film SF Join tbl_admin_language L on L.Language_Id=SF.Language  join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true'  and LS.Isactive='True' and Lan_Short_film_Id not in (" + lblAddSht.Text + ") Order by Title");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnUpcomming_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblDumpUpComming.Text != "")
                lblDumpUpComming.Text = lblDumpUpComming.Text + ",";

            lblDumpUpComming.Text = lblDumpUpComming.Text + lstUpcomming.SelectedValue.ToString() + ",";
            if (lblDumpUpComming.Text != "")
            {
                lblDumpUpComming.Text = lblDumpUpComming.Text.Remove(lblDumpUpComming.Text.Length - 1, 1);
                //Display_List(gdvUpComming, "Select Title_Id,Title_Name,Tag,(Select Language_Name from tbl_admin_language where Language_Id in (Select val from fn_String_To_Table(Languages,',',1)))as Language_Name from tbl_Register_Title T where Isactive='true' and Title_Id in (" + lblDumpUpComming.Text + ")");
                Display_List(gdvUpComming, "SELECT Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,Category,(Select Language_Name from tbl_admin_Language L where L.Language_Id=LS.Language) as Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and Lan_Short_film_Id in (" + lblDumpUpComming.Text + ")");
                CheckBoxList(lstUpcomming, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image+'$'+L.Language_Name as Title from tbl_Short_film SF Join tbl_admin_language L on L.Language_Id=SF.Language  join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='false'  and LS.Isactive='True' and Lan_Short_film_Id not in (" + lblDumpUpComming.Text + ") Order by Title");
                //CheckBoxList(lstUpcomming, "Title_Id", "Title", "Select Title_Id,Title_Name+'$'+'../Video_Images/'+Image as Title from tbl_Register_Title where Isactive='True' and Title_Name not in (Select Title from tbl_Short_film where Short_film_Id in (Select distinct Short_Film_Id from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) and Title_Id not in (" + lblDumpUpComming.Text + ") Order by Title_Name");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void txtNoofVisits_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtNoofVisits.Text.Trim() != "")
                txtBudget.Text = (Convert.ToInt32(txtNoofVisits.Text.Trim()) * 2).ToString();
            else
                txtBudget.Text = "";

            txtNoofVisits.Focus();

        }
        catch (Exception Ex)
        {

        }
    }

    protected void txtBudget_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBudget.Text.Trim() != "")
                txtNoofVisits.Text = (Convert.ToInt32(txtBudget.Text.Trim()) / 2).ToString();
            else
                txtNoofVisits.Text = "";

            txtNoofVisits.Focus();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CountryIds = "";
            for (int i = 0; i < lstCountry.Items.Count; i++)
                if (lstCountry.Items[i].Selected)
                    CountryIds = CountryIds + lstCountry.Items[i].Value + ",";

            string StateIds = "";
            for (int i = 0; i < lstState.Items.Count; i++)
                if (lstState.Items[i].Selected)
                    StateIds = StateIds + lstState.Items[i].Value + ",";

            string DistrictIds = "";
            for (int i = 0; i < lstDistrict.Items.Count; i++)
                if (lstDistrict.Items[i].Selected)
                    DistrictIds = DistrictIds + lstDistrict.Items[i].Value + ",";

            if (CountryIds != "")
            {
                CountryIds = CountryIds.Remove(CountryIds.Length - 1, 1);
                CheckBoxList(lstState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' and Country_Id in (" + CountryIds + ") Order by State_Name");
            }

            if (StateIds != "")
            {
                StateIds = StateIds.Remove(StateIds.Length - 1, 1);
                CheckBoxList(lstDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id in (" + StateIds + ") Order by District_Name");
            }

            if (DistrictIds != "")
            {
                DistrictIds = DistrictIds.Remove(DistrictIds.Length - 1, 1);
                CheckBoxList(lstCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id in (" + DistrictIds + ") order by City_Name");
            }




            //DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' and Country_Id=" + ddlCountry.SelectedValue.ToString() + " Order by State_Name", "Select");
            //DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");

            //DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlDistrict.SelectedValue.ToString() + " order by City_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string DistrictIds = "";
            for (int i = 0; i < lstDistrict.Items.Count; i++)
                if (lstDistrict.Items[i].Selected)
                    DistrictIds = DistrictIds + lstDistrict.Items[i].Value + ",";

            if (DistrictIds != "")
            {
                DistrictIds = DistrictIds.Remove(DistrictIds.Length - 1, 1);
                CheckBoxList(lstCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id in (" + DistrictIds + ") order by City_Name");
            }

            //DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' order by City_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string StateIds = "";
            for (int i = 0; i < lstState.Items.Count; i++)
                if (lstState.Items[i].Selected)
                    StateIds = StateIds + lstState.Items[i].Value + ",";

            string DistrictIds = "";
            for (int i = 0; i < lstDistrict.Items.Count; i++)
                if (lstDistrict.Items[i].Selected)
                    DistrictIds = DistrictIds + lstDistrict.Items[i].Value + ",";

            if (StateIds != "")
            {
                StateIds = StateIds.Remove(StateIds.Length - 1, 1);
                CheckBoxList(lstDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id in (" + StateIds + ") Order by District_Name");
            }

            if (DistrictIds != "")
            {
                DistrictIds = DistrictIds.Remove(DistrictIds.Length - 1, 1);
                CheckBoxList(lstCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id in (" + DistrictIds + ") order by City_Name");
            }

            //DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
            //DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlDistrict.SelectedValue.ToString() + " order by City_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void rdbDates_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbViews.Checked)
            {
                ddlDay.Enabled = false;
                ddlMonth.Enabled = false;
                ddlYear.Enabled = false;
                ddlEndDay.Enabled = false;
                ddlEndMonth.Enabled = false;
                ddlEndYear.Enabled = false;
            }
            else
            {
                ddlDay.Enabled = true;
                ddlMonth.Enabled = true;
                ddlYear.Enabled = true;
                ddlEndDay.Enabled = true;
                ddlEndMonth.Enabled = true;
                ddlEndYear.Enabled = true;
            }

            if (txtURL.Text.Trim() != "")
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "divPopup();", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;

            string[] Split = (lblAddSht.Text).Split(',');
            lblAddSht.Text = "";
            if (Split.Length > 0)
                for (int i = 0; i < Split.Length; i++)
                    if (Split[i] != lnk.CommandName)
                        lblAddSht.Text = lblAddSht.Text + Split[i] + ",";

            if (lblAddSht.Text != "")
            {
                lblAddSht.Text = lblAddSht.Text.Remove(lblAddSht.Text.Length - 1, 1);
                Display_List(gvSearchFilm, "SELECT Lan_Short_film_Id,LS.Short_film_Id,Title,Tag,Category,(Select Language_Name from tbl_admin_Language L where L.Language_Id=LS.Language) as Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and Lan_Short_film_Id in (" + lblAddSht.Text + ")");
                CheckBoxList(ddlSearchTitle, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image as Title from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true' and LS.Isactive='True' and Lan_Short_film_Id not in (" + lblAddSht.Text + ") Order by Title");
            }
            else
            {
                lblAddSht.Text = "";
                gvSearchFilm.DataSource = "";
                gvSearchFilm.DataBind();
                CheckBoxList(ddlSearchTitle, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image as Title from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='true' and LS.Isactive='True' Order by Title");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkDeleteUp_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;

            string[] Split = (lblDumpUpComming.Text).Split(',');
            lblDumpUpComming.Text = "";
            if (Split.Length > 0)
                for (int i = 0; i < Split.Length; i++)
                    if (Split[i] != lnk.CommandName)
                        lblDumpUpComming.Text = lblDumpUpComming.Text + Split[i] + ",";

            if (lblDumpUpComming.Text != "")
            {
                lblDumpUpComming.Text = lblDumpUpComming.Text.Remove(lblDumpUpComming.Text.Length - 1, 1);
                Display_List(gdvUpComming, "Select Title_Id,Title_Name,Tag,(Select Language_Name from tbl_admin_language where Language_Id in (Select val from fn_String_To_Table(Languages,',',1)))as Language_Name from tbl_Register_Title T where Isactive='true' and Title_Id in (" + lblDumpUpComming.Text + ")");
                CheckBoxList(lstUpcomming, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image+'$'+L.Language_Name as Title from tbl_Short_film SF Join tbl_admin_language L on L.Language_Id=SF.Language  join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='false'  and LS.Isactive='True' and Lan_Short_film_Id not in (" + lblDumpUpComming.Text + ") Order by Title");
                //CheckBoxList(lstUpcomming, "Title_Id", "Title", "Select Title_Id,Title_Name+'$'+'../Video_Images/'+Image as Title from tbl_Register_Title where Isactive='True' and Title_Name not in (Select Title from tbl_Short_film where Short_film_Id in (Select distinct Short_Film_Id from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) and Title_Id not in (" + lblDumpUpComming.Text + ") Order by Title_Name");
            }
            else
            {
                lblDumpUpComming.Text = "";
                gdvUpComming.DataSource = "";
                gdvUpComming.DataBind();
                CheckBoxList(lstUpcomming, "Lan_Short_film_Id", "Title", "Select Lan_Short_film_Id,LS.Short_film_Id,Title+'$'+'../Video_Images/'+LS.Short_film_Image+'$'+L.Language_Name as Title from tbl_Short_film SF Join tbl_admin_language L on L.Language_Id=SF.Language  join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Status='Approve' and LS.Publish='false'  and LS.Isactive='True' Order by Title");
                //CheckBoxList(lstUpcomming, "Title_Id", "Title", "Select Title_Id,Title_Name+'$'+'../Video_Images/'+Image as Title from tbl_Register_Title where Isactive='True' and Title_Name not in (Select Title from tbl_Short_film where Short_film_Id in (Select distinct Short_Film_Id from tbl_Language_Short_FilmId where Isactive='true' and Publish='true')) and Title_Id not in (" + lblDumpUpComming.Text + ") Order by Title_Name");
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
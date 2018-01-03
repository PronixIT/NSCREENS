using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmHome : System.Web.UI.Page
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


    public void Send_Artist_Data(string AId, string Artist_Name, int Artist_Id, string DumpArtist_Name, bool Isactive, string Description, string Img, string Gender, string Interestarea, int LocationId, string ContactInformation, string LanguesIds)
    {
        try
        {
            if (Artist_Name != "" && Description != "" && Interestarea != "" && LocationId != 0 && ContactInformation != "")
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
                    objAdmin_State.LanguesIds = LanguesIds.ToString();

                    DataSet objDataSet = Admin_State.Artist_Send_To_DB_Details(objAdmin_State);
                    if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) >= 0)
                    {
                        if (Img != "")
                            fupupdatePhoto.SaveAs(Server.MapPath("~/Artist_Photo/") + "Ar" + Artist_Id.ToString() + ".jpg");

                        ShowNotification("Artist", "Updated Successfully..", NotificationType.success);
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Artist", "Artist Already Existed..!", NotificationType.error);

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
                String myUrl = Request.RawUrl.ToString();
                var result = Path.GetFileName(myUrl);
                String Folder = Path.GetDirectoryName(myUrl);
                string[] SplitOffer = Folder.Split('\\');
                for (int i = 0; i < SplitOffer.Length; i++)
                    if (i == 1)
                        Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                //string CityId = "";
                //string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (string.IsNullOrEmpty(ipAddress))
                //{
                //    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                //}

                //string APIKey = "81dce31671738f9a3fdf05ee98c9a869dc78242e9603381233b3e663f0fcca3d";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, ipAddress);
                //using (WebClient client = new WebClient())
                //{
                //    string json = client.DownloadString(url);
                //    Location location = new JavaScriptSerializer().Deserialize<Location>(json);
                //    DataSet objDataSet = MasterCode.RetrieveQuery("Select City_Id from tbl_admin_city where City_Name='" + location.CityName + "'");
                //    if (objDataSet.Tables[0].Rows.Count > 0)
                //        CityId = objDataSet.Tables[0].Rows[0][0].ToString();
                //    //List<Location> locations = new List<Location>();
                //    //locations.Add(location);
                //    //gvLocation.DataSource = locations;
                //    //gvLocation.DataBind();
                //}

                //Display_List(lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?/'+cast(Advertisement_Id as varchar(max))+'/shortfilm' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image from tbl_Advertisement where Isactive='True' and Status='Approve' and City_Id='" + CityId + "' Order by Advertisement_Id desc");
                //string ShortId = Request.QueryString["shortfilm"];
                //if (ShortId==null)

                DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='true' order by District_Name", "All Districts");
                DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id="+ddlDistrict.SelectedValue.ToString()+" Order by City_Name", "All Locations");
                
                string Artist_Id = Request.QueryString["Id"];

                DataSet objDataSet = MasterCode.RetrieveQuery("Select Artist_Id,Artist_Name from tbl_admin_Artist where Artist_Id=" + Artist_Id);
                if (objDataSet.Tables[0].Rows.Count > 0)
                    lblArtistType.Text = "Artist List - " + objDataSet.Tables[0].Rows[0][1].ToString();

                lblArtistTypeMy.Text = "My Artist List";

                Display_List(lstMyArtist, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo from tbl_Artist_Details where Isactive='true' and Interest_Areas='" + Artist_Id + "' and CreatedById=" + Session["UserId"].ToString() + " order by Name");

                Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo from tbl_Artist_Details where Isactive='true' and Interest_Areas='" + Artist_Id + "' and CreatedById!=" + Session["UserId"].ToString() + " order by Name");
                CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "  select Language_Id,Language_Name from tbl_admin_language where Isactive='True' Order by Language_Id");
                //else
                //    Display_List(lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image from tbl_Advertisement where Isactive='True' and Status='Approve' and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and  ShortFilmId Like '%," + ShortId + ",%' Order by Advertisement_Id desc");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Artist_Id = Request.QueryString["Id"];

            string AddColoums = "";

            if (txtSearch.Text != "")
                AddColoums = AddColoums + " Name Like '" + txtSearch.Text + "%' and ";
            if (ddlDistrict.SelectedIndex != 0)
                AddColoums = AddColoums + " LocationId in (Select City_Id from tbl_Admin_city where District_Id="+ddlDistrict.SelectedValue.ToString()+") and ";
            if (ddlLocation.SelectedIndex != 0)
                AddColoums = AddColoums + " LocationId=" + ddlLocation.SelectedValue.ToString() + " and ";
            if (ddlGender.SelectedIndex != 0)
                AddColoums = AddColoums + " Gender='" + ddlGender.SelectedItem.ToString() + "' and ";

            if (AddColoums != "")
            {
                AddColoums = AddColoums.Remove(AddColoums.Length - 4, 4);
                Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo from tbl_Artist_Details where Isactive='true' and Interest_Areas='" + Artist_Id + "' and CreatedById!=" + Session["UserId"].ToString() + " and " + AddColoums + " order by Name");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lstRecentVideos_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Display")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Artist_Name,(Select City_Name from tbl_admin_city AC where AC.City_Id=AD.LocationId)as City_Name,ContactInformation,languagesId,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(LanguagesId,',',1))FOR XML PATH (''))as Languages from tbl_Artist_Details AD join tbl_admin_Artist AA on AA.Artist_Id=AD.Interest_Areas where AD.Isactive='true' and Artist_Details_Id=" + (e.Item.FindControl("lblArtistId") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    imgPhoto.ImageUrl = objDataSet.Tables[0].Rows[0]["Photo"].ToString();
                    lblName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                    lblArtist.Text = objDataSet.Tables[0].Rows[0]["Artist_Name"].ToString();
                    lblGender.Text = objDataSet.Tables[0].Rows[0]["Gender"].ToString();
                    lblCity.Text = objDataSet.Tables[0].Rows[0]["City_Name"].ToString();
                    lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                    lblContactInformation.Text = objDataSet.Tables[0].Rows[0]["ContactInformation"].ToString();
                    txtLanguagesDis.Text = objDataSet.Tables[0].Rows[0]["Languages"].ToString();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
            else if (e.CommandName == "DisplayEdit")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas,ContactInformation,LocationId,Isactive,languagesId from tbl_Artist_Details where Isactive='true' and Artist_Details_Id=" + (e.Item.FindControl("lblArtistId") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    string[] Languages = (objDataSet.Tables[0].Rows[0]["languagesId"].ToString()).Split(',');

                    if (Languages.Length > 0)
                        for (int i = 0; i < Languages.Length; i++)
                            if (lstLanguage.Items.FindByValue(Languages[i]) != null)
                                lstLanguage.Items.FindByValue(Languages[i]).Selected = true;

                    txtContactInformation.Text = objDataSet.Tables[0].Rows[0]["ContactInformation"].ToString();

                    DropDownList(ddlLocationId, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id="+ddlDistrict.SelectedValue.ToString()+" Order by City_Name", "Select");
                    ddlLocationId.ClearSelection();

                    if (ddlLocationId.Items.FindByValue(objDataSet.Tables[0].Rows[0]["LocationId"].ToString()) != null)
                        ddlLocationId.Items.FindByValue(objDataSet.Tables[0].Rows[0]["LocationId"].ToString()).Selected = true;

                    DropDownList(ddlUpdateInterestArea, "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

                    ddlUpdateInterestArea.ClearSelection();

                    if (ddlUpdateInterestArea.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Interest_Areas"].ToString()) != null)
                        ddlUpdateInterestArea.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Interest_Areas"].ToString()).Selected = true;

                    if (objDataSet.Tables[0].Rows[0]["Gender"].ToString() == "Female") { rdbUpdateFemale.Checked = true; rdbUpdateMale.Checked = true; }
                    else { rdbUpdateFemale.Checked = false; rdbUpdateMale.Checked = true; }

                    txtUpdateDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();

                    txtUpdateArtist.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                    lblID.Text = objDataSet.Tables[0].Rows[0]["Artist_Details_Id"].ToString();
                    lblDName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();

                    if (objDataSet.Tables[0].Rows[0]["Isactive"].ToString() == "True") { rdbActiveNoArtist.Checked = false; rdbActiveYesArtist.Checked = true; }
                    else { rdbActiveYesArtist.Checked = false; rdbActiveNoArtist.Checked = true; }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupEdit()", true);
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            switch (btn.CommandName)
            {
                case "Update":
                    string LanguesIds = ",";
                    for (int i = 0; i < lstLanguage.Items.Count; i++)
                        if (lstLanguage.Items[i].Selected)
                            LanguesIds = LanguesIds + lstLanguage.Items[i].Value + ",";

                    Send_Artist_Data(ddlUpdateInterestArea.SelectedValue.ToString(), txtUpdateArtist.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesArtist.Checked ? true : false, txtUpdateDescription.Text.Trim(), fupupdatePhoto.FileName, (rdbUpdateMale.Checked).ToString(), ddlUpdateInterestArea.SelectedValue.ToString(), Convert.ToInt32(ddlLocationId.SelectedValue.ToString()), txtContactInformation.Text.Trim(), LanguesIds);
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Artists_List(gvArtist, "select Artist_Id,Artist_Name,Isactive from tbl_Admin_Artist where Artist_Name Like '" + txtSearch.Text + "%' Order by Artist_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Artist", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
        }
        catch(Exception Ex)
        {

        }
    }
}
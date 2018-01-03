using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmArtistDetails : System.Web.UI.Page
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

    #region Public Methods

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
            if (Artist_Name != "" && Description != "" && Interestarea != "" && LocationId != 0 && ContactInformation != "" && LanguesIds != "")
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
                        if (Artist_Id == 0)
                        {
                            if (Img != "")
                                fupPhoto.SaveAs(Server.MapPath("~/Artist_Photo/") + "Ar" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                            ShowNotification("Artist", "Inserted Successfully..", NotificationType.success);

                            Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas as Artist_Name,Isactive from tbl_Artist_Details where Isactive='true'and CreatedById=" + Session["UserId"].ToString() + " order by Name");

                            txtName.Text = "";
                            txtDescription.Text = "";
                            ddlInterestArea.SelectedIndex = 0;
                            ddlLocation.SelectedIndex = 0;
                            txtContactInformation.Text = "";
                            lstLanguage.ClearSelection();
                        }
                        else
                        {
                            ShowNotification("Artist", "Updated Successfully..", NotificationType.success);
                            Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas as Artist_Name,Isactive from tbl_Artist_Details where Isactive='true'and CreatedById=" + Session["UserId"].ToString() + " order by Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Artist", "Artist Already Existed..!", NotificationType.error);
                        if (Artist_Id == 0)
                            txtName.Focus();
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

    public void Artists_List(GridView gdv, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                gdv.DataSource = objDataSet;
                gdv.DataBind();
            }
            else
            {
                ShowNotification("Artist", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Artist", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

    #endregion

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

                string AId = Request.QueryString["Id"];

                lblDetailsId.Text = AId;

                DropDownList(ddlInterestArea, "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

                DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' Order by City_Name", "Select");

                Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas as Artist_Name,Isactive from tbl_Artist_Details where Isactive='true'and CreatedById=" + Session["UserId"].ToString() + " order by Name");
                CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "  select Language_Id,Language_Name from tbl_admin_language where Isactive='True' Order by Language_Id");
                txtName.Focus();
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

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            switch (btn.CommandName)
            {
                case "Save":

                    string LanguesIds = ",";
                    for (int i = 0; i < lstLanguage.Items.Count; i++)
                        if (lstLanguage.Items[i].Selected)
                            LanguesIds = LanguesIds + lstLanguage.Items[i].Value + ",";

                    if (fupPhoto.FileName != "")
                        Send_Artist_Data(lblDetailsId.Text, txtName.Text.Trim(), 0, "", true, txtDescription.Text.Trim(), fupPhoto.FileName, (rdbMale.Checked).ToString(), ddlInterestArea.SelectedValue.ToString(), Convert.ToInt32(ddlLocation.SelectedValue.ToString()), txtContactInformation.Text.Trim(), LanguesIds);
                    else
                        ShowNotification("Artist", "Please Select Photo..!", NotificationType.error);
                    break;
                case "Clear":
                    txtName.Text = "";
                    txtDescription.Text = "";
                    ddlInterestArea.SelectedIndex = 0;
                    break;
                case "Update":
                    string LanguesIds1 = ",";
                    for (int i = 0; i < lstLanguage.Items.Count; i++)
                        if (lstLanguage.Items[i].Selected)
                            LanguesIds1 = LanguesIds1 + lstLanguage.Items[i].Value + ",";

                    Send_Artist_Data(ddlUpdateInterestArea.SelectedValue.ToString(), txtUpdateArtist.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesArtist.Checked ? true : false, txtUpdateDescription.Text.Trim(), fupupdatePhoto.FileName, (rdbUpdateMale.Checked).ToString(), ddlUpdateInterestArea.SelectedValue.ToString(), Convert.ToInt32(ddlLocation.SelectedValue.ToString()), txtContactInformation.Text.Trim(), LanguesIds1);
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

    protected void lnk_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            ListViewItem lst = (ListViewItem)lnk.NamingContainer;

            DropDownList(ddlUpdateInterestArea, "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

            ddlUpdateInterestArea.ClearSelection();

            if (ddlUpdateInterestArea.Items.FindByValue((lst.FindControl("lblGridInterest_Areas") as Label).Text) != null)
                ddlUpdateInterestArea.Items.FindByValue((lst.FindControl("lblGridInterest_Areas") as Label).Text).Selected = true;

            if ((lst.FindControl("lblGridGender") as Label).Text == "Female") { rdbUpdateFemale.Checked = true; rdbUpdateMale.Checked = true; }
            else { rdbUpdateFemale.Checked = false; rdbUpdateMale.Checked = true; }

            txtUpdateDescription.Text = (lst.FindControl("lblGridDescription") as Label).Text;

            txtUpdateArtist.Text = (lst.FindControl("lblGridArtist") as Label).Text;
            lblID.Text = (lst.FindControl("lblGridArtistId") as Label).Text;
            lblDName.Text = (lst.FindControl("lblGridArtist") as Label).Text;

            if ((lst.FindControl("lblGridArtistIsactive") as Label).Text == " Active ") { rdbActiveNoArtist.Checked = false; rdbActiveYesArtist.Checked = true; }
            else { rdbActiveYesArtist.Checked = false; rdbActiveNoArtist.Checked = true; }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lstRecentVideos_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Display"))
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("delete from tbl_Artist_Details where Artist_Details_Id=" + (e.Item.FindControl("lblGridArtistId") as Label).Text);

                Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas as Artist_Name,Isactive from tbl_Artist_Details where Isactive='true'and CreatedById=" + Session["UserId"].ToString() + " order by Name");

                ShowNotification("Artist", "Deleted Successfully..!", NotificationType.success);

                //DropDownList(ddlUpdateInterestArea, "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

                //ddlUpdateInterestArea.ClearSelection();

                //string dsdd = (e.Item.FindControl("lblGridInterest_Areas") as Label).Text;

                //if (ddlUpdateInterestArea.Items.FindByValue((e.Item.FindControl("lblGridInterest_Areas") as Label).Text) != null)
                //    ddlUpdateInterestArea.Items.FindByValue((e.Item.FindControl("lblGridInterest_Areas") as Label).Text).Selected = true;

                //if ((e.Item.FindControl("lblGridGender") as Label).Text == "Female") { rdbUpdateFemale.Checked = true; rdbUpdateMale.Checked = true; }
                //else { rdbUpdateFemale.Checked = false; rdbUpdateMale.Checked = true; }

                //txtUpdateDescription.Text = (e.Item.FindControl("lblGridDescription") as Label).Text;

                //txtUpdateArtist.Text = (e.Item.FindControl("lblGridArtist") as Label).Text;
                //lblID.Text = (e.Item.FindControl("lblGridArtistId") as Label).Text;
                //lblDName.Text = (e.Item.FindControl("lblGridArtist") as Label).Text;

                //if ((e.Item.FindControl("lblGridArtistIsactive") as Label).Text == " Active ") { rdbActiveNoArtist.Checked = false; rdbActiveYesArtist.Checked = true; }
                //else { rdbActiveYesArtist.Checked = false; rdbActiveNoArtist.Checked = true; }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
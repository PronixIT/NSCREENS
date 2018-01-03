using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using System.IO;

public partial class Admin_frmTitleRegister : System.Web.UI.Page
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

    public void Send_Title_Data(string Title_Name, int Title_Id, string DumpTitle_Name, bool Isactive, string Tag, string Languages, string Image, string DumpLanguage,string DumpTag)
    {
        try
        {
            if (Title_Name != "" && Languages != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.TitleName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Title_Name.ToLower()).Trim();
                    objAdmin_State.TitleId = Title_Id;
                    objAdmin_State.DumpTitleName = DumpTitle_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    objAdmin_State.Tag = Tag;
                    objAdmin_State.Languages = Languages;
                    objAdmin_State.DumpLanguesIds = DumpLanguage;
                    objAdmin_State.DumpTag= DumpTag;

                    DataSet objDataSet = Admin_State.Title_Send_To_DB(objAdmin_State);
                    if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (Title_Id == 0)
                        {
                            if (Image != "")
                            {
                                for (int i = 0; i < Convert.ToInt32(objDataSet.Tables[0].Rows[0][1].ToString())-1; i++)
                                    fupUploadImage.SaveAs(Server.MapPath("~/Video_Images/") + "Reg" + (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString())-i).ToString() + ".jpg");
                            }

                            ShowNotification("Title", "Register Successfully..", NotificationType.success);
                            txtTitle.Text = "";
                            txtTag.Text = "";
                            lstLanguage.ClearSelection();

                        }
                        else
                        {
                            if (Image != "")
                                fupUpdateImage.SaveAs(Server.MapPath("~/Video_Images/") + "Reg" + Title_Id + ".jpg");

                            ShowNotification("Title", "Updated Successfully..", NotificationType.success);

                        }

                        if (Session["UserName"] == "admin")
                            Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1)))as Languages,Languages as Languages1,CreatedById from tbl_Register_Title Order by Title_Name");
                        else
                            Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Languages as Languages1 from tbl_Register_Title where CreatedById='" + Session["UserId"].ToString() + "' Order by Title_Name");

                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Title", "Title Already Existed..!", NotificationType.error);
                        if (Title_Id == 0)
                            txtTitle.Focus();
                        else
                            txtUpdateTitle.Focus();
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-10")
                        ShowNotification("Title", "Title Already Existed..!..!", NotificationType.error);

                }
                else
                    ShowNotification("Title", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Title", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Titles_List(GridView gdv, string Query)
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
                ShowNotification("Title", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Title", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

                CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");
                if (Session["UserName"] == "admin")
                    Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1)))as Languages,Languages as Languages1,CreatedById from tbl_Register_Title Order by Title_Name");
                else
                    Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Languages as Languages1 from tbl_Register_Title where CreatedById='" + Session["UserId"].ToString() + "' Order by Title_Name");
                txtTitle.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);
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
                    if (fupUploadImage.FileName != "")
                        Send_Title_Data(txtTitle.Text.Trim(), 0, "", true, txtTag.Text.Trim(), LanguesIds, fupUploadImage.FileName, "","");
                    else
                        ShowNotification("Title", "Please Fill All Fields..!", NotificationType.error);
                    break;
                case "Clear":
                    txtTitle.Text = "";
                    txtTag.Text = "";
                    lstLanguage.ClearSelection();
                    break;
                case "Update":

                    string ULanguesIds = ",";
                    for (int i = 0; i < lstUpdateLanguage.Items.Count; i++)
                        if (lstUpdateLanguage.Items[i].Selected)
                            ULanguesIds = ULanguesIds + lstUpdateLanguage.Items[i].Value.ToString() + ",";

                    Send_Title_Data(txtUpdateTitle.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesTitle.Checked ? true : false, txtUpdateTag.Text.Trim(), ULanguesIds, fupUpdateImage.FileName, lblDumpLanguages.Text.Trim(),lblUpdateTagDump.Text);

                    btnClose.Focus();
                    break;
                case "Seach":
                    //Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive from tbl_Register_Title where Title_Name Like '" + txtSearch.Text + "%' Order by Title_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvTitle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateTitle.Text = (gvTitle.Rows[index].FindControl("lblGridTitle") as Label).Text;
                lblID.Text = (gvTitle.Rows[index].FindControl("lblGridTitleId") as Label).Text;
                lblDName.Text = (gvTitle.Rows[index].FindControl("lblGridTitle") as Label).Text;
                lblDumpLanguages.Text = (gvTitle.Rows[index].FindControl("lblGridLanguages1") as Label).Text;
                lblUpdateTagDump.Text = (gvTitle.Rows[index].FindControl("lblGridTag") as Label).Text;

                txtUpdateTag.Text = (gvTitle.Rows[index].FindControl("lblGridTag") as Label).Text;

                CheckBoxList(lstUpdateLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");

                string LangIds = "";
                lstUpdateLanguage.ClearSelection();
                string[] Split = ((gvTitle.Rows[index].FindControl("lblGridLanguages") as Label).Text).Split(',');
                if (Split.Length > 0)
                    for (int i = 0; i < Split.Length; i++)
                        foreach (ListItem li in lstUpdateLanguage.Items)
                            if (li.Text == Split[i])
                            {
                                li.Selected = true;
                                LangIds = LangIds + li.Text;
                            }
                txtUpdateLanguage.Text = LangIds;
                if ((gvTitle.Rows[index].FindControl("lblGridTitleIsactive") as Label).Text == " Active ") { rdbActiveNoTitle.Checked = false; rdbActiveYesTitle.Checked = true; }
                else { rdbActiveYesTitle.Checked = false; rdbActiveNoTitle.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
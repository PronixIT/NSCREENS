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

public partial class Admin_frmLanguage : System.Web.UI.Page
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

    public void Send_Language_Data(string Language_Name, int Language_Id, string DumpLanguage_Name, bool Isactive)
    {
        try
        {
            if (Language_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.LanguageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Language_Name.ToLower());
                    objAdmin_State.LanguageId = Language_Id;
                    objAdmin_State.DumpLanguageName = DumpLanguage_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    DataSet objDataSet = Admin_State.Language_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Language_Id == 0)
                        {
                            ShowNotification("Language", "Inserted Successfully..", NotificationType.success);
                            txtLanguage.Text = "";
                            Languages_List(gvLanguage, "select Language_Id,Language_Name,Isactive from tbl_Admin_Language Order by Language_Name");
                        }
                        else
                        {
                            ShowNotification("Language", "Updated Successfully..", NotificationType.success);
                            Languages_List(gvLanguage, "select Language_Id,Language_Name,Isactive from tbl_Admin_Language Order by Language_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Language", "Language Already Existed..!", NotificationType.error);
                        if (Language_Id == 0)
                            txtLanguage.Focus();
                        else
                            txtUpdateLanguage.Focus();
                    }
                    else
                        ShowNotification("Language", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Language", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Language", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Languages_List(GridView gdv, string Query)
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
                ShowNotification("Language", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Language", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

                Languages_List(gvLanguage, "select Language_Id,Language_Name,Isactive from tbl_Admin_Language Order by Language_Name");
                txtLanguage.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Language", dispErrorMsg, NotificationType.error);
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
                    Send_Language_Data(txtLanguage.Text.Trim(), 0, "", true);
                    break;
                case "Clear":
                    txtLanguage.Text = "";
                    break;
                case "Update":
                    Send_Language_Data(txtUpdateLanguage.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesLanguage.Checked ? true : false);
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Languages_List(gvLanguage, "select Language_Id,Language_Name,Isactive from tbl_Admin_Language where Language_Name Like '" + txtSearch.Text + "%' Order by Language_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Language", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvLanguage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateLanguage.Text = (gvLanguage.Rows[index].FindControl("lblGridLanguage") as Label).Text;
                lblID.Text = (gvLanguage.Rows[index].FindControl("lblGridLanguageId") as Label).Text;
                lblDName.Text = (gvLanguage.Rows[index].FindControl("lblGridLanguage") as Label).Text;

                if ((gvLanguage.Rows[index].FindControl("lblGridLanguageIsactive") as Label).Text == " Active ") { rdbActiveNoLanguage.Checked = false; rdbActiveYesLanguage.Checked = true; }
                else { rdbActiveYesLanguage.Checked = false; rdbActiveNoLanguage.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Language", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
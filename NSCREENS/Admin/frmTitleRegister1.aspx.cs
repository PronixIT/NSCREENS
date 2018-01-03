using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using System.Globalization;

public partial class Admin_frmTitle : System.Web.UI.Page
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

    public void Send_Title_Data(string Title_Name, int Title_Id, string DumpTitle_Name, bool Isactive)
    {
        try
        {
            if (Title_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.TitleName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Title_Name.ToLower());
                    objAdmin_State.TitleId = Title_Id;
                    objAdmin_State.DumpTitleName = DumpTitle_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    DataSet objDataSet = Admin_State.Title_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Title_Id == 0)
                        {
                            ShowNotification("Title", "Inserted Successfully..", NotificationType.success);
                            txtTitle.Text = "";
                            Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,Languages from tbl_Register_Title Order by Title_Name");
                        }
                        else
                        {
                            ShowNotification("Title", "Updated Successfully..", NotificationType.success);
                            Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,Languages from tbl_Register_Title Order by Title_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Title", "Title Already Existed..!", NotificationType.error);
                        if (Title_Id == 0)
                            txtTitle.Focus();
                        else
                            txtUpdateTitle.Focus();
                    }
                    else
                        ShowNotification("Title", "Not inserted..!", NotificationType.error);

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
                Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,Languages from tbl_Register_Title Order by Title_Name");
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
                    Send_Title_Data(txtTitle.Text.Trim(), 0, "", true);
                    break;
                case "Clear":
                    txtTitle.Text = "";
                    break;
                case "Update":
                    Send_Title_Data(txtUpdateTitle.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesTitle.Checked ? true : false);
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
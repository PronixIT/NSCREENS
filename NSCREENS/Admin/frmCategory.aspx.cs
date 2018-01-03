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

public partial class Admin_frmCategory : System.Web.UI.Page
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

    public void Send_Category_Data(string Category_Name, int Category_Id, string DumpCategory_Name, bool Isactive)
    {
        try
        {
            if (Category_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_Category objAdmin_Category = new Admin_Category();

                    objAdmin_Category.CategoryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Category_Name.ToLower());
                    objAdmin_Category.CategoryId = Category_Id;
                    objAdmin_Category.DumpCategoryName = DumpCategory_Name;
                    objAdmin_Category.Isactive = Isactive;
                    objAdmin_Category.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_Category.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    DataSet objDataSet = Admin_Category.Category_Send_To_DB(objAdmin_Category);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Category_Id == 0)
                        {
                            ShowNotification("Category", "Inserted Successfully..", NotificationType.success);
                            txtCategory.Text = "";
                            Categorys_List(gvCategory, "select Category_Id,Category_Name,Isactive from tbl_Admin_Category Order by Category_Name");
                        }
                        else
                        {
                            ShowNotification("Category", "Updated Successfully..", NotificationType.success);
                            Categorys_List(gvCategory, "select Category_Id,Category_Name,Isactive from tbl_Admin_Category Order by Category_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Category", "Category Already Existed..!", NotificationType.error);
                        if (Category_Id == 0)
                            txtCategory.Focus();
                        else
                            txtUpdateCategory.Focus();
                    }
                    else
                        ShowNotification("Category", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Category", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Category", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Categorys_List(GridView gdv, string Query)
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
                ShowNotification("Category", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Category", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

                Categorys_List(gvCategory, "select Category_Id,Category_Name,Isactive from tbl_Admin_Category Order by Category_Name");
                txtCategory.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Category", dispErrorMsg, NotificationType.error);
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
                    Send_Category_Data(txtCategory.Text.Trim(), 0, "", true);
                    break;
                case "Clear":
                    txtCategory.Text = "";
                    break;
                case "Update":
                    Send_Category_Data(txtUpdateCategory.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesCategory.Checked ? true : false);
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Categorys_List(gvCategory, "select Category_Id,Category_Name,Isactive from tbl_Admin_Category where Category_Name Like '" + txtSearch.Text + "%' Order by Category_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Category", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateCategory.Text = (gvCategory.Rows[index].FindControl("lblGridCategory") as Label).Text;
                lblID.Text = (gvCategory.Rows[index].FindControl("lblGridCategoryId") as Label).Text;
                lblDName.Text = (gvCategory.Rows[index].FindControl("lblGridCategory") as Label).Text;

                if ((gvCategory.Rows[index].FindControl("lblGridCategoryIsactive") as Label).Text == " Active ") { rdbActiveNoCategory.Checked = false; rdbActiveYesCategory.Checked = true; }
                else { rdbActiveYesCategory.Checked = false; rdbActiveNoCategory.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Category", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
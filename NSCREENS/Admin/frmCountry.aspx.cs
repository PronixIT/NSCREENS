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

public partial class Admin_frmCountry : System.Web.UI.Page
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

    public void Send_Country_Data(string Country_Name, int Country_Id, string DumpCountry_Name, bool Isactive)
    {
        try
        {
            if (Country_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.CountryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Country_Name.ToLower());
                    objAdmin_State.CountryId = Country_Id;
                    objAdmin_State.DumpCountryName = DumpCountry_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    DataSet objDataSet = Admin_State.Country_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Country_Id == 0)
                        {
                            ShowNotification("Country", "Inserted Successfully..", NotificationType.success);
                            txtCountry.Text = "";
                            Countrys_List(gvCountry, "select Country_Id,Country_Name,Isactive from tbl_Admin_Country Order by Country_Name");
                        }
                        else
                        {
                            ShowNotification("Country", "Updated Successfully..", NotificationType.success);
                            Countrys_List(gvCountry, "select Country_Id,Country_Name,Isactive from tbl_Admin_Country Order by Country_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Country", "Country Already Existed..!", NotificationType.error);
                        if (Country_Id == 0)
                            txtCountry.Focus();
                        else
                            txtUpdateCountry.Focus();
                    }
                    else
                        ShowNotification("Country", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Country", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Country", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Countrys_List(GridView gdv, string Query)
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
                ShowNotification("Country", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Country", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

                Countrys_List(gvCountry, "select Country_Id,Country_Name,Isactive from tbl_Admin_Country Order by Country_Name");
                txtCountry.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Country", dispErrorMsg, NotificationType.error);
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
                    Send_Country_Data(txtCountry.Text.Trim(), 0, "", true);
                    break;
                case "Clear":
                    txtCountry.Text = "";
                    break;
                case "Update":
                    Send_Country_Data(txtUpdateCountry.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesCountry.Checked ? true : false);
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Countrys_List(gvCountry, "select Country_Id,Country_Name,Isactive from tbl_Admin_Country where Country_Name Like '" + txtSearch.Text + "%' Order by Country_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Country", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateCountry.Text = (gvCountry.Rows[index].FindControl("lblGridCountry") as Label).Text;
                lblID.Text = (gvCountry.Rows[index].FindControl("lblGridCountryId") as Label).Text;
                lblDName.Text = (gvCountry.Rows[index].FindControl("lblGridCountry") as Label).Text;

                if ((gvCountry.Rows[index].FindControl("lblGridCountryIsactive") as Label).Text == " Active ") { rdbActiveNoCountry.Checked = false; rdbActiveYesCountry.Checked = true; }
                else { rdbActiveYesCountry.Checked = false; rdbActiveNoCountry.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Country", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
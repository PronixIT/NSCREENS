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

public partial class Admin_frmState : System.Web.UI.Page
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

    public void Send_State_Data(string State_Name, int State_Id, string DumpState_Name, bool Isactive, int DumpCountryId, int CountryId)
    {
        try
        {
            if (State_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.StateName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(State_Name.ToLower());
                    objAdmin_State.StateId = State_Id;
                    objAdmin_State.DumpState = DumpState_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdmin_State.DumpCountryId = DumpCountryId;
                    objAdmin_State.CountryId = CountryId;

                    DataSet objDataSet = Admin_State.State_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (State_Id == 0)
                        {
                            ShowNotification("State", "Inserted Successfully..", NotificationType.success);
                            txtState.Text = "";
                            ddlCountry.SelectedIndex = 0;
                            States_List(gvState, "select Country_Id,State_Id,State_Name,Isactive,(Select Country_Name from tbl_admin_country AC where AC.Country_Id=S.Country_Id)as Country_Name from tbl_Admin_State S Order by State_Name");
                        }
                        else
                        {
                            ShowNotification("State", "Updated Successfully..", NotificationType.success);
                            States_List(gvState, "select Country_Id,State_Id,State_Name,Isactive,(Select Country_Name from tbl_admin_country AC where AC.Country_Id=S.Country_Id)as Country_Name from tbl_Admin_State S Order by State_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("State", "State Already Existed..!", NotificationType.error);
                        if (State_Id == 0)
                            txtState.Focus();
                        else
                            txtUpdateState.Focus();
                    }
                    else
                        ShowNotification("State", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("State", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("State", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void States_List(GridView gdv, string Query)
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
                ShowNotification("State", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("State", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

                DropDownList(ddlCountry, "Country_Id", "Country_Name", "Select Country_Id,Country_Name from tbl_admin_country where Isactive='True' Order by Country_Name", "Select");
                States_List(gvState, "select Country_Id,State_Id,State_Name,Isactive,(Select Country_Name from tbl_admin_country AC where AC.Country_Id=S.Country_Id)as Country_Name from tbl_Admin_State S Order by State_Name");
                txtState.Focus();
            }

        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("State", dispErrorMsg, NotificationType.error);
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
                    Send_State_Data(txtState.Text.Trim(), 0, "", true, 0, Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
                    break;
                case "Clear":
                    txtState.Text = "";
                    ddlCountry.SelectedIndex = 0;
                    break;
                case "Update":
                    Send_State_Data(txtUpdateState.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesState.Checked ? true : false, Convert.ToInt32(lblDumpCountryId.Text.Trim()), Convert.ToInt32(ddlUpdateCountry.SelectedValue.ToString()));
                    btnClose.Focus();
                    break;
                case "Seach":
                    //States_List(gvState, "select State_Id,State_Name,Isactive from tbl_Admin_State where State_Name Like '" + txtSearch.Text + "%' Order by State_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("State", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateState.Text = (gvState.Rows[index].FindControl("lblGridState") as Label).Text;
                lblID.Text = (gvState.Rows[index].FindControl("lblGridStateId") as Label).Text;
                lblDName.Text = (gvState.Rows[index].FindControl("lblGridState") as Label).Text;
                lblDumpCountryId.Text = (gvState.Rows[index].FindControl("lblGridCountry_Id") as Label).Text;

                DropDownList(ddlUpdateCountry, "Country_Id", "Country_Name", "Select Country_Id,Country_Name from tbl_admin_country where Isactive='True' Order by Country_Name", "Select");
                if ((ddlUpdateCountry.Items.FindByValue((gvState.Rows[index].FindControl("lblGridCountry_Id") as Label).Text)) != null)
                    (ddlUpdateCountry.Items.FindByValue((gvState.Rows[index].FindControl("lblGridCountry_Id") as Label).Text)).Selected = true;

                if ((gvState.Rows[index].FindControl("lblGridStateIsactive") as Label).Text == " Active ") { rdbActiveNoState.Checked = false; rdbActiveYesState.Checked = true; }
                else { rdbActiveYesState.Checked = false; rdbActiveNoState.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("State", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
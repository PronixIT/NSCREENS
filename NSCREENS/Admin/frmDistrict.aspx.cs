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

public partial class Admin_frmDistrict : System.Web.UI.Page
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

    public void Clear_District()
    {
        txtDistrict.Text = "";
        ddlState.Focus();
    }

    public void Send_District_Data(int StateId, string District_Name, int District_Id, string DumpDistrict_Name, bool Isactive, int DumpState_Id)
    {
        try
        {
            if (District_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.StateId = StateId;
                    objAdmin_State.DistrictName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(District_Name.ToLower());
                    objAdmin_State.DumpDistrict = DumpDistrict_Name;
                    objAdmin_State.DistrictId = District_Id;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdmin_State.DumpSateId = DumpState_Id;

                    DataSet objDataSet = Admin_State.District_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (District_Id == 0)
                        {
                            ShowNotification("District", "Inserted Successfully..", NotificationType.success);
                            Clear_District();
                            if (ddlSearchState.SelectedIndex != 0)
                                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD where State_Id=" + ddlSearchState.SelectedValue.ToString() + " Order by District_Name");
                            else
                                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD Order by District_Name");
                        }
                        else
                        {
                            ShowNotification("District", "Updated Successfully..", NotificationType.success);
                            //if (txtSearchDistrict.Text != "")
                            //    Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD where State_Id=" + ddlSearchState.SelectedValue.ToString() + " and District_Name Like '" + txtSearchDistrict.Text + "%' Order by District_Name");
                            //else
                            if (ddlSearchState.SelectedIndex != 0)
                                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD where State_Id=" + ddlSearchState.SelectedValue.ToString() + " Order by District_Name");
                            else
                                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD Order by District_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("District", "District Already Existed..!", NotificationType.error);
                        if (District_Id == 0)
                            txtDistrict.Focus();
                        else
                            txtUpdateDistrict.Focus();
                    }
                    else
                        ShowNotification("District", "Not inserted..!", NotificationType.error);
                }
                else
                    ShowNotification("District", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("District", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Districts_List(GridView gv, string Query)
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

                DropDownList(ddlSearchState, "State_Id", "State_Name", "SELECT State_Id,State_Name FROM tbl_Admin_State WHERE Isactive='True' ORDER BY State_Name", "All States");
                DropDownList(ddlState, "State_Id", "State_Name", "SELECT State_Id,State_Name FROM tbl_Admin_State WHERE Isactive='True' ORDER BY State_Name", "Select");
                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD Order by District_Name");
                ddlState.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("District", dispErrorMsg, NotificationType.error);
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
                    Send_District_Data(Convert.ToInt32(ddlState.SelectedValue.ToString()), txtDistrict.Text.Trim(), 0, "", false, 0);
                    break;
                case "Clear":
                    Clear_District();
                    ddlState.SelectedIndex = 0;
                    break;
                case "Update":
                    Send_District_Data(Convert.ToInt32(ddlUpdateState.SelectedValue.ToString()), txtUpdateDistrict.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesDistrict.Checked ? true : false, Convert.ToInt32(lblDumpStateId.Text.Trim()));
                    btnClose.Focus();
                    break;
                case "Search":
                    //if (txtSearchDistrict.Text != "")
                    //    Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD where State_Id=" + ddlSearchState.SelectedValue.ToString() + "and District_Name Like '" + txtSearchDistrict.Text + "%' Order by District_Name");
                    //else
                    Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD where State_Id=" + ddlSearchState.SelectedValue.ToString() + " Order by District_Name");
                    //txtSearchDistrict.Focus();
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("District", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DropDownList(ddlUpdateState, "State_Id", "State_Name", "SELECT State_Id,State_Name FROM tbl_Admin_State WHERE Isactive='True' ORDER BY State_Name", "");
                if (ddlUpdateState.Items.FindByValue((gvDistrict.Rows[index].FindControl("lblGridStateId") as Label).Text) != null)
                    ddlUpdateState.Items.FindByValue((gvDistrict.Rows[index].FindControl("lblGridStateId") as Label).Text).Selected = true;

                txtUpdateDistrict.Text = (gvDistrict.Rows[index].FindControl("lblGridDistrict") as Label).Text;
                lblDName.Text = (gvDistrict.Rows[index].FindControl("lblGridDistrict") as Label).Text;
                lblID.Text = (gvDistrict.Rows[index].FindControl("lblGridDistrictId") as Label).Text;
                lblDumpStateId.Text = (gvDistrict.Rows[index].FindControl("lblGridStateId") as Label).Text;

                if ((gvDistrict.Rows[index].FindControl("lblGridDistrictIsactive") as Label).Text == " Active ") { rdbActiveNoDistrict.Checked = false; rdbActiveYesDistrict.Checked = true; }
                else { rdbActiveYesDistrict.Checked = false; rdbActiveNoDistrict.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("District", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void ddlSearchState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSearchState.SelectedIndex != 0)
                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD where State_Id=" + ddlSearchState.SelectedValue.ToString() + " Order by District_Name");
            else
                Districts_List(gvDistrict, "select District_Id,District_Name,State_Id,Isactive,(Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id)as State_Name from tbl_Admin_District AD Order by District_Name");
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("District", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
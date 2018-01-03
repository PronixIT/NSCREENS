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

public partial class Admin_frmCity : System.Web.UI.Page
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

    public void Clear_City()
    {
        txtCity.Text = "";
        ddlState.Focus();
    }

    public void Send_City_Data(int DistrictId, string City_Name, int City_Id, string DumpCity_Name, bool Isactive, int DumpDistrict_Id)
    {
        try
        {
            if (City_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.DistrictId = DistrictId;
                    objAdmin_State.CityName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(City_Name.ToLower());
                    objAdmin_State.DumpCity = DumpCity_Name;
                    objAdmin_State.CityId = City_Id;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdmin_State.DumpDistrictId = DumpDistrict_Id;

                    DataSet objDataSet = Admin_State.City_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (City_Id == 0)
                        {
                            ShowNotification("City", "Inserted Successfully..", NotificationType.success);
                            Clear_City();
                            //ddlSearchDistrict.SelectedIndex = 0;
                            //txtSearchCity.Text = "";
                            //Citys_List(gvCity, "select City_Id,City_Name,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC Order by City_Name");
                            Search();
                        }
                        else
                        {
                            ShowNotification("City", "Updated Successfully..", NotificationType.success);
                            Search();
                            //if (txtSearchCity.Text != "")
                            //    Citys_List(gvCity, "select City_Id,City_Name,City_Code,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC where District_Id=" + ddlSearchDistrict.SelectedValue.ToString() + " and City_Name Like '" + txtSearchCity.Text + "%' Order by City_Name");
                            //else
                            //    Citys_List(gvCity, "select City_Id,City_Name,City_Code,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC where District_Id=" + ddlSearchDistrict.SelectedValue.ToString() + " Order by City_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("City", "City Already Existed..!", NotificationType.error);
                        //if (District_Id == 0)
                        //    txtDistrict.Focus();
                        //else
                        //    txtUpdateDistrict.Focus();
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

    public void Citys_List(GridView gv, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = objDataSet;
                gv.DataBind();
                lblCityListList.Text = objDataSet.Tables[0].Rows.Count.ToString();
            }
            else
            {
                gv.DataSource = "";
                gv.DataBind();
                lblCityListList.Text = "0";
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void Search()
    {
        try
        {
            string AddColoums = "";
            if (ddlSearchState.SelectedIndex != 0)
                AddColoums = AddColoums + " District_Id in (Select District_Id from tbl_admin_district where State_Id=" + ddlSearchState.SelectedValue.ToString() + ") and ";
            if (ddlSearchDistrict.SelectedIndex != 0)
                AddColoums = AddColoums + " District_Id=" + ddlSearchDistrict.SelectedValue.ToString() + " and ";
            if (AddColoums != "")
            {
                AddColoums = AddColoums.Remove(AddColoums.Length - 4, 4);
                Citys_List(gvCity, "select City_Id,City_Name,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC where " + AddColoums);
            }
            else
                Citys_List(gvCity, "select City_Id,City_Name,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC Order by City_Name`");
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

                DropDownList(ddlState, "State_Id", "State_Name", "Select State_Id,State_Name from tbl_admin_state TA where Isactive='True' Order by State_Name", "Select");
                DropDownList(ddlSearchState, "State_Id", "State_Name", "Select State_Id,State_Name from tbl_admin_state TA where Isactive='True' Order by State_Name", "All States");
                DropDownList(ddlSearchDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' ORDER BY District_Name", "All Districts");
                DropDownList(ddlDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' and State_Id=" + ddlState.SelectedValue + " ORDER BY District_Name", "Select");
                Citys_List(gvCity, "select City_Id,City_Name,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC Order by City_Name");
                ddlState.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("City", dispErrorMsg, NotificationType.error);
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
                    Send_City_Data(Convert.ToInt32(ddlDistrict.SelectedValue.ToString()), txtCity.Text.Trim(), 0, "", false, 0);
                    break;
                case "Clear":
                    Clear_City();
                    ddlState.SelectedIndex = 0;
                    DropDownList(ddlDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' and State_Id=" + ddlState.SelectedValue + " ORDER BY District_Name", "Select");
                    break;
                case "Update":
                    Send_City_Data(Convert.ToInt32(ddlUpdateDistrict.SelectedValue.ToString()), txtUpdateCity.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesCity.Checked ? true : false, Convert.ToInt32(lblDumpDistrictId.Text.Trim()));
                    btnClose.Focus();
                    break;
                case "Search":
                    //if (txtSearchCity.Text != "")
                    //    Citys_List(gvCity, "select City_Id,City_Name,City_Code,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC where District_Id=" + ddlSearchDistrict.SelectedValue.ToString() + "and City_Name Like '" + txtSearchCity.Text + "%' Order by City_Name");
                    //else
                    //    Citys_List(gvCity, "select City_Id,City_Name,City_Code,District_Id,Isactive,(Select District_Name from tbl_admin_district AD where AD.District_Id=AC.District_Id)as District_Name,(Select (Select State_Name from tbl_admin_state TAS where TAS.State_Id=AD.State_Id) from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Name from tbl_Admin_City AC where District_Id=" + ddlSearchDistrict.SelectedValue.ToString() + " Order by City_Name");
                    //txtSearchCity.Focus();
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("City", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataSet objDataSet = MasterCode.RetrieveQuery("Select State_Id from tbl_admin_district AD where District_Id=" + (gvCity.Rows[index].FindControl("lblGridDistrictId") as Label).Text);

                DropDownList(ddlUpdateState, "State_Id", "State_Name", "Select State_Id,State_Name from tbl_admin_state TA where Isactive='True' Order by State_Name", "Select");
                if (objDataSet.Tables[0].Rows.Count > 0)
                    if (ddlUpdateState.Items.FindByValue(objDataSet.Tables[0].Rows[0]["State_Id"].ToString()) != null)
                        ddlUpdateState.Items.FindByValue(objDataSet.Tables[0].Rows[0]["State_Id"].ToString()).Selected = true;

                DropDownList(ddlUpdateDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' and State_Id=" + ddlUpdateState.SelectedValue + " ORDER BY District_Name", "");
                if (ddlUpdateDistrict.Items.FindByValue((gvCity.Rows[index].FindControl("lblGridDistrictId") as Label).Text) != null)
                    ddlUpdateDistrict.Items.FindByValue((gvCity.Rows[index].FindControl("lblGridDistrictId") as Label).Text).Selected = true;

                txtUpdateCity.Text = (gvCity.Rows[index].FindControl("lblGridCity") as Label).Text;
                lblDName.Text = (gvCity.Rows[index].FindControl("lblGridCity") as Label).Text;
                lblID.Text = (gvCity.Rows[index].FindControl("lblGridCityId") as Label).Text;
                lblDumpDistrictId.Text = (gvCity.Rows[index].FindControl("lblGridDistrictId") as Label).Text;

                if ((gvCity.Rows[index].FindControl("lblGridCityIsactive") as Label).Text == " Active ") { rdbActiveNoCity.Checked = false; rdbActiveYesCity.Checked = true; }
                else { rdbActiveYesCity.Checked = false; rdbActiveNoCity.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("City", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' and State_Id=" + ddlState.SelectedValue + " ORDER BY District_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlUpdateState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlUpdateDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' and State_Id=" + ddlState.SelectedValue + " ORDER BY District_Name", "");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlSearchState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlSearchDistrict, "District_Id", "District_Name", "SELECT District_Id,District_Name FROM tbl_Admin_District WHERE Isactive='True' and State_Id=" + ddlSearchState.SelectedValue + " ORDER BY District_Name", "All Districts");
            Search();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlSearchDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Search();
        }
        catch (Exception Ex)
        {

        }
    }
}
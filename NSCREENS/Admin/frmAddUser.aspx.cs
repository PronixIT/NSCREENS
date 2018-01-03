using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

public partial class Admin_frmAddUser : System.Web.UI.Page
{
    #region Private Methods & Enum

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

    public void User_Profile()
    {
        try
        {
            txtName.Text = "";
            txtMobileNumber.Text = "";
            txtEmailId.Text = "";
            txtAddress.Text = "";
            ddlDay.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
            DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
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

    public void Display_List(GridView gv, string Query)
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

                int Years = DateTime.Now.AddHours(Connection.SetHours).Year;

                for (int i = 0; i < 80; i++)
                    ddlYear.Items.Insert(i + 1, new ListItem((Years - i).ToString(), (i + 1).ToString()));

                DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' Order by State_Name", "Select");
                DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
                DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
                Display_List(gvUserlist, "SELECT Register_Id,Name,Mobile_No,EmailId,Address,Isactive,(Select City_Name from tbl_admin_city C where C.City_Id=RU.City_Id)as City_Name,Photo,convert(varchar(12),DOB,103)as DOB,(Select Username from tbl_user U where U.Staff_Id=RU.Register_Id)as Username,(Select Password from tbl_user U where U.Staff_Id=RU.Register_Id)as Password FROM tbl_Register_User RU order by Register_Id desc");
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
            string fud = fudPhoto.FileName;
            if (txtName.Text.Trim() != "" && txtMobileNumber.Text.Trim() != "" && txtEmailId.Text.Trim() != "" && txtAddress.Text.Trim() != "" && ddlDay.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && ddlYear.SelectedIndex != 0 && fud != "")
            {
                //DateTime date = Convert.ToDateTime(txtDateofBirth.Text.Trim());
                //string d2 = date.ToString("MM/dd/yyyy");

                Admin_User objAdmin_User = new Admin_User();

                objAdmin_User.Address = txtAddress.Text.Trim();
                objAdmin_User.EmailId = txtEmailId.Text.Trim();
                objAdmin_User.Mobile = txtMobileNumber.Text.Trim();
                objAdmin_User.Name = txtName.Text.Trim();
                objAdmin_User.DOB = Convert.ToDateTime(ddlMonth.SelectedValue.ToString() + "/" + ddlDay.SelectedValue.ToString() + "/" + ddlYear.SelectedItem.ToString());
                objAdmin_User.RegisterId = Convert.ToInt32(lblRegisterId.Text.Trim());
                objAdmin_User.DumpEmailId = lblDumpEmailId.Text.Trim();
                objAdmin_User.CityId = Convert.ToInt32(ddlCity.SelectedValue.ToString());
                objAdmin_User.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objAdmin_User.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                objAdmin_User.Gender = rdbMale.Checked ? "Male" : "Female";

                if (fud != "")
                    objAdmin_User.Img = "Img";
                else
                    objAdmin_User.Img = "";

                DataSet objDataSet = Admin_User.Send_User_To_DB(objAdmin_User);
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][1].ToString()) == 1)
                {
                    if (fud != "")
                        fudPhoto.SaveAs(Server.MapPath("~/User_Photos/") + "Img_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    ShowNotification("Profile", "Inserted Successfully..", NotificationType.success);
                    User_Profile();
                    Display_List(gvUserlist, "SELECT Register_Id,Name,Mobile_No,EmailId,Address,Isactive,(Select City_Name from tbl_admin_city C where C.City_Id=RU.City_Id)as City_Name,Photo,convert(varchar(12),DOB,103)as DOB,(Select Username from tbl_user U where U.Staff_Id=RU.Register_Id)as Username,(Select Password from tbl_user U where U.Staff_Id=RU.Register_Id)as Password FROM tbl_Register_User RU order by Register_Id desc");
                }
                else if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][1].ToString()) == -5)
                {
                    ShowNotification("Profile", "EmailId is already existed..", NotificationType.error);
                    txtEmailId.Focus();
                }
            }
            else
            {
                ShowNotification("Profile", "Please fill all fields..!", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            User_Profile();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
            DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
        }
        catch (Exception Ex)
        {

        }
    }
}
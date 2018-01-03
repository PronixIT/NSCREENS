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

public partial class Admin_frmProfile : System.Web.UI.Page
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
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,Photo,City_Id,(Select District_Id from tbl_admin_city C where C.City_Id=U.City_Id)as District_Id,(Select (select State_Id from tbl_admin_district AD where AD.District_Id=C.District_Id) from tbl_admin_city C where C.City_Id=U.City_Id)as State_Id from tbl_Register_User U where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                ddlState.ClearSelection();
                if (ddlState.Items.FindByValue(objDataSet.Tables[0].Rows[0]["State_Id"].ToString()) != null)
                    ddlState.Items.FindByValue(objDataSet.Tables[0].Rows[0]["State_Id"].ToString()).Selected = true;

                DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByValue(objDataSet.Tables[0].Rows[0]["District_Id"].ToString()) != null)
                    ddlDistrict.Items.FindByValue(objDataSet.Tables[0].Rows[0]["District_Id"].ToString()).Selected = true;

                DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
                ddlCity.ClearSelection();
                if (ddlCity.Items.FindByValue(objDataSet.Tables[0].Rows[0]["City_Id"].ToString()) != null)
                    ddlCity.Items.FindByValue(objDataSet.Tables[0].Rows[0]["City_Id"].ToString()).Selected = true;

                txtName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                txtMobileNumber.Text = objDataSet.Tables[0].Rows[0]["Mobile_No"].ToString();
                txtAddress.Text = objDataSet.Tables[0].Rows[0]["Address"].ToString();
                txtEmailId.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                lblRegisterId.Text = objDataSet.Tables[0].Rows[0]["Register_Id"].ToString();
                lblDumpEmailId.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                if (objDataSet.Tables[0].Rows[0]["DOB"].ToString() != "")
                    txtDateofBirth.Text = Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["DOB"].ToString()).ToString("yyyy-MM-dd");
                imgPhoto.ImageUrl = "~/User_Photos/" + objDataSet.Tables[0].Rows[0]["Photo"].ToString();
            }
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

                DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' Order by State_Name", "Select");
                DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
                DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
                User_Profile();
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
            if (txtName.Text.Trim() != "" && txtMobileNumber.Text.Trim() != "" && txtEmailId.Text.Trim() != "" && txtAddress.Text.Trim() != "" && txtDateofBirth.Text.Trim() != "" && fud != "")
            {
                Admin_User objAdmin_User = new Admin_User();

                objAdmin_User.Address = txtAddress.Text.Trim();
                objAdmin_User.EmailId = txtEmailId.Text.Trim();
                objAdmin_User.Mobile = txtMobileNumber.Text.Trim();
                objAdmin_User.Name = txtName.Text.Trim();
                objAdmin_User.DOB = Convert.ToDateTime(txtDateofBirth.Text.Trim());
                objAdmin_User.RegisterId = Convert.ToInt32(lblRegisterId.Text.Trim());
                objAdmin_User.DumpEmailId = lblDumpEmailId.Text.Trim();
                objAdmin_User.CityId = Convert.ToInt32(ddlCity.SelectedValue.ToString());
                objAdmin_User.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objAdmin_User.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);

                if (fud != "")
                    objAdmin_User.Img = "Img";
                else
                    objAdmin_User.Img = "";

                DataSet objDataSet = Admin_User.Send_User_To_DB(objAdmin_User);
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                {
                    if (fud != "")
                        fudPhoto.SaveAs(Server.MapPath("~/User_Photos/") + "Img_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    ShowNotification("Profile", "Updated Successfully..", NotificationType.success);
                    User_Profile();
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmBlockUser : System.Web.UI.Page
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
                Display_List(gvUserlist, "SELECT Register_Id,Name,Mobile_No,EmailId,Address,Isactive,(Select City_Name from tbl_admin_city C where C.City_Id=RU.City_Id)as City_Name,Photo,convert(varchar(12),DOB,103)as DOB,(Select Username from tbl_user U where U.Staff_Id=RU.Register_Id)as Username,(Select Password from tbl_user U where U.Staff_Id=RU.Register_Id)as Password FROM tbl_Register_User RU where Isactive='True' order by Register_Id desc");
            }
        }
        catch(Exception Ex)
        {
            
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            string AddColum = "";

            if (txtSearchName.Text != "")
            {
                AddColum = AddColum + " Name Like '" + txtSearchName.Text.Trim() + "%' and ";
            }

            if (AddColum != "")
            {
                AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                Display_List(gvUserlist, "SELECT Register_Id,Name,Mobile_No,EmailId,Address,Isactive,(Select City_Name from tbl_admin_city C where C.City_Id=RU.City_Id)as City_Name,Photo,convert(varchar(12),DOB,103)as DOB,(Select Username from tbl_user U where U.Staff_Id=RU.Register_Id)as Username,(Select Password from tbl_user U where U.Staff_Id=RU.Register_Id)as Password FROM tbl_Register_User RU where Isactive='True' and " + AddColum + " Order by Register_Id desc");
            }
            else
            {
                Display_List(gvUserlist, "SELECT Register_Id,Name,Mobile_No,EmailId,Address,Isactive,(Select City_Name from tbl_admin_city C where C.City_Id=RU.City_Id)as City_Name,Photo,convert(varchar(12),DOB,103)as DOB,(Select Username from tbl_user U where U.Staff_Id=RU.Register_Id)as Username,(Select Password from tbl_user U where U.Staff_Id=RU.Register_Id)as Password FROM tbl_Register_User RU where Isactive='True' Order by Register_Id desc");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void chkBlock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //DataSet objDataSet = MasterCode.RetrieveQuery("Update ");
            LinkButton chk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)chk.NamingContainer;

            DataSet objDataSet = MasterCode.ExcuteOneParameter((Row.FindControl("lblGridRegister_Id") as Label).Text, "Sp_Delete_User", "");
            if(objDataSet.Tables[0].Rows[0][0].ToString()=="1")
            {
                ShowNotification("Block User", "User Blocked Successfully..", NotificationType.success);
                Display_List(gvUserlist, "SELECT Register_Id,Name,Mobile_No,EmailId,Address,Isactive,(Select City_Name from tbl_admin_city C where C.City_Id=RU.City_Id)as City_Name,Photo,convert(varchar(12),DOB,103)as DOB,(Select Username from tbl_user U where U.Staff_Id=RU.Register_Id)as Username,(Select Password from tbl_user U where U.Staff_Id=RU.Register_Id)as Password FROM tbl_Register_User RU where Isactive='True' order by Register_Id desc");
            }
        }
        catch(Exception Ex)
        {

        }
    }
}
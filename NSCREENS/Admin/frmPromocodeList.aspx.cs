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

public partial class Admin_frmPromocodeList : System.Web.UI.Page
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

    public void CheckBoxList(ListBox cbl, string DataValueField, string DataTextField, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                cbl.DataSource = objDataSet;
                cbl.DataValueField = DataValueField;
                cbl.DataTextField = DataTextField;
                cbl.DataBind();
            }
            else
            {
                cbl.DataSource = "";
                cbl.DataBind();
            }
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

    public void Display_List(ListView lst, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                lst.DataSource = objDataSet;
                lst.DataBind();
            }
            else
            {
                if (lst == lstAllPromoters)
                {
                    Display_List(lstAllPromoters, "Select null as Register_Id ,'' as Name,'' as Mobile_No,'' as EmailId,'' as Address,'' as DOB,'' as PromoCode,'' as Photo,'' as MainPhoto");
                    //lstAllPromoters.Visible = false;
                    lstAllPromoters.Attributes.Add("CssClass","hide");
                }

                lst.DataSource = "";
                lst.DataBind();
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

                lstAllPromoters.Visible = true;
                Display_List(lstAllPromoters, "Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=R.Promo_CityId)as City_Name from tbl_Register_User R where Isactive='true' and PromoCode is not null and Register_Id in (Select Staff_Id from tbl_user U where U.User_Id='" + Session["UserId"].ToString() + "') order by Name");
                Display_List(lstRecentVideos, "Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=R.Promo_CityId)as City_Name from tbl_Register_User R where Isactive='true' and PromoCode is not null and Register_Id not in (Select Staff_Id from tbl_user U where U.User_Id='" + Session["UserId"].ToString() + "') order by Name");

                DropDownList(ddlState, "State_Id", "State_Name", "Select State_Id,State_Name from tbl_admin_state where Isactive='true' Order by State_Name", "All States");
                DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='true' and State_Id='"+ddlState.SelectedValue.ToString()+"' order by District_Name", "All Districts");
                DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);

        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AddColoums = "";

            if (txtSearch.Text != "")
                AddColoums = AddColoums + " Name Like '" + txtSearch.Text + "%' and ";
            if (ddlLocation.SelectedIndex != 0)
                AddColoums = AddColoums + " Promo_CityId=" + ddlLocation.SelectedValue.ToString() + " and ";
            if (ddlDistrict.SelectedIndex != 0)
                AddColoums = AddColoums + " Promo_CityId in (Select City_Id from tbl_Admin_city where District_Id=" + ddlDistrict.SelectedValue.ToString() + ") and ";
            if (ddlDistrict.SelectedIndex != 0)
                AddColoums = AddColoums + " Promo_CityId in (Select City_Id from tbl_Admin_city where District_Id in (Select District_Id from tbl_admin_district where State_Id="+ddlState.SelectedValue.ToString()+"))  and ";

            //Display_List(lstRecentVideos, "Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=R.City_Id)as City_Name from tbl_Register_User R where Isactive='true' and PromoCode is not null and Name='" + txtSearch.Text.ToString() + "' and Register_Id not in (Select Staff_Id from tbl_user U where U.User_Id='" + Session["UserId"].ToString() + "') order by Name");
            if (AddColoums != "")
            {
                AddColoums = AddColoums.Remove(AddColoums.Length - 4, 4);
                //Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo from tbl_Artist_Details where Isactive='true' and Interest_Areas='" + Artist_Id + "' and " + AddColoums + " order by Name");
                Display_List(lstRecentVideos, "Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=R.Promo_CityId)as City_Name from tbl_Register_User R where Isactive='true' and PromoCode is not null and Register_Id not in (Select Staff_Id from tbl_user U where U.User_Id='" + Session["UserId"].ToString() + "') and " + AddColoums + " order by Name");
            }

        }
        catch (Exception Ex)
        {

        }
    }
    protected void lstRecentVideos_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Display")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=ru.Promo_CityId)as City_Name,(Select (Select District_Name from tbl_admin_district D where D.District_Id=C.District_Id) from tbl_admin_City C where C.City_Id=ru.Promo_CityId)as District_Name,(Select (Select (Select State_Name from tbl_admin_state S where S.State_Id=D.State_Id) from tbl_admin_district D where D.District_Id=C.District_Id) from tbl_admin_City C where C.City_Id=ru.Promo_CityId)as State_Name,Contact_info,Promo_Img from tbl_Register_User ru jOIN tbl_user U ON  U.Staff_Id!=RU.Register_Id where RU.Isactive='true' and PromoCode is not null and U.User_Id='" + Session["UserId"].ToString() + "' AND Register_Id=" + (e.Item.FindControl("lblArtistId") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    imgPhoto.ImageUrl = objDataSet.Tables[0].Rows[0]["Photo"].ToString();
                    lblName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                    lblMobile_No.Text = objDataSet.Tables[0].Rows[0]["Contact_info"].ToString();
                    txtState.Text = objDataSet.Tables[0].Rows[0]["State_Name"].ToString();
                    txtDistrict.Text = objDataSet.Tables[0].Rows[0]["District_Name"].ToString();
                    lblEmailId.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                    lblAddress.Text = objDataSet.Tables[0].Rows[0]["Address"].ToString();
                    txtCity.Text = objDataSet.Tables[0].Rows[0]["City_Name"].ToString();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);


            }
        }
        catch (Exception Ex)
        {

        }
    }
    protected void Upload(object sender, EventArgs e)
    {
        try
        {
            string[] SPlit = (lblIndex.Text.Trim()).Split('_');

            if (SPlit.Length > 0)
                (lstAllPromoters.Items[Convert.ToInt32(SPlit[SPlit.Length - 1])].FindControl("fup") as FileUpload).SaveAs(Server.MapPath("~/User_Photos/" + (lstAllPromoters.Items[Convert.ToInt32(SPlit[SPlit.Length - 1])].FindControl("lblImage") as Label).Text));
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='true' and State_Id='" + ddlState.SelectedValue.ToString() + "' order by District_Name", "All Districts");
            DropDownList(ddlLocation, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
        }
        catch(Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender,EventArgs e)
    {
        try
        {
            string fud = fupImage.FileName;

            if (ddlPromoCity.SelectedIndex != 0 && txtContactinfo.Text.Trim() != "")
            {
                DataSet objDataSet1 = MasterCode.RetrieveQuery("Select Photo from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_User where User_Id=" + Session["UserId"].ToString() + ")");
                if (objDataSet1.Tables[0].Rows.Count > 0)
                {
                    if (fud != "")
                        fupImage.SaveAs(Server.MapPath("~/User_Photos/") + objDataSet1.Tables[0].Rows[0]["Photo"].ToString());

                    DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Register_User set Contact_info='" + txtContactinfo.Text.Trim() + "',Promo_CityId=" + ddlPromoCity.SelectedValue.ToString() + " where Register_Id in (Select Staff_Id from tbl_user where User_Id='" + Convert.ToInt32(Session["UserId"].ToString()) + "')");
                    Display_List(lstAllPromoters, "Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=R.Promo_CityId)as City_Name from tbl_Register_User R where Isactive='true' and PromoCode is not null and Register_Id in (Select Staff_Id from tbl_user U where U.User_Id='" + Session["UserId"].ToString() + "') order by Name");
                    ShowNotification("Promoter", "Updated Successfully..", NotificationType.success);
                }
            }
            else
            {
                ShowNotification("Promoter", "Please Select Image and City..", NotificationType.error);
            }
        }
        catch(Exception Ex)
        {

        }
    }

    protected void lstAllPromoters_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,PromoCode,'../User_Photos/'+Photo as Photo,Photo as MainPhoto,(Select City_Name from tbl_admin_City C where C.City_Id=ru.Promo_CityId)as City_Name,(Select (Select District_Name from tbl_admin_district D where D.District_Id=C.District_Id) from tbl_admin_City C where C.City_Id=ru.Promo_CityId)as District_Name,(Select (Select (Select State_Name from tbl_admin_state S where S.State_Id=D.State_Id) from tbl_admin_district D where D.District_Id=C.District_Id) from tbl_admin_City C where C.City_Id=ru.Promo_CityId)as State_Name,Contact_info,Promo_Img from tbl_Register_User ru jOIN tbl_user U ON  U.Staff_Id=RU.Register_Id where RU.Isactive='true' and PromoCode is not null and U.User_Id='" + Session["UserId"].ToString() + "'");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                DropDownList(ddlPromoState, "State_Id", "State_Name", "Select State_Id,State_Name from tbl_admin_state where Isactive='true' Order by State_Name", "All States");
                if (ddlPromoState.Items.FindByText(objDataSet.Tables[0].Rows[0]["State_Name"].ToString()) != null)
                    ddlPromoState.Items.FindByText(objDataSet.Tables[0].Rows[0]["State_Name"].ToString()).Selected = true;

                DropDownList(ddlPromoDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='true' and State_Id='" + ddlPromoState.SelectedValue.ToString() + "' order by District_Name", "All Districts");
                if (ddlPromoDistrict.Items.FindByText(objDataSet.Tables[0].Rows[0]["District_Name"].ToString()) != null)
                    ddlPromoDistrict.Items.FindByText(objDataSet.Tables[0].Rows[0]["District_Name"].ToString()).Selected = true;

                DropDownList(ddlPromoCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlPromoDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
                if (ddlPromoCity.Items.FindByText(objDataSet.Tables[0].Rows[0]["City_Name"].ToString()) != null)
                    ddlPromoCity.Items.FindByText(objDataSet.Tables[0].Rows[0]["City_Name"].ToString()).Selected = true;

                imgpromo.ImageUrl = objDataSet.Tables[0].Rows[0]["Photo"].ToString();
                txtpromoName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                txtContactinfo.Text = objDataSet.Tables[0].Rows[0]["Contact_info"].ToString();
                txtState.Text = objDataSet.Tables[0].Rows[0]["State_Name"].ToString();
                txtDistrict.Text = objDataSet.Tables[0].Rows[0]["District_Name"].ToString();
                lblEmailId.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                lblAddress.Text = objDataSet.Tables[0].Rows[0]["Address"].ToString();
                txtCity.Text = objDataSet.Tables[0].Rows[0]["City_Name"].ToString();
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupEdit()", true);
        }
        catch(Exception Ex)
        {

        }
    }

    protected void ddlPromoDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlPromoCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlPromoDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void ddlPromoState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlPromoDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='true' and State_Id='" + ddlPromoState.SelectedValue.ToString() + "' order by District_Name", "All Districts");
            DropDownList(ddlPromoCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' and District_Id=" + ddlPromoDistrict.SelectedValue.ToString() + " Order by City_Name", "All Locations");
        }
        catch (Exception Ex)
        {

        }
    }
}
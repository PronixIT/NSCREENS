using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomerName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Select Title from tbl_Short_film where Status='Approve' and Publish='true' and Title Like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> Title = new List<string>();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                    while (sdr.Read())
                        Title.Add(sdr["Title"].ToString());

                conn.Close();
                return Title;
            }
        }
    }

    public string master_lblUrl
    {
        get { return this.lblUrl.Text; }
        set { this.lblUrl.Text = value; }
    }

    public string SearchBox
    {
        get { return txtSearch1.Text; }
        set { txtSearch1.Text = value; }
    }

    public string PromoCode
    {
        get { return lblCode.Text; }
        set { lblCode.Text = value; }
    }

    public string Amount
    {
        get { return lblAmount.Text; }
        set { lblAmount.Text = value; }
    }

    public string EarningAmount
    {
        get { return lblEarningMoney.Text; }
        set { lblEarningMoney.Text = value; }
    }

    public void Button_Status()
    {
        if (Session["UserId"].ToString() != "1")
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select PromoCode from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id='" + Convert.ToInt32(Session["UserId"].ToString()) + "')");
            if (objDataSet.Tables[0].Rows[0][0].ToString() == "")
            {
                btnCreate.Visible = true;
                lblCode.Text = "";
                lblCode.Visible = false;
            }
            else
            {
                btnCreate.Visible = false;
                lblCode.Text = "Promoter Code: " + objDataSet.Tables[0].Rows[0]["PromoCode"].ToString();
                lblCode.Visible = true;
            }
        }
        else
        {
            btnCreate.Visible = false;
            lblCode.Text = "";
            lblCode.Visible = false;
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
            if (Session["UserName"] != null && Session["Menu"] != null)
            {
                if (!IsPostBack)
                {
                    //string shortfilm = Request.QueryString["shortfilm"];
                    //if (shortfilm != null)
                    //{

                    //}

                    DataSet objDataSetLogin = MasterCode.ExcuteOneParameter(lblUrl.Text, "Sp_Find_UserForm", Session["UserName"].ToString());
                    if (objDataSetLogin.Tables[0].Rows.Count > 0)
                    {
                        lt_Menu.Text = Session["Menu"].ToString();

                        lblUsername.Text = "Hi " + Session["Name"].ToString();

                        Button_Status();

                        DataSet objDataSetIn = MasterCode.RetrieveQuery("Select COUNT(*) from tbl_SendMessage where IsRead='false' and SendTo in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                        if (Convert.ToInt32(objDataSetIn.Tables[0].Rows[0][0].ToString()) > 0)
                        {
                            lblInbox.Text = objDataSetIn.Tables[0].Rows[0][0].ToString();

                            //Add.Attributes.Add("class", "pull-right inbox");
                        }
                        else
                        {
                            lblEarningMoney.Text = "0";
                            //Add.Attributes.Add("class", "");
                        }

                        if (Session["UserName"].ToString() == "admin")
                        {
                            //DataSet objDataSetE = MasterCode.RetrieveQuery("Select (Select case when SUM(Promoter_Budget) is null then 0 else SUM(Promoter_Budget) end +case when SUM(User_Budget) is null then 0 else SUM(User_Budget) end from tbl_Short_Film_Visits where Short_Film_Id in (Select Lan_Short_film_Id from tbl_Language_Short_FilmId where CreatedById=" + Session["UserId"].ToString() + "))+(Select case when SUM(Video_Sharing) is null then 0 else SUM(Video_Sharing) end from tbl_Short_Film_Visits where User_Share_Id=" + Session["UserId"].ToString() + ")+(Select case when sum(User_Budget) is null then 0 else sum(User_Budget) end +case when sum(Promoter_Budget) is null then 0 else sum(Promoter_Budget) end from tbl_Advertizment_Visits where Advertisement_Id in (Select Advertisement_Id from tbl_Advertisement where CreatedById=" + Session["UserId"].ToString() + "))");
                            DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                            if (objDataSetE.Tables[0].Rows.Count > 0)
                                lblEarningMoney.Text = "( " + objDataSetE.Tables[0].Rows[0][0].ToString() + " )";
                            else
                                lblEarningMoney.Text = "0.00";
                        }
                        else
                        {
                            DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                            if (objDataSetE.Tables[0].Rows.Count > 0)
                                lblEarningMoney.Text = "( " + objDataSetE.Tables[0].Rows[0][0].ToString() + " )";
                            else
                                lblEarningMoney.Text = "0.00";
                        }

                        DataSet objDataSet1234 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                        if (objDataSet1234.Tables[0].Rows.Count > 0)
                            lblAmount.Text = objDataSet1234.Tables[0].Rows[0][0].ToString();
                        else
                            lblAmount.Text = "0.00";
                    }
                    else
                    {
                        Session["LoginId"] = null;
                        Response.Redirect("~/Login.aspx", false);
                    }
                }
            }
            else
            {
                btnCreate.Visible = false;
                lblCode.Text = "";
                lblCode.Visible = false;

                Session["RedirectURL"] = Request.Url.AbsoluteUri;

                Response.Redirect("~/Login.aspx", false);
            }

            HtmlMeta keywords4 = new HtmlMeta();
            keywords4.Attributes.Add("property", "fb:app_id");
            keywords4.Content = "566086956918274";
            MetaPlaceHolder.Controls.Add(keywords4);

            HtmlMeta keywords3 = new HtmlMeta();
            keywords3.Attributes.Add("property", "og:image");
            keywords3.Content = Session["Short_film_Image"].ToString();
            MetaPlaceHolder.Controls.Add(keywords3);

            HtmlMeta keywords2 = new HtmlMeta();
            keywords2.Attributes.Add("property", "og:description");
            keywords2.Content = Session["Description"].ToString();
            MetaPlaceHolder.Controls.Add(keywords2);

            HtmlMeta keywords12 = new HtmlMeta();
            keywords12.Attributes.Add("property", "og:title");
            keywords12.Content = Session["Title"].ToString();
            MetaPlaceHolder.Controls.Add(keywords12);

            HtmlMeta keywords = new HtmlMeta();
            keywords.Attributes.Add("property", "og:url");
            keywords.Content = Session["URL"].ToString();
            MetaPlaceHolder.Controls.Add(keywords);

            HtmlMeta keywords1 = new HtmlMeta();
            keywords1.Attributes.Add("property", "og:type");
            keywords1.Content = "video.movie";
            MetaPlaceHolder.Controls.Add(keywords1);

        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' Order by State_Name", "Select");
            DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
            DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalScript", "MasterPagedivPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender,EventArgs e)
    {
        try
        {
            string fud = fupImage.FileName;

            if (fud != ""&&ddlCity.SelectedIndex!=0&&txtContactinfo.Text.Trim()!="")
            {
                DataSet objDataSet1 = MasterCode.RetrieveQuery("Select Photo from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_User where User_Id=" + Session["UserId"].ToString() + ")");
                if (objDataSet1.Tables[0].Rows.Count > 0)
                {
                    if (fud != "")
                        fupImage.SaveAs(Server.MapPath("~/User_Photos/") + objDataSet1.Tables[0].Rows[0]["Photo"].ToString());

                    int length = 6;
                    const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    StringBuilder res = new StringBuilder();
                    Random rnd = new Random();
                    while (0 < length--)
                    {
                        res.Append(valid[rnd.Next(valid.Length)]);
                    }

                    DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Register_User set Contact_info='" + txtContactinfo.Text.Trim() + "',Promo_CityId=" + ddlCity.SelectedValue.ToString() + ",PromoCode='" + res.ToString() + "' where Register_Id in (Select Staff_Id from tbl_user where User_Id='" + Convert.ToInt32(Session["UserId"].ToString()) + "')");
                    ShowNotification("Promo Code", " Created Successfully..", NotificationType.success);
                    Button_Status();
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

    protected void lnk_Click(object sender, EventArgs e)
    {
        try
        {
            Session["UserId"] = null;
            Session["UserCode"] = null;
            Session["UserName"] = null;
            Session["Menu"] = null;

            Response.Redirect("../Login.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Session["SearchFilm"] = txtSearch1.Text.Trim();

            Response.Redirect("frmHome.aspx", false);
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

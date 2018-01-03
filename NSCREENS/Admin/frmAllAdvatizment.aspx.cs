using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmHome : System.Web.UI.Page
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

    public void Display_List(string ShortFilmId, ListView lst, string Query)
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
                lst.DataSource = "";
                lst.DataBind();

                string Gender = "", DOB = "";
                string CityId = "";
                DataSet objDataSet112 = MasterCode.RetrieveQuery("Select Gender,DATEDIFF(hour,DOB,GETDATE())/8766 AS AgeYearsIntTrunc,City_Id from tbl_Register_User R join tbl_user U on R.Register_Id=U.Staff_Id where U.Username='" + Session["UserName"] + "'");
                if (objDataSet112.Tables[0].Rows.Count > 0)
                {
                    Gender = objDataSet112.Tables[0].Rows[0][0].ToString();
                    DOB = objDataSet112.Tables[0].Rows[0][1].ToString();
                    CityId = objDataSet.Tables[0].Rows[0][2].ToString();
                }

                Display_List(ShortFilmId, lstRecentVideos, "Select Description,Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max))+'&shortfilm=" + ShortFilmId + "' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image from tbl_Advertisement where Isactive='True' and Status='Approve' and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and ShortFilmId=',' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and City_Id Like '%" + CityId + "%' and Advertisement_Id not in (select Advertisement_Id from tbl_Advertizment_Visits where User_Id=" + Session["UserId"].ToString() + ") Order by Advertisement_Id desc");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void Districts_List1(GridView gv, string Query)
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
                DropDownList(ddloperator, "OperatorCode", "OperatorName", "Select OperatorCode,OperatorName from tbl_operator where Isactive='true' and RechargeType='Prepaid' Order by OperatorName", "Select");

                decimal WalletAmt = 0;

                DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                if (objDataSetE.Tables[0].Rows.Count > 0)
                    WalletAmt = Convert.ToDecimal(objDataSetE.Tables[0].Rows[0][0].ToString());
                else
                    WalletAmt = 0;

                txtEarningAmt.Text = WalletAmt.ToString();
                lblDumpEarningAmt.Text = WalletAmt.ToString();
                Districts_List1(gdvHistory, "SELECT R.ID,convert(varchar(10),Date,100)as Date,MobileNo,Amount,Code,OperatorName,R.CreatedDate,R.CreatedById,R.ModifiedDate,R.ModifiedById,R.Isactive,Status,PrevBalance,Balance,ResponseCode,ClientTXT,Area FROM Request R join tbl_operator O on R.Code=O.OperatorCode where R.Isactive='true' order by R.ID");
                Districts_List1(gdvoffers, "SELECT top 20 Offer_Id,Area,Operator_Code,OperatorName,O.RechargeType,Amount,TT_Type,Days,Description,O.CreatedDate,O.CreatedById,O.ModifiedDate,O.ModifiedById,O.Isactive FROM tbl_offers O join tbl_operator T on O.Operator_Code=T.OperatorCode where O.Isactive='true' order by Offer_Id");

                Districts_List1(gdvTran, "Select t.Id,Form_Name as Page,PageType,Transaction_Id,Debit,Credit,convert(varchar(20),CreatedDate,100) as Date from tbl_transactions t JOIN tbl_page P on  P.Id=t.Page_Id and CreatedById=" + Session["UserId"].ToString()+" order by Id desc");


                //string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (string.IsNullOrEmpty(ipAddress))
                //{
                //    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                //}

                //string APIKey = "81dce31671738f9a3fdf05ee98c9a869dc78242e9603381233b3e663f0fcca3d";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, ipAddress);
                //using (WebClient client = new WebClient())
                //{
                //    string json = client.DownloadString(url);
                //    Location location = new JavaScriptSerializer().Deserialize<Location>(json);
                //    DataSet objDataSet = MasterCode.RetrieveQuery("Select City_Id from tbl_admin_city where City_Name='" + location.CityName + "'");
                //    if (objDataSet.Tables[0].Rows.Count > 0)
                //        CityId = objDataSet.Tables[0].Rows[0][0].ToString();
                //    //List<Location> locations = new List<Location>();
                //    //locations.Add(location);
                //    //gvLocation.DataSource = locations;
                //    //gvLocation.DataBind();
                //}

                //Display_List(lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?/'+cast(Advertisement_Id as varchar(max))+'/shortfilm' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image from tbl_Advertisement where Isactive='True' and Status='Approve' and City_Id Like '%" + CityId + "%' Order by Advertisement_Id desc");

                string Gender = "", DOB = "";
                string CityId = "";

                DataSet objDataSet = MasterCode.RetrieveQuery("Select Gender,DATEDIFF(hour,DOB,GETDATE())/8766 AS AgeYearsIntTrunc,City_Id from tbl_Register_User R join tbl_user U on R.Register_Id=U.Staff_Id where U.Username='" + Session["UserName"] + "'");
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    Gender = objDataSet.Tables[0].Rows[0][0].ToString();
                    DOB = objDataSet.Tables[0].Rows[0][1].ToString();
                    CityId = objDataSet.Tables[0].Rows[0][2].ToString();
                }

                string ShortId = Request.QueryString["shortfilm"];
                //if (ShortId != null)
                //    Display_List(ShortId, lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max))+'&shortfilm=" + ShortId + "' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Description from tbl_Advertisement where Isactive='True' and Status='Approve' and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and  ShortFilmId Like '%," + ShortId + ",%' Order by Advertisement_Id desc");
                //else
                //    ShowNotification("Advertisement", "Please Select any Short Film..!", NotificationType.error);

                if (ShortId == null)
                    Display_List(ShortId, lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max))+'&shortfilm=" + ShortId + "' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Description from tbl_Advertisement where Isactive='True' and Status='Approve' and City_Id Like '%" + CityId + "%' and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and Advertisement_Id not in (select Advertisement_Id from tbl_Advertizment_Visits where User_Id=" + Session["UserId"].ToString() + ") Order by Advertisement_Id desc");
                else
                {
                    ShowNotification("Advertisement", "Your wallet amount is not sufficient,Do any of the below processes to refill the wallet", NotificationType.success);
                    Display_List(ShortId, lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max))+'&shortfilm=" + ShortId + "' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Description from tbl_Advertisement where Isactive='True' and Status='Approve' and City_Id Like '%" + CityId + "%' and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and  ShortFilmId Like '%," + ShortId + ",%' and Advertisement_Id not in (select Advertisement_Id from tbl_Advertizment_Visits where User_Id=" + Session["UserId"].ToString() + ") Order by Advertisement_Id desc");
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }

    protected void play_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            ListViewItem lst = (ListViewItem)lnk.NamingContainer;

            string Short = Request.QueryString["shortfilm"];
            string ShrUserId = Request.QueryString["userId"];
            if (Short == null)
            {
                ShowNotification("Advertisement", "Please Select Short Film..!", NotificationType.error);
            }
            else
            {
                if (ShrUserId == null)
                    Response.Redirect((lst.FindControl("lblURLPlay") as Label).Text, false);
                else
                    Response.Redirect((lst.FindControl("lblURLPlay") as Label).Text + "&userId=" + ShrUserId, false);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    #region Recharge

    public void rdbpaid_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ddloperator.ClearSelection();
            if (rdbprepaid.Checked)
                DropDownList(ddloperator, "OperatorCode", "OperatorName", "Select OperatorCode,OperatorName from tbl_operator where Isactive='true' and RechargeType='Prepaid' Order by OperatorName", "Select");
            else if (rdbPostpaid.Checked)
                DropDownList(ddloperator, "OperatorCode", "OperatorName", "Select OperatorCode,OperatorName from tbl_operator where Isactive='true' and RechargeType='PostPaid' Order by OperatorName", "Select");
            else if (rdbDTH.Checked)
                DropDownList(ddloperator, "OperatorCode", "OperatorName", "Select OperatorCode,OperatorName from tbl_operator where Isactive='true' and RechargeType='DTH' Order by OperatorName", "Select");
        }
        catch (Exception Ex)
        {

        }
    }

    public void clear()
    {

        txtAmount.Text = "";
        txtMobileNumber.Text = "";
        ddloperator.SelectedIndex = 0;
    }

    protected void btn_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtMobileNumber.Text.Trim() != "" && ddloperator.SelectedIndex != 0 && txtAmount.Text.Trim() != "")
            {
                if (Convert.ToDecimal(Master.Amount) >= Convert.ToDecimal(txtAmount.Text.Trim()))
                {
                    Recharge objRecharge = new Recharge();

                    objRecharge.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    objRecharge.MobileNo = txtMobileNumber.Text.Trim();
                    objRecharge.OperatorCode = ddloperator.SelectedValue.ToString();
                    objRecharge.CreatedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt");
                    objRecharge.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objRecharge.Area = ddlArea.SelectedItem.ToString();

                    DataSet objDataSet = Recharge.Send_Data_To_DB(objRecharge);
                    if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        string output;
                        output = Recharge.Recharge_Bal(txtMobileNumber.Text.Trim(), Convert.ToDecimal(txtAmount.Text.Trim()), ddloperator.SelectedValue.ToString(), objDataSet.Tables[0].Rows[0][0].ToString());

                        if (output == "1200")
                        {
                            ShowNotification("Recharge", "Recharged Successfully..", NotificationType.success);
                            Master.Amount = (Convert.ToDecimal(Master.Amount) - Convert.ToDecimal(txtAmount.Text.Trim())).ToString();

                        }
                        else
                        {
                            ShowNotification("Recharge", "ohh Some isue...", NotificationType.error);
                            Master.Amount = (Convert.ToDecimal(Master.Amount) - Convert.ToDecimal(txtAmount.Text.Trim())).ToString();
                        }


                        clear();
                    }
                }
                else
                {
                    ShowNotification("Recharge", "Invalid Amount..!", NotificationType.error);
                    txtAmount.Focus();
                }
            }
            else
                ShowNotification("Recharge", "Please Fill All Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Recharge", dispErrorMsg, NotificationType.error);

        }

    }

    public void Send_Data(string Ticket_Name, int Ticket_Id, string DumpTicket_Name, bool Isactive, string Subject, string Description, string Priority)
    {
        try
        {
            if (Ticket_Name != "")
            {
            }
            else
                ShowNotification("Ticket", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    //public void rdbrecharge_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        rechargeoffers.Visible = false;
    //        gdvHistory.Visible = false;
    //        gdvoffers.Visible = false;
    //        gdvrefillwallet.Visible = false;

    //        if (rdbrefillwallet.Checked)
    //        {
    //            lblType.Text = "Refill Wallet";
    //            gdvrefillwallet.Visible = true;
    //        }
    //        else if (rdboffers.Checked)
    //        {
    //            lblType.Text = "Offers";
    //            gdvoffers.Visible = true;

    //        }
    //        else if (rdbHistory.Checked)
    //        {
    //            lblType.Text = "History";
    //            gdvHistory.Visible = true;

    //        }
    //    }
    //    catch (Exception Ex)
    //    {

    //    }
    //}

    protected void getoffers_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AddColum = "";
        if (ddloperator.SelectedIndex != 0)
            AddColum = AddColum + "T.OperatorCode = '" + ddloperator.SelectedValue.ToString() + "' and ";
        if (ddlArea.SelectedIndex != 0)
            AddColum = AddColum + "Area = '" + ddlArea.SelectedValue.ToString() + "' and ";

        AddColum = AddColum.Remove(AddColum.Length - 4, 4);

        Districts_List1(gdvoffers, "SELECT top 20 Offer_Id,Area,Operator_Code,OperatorName,O.RechargeType,Amount,TT_Type,Days,Description,O.CreatedDate,O.CreatedById,O.ModifiedDate,O.ModifiedById,O.Isactive FROM tbl_offers O join tbl_operator T on O.Operator_Code=T.OperatorCode where O.Isactive='true' and " + AddColum + " order by Offer_Id");



    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTransferAmount.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtTransferAmount.Text.Trim()) <= Convert.ToDecimal(lblDumpEarningAmt.Text.Trim()))
                {
                    DataSet objDataSet = MasterCode.ExcuteOneParameter(txtTransferAmount.Text.Trim(), "Sp_Amount_Transfer", Session["UserId"].ToString());
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Master.Amount = (Convert.ToDecimal(Master.Amount) + Convert.ToDecimal(txtTransferAmount.Text)).ToString();
                        txtTransferAmount.Text = "";
                        //DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Register_User set User_Budget="+txtTransferAmount.Text.Trim()+" where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"] + ")");

                        ShowNotification("Amount Transfer", "Amount Transfer Successfully..", NotificationType.success);

                        decimal WalletAmt = 0;

                        DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                        if (objDataSetE.Tables[0].Rows.Count > 0)
                            WalletAmt = Convert.ToDecimal(objDataSetE.Tables[0].Rows[0][0].ToString());
                        else
                            WalletAmt = 0;


                        Master.EarningAmount = "( " + WalletAmt.ToString() + " )";
                        txtEarningAmt.Text = WalletAmt.ToString();
                        lblDumpEarningAmt.Text = WalletAmt.ToString();

                        string Short = Request.QueryString["shortfilm"];
                        string ShrUserId = Request.QueryString["userId"];
                        if (Short != null)
                        {
                            if (ShrUserId == null)
                                Response.Redirect("frmSingle.aspx?shortfilm=" + Short, false);
                            else
                                Response.Redirect("frmSingle.aspx?shortfilm=" + Short + "&userId=" + ShrUserId, false);
                        }
                    }
                }
                else
                {
                    ShowNotification("Amount Transfer", "Invalid Amount..", NotificationType.error);
                }
            }
            else
            {
                ShowNotification("Amount Transfer", "Please Enter Transfer Amount..", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    #endregion

    protected void btnNextButton_Click(object sender, EventArgs e)
    {
        try
        {
            string ShortId = Request.QueryString["shortfilm"];
            if (ShortId == null)
                Response.Redirect("frmHome.aspx", false);
            else
                Response.Redirect("frmSingle.aspx?shortfilm=" + ShortId, false);
        }
        catch (Exception Ex)
        {

        }
    }
}
using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    #region Private Variables & Classes

    string menu = "";
    MasterCode objMaster = new MasterCode();
    DataSet objDataSet = new DataSet();

    #endregion

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

    #region Public Methods

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }

    private DataTable GetMenuData(string Url)
    {
        try
        {
            //using (SqlConnection con = new SqlConnection(Connection.con))
            //{
            //SqlCommand cmd = new SqlCommand("spMenuItem", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@UserID", Session["UserId"].ToString());
            //DataTable dtMenuItems = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dtMenuItems);
            //cmd.Dispose();
            //sda.Dispose();
            DataSet objDataSet = MasterCode.RetrieveQuery("Select * from tbl_menu where MenuID in (" + Url + ") and Isactive='true'");
            if (objDataSet.Tables[0].Rows.Count > 0)
                return objDataSet.Tables[0];
            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        return null;
    }

    private void AddTopMenuItems(DataTable menuData)
    {
        DataView view = null;
        try
        {
            view = new DataView(menuData);
            view.RowFilter = "ParentID IS NULL";
            menu += "<ul>";
            foreach (DataRowView row in view)
            {
                //Adding the menu item
                MenuItem newMenuItem = new MenuItem(row["Text"].ToString(), row["MenuID"].ToString());
                newMenuItem.NavigateUrl = row["NavigateUrl"].ToString();
                //  menuBar.Items.Add(newMenuItem);
                if (newMenuItem.NavigateUrl == "#")
                {
                    //menu += "<li class='panel panel-default dropdown'><a  data-toggle='collapse' href='#" + row["MenuID"].ToString() + "' href=\"" +
                    //row["NavigateUrl"].ToString() + "\"><span class='" + row["Icon"].ToString() + "'></span><span class='title'>" + row["Text"].ToString() + "</span></a>";

                    menu += "<li><a href=\"" +
                    row["NavigateUrl"].ToString() + "\"><i class='" + row["Icon"].ToString() + "'></i> " + row["Text"].ToString() + "</a>";
                    AddChildMenuItems(menuData, newMenuItem, row["MenuID"].ToString());
                }
                else
                {
                    menu += "<li><a href=\"" +
                    row["NavigateUrl"].ToString() + "\"><i class='" + row["Icon"].ToString() + "'></i> " + row["Text"].ToString() + "</a>";

                }
                menu += "</li>";
            }
            menu += "</ul>";
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            view = null;
        }
    }

    private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem, string Id)
    {
        DataView view = null;
        try
        {
            view = new DataView(menuData);

            view.RowFilter = "ParentID=" + parentMenuItem.Value;

            int RowI = view.Count;

            if (view.Count > 10)
            {
                decimal AddR = view.Count;
                decimal AddColoums = Convert.ToDecimal((AddR) / 10);

                for (decimal i = 0; i < AddColoums; i++)
                {
                    if (parentMenuItem.Value == "501")
                    {
                        if (i == 0)
                            menu += "<ul>";
                        else if (i == 1)
                            menu += "<ul style='right:-210px'>";
                        else if (i == 2)
                            menu += "<ul style='right:-379px'>";
                    }
                    else if (parentMenuItem.Value == "505")
                    {
                        if (i == 0)
                            menu += "<ul>";
                        else if (i == 1)
                            menu += "<ul style='right:-140px'>";
                        else if (i == 2)
                            menu += "<ul style='right:-310px'>";
                    }
                    else
                    {
                        if (i == 0)
                            menu += "<ul>";
                        else if (i == 1)
                            menu += "<ul style='right:-210px'>";
                        else if (i == 2)
                            menu += "<ul style='right:-379px'>";
                    }

                    int TotalCount = 0;

                    foreach (DataRowView row in view)
                    {
                        if (TotalCount < 10)
                        {
                            TotalCount = TotalCount + 1;
                            MenuItem newMenuItem = new MenuItem(row["Text"].ToString(), row["MenuID"].ToString());
                            newMenuItem.NavigateUrl = row["NavigateUrl"].ToString();
                            menu += "<li><a href=\"" +
                                row["NavigateUrl"].ToString() + "\">" +
                                row["Text"].ToString() + "</a>";
                            parentMenuItem.ChildItems.Add(newMenuItem);

                            //AddChildMenuItems(menuData, newMenuItem);
                            menu += "</li>";

                            row.Delete();
                        }
                    }

                    if (TotalCount < 10)
                    {
                        for (int n = 10; n > TotalCount; n--)
                        {
                            MenuItem newMenuItem = new MenuItem("", "");
                            newMenuItem.NavigateUrl = "";
                            menu += "<li><a></a>";
                            parentMenuItem.ChildItems.Add(newMenuItem);
                            menu += "</li>";
                        }
                    }

                    menu += "</ul>";
                }
            }
            else
            {
                menu += "<ul>";

                foreach (DataRowView row in view)
                {
                    MenuItem newMenuItem = new MenuItem(row["Text"].ToString(), row["MenuID"].ToString());
                    newMenuItem.NavigateUrl = row["NavigateUrl"].ToString();
                    menu += "<li><a href=\"" +
                        row["NavigateUrl"].ToString() + "\">" +
                        row["Text"].ToString() + "</a>";
                    parentMenuItem.ChildItems.Add(newMenuItem);

                    //AddChildMenuItems(menuData, newMenuItem);
                    menu += "</li>";
                }
                menu += "</ul>";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            view = null;
        }
    }

    #endregion

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

    public void Send_To_DB()
    {
        try
        {
            Admin_User objAdmin_User = new Admin_User();

            objAdmin_User.Address = txtAddress.Text.Trim();
            objAdmin_User.EmailId = txtEmailId.Text.Trim();
            objAdmin_User.Mobile = txtMobileNumber.Text.Trim();
            objAdmin_User.Name = txtName.Text.Trim();
            objAdmin_User.DOB = Convert.ToDateTime("1999-1-1");
            objAdmin_User.RegisterId = 0;
            objAdmin_User.DumpEmailId = "";
            objAdmin_User.Img = "";
            objAdmin_User.CityId = Convert.ToInt32(ddlCity.SelectedValue.ToString());
            objAdmin_User.UserId = 0;
            objAdmin_User.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);

            DataSet objDataSet12 = Admin_User.Send_User_To_DB(objAdmin_User);
            if (objDataSet12.Tables[0].Rows[0][1].ToString() == "1")
            {
                objDataSet = MasterCode.RetrieveQuery("select User_Id,Isactive,Username,UserCode,UserRights from tbl_user where Username='" + txtUserName.Text.Trim() + "'");

                DataTable menuData = new DataTable();
                menuData = GetMenuData(objDataSet.Tables[0].Rows[0]["UserRights"].ToString());
                AddTopMenuItems(menuData);
                Session["Menu"] = menu.Replace("<ul></ul>", "");
                //if (objDataSet.Tables[0].Rows[0]["User_Id"].ToString() == "1" || objDataSet.Tables[0].Rows[0]["User_Id"].ToString() == "2" || objDataSet.Tables[0].Rows[0]["User_Id"].ToString() == "3")
                if (Session["RedirectURL"] == null)
                    Response.Redirect("Admin/frmHome.aspx", false);
                else
                {
                    Response.Redirect(Session["RedirectURL"].ToString(), false);
                    Session["RedirectURL"] = null;
                }
            }
            else
            {
                ShowNotification("Login", "Sorry! EmailId already exist Please use another one..!", NotificationType.error);
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
                Session["UserId"] = null;
                Session["UserCode"] = null;
                Session["UserName"] = null;
                Session["Menu"] = null;

                int Years = DateTime.Now.AddHours(Connection.SetHours).Year;

                for (int i = 0; i < 80; i++)
                    ddlYear.Items.Insert(i + 1, new ListItem((Years - i).ToString(), (i + 1).ToString()));

                string CityId = "";
                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                }

                string APIKey = "81dce31671738f9a3fdf05ee98c9a869dc78242e9603381233b3e663f0fcca3d";
                string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, ipAddress);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    Location location = new JavaScriptSerializer().Deserialize<Location>(json);
                    DataSet objDataSet = MasterCode.RetrieveQuery("Select City_Id,District_Id,(Select State_Id from tbl_admin_district AD where AD.District_Id=AC.District_Id)as State_Id from tbl_admin_city AC where City_Name='" + location.CityName + "'");
                    if (objDataSet.Tables[0].Rows.Count > 0)
                    {
                        DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' Order by State_Name", "Select");
                        //DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
                        //DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");
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

                        //txtAddress.Text = ddlCity.SelectedItem.ToString();

                        // ShowNotification("Forgot Password", objDataSet.Tables[0].Rows[0]["City_Id"].ToString(), NotificationType.success);

                        //Send_To_DB();

                    }
                    else
                    {
                        DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' Order by State_Name", "Select");
                        DropDownList(ddlDistrict, "District_Id", "District_Name", "Select District_Id,District_Name from tbl_admin_district where Isactive='True' and State_Id=" + ddlState.SelectedValue.ToString() + " Order by District_Name", "Select");
                        DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='True' and District_Id=" + ddlDistrict.SelectedValue + " Order by City_Name", "Select");

                        //ShowNotification("Forgot Password", "Out", NotificationType.success);
                    }
                }

                //HtmlHead headMain = (HtmlHead)Page.Master.FindControl("mainHead");

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

                txtUserName.Focus();
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserName.Text.Trim() != "" && txtPassword.Text != "")
            {
                objDataSet = MasterCode.RetrieveQuery("select User_Id,Isactive,Username,UserCode,UserRights,Staff_Id from tbl_user where Username='" + txtUserName.Text.Trim() + "' and Password='" + txtPassword.Text + "'");

                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    string IsActive = (objDataSet.Tables[0].Rows[0][1].ToString());
                    string UserIP = Request.UserHostAddress;

                    if (IsActive == "True")
                    {
                        DataSet objDataSet1 = null;
                        if (objDataSet.Tables[0].Rows[0]["Username"].ToString() != "admin")
                        {
                            objDataSet1 = MasterCode.RetrieveQuery("Select Name from tbl_Register_User where Register_Id=" + objDataSet.Tables[0].Rows[0]["Staff_Id"].ToString());
                            Session["Name"] = objDataSet1.Tables[0].Rows[0]["Name"].ToString();
                        }
                        else
                            Session["Name"] = "admin";

                        Session["UserId"] = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["User_Id"].ToString());
                        Session["UserCode"] = objDataSet.Tables[0].Rows[0]["Usercode"].ToString();
                        Session["UserName"] = objDataSet.Tables[0].Rows[0]["Username"].ToString();

                        objMaster.UserName = txtUserName.Text.Trim();
                        objMaster.Password = txtPassword.Text.Trim();
                        objMaster.IPAddress = UserIP;
                        objMaster.DateTime = DateTime.Now.AddHours(Connection.SetHours);
                        objMaster.UserId = Convert.ToInt32(Session["UserId"].ToString());

                        DataSet objDataSetLoginId = MasterCode.InsertLoginDetails(objMaster);
                        if (objDataSetLoginId.Tables[0].Rows.Count > 0)
                        {
                            Session["LoginId"] = objDataSetLoginId.Tables[0].Rows[0][0].ToString();
                        }
                        DataTable menuData = new DataTable();
                        menuData = GetMenuData(objDataSet.Tables[0].Rows[0]["UserRights"].ToString());
                        AddTopMenuItems(menuData);
                        Session["Menu"] = menu.Replace("<ul></ul>", "");
                        //if (objDataSet.Tables[0].Rows[0]["User_Id"].ToString() == "1" || objDataSet.Tables[0].Rows[0]["User_Id"].ToString() == "2" || objDataSet.Tables[0].Rows[0]["User_Id"].ToString() == "3")
                        if (Session["RedirectURL"] == null)
                            Response.Redirect("Admin/frmHome.aspx", false);
                        else
                        {
                            Response.Redirect(Session["RedirectURL"].ToString(), false);
                            Session["RedirectURL"] = null;
                        }
                        //else
                        //    Response.Redirect("User/UserHome.aspx", false);
                    }
                    else
                        ShowNotification("Login", "Please Contact Administrator..!", NotificationType.error);
                }
                else
                {
                    DateTime Date = DateTime.Today;
                    objMaster.UserName = txtUserName.Text.Trim();
                    objMaster.Password = txtPassword.Text.Trim();
                    objMaster.IPAddress = Request.UserHostAddress;
                    objMaster.DateTime = Date;

                    MasterCode.InsertFail(objMaster);

                    objDataSet = objMaster.GetCount(txtUserName.Text.Trim(), Request.UserHostAddress, Date);

                    if (objDataSet.Tables[0].Rows.Count >= 3)
                    {
                        objMaster.IsActive = "False";
                        objMaster.UserName = txtUserName.Text.Trim();
                        Session["count"] = 0;

                        int b = objMaster.Block();
                        if (b != 0)
                        {
                            ShowNotification("Login", "Your Login is Blocked", NotificationType.error);
                            Session["UserId"] = "0";
                        }
                    }
                    else
                    {
                        ShowNotification("Login", "Invalid Login!", NotificationType.error);
                        Session["UserId"] = "0";
                    }
                }
            }
            else
                ShowNotification("Login", "Please Enter All Fields!", NotificationType.error);
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);

            SendLogFile.SendMail();

            ShowNotification("Login", "Database is not Connected Properly..!", NotificationType.error);
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            string regexPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";

            bool chk= new Regex(regexPattern, RegexOptions.IgnoreCase).IsMatch(txtEmailId.Text.Trim());

            //string fud = fupImage.FileName;
            if (chk==true&&txtName.Text.Trim() != "" && txtMobileNumber.Text.Trim() != "" && txtEmailId.Text.Trim() != "" && txtAddress.Text.Trim() != "" && ddlDay.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && ddlYear.SelectedIndex != 0 &&ddlCity.SelectedIndex!=0)
            {
                Admin_User objAdmin_User = new Admin_User();

                objAdmin_User.Address = txtAddress.Text.Trim();
                objAdmin_User.EmailId = txtEmailId.Text.Trim();
                objAdmin_User.Mobile = txtMobileNumber.Text.Trim();
                objAdmin_User.Name = txtName.Text.Trim();
                objAdmin_User.DOB = Convert.ToDateTime(ddlMonth.SelectedValue.ToString() + "/" + ddlDay.SelectedValue.ToString() + "/" + ddlYear.SelectedItem.ToString());
                objAdmin_User.RegisterId = 0;
                objAdmin_User.DumpEmailId = "";
                objAdmin_User.Img = "";
                objAdmin_User.CityId = Convert.ToInt32(ddlCity.SelectedValue.ToString());
                objAdmin_User.UserId = 0;
                objAdmin_User.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);

                string Gender = "";
                if (rdbMale.Checked)
                    Gender = "Male";
                else if (rdbFemale.Checked)
                    Gender = "Female";
                else
                    Gender = "Others";

                objAdmin_User.Gender = Gender;//rdbMale.Checked ? "Male" : "Female";
                objAdmin_User.DumpCityId = "0";

                DataSet objDataSet = Admin_User.Send_User_To_DB(objAdmin_User);
                if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                {
                    //if (fud != "")
                    //    fupImage.SaveAs(Server.MapPath("~/User_Photos/") + "Img_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    DataSet objDataSetChk = MasterCode.RetrieveQuery("Select Email,SMS from tbl_Settings where Form_Settings='Register'");
                    if (objDataSetChk.Tables[0].Rows[0]["Email"].ToString() == "True")
                    {
                        SendEmail.createEmailBody(txtName.Text, txtEmailId.Text, txtMobileNumber.Text.Trim(), txtEmailId.Text, objDataSet.Tables[0].Rows[0]["Password"].ToString(), @"RegisterTemplate.html");

                        ShowNotification("User Registered", "Successfully..", NotificationType.success);
                    }
                    if (objDataSetChk.Tables[0].Rows[0]["SMS"].ToString() == "True")
                    {
                        //message = "Dear Customer kindly pay your monthly bill amount Rs. " + dgvList.Rows[i].Cells[6].Value + "/- Ignore if paid. Thanks&Regards Surendra";
                        //string baseURL = "http://mobicomm.dove-sms.com/mobicomm/submitsms.jsp?user=SIMPLE1&key=809e3aa368XX&mobile=" + dgvList.Rows[i].Cells[4].Value + "&message='" + message + "'&senderid=alerts&accusage=1";
                        //client.OpenRead(baseURL);
                        //Your user name
                        string user = "SIMPLE1";
                        //Your authentication key
                        string key = "809e3aa368XX";
                        //Multiple mobiles numbers separated by comma
                        string mobile = txtMobileNumber.Text.Trim();
                        //Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "SIMPLE";
                        //Your message to send, Add URL encoding here.
                        string message = HttpUtility.UrlEncode("Congratulations! your successfully registered in NScreens.com & username: " + txtEmailId.Text.Trim() + " Password: " + objDataSet.Tables[0].Rows[0]["Password"].ToString() + " ");

                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&key={0}", key);
                        sbPostData.AppendFormat("&mobile={0}", mobile);
                        sbPostData.AppendFormat("&message={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&accusage={0}", "1");

                        try
                        {
                            //Call Send SMS API
                            string sendSMSUri = "http://mobicomm.dove-sms.com/mobicomm/submitsms.jsp?";
                            //Create HTTPWebrequest
                            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                            //Prepare and Add URL Encoded data
                            UTF8Encoding encoding = new UTF8Encoding();
                            byte[] data = encoding.GetBytes(sbPostData.ToString());
                            //Specify post method
                            httpWReq.Method = "POST";
                            httpWReq.ContentType = "application/x-www-form-urlencoded";
                            httpWReq.ContentLength = data.Length;
                            using (Stream stream = httpWReq.GetRequestStream())
                            {
                                stream.Write(data, 0, data.Length);
                            }
                            //Get the response
                            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            string responseString = reader.ReadToEnd();

                            //Close the response
                            reader.Close();
                            response.Close();
                        }
                        catch (SystemException ex)
                        {
                            //MessageBox.Show(ex.Message.ToString());

                        }
                    }

                    txtName.Text = "";
                    txtMobileNumber.Text = "";
                    txtEmailId.Text = "";
                    txtAddress.Text = "";
                }
                else if (objDataSet.Tables[0].Rows[0][1].ToString() == "-5")
                    ShowNotification("User Registration", "EmailId Already existed..!", NotificationType.error);
            }
            else
            {
                ShowNotification("User Registration", "Please fill all fields..!", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUsernameForforgetpass.Text.Trim() != "")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("select Password,(select EmailId from tbl_Register_User UR where UR.Register_Id=U.Staff_Id)as EmailId,(select Name from tbl_Register_User UR where UR.Register_Id=U.Staff_Id)as Name,(select Mobile_No from tbl_Register_User UR where UR.Register_Id=U.Staff_Id)as Mobile_No from tbl_user U where UserName='" + txtUsernameForforgetpass.Text + "'");

                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    DataSet objDataSetChk = MasterCode.RetrieveQuery("Select Email,SMS from tbl_Settings where Form_Settings='fgPass'");
                    if (objDataSetChk.Tables[0].Rows[0]["Email"].ToString() == "True")
                    {
                        SendEmail.createEmailBody(objDataSet.Tables[0].Rows[0]["Name"].ToString(), objDataSet.Tables[0].Rows[0]["EmailId"].ToString(), objDataSet.Tables[0].Rows[0]["Mobile_No"].ToString(), objDataSet.Tables[0].Rows[0]["EmailId"].ToString(), objDataSet.Tables[0].Rows[0]["Password"].ToString(), @"ForgetPass.html");

                        //string Body = "<span style='color: Navy'>G'day " + objDataSet.Tables[0].Rows[0]["Name"].ToString() + ",</span><br /><span style='color: DarkGreen'><br />Your User Name : " + objDataSet.Tables[0].Rows[0]["EmailId"].ToString() + "<br />Your Password : " + objDataSet.Tables[0].Rows[0]["Password"].ToString() + "</span></span>";

                        //string Body = "<table width='600' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF'>" +
                        //              "<tr><td><table width='600' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF'><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr><td height='120' align='right'><img src='http://www.nscreens.com/images/logo.png' />" +
                        //              "</td></tr></table></td></tr><tr><td bgcolor='#d82727'><table width='98%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><p style='font-size: 18px; color: #ffffff; margin: 1; padding: 0; font-family: Arial, Helvetica, sans-serif;'>" +
                        //              "Hello " + objDataSet.Tables[0].Rows[0]["Name"].ToString() + ",</p></td></tr><tr><td><p style='font-size: 14px; color: #ffffff; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>Greetings, </p></td></tr></table></td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='5' bgcolor='#ffffff'><tr>" +
                        //              "<td><p style='font-size: 13px; margin: 1; padding: 1; color: #333333; font-family: Arial, Helvetica, sans-serif;'>Your Login Details</p></td></tr><tr><td><table width='60%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>Username :</p></td>" +
                        //              "<td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>" + objDataSet.Tables[0].Rows[0]["EmailId"].ToString() + "</p></td></tr><tr><td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>Password :</p></td><td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>" + objDataSet.Tables[0].Rows[0]["Password"].ToString() + "</p></td></tr></table></td></tr><tr><td>&nbsp;</td></tr>" +
                        //              "</table></td></tr><tr><td>&nbsp;</td></tr><tr><td><p style='font-size: 13px; margin: 1; padding: 0; color: #ffffff; font-family: Arial, Helvetica, sans-serif;'>Please note that this is a system-generated mail with regard to your account at nscreens.com. Your profile information is of utmost importance to us and hence you can be rest assured of its security.</p><p style='font-size: 13px; margin: 1; padding: 0; color: #ffffff; font-family: Arial, Helvetica, sans-serif;'>We welcome you all to contact us for any query, comments and suggestions at <a href='mailto:support@nscreens.com'>" +
                        //              "support@nscreens.com</a> or please call us at +91--_____</p></td></tr></table></td></tr><tr><td bgcolor='#b90303'><p style='font-size: 13px; margin: 1; padding: 0; text-align: right; color: #ffffff;font-family: Arial, Helvetica, sans-serif;'>With Best Wishes&nbsp;&nbsp;<br />nscreens.com&nbsp;&nbsp;</p></td></tr></table></td></tr></table>";

                        //MailMessage mail = new MailMessage();

                        //mail.To.Add(objDataSet.Tables[0].Rows[0]["EmailId"].ToString());
                        //mail.From = new MailAddress("nscreens.eluru@gmail.com");
                        //mail.Subject = "Your Password NScreens";

                        //StringBuilder objStringBuilder = new StringBuilder();
                        //objStringBuilder.Append(Body);

                        //AlternateView objAlternateView;
                        //objAlternateView = AlternateView.CreateAlternateViewFromString(objStringBuilder.ToString(), null, "text/html");

                        //mail.AlternateViews.Add(objAlternateView);
                        //mail.IsBodyHtml = true;

                        //SmtpClient smtp = new SmtpClient();
                        //smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                        //smtp.EnableSsl = true;
                        //smtp.Port = 587;
                        //smtp.Credentials = new System.Net.NetworkCredential("nscreens.eluru@gmail.com", "9885908149");//Or your Smtp Email ID and Password

                        //smtp.Send(mail);
                        //((IDisposable)mail).Dispose();

                        //DataSet objDataSetBlock = SignUp.UpdateBlock(txtUsernameForforgetpass.Text.Trim());

                        ShowNotification("Forgot Password", "Password send successfully..", NotificationType.success);
                        txtUsernameForforgetpass.Text = "";
                    }
                    if (objDataSetChk.Tables[0].Rows[0]["SMS"].ToString() == "true")
                    {
                        //message = "Dear Customer kindly pay your monthly bill amount Rs. " + dgvList.Rows[i].Cells[6].Value + "/- Ignore if paid. Thanks&Regards Surendra";
                        //string baseURL = "http://mobicomm.dove-sms.com/mobicomm/submitsms.jsp?user=SIMPLE1&key=809e3aa368XX&mobile=" + dgvList.Rows[i].Cells[4].Value + "&message='" + message + "'&senderid=alerts&accusage=1";
                        //client.OpenRead(baseURL);
                        //Your user name
                        string user = "SIMPLE1";
                        //Your authentication key
                        string key = "809e3aa368XX";
                        //Multiple mobiles numbers separated by comma
                        string mobile = objDataSet.Tables[0].Rows[0]["Mobile_No"].ToString();
                        //Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "SIMPLE";
                        //Your message to send, Add URL encoding here.
                        string message = HttpUtility.UrlEncode("Good Day " + objDataSet.Tables[0].Rows[0]["Name"].ToString() + " your password is " + objDataSet.Tables[0].Rows[0]["Password"].ToString() + "");

                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&key={0}", key);
                        sbPostData.AppendFormat("&mobile={0}", mobile);
                        sbPostData.AppendFormat("&message={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&accusage={0}", "1");

                        try
                        {
                            //Call Send SMS API
                            string sendSMSUri = "http://mobicomm.dove-sms.com/mobicomm/submitsms.jsp?";
                            //Create HTTPWebrequest
                            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                            //Prepare and Add URL Encoded data
                            UTF8Encoding encoding = new UTF8Encoding();
                            byte[] data = encoding.GetBytes(sbPostData.ToString());
                            //Specify post method
                            httpWReq.Method = "POST";
                            httpWReq.ContentType = "application/x-www-form-urlencoded";
                            httpWReq.ContentLength = data.Length;
                            using (Stream stream = httpWReq.GetRequestStream())
                            {
                                stream.Write(data, 0, data.Length);
                            }
                            //Get the response
                            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            string responseString = reader.ReadToEnd();

                            //Close the response
                            reader.Close();
                            response.Close();
                        }
                        catch (SystemException ex)
                        {
                            //MessageBox.Show(ex.Message.ToString());

                        }
                    }
                }
                else
                    ShowNotification("Forgot Password", "Sorry, Invalid Email Address..!", NotificationType.error);
            }
            else
                ShowNotification("Forgot Password", "Please Enter EmailId..!", NotificationType.error);
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            SendLogFile.SendMail();
            ShowNotification("Forgot Password", "Sorry, Mail not sent due to Network problem.. Please Try Again..!", NotificationType.error);
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
    protected void txtEmailId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string regexPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";

            bool chk= new Regex(regexPattern, RegexOptions.IgnoreCase).IsMatch(txtEmailId.Text.Trim());

            //string fud = fupImage.FileName;
            if (chk==false)
                ShowNotification("User Registration", "Invalid EmailId..!", NotificationType.error);

            txtEmailId.Focus();

        }catch(Exception Ex)
        {

        }
    }
}
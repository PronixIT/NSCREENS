using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Settings : System.Web.UI.Page
{
    static string conn = Connection.con;
    SqlConnection con = new SqlConnection(conn);

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

    public void User_Profile()
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Register_Id,Name,Mobile_No,EmailId,Address,CONVERT(varchar(12),DOB,100)as DOB,Photo,City_Id,(Select District_Id from tbl_admin_city C where C.City_Id=U.City_Id)as District_Id,(Select (select State_Id from tbl_admin_district AD where AD.District_Id=C.District_Id) from tbl_admin_city C where C.City_Id=U.City_Id)as State_Id,Gender,Profile_Updated_Date from tbl_Register_User U where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                if (objDataSet.Tables[0].Rows[0]["Gender"].ToString() == "Male")
                    rdbMale.Checked = true;
                else
                    rdbFemale.Checked = true;

                //int CalDays =Convert.ToInt32(Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["Profile_Updated_Date"].ToString()) - DateTime.Now.AddHours(Connection.SetHours));

                if (Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["Profile_Updated_Date"].ToString()).AddDays(14) <= DateTime.Now.AddHours(Connection.SetHours))
                {
                    rdbFemale.Enabled = true;
                    rdbMale.Enabled = true;
                    rdbOthers.Enabled = true;

                    ddlCity.Enabled = true;
                    ddlState.Enabled = true;
                    ddlDistrict.Enabled = true;

                }
                else
                {
                    rdbFemale.Enabled = false;
                    rdbMale.Enabled = false;
                    rdbOthers.Enabled = false;

                    ddlCity.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                }
                DateTime BirthDate = Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["DOB"].ToString());

                ddlDay.ClearSelection();
                if (ddlDay.Items.FindByValue(BirthDate.Day.ToString()) != null)
                    ddlDay.Items.FindByValue(BirthDate.Day.ToString()).Selected = true;

                ddlMonth.ClearSelection();
                if (ddlMonth.Items.FindByValue(BirthDate.Month.ToString()) != null)
                    ddlMonth.Items.FindByValue(BirthDate.Month.ToString()).Selected = true;

                ddlYear.ClearSelection();
                if (ddlYear.Items.FindByText(BirthDate.Year.ToString()) != null)
                    ddlYear.Items.FindByText(BirthDate.Year.ToString()).Selected = true;

                DropDownList(ddlState, "State_Id", "State_Name", "select State_Id,State_Name from tbl_admin_state where Isactive='True' Order by State_Name", "Select");
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

                lblDumpCityId.Text = objDataSet.Tables[0].Rows[0]["City_Id"].ToString();

                txtName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                txtMobileNumber.Text = objDataSet.Tables[0].Rows[0]["Mobile_No"].ToString();
                txtAddress.Text = objDataSet.Tables[0].Rows[0]["Address"].ToString();
                txtEmailId.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                lblRegisterId.Text = objDataSet.Tables[0].Rows[0]["Register_Id"].ToString();
                lblDumpEmailId.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();

                imgPhoto.ImageUrl = "~/User_Photos/" + objDataSet.Tables[0].Rows[0]["Photo"].ToString();
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

                User_Profile();

                DropDownList(ddlSearchTitle, "Short_film_Id", "Title", "Select Short_film_Id,Title from tbl_Short_film where Isactive='True' and CreatedById='" + Session["UserId"].ToString() + "' Order by Title", "Select");
                DropDownList(ddlSearchCategory, "Category_Id", "Category_Name", "Select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name", "Select");

                Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,LS.Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.CreatedById='" + Session["UserId"].ToString() + "'");

                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True' and CreatedById='" + Session["UserId"].ToString() + "'");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        try
        {
            string AddColums = "";
            if (ddlSearchTitle.SelectedIndex != 0)
                AddColums = AddColums + " LS.Short_film_Id=" + ddlSearchTitle.SelectedValue.ToString() + " and ";
            if (ddlSearchCategory.SelectedIndex != 0)
                AddColums = AddColums + " Category Like '%," + ddlSearchCategory.SelectedValue.ToString() + ",%' and ";

            if (ddlShortStatus.SelectedIndex != 0)
                AddColums = AddColums + " LS.Status='" + ddlShortStatus.SelectedItem.ToString() + "' and ";

            if (AddColums != "")
            {
                AddColums = AddColums.Remove(AddColums.Length - 4, 4);
                Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,(Select Language_Name from tbl_admin_language where Language_Id=SF.Language)as Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film   SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.CreatedById='" + Session["UserId"].ToString() + "' and " + AddColums);
            }
            else
                Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,LS.Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.ModifiedDate,LS.ModifiedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.CreatedById='" + Session["UserId"].ToString() + "'");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            string AddColum = "";
            if (ddlStatus.SelectedIndex != 0)
                AddColum = AddColum + " Status='" + ddlStatus.SelectedItem.ToString() + "' and ";

            if (AddColum != "")
            {
                AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True' and CreatedById='" + Session["UserId"].ToString() + "' and " + AddColum);
            }
            else
            {
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True' and CreatedById='" + Session["UserId"].ToString() + "'");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;

            Display_List(gvViews, "Select Visits_Id,Advertisement_Id,(Select Name from tbl_Register_User RU where RU.Register_Id=U.Staff_Id)as Username,CONVERT(varchar(max),Date_Time,100)as Date_Time,City_Id,IPAddress from tbl_Advertizment_Visits V,tbl_user U where Advertisement_Id=" + lnk.CommandName + " and V.User_Id=U.User_Id");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkViewShort_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;

            Display_List(gvShortView, "Select Visits_Id,Short_Film_Id,(Select Name from tbl_Register_User RU where RU.Register_Id=U.Staff_Id)as Username,CONVERT(varchar(max),Date_Time)as Date_Time,City_Id,IPAddress,User_Budget,Video_Sharing from tbl_Short_Film_Visits V,tbl_user U where Short_Film_Id=" + lnk.CommandName + " and V.User_Id=U.User_Id Order by Visits_Id desc");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupShort()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOldPassword.Text != "" && txtNewPassword.Text != "" && txtConfirmPassword.Text != "")
            {
                if (Session["Username"] != null)
                {
                    if (txtNewPassword.Text.Trim().Length >= 5)
                    {
                        if (txtNewPassword.Text == txtConfirmPassword.Text)
                        {
                            string UserName = Session["Username"].ToString();
                            SqlCommand cmd = new SqlCommand("Sp_Login_ChangePassword", con); con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@OldPassword", SqlDbType.VarChar)).Value = txtOldPassword.Text;
                            cmd.Parameters.Add(new SqlParameter("@NewPassword", SqlDbType.VarChar)).Value = txtNewPassword.Text.Trim();
                            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = Session["Username"].ToString();
                            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = 1;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    DataSet objDataSet = MasterCode.RetrieveQuery("select Password,(select EmailId from tbl_Register_User UR where UR.Register_Id=U.Staff_Id)as EmailId,(select Name from tbl_Register_User UR where UR.Register_Id=U.Staff_Id)as Name,(select Mobile_No from tbl_Register_User UR where UR.Register_Id=U.Staff_Id)as Mobile_No from tbl_user U where UserName='" + UserName + "'");

                                    if (objDataSet.Tables[0].Rows.Count > 0)
                                    {
                                        DataSet objDataSetChk = MasterCode.RetrieveQuery("Select Email,SMS from tbl_Settings where Form_Settings='ChgPass'");
                                        if (objDataSetChk.Tables[0].Rows[0]["Email"].ToString() == "True")
                                        {
                                            //string Body = "<span style='color: Navy'>G'day " + objDataSet.Tables[0].Rows[0]["Name"].ToString() + ",</span><br /><span style='color: DarkGreen'><br />Your User Name : " + objDataSet.Tables[0].Rows[0]["EmailId"].ToString() + "<br />Your Password : " + objDataSet.Tables[0].Rows[0]["Password"].ToString() + "</span></span>";

                                            string Body = "<table width='600' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF'>" +
                                                          "<tr><td><table width='600' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF'><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr><td height='120' align='right'><img src='http://www.nscreens.com/images/logo.png' />" +
                                                          "</td></tr></table></td></tr><tr><td bgcolor='#d82727'><table width='98%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><p style='font-size: 18px; color: #ffffff; margin: 1; padding: 0; font-family: Arial, Helvetica, sans-serif;'>" +
                                                          "Hello " + objDataSet.Tables[0].Rows[0]["Name"].ToString() + ",</p></td></tr><tr><td><p style='font-size: 14px; color: #ffffff; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>Greetings, </p></td></tr></table></td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='5' bgcolor='#ffffff'><tr>" +
                                                          "<td><p style='font-size: 13px; margin: 1; padding: 1; color: #333333; font-family: Arial, Helvetica, sans-serif;'>Your Login Details</p></td></tr><tr><td><table width='60%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>Username :</p></td>" +
                                                          "<td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>" + objDataSet.Tables[0].Rows[0]["EmailId"].ToString() + "</p></td></tr><tr><td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>Password :</p></td><td><p style='font-size: 12px; color: #333333; margin: 0; padding: 0; font-family: Arial, Helvetica, sans-serif;'>" + objDataSet.Tables[0].Rows[0]["Password"].ToString() + "</p></td></tr></table></td></tr><tr><td>&nbsp;</td></tr>" +
                                                          "</table></td></tr><tr><td>&nbsp;</td></tr><tr><td><p style='font-size: 13px; margin: 1; padding: 0; color: #ffffff; font-family: Arial, Helvetica, sans-serif;'>Please note that this is a system-generated mail with regard to your account at nscreens.com. Your profile information is of utmost importance to us and hence you can be rest assured of its security.</p><p style='font-size: 13px; margin: 1; padding: 0; color: #ffffff; font-family: Arial, Helvetica, sans-serif;'>We welcome you all to contact us for any query, comments and suggestions at <a href='mailto:support@nscreens.com'>" +
                                                          "support@nscreens.com</a> or please call us at +91--_____</p></td></tr></table></td></tr><tr><td bgcolor='#b90303'><p style='font-size: 13px; margin: 1; padding: 0; text-align: right; color: #ffffff;font-family: Arial, Helvetica, sans-serif;'>With Best Wishes&nbsp;&nbsp;<br />nscreens.com&nbsp;&nbsp;</p></td></tr></table></td></tr></table>";

                                            MailMessage mail = new MailMessage();

                                            mail.To.Add(objDataSet.Tables[0].Rows[0]["EmailId"].ToString());
                                            mail.From = new MailAddress("nscreens.eluru@gmail.com");
                                            mail.Subject = "Your Password NScreens";

                                            StringBuilder objStringBuilder = new StringBuilder();
                                            objStringBuilder.Append(Body);

                                            AlternateView objAlternateView;
                                            objAlternateView = AlternateView.CreateAlternateViewFromString(objStringBuilder.ToString(), null, "text/html");

                                            mail.AlternateViews.Add(objAlternateView);
                                            mail.IsBodyHtml = true;

                                            SmtpClient smtp = new SmtpClient();
                                            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                                            smtp.EnableSsl = true;
                                            smtp.Port = 587;
                                            smtp.Credentials = new System.Net.NetworkCredential("nscreens.eluru@gmail.com", "9885908149");//Or your Smtp Email ID and Password

                                            smtp.Send(mail);
                                            ((IDisposable)mail).Dispose();

                                            //DataSet objDataSetBlock = SignUp.UpdateBlock(txtUsernameForforgetpass.Text.Trim());

                                            ShowNotification("Change Password", "Password Changed successfully..", NotificationType.success);

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
                                            string mobile = objDataSet.Tables[0].Rows[0]["Mobile_No"].ToString();
                                            //Sender ID,While using route4 sender id should be 6 characters long.
                                            string senderid = "SIMPLE";
                                            //Your message to send, Add URL encoding here.
                                            string message = HttpUtility.UrlEncode("Good Day " + objDataSet.Tables[0].Rows[0]["Name"].ToString() + " your changed password is " + objDataSet.Tables[0].Rows[0]["Password"].ToString() + "");

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

                                    Session["Username"] = null;
                                    Session["UserFullName"] = null;
                                    Session["ChangePassword"] = "Success";

                                    Response.Redirect("../Login.aspx", false);
                                }
                                else
                                    ShowNotification("Change Password", "Invalid Password..!", NotificationType.error);
                            }
                        }
                        else
                            ShowNotification("Change Password", "Sorry, New Password and Confirm Password should be Same..!", NotificationType.error);
                    }
                    else
                        ShowNotification("Change Password", "Sorry, New Password should be at least 5 characters..!", NotificationType.error);
                }
                else
                    ShowNotification("Change Password", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Change Password", "Please Fill All Fields", NotificationType.error);
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            SendLogFile.SendMail();
            ShowNotification("Change Password", dispErrorMsg, NotificationType.error);
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

    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        try
        {
            string fud = fudPhoto.FileName;
            if (txtName.Text.Trim() != "" && txtMobileNumber.Text.Trim() != "" && txtEmailId.Text.Trim() != "" && txtAddress.Text.Trim() != "" && ddlDay.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && ddlYear.SelectedIndex != 0)
            {
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
                objAdmin_User.DumpCityId = lblDumpCityId.Text.Trim();

                if (fud != "")
                    objAdmin_User.Img = "Img";
                else
                    objAdmin_User.Img = "";

                DataSet objDataSet = Admin_User.Send_User_To_DB(objAdmin_User);
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                {
                    if (fud != "")
                        fudPhoto.SaveAs(Server.MapPath("~/User_Photos/") + "Img_" + lblRegisterId.Text.Trim() + ".jpg");

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
}
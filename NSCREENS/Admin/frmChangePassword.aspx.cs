using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class Password_frmChangePassword : System.Web.UI.Page
{
    static string conn = Connection.con;
    SqlConnection con = new SqlConnection(conn);

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

    public void Clear()
    {
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtConfirmPassword.Text = "";

        txtOldPassword.Focus();
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

                //string Login = Session["Username"].ToString();

                //if (Login == null)
                //    Response.Redirect("frmLogin.aspx", false);

                //ShowNotification("Change Password", "New Password should be at least 5 characters..", NotificationType.info);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            SendLogFile.SendMail();
            ShowNotification("Change Password", "Page Load Error", NotificationType.error);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
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

    //protected void btnSubmitUsercode_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtOldUsercode.Text != "" && txtNewUsercode.Text != "" && txtConfirmUsercode.Text != "")
    //        {
    //            if (Session["Username"] != null)
    //            {
    //                if (txtNewUsercode.Text.Trim().Length >= 4)
    //                {
    //                    if (txtNewUsercode.Text == txtConfirmUsercode.Text)
    //                    {
    //                        string UserName = Session["Username"].ToString();
    //                        SqlCommand cmd = new SqlCommand("Sp_Login_ChangePassword", con); con.Open();
    //                        cmd.CommandType = CommandType.StoredProcedure;
    //                        cmd.Parameters.Add(new SqlParameter("@OldPassword", SqlDbType.VarChar)).Value = txtOldUsercode.Text;
    //                        cmd.Parameters.Add(new SqlParameter("@NewPassword", SqlDbType.VarChar)).Value = txtNewUsercode.Text.Trim();
    //                        cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = Session["Username"].ToString();
    //                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = 2;
    //                        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //                        DataSet ds = new DataSet();
    //                        da.Fill(ds);

    //                        if (ds.Tables[0].Rows.Count > 0)
    //                        {
    //                            if (ds.Tables[0].Rows[0][0].ToString() == "1")
    //                            {
    //                                Session["Username"] = null;
    //                                Session["UserFullName"] = null;
    //                                Session["ChangePassword"] = "Success";
    //                                Response.Redirect("~/Admin/Login.aspx", false);
    //                            }
    //                            else
    //                                ShowNotification("Change Password", "Invalid Password..!", NotificationType.error);
    //                        }
    //                    }
    //                    else
    //                        ShowNotification("Change Password", "Sorry, New Password and Confirm Password should be Same..!", NotificationType.error);
    //                }
    //                else
    //                    ShowNotification("Change Password", "Sorry, New Password should be at least 5 characters..!", NotificationType.error);
    //            }
    //            else
    //                ShowNotification("Change Password", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
    //        }
    //        else
    //            ShowNotification("Change Password", "Please Fill All Fields", NotificationType.error);
    //    }
    //    catch (Exception Ex)
    //    {
    //        StackTrace objStackTrace = new StackTrace();
    //        string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
    //        string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
    //        LogFile.WriteToLog(dispErrorMsg, Ex);
    //        SendLogFile.SendMail();
    //        ShowNotification("Change Password", dispErrorMsg, NotificationType.error);
    //    }
    //}
}
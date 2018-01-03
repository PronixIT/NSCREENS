using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmAdvatizmentReport : System.Web.UI.Page
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

                //CheckBoxList(lstUsers, "User_Id", "Username", "Select User_Id,Username from tbl_user U where Isactive='true' and Staff_Id!=0 order by Username");
                Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' Order by Register_Id desc");
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
            string AddColum = "";
            //        //if (txtStartDate.Text.Trim() != "")
            //        //    AddColum = AddColum + " CreatedDate='" + txtStartDate.Text.Trim() + "' and ";
            //        //if (txtEndDate.Text.Trim() != "")
            //        //    AddColum = AddColum + " CreatedDate='" + txtEndDate.Text.Trim() + "' and ";

            //        //string CoveredAreaIds = ",";
            //        //for (int i = 0; i < lstUsers.Items.Count; i++)
            //        //    if (lstUsers.Items[i].Selected)
            //        //        CoveredAreaIds = CoveredAreaIds + lstUsers.Items[i].Value.ToString() + ",";

            //        //if (CoveredAreaIds != "")
            //        //    AddColum = AddColum + " CreatedById in (0" + CoveredAreaIds + "0) and ";

            if (txtSearchName.Text != "")
            {
                AddColum = AddColum + " Name Like '" + txtSearchName.Text.Trim() + "%' and ";
            }

            if (AddColum != "")
            {
                AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' and " + AddColum + " Order by Register_Id desc");
            }
            else
            {
                Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' Order by Register_Id desc");
            }
            //        if (rdbSendEmail.Checked)
            //        {
            //            for (int i = 0; i < gvUserList.Rows.Count; i++)
            //            {
            //                string Body = "Send Email";

            //                MailMessage mail = new MailMessage();

            //                mail.To.Add((gvUserList.Rows[i].FindControl("lblEmailId") as Label).Text);
            //                mail.From = new MailAddress("nscreens.eluru@gmail.com");
            //                mail.Subject = "NScreens";

            //                StringBuilder objStringBuilder = new StringBuilder();
            //                objStringBuilder.Append(Body);

            //                AlternateView objAlternateView;
            //                objAlternateView = AlternateView.CreateAlternateViewFromString(objStringBuilder.ToString(), null, "text/html");

            //                mail.AlternateViews.Add(objAlternateView);
            //                mail.IsBodyHtml = true;

            //                SmtpClient smtp = new SmtpClient();
            //                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            //                smtp.EnableSsl = true;

            //                smtp.Credentials = new System.Net.NetworkCredential("nscreens.eluru@gmail.com", "9885908149");//Or your Smtp Email ID and Password

            //                smtp.Send(mail);
            //                ((IDisposable)mail).Dispose();
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < gvUserList.Rows.Count; i++)
            //            {
            //                //message = "Dear Customer kindly pay your monthly bill amount Rs. " + dgvList.Rows[i].Cells[6].Value + "/- Ignore if paid. Thanks&Regards Surendra";
            //                //string baseURL = "http://mobicomm.dove-sms.com/mobicomm/submitsms.jsp?user=SIMPLE1&key=809e3aa368XX&mobile=" + dgvList.Rows[i].Cells[4].Value + "&message='" + message + "'&senderid=alerts&accusage=1";
            //                //client.OpenRead(baseURL);
            //                //Your user name
            //                string user = "SIMPLE1";
            //                //Your authentication key
            //                string key = "809e3aa368XX";
            //                //Multiple mobiles numbers separated by comma
            //                string mobile = (gvUserList.Rows[i].FindControl("lblMobile_No") as Label).Text;
            //                //Sender ID,While using route4 sender id should be 6 characters long.
            //                string senderid = "SIMPLE";
            //                //Your message to send, Add URL encoding here.
            //                string message = HttpUtility.UrlEncode("Send Text");

            //                //Prepare you post parameters
            //                StringBuilder sbPostData = new StringBuilder();
            //                sbPostData.AppendFormat("user={0}", user);
            //                sbPostData.AppendFormat("&key={0}", key);
            //                sbPostData.AppendFormat("&mobile={0}", mobile);
            //                sbPostData.AppendFormat("&message={0}", message);
            //                sbPostData.AppendFormat("&senderid={0}", senderid);
            //                sbPostData.AppendFormat("&accusage={0}", "1");

            //                try
            //                {
            //                    //Call Send SMS API
            //                    string sendSMSUri = "http://mobicomm.dove-sms.com/mobicomm/submitsms.jsp?";
            //                    //Create HTTPWebrequest
            //                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //                    //Prepare and Add URL Encoded data
            //                    UTF8Encoding encoding = new UTF8Encoding();
            //                    byte[] data = encoding.GetBytes(sbPostData.ToString());
            //                    //Specify post method
            //                    httpWReq.Method = "POST";
            //                    httpWReq.ContentType = "application/x-www-form-urlencoded";
            //                    httpWReq.ContentLength = data.Length;
            //                    using (Stream stream = httpWReq.GetRequestStream())
            //                    {
            //                        stream.Write(data, 0, data.Length);
            //                    }
            //                    //Get the response
            //                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            //                    StreamReader reader = new StreamReader(response.GetResponseStream());
            //                    string responseString = reader.ReadToEnd();

            //                    //Close the response
            //                    reader.Close();
            //                    response.Close();
            //                }
            //                catch (SystemException ex)
            //                {
            //                    //MessageBox.Show(ex.Message.ToString());

            //                }

            //            }
            //            if (gvUserList.Rows.Count > 0)
            //                ShowNotification("User", "Send Successfully..", NotificationType.success);
            //        }
        }
        catch (Exception Ex)
        {

        }
    }
    protected void gvUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                string AddColum = "";

                int index = Convert.ToInt32(e.CommandArgument);

                DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_user set Isactive='true',ModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000' where Username='" + (gvUserList.Rows[index].FindControl("lblgridUsername") as Label).Text + "'");
                DataSet objDataSet23 = MasterCode.RetrieveQuery("Update tbl_Register_User set Isactive='true',ModifiedDate=getdate() where Register_Id in (Select Staff_Id from tbl_user where Username='" + lblUsrname.Text + "')");
                DataSet objDataSet1 = MasterCode.RetrieveQuery("Delete from tbl_LoginFailed where UserName='" + (gvUserList.Rows[index].FindControl("lblgridUsername") as Label).Text + "' and DateTime='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000'");

                ShowNotification("User", "Active Successfully..", NotificationType.success);

                if (txtSearchName.Text != "")
                    AddColum = AddColum + " Name Like '" + txtSearchName.Text.Trim() + "%' and ";

                if (AddColum != "")
                {
                    AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                    Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' and " + AddColum + " Order by Register_Id desc");
                }
                else
                    Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' Order by Register_Id desc");

                //lblName.Text = (gvUserList.Rows[index].FindControl("lblName") as Label).Text;
                //lblUsrname.Text = (gvUserList.Rows[index].FindControl("lblgridUsername") as Label).Text;

                //rdbActiveYesState.Checked = true;

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch(Exception Ex)
        {

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string Block = "";
            if (rdbActiveYesState.Checked)
                Block = "false";
            else
                Block = "true";

            DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_user set Isactive='" + Block + "',ModifiedDate=getdate() where Username='" + lblUsrname.Text + "'");
            DataSet objDataSet23 = MasterCode.RetrieveQuery("Update tbl_Register_User set Isactive='" + Block + "',ModifiedDate=getdate() where Register_Id in (Select Staff_Id from tbl_user where Username='" + lblUsrname.Text + "')");
            DataSet objDataSet1 = MasterCode.RetrieveQuery("Delete from tbl_LoginFailed where UserName='" + lblUsrname.Text + "' and DateTime='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000'");
            ShowNotification("User", "Active Successfully..", NotificationType.success);

            string AddColum = "";

            if (txtSearchName.Text != "")
            {
                AddColum = AddColum + " Name Like '" + txtSearchName.Text.Trim() + "%' and ";
            }

            if (AddColum != "")
            {
                AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' and " + AddColum + " Order by Register_Id desc");
            }
            else
            {
                Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' Order by Register_Id desc");
            }
        }
        catch(Exception Ex)
        {

        }
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton chk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)chk.NamingContainer;

                DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_user set Isactive='true',ModifiedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000' where Username='" + (Row.FindControl("lblgridUsername") as Label).Text + "'");
                DataSet objDataSet23 = MasterCode.RetrieveQuery("Update tbl_Register_User set Isactive='true',ModifiedDate=getdate() where Register_Id in (Select Staff_Id from tbl_user where Username='" + lblUsrname.Text + "')");
                DataSet objDataSet1 = MasterCode.RetrieveQuery("Delete from tbl_LoginFailed where UserName='" + (Row.FindControl("lblgridUsername") as Label).Text + "' and DateTime='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000'");

                ShowNotification("User", "Active Successfully..", NotificationType.success);

                string AddColum = "";

                if (txtSearchName.Text != "")
                {
                    AddColum = AddColum + " Name Like '" + txtSearchName.Text.Trim() + "%' and ";
                }

                if (AddColum != "")
                {
                    AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                    Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' and " + AddColum + " Order by Register_Id desc");
                }
                else
                {
                    Display_List(gvUserList, "SELECT  [Register_Id],[Name],[Mobile_No],[EmailId],[Address] ,convert(varchar(12),DOB,103)as [DOB],'../User_Photos/'+Photo as Photo,U.Isactive,(Select Username from tbl_user TU where TU.Staff_Id=U.Register_Id)as Username,(Select Password from tbl_user TU where TU.Staff_Id=U.Register_Id)as Password FROM tbl_Register_User U Join tbl_user T on U.Register_Id=T.Staff_Id where T.Isactive='false' Order by Register_Id desc");
                }

            
        }
        catch (Exception Ex)
        {

        }
    }
}
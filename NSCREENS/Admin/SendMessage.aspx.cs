using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_SendMessage : System.Web.UI.Page
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
                cmd.CommandText = "Select Username from tbl_user where Isactive='true' and Username Like @SearchText + '%' Order by Username";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> Title = new List<string>();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                    while (sdr.Read())
                        Title.Add(sdr["Username"].ToString());

                conn.Close();
                return Title;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                String myUrl = Request.RawUrl.ToString();
                var result = Path.GetFileName(myUrl);
                String Folder = Path.GetDirectoryName(myUrl);
                string[] SplitOffer = Folder.Split('\\');
                for (int i = 0; i < SplitOffer.Length; i++)
                    if (i == 1)
                        Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;
            }
        }
        catch(Exception Ex)
        {

        }
    }

    protected void btn_Click(object sender,EventArgs e)
    {
        try
        {
            if(txtUserName.Text.Trim()!=""&&txtMessage.Content.Trim()!=""&&txtSubject.Text.Trim()!="")
            {
                SendMessage objSendMessage=new SendMessage();

                objSendMessage.SendFrom = Session["UserId"].ToString();
                objSendMessage.SendTo = txtUserName.Text.Trim();
                objSendMessage.Message = txtMessage.Content.Trim();
                objSendMessage.Subject= txtSubject.Text.Trim();
                objSendMessage.CreatedDate=DateTime.Now;

                DataSet objDataSet = SendMessage.Send_Message(objSendMessage);
                if(objDataSet.Tables[0].Rows[0][0].ToString()=="1")
                {
                    ShowNotification("Send Message", "Send Successfully..", NotificationType.success);
                    txtMessage.Content = "";
                    txtUserName.Text = "";
                    txtSubject.Text = "";
                }
            }
            else
            {
                ShowNotification("Send Message", "Please Fill all Fields..!", NotificationType.error);
            }
        }
        catch(Exception Ex)
        {

        }
    }
}
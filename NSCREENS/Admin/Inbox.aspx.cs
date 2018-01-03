using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_Inbox : System.Web.UI.Page
{
    public void Display_List(ListView gv,string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if(objDataSet.Tables[0].Rows.Count>0)
            {
                gv.DataSource = objDataSet;
                gv.DataBind();
            }
            else
            {
                gv.DataSource = objDataSet;
                gv.DataBind();
            }
        }
        catch(Exception Ex)
        {

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

                DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_SendMessage set IsRead='true' where IsRead='false' and SendTo in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");

                //Display_List(gvInbox, "Select Send_Message_Id,Message from tbl_SendMessage where SendTo in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                Display_List(lstInbox, "Select Send_Message_Id,'#'+cast(Send_Message_Id as varchar(max)) as id,Message,Subject from tbl_SendMessage where SendTo in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            }
        }
        catch(Exception Ex)
        {

        }
    }
}
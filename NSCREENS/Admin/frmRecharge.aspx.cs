using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
public partial class frmRecharge : System.Web.UI.Page
{
    //DataBinding db = new DataBinding();
    #region Private Methods & Enum

    public static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlConnection conn = new SqlConnection(con);
    public static string username;

    enum NotificationType
    {
        info,
        success,
        error
    }


    SqlDataAdapter adp;
    SqlDataReader rd;
    MasterCode objMaster = new MasterCode();
    DataSet objDataSet = new DataSet();

    private void ShowNotification(string title, string msg, NotificationType nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

    #endregion

    public void DropDownList(DropDownList ddl, string DataValueField, string DataField, string Query, string DisplayName)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = objDataSet;
                ddl.DataValueField = DataValueField;
                ddl.DataTextField = DataField;
                ddl.DataBind();

                if (DisplayName != "")
                    ddl.Items.Insert(0, new ListItem(DisplayName, "0"));
            }
            else
            {
                if (ddl.Items.Count > 0)
                    ddl.Items.Clear();
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
                //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //scriptManager.RegisterPostBackControl(this.imgExcel);

                //DropDownList(ddlCode, "id", "operatorname", "SELECT id,operatorname FROM Operator where Isactive='true' order by operatorname", "All");
                btnSubmit.Attributes.Add("onclick", "this.disabled=true;" + ClientScript.GetPostBackEventReference(btnSubmit, "").ToString());
                
                String myUrl = Request.RawUrl.ToString();
                var result = Path.GetFileName(myUrl);
                String Folder = Path.GetDirectoryName(myUrl);
                string[] SplitOffer = Folder.Split('\\');
                for (int i = 0; i < SplitOffer.Length; i++)
                {
                    if (i == 2)
                    {
                        Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;
                    }
                }

                username = Session["UserName"].ToString();
               
       

                if (Session["Menu"] != null)
                {
                    DataSet ds = null;
                    string Id = Session["UserId"].ToString();

                }
                else
                {
                    Session["LoginId"] = null;
                    Response.Redirect("~/frmLogin.aspx", false);
                }
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("/frmLogin.aspx");
                }
                if (!IsPostBack)
                {
                    username = Session["UserName"].ToString();
                    DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    string Date = dt.ToString("yyyy-MM-dd");
                    //db.Display_List(GridView2, "" + Date + " 00:00:00.000", "" + Date + " 23:59:59.999", "" + username + "", "", "", "", "SP_GET_RECHARGEREPORT_DATA_HOME");

                }


            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);

            ShowNotification("Home", dispErrorMsg, NotificationType.error);
        }
    }

   

  

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell cell = e.Row.Cells[0];
            string Status = (cell.Text);
            if (Status == "Failure")
            {
                cell.BackColor = Color.Red;
            }
            if (Status == "Success")
            {
                cell.BackColor = Color.Green;
            }
            if (Status == "Pending")
            {
                cell.BackColor = Color.Gray;
            }

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
            System.Threading.Thread.Sleep(1000);
            btnSubmit.Enabled = false;
            btnSubmit.Text="Please wait...";
            Button btn = sender as Button;
            switch (btn.CommandName)
            {
                case "Prepaid":
                  
                    clear();
                    break;
               
                case "Save":
                    Send_Data(txtUpdatetid.Text.Trim(), 0, "", true, TextBox4.Text.Trim(), txtDeatils.Text.Trim(), "0");
                    clear();
                    break;
               

            }
            btnSubmit.Enabled = true;
            btnSubmit.Text = "Submit";
           
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Bank", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }

    }


    public void rdbprepaid_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbprepaid.Checked == true)
        {
            this.ddloperator.Items.Clear();
            Prepaidcombobind();

            ddloperator.SelectedIndex = 0;
        }

    }
    public void rdbPostpaid_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbPostpaid.Checked == true)
        {
            this.ddloperator.Items.Clear();
            Postpaidcombobind();

            ddloperator.SelectedIndex = 0;
        }

    }
    public void rdbDTH_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDTH.Checked == true)
        {
            this.ddloperator.Items.Clear();
            Dthcombobind();
            ddloperator.SelectedIndex = 0;
        }
    }
    public void Prepaidcombobind()
    {
        ddloperator.Items.Add("Aircel");
        ddloperator.Items.Add("Airtel");
        ddloperator.Items.Add("Bsnl");
        ddloperator.Items.Add("Docomo Topup");
        ddloperator.Items.Add("Tata Indicom");
        ddloperator.Items.Add("Idea");
        ddloperator.Items.Add("Jio");
        ddloperator.Items.Add("MTS");
        ddloperator.Items.Add("Reliance (GSM/CDMA)");
        ddloperator.Items.Add("Telenor");
        ddloperator.Items.Add("Vodafone");


    }
    public void Dthcombobind()
    {

        ddloperator.Items.Add("Airtel DTH");
        ddloperator.Items.Add("Bigtv DTH");
        ddloperator.Items.Add("Dishtv DTH");
        ddloperator.Items.Add("Tatasky DTH");
        ddloperator.Items.Add("Sundirect DTH");
        ddloperator.Items.Add("VideoconD2h DTH");

    }
    public void Postpaidcombobind()
    {
        ddloperator.Items.Add("Aircel Postpaid");
        ddloperator.Items.Add("Airtel Postpaid");
        ddloperator.Items.Add("Bsnl Postpaid");
        ddloperator.Items.Add("Docomo Postpaid");
        ddloperator.Items.Add("Idea Postpaid");
        ddloperator.Items.Add("Reliance Postpaid");
        ddloperator.Items.Add("Vodafone Postpaid");


    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdatetid.Text = (GridView2.Rows[index].FindControl("lblGridID") as Label).Text;

                TextBox4.Text = (GridView2.Rows[index].FindControl("lblGridMobileNo") as Label).Text;
                lblID.Text = (GridView2.Rows[index].FindControl("lblGridID") as Label).Text;


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Home", dispErrorMsg, NotificationType.error);

        }
    }

  
    public void Send_Data(string Ticket_Name, int Ticket_Id, string DumpTicket_Name, bool Isactive, string Subject, string Description, string Priority)
    {
        try
        {
            if (Ticket_Name != "")
            {
               
                    //Admin_Bank objAdmin_Bank = new Admin_Bank();

                    //objAdmin_Bank.TicketName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Ticket_Name.ToLower());
                    //objAdmin_Bank.TicketId = Ticket_Id;
                    //objAdmin_Bank.DumpTicket = DumpTicket_Name;
                    //objAdmin_Bank.Isactive = Isactive;
                    //objAdmin_Bank.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    //objAdmin_Bank.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    //objAdmin_Bank.Subject = Subject;
                    //objAdmin_Bank.Description = Description;
                    //objAdmin_Bank.Priority = Priority;

                    //DataSet objDataSet = Admin_Bank.Ticket_Send_To_DB(objAdmin_Bank);
                    //if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    //{
                    //    if (Ticket_Id == 0)
                    //    {
                    //        ShowNotification("Ticket", "Inserted Successfully..", NotificationType.success);
                           
                    //    }
                    //    else
                    //    {
                    //        ShowNotification("Ticket", "Updated Successfully..", NotificationType.success);
                           
                    //    }
                    //}
                    //else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    //{
                    //    ShowNotification("Ticket", "Ticket Already Existed..!", NotificationType.error);
                       
                    //}
                    //else
                    //    ShowNotification("Ticket", "Not inserted..!", NotificationType.error);

               
            }
            else
                ShowNotification("Ticket", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }
}

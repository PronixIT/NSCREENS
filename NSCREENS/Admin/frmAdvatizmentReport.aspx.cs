using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmAdvatizmentReport : System.Web.UI.Page
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

                CheckBoxList(lstUsers, "User_Id", "Username", "Select User_Id,Username from tbl_user U where Isactive='true' and Staff_Id!=0 order by Username");
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True'");
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
            if (txtStartDate.Text.Trim() != "")
                AddColum = AddColum + " StartDate='" + txtStartDate.Text.Trim() + "' and ";
            if (txtEndDate.Text.Trim() != "")
                AddColum = AddColum + " EndDate='" + txtEndDate.Text.Trim() + "' and ";
            if (ddlStatus.SelectedIndex != 0)
                AddColum = AddColum + " Status='" + ddlStatus.SelectedItem.ToString() + "' and ";
            //if (ddlUser.SelectedIndex != 0)
            //    AddColum = AddColum + " CreatedById='" + ddlUser.SelectedItem.ToString() + "' and ";

            string CoveredAreaIds = ",";
            for (int i = 0; i < lstUsers.Items.Count; i++)
                if (lstUsers.Items[i].Selected)
                    CoveredAreaIds = CoveredAreaIds + lstUsers.Items[i].Value.ToString() + ",";

            if (CoveredAreaIds != ",")
                AddColum = AddColum + " CreatedById in (0" + CoveredAreaIds + "0) and ";

            if (AddColum != "")
            {
                AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True' and " + AddColum);
            }
            else
            {
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Advertisement where Isactive='True'");
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
            DataSet objDataSet = MasterCode.RetrieveQuery("Select EmailId from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id in (Select CreatedById from tbl_Advertisement where Advertisement_Id=" + lnk.CommandName + "))");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                lblDumpEmail.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
            }
            else
            {
                lblDumpEmail.Text = "";
            }
            Display_List(gvViews, "Select Visits_Id,Advertisement_Id,Username,CONVERT(varchar(max),Date_Time,100)as Date_Time,City_Id,IPAddress,(Select Name from tbl_Register_User R where R.Register_Id=U.Staff_Id)as Name,(Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id)as EmailId from tbl_Advertizment_Visits V,tbl_user U where Advertisement_Id=" + lnk.CommandName + " and V.User_Id=U.User_Id");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblDumpEmail.Text != "")
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        gvViews.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        MailMessage mm = new MailMessage("nscreens.eluru@gmail.com", lblDumpEmail.Text);
                        mm.Subject = "Advertisement Views";
                        mm.Body = "Advertisement Views : <hr />" + sw.ToString(); ;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = "nscreens.eluru@gmail.com";
                        NetworkCred.Password = "9885908149";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                        ShowNotification("Advertisement", "Mail Sent successfully..!", NotificationType.success);
                    }
                }
            }
            else
                ShowNotification("Advertisement", "This Video upload by Admin..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
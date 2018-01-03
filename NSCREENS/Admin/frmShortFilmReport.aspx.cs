using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
                DropDownList(ddlSearchTitle, "Short_film_Id", "Title", "Select Short_film_Id,Title from tbl_Short_film where Isactive='True' Order by Title", "Select");
                DropDownList(ddlSearchCategory, "Category_Id", "Category_Name", "Select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name", "Select");
                DropDownList(ddlSearchChannel, "Channel_Id", "Channel_Name", "Select Channel_Id,Channel_Name from tbl_admin_channel where Isactive='True' Order by Channel_Name", "Select");

                Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,LS.Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' Order by Title");
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
            if (ddlSearchChannel.SelectedIndex != 0)
                AddColums = AddColums + " Channel=" + ddlSearchChannel.SelectedValue.ToString() + " and ";
            if (ddlStatus.SelectedIndex != 0)
                AddColums = AddColums + " LS.Status='" + ddlStatus.SelectedItem.ToString() + "' and ";

            string CoveredAreaIds = ",";
            for (int i = 0; i < lstUsers.Items.Count; i++)
                if (lstUsers.Items[i].Selected)
                    CoveredAreaIds = CoveredAreaIds + lstUsers.Items[i].Value.ToString() + ",";

            if (CoveredAreaIds != ",")
                AddColums = AddColums + " LS.CreatedById in (0" + CoveredAreaIds + "0) and ";

            if (AddColums != "")
            {
                AddColums = AddColums.Remove(AddColums.Length - 4, 4);
                Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,LS.Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and " + AddColums);
            }
            else
                Display_List(gvSearchFilm, "SELECT LS.Short_film_Id,Title,Tag,Category,LS.Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.CreatedDate,LS.CreatedById,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,LS.Visits FROM tbl_Short_film  SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' Order by Title");
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

            DataSet objDataSet = MasterCode.RetrieveQuery("Select EmailId from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id in (Select CreatedById from tbl_Short_film where Short_film_Id=" + lnk.CommandName + "))");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                lblDumpEmail.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
            }
            else
            {
                lblDumpEmail.Text = "";
            }

            Display_List(gvViews, "Select Visits_Id,Short_Film_Id,Username,CONVERT(varchar(max),Date_Time,100)as Date_Time,City_Id,IPAddress,(Select Name from tbl_Register_User R where R.Register_Id=U.Staff_Id)as Name,(Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id)as EmailId from tbl_Short_Film_Visits V,tbl_user U where Short_Film_Id=" + lnk.CommandName + " and V.User_Id=U.User_Id");

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
                        mm.Subject = "ShortFilm Views";
                        mm.Body = "ShortFilm Views : <hr />" + sw.ToString(); ;
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
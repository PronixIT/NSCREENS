using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using System.IO;

public partial class Admin_frmAdvertisementApproval : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    String myUrl = Request.RawUrl.ToString();
                    var result = Path.GetFileName(myUrl);
                    String Folder = Path.GetDirectoryName(myUrl);
                    string[] SplitOffer = Folder.Split('\\');
                    for (int i = 0; i < SplitOffer.Length; i++)
                        if (i == 1)
                            Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;
                    //SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Requested'
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvShortFilm, "SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Requested'");
                    else
                        Display_List(gvShortFilm, "SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Requested' and LS.CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
                }
                else
                    ShowNotification("Short Film", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void gvShortFilm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                lblAdvertisementId.Text = (gvShortFilm.Rows[index].FindControl("lblGridLan_Short_film_Id") as Label).Text;
                txtAppTitle.Text = (gvShortFilm.Rows[index].FindControl("lblGridTitle") as Label).Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Language_Short_FilmId set Status='" + ddlAppStatus.SelectedItem.ToString() + "' where Lan_Short_film_Id=" + lblAdvertisementId.Text);

            DataSet objDataSet2 = MasterCode.RetrieveQuery("SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and  Lan_Short_film_Id=" + lblAdvertisementId.Text);

            SendMessage objSendMessage = new SendMessage();

            objSendMessage.SendFrom = Session["UserId"].ToString();
            objSendMessage.SendTo = objDataSet2.Tables[0].Rows[0]["EmailId"].ToString();
            objSendMessage.Message = "Hello " + Session["Name"].ToString() + ",<br/><br/>&nbsp;&nbsp;&nbsp;Your film " + txtAppTitle.Text + " has been approved successfully.You can publish it anytime in the option Publish shortfilm located at the top right." +
                 "All the best for your film. <br/><br/>With Love,<br/>Nscreens";
            objSendMessage.CreatedDate = DateTime.Now;
            objSendMessage.Subject = "Approve " + txtAppTitle.Text;

            DataSet objDataSet1 = SendMessage.Send_Message(objSendMessage);

            ShowNotification("Short film", "Short film " + ddlAppStatus.SelectedItem.ToString() + " Successfully..", NotificationType.success);
            if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                Display_List(gvShortFilm, "SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Requested'");
            else
                Display_List(gvShortFilm, "SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Requested' and LS.CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
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

            DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Lan_Short_film_Id,SF.Short_film_Id,Title,Tag,Category,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,LS.Status,LS.Video,LS.Isactive,case when LS.Status='Approve' then 'label label-success' else case when LS.Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,(Select Language_Name from tbl_admin_language where Language_Id=LS.Language)as Language,(Select (Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id) from tbl_user U where U.User_Id=SF.CreatedById)as EmailId FROM tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and  Lan_Short_film_Id=" + (Row.FindControl("lblGridLan_Short_film_Id") as Label).Text);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();
                txtLanguages.Text = objDataSet.Tables[0].Rows[0]["Language"].ToString();
                txtChannel_Name.Text = objDataSet.Tables[0].Rows[0]["Channel_Name"].ToString();
                txtDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                txtEmail.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Video"].ToString() + "' height='376px' width='100%' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                //playvideo.Attributes.Add("scr", objDataSet.Tables[0].Rows[0]["Video"].ToString());

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupView()", true);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
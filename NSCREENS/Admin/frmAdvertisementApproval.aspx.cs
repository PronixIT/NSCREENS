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

                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and Status='Requested'");
                    else
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and  and Status='Requested' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
                }
                else
                    ShowNotification("Advertisement", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void gvadvertisement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                lblAdvertisementId.Text = (gvadvertisement.Rows[index].FindControl("lblGridAdvertisement_Id") as Label).Text;
                txtAppTitle.Text = (gvadvertisement.Rows[index].FindControl("lblGridTitle") as Label).Text; ;

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
            DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Advertisement set Status='" + ddlAppStatus.SelectedItem.ToString() + "' where Advertisement_Id=" + lblAdvertisementId.Text);

            DataSet objDataSet2 = MasterCode.RetrieveQuery("SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,A.CreatedDate,A.CreatedById,A.ModifiedDate,A.ModifiedById,A.Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Gender,Agefrom+'-'+Ageto as Age,Video as Video,(Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id)as EmailId FROM tbl_Advertisement A join tbl_User U on A.CreatedById=U.User_Id where A.Isactive='True' and Advertisement_Id=" + lblAdvertisementId.Text);

            SendMessage objSendMessage = new SendMessage();

            objSendMessage.SendFrom = Session["UserId"].ToString();
            objSendMessage.SendTo = objDataSet2.Tables[0].Rows[0]["EmailId"].ToString();
            objSendMessage.Message = "Hello " + Session["Name"].ToString() + ",<br/><br/>&nbsp;&nbsp;&nbsp;Your advertisement " + txtAppTitle.Text + " has been approved successfully.Hope it works well for your product.<br/><br/>With Love,<br/>Nscreens";
            objSendMessage.CreatedDate = DateTime.Now;
            objSendMessage.Subject = "Approve " + txtAppTitle.Text;

            DataSet objDataSet1 = SendMessage.Send_Message(objSendMessage);

            ShowNotification("Advertisement", "Advertisement " + ddlAppStatus.SelectedItem.ToString() + " Successfully..", NotificationType.success);
            if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and Status='Requested'");
            else
                Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and  and Status='Requested' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
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

            DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,A.CreatedDate,A.CreatedById,A.ModifiedDate,A.ModifiedById,A.Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Gender,Agefrom+'-'+Ageto as Age,Video as Video,(Select EmailId from tbl_Register_User R where R.Register_Id=U.Staff_Id)as EmailId FROM tbl_Advertisement A join tbl_User U on A.CreatedById=U.User_Id where A.Isactive='True' and Advertisement_Id=" + (Row.FindControl("lblGridAdvertisement_Id") as Label).Text);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                txtEmail.Text = objDataSet.Tables[0].Rows[0]["EmailId"].ToString();
                txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();
                txtNoofVisits.Text = objDataSet.Tables[0].Rows[0]["NoofVisits"].ToString();
                txtStartDate.Text = objDataSet.Tables[0].Rows[0]["StartDate"].ToString();
                txtEndDate.Text = objDataSet.Tables[0].Rows[0]["EndDate"].ToString();
                txtGender.Text = objDataSet.Tables[0].Rows[0]["Gender"].ToString();
                txtAge.Text = objDataSet.Tables[0].Rows[0]["Age"].ToString();
                //txtCity.Text = objDataSet.Tables[0].Rows[0]["City_Name"].ToString();
                txtDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Video"].ToString() + "' height='376px' width='100%' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                //playvideo.Attributes.Add("scr", objDataSet.Tables[0].Rows[0]["Video"].ToString());
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupView()", true);
        }
        catch (Exception Ex)
        {

        }
    }
}
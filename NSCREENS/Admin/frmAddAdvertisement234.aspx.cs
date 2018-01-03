using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.IO;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Text;

public partial class Admin_frmAddAdvertisement : System.Web.UI.Page
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

    public void Clear_Advertisement()
    {
        txtTitle.Text = "";
        txtTag.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtNoofVisits.Text = "";
        txtBudget.Text = "";
        txtPromoCode.Text = "";
        txtDescription.Text = "";
    }

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
            GoogleConnect.ClientId = "87129835825-ej8e7d4avtumoscft9n8f4t3en59v53r.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "lxWzgwPCeFalM2CA50blJgSG";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            GoogleConnect.API = EnumAPI.Drive;

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    Session["File"] = fudUploadVideo.PostedFile;
                    Session["Description"] = txtTitle.Text;

                    MailMessage mail = new MailMessage();

                     GoogleConnect.Authorize("https://www.googleapis.com/auth/drive.file");
                    //GoogleConnect.Authorize("https://script.google.com/macros/s/AKfycbwuhtSthejPvSqcC0pNR2Blw1yuwyqIj5hvyd_JPJIdeNSlD_tn/exec");
                }
                if (Session["UserId"] != null)
                {
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True'");
                    else
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
                }
                else
                    ShowNotification("Advertisement", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
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

            string Video = fudUploadVideo.FileName;
            if (txtTitle.Text.Trim() != "" && txtTag.Text.Trim() != "" && txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "" && txtNoofVisits.Text.Trim() != "" && txtBudget.Text.Trim() != "" && txtPromoCode.Text.Trim() != "" && txtDescription.Text.Trim() != "" && Video != "")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    Session["File"] = fudUploadVideo.PostedFile;
                    Session["Description"] = txtTitle.Text;
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.PostFile(code, (HttpPostedFile)Session["File"], Session["Description"].ToString());
                    GoogleDriveFile file = (new JavaScriptSerializer()).Deserialize<GoogleDriveFile>(json);
                    //tblFileDetails.Visible = true;
                    //lblTitle.Text = file.Title;
                    //lblId.Text = file.Id;
                    //imgIcon.ImageUrl = file.IconLink;
                    //lblCreatedDate.Text = file.CreatedDate.ToString();
                    //lnkDownload.NavigateUrl = file.WebContentLink;
                    lblVideoLLink.Text = file.WebContentLink;

                    Advertisement objAdvertisement = new Advertisement();

                    string FileExt = Path.GetExtension(fudUploadVideo.FileName);

                    objAdvertisement.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtTitle.Text.Trim());
                    objAdvertisement.Tag = txtTag.Text.Trim();
                    objAdvertisement.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    objAdvertisement.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                    objAdvertisement.NoofVisits = txtNoofVisits.Text.Trim();
                    objAdvertisement.Budget = Convert.ToDecimal(txtBudget.Text.Trim());
                    objAdvertisement.PromoCode = txtPromoCode.Text.Trim();
                    objAdvertisement.Description = txtDescription.Text.Trim();
                    string[] Split = (lblVideoLLink.Text).Split('=');

                    objAdvertisement.Video = Split[0] + "=" + Split[1] + "=view";
                    objAdvertisement.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdvertisement.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);

                    DataSet objDataSet = Advertisement.Advertisement_Send_To_DB(objAdvertisement);
                    if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                    {
                        ShowNotification("Advertisement", "inserted Successfully..", NotificationType.success);

                        Clear_Advertisement();
                        if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                            Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True'");
                        else
                            Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
                    }
                    else
                        ShowNotification("Advertisement", "not inserted..", NotificationType.error);
                }
                else
                {
                    ShowNotification("Advertisement", "Please Fill All Fields..!", NotificationType.error);
                }



                //if (!string.IsNullOrEmpty(file.ThumbnailLink))
                //{
                //    rowThumbnail.Visible = true;
                //    imgThumbnail.ImageUrl = file.ThumbnailLink;
                //}
            }
            if (Request.QueryString["error"] == "access_denied")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Advertisement();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void gvadvertisement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

        }
    }

    public class GoogleDriveFile
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string OriginalFilename { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string WebContentLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
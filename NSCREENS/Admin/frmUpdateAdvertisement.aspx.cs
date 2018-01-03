using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

public partial class Admin_frmUpdateAdvertisement : System.Web.UI.Page
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
        ddlSearchCategory.SelectedIndex = 0;
        ddlSearchChannel.SelectedIndex = 0;
        ddlSearchTitle.SelectedIndex = 0;
        gvSearchFilm.DataSource = "";
        gvSearchFilm.DataBind();
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
            if (!IsPostBack)
            {
                String myUrl = Request.RawUrl.ToString();
                var result = Path.GetFileName(myUrl);
                String Folder = Path.GetDirectoryName(myUrl);
                string[] SplitOffer = Folder.Split('\\');
                for (int i = 0; i < SplitOffer.Length; i++)
                    if (i == 1)
                        Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                DropDownList(ddlSearchTitle, "Short_film_Id", "Title", "Select Short_film_Id,Title from tbl_Short_film where Isactive='True' Order by Title", "Select");
                DropDownList(ddlSearchCategory, "Category_Id", "Category_Name", "Select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name", "Select");
                DropDownList(ddlSearchChannel, "Channel_Id", "Channel_Name", "Select Channel_Id,Channel_Name from tbl_admin_channel where Isactive='True' Order by Channel_Name", "Select");

                DropDownList(ddlCity, "City_Id", "City_Name", "Select City_Id,City_Name from tbl_admin_city where Isactive='true' order by City_Name", "Select");

                if (Session["UserId"] != null)
                {
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and Status!='Approve' and Status!='Approve'");
                    else
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and Status!='Approve' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
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
            string Image = fupImage.FileName;
            if (txtTitle.Text.Trim() != "" && txtTag.Text.Trim() != "" && txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "" && txtNoofVisits.Text.Trim() != "" && txtBudget.Text.Trim() != "" && txtPromoCode.Text.Trim() != "" && txtDescription.Text.Trim() != "" && lblAdvertisementId.Text.Trim() != "0")
            {
                Advertisement objAdvertisement = new Advertisement();

                string FileExt = Path.GetExtension(fudUploadVideo.FileName);

                string ShortId = ",";
                for (int i = 0; i < gvSearchFilm.Rows.Count; i++)
                    if ((gvSearchFilm.Rows[i].FindControl("rdbSelect") as RadioButton).Checked == true)
                        ShortId = (gvSearchFilm.Rows[i].FindControl("lblGridAdvertisement_Id") as Label).Text;

                objAdvertisement.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtTitle.Text.Trim());
                objAdvertisement.Tag = txtTag.Text.Trim();
                objAdvertisement.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                objAdvertisement.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                objAdvertisement.NoofVisits = txtNoofVisits.Text.Trim();
                objAdvertisement.Budget = Convert.ToDecimal(txtBudget.Text.Trim());
                objAdvertisement.PromoCode = txtPromoCode.Text.Trim();
                objAdvertisement.Description = txtDescription.Text.Trim();

                objAdvertisement.Video = FileExt;
                objAdvertisement.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objAdvertisement.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                objAdvertisement.ShortFilmId = ShortId;
                objAdvertisement.Image = Image;
                objAdvertisement.AdvertisementId = Convert.ToInt32(lblAdvertisementId.Text.Trim());

                DataSet objDataSet = Advertisement.Advertisement_Send_To_DB(objAdvertisement);
                if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                {
                    if (Video != "")
                        fudUploadVideo.SaveAs(Server.MapPath("~/Videos/") + "Video_" + objDataSet.Tables[0].Rows[0][0].ToString() + FileExt);

                    if (Image != "")
                        fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Advatizments_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    ShowNotification("Advertisement", "Updated Successfully..", NotificationType.success);

                    Clear_Advertisement();
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and Status!='Approve'");
                    else
                        Display_List(gvadvertisement, "SELECT Advertisement_Id,Title,Tag,convert(varchar(12),StartDate,100)as StartDate,convert(varchar(12),EndDate,100)as EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Advertisement where Isactive='True' and Status!='Approve' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));
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
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataSet objDataSet = MasterCode.RetrieveQuery("SELECT City_Id,Advertisement_Id,Title,Tag,StartDate,EndDate,NoofVisits,Budget,PromoCode,Status,Description,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,ShortFilmId,Advertisement_Image FROM tbl_Advertisement where Advertisement_Id=" + (gvadvertisement.Rows[index].FindControl("lblGridAdvertisement_Id") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    lblAdvertisementId.Text = objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString();
                    txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                    txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();
                    txtStartDate.Text = Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["StartDate"].ToString()).ToString("yyyy-MM-dd");
                    txtEndDate.Text = Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["EndDate"].ToString()).ToString("yyyy-MM-dd");
                    txtNoofVisits.Text = objDataSet.Tables[0].Rows[0]["NoofVisits"].ToString();
                    txtBudget.Text = objDataSet.Tables[0].Rows[0]["Budget"].ToString();
                    txtDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                    //txtPromoCode.Text = objDataSet.Tables[0].Rows[0][""].ToString();

                    ddlCity.ClearSelection();
                    if (ddlCity.Items.FindByValue(objDataSet.Tables[0].Rows[0]["City_Id"].ToString()) != null)
                        ddlCity.Items.FindByValue(objDataSet.Tables[0].Rows[0]["City_Id"].ToString()).Selected = true;

                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvSearchFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True'");
                    else
                        Display_List(gvSearchFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

                    for (int i = 0; i <= gvSearchFilm.Rows.Count; i++)
                        if (Convert.ToInt32((gvSearchFilm.Rows[i].FindControl("lblGridAdvertisement_Id") as Label).Text) == Convert.ToInt32(objDataSet.Tables[0].Rows[0]["ShortFilmId"].ToString()))
                            (gvSearchFilm.Rows[i].FindControl("rdbSelect") as RadioButton).Checked = true;

                }
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
                AddColums = AddColums + " Short_film_Id=" + ddlSearchTitle.SelectedValue.ToString() + " and ";
            if (ddlSearchCategory.SelectedIndex != 0)
                AddColums = AddColums + " Category=" + ddlSearchCategory.SelectedValue.ToString() + " and ";
            if (ddlSearchChannel.SelectedIndex != 0)
                AddColums = AddColums + " Channel=" + ddlSearchChannel.SelectedValue.ToString() + " and ";

            if (AddColums != "")
            {
                AddColums = AddColums.Remove(AddColums.Length - 4, 4);

                if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                    Display_List(gvSearchFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and " + AddColums);
                else
                    Display_List(gvSearchFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()) + " and " + AddColums);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
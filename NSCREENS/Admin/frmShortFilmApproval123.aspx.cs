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

public partial class Admin_frmShortFilmApproval : System.Web.UI.Page
{
    DataTable objDataTable = new DataTable();

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

    public void Clear_Short_Film()
    {
        txtTitle.Text = "";
        txtTag.Text = "";
        txtDescription.Text = "";
        txtTitle.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlChannel.SelectedIndex = 0;
        ddlLanguage.SelectedIndex = 0;
        lblTitleAvailable.Text = "";

        ViewState["Artist"] = null;
        Display_List(gvArtist, "select null as Id,null as ArtistId,''as Artist,'' as Name");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    DropDownList(ddlCategory, "Category_Id", "Category_Name", "select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name", "Select");
                    DropDownList(ddlChannel, "Channel_Id", "Channel_Name", "Select Channel_Id,Channel_Name from tbl_admin_channel where Isactive='True' Order by Channel_Name", "Select");
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Short_film SF where Isactive='True' Order by Short_film_Id desc");
                    else
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

                    Display_List(gvArtist, "select null as Id,null as ArtistId,''as Artist,'' as Name");

                    DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                }
                else
                    ShowNotification("Short Film", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
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
            if (txtTitle.Text.Trim() != "" && txtTag.Text.Trim() != "" && ddlCategory.SelectedIndex != 0 && ddlChannel.SelectedIndex != 0 && txtDescription.Text.Trim() != "" && Video != "" && Image != "" && ViewState["Artist"] != null)
            {
                Short_Film objShort_Film = new Short_Film();

                string FileExt = Path.GetExtension(fudUploadVideo.FileName);

                objShort_Film.Title = txtTitle.Text.Trim();
                objShort_Film.Tag = txtTag.Text.Trim();
                objShort_Film.Category = ddlCategory.SelectedValue.ToString();
                objShort_Film.Language = ddlLanguage.SelectedItem.ToString();
                objShort_Film.Channel = Convert.ToInt32(ddlChannel.SelectedValue.ToString());
                objShort_Film.Video = FileExt;
                objShort_Film.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                objShort_Film.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objShort_Film.Description = txtDescription.Text.Trim();
                objShort_Film.ShortIds = "";
                objShort_Film.Image = Image;
                objShort_Film.Short_film_Id = 0;
                objShort_Film.Duration = Convert.ToDecimal(txtDuration.Text.Trim());

                DataSet objDataSet = Short_Film.Short_Film_Send_To_DB(objShort_Film, (DataTable)ViewState["Artist"]);
                if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                {
                    if (Video != "")
                        fudUploadVideo.SaveAs(Server.MapPath("~/Videos/") + "Shortfilm_" + objDataSet.Tables[0].Rows[0][0].ToString() + FileExt);

                    if (Image != "")
                        fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Shortfilm_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    ShowNotification("Short Film", "inserted Successfully..", NotificationType.success);

                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Short_film SF where Isactive='True' Order by Short_film_Id desc");
                    else
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr,Visits FROM tbl_Short_film SF where Isactive='True' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

                    Clear_Short_Film();
                }
            }
            else
            {
                ShowNotification("Short Film", "Please Fill All Fields..!", NotificationType.error);
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
            Clear_Short_Film();
        }
        catch (Exception Ex)
        {

        }
    }

    protected void gvShortFilm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;

            if ((Row.FindControl("txtName") as TextBox).Text != "" && (Row.FindControl("ddlArtist") as DropDownList).SelectedIndex != 0)
            {
                if (ViewState["Artist"] == null)
                {
                    objDataTable.Columns.Add("Id", typeof(Int32));
                    objDataTable.Columns.Add("ArtistId", typeof(Int32));
                    objDataTable.Columns.Add("Artist", typeof(String));
                    objDataTable.Columns.Add("Name", typeof(String));
                    objDataTable.Columns["Id"].AutoIncrementSeed = 1;
                    objDataTable.Columns["Id"].AutoIncrement = true;
                }
                else
                    objDataTable = (DataTable)ViewState["Artist"];

                DataRow objDataRow = objDataTable.NewRow();

                objDataRow["ArtistId"] = (Row.FindControl("ddlArtist") as DropDownList).SelectedValue;
                objDataRow["Artist"] = (Row.FindControl("ddlArtist") as DropDownList).SelectedItem;
                objDataRow["Name"] = (Row.FindControl("txtName") as TextBox).Text;

                objDataTable.Rows.Add(objDataRow);

                ViewState["Artist"] = objDataTable;

                gvArtist.DataSource = (DataTable)ViewState["Artist"];
                gvArtist.DataBind();

                DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objButton = sender as LinkButton;
            GridViewRow Row = (GridViewRow)objButton.NamingContainer;

            string ArtistId = (Row.FindControl("lblId") as Label).Text;

            DataTable objDataTable = new DataTable();
            objDataTable = (DataTable)ViewState["Artist"];
            if (objDataTable != null)
            {
                DataRow[] drr = objDataTable.Select("Id='" + (Row.FindControl("lblId") as Label).Text + "'");

                for (int i = 0; i < drr.Length; i++)
                    objDataTable.Rows.Remove(drr[i]);

                objDataTable.AcceptChanges();
                ViewState["Artist"] = objDataTable;
                if (objDataTable.Rows.Count > 0)
                {
                    gvArtist.DataSource = (DataTable)ViewState["Artist"];
                    gvArtist.DataBind();
                }
                else
                    Display_List(gvArtist, "select null as Id,null as ArtistId,''as Artist,'' as Name");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void txtTitle_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtTitle.Text.Trim() != "")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Select COUNT(*) from tbl_Short_film where Title='" + txtTitle.Text.Trim() + "'");
                DataSet objDataSet1 = MasterCode.RetrieveQuery("select COUNT(*) from tbl_Register_Title where Isactive='True' and Title_Name='" + txtTitle.Text.Trim() + "'");
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) == 0 && Convert.ToInt32(objDataSet1.Tables[0].Rows[0][0].ToString()) == 0)
                {
                    lblTitleAvailable.Text = "Available";
                    lblTitleAvailable.CssClass = "label label-success";
                }
                else
                {
                    txtTitle.Text = "";
                    lblTitleAvailable.Text = "Unavailable";
                    lblTitleAvailable.CssClass = "label label-danger";
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
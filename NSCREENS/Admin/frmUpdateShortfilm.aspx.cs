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

public partial class Admin_frmUpdateShortfilm : System.Web.UI.Page
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
        lblShortFilmId.Text = "0";
        txtTitle.Text = "";
        txtTag.Text = "";
        txtDescription.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlChannel.SelectedIndex = 0;
        ddlLanguage.SelectedIndex = 0;

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
                    String myUrl = Request.RawUrl.ToString();
                    var result = Path.GetFileName(myUrl);
                    String Folder = Path.GetDirectoryName(myUrl);
                    string[] SplitOffer = Folder.Split('\\');
                    for (int i = 0; i < SplitOffer.Length; i++)
                        if (i == 1)
                            Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

                    DropDownList(ddlCategory, "Category_Id", "Category_Name", "select Category_Id,Category_Name from tbl_admin_category where Isactive='True' Order by Category_Name", "Select");
                    DropDownList(ddlChannel, "Channel_Id", "Channel_Name", "Select Channel_Id,Channel_Name from tbl_admin_channel where Isactive='True' Order by Channel_Name", "Select");
                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and Status!='Approve' and Status!='Approve'");
                    else
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and Status!='Approve' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

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
            if (txtTitle.Text.Trim() != "" && txtTag.Text.Trim() != "" && ddlCategory.SelectedIndex != 0 && ddlChannel.SelectedIndex != 0 && txtDescription.Text.Trim() != "" && lblShortFilmId.Text.Trim() != "0" && ViewState["Artist"] != null)
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
                objShort_Film.Short_film_Id = Convert.ToInt32(lblShortFilmId.Text.Trim());

                DataSet objDataSet = Short_Film.Short_Film_Send_To_DB(objShort_Film, (DataTable)ViewState["Artist"]);
                if (objDataSet.Tables[0].Rows[0][1].ToString() == "1")
                {
                    if (Video != "")
                        fudUploadVideo.SaveAs(Server.MapPath("~/Videos/") + "Shortfilm_" + objDataSet.Tables[0].Rows[0][0].ToString() + FileExt);

                    if (Image != "")
                        fupImage.SaveAs(Server.MapPath("~/Video_Images/") + "Shortfilm_" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                    ShowNotification("Short Film", "Updated Successfully..", NotificationType.success);

                    if (Convert.ToInt32(Session["UserId"].ToString()) == 1)
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and Status!='Approve'");
                    else
                        Display_List(gvShortFilm, "SELECT Short_film_Id,Title,Tag,Hero,Heroine,Technician,(Select Category_Name from tbl_admin_category AC where AC.Category_Id=SF.Category)as Category_Name,Category,Language,(Select Channel_Name from tbl_admin_channel AC where AC.Channel_Id=SF.Channel)as Channel_Name,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,case when Status='Approve' then 'label label-success' else case when Status='Unapprove' then 'label label-danger' else 'label label-warning' end end as StatusClr FROM tbl_Short_film SF where Isactive='True' and Status!='Approve' and CreatedById=" + Convert.ToInt32(Session["UserId"].ToString()));

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
            if (e.CommandName.Equals("detail"))
            {
                ViewState["Artist"] = null;
                int index = Convert.ToInt32(e.CommandArgument);
                DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Short_film_Id,Title,Tag,Category,Language,Channel,Description,Status,Video,CreatedDate,CreatedById,ModifiedDate,ModifiedById,Isactive,Visits,ShortIds,Short_film_Image FROM tbl_Short_film where Short_film_Id=" + (gvShortFilm.Rows[index].FindControl("lblGridAdvertisement_Id") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    DataSet objDataSetArtist = MasterCode.RetrieveQuery("Select Short_Artist_Id,Artist_Id,(Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=SA.Artist_Id)as Artist,Name from tbl_Short_Artist SA where Isactive='true' and Short_Film_Id=" + objDataSet.Tables[0].Rows[0]["Short_film_Id"].ToString());
                    if (objDataSetArtist.Tables[0].Rows.Count > 0)
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

                        for (int i = 0; i < objDataSetArtist.Tables[0].Rows.Count; i++)
                        {
                            DataRow objDataRow = objDataTable.NewRow();

                            objDataRow["ArtistId"] = objDataSetArtist.Tables[0].Rows[i]["Artist_Id"].ToString();
                            objDataRow["Artist"] = objDataSetArtist.Tables[0].Rows[i]["Artist"].ToString();
                            objDataRow["Name"] = objDataSetArtist.Tables[0].Rows[i]["Name"].ToString();

                            objDataTable.Rows.Add(objDataRow);
                        }
                        ViewState["Artist"] = objDataTable;

                        gvArtist.DataSource = (DataTable)ViewState["Artist"];
                        gvArtist.DataBind();
                    }
                    else
                    {
                        DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");
                    }

                    lblShortFilmId.Text = objDataSet.Tables[0].Rows[0]["Short_film_Id"].ToString();
                    txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();

                    ddlLanguage.ClearSelection();
                    if (ddlLanguage.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Language"].ToString()) != null)
                        ddlLanguage.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Language"].ToString()).Selected = true;
                    txtDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                    txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();

                    ddlCategory.ClearSelection();
                    if (ddlCategory.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Category"].ToString()) != null)
                        ddlCategory.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Category"].ToString()).Selected = true;

                    ddlChannel.ClearSelection();
                    if (ddlChannel.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Channel"].ToString()) != null)
                        ddlChannel.Items.FindByValue(objDataSet.Tables[0].Rows[0]["Channel"].ToString()).Selected = true;

                    DropDownList((gvArtist.FooterRow.FindControl("ddlArtist") as DropDownList), "Artist_Id", "Artist_Name", "Select Artist_Id,Artist_Name from tbl_admin_Artist where Isactive='True' Order by Artist_Name", "Select");

                    txtDescription.Focus();
                }
            }
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
            DataSet objDataSet = MasterCode.RetrieveQuery("");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {

            }
        }
        catch (Exception Ex)
        {

        }
    }
}
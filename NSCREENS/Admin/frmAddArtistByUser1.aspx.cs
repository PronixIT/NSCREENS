using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmAddArtistByUser : System.Web.UI.Page
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

    public void Artists_List(GridView gdv, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                gdv.DataSource = objDataSet;
                gdv.DataBind();
            }
            else
            {
                ShowNotification("Artist", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Artist", "kgfkjghfkdjghdfkghkf", NotificationType.error);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string Artist_Id = Request.QueryString["Id"];

                DataSet objDataSet = MasterCode.RetrieveQuery("Select Artist_Id,Artist_Name from tbl_admin_Artist where Artist_Id="+Artist_Id);
                if (objDataSet.Tables[0].Rows.Count > 0)
                    lblArtistType.Text = objDataSet.Tables[0].Rows[0][1].ToString();

                Artists_List(gvArtist, "SELECT Artist_Details_Id,Name,Description,Gender,Artist_Name,AD.Isactive FROM tbl_Artist_Details AD Join tbl_admin_Artist TA on AD.Interest_Areas=TA.Artist_Id and Interest_Areas='" + Artist_Id + "'");
            }
        }
        catch (Exception Ex)
        {

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}
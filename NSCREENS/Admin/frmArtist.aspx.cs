using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmState : System.Web.UI.Page
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

    #region Public Methods

    public void Send_Artist_Data(string Artist_Name, int Artist_Id, string DumpArtist_Name, bool Isactive)
    {
        try
        {
            if (Artist_Name != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.ArtistName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Artist_Name.ToLower());
                    objAdmin_State.ArtistId = Artist_Id;
                    objAdmin_State.DumpArtistName = DumpArtist_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    DataSet objDataSet = Admin_State.Artist_Send_To_DB(objAdmin_State);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Artist_Id == 0)
                        {
                            ShowNotification("Artist", "Inserted Successfully..", NotificationType.success);
                            txtArtist.Text = "";
                            Artists_List(gvArtist, "select Artist_Id,Artist_Name,Isactive from tbl_Admin_Artist Order by Artist_Name");
                        }
                        else
                        {
                            ShowNotification("Artist", "Updated Successfully..", NotificationType.success);
                            Artists_List(gvArtist, "select Artist_Id,Artist_Name,Isactive from tbl_Admin_Artist Order by Artist_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Artist", "Artist Already Existed..!", NotificationType.error);
                        if (Artist_Id == 0)
                            txtArtist.Focus();
                        else
                            txtUpdateArtist.Focus();
                    }
                    else
                        ShowNotification("Artist", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Artist", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Artist", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

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

    #endregion

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

                Artists_List(gvArtist, "select Artist_Id,Artist_Name,Isactive from tbl_Admin_Artist Order by Artist_Name");
                txtArtist.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Artist", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            switch (btn.CommandName)
            {
                case "Save":
                    Send_Artist_Data(txtArtist.Text.Trim(), 0, "", true);
                    break;
                case "Clear":
                    txtArtist.Text = "";
                    break;
                case "Update":
                    Send_Artist_Data(txtUpdateArtist.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesArtist.Checked ? true : false);
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Artists_List(gvArtist, "select Artist_Id,Artist_Name,Isactive from tbl_Admin_Artist where Artist_Name Like '" + txtSearch.Text + "%' Order by Artist_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Artist", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvArtist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateArtist.Text = (gvArtist.Rows[index].FindControl("lblGridArtist") as Label).Text;
                lblID.Text = (gvArtist.Rows[index].FindControl("lblGridArtistId") as Label).Text;
                lblDName.Text = (gvArtist.Rows[index].FindControl("lblGridArtist") as Label).Text;

                if ((gvArtist.Rows[index].FindControl("lblGridArtistIsactive") as Label).Text == " Active ") { rdbActiveNoArtist.Checked = false; rdbActiveYesArtist.Checked = true; }
                else { rdbActiveYesArtist.Checked = false; rdbActiveNoArtist.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Artist", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
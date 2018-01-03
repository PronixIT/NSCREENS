using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Globalization;

public partial class Admin_frmChannel : System.Web.UI.Page
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

    public void Send_Channel_Data(string Channel_Name, int Channel_Id, string DumpChannel_Name, bool Isactive, string Description)
    {
        try
        {
            if (Channel_Name != "" && Description != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_Channel objAdmin_Channel = new Admin_Channel();

                    objAdmin_Channel.ChannelName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Channel_Name.ToLower());
                    objAdmin_Channel.ChannelId = Channel_Id;
                    objAdmin_Channel.DumpChannelName = DumpChannel_Name;
                    objAdmin_Channel.Isactive = Isactive;
                    objAdmin_Channel.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_Channel.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdmin_Channel.Description = Description;

                    DataSet objDataSet = Admin_Channel.Channel_Send_To_DB(objAdmin_Channel);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Channel_Id == 0)
                        {
                            ShowNotification("Channel", "Inserted Successfully..", NotificationType.success);
                            txtChannel.Text = "";
                            txtDescription.Text = "";
                            Channels_List(gvChannel, "select Channel_Id,Channel_Name,Isactive,Description from tbl_Admin_Channel Order by Channel_Name");
                        }
                        else
                        {
                            ShowNotification("Channel", "Updated Successfully..", NotificationType.success);
                            Channels_List(gvChannel, "select Channel_Id,Channel_Name,Isactive,Description from tbl_Admin_Channel Order by Channel_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Channel", "Channel Already Existed..!", NotificationType.error);
                        if (Channel_Id == 0)
                            txtChannel.Focus();
                        else
                            txtUpdateChannel.Focus();
                    }
                    else
                        ShowNotification("Channel", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Channel", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Channel", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Channels_List(GridView gdv, string Query)
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
                ShowNotification("Channel", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Channel", "kgfkjghfkdjghdfkghkf", NotificationType.error);
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Channels_List(gvChannel, "select Channel_Id,Channel_Name,Isactive,Description from tbl_Admin_Channel Order by Channel_Name");
                txtChannel.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Channel", dispErrorMsg, NotificationType.error);
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
                    Send_Channel_Data(txtChannel.Text.Trim(), 0, "", true, txtDescription.Text.Trim());
                    break;
                case "Clear":
                    txtChannel.Text = "";
                    txtDescription.Text = "";
                    break;
                case "Update":
                    Send_Channel_Data(txtUpdateChannel.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesChannel.Checked ? true : false, txtUpdateDescription.Text.Trim());
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Channels_List(gvChannel, "select Channel_Id,Channel_Name,Isactive from tbl_Admin_Channel where Channel_Name Like '" + txtSearch.Text + "%' Order by Channel_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Channel", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvChannel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtUpdateChannel.Text = (gvChannel.Rows[index].FindControl("lblGridChannel") as Label).Text;
                lblID.Text = (gvChannel.Rows[index].FindControl("lblGridChannelId") as Label).Text;
                lblDName.Text = (gvChannel.Rows[index].FindControl("lblGridChannel") as Label).Text;
                txtUpdateDescription.Text = (gvChannel.Rows[index].FindControl("lblGridDescription") as Label).Text;

                if ((gvChannel.Rows[index].FindControl("lblGridChannelIsactive") as Label).Text == " Active ") { rdbActiveNoChannel.Checked = false; rdbActiveYesChannel.Checked = true; }
                else { rdbActiveYesChannel.Checked = false; rdbActiveNoChannel.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Channel", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
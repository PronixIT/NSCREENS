using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_frmSetting : System.Web.UI.Page
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

                DataSet objDataSet = MasterCode.RetrieveQuery("Select Sent_Settings_Id,Form_Settings,Email,SMS from tbl_Settings");
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    chkRegisterEmail.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["Email"].ToString());
                    chkChangePasswordEmail.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[1]["Email"].ToString());
                    chkForgetPasswordEmail.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[2]["Email"].ToString());
                    chkWalletAmountEmail.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[3]["Email"].ToString());

                    chkRegisterSMS.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[0]["SMS"].ToString());
                    chkChangePasswordSMS.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[1]["SMS"].ToString());
                    chkForgetPasswordSMS.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[2]["SMS"].ToString());
                    chkWalletAmountSMS.Checked = Convert.ToBoolean(objDataSet.Tables[0].Rows[3]["SMS"].ToString());
                }
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
            int n = 4;
            for (int i = 1; i <= n; i++)
            {
                string Email1 = "", SMS1 = "";
                if (i == 1)
                {
                    Email1 = chkRegisterEmail.Checked.ToString();
                    SMS1 = chkRegisterSMS.Checked.ToString();
                }

                if (i == 2)
                {
                    Email1 = chkChangePasswordEmail.Checked.ToString();
                    SMS1 = chkChangePasswordSMS.Checked.ToString();
                }

                if (i == 3)
                {
                    Email1 = chkForgetPasswordEmail.Checked.ToString();
                    SMS1 = chkForgetPasswordSMS.Checked.ToString();
                }

                if (i == 4)
                {
                    Email1 = chkWalletAmountEmail.Checked.ToString();
                    SMS1 = chkWalletAmountSMS.Checked.ToString();
                }

                DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Settings set Email='" + Email1 + "',SMS='" + SMS1.ToString() + "' where Sent_Settings_Id=" + i.ToString());

                ShowNotification("Settings", "Changed Successfully..", NotificationType.success);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
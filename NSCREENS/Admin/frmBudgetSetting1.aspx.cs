using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Diagnostics;

public partial class Admin_frmBudgetSetting : System.Web.UI.Page
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

    public void Display()
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Budget_Settings_Id,Budget,Short_Film,Admin,Promoter,Video_Sharing FROM tbl_Budget_Settings");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                txtAddBudget.Text = objDataSet.Tables[0].Rows[1]["Budget"].ToString();
                txtShortFilm.Text = objDataSet.Tables[0].Rows[1]["Short_Film"].ToString();
                txtAdmin.Text = objDataSet.Tables[0].Rows[1]["Admin"].ToString();
                txtVideoSharing.Text = objDataSet.Tables[0].Rows[1]["Video_Sharing"].ToString();

                txtPromoterAddBudget.Text = objDataSet.Tables[0].Rows[0]["Budget"].ToString();
                txtPromoterShortFilm.Text = objDataSet.Tables[0].Rows[0]["Short_Film"].ToString();
                txtPromoterAdmin.Text = objDataSet.Tables[0].Rows[0]["Admin"].ToString();
                txtPromoterPromoter.Text = objDataSet.Tables[0].Rows[0]["Promoter"].ToString();
                txtpromoterVideoSharing.Text = objDataSet.Tables[0].Rows[0]["Video_Sharing"].ToString();

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
                Display();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("State", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            //Button btn = sender as Button;
            //switch (btn.CommandName)
            //{
            //    case "Save":
            //        Send_State_Data(txtState.Text.Trim(), 0, "", true,0,Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
            //        break;
            //    case "Clear":
            //        txtState.Text = "";
            //        break;
            //    case "Update":
            //        Send_State_Data(txtUpdateState.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesState.Checked ? true : false, Convert.ToInt32(lblDumpCountryId.Text.Trim()), Convert.ToInt32(ddlUpdateCountry.SelectedValue.ToString()));
            //        btnClose.Focus();
            //        break;
            //    case "Seach":
            //        //States_List(gvState, "select State_Id,State_Name,Isactive from tbl_Admin_State where State_Name Like '" + txtSearch.Text + "%' Order by State_Name");
            //        break;
            //}

            Button btn = sender as Button;
            switch (btn.CommandName)
            {
                case "Save":
                    if (txtAddBudget.Text.Trim() != "" && txtAdmin.Text.Trim() != "" && txtShortFilm.Text.Trim() != "" && txtVideoSharing.Text.Trim() != "")
                    {
                        if ((Convert.ToDecimal(txtShortFilm.Text.Trim()) + Convert.ToDecimal(txtAdmin.Text.Trim()) + Convert.ToDecimal(txtVideoSharing.Text.Trim())) == Convert.ToDecimal(txtAddBudget.Text.Trim()))
                        {
                            DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtAddBudget.Text.Trim() + ",Short_Film=" + txtShortFilm.Text.Trim() + ",Admin=" + txtAdmin.Text.Trim() + ",Promoter=0,Video_Sharing=" + txtVideoSharing.Text.Trim() + " where Budget_Settings_Id=2");
                            txtAddBudget.Enabled = false;
                            txtShortFilm.Enabled = false;
                            txtAdmin.Enabled = false;
                            txtVideoSharing.Enabled = false;

                            ShowNotification("Budget Settings", "Updated Successfully..", NotificationType.success);
                            Display();
                        }
                        else
                            ShowNotification("Budget Settings", "Invalid Budget..!", NotificationType.error);
                    }
                    else
                        ShowNotification("Budget Settings", "Please Fill All Fields..!", NotificationType.error);

                    break;

                case "PromoterSave":
                    if (txtPromoterAddBudget.Text.Trim() != "" && txtPromoterAdmin.Text.Trim() != "" && txtPromoterShortFilm.Text.Trim() != "" && txtpromoterVideoSharing.Text.Trim() != "" && txtPromoterPromoter.Text.Trim() != "")
                    {
                        if ((Convert.ToDecimal(txtPromoterShortFilm.Text.Trim()) + Convert.ToDecimal(txtPromoterAdmin.Text.Trim()) + Convert.ToDecimal(txtpromoterVideoSharing.Text.Trim()) + Convert.ToDecimal(txtPromoterPromoter.Text.Trim())) == Convert.ToDecimal(txtPromoterAddBudget.Text.Trim()))
                        {
                            DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtPromoterAddBudget.Text.Trim() + ",Short_Film=" + txtPromoterShortFilm.Text.Trim() + ",Admin=" + txtPromoterAdmin.Text.Trim() + ",Promoter=" + txtPromoterPromoter.Text.Trim() + ",Video_Sharing=" + txtpromoterVideoSharing.Text.Trim() + " where Budget_Settings_Id=1");
                            txtPromoterAddBudget.Enabled = false;
                            txtPromoterShortFilm.Enabled = false;
                            txtPromoterAdmin.Enabled = false;
                            txtPromoterPromoter.Enabled = false;
                            txtpromoterVideoSharing.Enabled = false;

                            ShowNotification("Budget Settings with Promoter", "Updated Successfully..", NotificationType.success);
                            Display();
                        }
                        else
                            ShowNotification("Budget Settings with Promoter", "Invalid Budget..!", NotificationType.error);
                    }
                    else
                        ShowNotification("Budget Settings with Promoter", "Please Fill All Fields..!", NotificationType.error);

                    break;

                case "Edit":
                    txtAddBudget.Enabled = true;
                    txtShortFilm.Enabled = true;
                    txtAdmin.Enabled = true;
                    txtVideoSharing.Enabled = true;
                    break;

                case "PromoterEdit":
                    txtPromoterAddBudget.Enabled = true;
                    txtPromoterShortFilm.Enabled = true;
                    txtPromoterAdmin.Enabled = true;
                    txtPromoterPromoter.Enabled = true;
                    txtpromoterVideoSharing.Enabled = true;
                    break;

                case "Clear":
                    txtAddBudget.Text = "";
                    txtShortFilm.Text = "";
                    txtAdmin.Text = "";
                    txtPromoter.Text = "";
                    txtVideoSharing.Text = "";
                    break;

                case "PromoterClear":
                    txtPromoterAddBudget.Text = "";
                    txtPromoterShortFilm.Text = "";
                    txtPromoterAdmin.Text = "";
                    txtPromoterPromoter.Text = "";
                    txtpromoterVideoSharing.Text = "";
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("State", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
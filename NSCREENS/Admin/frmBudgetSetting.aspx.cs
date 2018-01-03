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
                gvSettings.DataSource = objDataSet;
                gvSettings.DataBind();

                //txtAddBudget.Text = objDataSet.Tables[0].Rows[1]["Budget"].ToString();
                //txtShortFilm.Text = objDataSet.Tables[0].Rows[1]["Short_Film"].ToString();
                //txtAdmin.Text = objDataSet.Tables[0].Rows[1]["Admin"].ToString();
                //txtVideoSharing.Text = objDataSet.Tables[0].Rows[1]["Video_Sharing"].ToString();

                //txtAddBudget.Text = objDataSet.Tables[0].Rows[0]["Budget"].ToString();
                //txtShortFilm.Text = objDataSet.Tables[0].Rows[0]["Short_Film"].ToString();
                //txtAdmin.Text = objDataSet.Tables[0].Rows[0]["Admin"].ToString();
                //txtPromoter.Text = objDataSet.Tables[0].Rows[0]["Promoter"].ToString();
                //txtVideoSharing.Text = objDataSet.Tables[0].Rows[0]["Video_Sharing"].ToString();

            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void clear()
    {
        try
        {
            txtAddBudget.Enabled = true;
            txtShortFilm.Enabled = true;
            txtAdmin.Enabled = true;
            txtVideoSharing.Enabled = true;
            txtPromoter.Enabled = true;

            txtAddBudget.Text = "";
            txtShortFilm.Text = "";
            txtAdmin.Text = "";
            txtPromoter.Text = "";
            txtVideoSharing.Text = "";
        }
        catch(Exception Ex)
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
                    if (lblDumpBudId.Text == "2")
                    {
                        lblTitle.Text = "Budget Settings";
                        txtPromoter.Text = "0";
                        txtPromoter.Enabled = false;
                        txtVideoSharing.Enabled = false;
                        txtVideoSharing.Text = "0";

                        if (txtAddBudget.Text.Trim() != "" && txtAdmin.Text.Trim() != "" && txtShortFilm.Text.Trim() != "" && txtVideoSharing.Text.Trim() != "")
                        {
                            if ((Convert.ToDecimal(txtShortFilm.Text.Trim()) + Convert.ToDecimal(txtAdmin.Text.Trim())) == Convert.ToDecimal(txtAddBudget.Text.Trim()))
                            {
                                DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtAddBudget.Text.Trim() + ",Short_Film=" + txtShortFilm.Text.Trim() + ",Admin=" + txtAdmin.Text.Trim() + ",Promoter=" + txtPromoter.Text.Trim() + ",Video_Sharing=" + txtVideoSharing.Text.Trim() + " where Budget_Settings_Id=2");

                                ShowNotification("Budget Settings", "Updated Successfully..", NotificationType.success);
                                Display();
                                clear();
                            }
                            else
                                ShowNotification("Budget Settings", "Invalid Budget..!", NotificationType.error);
                        }
                        else
                            ShowNotification("Budget Settings", "Please Fill All Fields..!", NotificationType.error);
                    }
                    else if (lblDumpBudId.Text == "1")
                    {
                        lblTitle.Text = "Budget Settings With Promoter";
                        txtPromoter.Enabled = true;
                        txtVideoSharing.Enabled = false;

                        if (txtAddBudget.Text.Trim() != "" && txtAdmin.Text.Trim() != "" && txtShortFilm.Text.Trim() != "" && txtVideoSharing.Text.Trim() != "" && txtPromoter.Text.Trim() != "")
                        {
                            if ((Convert.ToDecimal(txtShortFilm.Text.Trim()) + Convert.ToDecimal(txtAdmin.Text.Trim()) + Convert.ToDecimal(txtPromoter.Text.Trim())) == Convert.ToDecimal(txtAddBudget.Text.Trim()))
                            {
                                DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtAddBudget.Text.Trim() + ",Short_Film=" + txtShortFilm.Text.Trim() + ",Admin=" + txtAdmin.Text.Trim() + ",Promoter=" + txtPromoter.Text.Trim() + ",Video_Sharing=" + txtVideoSharing.Text.Trim() + " where Budget_Settings_Id=1");

                                ShowNotification("Budget Settings with Promoter", "Updated Successfully..", NotificationType.success);
                                Display();
                                clear();
                            }
                            else
                                ShowNotification("Budget Settings with Promoter", "Invalid Budget..!", NotificationType.error);
                        }
                        else
                            ShowNotification("Budget Settings with Promoter", "Please Fill All Fields..!", NotificationType.error);
                    }
                    else if (lblDumpBudId.Text == "3")
                    {
                        lblTitle.Text = "Budget Settings with sharing";
                        txtPromoter.Enabled = false;
                        txtVideoSharing.Enabled = true;

                        if (txtAddBudget.Text.Trim() != "" && txtAdmin.Text.Trim() != "" && txtShortFilm.Text.Trim() != "" && txtVideoSharing.Text.Trim() != "" && txtPromoter.Text.Trim() != "")
                        {
                            if ((Convert.ToDecimal(txtShortFilm.Text.Trim()) + Convert.ToDecimal(txtAdmin.Text.Trim()) + Convert.ToDecimal(txtVideoSharing.Text.Trim()) + Convert.ToDecimal(txtPromoter.Text.Trim())) == Convert.ToDecimal(txtAddBudget.Text.Trim()))
                            {
                                DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtAddBudget.Text.Trim() + ",Short_Film=" + txtShortFilm.Text.Trim() + ",Admin=" + txtAdmin.Text.Trim() + ",Promoter=" + txtPromoter.Text.Trim() + ",Video_Sharing=" + txtVideoSharing.Text.Trim() + " where Budget_Settings_Id=3");

                                ShowNotification("Budget Settings with Promoter", "Updated Successfully..", NotificationType.success);
                                Display();
                                clear();
                            }
                            else
                                ShowNotification("Budget Settings with Promoter", "Invalid Budget..!", NotificationType.error);
                        }
                        else
                            ShowNotification("Budget Settings with Promoter", "Please Fill All Fields..!", NotificationType.error);
                    }
                    else if (lblDumpBudId.Text == "4")
                    {
                        lblTitle.Text = "Budget Settings with sharing and promoter";
                        txtPromoter.Enabled = true;
                        txtVideoSharing.Enabled = true;

                        if (txtAddBudget.Text.Trim() != "" && txtAdmin.Text.Trim() != "" && txtShortFilm.Text.Trim() != "" && txtVideoSharing.Text.Trim() != "" && txtPromoter.Text.Trim()!="")
                        {
                            if ((Convert.ToDecimal(txtShortFilm.Text.Trim()) + Convert.ToDecimal(txtAdmin.Text.Trim()) + Convert.ToDecimal(txtVideoSharing.Text.Trim()) + Convert.ToDecimal(txtPromoter.Text.Trim())) == Convert.ToDecimal(txtAddBudget.Text.Trim()))
                            {
                                DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtAddBudget.Text.Trim() + ",Short_Film=" + txtShortFilm.Text.Trim() + ",Admin=" + txtAdmin.Text.Trim() + ",Promoter=" + txtPromoter.Text.Trim() + ",Video_Sharing=" + txtVideoSharing.Text.Trim() + " where Budget_Settings_Id=4");

                                ShowNotification("Budget Settings", "Updated Successfully..", NotificationType.success);
                                Display();
                                clear();
                            }
                            else
                                ShowNotification("Budget Settings", "Invalid Budget..!", NotificationType.error);
                        }
                        else
                            ShowNotification("Budget Settings", "Please Fill All Fields..!", NotificationType.error);
                    }





                    break;

                case "PromoterSave":
                    //if (txtAddBudget.Text.Trim() != "" && txtAdmin.Text.Trim() != "" && txtShortFilm.Text.Trim() != "" && txtVideoSharing.Text.Trim() != "" && txtPromoter.Text.Trim() != "")
                    //{
                    //    if ((Convert.ToDecimal(txtShortFilm.Text.Trim()) + Convert.ToDecimal(txtAdmin.Text.Trim()) + Convert.ToDecimal(txtVideoSharing.Text.Trim()) + Convert.ToDecimal(txtPromoter.Text.Trim())) == Convert.ToDecimal(txtAddBudget.Text.Trim()))
                    //    {
                    //        DataSet objDataSet = MasterCode.RetrieveQuery("update tbl_Budget_Settings set Budget=" + txtAddBudget.Text.Trim() + ",Short_Film=" + txtShortFilm.Text.Trim() + ",Admin=" + txtAdmin.Text.Trim() + ",Promoter=" + txtPromoter.Text.Trim() + ",Video_Sharing=" + txtVideoSharing.Text.Trim() + " where Budget_Settings_Id=1");
                    //        txtAddBudget.Enabled = false;
                    //        txtShortFilm.Enabled = false;
                    //        txtAdmin.Enabled = false;
                    //        txtPromoter.Enabled = false;
                    //        txtVideoSharing.Enabled = false;

                    //        ShowNotification("Budget Settings with Promoter", "Updated Successfully..", NotificationType.success);
                    //        Display();
                    //    }
                    //    else
                    //        ShowNotification("Budget Settings with Promoter", "Invalid Budget..!", NotificationType.error);
                    //}
                    //else
                    //    ShowNotification("Budget Settings with Promoter", "Please Fill All Fields..!", NotificationType.error);

                    break;

                case "Edit":
                    txtAddBudget.Enabled = true;
                    txtShortFilm.Enabled = true;
                    txtAdmin.Enabled = true;
                    txtVideoSharing.Enabled = true;
                    break;

                case "PromoterEdit":
                    //txtAddBudget.Enabled = true;
                    //txtShortFilm.Enabled = true;
                    //txtAdmin.Enabled = true;
                    //txtPromoter.Enabled = true;
                    //txtVideoSharing.Enabled = true;
                    break;

                case "Clear":
                    txtAddBudget.Text = "";
                    txtShortFilm.Text = "";
                    txtAdmin.Text = "";
                    txtPromoter.Text = "";
                    txtVideoSharing.Text = "";
                    break;

                case "PromoterClear":
                    //txtAddBudget.Text = "";
                    //txtShortFilm.Text = "";
                    //txtAdmin.Text = "";
                    //txtPromoter.Text = "";
                    //txtVideoSharing.Text = "";
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

    protected void gvSettings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataSet objDataSet = MasterCode.RetrieveQuery("SELECT Budget_Settings_Id,Budget,Short_Film,Admin,Promoter,Video_Sharing FROM tbl_Budget_Settings where Budget_Settings_Id=" + (gvSettings.Rows[index].FindControl("lblGridBudget_Settings_Id") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    if ((gvSettings.Rows[index].FindControl("lblGridBudget_Settings_Id") as Label).Text == "2")
                    {
                        lblTitle.Text = "Budget Settings";
                        txtPromoter.Text = "0";
                        txtPromoter.Enabled = false;
                        txtVideoSharing.Enabled = false;
                        txtVideoSharing.Text = "0";
                    }
                    else if ((gvSettings.Rows[index].FindControl("lblGridBudget_Settings_Id") as Label).Text == "1")
                    {
                        lblTitle.Text = "Budget Settings With Promoter";
                        txtPromoter.Enabled = true;
                        txtVideoSharing.Enabled = false;
                    }
                    else if ((gvSettings.Rows[index].FindControl("lblGridBudget_Settings_Id") as Label).Text == "3")
                    {
                        lblTitle.Text = "Budget Settings with sharing";
                        txtPromoter.Enabled = false;
                        txtVideoSharing.Enabled = true;
                    }
                    else if ((gvSettings.Rows[index].FindControl("lblGridBudget_Settings_Id") as Label).Text == "4")
                    {
                        lblTitle.Text = "Budget Settings with sharing and promoter";
                        txtPromoter.Enabled = true;
                        txtVideoSharing.Enabled = true;
                    }

                    lblDumpBudId.Text = (gvSettings.Rows[index].FindControl("lblGridBudget_Settings_Id") as Label).Text;
                    txtAddBudget.Text = objDataSet.Tables[0].Rows[0]["Budget"].ToString();
                    txtShortFilm.Text = objDataSet.Tables[0].Rows[0]["Short_Film"].ToString();
                    txtAdmin.Text = objDataSet.Tables[0].Rows[0]["Admin"].ToString();
                    txtVideoSharing.Text = objDataSet.Tables[0].Rows[0]["Video_Sharing"].ToString();
                    txtPromoter.Text = objDataSet.Tables[0].Rows[0]["Promoter"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
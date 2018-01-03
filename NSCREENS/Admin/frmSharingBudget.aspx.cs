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

public partial class Admin_frmTitleRegister : System.Web.UI.Page
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

    public void CheckBoxList(ListBox cbl, string DataValueField, string DataTextField, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                cbl.DataSource = objDataSet;
                cbl.DataValueField = DataValueField;
                cbl.DataTextField = DataTextField;
                cbl.DataBind();
            }
            else
            {
                cbl.DataSource = "";
                cbl.DataBind();
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void Titles_List(GridView gdv, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                gdv.DataSource = objDataSet;
                gdv.DataBind();

                if (Session["UserName"].ToString() != "admin")
                {
                    if (gdv.ID == "gvAdd")
                    {
                        decimal totalPromoter = objDataSet.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Promoter"));

                        gdv.FooterRow.Cells[4].Text = totalPromoter.ToString("N2");
                    }
                    if (gdv.ID == "gvShort")
                    {
                        decimal total = objDataSet.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Uploader"));

                        gdv.FooterRow.Cells[3].Text = total.ToString("N2");
                    }
                    if (gdv.ID == "gvShare")
                    {
                        decimal total = objDataSet.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Video Share"));

                        gdv.FooterRow.Cells[3].Text = total.ToString("N2");
                    }
                }
                else
                {
                    decimal total = objDataSet.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Admin"));

                    //gdv.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    if (gdv.ID == "gvAdd")
                        gdv.FooterRow.Cells[3].Text = total.ToString("N2");
                    if (gdv.ID == "gvShort")
                        gdv.FooterRow.Cells[3].Text = total.ToString("N2");
                    if (gdv.ID == "gvShare")
                    {
                        decimal total1 = objDataSet.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Video Share"));

                        gdv.FooterRow.Cells[3].Text = total1.ToString("N2");
                    }
                }
            }
            else
            {
                //ShowNotification("Budget", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Budget", "Error", NotificationType.error);
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

                if (Session["UserName"].ToString() == "admin")
                {
                    //DataSet objDataSetE = MasterCode.RetrieveQuery("Select (Select case when SUM(Promoter_Budget) is null then 0 else SUM(Promoter_Budget) end +case when SUM(User_Budget) is null then 0 else SUM(User_Budget) end from tbl_Short_Film_Visits where Short_Film_Id in (Select Lan_Short_film_Id from tbl_Language_Short_FilmId where CreatedById=" + Session["UserId"].ToString() + "))+(Select case when SUM(Video_Sharing) is null then 0 else SUM(Video_Sharing) end from tbl_Short_Film_Visits where User_Share_Id=" + Session["UserId"].ToString() + ")+(Select case when sum(User_Budget) is null then 0 else sum(User_Budget) end +case when sum(Promoter_Budget) is null then 0 else sum(Promoter_Budget) end from tbl_Advertizment_Visits where Advertisement_Id in (Select Advertisement_Id from tbl_Advertisement where CreatedById=" + Session["UserId"].ToString() + "))");
                    //DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                    DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                    if (objDataSetE.Tables[0].Rows.Count > 0)
                        lblEarning.Text = objDataSetE.Tables[0].Rows[0][0].ToString();
                    else
                        lblEarning.Text = "0.00";

                    txtEarningAmt.Text = lblEarning.Text;
                    lblDumpEarningAmt.Text = lblEarning.Text;


                    Titles_List(gvSettings, "SELECT Budget_Settings_Id,Budget,Short_Film,Admin,Promoter,Video_Sharing FROM tbl_Budget_Settings");
                    Titles_List(gvAdd, "Select cast(ROW_NUMBER() OVER (ORDER BY Visits_Id) as varchar(max))as [S. No.],(Select Title from tbl_Advertisement S where S.Advertisement_Id=V.Advertisement_Id)as Title,convert(varchar(12),Date_Time,100)as Date,Admin_Budget as Admin,User_Budget as Uploader,Promoter_Budget as Promoter from tbl_Advertizment_Visits V");
                    Titles_List(gvShort, "Select cast(ROW_NUMBER() OVER (ORDER BY Visits_Id) as varchar(max))as [S. No.],(Select  (Select Title from tbl_Short_film S where S.Short_film_Id=TL.Short_Film_Id) from tbl_Language_Short_FilmId TL where TL.Lan_Short_film_Id=V.Short_Film_Id)as Title,convert(varchar(12),Date_Time,100)as Date,Admin_Budget as Admin,User_Budget as Uploader,Promoter_Budget as Promoter,Video_Sharing as [Video Share] from tbl_Short_Film_Visits V ");
                    Titles_List(gvShare, "Select cast(ROW_NUMBER() OVER (ORDER BY Visits_Id) as varchar(max))as [S. No.],(Select Title from tbl_Short_film S where S.Short_film_Id=V.Short_Film_Id)as Title,convert(varchar(12),Date_Time,100)as Date,Video_Sharing as [Video Share],Promoter_Budget as Promoter,User_Budget as Uploader from tbl_Short_Film_Visits V where User_Share_Id=" + Session["UserId"].ToString() + "");
                }
                else
                {
                    DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                    if (objDataSetE.Tables[0].Rows.Count > 0)
                        lblEarning.Text = objDataSetE.Tables[0].Rows[0][0].ToString();
                    else
                        lblEarning.Text = "0.00";

                    txtEarningAmt.Text = lblEarning.Text;
                    lblDumpEarningAmt.Text = lblEarning.Text;

                    Titles_List(gvSettings, "SELECT Budget_Settings_Id,Budget,Short_Film,Admin,Promoter,Video_Sharing FROM tbl_Budget_Settings");
                    Titles_List(gvAdd, "Select cast(ROW_NUMBER() OVER (ORDER BY Visits_Id) as varchar(max))as [S. No.],(Select Title from tbl_Advertisement S where S.Advertisement_Id=V.Advertisement_Id)as Title,convert(varchar(12),Date_Time,100)as Date,User_Budget as Uploader,Promoter_Budget as Promoter from tbl_Advertizment_Visits V where Advertisement_Id in (Select Advertisement_Id from tbl_Advertisement where CreatedById=" + Session["UserId"].ToString() + ")");
                    Titles_List(gvShort, "Select cast(ROW_NUMBER() OVER (ORDER BY Visits_Id) as varchar(max))as [S. No.],(Select  (Select Title from tbl_Short_film S where S.Short_film_Id=TL.Short_Film_Id) from tbl_Language_Short_FilmId TL where TL.Lan_Short_film_Id=V.Short_Film_Id)as Title,convert(varchar(12),Date_Time,100)as Date,User_Budget as Uploader,Promoter_Budget as Promoter,Video_Sharing as [Video Share] from tbl_Short_Film_Visits V where  Short_Film_Id in (Select Lan_Short_film_Id from tbl_Language_Short_FilmId U where CreatedById=" + Session["UserId"].ToString() + ")");
                    Titles_List(gvShare, "Select cast(ROW_NUMBER() OVER (ORDER BY Visits_Id) as varchar(max))as [S. No.],(Select Title from tbl_Short_film S where S.Short_film_Id=V.Short_Film_Id)as Title,convert(varchar(12),Date_Time,100)as Date,Video_Sharing as [Video Share],Promoter_Budget as Promoter,User_Budget as Uploader from tbl_Short_Film_Visits V where User_Share_Id=" + Session["UserId"].ToString() + "");


                }
                Titles_List(gvTrans, "Select Id,Transaction_Id,Debit as Credit,convert(varchar(12),CreatedDate,100)as CreatedDate,'Refill Wallet' as Page from tbl_transactions S where Page_Id=3 and CreatedById=" + Session["UserId"].ToString());
                //Titles_List(gvTitle, "Select Visits_Id,(Select Title from tbl_Short_film S where S.Short_film_Id=V.Short_Film_Id)as Short_Film_Id,User_Id,convert(varchar(12),Date_Time,100)as Date_Time,Video_Sharing,User_Budget from tbl_Short_Film_Visits V where User_Share_Id=" + Session["UserId"].ToString() + " or Short_Film_Id in (Select Short_film_Id from tbl_Short_film U where CreatedById=" + Session["UserId"].ToString() + ") ");
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Budget", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTransferAmount.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtTransferAmount.Text.Trim()) <= Convert.ToDecimal(lblDumpEarningAmt.Text.Trim()))
                {
                    DataSet objDataSet = MasterCode.ExcuteOneParameter(txtTransferAmount.Text.Trim(), "Sp_Amount_Transfer", Session["UserId"].ToString());
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Master.Amount = (Convert.ToDecimal(Master.Amount) + Convert.ToDecimal(txtTransferAmount.Text)).ToString();
                        txtTransferAmount.Text = "";
                        //DataSet objDataSet = MasterCode.RetrieveQuery("Update tbl_Register_User set User_Budget="+txtTransferAmount.Text.Trim()+" where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"] + ")");

                        ShowNotification("Amount Transfer", "Amount Transfer Successfully..", NotificationType.success);

                        DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                        if (objDataSetE.Tables[0].Rows.Count > 0)
                            lblEarning.Text = objDataSetE.Tables[0].Rows[0][0].ToString();
                        else
                            lblEarning.Text = "0.00";


                        Master.EarningAmount = "( " + lblEarning.Text + " )";
                        txtEarningAmt.Text = lblEarning.Text;
                        lblDumpEarningAmt.Text = lblEarning.Text;
                    }
                }
                else
                {
                    ShowNotification("Amount Transfer", "Invalid Amount..", NotificationType.error);
                }
            }
            else
            {
                ShowNotification("Amount Transfer", "Please Enter Transfer Amount..", NotificationType.error);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
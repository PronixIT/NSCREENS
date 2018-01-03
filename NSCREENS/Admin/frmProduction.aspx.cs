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

public partial class Admin_frmProduction : System.Web.UI.Page
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

    public void Display_List(ListView lst, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                lst.DataSource = objDataSet;
                lst.DataBind();
            }
            else
            {
                lst.DataSource = "";
                lst.DataBind();
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void Send_Production_Data(string Production_Name, int Production_Id, string DumpProduction_Name, bool Isactive, string Description, string Img)
    {
        try
        {
            if (Production_Name != "" && Description != "" && Img != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_Production objAdmin_Production = new Admin_Production();

                    objAdmin_Production.ProductionName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Production_Name.ToLower());
                    objAdmin_Production.ProductionId = Production_Id;
                    objAdmin_Production.DumpProductionName = DumpProduction_Name;
                    objAdmin_Production.Isactive = Isactive;
                    objAdmin_Production.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_Production.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objAdmin_Production.Description = Description;
                    objAdmin_Production.Img = Img;

                    DataSet objDataSet = Admin_Production.Production_Send_To_DB(objAdmin_Production);
                    if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Production_Id == 0)
                        {
                            if (Img != "")
                                fupProductionImage.SaveAs(Server.MapPath("~/ProductionImg/") + "pro" + objDataSet.Tables[0].Rows[0][1].ToString() + ".jpg");

                            btnSubmit.Visible = false;
                            ShowNotification("Production", "Inserted Successfully..", NotificationType.success);
                            Response.Redirect("frmProductionList.aspx",false);
                            txtProduction.Text = "";
                            txtDescription.Text = "";
                            Productions_List(gvProduction, "select Channel_Id,Channel_Name,Isactive,Description from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString() + " Order by Channel_Name");
                        }
                        else
                        {
                            if (Img != "")
                                fudUpdateImg.SaveAs(Server.MapPath("~/ProductionImg/") + "pro" + Production_Id.ToString() + ".jpg");
                            ShowNotification("Production", "Updated Successfully..", NotificationType.success);
                            Productions_List(gvProduction, "select Channel_Id,Channel_Name,Isactive,Description from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString() + " Order by Channel_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Production", "Production Already Existed..!", NotificationType.error);
                        if (Production_Id == 0)
                            txtProduction.Focus();
                        else
                            txtUpdateProduction.Focus();
                    }
                    else
                        ShowNotification("Production", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Production", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Production", "Please fill all Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

        }
    }

    public void Productions_List(GridView gdv, string Query)
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
                ShowNotification("Production", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Production", "kgfkjghfkdjghdfkghkf", NotificationType.error);
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

                DataSet objDataSet = MasterCode.RetrieveQuery("select count(*) from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString());
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                {
                    Response.Redirect("frmProductionList.aspx", false);
                    btnSubmit.Visible = false;
                }
                else
                    btnSubmit.Visible = true;
                //Productions_List(gvProduction, "select Channel_Id,Channel_Name,Isactive,Description from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString() + " Order by Channel_Name");
                txtProduction.Focus();

                Display_List(lstOther, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where Isactive='true' Order by Channel_Name");

                //ShowNotification("Production", "Please Create Production First..", NotificationType.info);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Production", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void txtSearch1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Artist_Id = Request.QueryString["Id"];

            string AddColoums = "";

            if (txtSearch1.Text != "")
                AddColoums = AddColoums + " Channel_Name Like '" + txtSearch1.Text + "%' and ";

            if (AddColoums != "")
            {
                AddColoums = AddColoums.Remove(AddColoums.Length - 4, 4);
                Display_List(lstOther, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where  Isactive='true' and " + AddColoums + " Order by Channel_Name");
            }
            else
                Display_List(lstOther, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where  Isactive='true' Order by Channel_Name");
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkMore_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            Response.Redirect("MyProfile.aspx?ProductionId=" + lnk.CommandName, false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPassword1.Text != "")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("select count(*) from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString());
                if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                    ShowNotification("Production", "Sorry Only one production for Single user..!", NotificationType.error);
                else
                {
                    if (txtProduction.Text.Trim() != "" && txtDescription.Text.Trim() != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup1()", true);
                    }
                    else
                        ShowNotification("Production", "Please fill All Fields..!", NotificationType.error);
                }
            }
            else
                ShowNotification("Production", "Please fill All Fields..!", NotificationType.error);
        }
        catch (Exception Ex)
        {

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
                    if (txtPassword1.Text != "" && txtDescription.Text.Trim() != "" && txtProduction.Text.Trim() != "" && fupProductionImage.FileName != "")
                    {
                        DataSet objDataSet1 = MasterCode.RetrieveQuery("select count(*) from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString());
                        if (Convert.ToInt32(objDataSet1.Tables[0].Rows[0][0].ToString()) > 0)
                            ShowNotification("Production", "Sorry Only one production for Single user..!", NotificationType.error);
                        else
                        {
                            DataSet objDataSet = MasterCode.RetrieveQuery("Select Password from tbl_user where Username='" + Session["UserName"].ToString() + "'");
                            if (objDataSet.Tables[0].Rows[0][0].ToString() == txtPassword1.Text)
                                Send_Production_Data(txtProduction.Text.Trim(), 0, "", true, txtDescription.Text.Trim(), fupProductionImage.FileName);
                            else
                                ShowNotification("Production", "Invalid Password..!", NotificationType.error);
                        }
                    }
                    else
                        ShowNotification("Production", "Please fill All fields..!", NotificationType.error);
                    break;
                case "Clear":
                    txtProduction.Text = "";
                    txtDescription.Text = "";
                    break;
                case "Update":
                    Send_Production_Data(txtUpdateProduction.Text.Trim(), Convert.ToInt32(lblID.Text.Trim()), lblDName.Text.Trim(), rdbActiveYesProduction.Checked ? true : false, txtUpdateDescription.Text.Trim(), fudUpdateImg.FileName);
                    btnClose.Focus();
                    break;
                case "Seach":
                    //Productions_List(gvProduction, "select Production_Id,Production_Name,Isactive from tbl_Admin_Production where Production_Name Like '" + txtSearch1.Text + "%' Order by Production_Name");
                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Production", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvProduction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataSet objDataSet = MasterCode.RetrieveQuery("select '~/ProductionImg/'+Img as Img from tbl_admin_channel where Channel_Id=" + (gvProduction.Rows[index].FindControl("lblGridProductionId") as Label).Text);
                txtUpdateProduction.Text = (gvProduction.Rows[index].FindControl("lblGridProduction") as Label).Text;
                lblID.Text = (gvProduction.Rows[index].FindControl("lblGridProductionId") as Label).Text;
                lblDName.Text = (gvProduction.Rows[index].FindControl("lblGridProduction") as Label).Text;
                txtUpdateDescription.Text = (gvProduction.Rows[index].FindControl("lblGridDescription") as Label).Text;

                imgupdate.ImageUrl = objDataSet.Tables[0].Rows[0]["Img"].ToString();

                if ((gvProduction.Rows[index].FindControl("lblGridProductionIsactive") as Label).Text == " Active ") { rdbActiveNoProduction.Checked = false; rdbActiveYesProduction.Checked = true; }
                else { rdbActiveYesProduction.Checked = false; rdbActiveNoProduction.Checked = true; }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Production", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }
}
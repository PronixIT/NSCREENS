using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomerName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Select Title from tbl_Short_film where Title Like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> Title = new List<string>();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                    while (sdr.Read())
                        Title.Add(sdr["Title"].ToString());

                conn.Close();
                return Title;
            }
        }
    }

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

    public void Send_Title_Data(string Title_Name, int Title_Id, string DumpTitle_Name, bool Isactive, string Tag, string Languages, string Image)
    {
        try
        {
            if (Title_Name != "" && Tag != "" && Languages != "" && Image != "")
            {
                if (Session["UserCode"] != null)
                {
                    Admin_State objAdmin_State = new Admin_State();

                    objAdmin_State.TitleName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Title_Name.ToLower());
                    objAdmin_State.TitleId = Title_Id;
                    objAdmin_State.DumpTitleName = DumpTitle_Name;
                    objAdmin_State.Isactive = Isactive;
                    objAdmin_State.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                    objAdmin_State.UserId = Convert.ToInt32(Session["UserId"].ToString());

                    objAdmin_State.Tag = Tag;
                    objAdmin_State.Languages = Languages;

                    DataSet objDataSet = Admin_State.Title_Send_To_DB(objAdmin_State);
                    if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (Title_Id == 0)
                        {
                            //if (Image != "")
                            //    fupUploadImage.SaveAs(Server.MapPath("~/Video_Images/") + "Reg" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                            ShowNotification("Title", "Inserted Successfully..", NotificationType.success);
                            ddlTitle.SelectedIndex = 0;
                            txtTag.Text = "";
                            ddlLanguage.SelectedIndex = 0;
                            Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Languages as Languages1 from tbl_Register_Title Order by Title_Name");
                        }
                        else
                        {
                            //if (Image != "")
                            //    fupUpdateImage.SaveAs(Server.MapPath("~/Video_Images/") + "Reg" + objDataSet.Tables[0].Rows[0][0].ToString() + ".jpg");

                            ShowNotification("Title", "Updated Successfully..", NotificationType.success);
                            Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Languages as Languages1 from tbl_Register_Title Order by Title_Name");
                        }
                    }
                    else if (objDataSet.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        ShowNotification("Title", "Title Already Existed..!", NotificationType.error);
                        if (Title_Id == 0)
                            ddlTitle.Focus();
                        else
                            txtUpdateTitle.Focus();
                    }
                    else
                        ShowNotification("Title", "Not inserted..!", NotificationType.error);

                }
                else
                    ShowNotification("Title", "Sorry, Your Session had been Expired.. So Please Logout Once & Login Again..!", NotificationType.error);
            }
            else
                ShowNotification("Title", "Please fill all Fields..!", NotificationType.error);
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
            }
            else
            {
                ShowNotification("Title", "List is empty..!", NotificationType.info);
                gdv.DataSource = "";
                gdv.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ShowNotification("Title", "kgfkjghfkdjghdfkghkf", NotificationType.error);
        }
    }

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

                //CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");
                DropDownList(ddlLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name", "Select");
                if (Session["UserName"] == "admin")
                {
                    DropDownList(ddlTitle, "Title_Id", "Title_Name", "Select Title_Id,Title_Name from tbl_Register_Title where Isactive='true' order by Title_Name", "Select");
                    Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,CreatedById,Languages as Languages1 from tbl_Register_Title Order by Title_Name");
                }
                else
                {
                    DropDownList(ddlTitle, "Title_Id", "Title_Name", "Select Title_Id,Title_Name from tbl_Register_Title where Isactive='true' and CreatedById=" + Session["UserId"].ToString() + " order by Title_Name", "Select");
                    Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Languages as Languages1 from tbl_Register_Title where CreatedById='" + Session["UserId"].ToString() + "' Order by Title_Name");
                }
                ddlTitle.Focus();
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);
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

                    if (txtURL.Text.Trim() != "" && lblID.Text.Trim() != "" && ddlLanguage.SelectedIndex != 0)
                    {
                        Short_Film objShort_Film = new Short_Film();

                        objShort_Film.CreatedDate = DateTime.Now.AddHours(Connection.SetHours);
                        objShort_Film.UserId = Convert.ToInt32(Session["UserId"].ToString());

                        objShort_Film.Short_film_Id = Convert.ToInt32(lblID.Text.Trim());

                        string Url1 = txtURL.Text.Trim();
                        string[] SplitUrl1 = Url1.Split('/');
                        string NewUrl1 = "";
                        if (SplitUrl1.Length != 0)
                            for (int i = 0; i < SplitUrl1.Length; i++)
                                if (i == 1)
                                    NewUrl1 = NewUrl1 + "//player." + SplitUrl1[i].ToString();
                                else if (i == 3)
                                    NewUrl1 = NewUrl1 + "/video/" + SplitUrl1[i].ToString();
                                else
                                    NewUrl1 = NewUrl1 + SplitUrl1[i].ToString();

                        objShort_Film.Trailer = NewUrl1;
                        objShort_Film.Language = ddlLanguage.SelectedValue.ToString();

                        DataSet objDataSet = Short_Film.Trailer_Send_To_DB(objShort_Film, (DataTable)ViewState["Artist"]);
                        if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            ShowNotification("Trailer", "Your trailer is uploaded successfully and we will inform you shortly once it got approved", NotificationType.success);

                            ddlTitle.SelectedIndex = 0;
                            txtTag.Text = "";
                            ddlLanguage.SelectedIndex = 0;
                            lblID.Text = "";

                            if (Session["UserName"] == "admin")
                                Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,CreatedById,Languages as Languages1 from tbl_Register_Title Order by Title_Name");
                            else
                                Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Languages as Languages1 from tbl_Register_Title where CreatedById='" + Session["UserId"].ToString() + "' Order by Title_Name");
                        }
                    }
                    else
                    {
                        ShowNotification("Trailer", "Please Select Trailer..!", NotificationType.error);
                    }
                    break;
                case "Clear":
                    ddlTitle.SelectedIndex = 0;
                    txtTag.Text = "";
                    ddlLanguage.SelectedIndex = 0;
                    lblID.Text = "";
                    break;
                case "Update":

                    string ULanguesIds = ",";
                    for (int i = 0; i < lstUpdateLanguage.Items.Count; i++)
                        if (lstUpdateLanguage.Items[i].Selected)
                            ULanguesIds = ULanguesIds + lstUpdateLanguage.Items[i].Value.ToString() + ",";
                    btnClose.Focus();
                    break;
                case "Seach":

                    break;
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void gvTitle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("detail"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                txtTitle.Text = (gvTitle.Rows[index].FindControl("lblGridTitle") as Label).Text;

                ddlTitle.ClearSelection();
                if (ddlTitle.Items.FindByText((gvTitle.Rows[index].FindControl("lblGridTitle") as Label).Text) != null)
                    ddlTitle.Items.FindByText((gvTitle.Rows[index].FindControl("lblGridTitle") as Label).Text).Selected = true;

                lblID.Text = (gvTitle.Rows[index].FindControl("lblGridTitleId") as Label).Text;

                txtTag.Text = (gvTitle.Rows[index].FindControl("lblGridTag") as Label).Text;

                //CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");

                DropDownList(ddlLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' and Language_Id in (0" + (gvTitle.Rows[index].FindControl("lblGridLanguages1") as Label).Text + "0) order by Language_Name", "Select");

                //lstLanguage.ClearSelection();
                //string[] Split = ((gvTitle.Rows[index].FindControl("lblGridLanguages") as Label).Text).Split(',');
                //if (Split.Length > 0)
                //    for (int i = 0; i < Split.Length; i++)
                //        foreach (ListItem li in lstLanguage.Items)
                //            if (li.Text == Split[i])
                //                li.Selected = true;

                //if(ddlLanguage.Items.FindByText((gvTitle.Rows[index].FindControl("lblGridLanguages") as Label).Text)!=null)
                //ddlLanguage.Items.FindByText((gvTitle.Rows[index].FindControl("lblGridLanguages") as Label).Text).Selected=true;

                if ((gvTitle.Rows[index].FindControl("lblGridTitleIsactive") as Label).Text == " Active ") { rdbActiveNoTitle.Checked = false; rdbActiveYesTitle.Checked = true; }
                else { rdbActiveYesTitle.Checked = false; rdbActiveNoTitle.Checked = true; }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
            }
        }
        catch (Exception Ex)
        {
            StackTrace objStackTrace = new StackTrace();
            string calledMethodName = objStackTrace.GetFrame(1).GetMethod().Name;
            string dispErrorMsg = string.Format("Error occurred in {0} method.", calledMethodName);
            LogFile.WriteToLog(dispErrorMsg, Ex);
            ShowNotification("Title", dispErrorMsg, NotificationType.error);
            SendLogFile.SendMail();
        }
    }

    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Title_Id,Title_Name,Languages,Tag,Isactive from tbl_Register_Title where Isactive='true' and Title_Id=" + ddlTitle.SelectedValue.ToString());
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                txtTitle.Text = objDataSet.Tables[0].Rows[0]["Title_Name"].ToString();

                ddlTitle.ClearSelection();
                if (ddlTitle.Items.FindByText(objDataSet.Tables[0].Rows[0]["Title_Name"].ToString()) != null)
                    ddlTitle.Items.FindByText(objDataSet.Tables[0].Rows[0]["Title_Name"].ToString()).Selected = true;

                lblID.Text = objDataSet.Tables[0].Rows[0]["Title_Id"].ToString();
                //lblDName.Text = (gvTitle.Rows[index].FindControl("lblGridTitle") as Label).Text;

                txtTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();

                DropDownList(ddlLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' and Language_Id in (0" + objDataSet.Tables[0].Rows[0]["Languages"].ToString() + "0) order by Language_Name", "Select");

                //CheckBoxList(lstLanguage, "Language_Id", "Language_Name", "Select Language_Id,Language_Name from tbl_admin_language where Isactive='true' order by Language_Name");

                //lstLanguage.ClearSelection();
                //string[] Split = (objDataSet.Tables[0].Rows[0]["Languages"].ToString()).Split(',');
                //if (Split.Length > 0)
                //    for (int i = 0; i < Split.Length; i++)
                //        foreach (ListItem li in lstLanguage.Items)
                //            if (li.Value == Split[i])
                //                li.Selected = true;

                if (objDataSet.Tables[0].Rows[0]["Isactive"].ToString() == "True") { rdbActiveNoTitle.Checked = false; rdbActiveYesTitle.Checked = true; }
                else { rdbActiveYesTitle.Checked = false; rdbActiveNoTitle.Checked = true; }
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
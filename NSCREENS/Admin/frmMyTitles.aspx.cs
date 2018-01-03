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

                Display_List(lstRecentVideos, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages,Tag,'~/Video_Images/'+Image as Photo from tbl_Register_Title where Isactive='true' and CreatedById=" + Session["UserId"].ToString() + " Order by Title_Name");
                //Titles_List(gvTitle, "select Title_Id,Title_Name,Isactive,Tag,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(Languages,',',1))FOR XML PATH (''))as Languages from tbl_Register_Title where Isactive='true' and CreatedById=" + Session["UserId"].ToString() + " Order by Title_Name");
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

}
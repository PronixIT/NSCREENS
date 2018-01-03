using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmProductionList : System.Web.UI.Page
{
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

                Display_List(lstRecentVideos, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where CreatedById=" + Session["UserId"].ToString() + " Order by Channel_Name");
                Display_List(lstOther, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where CreatedById!=" + Session["UserId"].ToString() + " Order by Channel_Name");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Artist_Id = Request.QueryString["Id"];

            string AddColoums = "";

            if (txtSearch.Text != "")
                AddColoums = AddColoums + " Channel_Name Like '" + txtSearch.Text + "%' and ";

            if (AddColoums != "")
            {
                AddColoums = AddColoums.Remove(AddColoums.Length - 4, 4);
                Display_List(lstOther, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where CreatedById!=" + Session["UserId"].ToString() + " and " + AddColoums + " Order by Channel_Name");
            }
            else
                Display_List(lstOther, "select Channel_Id,Channel_Name,Isactive,Description,'../ProductionImg/'+Img as Img from tbl_Admin_Channel where CreatedById!=" + Session["UserId"].ToString() + " Order by Channel_Name");
        }
        catch (Exception Ex)
        {

        }
    }
    protected void lnkMore_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk=sender as LinkButton;
            Response.Redirect("MyProfile.aspx?ProductionId="+lnk.CommandName, false);
        }
        catch(Exception Ex)
        {

        }
    }
}
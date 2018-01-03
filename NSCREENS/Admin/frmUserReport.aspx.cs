using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmAdvatizmentReport : System.Web.UI.Page
{
    public void Display_List(GridView gv, string Query)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery(Query);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = objDataSet;
                gv.DataBind();
            }
            else
            {
                gv.DataSource = "";
                gv.DataBind();
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

                CheckBoxList(lstUsers, "User_Id", "Username", "Select User_Id,Username from tbl_user U where Isactive='true' and Staff_Id!=0 order by Username");
                Display_List(gvUserList, "Select Budget_Id,User_Budget,Promoter_Budget,Admin_Budget,(Select Name from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id in (Select CreatedById from tbl_Short_film S where S.Short_film_Id=B.Short_Film_Id)))as Name,(Select Title from tbl_Short_film S where S.Short_film_Id=B.Short_Film_Id)as Title from tbl_Budget B ");
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
            string AddColum = "";
            if (txtStartDate.Text.Trim() != "")
                AddColum = AddColum + " CreatedDate='" + txtStartDate.Text.Trim() + "' and ";
            if (txtEndDate.Text.Trim() != "")
                AddColum = AddColum + " CreatedDate='" + txtEndDate.Text.Trim() + "' and ";

            string CoveredAreaIds = ",";
            for (int i = 0; i < lstUsers.Items.Count; i++)
                if (lstUsers.Items[i].Selected)
                    CoveredAreaIds = CoveredAreaIds + lstUsers.Items[i].Value.ToString() + ",";

            if (CoveredAreaIds != "")
                AddColum = AddColum + " CreatedById in (0" + CoveredAreaIds + "0) and ";

            if (AddColum != "")
            {
                AddColum = AddColum.Remove(AddColum.Length - 4, 4);
                Display_List(gvUserList, "Select Budget_Id,User_Budget,Promoter_Budget,Admin_Budget,(Select Name from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id in (Select CreatedById from tbl_Short_film S where S.Short_film_Id=B.Short_Film_Id)))as Name,(Select Title from tbl_Short_film S where S.Short_film_Id=B.Short_Film_Id)as Title from tbl_Budget B where " + AddColum);
            }
            else
            {
                Display_List(gvUserList, "Select Budget_Id,User_Budget,Promoter_Budget,Admin_Budget,(Select Name from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id in (Select CreatedById from tbl_Short_film S where S.Short_film_Id=B.Short_Film_Id)))as Name,(Select Title from tbl_Short_film S where S.Short_film_Id=B.Short_Film_Id)as Title from tbl_Budget B ");
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
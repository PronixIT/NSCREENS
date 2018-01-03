using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmUserRights : System.Web.UI.Page
{
    MasterCode objMaster = new MasterCode();
    DataSet objDataSet = new DataSet();

    #region Private Methods & Enum

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

    public void CheckUrl()
    {
        String myUrl = Request.RawUrl.ToString();
        var result = Path.GetFileName(myUrl);
        String Folder = Path.GetDirectoryName(myUrl);
        string[] SplitOffer = Folder.Split('\\');
        for (int i = 0; i < SplitOffer.Length; i++)
        {
            if (i == 2)
            {
                //Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String myUrl = Request.RawUrl.ToString();
            var result = Path.GetFileName(myUrl);
            String Folder = Path.GetDirectoryName(myUrl);
            string[] SplitOffer = Folder.Split('\\');
            for (int i = 0; i < SplitOffer.Length; i++)
                if (i == 1)
                    Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;

            CheckUrl();
            tvsample.Attributes.Add("onclick", "OnTreeClick(event)");
            LoadData();
        }
    }

    public void LoadData()
    {
        ddlPerson.Items.Clear();
        objDataSet = objMaster.GetUnameBname();

        ddlPerson.DataSource = objDataSet.Tables[0];
        ddlPerson.DataTextField = "Username";
        ddlPerson.DataValueField = "User_Id";
        ddlPerson.DataBind();
        ddlPerson.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    private void AddChildMenuItems(DataTable menuData, TreeNode parentMenuItem)
    {
        DataView view = null;
        try
        {
            view = new DataView(menuData);
            view.RowFilter = "ParentID=" + parentMenuItem.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newTreeNode = new TreeNode(row["Text"].ToString(), row["MenuID"].ToString());

                parentMenuItem.ChildNodes.Add(newTreeNode);

                AddChildMenuItems(menuData, newTreeNode);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            view = null;
        }
    }

    private void GetNodeRecursive(TreeNode treeNode)
    {
        string id = lblId.Text;

        string[] Id = (lblId.Text).Split(',');

        treeNode.Checked = false;
        for (int i = 0; i < Id.Length; i++)
            if (Id[i] == treeNode.Value.ToString())
                treeNode.Checked = true;

        //if (id.Contains(treeNode.Value.ToString()))
        //    treeNode.Checked = true;
        //else
        //    treeNode.Checked = false;

        foreach (TreeNode tn in treeNode.ChildNodes)
        {
            GetNodeRecursive(tn);
        }
    }

    protected void ddlPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPerson.SelectedIndex != 0)
        {
            objMaster.UserId = Convert.ToInt32(ddlPerson.SelectedValue.ToString());
            DataSet ds = MasterCode.RetrieveQuery("Select Register_Id,Name,Mobile_No,EmailId,Address,(Select UserRights from tbl_user U where U.Staff_Id=RU.Register_Id)as UserRights from tbl_Register_User RU where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + ddlPerson.SelectedValue + ")"); //objMaster.GetStaffDetailsById();
            lblId.Text = ds.Tables[0].Rows[0]["UserRights"].ToString();
            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtDesignation.Text = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
            DataTable treeData = null;
            try
            {
                tvsample.Nodes.Clear();
                treeData = new DataTable();
                treeData = GetTreeData();
                AddTopMenuItems(treeData);
                TreeNodeCollection nodes = this.tvsample.Nodes;
                foreach (TreeNode n in nodes)
                {
                    GetNodeRecursive(n);
                }
                //ChkAdmingDeskList.DataSource = treeData;
                //ChkAdmingDeskList.DataTextField = "Text";
                //ChkAdmingDeskList.DataValueField = "MenuID";
                //ChkAdmingDeskList.DataBind();

                //DataSet dsAdmindesk = new DataSet();
                //dsAdmindesk = objMaster.GetAdminList();

                //for (int i = 0; i < dsAdmindesk.Tables[0].Rows.Count; i++)
                //{
                //}
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                treeData = null;
            }
        }
        else
        {
            tvsample.Nodes.Clear();
            txtName.Text = "";
            txtDesignation.Text = "";
        }
    }

    private DataTable GetTreeData()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(Connection.con))
            {
                string SelectQuery = "select * from tbl_menu";

                SqlCommand cmd = new SqlCommand(SelectQuery, con);
                //objDataSet=objMaster.GetMenu();
                DataTable dtMenuItems = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtMenuItems);
                cmd.Dispose();
                sda.Dispose();
                return dtMenuItems;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        return null;
    }

    private void AddTopMenuItems(DataTable menuData)
    {
        DataView view = null;
        try
        {
            view = new DataView(menuData);
            view.RowFilter = "ParentID IS NULL";
            foreach (DataRowView row in view)
            {
                //Adding the menu item
                TreeNode newTreeNode = new TreeNode(row["Text"].ToString(), row["MenuID"].ToString());
                // MenuItem newMenuItem = new MenuItem(row["Text"].ToString(), row["MenuID"].ToString());
                tvsample.Nodes.Add(newTreeNode);
                AddChildMenuItems(menuData, newTreeNode);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            view = null;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string pid = "";

            //if (txtUSerCode.Text == Session["UserCode"].ToString())
            //{
                foreach (TreeNode node in tvsample.CheckedNodes)
                {
                    pid += node.Value.ToString() + ",";
                }
                pid = pid.Remove(pid.Length - 1);
                objMaster.UserId = Convert.ToInt32(ddlPerson.SelectedValue);
                objMaster.UserRights = pid;

                int i = objMaster.UpdateRightsANDActiveUser();

                if (i <= 1)
                {
                    ShowNotification("User Rights", "Updated Successfully..", NotificationType.success);
                }
                else
                {
                    ShowNotification("User Rights", "Not Updated..!", NotificationType.error);
                }
            //}
            //else
            //{
            //    ShowNotification("User Rights", "Invalid User Code..!", NotificationType.error);
            //    txtUSerCode.Focus();
            //}
        }
        catch (Exception Ex)
        {
            ShowNotification("User Rights", "Update Error..!", NotificationType.error);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmHome : System.Web.UI.Page
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

                DataSet objDataSet = MasterCode.RetrieveQuery("select Channel_Name,Description,'~/ProductionImg/'+Img as Img from tbl_admin_channel where Isactive='true' and CreatedById=" + Session["UserId"].ToString());
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    img.ImageUrl = objDataSet.Tables[0].Rows[0]["Img"].ToString();
                    lblProductionName.Text = objDataSet.Tables[0].Rows[0][0].ToString();
                    lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                }
                Display_List(lstRecentVideos, "Select Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and CreatedById=" + Session["UserId"].ToString() + " Order by Short_film_Id desc");
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
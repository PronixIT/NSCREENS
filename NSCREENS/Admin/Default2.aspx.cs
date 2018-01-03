using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

public partial class Admin_Default2 : System.Web.UI.Page
{
    [System.Web.Services.WebMethod]
    public static string GetCurrentTime(string UserId,string ShortFilmId)
    {
        DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget+(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + UserId + ")");


        return objDataSet2.Tables[0].Rows[0][0].ToString() ;
    }

    public void DisplayOnly_Add(string ShortFilmId)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Advertisement where Isactive='True' and ShortFilmId=" + ShortFilmId);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Add_Budget()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString());
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
                if (Session["UserId"] != null)
                {
                    txt.Text = Session["UserId"].ToString();
                    string ShortFilmId = Request.QueryString["shortfilm"];

                    if (Request.QueryString["shortfilm"] == null)
                    {
                        string Advertisement = Request.QueryString["Advertisement"];
                        DisplayOnly_Add(Advertisement);
                    }
                    else
                    {
                    }
                }
                else
                    Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
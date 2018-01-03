using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_frmSingle : System.Web.UI.Page
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

    public void Display_Add(string ShortFilmId)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True' and ShortFilmId=" + ShortFilmId);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                DataSet obj = MasterCode.RetrieveQuery("select Short_film_Id,Title,'../Videos/'+Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                lblNextVideo.Text = obj.Tables[0].Rows[0]["Shortfilm"].ToString();
                id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                lblTitle.Text = obj.Tables[0].Rows[0]["Title"].ToString();
                //Video.Attributes.Add("src", objDataSet.Tables[0].Rows[0]["Advertisement"].ToString());
                add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Display()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                lblPublished.Text = obj.Tables[0].Rows[0]["Publish"].ToString();
                lblDescription.Text = obj.Tables[0].Rows[0]["Description"].ToString();
                lblViews.Text = obj.Tables[0].Rows[0]["Visits"].ToString();
                DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString());

            }
            else
            {
                DataSet objDataSet1 = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True'");
                if (objDataSet1.Tables[0].Rows.Count > 0)
                {
                    DataSet obj = MasterCode.RetrieveQuery("select Short_film_Id,Title,'../Videos/'+Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                    lblNextVideo.Text = obj.Tables[0].Rows[0]["Shortfilm"].ToString();
                    id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                    lblTitle.Text = obj.Tables[0].Rows[0]["Title"].ToString();
                    //Video.Attributes.Add("src", objDataSet1.Tables[0].Rows[0]["Advertisement"].ToString());
                    add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Display()'><source id='add123' src='" + objDataSet1.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                    lblPublished.Text = obj.Tables[0].Rows[0]["Publish"].ToString();
                    lblDescription.Text = obj.Tables[0].Rows[0]["Description"].ToString();
                    lblViews.Text = obj.Tables[0].Rows[0]["Visits"].ToString();
                    DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet1.Tables[0].Rows[0]["Advertisement_Id"].ToString());
                }
            }
            Display_List(lstMostLiked, "Select top 4 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");
            Display_List(lstRelatedVideos, "Select top 3 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");
            Display_List(lstMost, "Select top 2 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Visits desc ");
        }
        catch (Exception Ex)
        {

        }
    }

    public void DisplayOnly_Add(string ShortFilmId)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Advertisement where Isactive='True' and ShortFilmId=" + ShortFilmId);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                //Video.Attributes.Add("src", objDataSet.Tables[0].Rows[0]["Advertisement"].ToString());
                add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Add_Budget()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();
                DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString());
                DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget+(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");

            }

            Display_List(lstMostLiked, "Select top 4 Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Visits from tbl_Advertisement where Isactive='True' and Status='Approve' order by Advertisement_Id desc ");
            Display_List(lstRelatedVideos, "Select top 3 Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Visits,'0' as Duration from tbl_Advertisement where Isactive='True' and Status='Approve' order by Advertisement_Id desc ");
            Display_List(lstMost, "Select top 2 Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Visits,'0' as Duration from tbl_Advertisement where Isactive='True' and Status='Approve' order by Advertisement_Id desc ");
        }
        catch (Exception Ex)
        {

        }
    }

    public void Display_Film(string ShortFilmId)
    {
        try
        {
            string LoadURL = "";
            //string url = Request.Url.AbsoluteUri;
            //string[] Split = url.Split('%');

            //Display_List(lstComments, "Select Comments_Id,Name,Message from tbl_Comments where Isactive='True' and ShortFilm_Id=" + Split[Split.Length - 2].Substring(2) + " Order by Comments_Id desc");

            //DataSet objLike = MasterCode.RetrieveQuery("Select COUNT(*) from tbl_Like where ShortFilm_Id=" + Split[Split.Length - 2].Substring(2));
            //lblDisplayLikes.Text = objLike.Tables[0].Rows[0][0].ToString() + " Like";

            DataSet objDataSetBudget = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            if (objDataSetBudget.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDecimal(objDataSetBudget.Tables[0].Rows[0][0].ToString()) != 0)
                {
                    if (ShortFilmId != "")
                    {
                        DataSet objDataSet = MasterCode.RetrieveQuery("select Short_film_Id,Title,'../Videos/'+Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                            LoadURL = objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString();
                            add.InnerHtml = "<video id='Video' height='376px' width='100%' controls onclick='Play_Pause()'><source src='" + objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString() + "' type='video/mp4'>";
                            id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);

                            Display_List(lstMostLiked, "Select top 4 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");

                            ////  Video.Attributes.Add("src", objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString());
                            lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                            lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                            lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();

                            //DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                            //if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                            //    objDataSetGetBudget = MasterCode.RetrieveQuery("insert into tbl_Budget(User_Budget,Promoter_Budget,Admin_Budget,Short_Film_Id,CreatedDate,CreatedById) values(" + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + ShortFilmId + ",'" + DateTime.Now.AddHours(Connection.SetHours).ToShortDateString() + "'," + Session["UserId"].ToString() + ")");

                            //DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Short_film set Visits=Visits+1 where Short_film_Id=" + ShortFilmId);
                            //DataSet objDataSetMinBud = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget-(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                            //DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                            //if (objDataSet2.Tables[0].Rows.Count > 0)
                            //    this.Master.Amount = objDataSet2.Tables[0].Rows[0][0].ToString();
                            ////btnPlay.Enabled = false;
                        }
                    }
                    else
                        Display_Add(ShortFilmId);
                }
                else
                    Display_Add(ShortFilmId);
            }
            else
                Display_Add(ShortFilmId);
        }
        catch (Exception Ex)
        {

        }
    }

    //[System.Web.Services.WebMethod]
    //public static string GetCurrentTime(string name)
    //{
    //    DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + name);
    //    DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget+(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + name + ")");
    //    //DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + name + ")");
    //    //if (objDataSet2.Tables[0].Rows.Count > 0)
    //    //    this.Master.Amount = objDataSet2.Tables[0].Rows[0][0].ToString();
    //    return "Hello " + name + Environment.NewLine + "";
    //}

    public string GetData()
    {
        string Plots = "";
        //DataSet objDataSet = MasterCode.RetrieveQuery("select Plot_ID,Plot_UI_Id,Road_Facing,Plot_N0,House_Facing,Length,Breadth,case when Status=1 then '#9dcc77' else '#ed719e' end as Available,Price,((Length*Breadth)/9) as Area_In_Yards,(Select Status_Name from tbl_admin_Status TA where TA.Status_Id=AP.Status)as Status_Name from tbl_admin_Add_Plot AP where Isactive='True' and Layout_Name_ID=2");
        //if (objDataSet.Tables[0].Rows.Count > 0)
        //    for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
        //        Plots = Plots + "addEvent('" + objDataSet.Tables[0].Rows[i]["Plot_UI_Id"].ToString() + "');";
        if (lblAddId.Text.Trim() != "")
        {
            DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + lblAddId.Text.Trim());
            DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget+(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            if (objDataSet2.Tables[0].Rows.Count > 0)
                this.Master.Amount = objDataSet2.Tables[0].Rows[0][0].ToString();
        }
        return Plots;
    }

    public string AddBudget()
    {
        try
        {
            string url = Request.Url.AbsoluteUri;
            string[] Split = url.Split('/');

            DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
            if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                objDataSetGetBudget = MasterCode.RetrieveQuery("insert into tbl_Budget(User_Budget,Promoter_Budget,Admin_Budget,Short_Film_Id,CreatedDate,CreatedById) values(" + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + Split[Split.Length - 2] + ",'" + DateTime.Now.AddHours(Connection.SetHours).ToShortDateString() + "'," + Session["UserId"].ToString() + ")");

            DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Short_film set Visits=Visits+1 where Short_film_Id=" + Split[Split.Length - 2]);
        }
        catch (Exception Ex)
        {

        }

        return "";
    }

    public string GetLikes()
    {
        try
        {
            string url = Request.Url.AbsoluteUri;
            string[] Split = url.Split('%');

            string s = Split[Split.Length - 2];

            DataSet objDataSetLike = MasterCode.RetrieveQuery("Select COUNT(*) from tbl_Like where ShortFilm_Id=" + Convert.ToInt32(s.Substring(2)) + " and CreatedById=" + Session["UserId"].ToString() + "");
            if (Convert.ToInt32(objDataSetLike.Tables[0].Rows[0][0].ToString()) == 0)
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("insert into tbl_Like(ShortFilm_Id,CreatedById) values(" + Convert.ToInt32(s.Substring(2)) + "," + Session["UserId"].ToString() + ")");
            }

            //DataSet objLike = MasterCode.RetrieveQuery("Select COUNT(*) from tbl_Like where ShortFilm_Id=" + s.Substring(2));
            //lblDisplayLikes.Text = objLike.Tables[0].Rows[0][0].ToString() + " Like";
        }
        catch (Exception Ex)
        {

        }
        return "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    string ShortFilmId = Request.QueryString["shortfilm"];

                    if (Request.QueryString["shortfilm"] == null)
                    {
                        string Advertisement = Request.QueryString["Advertisement"];
                        DisplayOnly_Add(Advertisement);
                    }
                    else
                    {
                        Display_Film(ShortFilmId);
                    }

                    ////Display_List(lstUpVideo, "Select Short_film_Id,Title,Tag,cast(Visits as varchar(max))+' Viwes' as Visits,'frmSingle.aspx?/'+cast(Short_film_Id as varchar(max))+'/shortfilm' as shortfilm,'~/Video_Images/'+Short_film_Image as Short_film_Image from tbl_Short_film where Isactive='True' and Status='Approve'");
                    ////Display_List(lstAdvatisment, "select Advertisement_Id,Title,Tag,'~/Videos/'+Video as Video,'~/Video_Images/'+Advertisement_Image as Advertisement_Image,cast(Visits as varchar(max))+' Views'as Visits from tbl_Advertisement where Isactive='True' and Status='Approve' and  StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToShortDateString() + "' and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToShortDateString() + "' Order by Advertisement_Id desc");
                    //DataSet objDataSet = MasterCode.RetrieveQuery("select Short_film_Id,Title,'../Videos/'+Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,'http://localhost:1565/Video_Images/'+Short_film_Image as Short_film_Image from tbl_Short_film where Status='Approve' and Short_film_Id=" + Split[Split.Length - 2]);
                    //lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                    //lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                    ////add.InnerHtml = "<img width='640' height='426' src='" + objDataSet.Tables[0].Rows[0]["Short_film_Image"].ToString() + "'/>";

                    //add.InnerHtml = "<img class='img-responsive' src='../images/main-vid-image-md-1.jpg' alt='video_thumb'>";

                    ////Display_List(lstComments, "Select Comments_Id,Name,Message from tbl_Comments where Isactive='True' and ShortFilm_Id=" + Split[Split.Length - 2] + " Order by Comments_Id desc");
                    ////add.InnerHtml = "<img src='http://localhost:1565/User_Design/images/t1.jpg'/>";
                    ////Video.Attributes.Add("src", "http://localhost:1565/User_Design/images/t1.jpg");
                    ////btnPlay.Enabled = true;

                }
                else
                    Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtName.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtPhone.Text.Trim() != "" && txtMessage.Text.Trim() != "")
    //        {
    //            string url = Request.Url.AbsoluteUri;
    //            string[] Split = url.Split('%');

    //            string s = Split[Split.Length - 2];

    //            User_Comments objUser_Comments = new User_Comments();

    //            objUser_Comments.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtName.Text.Trim());
    //            objUser_Comments.Email = txtEmail.Text.Trim();
    //            objUser_Comments.Phone = txtPhone.Text.Trim();
    //            objUser_Comments.Message = txtMessage.Text.Trim();
    //            objUser_Comments.UserId = Session["UserId"].ToString();
    //            objUser_Comments.CreatedDate = DateTime.Now;
    //            objUser_Comments.ShortFilm = Convert.ToInt32(s.Substring(2));

    //            DataSet objDataSet = User_Comments.Send_To_DB(objUser_Comments);
    //            if (objDataSet.Tables[0].Rows[0][0].ToString() == "1")
    //            {
    //                Display_List(lstComments, "Select Comments_Id,Name,Message from tbl_Comments where Isactive='True' and ShortFilm_Id=" + s.Substring(2) + " Order by Comments_Id desc");
    //                txtName.Text = "";
    //                txtEmail.Text = "";
    //                txtPhone.Text = "";
    //                txtMessage.Text = "";
    //            }
    //        }
    //    }
    //    catch (Exception Ex)
    //    {

    //    }
    //}

    protected void lnkLike_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkshortfilm_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            ListViewItem Row = (ListViewItem)lnk.NamingContainer;
            if (Session["UserId"] != null)
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True' and Advertisement_Id=" + lnk.CommandName);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    lblNextVideo.Text = "";
                    lblAddId.Text = lnk.CommandName;
                    lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                    add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Add_Budget()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";

                    lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                    lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnPlay_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] != null)
            {
                string url = Request.Url.AbsoluteUri;
                string[] Split = url.Split('%');
                if (Split.Length > 0)
                {
                    DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                    if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                        objDataSetGetBudget = MasterCode.RetrieveQuery("insert into tbl_Budget(User_Budget,Promoter_Budget,Admin_Budget,Short_Film_Id,CreatedDate,CreatedById) values(" + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + Split[Split.Length - 2].Substring(2) + ",'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "'," + Session["UserId"].ToString() + ")");

                    DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Short_film set Visits=Visits+1 where Short_film_Id=" + Split[Split.Length - 2].Substring(2));
                    DataSet objDataSetMinBud = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget-(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                    DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                    if (objDataSet2.Tables[0].Rows.Count > 0)
                        this.Master.Amount = objDataSet2.Tables[0].Rows[0][0].ToString();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "Play_Pause123()", true);
                }
            }
            else
                Response.Redirect("~/Login.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }
}
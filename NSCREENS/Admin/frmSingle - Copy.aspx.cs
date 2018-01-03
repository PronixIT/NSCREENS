using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

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
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True' and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and ShortFilmId Like '%," + ShortFilmId + ",%'");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                if (objDataSet.Tables[0].Rows.Count == 1)
                {
                    DataSet obj = MasterCode.RetrieveQuery("select Short_film_Id,Title,Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits,Trailer from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                    lblNextVideo.Text = obj.Tables[0].Rows[0]["Shortfilm"].ToString();
                    id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                    lblTitle.Text = obj.Tables[0].Rows[0]["Title"].ToString();
                    //Video.Attributes.Add("src", objDataSet.Tables[0].Rows[0]["Advertisement"].ToString());
                    //add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Display()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                    txtNextPN.Text = "No";
                    add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "?autoplay=1&api=1&player_id=player1' width='630' height='354' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                    lblPublished.Text = obj.Tables[0].Rows[0]["Publish"].ToString();
                    lblDescription.Text = obj.Tables[0].Rows[0]["Description"].ToString();
                    lblViews.Text = obj.Tables[0].Rows[0]["Visits"].ToString();
                    DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString());

                    DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                    if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                    {
                        DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Advertizment_Visits(Advertisement_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing) values(" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString() + "," + Session["UserId"].ToString() + ",'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd HH:mm") + "','Eluru','" + Request.UserHostAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + ")");
                    }

                    Display_List(lstMostLiked, "Select top 4 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");
                    Display_List(lstRelatedVideos, "Select top 3 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");
                    Display_List(lstMost, "Select top 2 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Visits desc ");

                }
                else
                    Response.Redirect("frmAllAdvatizment.aspx?ShortFilm=" + ShortFilmId, false);
            }
            else
            {
                Response.Redirect("frmAllAdvatizment.aspx", false);
                //DataSet objDataSet1 = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True' and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "'");
                //if (objDataSet1.Tables[0].Rows.Count > 0)
                //{
                //    DataSet obj = MasterCode.RetrieveQuery("select Short_film_Id,Title,Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits,Trailer from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                //    lblNextVideo.Text = obj.Tables[0].Rows[0]["Shortfilm"].ToString();
                //    id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                //    lblTitle.Text = obj.Tables[0].Rows[0]["Title"].ToString();
                //    //Video.Attributes.Add("src", objDataSet1.Tables[0].Rows[0]["Advertisement"].ToString());
                //    add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Display()'><source id='add123' src='" + objDataSet1.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                //    lblPublished.Text = obj.Tables[0].Rows[0]["Publish"].ToString();
                //    lblDescription.Text = obj.Tables[0].Rows[0]["Description"].ToString();
                //    lblViews.Text = obj.Tables[0].Rows[0]["Visits"].ToString();
                //    DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet1.Tables[0].Rows[0]["Advertisement_Id"].ToString());
                //}
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void DisplayOnly_Add(string ShortFilmId)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Advertisement where Isactive='True' and Advertisement_Id=" + ShortFilmId);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                //add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Add_Budget()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";
                txtNextPN.Text = "AddB";
                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "?autoplay=1&api=1&player_id=player1' width='630' height='354' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();

                DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                {
                    DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Advertizment_Visits(Advertisement_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing) values(" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString() + "," + Session["UserId"].ToString() + ",'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd HH:mm") + "','Eluru','" + Request.UserHostAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + ")");
                }

                DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString());

            }

            string Gender = "", DOB = "";

            DataSet objDataSetA = MasterCode.RetrieveQuery("Select Gender,DATEDIFF(hour,DOB,GETDATE())/8766 AS AgeYearsIntTrunc from tbl_Register_User R join tbl_user U on R.Register_Id=U.Staff_Id where U.Username='" + Session["UserName"] + "'");
            if (objDataSetA.Tables[0].Rows.Count > 0)
            {
                Gender = objDataSetA.Tables[0].Rows[0][0].ToString();
                DOB = objDataSetA.Tables[0].Rows[0][1].ToString();
            }

            Display_List(lstMostLiked, "Select top 4 Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Visits from tbl_Advertisement where Isactive='True' and Status='Approve' and NoofVisits>Visits and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' order by Advertisement_Id desc ");
            Display_List(lstRelatedVideos, "Select top 3 Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Visits,'0' as Duration from tbl_Advertisement where Isactive='True' and Status='Approve' and NoofVisits>Visits and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' order by Advertisement_Id desc ");
            Display_List(lstMost, "Select top 2 Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image,Visits,'0' as Duration from tbl_Advertisement where Isactive='True' and Status='Approve' and NoofVisits>Visits and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' order by Advertisement_Id desc ");
        }
        catch (Exception Ex)
        {

        }
    }

    public void Display_Film(string ShortFilmId, string UserId,string VideoType)
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
                        DataSet objDataSet = MasterCode.RetrieveQuery("select Short_film_Id,Title,Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits,Trailer from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                            LoadURL = objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString();
                            //add.InnerHtml = "<video id='Video' height='376px' width='100%' controls autoplay onclick='Play_Pause()'><source src='" + objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString() + "' type='video/mp4'>";
                            txtNextPN.Text = "";

                            if (VideoType == "Film")
                                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString() + "?autoplay=1&api=1&player_id=player1' width='630' height='354' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                            else
                                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Trailer"].ToString() + "?autoplay=1&api=1&player_id=player1' width='630' height='354' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";

                            id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString());
                            a.Attributes.Add("href", "https://plus.google.com/share?url=http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString());

                            Display_List(lstMostLiked, "Select top 4 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");

                            ////  Video.Attributes.Add("src", objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString());
                            lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                            lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                            lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();
                            DataSet objDataSetGetBudget = null;
                            if (UserId == "" || UserId == null)
                                UserId = "0";

                            if (UserId == "0")
                                objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                            else
                                objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 3 else 4 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                            if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                            {
                                DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Short_Film_Visits(Short_Film_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing,User_Share_Id) values(" + ShortFilmId + "," + Session["UserId"].ToString() + ",'" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt") + "','Eluru','" + Request.UserHostAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + "," + UserId + ")");
                            }

                            DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Short_film set Visits=Visits+1 where Short_film_Id=" + ShortFilmId);
                            DataSet objDataSetMinBud = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget-(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                            DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                            if (objDataSet2.Tables[0].Rows.Count > 0)
                                this.Master.Amount = objDataSet2.Tables[0].Rows[0][0].ToString();
                            //btnPlay.Enabled = false;

                            Display_List(lstMostLiked, "Select top 4 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");
                            Display_List(lstRelatedVideos, "Select top 3 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");
                            Display_List(lstMost, "Select top 2 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Visits desc ");
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

    [System.Web.Services.WebMethod]
    public static string GetCurrentTime(string UserId, string ShortFilmId)
    {
        DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget+(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        return objDataSet2.Tables[0].Rows[0][0].ToString();
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
                    txtUserId.Text = Session["UserId"].ToString();
                    string ShortFilmId = Request.QueryString["shortfilm"];
                    string userId1 = Request.QueryString["userId"];
                    txtVideoUserId.Text = userId1;

                    txtShortFilmId.Text = ShortFilmId;

                    if (Request.QueryString["shortfilm"] == null)
                    {
                        string Advertisement = Request.QueryString["Advertisement"];
                        DisplayOnly_Add(Advertisement);
                    }
                    else
                    {
                        DataSet objDataSet = MasterCode.RetrieveQuery("select Short_film_Id,Title,Video as Shortfilm,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits,'../Video_Images/'+Short_film_Image as Short_film_Image from tbl_Short_film where Short_film_Id=" + ShortFilmId);
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            add.InnerHtml = "<img Width='773px' Height='435px'  src='" + objDataSet.Tables[0].Rows[0]["Short_film_Image"].ToString() + "'/>";

                            lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                            Display_List(lstMostLiked, "Select top 4 Short_film_Id,Title,Hero,Video,Visits,'frmSingle.aspx?shortfilm='+cast(Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,Duration,0),108),5)as Duration from tbl_Short_film where Isactive='True' and Status='Approve' and Channel in (Select Channel from tbl_Short_film where Short_film_Id=" + ShortFilmId + ") order by Short_film_Id desc ");

                            lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                            lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                            lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();
                        }

                        //Display_Film(ShortFilmId, userId1);
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

    //protected void lnkshortfilm_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnk = sender as LinkButton;
    //        ListViewItem Row = (ListViewItem)lnk.NamingContainer;
    //        if (Session["UserId"] != null)
    //        {
    //            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,'../Videos/'+Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True' and Advertisement_Id=" + lnk.CommandName);
    //            if (objDataSet.Tables[0].Rows.Count > 0)
    //            {
    //                lblNextVideo.Text = "";
    //                lblAddId.Text = lnk.CommandName;
    //                lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
    //                add.InnerHtml = "<video id='Video' height='376px' width='100%' autoplay onended='Add_Budget()'><source id='add123' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "' type='video/mp4'>";

    //                lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
    //                lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception Ex)
    //    {

    //    }
    //}

    protected void btnPlay_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] != null)
            {
                Display_Film(txtShortFilmId.Text.Trim(), txtVideoUserId.Text,"Film");
            }
            else
                Response.Redirect("~/Login.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void btnTrailer_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] != null)
            {
                Display_Film(txtShortFilmId.Text.Trim(), txtVideoUserId.Text,"Trailer");
            }
            else
                Response.Redirect("~/Login.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }
}
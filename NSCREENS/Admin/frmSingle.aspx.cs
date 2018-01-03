using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;

public partial class Admin_frmSingle : System.Web.UI.Page
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
            string Gender = "", DOB = "";

            DataSet objDataSet11 = MasterCode.RetrieveQuery("Select Gender,DATEDIFF(hour,DOB,GETDATE())/8766 AS AgeYearsIntTrunc from tbl_Register_User R join tbl_user U on R.Register_Id=U.Staff_Id where U.Username='" + Session["UserName"] + "'");
            if (objDataSet11.Tables[0].Rows.Count > 0)
            {
                Gender = objDataSet11.Tables[0].Rows[0][0].ToString();
                DOB = objDataSet11.Tables[0].Rows[0][1].ToString();
            }

            DataSet objDataSetgeetsht = MasterCode.RetrieveQuery("Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + ShortFilmId);


            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,Video as Advertisement,CONVERT(varchar(12),CreatedDate,100)as Publish,Description from tbl_Advertisement where Isactive='True' and Gender in ('ALL','" + Gender + "') and " + DOB + " >= Agefrom and " + DOB + " <= Ageto and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and ShortFilmId Like '%," + objDataSetgeetsht.Tables[0].Rows[0][0].ToString() + ",%' and Advertisement_Id not in (select Advertisement_Id from tbl_Advertizment_Visits where User_Id=" + Session["UserId"].ToString() + ")");
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                if (objDataSet.Tables[0].Rows.Count == -1)
                {
                    DataSet obj = MasterCode.RetrieveQuery("select Lan_Short_film_Id,LS.Short_film_Id,Title,LS.Video as Shortfilm,CONVERT(varchar(12),LS.CreatedDate,100)as Publish,Description,LS.Visits,Trailer from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where Lan_Short_film_Id=" + ShortFilmId);
                    lblNextVideo.Text = obj.Tables[0].Rows[0]["Shortfilm"].ToString();
                    lblAddId.Text = objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString();
                    id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId);
                    lblTitle.Text = obj.Tables[0].Rows[0]["Title"].ToString();

                    txtNextPN.Text = "No";
                    add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "?autoplay=1&api=1&player_id=player1' style='Height:550px; Width:1140px;' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                    lblPublished.Text = obj.Tables[0].Rows[0]["Publish"].ToString();
                    lblDescription.Text = obj.Tables[0].Rows[0]["Description"].ToString();
                    lblViews.Text = obj.Tables[0].Rows[0]["Visits"].ToString();
                    //DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString());

                    //DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                    //if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                    //{
                    //    DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Advertizment_Visits(Advertisement_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing) values(" + objDataSet.Tables[0].Rows[0]["Advertisement_Id"].ToString() + "," + Session["UserId"].ToString() + ",'" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt") + "','Eluru','" + Request.UserHostAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + ")");
                    //}

                    Display_List(lstRelatedVideos, "Select top 18 Lan_Short_film_Id,LS.Short_film_Id,Title,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(LS.Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true'  and Channel in (Select Channel from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + ShortFilmId + ")) order by Short_film_Id desc ");

                }
                else
                {
                    if (txtVideoUserId.Text.Trim() == "0")
                        Response.Redirect("frmAllAdvatizment.aspx?ShortFilm=" + ShortFilmId, false);
                    else
                        Response.Redirect("frmAllAdvatizment.aspx?ShortFilm=" + ShortFilmId + "&userId=" + txtVideoUserId.Text.Trim(), false);
                }
            }
            else
            {
                if (txtVideoUserId.Text.Trim() == "0")
                    Response.Redirect("frmAllAdvatizment.aspx?ShortFilm=" + ShortFilmId, false);
                else
                    Response.Redirect("frmAllAdvatizment.aspx?ShortFilm=" + ShortFilmId + "&userId=" + txtVideoUserId.Text.Trim(), false);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public void DisplayOnly_Add(string ShortFilmId, string shortfilm)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("Select Advertisement_Id,Title,Video as Advertisement,Tag,CONVERT(varchar(12),CreatedDate,100)as Publish,Description,Visits from tbl_Advertisement where Isactive='True' and Advertisement_Id=" + ShortFilmId);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                //lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                DataSet obj = MasterCode.RetrieveQuery("select Lan_Short_film_Id,LS.Short_film_Id,Title,LS.Video as Shortfilm,CONVERT(varchar(12),LS.CreatedDate,100)as Publish,Description,LS.Visits,Trailer from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where Lan_Short_film_Id=" + shortfilm);
                lblNextVideo.Text = obj.Tables[0].Rows[0]["Shortfilm"].ToString();
                txtNextPN.Text = "No";
                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Advertisement"].ToString() + "?autoplay=1&api=1&player_id=player1' style='Height:550px; Width:1140px;' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                //lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                //lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                //lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();
                //lblTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();
            }

            string Gender = "", DOB = "";

            DataSet objDataSetA = MasterCode.RetrieveQuery("Select Gender,DATEDIFF(hour,DOB,GETDATE())/8766 AS AgeYearsIntTrunc from tbl_Register_User R join tbl_user U on R.Register_Id=U.Staff_Id where U.Username='" + Session["UserName"] + "'");
            if (objDataSetA.Tables[0].Rows.Count > 0)
            {
                Gender = objDataSetA.Tables[0].Rows[0][0].ToString();
                DOB = objDataSetA.Tables[0].Rows[0][1].ToString();
            }

            //Display_List(lstRelatedVideos, "Select top 3 Lan_Short_film_Id,LS.Short_film_Id,Title,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(LS.Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true'  and Channel in (Select Channel from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + ShortFilmId + ")) order by Short_film_Id desc ");

        }
        catch (Exception Ex)
        {

        }
    }

    public void Display_Film(string ShortFilmId, string UserId, string VideoType)
    {
        try
        {
            string LoadURL = "";

            DataSet objDataSetBudget = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
            if (objDataSetBudget.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDecimal(objDataSetBudget.Tables[0].Rows[0][0].ToString()) >= 2)
                {
                    if (ShortFilmId != "")
                    {
                        DataSet objDataSet = MasterCode.RetrieveQuery("select Lan_Short_film_Id,LS.Short_film_Id,Title,LS.Video as Shortfilm,CONVERT(varchar(12),LS.CreatedDate,100)as Publish,Description,LS.Visits,Trailer,LS.CreatedById from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where Lan_Short_film_Id=" + ShortFilmId);
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                            LoadURL = objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString();
                            txtNextPN.Text = "";

                            id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString());
                            Googleplus.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString());

                            lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                            lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                            lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();

                            if (VideoType == "Film")
                            {
                                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSet.Tables[0].Rows[0]["Shortfilm"].ToString() + "?autoplay=1&api=1&player_id=player1' style='Height:550px; Width:1140px;' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";

                                DataSet objDataSetGetBudget = null;
                                if (UserId == "" || UserId == null)
                                    UserId = "0";

                                if (UserId == "0")
                                    objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 2 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                                else
                                    objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 3 else 3 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");

                                //objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 2 else 1 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");
                                //objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode IS NULL then 3 else 4 end from tbl_Register_User where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + "))");

                                decimal Earning = 0;

                                if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
                                {
                                    DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Short_Film_Visits(Short_Film_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing,User_Share_Id) values(" + ShortFilmId + "," + Session["UserId"].ToString() + ",'" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt") + "','Eluru','" + Request.UserHostAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + "," + UserId + ")");

                                    //if (UserId == objDataSetGetBudget.Tables[0].Rows[0]["CreatedById"].ToString())
                                    //    Earning = Earning + Convert.ToDecimal(objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"]);
                                    //if (UserId == Session["UserId"].ToString())
                                    //    Earning = Earning + Convert.ToDecimal(objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"]);

                                    DataSet objDataSetMinBud23 = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + objDataSet.Tables[0].Rows[0]["CreatedById"].ToString() + ")");
                                    DataSet objDataSetMin1Bud23 = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=1)");
                                    if (UserId != "0")
                                    {
                                        DataSet objDataSetMinBud1 = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");
                                    }
                                }

                                DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Language_Short_FilmId set Visits=Visits+1 where Lan_Short_film_Id=" + ShortFilmId);
                                DataSet objDataSetMinBud = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget-(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                                DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                                if (objDataSet2.Tables[0].Rows.Count > 0)
                                    this.Master.Amount = objDataSet2.Tables[0].Rows[0][0].ToString();

                                if (Session["UserName"].ToString() == "admin")
                                {
                                    //DataSet objDataSetE = MasterCode.RetrieveQuery("Select (Select case when SUM(Promoter_Budget) is null then 0 else SUM(Promoter_Budget) end +case when SUM(User_Budget) is null then 0 else SUM(User_Budget) end from tbl_Short_Film_Visits where Short_Film_Id in (Select Lan_Short_film_Id from tbl_Language_Short_FilmId where CreatedById=" + Session["UserId"].ToString() + "))+(Select case when SUM(Video_Sharing) is null then 0 else SUM(Video_Sharing) end from tbl_Short_Film_Visits where User_Share_Id=" + Session["UserId"].ToString() + ")+(Select case when sum(User_Budget) is null then 0 else sum(User_Budget) end +case when sum(Promoter_Budget) is null then 0 else sum(Promoter_Budget) end from tbl_Advertizment_Visits where Advertisement_Id in (Select Advertisement_Id from tbl_Advertisement where CreatedById=" + Session["UserId"].ToString() + "))");
                                    DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                                    if (objDataSetE.Tables[0].Rows.Count > 0)
                                        this.Master.EarningAmount = "( " + objDataSetE.Tables[0].Rows[0][0].ToString() + " )";
                                    else
                                        this.Master.EarningAmount = "0.00";
                                }
                                else
                                {
                                    DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + Session["UserId"].ToString() + ")");
                                    if (objDataSetE.Tables[0].Rows.Count > 0)
                                        this.Master.EarningAmount = "( " + objDataSetE.Tables[0].Rows[0][0].ToString() + " )";
                                    else
                                        this.Master.EarningAmount = "0.00";
                                }

                                //DataSet objDataSetE = MasterCode.RetrieveQuery("Select (Select case when SUM(Promoter_Budget) is null then 0 else SUM(Promoter_Budget) end+case when SUM(Video_Sharing) is null then 0 else SUM(Video_Sharing) end +case when SUM(User_Budget) is null then 0 else SUM(User_Budget) end from tbl_Short_Film_Visits where User_Share_Id=" + Session["UserId"].ToString() + " or Short_Film_Id in (Select Short_film_Id from tbl_Short_film where CreatedById=" + Session["UserId"].ToString() + "))+(Select case when sum(User_Budget) is null then 0 else sum(User_Budget) end +case when sum(Promoter_Budget) is null then 0 else sum(Promoter_Budget) end from tbl_Advertizment_Visits where Advertisement_Id in (Select Advertisement_Id from tbl_Advertisement where CreatedById=" + Session["UserId"].ToString() + "))");
                                //if (objDataSet2.Tables[0].Rows.Count > 0)
                                //    this.Master.EarningAmount = "( " + objDataSetE.Tables[0].Rows[0][0].ToString() + " )";

                                ShowNotification("Film", "2/- Deducted from your wallet.. ", NotificationType.success);
                            }
                            else
                            {
                                DataSet objDataSetTrailer = MasterCode.RetrieveQuery("Select Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Trailer_Id=" + ShortFilmId + " order by Trailer_Id");
                                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSetTrailer.Tables[0].Rows[0]["Trailer_Url"].ToString() + "?autoplay=1&api=1&player_id=player1' style='Height:550px; Width:1140px;' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#myModal').modal('hide');", true);
                            }

                            Display_List(lstRelatedVideos, "Select top 18 Lan_Short_film_Id,LS.Short_film_Id,Title,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(LS.Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true'  and Channel in (Select Channel from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + ShortFilmId + ")) order by Short_film_Id desc ");

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
    public static string GetCurrentTime(string UserId, string ShortFilmId, string IpAddress)
    {
        if (Convert.ToInt32(ShortFilmId) != 0)
        {
            DataSet objDataSetGetBudget = null;
            if (UserId == "" || UserId == null)
                UserId = "0";

            if (UserId == "0")
                objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id=2");
            else
                objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id=3");

            if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
            {
                DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Short_Film_Visits(Short_Film_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing,User_Share_Id) values(" + ShortFilmId + "," + UserId + ",'" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt") + "','Eluru','" + IpAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + "," + UserId + ")");

                //if (UserId == objDataSetGetBudget.Tables[0].Rows[0]["CreatedById"].ToString())
                //    Earning = Earning + Convert.ToDecimal(objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"]);
                //if (UserId == SharingUserId)
                //    Earning = Earning + Convert.ToDecimal(objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"]);
            }

            DataSet objDataSetUp123 = MasterCode.RetrieveQuery("update tbl_Language_Short_FilmId set Visits=Visits+1 where Lan_Short_film_Id=" + ShortFilmId);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
        }

        DataSet objDataSetUp = MasterCode.RetrieveQuery("update tbl_Register_User set User_Budget=User_Budget+(Select Budget from tbl_Budget_Settings where Budget_Settings_Id=2) where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        return objDataSet2.Tables[0].Rows[0][0].ToString();
    }

    [System.Web.Services.WebMethod]
    public static string GetAdd(string UserId, string AddId, string IpAddress)
    {
        if (Convert.ToInt32(AddId) != 0)
        {
            DataSet objDataSet1 = MasterCode.RetrieveQuery("Select CreatedById from tbl_Advertisement where Advertisement_Id=" + AddId);

            DataSet objDataSetGetBudget = MasterCode.RetrieveQuery("Select '0' as Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id in (Select case when PromoCode='' then 2 else 1 end from tbl_Advertisement where Advertisement_Id=" + AddId + ")");
            if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
            {//Visitor User id

                decimal Earning = 0;
                DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Advertizment_Visits(Advertisement_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing) values(" + AddId + "," + UserId + ",'" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt") + "','Eluru','" + IpAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + ")");

                //if (UserId == objDataSet1.Tables[0].Rows[0]["CreatedById"].ToString())
                Earning = Earning + Convert.ToDecimal(objDataSetGetBudget.Tables[0].Rows[0]["Promoter"]);

                DataSet objDataSetMinBud = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + Earning + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + objDataSet1.Tables[0].Rows[0]["CreatedById"].ToString() + ")");
                DataSet objDataSetMin1Bud23 = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=1)");
            }

            DataSet objDataSetVisits = MasterCode.RetrieveQuery("Update tbl_Advertisement set Visits=Visits+1 where Advertisement_Id=" + AddId);
        }

        DataSet objDataSet2 = MasterCode.RetrieveQuery("Select User_Budget from tbl_Register_User where Isactive='True' and Register_Id in (select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        return objDataSet2.Tables[0].Rows[0][0].ToString();
    }

    [System.Web.Services.WebMethod]
    public static string GetCurrentTimeShortFilm(string SharingUserId, string UserId, string ShortFilmId, string IpAddress)
    {
        if (Convert.ToInt32(ShortFilmId) != 0)
        {
            DataSet objDataSet1 = MasterCode.RetrieveQuery("Select CreatedById from tbl_Language_Short_FilmId where Short_Film_Id=" + ShortFilmId);

            DataSet objDataSetGetBudget = null;

            if (SharingUserId == "0")
                objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id=2");
            else
                objDataSetGetBudget = MasterCode.RetrieveQuery("Select Short_Film,Admin,Promoter,Video_Sharing from tbl_Budget_Settings where Budget_Settings_Id=3");

            decimal Earning = 0;
            if (objDataSetGetBudget.Tables[0].Rows.Count > 0)
            {
                DataSet objDataSetVisits12 = MasterCode.RetrieveQuery("insert into tbl_Short_Film_Visits(Short_Film_Id,User_Id,Date_Time,City_Id,IPAddress,User_Budget,Promoter_Budget,Admin_Budget,Video_Sharing,User_Share_Id) values(" + ShortFilmId + "," + UserId + ",'" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss tt") + "','Eluru','" + IpAddress + "'," + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Promoter"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + "," + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + "," + SharingUserId + ")");

                DataSet objDataSetMinBud = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Short_Film"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + objDataSet1.Tables[0].Rows[0]["CreatedById"].ToString() + ")");
                DataSet objDataSetMinBud1 = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Video_Sharing"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + SharingUserId + ")");
                DataSet objDataSetMin1Bud23 = MasterCode.RetrieveQuery("update tbl_Register_User set EarningAmt=EarningAmt+" + objDataSetGetBudget.Tables[0].Rows[0]["Admin"] + " where Register_Id in (Select Staff_Id from tbl_user where User_Id=1)");

            }

            DataSet objDataSetUp123 = MasterCode.RetrieveQuery("update tbl_Language_Short_FilmId set Visits=Visits+1 where Lan_Short_film_Id=" + ShortFilmId);
        }
        DataSet objDataSetE = null;
        if (UserId == "1")
            objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");
        else
            objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");

        //DataSet objDataSetE = MasterCode.RetrieveQuery("Select EarningAmt from tbl_Register_User  where Register_Id in (Select Staff_Id from tbl_user where User_Id=" + UserId + ")");
        //DataSet objDataSetE = MasterCode.RetrieveQuery("Select SUM(Video_Sharing)+SUM(User_Budget) from tbl_Short_Film_Visits where User_Share_Id=" + UserId + " or Short_Film_Id in (Select Short_film_Id from tbl_Short_film where CreatedById=" + UserId + ")");

        return "( " + objDataSetE.Tables[0].Rows[0][0].ToString() + " )";
    }

    public void Load_Image(string ShortFilmId)
    {
        try
        {
            DataSet objDataSet = MasterCode.RetrieveQuery("select Lan_Short_film_Id,LS.Short_film_Id,Title,LS.Video as Shortfilm,CONVERT(varchar(12),LS.CreatedDate,100)as Publish,Description,LS.Visits,'http://www.nscreens.com/Video_Images/'+LS.Short_film_Image as Short_film_Image,Tag from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where Lan_Short_film_Id=" + ShortFilmId);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                string Add = "";
                DataSet objDataSet1 = MasterCode.RetrieveQuery("select (Select Artist_Name from tbl_admin_Artist AA where AA.Artist_Id=AD.Artist_Id)+'-'+Name as Artist from tbl_Artist_Details AD where Artist_Details_Id in (select Artist_Id from tbl_Short_Artist where Language_Short_Id=" + ShortFilmId + ")");
                if (objDataSet1.Tables[0].Rows.Count > 0)
                    for (int i = 0; i < objDataSet1.Tables[0].Rows.Count; i++)
                        Add = Add + objDataSet1.Tables[0].Rows[i][0].ToString() + "<br/>";

                //lblArtist.Text = Add;

                add.InnerHtml = "<img Width='1140px' Height='550px'  src='" + objDataSet.Tables[0].Rows[0]["Short_film_Image"].ToString() + "'/>";

                id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString());
                Googleplus.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString());

                Session["Title"] = objDataSet.Tables[0].Rows[0]["Title"].ToString();
                Session["Description"] = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                Session["Short_film_Image"] = objDataSet.Tables[0].Rows[0]["Short_film_Image"].ToString();
                Session["URL"] = "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + ShortFilmId + "&userId=" + Session["UserId"].ToString();

                lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title"].ToString();

                lblPublished.Text = objDataSet.Tables[0].Rows[0]["Publish"].ToString();
                lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                lblViews.Text = objDataSet.Tables[0].Rows[0]["Visits"].ToString();
                lblTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();

                Display_List(lstRelatedVideos, "Select top 18 Lan_Short_film_Id,LS.Short_film_Id,Title,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(LS.Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true'  and Channel in (Select Channel from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + ShortFilmId + ")) order by Short_film_Id desc ");

                Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,(Select Artist_Name from tbl_admin_Artist A where A.Artist_Id=Interest_Areas)as Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas as Artist_Name,Isactive from tbl_Artist_Details where Isactive='true' and Artist_Details_Id in (select Artist_Id from tbl_Short_Artist where Language_Short_Id=" + ShortFilmId + ") order by Name");

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

                    String myUrl = Request.RawUrl.ToString();
                    var result = Path.GetFileName(myUrl);
                    String Folder = Path.GetDirectoryName(myUrl);
                    string[] SplitOffer = Folder.Split('\\');
                    for (int i = 0; i < SplitOffer.Length; i++)
                        if (i == 1)
                            Master.master_lblUrl = "../" + SplitOffer[i] + "/" + result;



                    #region Start Film

                    DataSet objDataSet369 = MasterCode.RetrieveQuery("Select Channel_Name,'MyProfile.aspx?ProductionId='+cast(Channel_Id as varchar(max)) as ProductionId from tbl_admin_channel where CreatedById=" + Session["UserId"].ToString());
                    if (objDataSet369.Tables[0].Rows.Count > 0)
                    {
                        lblProduction.Text = objDataSet369.Tables[0].Rows[0]["Channel_Name"].ToString();
                        aproduction.HRef = objDataSet369.Tables[0].Rows[0]["ProductionId"].ToString();
                    }
                    else
                    {
                        lblProduction.Text = "";
                        aproduction.HRef = "#";
                    }

                    string TrailerId = Request.QueryString["Trailer"];
                    txtUserId.Text = Session["UserId"].ToString();
                    if (Request.QueryString["Trailer"] == null)
                    {
                        fbacc.Visible = true;
                        gacc.Visible = true;
                        btnShortFilm.Enabled = true;
                        ArtistList.Visible = true;
                        string ShortFilmId = Request.QueryString["shortfilm"];
                        string userId1 = Request.QueryString["userId"];
                        if (Request.QueryString["userId"] == null)
                            txtVideoUserId.Text = "0";
                        else
                            txtVideoUserId.Text = userId1;

                        txtShortFilmId.Text = ShortFilmId;


                        if (Request.QueryString["shortfilm"] != null && Request.QueryString["Advertisement"] != null)
                        {
                            DataSet objDataSet = MasterCode.RetrieveQuery("select count(*) from tbl_Advertizment_Visits where Advertisement_Id=" + Request.QueryString["Advertisement"] + " and User_Id=" + Session["UserId"].ToString());
                            if (Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString()) == 0)
                            {
                                lblAddId.Text = Request.QueryString["Advertisement"].ToString();

                                string Advertisement = Request.QueryString["Advertisement"];
                                string shortfilm = Request.QueryString["shortfilm"];

                                Load_Image(ShortFilmId);

                                DisplayOnly_Add(Advertisement, shortfilm);
                            }
                            else
                                Response.Redirect("frmAllAdvatizment.aspx?ShortFilm=" + Request.QueryString["shortfilm"], false);
                        }
                        //else if (Request.QueryString["shortfilm"] == null)
                        //{
                        //    string Advertisement = Request.QueryString["Advertisement"];
                        //    DisplayOnly_Add(Advertisement);
                        //}
                        else
                        {
                            Load_Image(ShortFilmId);
                        }
                    }
                    else
                    {
                        btnShortFilm.Enabled = false;
                        gacc.Visible = false;
                        fbacc.Visible = false;
                        ArtistList.Visible = false;
                        DataSet objDataSet = MasterCode.RetrieveQuery("Select Trailer_Id,Trailer_Url,Title_Name,Tag,convert(varchar(12),R.CreatedDate,100) as CreatedDate,'../Video_Images/'+Image as Short_film_Image,R.CreatedById from tbl_Trailer T join tbl_Register_Title R on T.Register_Title_Id=R.Title_Id where R.Title_Id=" + TrailerId);
                        if (objDataSet.Tables[0].Rows.Count > 0)
                        {
                            add.InnerHtml = "<img Width='1140px' Height='550px'  src='" + objDataSet.Tables[0].Rows[0]["Short_film_Image"].ToString() + "'/>";

                            //id.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + TrailerId);
                            //Googleplus.Attributes.Add("data-href", "http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=" + TrailerId);

                            lblTitle.Text = objDataSet.Tables[0].Rows[0]["Title_Name"].ToString();
                            lblTag.Text = objDataSet.Tables[0].Rows[0]["Tag"].ToString();

                            lblPublished.Text = objDataSet.Tables[0].Rows[0]["CreatedDate"].ToString();
                            Display_List(lstRelatedVideos, "Select top 18 Lan_Short_film_Id,LS.Short_film_Id,Title,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(LS.Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true'  and Channel in (Select Channel from tbl_Short_film where CreatedById=" + objDataSet.Tables[0].Rows[0]["CreatedById"].ToString() + ") order by Lan_Short_film_Id desc ");
                        }
                        else
                        {
                            DataSet objDataSetF = MasterCode.RetrieveQuery("Select Title_Id,Title_Name,Tag,convert(varchar(12),R.CreatedDate,100) as CreatedDate,'../Video_Images/'+Image as Short_film_Image,R.CreatedById from tbl_Register_Title R where R.Title_Id=" + TrailerId);
                            if (objDataSetF.Tables[0].Rows.Count > 0)
                            {
                                add.InnerHtml = "<img Width='1140px' Height='550px'  src='" + objDataSetF.Tables[0].Rows[0]["Short_film_Image"].ToString() + "'/>";

                                lblTitle.Text = objDataSetF.Tables[0].Rows[0]["Title_Name"].ToString();
                                lblTag.Text = objDataSetF.Tables[0].Rows[0]["Tag"].ToString();

                                lblPublished.Text = objDataSetF.Tables[0].Rows[0]["CreatedDate"].ToString();
                            }
                            Display_List(lstRelatedVideos, "Select top 18 Lan_Short_film_Id,LS.Short_film_Id,Title,Hero,LS.Video,LS.Visits,'frmSingle.aspx?shortfilm='+cast(LS.Lan_Short_film_Id as varchar(max)) as shortfilm,'../Video_Images/'+LS.Short_film_Image as Short_film_Image,RIGHT(CONVERT(CHAR(8),DATEADD(second,LS.Duration,0),108),5)as Duration from tbl_Short_film SF join tbl_Language_Short_FilmId LS on SF.Short_film_Id=LS.Short_Film_Id where LS.Isactive='True' and LS.Status='Approve' and LS.Publish='true'  and Channel in (Select Channel from tbl_Short_film where CreatedById=" + objDataSetF.Tables[0].Rows[0]["CreatedById"].ToString() + ") order by Lan_Short_film_Id desc ");
                        }


                    }
                    #endregion

                }
                else
                    Response.Redirect("~/Login.aspx", false);
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
                Display_Film(txtShortFilmId.Text.Trim(), txtVideoUserId.Text, "Film");
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
            //Display_List(lstTrailers, "Select 'Trailer-'+cast(ROW_NUMBER() OVER (ORDER BY Trailer_Id) as varchar(max)) AS serialnumber,Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Isactive='true' and Status='Approve' and ShortFilm_Id=" + txtShortFilmId.Text.Trim() + " order by Trailer_Id");
            if (Request.QueryString["Trailer"] == null)
            {//Select 'Trailer-'+cast(ROW_NUMBER() OVER (ORDER BY Trailer_Id) as varchar(max)) AS serialnumber,Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Isactive='true' and Status='Approve' and ShortFilm_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=23) or Register_Title_Id in (Select Title_Id from tbl_Register_Title where Title_Name in (Select distinct Title from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=23)) and Languages in (Select Language from tbl_Language_Short_FilmId where Lan_Short_film_Id=23)) order by Trailer_Id
                DataSet objDataSet = MasterCode.RetrieveQuery("Select count(*) from tbl_Trailer where Isactive='true' and Status='Approve' and ShortFilm_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + txtShortFilmId.Text.Trim() + ")");
                if (objDataSet.Tables[0].Rows[0][0].ToString() == "0")
                {
                    //Select Language from tbl_Language_Short_FilmId where Lan_Short_film_Id=23
                    DataSet objDataSet1 = MasterCode.RetrieveQuery("Select Language from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + txtShortFilmId.Text.Trim());

                    Display_List(lstTrailers, "Select 'Trailer-'+cast(ROW_NUMBER() OVER (ORDER BY Trailer_Id) as varchar(max)) AS serialnumber,Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Isactive='true' and Status='Approve' and (ShortFilm_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + txtShortFilmId.Text.Trim() + ") or Register_Title_Id in (Select Title_Id from tbl_Register_Title where Title_Name in (Select distinct Title from tbl_Short_film where Short_film_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + txtShortFilmId.Text.Trim() + ")) and Languages Like '%," + objDataSet1.Tables[0].Rows[0][0].ToString() + ",%')) order by Trailer_Id");
                }
                else
                    Display_List(lstTrailers, "Select 'Trailer-'+cast(ROW_NUMBER() OVER (ORDER BY Trailer_Id) as varchar(max)) AS serialnumber,Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Isactive='true' and Status='Approve' and ShortFilm_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + txtShortFilmId.Text.Trim() + ") order by Trailer_Id");

                //Display_List(lstTrailers, "Select 'Trailer-'+cast(ROW_NUMBER() OVER (ORDER BY Trailer_Id) as varchar(max)) AS serialnumber,Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Isactive='true' and Status='Approve' and ShortFilm_Id in (Select Short_Film_Id from tbl_Language_Short_FilmId where Lan_Short_film_Id=" + txtShortFilmId.Text.Trim() + ") order by Trailer_Id");
            }
            else
            {
                Display_List(lstTrailers, "Select 'Trailer-'+cast(ROW_NUMBER() OVER (ORDER BY Trailer_Id) as varchar(max)) AS serialnumber,Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Isactive='true' and Status='Approve' and Register_Title_Id=" + Request.QueryString["Trailer"].ToString() + " order by Trailer_Id");
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);

            //if (Session["UserId"] != null)
            //{
            //    Display_Film(txtShortFilmId.Text.Trim(), txtVideoUserId.Text, "Trailer");
            //}
            //else
            //    Response.Redirect("~/Login.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void img_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = sender as ImageButton;

            if (Session["UserId"] != null)
            {
                //Display_Film(btn.CommandName, txtVideoUserId.Text, "Trailer");

                DataSet objDataSetTrailer = MasterCode.RetrieveQuery("Select Trailer_Id,Trailer_Url,case when ShortFilm_Id is null then (Select '../Video_Images/'+Image from tbl_Register_Title R where R.Title_Id=TT.Register_Title_Id)  else (Select '../Video_Images/'+Short_film_Image from tbl_Language_Short_FilmId F where F.Short_Film_Id=TT.ShortFilm_Id) end as Image,(Select Language_Name from tbl_admin_language L where L.Language_Id=TT.LanguageId)as Language_Name from tbl_Trailer TT where Trailer_Id=" + btn.CommandName + " order by Trailer_Id");
                add.InnerHtml = "<iframe id='player1' runat='server' src='" + objDataSetTrailer.Tables[0].Rows[0]["Trailer_Url"].ToString() + "?autoplay=1&api=1&player_id=player1' style='Height:550px; Width:1140px;' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#myModal').modal('hide');", true);
            }
            else
                Response.Redirect("~/Login.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkMoreDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string ShortFilmId = Request.QueryString["shortfilm"];

            Display_List(lstRecentVideos, "Select Artist_Details_Id,Name,(Select Artist_Name from tbl_admin_Artist A where A.Artist_Id=Interest_Areas)as Description,'../Artist_Photo/'+Photo as Photo,Gender,Interest_Areas as Artist_Name,Isactive from tbl_Artist_Details where Isactive='true' and Artist_Details_Id in (select Artist_Id from tbl_Short_Artist where Language_Short_Id=" + ShortFilmId + ") order by Name");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupMore()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lstRecentVideos_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Display")
            {
                DataSet objDataSet = MasterCode.RetrieveQuery("Select Artist_Details_Id,Name,Description,'../Artist_Photo/'+Photo as Photo,Gender,Artist_Name,(Select City_Name from tbl_admin_city AC where AC.City_Id=AD.LocationId)as City_Name,ContactInformation,languagesId,(Select ','+Language_Name from tbl_admin_language where Language_Id in (select val from fn_String_To_Table(LanguagesId,',',1))FOR XML PATH (''))as Languages from tbl_Artist_Details AD join tbl_admin_Artist AA on AA.Artist_Id=AD.Interest_Areas where AD.Isactive='true' and Artist_Details_Id=" + (e.Item.FindControl("lblGridArtistId") as Label).Text);
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    imgPhoto.ImageUrl = objDataSet.Tables[0].Rows[0]["Photo"].ToString();
                    lblName.Text = objDataSet.Tables[0].Rows[0]["Name"].ToString();
                    lblArtist.Text = objDataSet.Tables[0].Rows[0]["Artist_Name"].ToString();
                    lblGender.Text = objDataSet.Tables[0].Rows[0]["Gender"].ToString();
                    lblCity.Text = objDataSet.Tables[0].Rows[0]["City_Name"].ToString();
                    lblDescription1.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                    lblContactInformation.Text = objDataSet.Tables[0].Rows[0]["ContactInformation"].ToString();
                    txtLanguagesDis.Text = objDataSet.Tables[0].Rows[0]["Languages"].ToString();
                }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupView()", true);
            }
        }
        catch (Exception Ex)
        {

        }
    }
}
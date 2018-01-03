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

                //string CityId = "";
                //string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (string.IsNullOrEmpty(ipAddress))
                //{
                //    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                //}

                //string APIKey = "81dce31671738f9a3fdf05ee98c9a869dc78242e9603381233b3e663f0fcca3d";
                //string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, ipAddress);
                //using (WebClient client = new WebClient())
                //{
                //    string json = client.DownloadString(url);
                //    Location location = new JavaScriptSerializer().Deserialize<Location>(json);
                //    DataSet objDataSet = MasterCode.RetrieveQuery("Select City_Id from tbl_admin_city where City_Name='" + location.CityName + "'");
                //    if (objDataSet.Tables[0].Rows.Count > 0)
                //        CityId = objDataSet.Tables[0].Rows[0][0].ToString();
                //    //List<Location> locations = new List<Location>();
                //    //locations.Add(location);
                //    //gvLocation.DataSource = locations;
                //    //gvLocation.DataBind();
                //}

                //Display_List(lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?/'+cast(Advertisement_Id as varchar(max))+'/shortfilm' as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image from tbl_Advertisement where Isactive='True' and Status='Approve' and City_Id='" + CityId + "' Order by Advertisement_Id desc");
                //string ShortId = Request.QueryString["shortfilm"];
                //if (ShortId==null)
                Display_List(lstRecentVideos, "select Channel_Id,Channel_Name,Description,'../ProductionImg/'+Img as Img from tbl_admin_channel where Isactive='true' Order by Channel_Name");
                //else
                //    Display_List(lstRecentVideos, "Select Advertisement_Id,Title,Video,Visits,'frmSingle.aspx?Advertisement='+cast(Advertisement_Id as varchar(max)) as shortfilm,'../Video_Images/'+Advertisement_Image as Short_film_Image from tbl_Advertisement where Isactive='True' and Status='Approve' and NoofVisits>Visits and EndDate>'" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and StartDate<='" + DateTime.Now.AddHours(Connection.SetHours).ToString("yyyy-MM-dd") + "' and  ShortFilmId Like '%," + ShortId + ",%' Order by Advertisement_Id desc");
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }
}
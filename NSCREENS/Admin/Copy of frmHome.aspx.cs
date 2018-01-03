using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_frmHome : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DataSet objDataSet = MasterCode.ExcuteOneParameter(DateTime.Now.AddHours(Connection.SetHours).ToShortDateString(), "Sp_Home", DateTime.Now.AddHours(Connection.SetHours).AddDays(1).ToShortDateString());
                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    lblTotalBuses.Text = objDataSet.Tables[0].Rows[0][0].ToString();
                    lblRunningBuses.Text = objDataSet.Tables[2].Rows[0][0].ToString();
                    lblVacantBuses.Text = objDataSet.Tables[1].Rows[0][0].ToString();
                    lblHoldedBuses.Text = objDataSet.Tables[3].Rows[0][0].ToString();

                    if (objDataSet.Tables[4].Rows.Count > 5)
                        pnlRelesing.ScrollBars.Equals("Vertical");

                    if (objDataSet.Tables[5].Rows.Count > 5)
                        pnlBilling.ScrollBars.Equals("Vertical");

                    if (objDataSet.Tables[6].Rows.Count > 5)
                        pnlGoing.ScrollBars.Equals("Vertical");

                    if (objDataSet.Tables[6].Rows.Count > 5)
                        pnlPending.ScrollBars.Equals("Vertical");

                    if (objDataSet.Tables[7].Rows.Count > 5)
                        pnlPendingQuotations.ScrollBars.Equals("Vertical");

                    gvNewBus.DataSource = objDataSet.Tables[4];
                    gvNewBus.DataBind();

                    gvQuotation.DataSource = objDataSet.Tables[5];
                    gvQuotation.DataBind();

                    gvVendors.DataSource = objDataSet.Tables[6];
                    gvVendors.DataBind();

                    gvPendingWork.DataSource = objDataSet.Tables[8];
                    gvPendingWork.DataBind();

                    gvPendingQuotations.DataSource = objDataSet.Tables[7];
                    gvPendingQuotations.DataBind();

                    gvunallocateQuotations.DataSource = objDataSet.Tables[9];
                    gvunallocateQuotations.DataBind();
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton rdb = sender as LinkButton;
            GridViewRow Row = (GridViewRow)rdb.NamingContainer;

            lblDumpQuotationId.Text = (Row.FindControl("lblGridNew_Quotation_Id") as Label).Text;

            DataSet objDataSet = MasterCode.RetrieveQuery("Select Running_Bus_Id,(Select BusDepot_Name from tbl_admin_BusDepot AB where AB.BusDepot_Id=RB.BusDepotNameId)as BusDepot_Name,(Select BusModelType_Name from tbl_admin_busmodeltype MT where MT.BusModelType_Id=RB.BusModelType_Id)as BusModelType_Name,(Select COUNT(*) from tbl_new_bus where Quotation_Id=0 and BusDepotNameId in (Select BusDepot_Id from tbl_admin_BusDepot BD where BD.BusDepot_Id=RB.BusDepotNameId) and BusModelType in (Select BusModelType_Id from tbl_admin_busmodeltype BT where BT.BusModelType_Id=RB.BusModelType_Id)) as AvailableBuses,Required_Qty,Mounting_Chrg,Printing_Chrg,(Select NetAmt from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as NetAmt,(Select Buses_Ids from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as Buses_Ids,(Select UnitPrice from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as UnitPrice,(Required_Qty*(Select NoofDays*UnitPrice from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + "))as Amount,((Required_Qty*(Select (NoofDays*UnitPrice) from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + "))+(Select Mounting_Chrg+Printing_Chrg from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + "))as TotalAmount,(Select (Select (Select Company_Name from tbl_Client TC where TC.Client_Id=C.Client_Id) from tbl_admin_campaign C where C.Campaign_Id=NQ.Campaign_Id) from tbl_new_quotation NQ where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as Company_Name,(Select (Select Logo from tbl_Company TC where TC.Company_Id=NQ.Company_Id) from tbl_new_quotation NQ where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as Logo,(Select (Select (Select Billing_Address from tbl_Client TC where TC.Client_Id=C.Client_Id) from tbl_admin_campaign C where C.Campaign_Id=NQ.Campaign_Id) from tbl_new_quotation NQ where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as Billing_Address,(Select (Select (Select Contact_Number from tbl_Client TC where TC.Client_Id=C.Client_Id) from tbl_admin_campaign C where C.Campaign_Id=NQ.Campaign_Id) from tbl_new_quotation NQ where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as Contact_Number,(Select NetAmt-Grass from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as TaxAmt,(Select Grass from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as Grass,(Select NoofDays from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as NoofDays,(Select PMchrg from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as PMCharges,(Select CONVERT(varchar(12),ToDate,100) from tbl_new_quotation where New_Quotation_Id=" + lblDumpQuotationId.Text + ")as ToDate from tbl_running_buses RB where Quotation_Id=" + lblDumpQuotationId.Text);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                lblQuotationDate.Text = Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["ToDate"].ToString()).ToString("dd/MM/yyyy");
                lblQuotationNumber.Text = "GT/Q/" + lblDumpQuotationId.Text;

                lblTaxAmount.Text = objDataSet.Tables[0].Rows[0]["TaxAmt"].ToString();
                lblGrossAmount.Text = objDataSet.Tables[0].Rows[0]["Grass"].ToString();
                lblNetAmount.Text = objDataSet.Tables[0].Rows[0]["NetAmt"].ToString();

                lblCompanyName.Text = objDataSet.Tables[0].Rows[0]["Company_Name"].ToString();
                lblmobileno.Text = objDataSet.Tables[0].Rows[0]["Contact_Number"].ToString();
                lblAddress.Text = objDataSet.Tables[0].Rows[0]["Billing_Address"].ToString();
                lblNoofDays.Text = objDataSet.Tables[0].Rows[0]["NoofDays"].ToString();
                imglogo.ImageUrl = "~/Company_Logo/" + objDataSet.Tables[0].Rows[0]["Logo"].ToString();
                gvBuses.DataSource = objDataSet;
                gvBuses.DataBind();
            }
            else
            {
                gvBuses.DataSource = "";
                gvBuses.DataBind();
            }
            //DropDownList(ddlBusDepotName, "BusDepotNameId", "BusDepot_Name", "Select distinct BusDepotNameId,(Select BusDepot_Name from tbl_admin_BusDepot BD where BD.BusDepot_Id=RB.BusDepotNameId)+' ('+cast(Required_Qty as varchar(max))+') 'as BusDepot_Name from tbl_running_buses RB where Quotation_Id=" + (Row.FindControl("lblGridNew_Quotation_Id") as Label).Text, "Select");

            //string DepotId = "";
            //if (ddlBusDepotName.Items.Count > 0)
            //    for (int i = 0; i < ddlBusDepotName.Items.Count; i++)
            //        DepotId = DepotId + ddlBusDepotName.Items[i].Value.ToString() + ",";

            //if (DepotId != "")
            //    DepotId = DepotId.Remove(DepotId.Length - 1, 1);
            //DropDownList(ddlBusNumber, "New_Bus_Id", "BusNumber", "Select New_Bus_Id,BusNumber from tbl_new_bus where Isactive='True' and Quotation_Id=0 and BusDepotNameId in (" + ddlBusDepotName.SelectedValue.ToString() + ") Order by BusNumber", "Select");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopup()", true);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void lnkViewVendor_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow Row = (GridViewRow)lnk.NamingContainer;

            DataSet objDataSet = MasterCode.RetrieveQuery("Select 'GT/V/'+CAST(Vendor_Payment_Id as varchar(max))as PONo,CONVERT(varchar(12),CreatedDate,100)as CreatedDate,Total_Amount,Net_Amt,(Total_Amount-Net_Amt)as TDSAmt,Bus_Id,(Select (Select (Select City_Name from tbl_admin_city AC where AC.City_Id=AR.City_Id) from tbl_admin_route AR where AR.Route_Id=V.Location_Id) from tbl_vendor V where V.Vendor_Id=VP.Vendor_Id)as City_Name,(Select (Select '~/Company_Logo/'+Logo from tbl_Company C where C.Company_Id=NQ.Company_Id) from tbl_new_quotation NQ  where NQ.New_Quotation_Id=VP.Quotation_Id)as Logo,Description from tbl_vendor_payment VP where Vendor_Payment_Id=" + (Row.FindControl("lblGridVendor_Payment_Id") as Label).Text);
            if (objDataSet.Tables[0].Rows.Count > 0)
            {
                string Buses = objDataSet.Tables[0].Rows[0]["Bus_Id"].ToString();
                if (Buses != "")
                {
                    Buses = Buses.Remove(Buses.Length - 1, 1);
                    Display_List(gvDisplayBuses, "Select New_Bus_Id,(Select BusDepot_Name from tbl_admin_BusDepot BD where BD.BusDepot_Id=NB.BusDepotNameId)as BusDepot_Name,(Select BusModelType_Name from tbl_admin_busmodeltype MT where MT.BusModelType_Id=NB.BusModelType)as BusModelType_Name,BusNumber,(Select Route_Name from tbl_admin_route AR where AR.Route_Id=NB.RouteFrom)as RouteFrom,(Select Route_Name from tbl_admin_route AR where AR.Route_Id=NB.RouteTo)as RouteTo,Isactive from tbl_new_bus NB where Hold_Bus='False' and New_Bus_Id in (" + Buses + ")");
                }

                lblDescription.Text = objDataSet.Tables[0].Rows[0]["Description"].ToString();
                lblVendorCampaign.Text = (Row.FindControl("lblGridCampaign_Name") as Label).Text;
                ImgComapny.ImageUrl = objDataSet.Tables[0].Rows[0]["Logo"].ToString();
                lblVendorPODate.Text = objDataSet.Tables[0].Rows[0]["CreatedDate"].ToString();
                lblVendorPONumber.Text = objDataSet.Tables[0].Rows[0]["PONo"].ToString();
                lblVendorTDSAmount.Text = objDataSet.Tables[0].Rows[0]["TDSAmt"].ToString();
                lblVendorGrossAmount.Text = objDataSet.Tables[0].Rows[0]["Total_Amount"].ToString();
                lblVendorNetAmount.Text = objDataSet.Tables[0].Rows[0]["Net_Amt"].ToString();

                lblVendorName.Text = (Row.FindControl("lblGridFirst_Name") as Label).Text;
                lblVendorPhoneNo.Text = (Row.FindControl("lblGridMobile_No") as Label).Text;
                lblVendorCity.Text = objDataSet.Tables[0].Rows[0]["City_Name"].ToString();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "divPopupvendor()", true);
        }
        catch (Exception Ex)
        {

        }
    }
}
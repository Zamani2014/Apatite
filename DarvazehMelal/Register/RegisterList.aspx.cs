using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;

public partial class Register_RegisterList :PageBaseClass
{
    const string vsKey = "searchCriteria"; //ViewState key
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();

        if (!IsPostBack)
        {
            searchOrders(string.Empty);
        }

        if (Page.Request.QueryString.Count != 0)
        {
            string Province = Page.Request.QueryString["Province"];
            string City = Page.Request.QueryString["City"];
            string Region = Page.Request.QueryString["Region"];
            string BuildingType = Page.Request.QueryString["BuildingType"];
            int LowPrice = Int32.Parse(Page.Request.QueryString["LowPrice"]);
            int HighPrice = Int32.Parse(Page.Request.QueryString["HighPrice"]);
            int Range = Int32.Parse(Page.Request.QueryString["Range"]);
            int TransactionType = Int32.Parse(Page.Request.QueryString["TransactionType"]);

            if (TransactionType == 1)
            {
                string MyQuery = "SELECT * FROM BuildingTbl, OwnerTbl WHERE BuildingTbl.OwnerID = OwnerTbl.ID AND Range='" + Range + "' AND TotPrice >'" + LowPrice + "' AND TotPrice <'" + HighPrice + "' AND (Province='" + '"' + Province + '"' + "' OR CONTAINS(Province,'" + '"' + Province + '"' + "')) AND (City='" + '"' + City + '"' + "' OR CONTAINS(City,'" + '"' + City + '"' + "')) AND (Region='" + '"' + Region + '"' + "' OR CONTAINS(Region, '" + '"' + Region + '"' + "')) AND (BuildingType='" + '"' + BuildingType + '"' + "' OR CONTAINS(BuildingType, '" + '"' + BuildingType + '"' + "')) AND (TransactionType='" + '"' + "فروش" + '"' + "' OR CONTAINS(TransactionType, '" + '"' + "فروش" + '"' + "'))";
                SqlCommand cmd1 = new SqlCommand(MyQuery, sqlConnection);
                SqlDataAdapter DA = new SqlDataAdapter(cmd1);
                SqlDataReader DR = cmd1.ExecuteReader();
                DataSet DS = new DataSet();

                if (DR.Read())
                {
                    DR.Close();
                    DA.Fill(DS);

                    this.gvResults.DataSource = DS;
                    this.gvResults.DataBind();
                }
                else
                {
                    this.Label65.Visible = true;
                    this.gvResults.Visible = false;
                    this.AspNetPager1.Visible = false;
                }
            }
            else if (TransactionType == 2)
            {
                string MyQuery = "SELECT * FROM BuildingTbl, OwnerTbl WHERE BuildingTbl.OwnerID = OwnerTbl.ID AND Range='" + Range + "' AND TotPrice >'" + LowPrice + "' AND TotPrice <'" + HighPrice + "' AND (Province='" + '"' + Province + '"' + "' OR CONTAINS(Province,'" + '"' + Province + '"' + "')) AND (City='" + '"' + City + '"' + "' OR CONTAINS(City,'" + '"' + City + '"' + "')) AND (Region='" + '"' + Region + '"' + "' OR CONTAINS(Region, '" + '"' + Region + '"' + "')) AND (BuildingType='" + '"' + BuildingType + '"' + "' OR CONTAINS(BuildingType, '" + '"' + BuildingType + '"' + "')) AND (TransactionType='" + '"' + "رهن و اجاره" + '"' + "' OR CONTAINS(TransactionType, '" + '"' + "رهن و اجاره" + '"' + "') OR CONTAINS(TransactionType, '" + '"' + "رهن" + '"' + "') OR CONTAINS(TransactionType, '" + '"' + "اجاره" + '"' + "'))";
                SqlCommand cmd1 = new SqlCommand(MyQuery, sqlConnection);
                SqlDataAdapter DA = new SqlDataAdapter(cmd1);
                SqlDataReader DR = cmd1.ExecuteReader();
                DataSet DS = new DataSet();

                if (DR.Read())
                {
                    DR.Close();
                    DA.Fill(DS);

                    this.gvResults.DataSource = DS;
                    this.gvResults.DataBind();
                }
                else
                {
                    this.Label65.Visible = true;
                    this.gvResults.Visible = false;
                    this.AspNetPager1.Visible = false;
                }
            }
        }
    }
    protected void openLinkClick(object sender, EventArgs e)
    {
        int ID = 0;

        Control ctl = new Control();
        ctl = this.gvResults.SelectedRow.FindControl("IDlbl");
        Label IDlblVar = (Label)ctl;
        ID = int.Parse(IDlblVar.Text);

        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('ShowDetails.aspx?BuildingID=" + ID + "','_blank','toolbar=yes,resizable=yes,scrollbars=yes, height=900, width=1100',' ');", true);
    }
    void searchOrders(string sWhere)
    {
        SqlDataSource1.SelectCommand = "SELECT * FROM [BuildingTbl], [OwnerTbl] WHERE BuildingTbl.OwnerID = OwnerTbl.ID";
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        AspNetPager1.RecordCount = dv.Count;

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;
        gvResults.DataSource = pds;
        gvResults.DataBind();
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        searchOrders((string)ViewState[vsKey]);
    }
}
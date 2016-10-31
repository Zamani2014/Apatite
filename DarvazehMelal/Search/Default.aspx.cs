using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

public partial class Search_Default : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();


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
                }
            }
            else if (TransactionType == 2)
            {
                string MyQuery = "SELECT * FROM BuildingTbl, OwnerTbl WHERE BuildingTbl.OwnerID = OwnerTbl.ID AND Range='" + Range + "' AND TotPrice >'" + LowPrice + "' AND TotPrice <'" + HighPrice + "' AND (Province='" + '"' + Province + '"' + "' OR CONTAINS(Province,'" + '"' + Province + '"' + "')) AND (City='" + '"' + City + '"' + "' OR CONTAINS(City,'" + '"' + City + '"' + "')) AND (Region='" + '"' + Region + '"' + "' OR CONTAINS(Region, '" + '"' + Region + '"' + "')) AND (BuildingType='" + '"' + BuildingType + '"' + "' OR CONTAINS(BuildingType, '" + '"' + BuildingType + '"' + "')) AND (TransactionType='" + '"' + "فروش" + '"' + "' OR CONTAINS(TransactionType, '" + '"' + "رهن و اجاره" + '"' + "' OR CONTAINS(TransactionType, '" + '"' + "رهن" + '"' + "' OR CONTAINS(TransactionType, '" + '"' + "اجاره" + '"' + "'))";
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
                }
            }
    }
}
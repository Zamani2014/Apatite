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

public partial class LatestNews : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();

        if (Page.Request.QueryString.AllKeys.Contains("NewsID"))
        {
            int NewsID = Int32.Parse(Page.Request.QueryString["NewsID"]);
            string MyQuery = "SELECT * FROM NewsTbl WHERE ID='" + NewsID + "'";
            SqlCommand cmd0 = new SqlCommand(MyQuery, sqlConnection);
            SqlDataAdapter DA = new SqlDataAdapter(cmd0);
            SqlDataReader DR = cmd0.ExecuteReader();
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
                WebMsgBox.WebMsgBox.Show("موری برای شناسه وارد شده یافت نشد !");
            }
        }
        else
        {

        }
    }
}
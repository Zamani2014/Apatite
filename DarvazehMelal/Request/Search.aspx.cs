using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Request_Search : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();

        if (Page.Request.QueryString.AllKeys.Contains("ID"))
        {
            int ID = Int32.Parse(Page.Request.QueryString["ID"]);
            string MyQuery = "SELECT * FROM RequestTbl WHERE ID='" + ID + "'";
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
                this.Label65.Visible = true;
            }
        }
        else
        {

        }
    }
}
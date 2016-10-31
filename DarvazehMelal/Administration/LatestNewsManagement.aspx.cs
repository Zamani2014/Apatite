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

public partial class Administration_LatestNewsManagement : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ID = GenerateIDColumn.GetNewID("NewsTbl");
        string NewsSubject = this.TextBox1.Text;
        string NewsText = this.TextBox2.Text;
        string DateTime = GetDateTime.GenerateDateTime();

        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();
        string query = "INSERT INTO NewsTbl (ID, Subject, News, DateTime) values (@ID, @Subject, @News, @DateTime)";
        SqlCommand cmd = new SqlCommand(query, sqlConnection);

        cmd.Parameters.Add("@ID", SqlDbType.Int);
        cmd.Parameters.Add("@Subject", SqlDbType.NVarChar);
        cmd.Parameters.Add("@News", SqlDbType.NVarChar);
        cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);

        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters["@Subject"].Value = NewsSubject;
        cmd.Parameters["@News"].Value = NewsText;
        cmd.Parameters["@DateTime"].Value = DateTime;

        cmd.ExecuteNonQuery();
        sqlConnection.Close();
        WebMsgBox.WebMsgBox.Show("اطلاعات خبر مورد نظر با موفقیت درج گردید !");

        this.GridView1.DataBind();
    }
}
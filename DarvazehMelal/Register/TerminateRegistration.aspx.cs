using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;

public partial class Register_TerminateRegistration : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int BuildingID = int.Parse(Page.Request["bid"]);
            int ID = GenerateIDColumn.GetNewID("MapTbl");
            string saveLatitude = Page.Request["lat"].ToString();
            string saveLongitude = Page.Request["lng"].ToString();
           
            string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query1 = "SELECT BuildingID FROM MapTbl WHERE BuildingID=" + BuildingID;
            SqlCommand cmd0 = new SqlCommand(query1, sqlConnection);
            SqlDataReader dr = cmd0.ExecuteReader();

            if (dr.Read())
            {
                string query0 = "UPDATE MapTbl SET Latitude=@saveLatitude, Longitude=@saveLongitude WHERE BuildingID=" + BuildingID;
                SqlCommand cmd1 = new SqlCommand(query0, sqlConnection);

                cmd1.Parameters.Add("@saveLatitude", SqlDbType.VarChar);
                cmd1.Parameters.Add("@saveLongitude", SqlDbType.VarChar);

                cmd1.Parameters["@saveLatitude"].Value = saveLatitude;
                cmd1.Parameters["@saveLongitude"].Value = saveLongitude;
                dr.Close();
                cmd1.ExecuteNonQuery();
            }
            else
            {
                string query2 = "INSERT INTO MapTbl (ID, BuildingID, Latitude, Longitude) values (@ID, @BuildingID, @Latitude, @Longitude)";
                SqlCommand cmd1 = new SqlCommand(query2, sqlConnection);

                cmd1.Parameters.Add("@ID", SqlDbType.Int);
                cmd1.Parameters.Add("@BuildingID", SqlDbType.Int);
                cmd1.Parameters.Add("@Latitude", SqlDbType.VarChar);
                cmd1.Parameters.Add("@Longitude", SqlDbType.VarChar);

                cmd1.Parameters["@ID"].Value = ID;
                cmd1.Parameters["@BuildingID"].Value = BuildingID;
                cmd1.Parameters["@Latitude"].Value = saveLatitude;
                cmd1.Parameters["@Longitude"].Value = saveLongitude;

                dr.Close();
                cmd1.ExecuteNonQuery();
                sqlConnection.Close();

                this.Label41.Text = BuildingID.ToString();
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("خطایی رخ داده است : " + ex.Message);
            //WebMsgBox.WebMsgBox.Show(GoogleMap1.Markers.ToString());
        }
    }
}
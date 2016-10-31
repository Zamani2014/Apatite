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

public partial class Register_ShowDetails : PageBaseClass
{
    int BuildingID;
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();

        if (Page.Request.QueryString.AllKeys.Contains("BuildingID"))
        {
            BuildingID = Int32.Parse(Page.Request.QueryString["BuildingID"]);
            
            string MyQuery = "SELECT * FROM BuildingTbl, OwnerTbl WHERE BuildingTbl.OwnerID = OwnerTbl.ID AND BuildingTbl.ID='" + BuildingID + "'";
            string GeoQuery = "SELECT Latitude, Longitude FROM MapTbl WHERE BuildingID =" + BuildingID +"";
            
            SqlCommand cmd0 = new SqlCommand(MyQuery, sqlConnection);
            SqlCommand cmd1 = new SqlCommand(GeoQuery, sqlConnection);

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

            #region Comments & Facilities
            SqlDataReader DR2 = cmd0.ExecuteReader();
            if (DR2.Read())
            {
                if (DR2["Facilities"].ToString() == string.Empty)
                {
                    this.facilitiesDiv.Visible = false;
                }
                if (DR2["Comments"].ToString() == string.Empty)
                {
                    this.commentsDiv.Visible = false;
                }
            }
            else
            {
                this.facilitiesDiv.Visible = false;
                this.commentsDiv.Visible = false;
            }
            DR2.Close();
            #endregion
            #region GoogleMap
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            SqlDataReader DR1 = cmd1.ExecuteReader();

            if (DR1.Read())
            {
                if (DR1["Latitude"].ToString() == string.Empty || DR1["Longitude"].ToString() == string.Empty)
                {
                    this.googleMapdiv.Visible = false;
                }
                else
                {
                    this.GoogleMarkers1.Markers[0].Position.Latitude = double.Parse(DR1["Latitude"].ToString());
                    this.GoogleMap1.Latitude = double.Parse(DR1["Latitude"].ToString());

                    this.GoogleMarkers1.Markers[0].Position.Longitude = double.Parse(DR1["Longitude"].ToString());
                    this.GoogleMap1.Longitude = double.Parse(DR1["Longitude"].ToString());
                }
            }
            else
            {
                this.googleMapdiv.Visible = false;
            }
            DR1.Close();
            #endregion
            LoadImages();
        }
        else
        {
            string Province = Page.Request.QueryString["Province"];
            string City = Page.Request.QueryString["City"];
            string Region = Page.Request.QueryString["City"];
            string BuildingType = Page.Request.QueryString["BuildingType"];
            int LowPrice = Int32.Parse(Page.Request.QueryString["LowPrice"]);
            int HighPrice = Int32.Parse(Page.Request.QueryString["HighPrice"]);
            int Range = Int32.Parse(Page.Request.QueryString["Range"]);
            int TransactionType = Int32.Parse(Page.Request.QueryString["TransactionType"]);

            string MyQuery = "SELECT * FROM BuildingTbl, OwnerTbl WHERE BuildingTbl.OwnerID = OwnerTbl.ID AND Range='" + Range + "' AND TotPrice >" + LowPrice + "AND TotPrice <" + HighPrice + " AND Province='" + Province + "' AND City='" + City + "' AND Region='" + Region + "' AND BuildingType='" + BuildingType + "' AND TransactionType='" + TransactionType + "'";

            SqlCommand cmd0 = new SqlCommand(MyQuery, sqlConnection);
            SqlDataAdapter DA = new SqlDataAdapter(cmd0);
            SqlDataReader DR = cmd0.ExecuteReader();
            DataSet DS = new DataSet();
            DR.Read();
            BuildingID = int.Parse(DR["BuildingID"].ToString());

            if (DR.Read())
            {
                DR.Close();
                DA.Fill(DS);

                //this.gvResults.DataSource = DS;
                //this.gvResults.DataBind();
            }
            else
            {
                WebMsgBox.WebMsgBox.Show("موری برای شناسه وارد شده یافت نشد !");
                //this.Label65.Visible = true;
            }
            #region Comments & Facilities
            SqlDataReader DR2 = cmd0.ExecuteReader();
            if (DR2.Read())
            {
                if (DR2["Facilities"].ToString() == string.Empty)
                {
                    this.facilitiesDiv.Visible = false;
                }
                if (DR2["Comments"].ToString() == string.Empty)
                {
                    this.facilitiesDiv.Visible = false;
                }
            }
            else
            {
                this.facilitiesDiv.Visible = false;
                this.facilitiesDiv.Visible = false;
            }
            DR2.Close();
            #endregion
            #region GoogleMap
            string GeoQuery = "SELECT Latitude, Longitude FROM MapTbl WHERE BuildingID =" + BuildingID + "";
            SqlCommand cmd1 = new SqlCommand(GeoQuery, sqlConnection);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            SqlDataReader DR1 = cmd1.ExecuteReader();

            if (DR1.Read())
            {
                if (DR1["Latitude"].ToString() == string.Empty || DR1["Longitude"].ToString() == string.Empty)
                {
                    this.googleMapdiv.Visible = false;
                }
                else
                {
                    this.GoogleMarkers1.Markers[0].Position.Latitude = double.Parse(DR1["Latitude"].ToString());
                    this.GoogleMap1.Latitude = double.Parse(DR1["Latitude"].ToString());

                    this.GoogleMarkers1.Markers[0].Position.Longitude = double.Parse(DR1["Longitude"].ToString());
                    this.GoogleMap1.Longitude = double.Parse(DR1["Longitude"].ToString());
                }
            }
            else
            {
                this.googleMapdiv.Visible = false;
            }
            DR1.Close();
            #endregion
            LoadImages();
        }
    }
    protected void LoadImages()
    {
        try
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = "SELECT Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9 FROM BuildingImages WHERE BuildingID=" + BuildingID;
            SqlCommand cmd0 = new SqlCommand(query, sqlConnection);
            SqlDataReader dr = cmd0.ExecuteReader();
            
            if (dr.Read()) 
            {
                if (int.Parse(dr["Image1"].ToString()) != null || int.Parse(dr["Image2"].ToString()) != null || int.Parse(dr["Image3"].ToString()) != null ||
                int.Parse(dr["Image4"].ToString()) != null || int.Parse(dr["Image5"].ToString()) != null || int.Parse(dr["Image6"].ToString()) != null ||
                int.Parse(dr["Image7"].ToString()) != null || int.Parse(dr["Image8"].ToString()) != null || int.Parse(dr["Image9"].ToString()) != null)
                {
                    //this.Image1.ImageUrl = "~/GetImageFromDB.ashx?ID=" + dr["Image1"];
                    //int[] Images = new int[10];

                    //for (int i = 1; i < 10; i++)
                    //{
                    //    Images[i] = int.Parse(dr["Image" + i].ToString());
                    //    WebMsgBox.WebMsgBox.Show(Images[i].ToString());
                    //}

                    Control ctrl = new Control();
                    System.Web.UI.WebControls.ImageButton img = new System.Web.UI.WebControls.ImageButton();
                    for (int i = 1; i < 10; i++)
                    {
                        ctrl = this.Div1.FindControl("Image" + i.ToString());
                        img = (System.Web.UI.WebControls.ImageButton)ctrl;
                        img.ImageUrl = "~/GetImageFromDB.ashx?ID=" + dr["Image" + i].ToString();
                        img.DataBind();
                    }
                }
                else
                {
                    this.Div1.Visible = false;
                }
            }
            else
            {
                this.Div1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show(ex.Message + ex.StackTrace + ex.InnerException);
            Response.Write(ex.Message + ex.StackTrace);
        }
    }
    protected void Image_Command(object sender, CommandEventArgs e)
    {
        ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('ShowPhoto.aspx?BuildingID=" + BuildingID + "&ImageName=" + e.CommandArgument.ToString() +"','Graph','height=600,width=800');", true);
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register_ShowPhoto : System.Web.UI.Page
{
    int BuildingID;
    string ImageName;

    protected void Page_Load(object sender, EventArgs e)
    {
        BuildingID = int.Parse(Page.Request.QueryString["BuildingID"].ToString());
        ImageName = Page.Request.QueryString["ImageName"].ToString();
        LoadImages();
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

                    this.Image1.ImageUrl = "~/GetImageFromDB.ashx?ID=" + dr[ImageName.ToString()].ToString();
                }
                else
                {
                    Response.Write("متاسفانه در نمایش تصویر مشکلی ایجاد شده است، در صورت تکرار با مدیر سایت تماس بگیرید .");
                }
            }
            else
            {
                Response.Write("متاسفانه در نمایش تصویر مشکلی ایجاد شده است، در صورت تکرار با مدیر سایت تماس بگیرید .");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show(ex.Message + ex.StackTrace + ex.InnerException);
            Response.Write(ex.Message + ex.StackTrace);
        }
    }

}
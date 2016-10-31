<%@ WebHandler Language="C#" Class="GetImageFromDB" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class GetImageFromDB : IHttpHandler 
{
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.Clear();

        if (!String.IsNullOrEmpty(context.Request.QueryString["ID"]))
        {
            int id = Int32.Parse(context.Request.QueryString["ID"]);

            Image image = GetImage(id);

            context.Response.ContentType = "image/jpeg";
            image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        }
        else
        {
            context.Response.ContentType = "text/html";
            context.Response.Write("<p>Need a valid id</p>");
        }
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

    private Image GetImage(int ID)
    {
        MemoryStream memoryStream = new MemoryStream();
        //Retrieve image from Database to a memeory stream.
        // If you are using a different method, use it and assign the data to the 'memoryStream' variable.
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT imageData FROM ImagesTbl WHERE ID=" + ID, sqlConnection))
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            byte[] btImage = (byte[])sqlDataReader["imageData"];
                            memoryStream = new MemoryStream(btImage, false);
                        }
                    }
                }
            }
            sqlConnection.Close();
        }
        Image image = Image.FromStream(memoryStream);
        return image;
    }
}
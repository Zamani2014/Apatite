using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class EasyControls_Scrolling_News : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection myCon = new SqlConnection(ConnectionString);
        string strSql = "SELECT * FROM BuildingTbl, OwnerTbl WHERE BuildingTbl.OwnerID = OwnerTbl.ID ORDER BY BuildingTbl.DateTime ASC";
        string strScrolling = "";
        HtmlTableCell cellScrolling = new HtmlTableCell();
        SqlCommand myComd = new SqlCommand(strSql, myCon);
        SqlDataReader sqlRdr;
        try
        {
            myCon.Open();
            sqlRdr = myComd.ExecuteReader();
            strScrolling = "<Marquee OnMouseOver='this.stop();' OnMouseOut='this.start();' direction='up' scrollamount='2' width='100%'>";
            while (sqlRdr.Read())
            {
                strScrolling = strScrolling + "<a style='color:#000000; text-decoration:none' href='#' OnClick=" + "javascript:window.open('Register/ShowDetails.aspx?BuildingID=" + sqlRdr.GetValue(0) + "','NewsDetail','width=960,height=700;toolbar=yes;');" + ">" + sqlRdr.GetValue(32) + ", " + sqlRdr.GetValue(2) + " " + "متری, " + "در " + sqlRdr.GetValue(30) + ", " + sqlRdr.GetValue(29) + ", " + sqlRdr.GetValue(28) + " (" + sqlRdr.GetValue(31) + ")" + "</a><br><br>";
            }
            strScrolling = strScrolling + "</Marquee>";
            sqlRdr.Close(); 
            cellScrolling.InnerHtml = strScrolling;
            rowScrolling.Cells.Add(cellScrolling);
        }
        catch (Exception msg)
        {
            Response.Write(msg.Message);
        }
        finally
        {
            //close sql connection   
            myCon.Close();
        }
    }
}
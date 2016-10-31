using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class ScrollingRequests : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection myCon = new SqlConnection(ConnectionString);
        string strSql = "SELECT * FROM RequestTbl ORDER BY DateTime ASC";
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
                strScrolling = strScrolling + "<a style='color:#000000; text-decoration:none' href='#' OnClick=" + "javascript:window.open('Request/Search.aspx?ID=" + sqlRdr.GetValue(0) + "','NewsDetail','width=800,height=600;toolbar=yes;');" + ">" + sqlRdr.GetValue(9) + ", " + sqlRdr.GetValue(12) + " " + "در " + sqlRdr.GetValue(8) + ", " + sqlRdr.GetValue(7) + ", " + sqlRdr.GetValue(6) + " (" + sqlRdr.GetValue(11) + ")" + "</a><br><br>";
            }
            strScrolling = strScrolling + "</Marquee>";
            sqlRdr.Close(); 
            cellScrolling.InnerHtml = strScrolling;
            rowScrolling.Cells.Add(cellScrolling);
        }
        catch (Exception msg)
        {
            Response.Write(msg.Message + msg.StackTrace);
        }
        finally
        {
            //close sql connection   
            myCon.Close();
        }
    }
}
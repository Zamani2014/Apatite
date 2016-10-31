using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Net;
using System.Data;

public partial class Request_Request : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
    }
    protected void submitBtn_Click(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection objconnection = new SqlConnection(ConnectionString);
        objconnection.Open();

        try
        {
            int ID = GenerateIDColumn.GetNewID("RequestTbl");
            string Name = this.TextBox2.Text;
            string EMail = this.TextBox1.Text;
            string Tel = this.TextBox3.Text;
            string Mobile = this.TextBox4.Text;
            string Province = this.DropDownList6.SelectedItem.Text;
            string State = this.TextBox6.Text;
            string City = this.TextBox7.Text;
            string Region = this.TextBox8.Text; ;
            string BuildingType = this.DropDownList1.SelectedItem.Text;
            string DocType = this.DropDownList2.SelectedItem.Text;
            string TransactionType = this.DropDownList3.SelectedItem.Text;
            string Range = this.DropDownList4.SelectedItem.Text;
            string Price = this.DropDownList5.SelectedItem.Text;
            string Comments = this.TextBox9.Text;
            string DateTime = GetDateTime.GenerateDateTime();

            //string query1 = "SELECT * FROM RequestTbl WHERE Name='" + Name + "'" + ", EMail='" + EMail + "'" + ", Tel='" + Tel + "'" + ", Mobile='" + Mobile + "'" + ", Province='" + Province + "'" + ", State='" + State + "'" + ", City='" + City + "'" + ", Region='" + Region + "'" + ", BuildingType='" + BuildingType + "'" + ", DocType='" + DocType + "'" + ", TransactionType='" + TransactionType + "'" + ", Range='" + Range + "'" + ", Price='" + Price +"'";
            //SqlCommand cmd0 = new SqlCommand(query1, objconnection);
            //SqlDataReader DR = cmd0.ExecuteReader();
            //if (DR.Read())
            //{
            //    WebMsgBox.WebMsgBox.Show("شما یکبار این تقاضا را ثبت کرده اید !");
            //}
            //else
            //{
                string myQuery = "INSERT INTO RequestTbl (ID, Name, EMail, Tel, Mobile, Province, State, City, Region, BuildingType, DocType, TransactionType, Range, Price, Comments, DateTime) values (@ID, @Name, @EMail, @Tel, @Mobile, @Province, @State, @City, @Region, @BuildingType, @DocType, @TransactionType, @Range, @Price, @Comments, @DateTime)";
                SqlCommand cmd = new SqlCommand(myQuery, objconnection);

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@EMail", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Tel", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Province", SqlDbType.NVarChar);
                cmd.Parameters.Add("@State", SqlDbType.NVarChar);
                cmd.Parameters.Add("@City", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Region", SqlDbType.NVarChar);
                cmd.Parameters.Add("@BuildingType", SqlDbType.NVarChar);
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar);
                cmd.Parameters.Add("@TransactionType", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Range", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Price", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Comments", SqlDbType.NText);
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);

                cmd.Parameters["@ID"].Value = ID;
                cmd.Parameters["@Name"].Value = Name;
                cmd.Parameters["@EMail"].Value = EMail;
                cmd.Parameters["@Tel"].Value = Tel;
                cmd.Parameters["@Mobile"].Value = Mobile;
                cmd.Parameters["@Province"].Value = Province;
                cmd.Parameters["@State"].Value = State;
                cmd.Parameters["@City"].Value = City;
                cmd.Parameters["@Region"].Value = Region;
                cmd.Parameters["@BuildingType"].Value = BuildingType;
                cmd.Parameters["@DocType"].Value = DocType;
                cmd.Parameters["@TransactionType"].Value = TransactionType;
                cmd.Parameters["@Range"].Value = Range;
                cmd.Parameters["@Price"].Value = Price;
                cmd.Parameters["@Comments"].Value = Comments;
                cmd.Parameters["@DateTime"].Value = DateTime;

                cmd.ExecuteNonQuery();
                WebMsgBox.WebMsgBox.Show("اطلاعات تقاضای ملکی شما با موفقیت ثبت شد، کد تقاضای شما برابر است با :" + ID);

                //string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string ClientIP = Request.UserHostAddress;
                //IPHostEntry HostInfo = Dns.GetHostEntry(ClientIP);
                //string ClientHost = HostInfo.HostName.ToString();
                string ClientHost = Request.UserHostName;
                string browser = "Not Detected";
                string mypage = Request.ServerVariables["HTTP_REFERER"];
                // Getting Browser Name of Visitor
                if (Request.ServerVariables["HTTP_USER_AGENT"].Contains("MSIE"))
                {
                    browser = "Internet Explorer";
                }
                if (Request.ServerVariables["HTTP_USER_AGENT"].Contains("FireFox"))
                {
                    browser = "Mozilla FireFox";
                }
                if (Request.ServerVariables["HTTP_USER_AGENT"].Contains("Mozilla"))
                {
                    browser = "Mozilla FireFox";
                }
                if (Request.ServerVariables["HTTP_USER_AGENT"].Contains("Opera"))
                {
                    browser = "Opera";
                }
                if (Request.ServerVariables["HTTP_USER_AGENT"].Contains("Chrome"))
                {
                    browser = "Google Chrome";
                }

                PersianCalendar jc = new PersianCalendar();
                DateTime thisDate = System.DateTime.Now;

                string p_year = jc.GetYear(thisDate).ToString();
                string p_month = jc.GetMonth(thisDate).ToString();
                string p_day = jc.GetDayOfMonth(thisDate).ToString();

                string mytime = jc.GetHour(thisDate).ToString() + ":" + jc.GetMinute(thisDate).ToString() + ":" + jc.GetSecond(thisDate).ToString() + ":" + jc.GetMilliseconds(thisDate).ToString();
                string mydate = p_year + "/" + p_month + "/" + p_day;
                string mybrowser = browser;

                string strsql1 = "INSERT INTO RequestTblUsers (ID, CompName, CompIP, CompDate, CompTime, CompBrowser, Referer) values (@ID, @name, @ip, @dat, @tim, @browser, @referer);";
                SqlCommand objcommand = new SqlCommand(strsql1, objconnection);

                objcommand.Parameters.Add("@ID", SqlDbType.Int);
                objcommand.Parameters.Add("@name", SqlDbType.NVarChar);
                objcommand.Parameters.Add("@ip", SqlDbType.NVarChar);
                objcommand.Parameters.Add("@dat", SqlDbType.NVarChar);
                objcommand.Parameters.Add("@tim", SqlDbType.NVarChar);
                objcommand.Parameters.Add("@browser", SqlDbType.NVarChar);
                objcommand.Parameters.Add("@referer", SqlDbType.NVarChar);

                objcommand.Parameters["@ID"].Value = ID;
                objcommand.Parameters["@name"].Value = ClientHost;
                objcommand.Parameters["@ip"].Value = ClientIP;
                objcommand.Parameters["@dat"].Value = mydate;
                objcommand.Parameters["@tim"].Value = mytime;
                objcommand.Parameters["@browser"].Value = mybrowser;
                objcommand.Parameters["@referer"].Value = string.Empty;
                //objcommand.Parameters["@referer"].Value = mypage;

                objcommand.ExecuteNonQuery();

                objconnection.Close();
            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + ex.StackTrace);
            WebMsgBox.WebMsgBox.Show("متاسفانه خطایی رخ داده است :" + ex.Message + ex.StackTrace);
        }
    }
    protected void CAPTCHARefresh_Click(object sender, EventArgs e)
    {
        this.imgAntiBotImage.ImageUrl = "~/antibotimage.ashx?random=" + DateTime.Now.Ticks.ToString();
    }

}
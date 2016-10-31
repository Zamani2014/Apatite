using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using Artem.Google.UI;
using System.Globalization;
using WebMsgBox;
using System.Net;
using GoogleMap;
using System.Web.Services;

public partial class Register_Register : PageBaseClass
{
    #region Variables 
    protected string Message;
    protected byte[] binaryImagedata;
    protected HttpPostedFile selectedFile;
    protected static int BuildingID;
    protected static int OwnerID;
    protected static double Latitude = 0;
    protected static double Longitude = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Message = "";

            //Main UpdatePanel
            UpdatePanel1.Visible = true;
            // Other UpdatePanel
            UpdatePanel2.Visible = true;
            UpdatePanel3.Visible = false;
            UpdatePanel4.Visible = false;
            WebMsgBox.WebMsgBox.Show("در هر مرحله از تکمیل فرم ثبت ملک از کلیک بر روی دکمه برگشت صفحه خودداری نمائید !");
        }
    }
    protected void step1Btn_Click(object sender, EventArgs e)
    {
        try
        {
            int ID = GenerateIDColumn.GetNewID("OwnerTbl");
            OwnerID = ID;
            string Country = this.DropDownList5.SelectedValue;
            string Province = this.DropDownList1.SelectedValue;
            string City = this.TextBox2.Text;
            string Region = this.TextBox3.Text;
            string TransactionType = this.DropDownList2.SelectedValue;
            string BuildingType = this.DropDownList3.SelectedValue;
            string DocType = this.DropDownList4.SelectedValue;
            string OwnerName = this.TextBox4.Text;
            string EMail = this.TextBox5.Text;
            string Address = this.TextBox6.Text;
            int No = int.Parse(this.TextBox7.Text);
            string Tel1 = this.TextBox8.Text;
            string Tel2 = this.TextBox9.Text;
            string Mobile = this.TextBox10.Text;
            string Comments = "توضیحات";
            string DateTime = GetDateTime.GenerateDateTime();

            string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = "INSERT INTO OwnerTbl (ID, Country, Province, City, Region, TransactionType, BuildingType, DocType, OwnerName, EMail, Address, No, Tel1, Tel2, Mobile, Comments, DateTime) values (@ID, @Country, @Province, @City, @Region, @TransactionType, @BuildingType, @DocType, @OwnerName, @EMail, @Address, @No, @Tel1, @Tel2, @Mobile, @Comments, @DateTime)";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Province", SqlDbType.NVarChar);
            cmd.Parameters.Add("@City", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Region", SqlDbType.NVarChar);
            cmd.Parameters.Add("@TransactionType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BuildingType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OwnerName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EMail", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
            cmd.Parameters.Add("@No", SqlDbType.Int);
            cmd.Parameters.Add("@Tel1", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Tel2", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);

            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@Country"].Value = Country;
            cmd.Parameters["@Province"].Value = Province;
            cmd.Parameters["@City"].Value = City;
            cmd.Parameters["@Region"].Value = Region;
            cmd.Parameters["@TransactionType"].Value = TransactionType;
            cmd.Parameters["@BuildingType"].Value = BuildingType;
            cmd.Parameters["@DocType"].Value = DocType;
            cmd.Parameters["@OwnerName"].Value = OwnerName;
            cmd.Parameters["@EMail"].Value = EMail;
            cmd.Parameters["@Address"].Value = Address;
            cmd.Parameters["@No"].Value = No;
            cmd.Parameters["@Tel1"].Value = Tel1;
            cmd.Parameters["@Tel2"].Value = Tel2;
            cmd.Parameters["@Mobile"].Value = Mobile;
            cmd.Parameters["@Comments"].Value = Comments;
            cmd.Parameters["@DateTime"].Value = DateTime;

            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            UpdatePanel2.Visible = false;
            UpdatePanel3.Visible = true;
        }
        catch (Exception ex)
        {
            //WebMsgBox.WebMsgBox.Show("خطایی رخ داده است :" + ex.Message);
            Response.Write(ex.Message + ex.StackTrace);
        }
    }
    protected void step2Btn_Click(object sender, EventArgs e)
    {
        try
        {
            #region DB Vars
            int ID = GenerateIDColumn.GetNewID("BuildingTbl");
            BuildingID = ID;
            this.BuildingIDLbl.Text = BuildingID.ToString();
            string Range = TextBox15.Text;
            string Area = TextBox16.Text;
            int RoomNo = int.Parse(this.DropDownList10.SelectedValue);
            int ClassNo = int.Parse(TextBox17.Text);
            int ClassTot = int.Parse(TextBox17.Text);
            int UnitsInClass = int.Parse(TextBox19.Text);
            int UnitsTot = (TextBox20.Text != "") ? int.Parse(TextBox20.Text) : 0;
            string BuildingView = this.DropDownList11.SelectedValue;
            string ResidentType = DropDownList12.SelectedValue;
            string YoursOld = "";

            if (RadioButton1.Checked)
            {
                YoursOld += RadioButton1.Text;
            }
            else if (RadioButton2.Checked)
            {
                YoursOld += RadioButton2.Text;
            }

            string Old = this.TextBox1.Text;

            if (this.CheckBox5.Checked)
            {
                Old = Old + " - " + CheckBox5.Text;
            }

            string GeoPosition = "";
            if (CheckBox6.Checked)
            {
                GeoPosition += CheckBox6.Text;
            }
            if (CheckBox7.Checked)
            {
                GeoPosition += CheckBox7.Text;
            }
            if (CheckBox8.Checked)
            {
                GeoPosition += CheckBox8.Text;
            }
            if (CheckBox9.Checked)
            {
                GeoPosition += CheckBox9.Text;
            }
            string Comments = TextBox21.Text;
            string Cabinet = this.DropDownList6.SelectedValue;
            string Sanitary = this.DropDownList7.SelectedValue;
            string Floor = this.DropDownList8.SelectedValue;
            int Price = int.Parse(TextBox11.Text);
            int TotPrice = int.Parse(TextBox12.Text);
            string Currency = this.DropDownList9.SelectedValue;
            int Parking = (TextBox13.Text != "") ? int.Parse(TextBox13.Text) : 0;
            int TelsNo = (TextBox14.Text != "") ? int.Parse(TextBox14.Text) : 0;
            string Other = "";

            if (this.CheckBox1.Checked)
            {
                Other += " - " + CheckBox1.Text;
            }
            if (this.CheckBox2.Checked)
            {
                Other += " - " + CheckBox2.Text;
            }
            if (this.CheckBox3.Checked)
            {
                Other += " - " + CheckBox3.Text;
            }
            if (CheckBox4.Checked)
            {
                Other += " - " + CheckBox4.Text;
            }

            string Facilities = "";

            if (this.CheckBox10.Checked)
            {
                Facilities += " - " + CheckBox10.Text;
            }
            if (this.CheckBox11.Checked)
            {
                Facilities += " - " + CheckBox11.Text;
            }
            if (this.CheckBox12.Checked)
            {
                Facilities += " - " + CheckBox12.Text;
            }
            if (CheckBox13.Checked)
            {
                Facilities += " - " + CheckBox13.Text;
            }
            if (this.CheckBox14.Checked)
            {
                Facilities += " - " + CheckBox14.Text;
            }
            if (this.CheckBox15.Checked)
            {
                Facilities += " - " + CheckBox15.Text;
            }
            if (this.CheckBox16.Checked)
            {
                Facilities += " - " + CheckBox16.Text;
            }
            if (CheckBox17.Checked)
            {
                Facilities += " - " + CheckBox17.Text;
            }
            if (this.CheckBox18.Checked)
            {
                Facilities += " - " + CheckBox18.Text;
            }
            if (this.CheckBox19.Checked)
            {
                Facilities += " - " + CheckBox19.Text;
            }
            if (this.CheckBox20.Checked)
            {
                Facilities += " - " + CheckBox20.Text;
            }
            if (CheckBox21.Checked)
            {
                Facilities += " - " + CheckBox21.Text;
            }
            if (this.CheckBox22.Checked)
            {
                Facilities += " - " + CheckBox22.Text;
            }
            if (this.CheckBox23.Checked)
            {
                Facilities += " - " + CheckBox23.Text;
            }
            if (this.CheckBox24.Checked)
            {
                Facilities += " - " + CheckBox24.Text;
            }
            if (CheckBox25.Checked)
            {
                Facilities += " - " + CheckBox25.Text;
            }
            if (this.CheckBox26.Checked)
            {
                Facilities += " - " + CheckBox26.Text;
            }
            if (this.CheckBox27.Checked)
            {
                Facilities += " - " + CheckBox27.Text;
            }
            if (this.CheckBox28.Checked)
            {
                Facilities += " - " + CheckBox28.Text;
            }
            if (CheckBox29.Checked)
            {
                Facilities += " - " + CheckBox29.Text;
            }
            if (this.CheckBox30.Checked)
            {
                Facilities += " - " + CheckBox30.Text;
            }
            if (this.CheckBox31.Checked)
            {
                Facilities += " - " + CheckBox31.Text;
            }
            if (this.CheckBox32.Checked)
            {
                Facilities += " - " + CheckBox32.Text;
            }
            if (CheckBox33.Checked)
            {
                Facilities += " - " + CheckBox33.Text;
            }
            if (CheckBox34.Checked)
            {
                Facilities += " - " + CheckBox34.Text;
            }
            string DateTime = GetDateTime.GenerateDateTime();
            #endregion

            string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = "INSERT INTO BuildingTbl (ID, OwnerID, Range, Area, RoomNo, ClassNo, ClassTot, UnitsInClass, UnitsTot, BuildingView, ResidentType, YoursOld, Old, GeoPosition, Comments, Cabinet, Sanitary, Floor, Price, TotPrice, Currency, Parking, TelsNo, Other, Facilities, DateTime) values (@ID, @OwnerID, @Range, @Area, @RoomNo, @ClassNo, @ClassTot, @UnitsInClass, @UnitsTot, @BuildingView, @ResidentType, @YoursOld, @Old, @GeoPosition, @Comments, @Cabinet, @Sanitary, @Floor, @Price, @TotPrice, @Currency, @Parking, @TelsNo, @Other, @Facilities, @DateTime)";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@OwnerID", SqlDbType.Int);
            cmd.Parameters.Add("@Range", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Area", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RoomNo", SqlDbType.Int);
            cmd.Parameters.Add("@ClassNo", SqlDbType.Int);
            cmd.Parameters.Add("@ClassTot", SqlDbType.Int);
            cmd.Parameters.Add("@UnitsInClass", SqlDbType.Int);
            cmd.Parameters.Add("@UnitsTot", SqlDbType.Int);
            cmd.Parameters.Add("@BuildingView", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ResidentType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@YoursOld", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Old", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GeoPosition", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Cabinet", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Sanitary", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Floor", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Price", SqlDbType.Int);
            cmd.Parameters.Add("@TotPrice", SqlDbType.Int);
            cmd.Parameters.Add("@Currency", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Parking", SqlDbType.Int);
            cmd.Parameters.Add("@TelsNo", SqlDbType.Int);
            cmd.Parameters.Add("@Other", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Facilities", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);

            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@OwnerID"].Value = OwnerID;
            cmd.Parameters["@Range"].Value = Range;
            cmd.Parameters["@Area"].Value = Area;
            cmd.Parameters["@RoomNo"].Value = RoomNo;
            cmd.Parameters["@ClassNo"].Value = ClassNo;
            cmd.Parameters["@ClassTot"].Value = ClassTot;
            cmd.Parameters["@UnitsInClass"].Value = UnitsInClass;
            cmd.Parameters["@UnitsTot"].Value = UnitsTot;
            cmd.Parameters["@BuildingView"].Value = BuildingView;
            cmd.Parameters["@ResidentType"].Value = ResidentType;
            cmd.Parameters["@YoursOld"].Value = YoursOld;
            cmd.Parameters["@Old"].Value = Old;
            cmd.Parameters["@GeoPosition"].Value = GeoPosition;
            cmd.Parameters["@Comments"].Value = Comments;
            cmd.Parameters["@Cabinet"].Value = Cabinet;
            cmd.Parameters["@Sanitary"].Value = Sanitary;
            cmd.Parameters["@Floor"].Value = Floor;
            cmd.Parameters["@Price"].Value = Price;
            cmd.Parameters["@TotPrice"].Value = TotPrice;
            cmd.Parameters["@Currency"].Value = Currency;
            cmd.Parameters["@Parking"].Value = Parking;
            cmd.Parameters["@TelsNo"].Value = TelsNo;
            cmd.Parameters["@Other"].Value = Other;
            cmd.Parameters["@Facilities"].Value = Facilities;
            cmd.Parameters["@DateTime"].Value = DateTime;

            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            #region Insert User Info

            sqlConnection.Open();

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

            string strsql1 = "INSERT INTO RegisterTblUsers (ID, CompName, CompIP, CompDate, CompTime, CompBrowser, Referer) values (@ID, @name, @ip, @dat, @tim, @browser, @referer);";
            SqlCommand objcommand = new SqlCommand(strsql1, sqlConnection);

            objcommand.Parameters.Add("@ID", SqlDbType.Int);
            objcommand.Parameters.Add("@name", SqlDbType.NVarChar);
            objcommand.Parameters.Add("@ip", SqlDbType.NVarChar);
            objcommand.Parameters.Add("@dat", SqlDbType.NVarChar);
            objcommand.Parameters.Add("@tim", SqlDbType.NVarChar);
            objcommand.Parameters.Add("@browser", SqlDbType.NVarChar);
            objcommand.Parameters.Add("@referer", SqlDbType.NVarChar);

            objcommand.Parameters["@ID"].Value = OwnerID;
            objcommand.Parameters["@name"].Value = ClientHost;
            objcommand.Parameters["@ip"].Value = ClientIP;
            objcommand.Parameters["@dat"].Value = mydate;
            objcommand.Parameters["@tim"].Value = mytime;
            objcommand.Parameters["@browser"].Value = mybrowser;
            objcommand.Parameters["@referer"].Value = string.Empty;
            //objcommand.Parameters["@referer"].Value = mypage;

            objcommand.ExecuteNonQuery();

            sqlConnection.Close();
            #endregion

            UpdatePanel3.Visible = false;
            UpdatePanel4.Visible = true;


        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("خطایی رخ داده است :" + ex.Message + ex.StackTrace);
            Response.Write(ex.Message + ex.StackTrace);
        }
    }
    protected void CAPTCHARefresh_Click(object sender, EventArgs e)
    {
        this.imgAntiBotImage.ImageUrl = "~/antibotimage.ashx?random=" + DateTime.Now.Ticks.ToString();
    }
    protected void RadioBtn2_CheckedChanged(object sender, EventArgs e)
    {
        this.TextBox1.Enabled = true;
        this.CheckBox5.Enabled = true;
    }
    protected void RadioBtn1_CheckedChanged(object sender, EventArgs e)
    {
        this.TextBox1.Enabled = false;
        this.CheckBox5.Enabled = false;
        this.CheckBox5.Checked = false;
    }
    protected void UploadBtn_Click(object sender, EventArgs e)
    {
        BinaryReadImage();
        UploadToDB();
        DisplayMessage();
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
                //this.Image1.ImageUrl = "~/GetImageFromDB.ashx?ID=" + dr["Image1"];
                //int[] Images = new int[10];

                //for (int i = 1; i < 10; i++)
                //{
                //    Images[i] = int.Parse(dr["Image" + i].ToString());
                //    WebMsgBox.WebMsgBox.Show(Images[i].ToString());
                //}

                Control ctrl = new Control();
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                for (int i = 1; i < 10; i++)
                {
                    ctrl = this.UpdatePanel4.FindControl("Image" + i.ToString());
                    img = (System.Web.UI.WebControls.Image)ctrl;
                    img.ImageUrl = "~/GetImageFromDB.ashx?ID=" + dr["Image" + i].ToString();
                    img.DataBind();
                }
            }
            else
            {
                WebMsgBox.WebMsgBox.Show("تصویری برای نمایش وجود ندارد !");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show(ex.Message + ex.StackTrace + ex.InnerException);
            Response.Write(ex.Message + ex.StackTrace);
        }
    }
    protected void DisplayMessage()
    {
        if (Message == "")
        {
            Message = "تصویر مورد نظر با موفقیت بالاگذاری شد .";
        }
        Label40.Text = Message;
    }
    protected void UploadToDB()
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();
        string query = "SELECT BuildingID FROM ImagesTbl WHERE BuildingID=" + BuildingID;
        SqlCommand cmd0 = new SqlCommand(query, sqlConnection);
        SqlDataReader dr = cmd0.ExecuteReader();

        int totalRows = 0;
        while (dr.Read())
        {
            totalRows += 1;
        }

        if (totalRows < 9)
        {
            if (Message == "")
            {
                try
                {
                    string myquery = "INSERT INTO ImagesTbl (ID, BuildingID, imageData, imageName, imageType) values (@ID, @BuildingID, @imageData, @imageName, @imageType)";
                    int ID = GenerateIDColumn.GetNewID("ImagesTbl");

                    SqlCommand cmd = new SqlCommand(myquery, sqlConnection);

                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters.Add("@BuildingID", SqlDbType.Int);
                    cmd.Parameters.Add("@imageData", SqlDbType.Image);
                    cmd.Parameters.Add("@imageName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@imageType", SqlDbType.NVarChar);

                    cmd.Parameters["@ID"].Value = ID;
                    cmd.Parameters["@BuildingID"].Value = BuildingID;
                    cmd.Parameters["@imageData"].Value = binaryImagedata;
                    cmd.Parameters["@imageName"].Value = FileUpload1.PostedFile.FileName.ToString();
                    cmd.Parameters["@imageType"].Value = selectedFile.ContentType.ToString();

                    //cmd.CommandType = CommandType.StoredProcedure;
                    //SqlCommandBuilder.DeriveParameters(cmd);
                    //cmd.Parameters[1].Value = binaryImagedata;
                    //cmd.Parameters[2].Value = FileUpload1.PostedFile.FileName.ToString();
                    //cmd.Parameters[3].Value = selectedFile.ContentType.ToString();
                    dr.Close();
                    cmd.ExecuteNonQuery();

                    if (totalRows == 0)
                    {
                        int NewID = GenerateIDColumn.GetNewID("BuildingImages");
                        int Image1 = ID;

                        string query1 = "INSERT INTO BuildingImages(ID, BuildingID, Image1) values (@ID, @BuildingID, @Image1)";
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@ID", SqlDbType.Int);
                        cmd1.Parameters.Add("@BuildingID", SqlDbType.Int);
                        cmd1.Parameters.Add("@Image1", SqlDbType.Int);

                        cmd1.Parameters["@ID"].Value = NewID;
                        cmd1.Parameters["@BuildingID"].Value = BuildingID;
                        cmd1.Parameters["@Image1"].Value = Image1;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 1)
                    {
                        int Image2 = ID;

                        string query1 = "UPDATE BuildingImages SET Image2=@Image2 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image2", SqlDbType.Int);

                        cmd1.Parameters["@Image2"].Value = Image2;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 2)
                    {
                        int Image3 = ID;

                        string query1 = "UPDATE BuildingImages SET Image3=@Image3 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image3", SqlDbType.Int);

                        cmd1.Parameters["@Image3"].Value = Image3;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 3)
                    {
                        int Image4 = ID;

                        string query1 = "UPDATE BuildingImages SET Image4=@Image4 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image4", SqlDbType.Int);

                        cmd1.Parameters["@Image4"].Value = Image4;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 4)
                    {
                        int Image5 = ID;

                        string query1 = "UPDATE BuildingImages SET Image5=@Image5 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image5", SqlDbType.Int);

                        cmd1.Parameters["@Image5"].Value = Image5;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 5)
                    {
                        int Image6 = ID;

                        string query1 = "UPDATE BuildingImages SET Image6=@Image6 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image6", SqlDbType.Int);

                        cmd1.Parameters["@Image6"].Value = Image6;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 6)
                    {
                        int Image7 = ID;

                        string query1 = "UPDATE BuildingImages SET Image7=@Image7 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image7", SqlDbType.Int);

                        cmd1.Parameters["@Image7"].Value = Image7;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 7)
                    {
                        int Image8 = ID;

                        string query1 = "UPDATE BuildingImages SET Image8=@Image8 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image8", SqlDbType.Int);

                        cmd1.Parameters["@Image8"].Value = Image8;
                        cmd1.ExecuteNonQuery();
                    }
                    else if (totalRows == 8)
                    {
                        int Image9 = ID;

                        string query1 = "UPDATE BuildingImages SET Image9=@Image9 WHERE BuildingID=" + BuildingID;
                        SqlCommand cmd1 = new SqlCommand(query1, sqlConnection);

                        cmd1.Parameters.Add("@Image9", SqlDbType.Int);

                        cmd1.Parameters["@Image9"].Value = Image9;
                        cmd1.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Message += "ثبت تصاویر در بانک اطلاعاتی با شکست مواجه شد !" + ex.Message + ex.StackTrace;
                }
            }
        }
        else
        {
            WebMsgBox.WebMsgBox.Show("شما امکان ارسال بیشتر از نه تصویر را برای فایل مسکن خود ندارید !");
        }
    }
    protected void BinaryReadImage()
    {
        try
        {
            if (this.FileUpload1.HasFile)
            {
                selectedFile = FileUpload1.PostedFile;
                binaryImagedata = new byte[selectedFile.ContentLength];
                selectedFile.InputStream.Read(binaryImagedata, 0, selectedFile.ContentLength);
                Message += validation(selectedFile);
            }
            else
            {
                WebMsgBox.WebMsgBox.Show("لطفا فایلی را برای بالاگذاری انتخاب نمائید !");
            }
        }
        catch (Exception ex)
        {
            Message += "خواندن دودویی داده های تصویر با شکست مواجه شد !";
        }
    }
    protected string validation(HttpPostedFile selectedFile)
    {
        string errs = "";

        if (selectedFile.ContentLength == 0)
        {
            errs += "شما هیچ فایلی را برای بالاگذاری انتخاب نکرده اید !";
        }

        if (selectedFile.ContentType.ToLower() != @"image/gif" && selectedFile.ContentType.ToLower() != @"image/jpeg" && selectedFile.ContentType.ToLower() != @"image/pjpeg" && selectedFile.ContentType.ToLower() != @"image/png")
        {
            errs += "نوع فایل انتخابی نامعتبر است !";
        }

        System.Drawing.Image drawingImage = null;
        drawingImage = System.Drawing.Image.FromStream(new System.IO.MemoryStream(binaryImagedata));

        if ((drawingImage.Height > 600) && (drawingImage.Width > 800))
        {
            errs += "حجم تصویر انتخابی بزرگ است !";
        }

        if (selectedFile.ContentLength > 150000)
        {
            errs += "اندازه فایل انتخابی نامعتبر است !";
        }
        return errs;
    }
    protected void ImagesReset_Click(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["DarvazehConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();

        string query = "DELETE FROM ImagesTbl WHERE BuildingID=" + BuildingID;
        SqlCommand cmd0 = new SqlCommand(query, sqlConnection);

        string query2 = "DELETE FROM BuildingImages WHERE BuildingID=" + BuildingID;
        SqlCommand cmd1 = new SqlCommand(query2, sqlConnection);

        cmd0.ExecuteNonQuery();
        cmd1.ExecuteNonQuery();

        LoadImages();
    }
}
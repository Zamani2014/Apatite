using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using AjaxControlToolkit;

public partial class EasyControls_HouseTabs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        try
        {
            string Province = this.DropDownList1.SelectedValue;
            string City = (this.TextBox2.Text != "") ? this.TextBox2.Text : null;
            string Region = (this.TextBox3.Text != "") ? this.TextBox3.Text : null;
            string BuildingType = this.DropDownList2.SelectedValue;
            int LowPrice = (TextBox4.Text != "") ? int.Parse(TextBox4.Text) : 0;
            int HighPrice = (TextBox5.Text != "") ? int.Parse(TextBox5.Text) : int.MaxValue;
            int Range = (TextBox10.Text != "") ? int.Parse(this.TextBox10.Text) : 0;

            Page.Response.Redirect("~/Register/RegisterList.aspx?Province=" + Province + "&City=" + City + "&Region=" + Region + "&BuildingType=" + BuildingType + "&LowPrice=" + LowPrice + "&HighPrice=" + HighPrice + "&Range=" + Range + "&TransactionType=1");
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("متاسفانه خطایی رخ داده است ، لطفا در وارد کردن اطلاعات دقت فرمائید !");
            Response.Write(ex.StackTrace);
        }
    }
    protected void SearchBtn2_Click(object sender, EventArgs e)
    {
        try
        {
            string Province = this.DropDownList4.SelectedValue;
            string City = (this.TextBox6.Text != "") ? this.TextBox6.Text : null;
            string Region = (this.TextBox7.Text != "") ? this.TextBox7.Text : null;
            string BuildingType = this.DropDownList5.SelectedValue;
            int LowPrice = (TextBox8.Text != "") ? int.Parse(TextBox8.Text) : 0;
            int HighPrice = (TextBox9.Text != "") ? int.Parse(TextBox9.Text) : int.MaxValue;
            int Range = (TextBox11.Text != "") ? int.Parse(this.TextBox11.Text) : 0;

            Page.Response.Redirect("~/Register/RegisterList.aspx?Province=" + Province + "&City=" + City + "&Region=" + Region + "&BuildingType=" + BuildingType + "&LowPrice=" + LowPrice + "&HighPrice=" + HighPrice + "&Range=" + Range + "&TransactionType=2");
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("متاسفانه خطایی رخ داده است ، لطفا در وارد کردن اطلاعات دقت فرمائید !");
            Response.Write(ex.StackTrace);
        }
    }
    protected void SearchBtn3_Click(object sender, EventArgs e)
    {
        try
        {

            if (this.TextBox1.Text == "")
            {
                WebMsgBox.WebMsgBox.Show("لطفا یک شناسه ملک وارد نمائید !");
            }
            else
            {
                int BuildingID = int.Parse(this.TextBox1.Text);
                Page.Response.Redirect("~/Register/ShowDetails.aspx?BuildingID=" + BuildingID);
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("متاسفانه خطایی رخ داده است ، لطفا در وارد کردن اطلاعات دقت فرمائید !");
        }
    }
}
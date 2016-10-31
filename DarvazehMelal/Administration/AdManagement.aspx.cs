using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using System.Xml;
using System.Data;

public partial class Administration_AdManagement : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet DS = new DataSet();
        DS.ReadXml(Server.MapPath("~/xml/Ads.xml"));
        if (!IsPostBack)
        {
            if (DS.Tables.Count != 0)
            {
                this.GridView1.DataBind();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.FileUpload1.HasFile)
            {
                this.FileUpload1.SaveAs(Server.MapPath("~/Images/Ads/" + this.FileUpload1.FileName));
                string Subject = this.TextBox1.Text;
                //string FilePath = "http://www.AmlakeMelal.ir/" + "Images/Ads/" + this.FileUpload1.FileName;
                string FilePath = Server.MapPath("Images/Ads/" + this.FileUpload1.FileName);
                string Link = this.TextBox2.Text;
                string Comments = this.TextBox3.Text;
                Guid ID = Guid.NewGuid();

                XmlDocument doc = new XmlDocument();

                doc.Load(Server.MapPath("~/xml/Ads.xml"));

                XmlElement root = doc.CreateElement("ads");
                //root.SetAttribute("id", txtid.Text);
                XmlElement id = doc.CreateElement("id");
                XmlElement url = doc.CreateElement("url");
                XmlElement target = doc.CreateElement("target");
                XmlElement alt = doc.CreateElement("alt");
                XmlElement imageURL = doc.CreateElement("imageURL");
                XmlElement comments = doc.CreateElement("comments");

                id.InnerText = ID.ToString();
                //url.InnerText = Link;
                target.InnerText = "_new";
                alt.InnerText = Subject;
                imageURL.InnerText = FilePath;
                comments.InnerText = Comments;

                XmlCDataSection cdata = doc.CreateCDataSection("<url>");
                cdata.InnerText = Link;

                root.AppendChild(id);
                root.AppendChild(url).AppendChild(cdata);
                root.AppendChild(target);
                root.AppendChild(alt);
                root.AppendChild(imageURL);
                root.AppendChild(comments);

                doc.DocumentElement.AppendChild(root);
                doc.Save(Server.MapPath("~/xml/Ads.xml"));

                this.GridView1.DataBind();

            }
            else
            {
                WebMsgBox.WebMsgBox.Show("لطفا فایلی را برای بالاگذاری انتخاب نمائید !");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("متاسفانه خطایی رخ داده است !" + ex.Message);
        }
    }
    public void GridView1_OnDataBinding(object sender, EventArgs e)
    {
        DataSet DS = new DataSet();
        DS.ReadXml(Server.MapPath("~/xml/Ads.xml"));
        this.GridView1.DataSource = DS;
    }
    public void GridView1_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string adID = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            XmlDocument doc2 = new XmlDocument();
            doc2.Load(Server.MapPath("~/xml/Ads.xml"));
            XmlNode nodeList = doc2.SelectSingleNode("/sites/ads[id='" + adID.ToString() + "']");
            doc2.DocumentElement.RemoveChild(nodeList);
            doc2.Save(Server.MapPath("~/xml/Ads.xml"));
            //this.GridView1.DataBind();
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            WebMsgBox.WebMsgBox.Show("متاسفانه خطایی رخ داده است !" + ex.Message);
        }
    }
}
//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using MyWebPagesStarterKit.Controls;
using System.Collections.Generic;
using System.Web;
using MyWebPagesStarterKit;
using System.Web.UI.HtmlControls;

public partial class Administration_WebSite : PageBaseClass
{
    protected string lang;

    protected void Page_Load(object sender, EventArgs e)
    {
        //define language for the documentation path
        lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        if (!File.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Documentation/" + lang + "/quick_guide.html"))))
        {
            lang = "en";
        }

        if (!IsPostBack)
        {
            txtWebSiteTitle.Text = _website.WebSiteTitle;

            List<ThemeEntry> availableThemes = new List<ThemeEntry>();
            foreach (string dir in Directory.GetDirectories(Server.MapPath("~/App_Themes")))
            {
                ThemeEntry entry = new ThemeEntry();
                entry.Name = Path.GetFileName(dir);
                entry.Thumbnail = "~/Images/thumbnail_notavailable.jpg";
                
                if (File.Exists(dir + "/thumbnail.jpg"))
                {
                    entry.Thumbnail = string.Format("~/App_Themes/{0}/thumbnail.jpg", entry.Name);
                }
                availableThemes.Add(entry);
            }
            ViewState["Themes"] = availableThemes;

            txtSmtpServer.Text = _website.SmtpServer;
            txtSmtpUser.Text = _website.SmtpUser;
            txtSmtpPassword.Text = _website.SmtpPassword;
            txtSmtpDomain.Text = _website.SmtpDomain;
            txtFooter.Text = _website.FooterText;
            txtDescription.Text = _website.Description;
            txtKeywords.Text = _website.Keywords;
            txtSenderMail.Text = _website.MailSenderAddress;
            chkSectionRss.Checked = _website.EnableSectionRss;
			chkIE8Compatibility.Checked = _website.EnableIE8CompatibilityMetatag;
			chkAllowUserSelfRegistration.Checked = _website.AllowUserSelfRegistration;
			chkEnableVersionChecking.Checked = _website.EnableVersionChecking;
        }

        btnReset.OnClientClick = string.Format("return confirm('{0}')", ((string)GetGlobalResourceObject("StringsRes", "adm_Website_Reset_Confirmation")).Replace("'", "''"));
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        lstThemes.DataSource = (List<ThemeEntry>)ViewState["Themes"];
        lstThemes.DataBind();
    }

    protected void lstThemes_ItemCommand(object source, DataListCommandEventArgs e)
    {
        _website.Theme = (string)lstThemes.DataKeys[e.Item.ItemIndex];
        _website.SaveData();
        Server.Transfer(Request.RawUrl);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        _website.ResetWebSite();
        FormsAuthentication.SignOut();
		Response.Redirect("~/ChangePassword.aspx?firstuse=true", true);
       //Response.Redirect(string.Format("~/Default.aspx?pg={0}", MyWebPagesStarterKit.WebSite.GetInstance().HomepageId), true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _website.WebSiteTitle = txtWebSiteTitle.Text.Trim();
        _website.SmtpServer = txtSmtpServer.Text.Trim();
        _website.SmtpUser = txtSmtpUser.Text.Trim();
        _website.SmtpPassword = txtSmtpPassword.Text.Trim();
        _website.SmtpDomain = txtSmtpDomain.Text.Trim();
        _website.MailSenderAddress = txtSenderMail.Text.Trim();
        _website.FooterText = txtFooter.Text.Trim();
        _website.LocaleID = cmbLanguage.SelectedValue;
        _website.Keywords = txtKeywords.Text.Trim();
        _website.Description = txtDescription.Text.Trim();
        _website.EnableSectionRss = chkSectionRss.Checked;
		_website.EnableIE8CompatibilityMetatag = chkIE8Compatibility.Checked;
		_website.AllowUserSelfRegistration = chkAllowUserSelfRegistration.Checked;
		_website.EnableVersionChecking = chkEnableVersionChecking.Checked;
        _website.SaveData();

        SitemapEditor editor = new SitemapEditor();
        editor.UpdateAdministrationTitles(System.Globalization.CultureInfo.CreateSpecificCulture(cmbLanguage.SelectedValue));
        editor.Save();

        Response.Redirect(Request.RawUrl);
    }

    [Serializable]
    public struct ThemeEntry
    {
        private string _name;
        private string _thumbnail;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Thumbnail
        {
            get { return _thumbnail; }
            set { _thumbnail = value; }
        }
    }
}

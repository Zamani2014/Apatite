//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit.Providers;
using System.Xml;
using System.IO;

public partial class Administration_Navigation : PageBaseClass
{
    private SitemapEditor editor = null;
    protected string lang;

    protected void Page_Load(object sender, EventArgs e)
    {
        //define language for the documentation path
        lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        if (!File.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Documentation/" + lang + "/quick_guide.html"))))
        {
            lang = "en";
        }
       
        if (!User.IsInRole(RoleNames.Administrators.ToString()))
            Response.Redirect("Login.aspx");

        Response.Expires = 0;
        Response.ExpiresAbsolute = DateTime.Now.AddMinutes(-1.0);
        Response.CacheControl = "private";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            btnDelete.OnClientClick = "if (!confirm('" + Resources.StringsRes.adm_Navigation_PageDeletionWarning + "')) return false;";
            populateList(SiteMap.RootNode, 0);
            if (Request.QueryString["sel"] != null)
            {
                lstPages.SelectedValue = Request.QueryString["sel"];
                showPageDetails(lstPages.SelectedValue);
            }
        }
    }

    protected void valVirtualPath_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string virtualPath = txtVirtualPath.Text.Trim();
        if (virtualPath != string.Empty)
        {
            if (editor == null)
                editor = new SitemapEditor();

            if (!virtualPath.StartsWith("~/"))
            {
                if (virtualPath.StartsWith("/"))
                    virtualPath = virtualPath.Substring(1);
                virtualPath = "~/" + virtualPath;
            }
            if (!virtualPath.EndsWith(".aspx"))
            {
                virtualPath += ".aspx";
            }
            virtualPath = virtualPath.Replace(' ', '_');
            txtVirtualPath.Text = virtualPath;

            XmlNode node = editor.FindNodeByVirtualPath(virtualPath.ToLower());
            if (
                (node != null) 
                && 
                (
                    (node.Attributes["pageId"] == null) 
                    ||
                    (node.Attributes["pageId"].Value != lstPages.SelectedValue)
                )
            )
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        else
        {
            args.IsValid = true;
        }
    }

    private void populateList(SiteMapNode parentNode, int level)
    {
        foreach (SiteMapNode node in parentNode.ChildNodes)
        {
            if (node["pageId"] != null)
            {
                string title = node.Title;
                for (int i = 0; i < level; i++)
                    title = "..." + title;
                if (node["visible"] == false.ToString())
                    title += " (invisible)";
                if (node["pageId"] == _website.HomepageId)
                    title += " (Homepage)";
                lstPages.Items.Add(new ListItem(title, node["pageId"]));
                populateList(node, level + 1);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Validate();
        if (IsValid)
        {
            WebPage page = new WebPage(lstPages.SelectedValue);

            page.Title = txtTitle.Text.Trim();
            
            /* SEO Tags management */
            page.Description = txtDescription.Text.Trim();
            page.Keywords = txtKeywords.Text.Trim(); 

            page.NavigationName = txtNavigation.Text.Trim();
            page.Visible = chkVisible.Checked;
            page.AllowAnonymousAccess = chkAnonymousAccess.Checked;
            page.EditPowerUser = chkEditPowerUser.Checked; // (PowerUser Management)

            string virtualPath = txtVirtualPath.Text.Trim().ToLower();
            if (virtualPath != string.Empty)
            {
                if (!virtualPath.StartsWith("~/"))
                {
                    if (virtualPath.StartsWith("/"))
                        virtualPath = virtualPath.Substring(1);
                    virtualPath = "~/" + virtualPath;
                }
                if (!virtualPath.EndsWith(".aspx"))
                {
                    virtualPath += ".aspx";
                }

                virtualPath = virtualPath.Replace(' ', '_').Replace("+",string.Empty);
            }
            page.VirtualPath = virtualPath;
            txtVirtualPath.Text = virtualPath;
            page.SaveData();

            if (editor == null)
                editor = new SitemapEditor();
            editor.UpdatePage(page);
            editor.Save();

            Response.Redirect(string.Format("{0}?sel={1}", Request.Url.AbsolutePath, lstPages.SelectedValue));
        }
    }

    protected void btnSetHomepage_Click(object sender, EventArgs e)
    {
        WebPage page = new WebPage(lstPages.SelectedValue);

        page.AllowAnonymousAccess = true;
        page.Visible = true;
        page.SaveData();
        _website.HomepageId = lstPages.SelectedValue;
        _website.SaveData();

        Response.Redirect(Request.RawUrl);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Sidebar.GetInstance().RemoveSection(lstPages.SelectedValue);
        WebPage page = new WebPage(lstPages.SelectedValue);
        page.Delete();
        SitemapEditor editor = new SitemapEditor();
        editor.DeletePage(lstPages.SelectedValue);
        editor.Save();
       

        Response.Redirect(Request.Url.AbsolutePath);
    }

    protected void movePage_Command(object sender, CommandEventArgs e)
    {
        SitemapEditor editor = new SitemapEditor();
        switch (e.CommandName)
        {
            case "up":
                editor.MoveUp(lstPages.SelectedValue);
                break;
            case "down":
                editor.MoveDown(lstPages.SelectedValue);
                break;
            case "left":
                editor.MoveLevelUp(lstPages.SelectedValue);
                break;
            case "right":
                editor.MoveLevelDown(lstPages.SelectedValue);
                break;
        }
        editor.Save();

        Response.Redirect(string.Format("{0}?sel={1}", Request.Url.AbsolutePath, lstPages.SelectedValue));
    }

    protected void btnNewPage_Click(object sender, EventArgs e)
    {
        string PageId = lstPages.SelectedValue;

        WebPage page = new WebPage();
        page.NavigationName = Resources.StringsRes.adm_Navigation_NewPageDefaultName;
        page.AllowAnonymousAccess = true;
        page.Visible = true;
        page.SaveData();

        SitemapEditor editor = new SitemapEditor();
        editor.InsertPage(page, PageId);
        editor.Save();

        Response.Redirect(string.Format("{0}?sel={1}", Request.Url.AbsolutePath, page.PageId));
    }

    protected void lstPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        showPageDetails(lstPages.SelectedValue);
    }

    private void showPageDetails(string pageId)
    {
        WebPage page = new WebPage(pageId);
        txtTitle.Text = page.Title;
        txtDescription.Text = page.Description;
        txtKeywords.Text = page.Keywords; 
        txtNavigation.Text = page.NavigationName;
        txtVirtualPath.Text = page.VirtualPath;
        chkVisible.Checked = page.Visible;
        chkAnonymousAccess.Checked = page.AllowAnonymousAccess;
        chkEditPowerUser.Checked = page.EditPowerUser; // (PowerUser Management)

        bool isHomepage = (pageId == _website.HomepageId);
        chkVisible.Enabled = !isHomepage;
        chkAnonymousAccess.Enabled = !isHomepage;
        btnSetHomepage.Visible = !isHomepage;

        SitemapEditor editor = new SitemapEditor();
        btnDelete.Visible =
            (
                (lstPages.Items.Count > 1)
                &&
                (pageId != _website.HomepageId)
                &&
                (!editor.HasChildNodes(pageId))
            );

        tblPageDetails.Visible = true;
        tblMoveIcons.Visible = true;
        LocalizeMoveTitle.Visible = true;
    }
}

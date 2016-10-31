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

public partial class Administration_Sections : PageBaseClass
{
    #region Properties
    public string SelectedSectionId
    {
        get
        {
            if (ViewState["SelectedSectionId"] == null)
                return string.Empty;
            else
                return (string)ViewState["SelectedSectionId"];
        }
        set
        {
            ViewState["SelectedSectionId"] = value;
        }
    }

    public string SourcePageId
    {
        get
        {
            if (ViewState["SourcePageId"] == null)
                return string.Empty;
            else
                return (string)ViewState["SourcePageId"];
        }
        set
        {
            ViewState["SourcePageId"] = value;
        }
    }

    public string TargetPageId
    {
        get
        {
            if (ViewState["TargetPageId"] == null)
                return string.Empty;
            else
                return (string)ViewState["TargetPageId"];
        }
        set
        {
            ViewState["TargetPageId"] = value;
        }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole(RoleNames.Administrators.ToString()))
            Response.Redirect("Login.aspx");

        Response.Expires = 0;
        Response.ExpiresAbsolute = DateTime.Now.AddMinutes(-1.0);
        Response.CacheControl = "private";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

//        btnMove.Visible = showButton();

        if (!IsPostBack)
        {
            populateLists(SiteMap.RootNode, 0);
            btnMove.Visible = false;
            lstTargetPage.Visible = false;
            lstPageSections.Visible = false;
            thSection.Visible = false;
            thTargetPage.Visible = false;
        }
    }

    private void populateLists(SiteMapNode parentNode, int level)
    {
        WebSite website = WebSite.GetInstance();
        if (level == 0)
        {
            lstSourcePage.Items.Clear();
            //lstPageSections.Items.Clear();
            lstTargetPage.Items.Clear();
        }
        foreach (SiteMapNode node in parentNode.ChildNodes)
        {
            if (node["pageId"] != null)
            {
                string title = node.Title;
                for (int i = 0; i < level; i++)
                    title = "..." + title;
                if (node["visible"] == false.ToString())
                    title += " (invisible)";
                if (node["pageId"] == website.HomepageId)
                    title += " (Homepage)";
                lstSourcePage.Items.Add(new ListItem(title, node["pageId"]));
                lstTargetPage.Items.Add(new ListItem(title, node["pageId"]));
                populateLists(node, level + 1);
            }
        }
    }

    private void populateTargetPageList(SiteMapNode parentNode, int level)
    {
        WebSite website = WebSite.GetInstance();
        if (level == 0)
        {
            lstTargetPage.Items.Clear();
        }
        foreach (SiteMapNode node in parentNode.ChildNodes)
        {
            if (node["pageId"] != null)
            {
                string title = node.Title;
                for (int i = 0; i < level; i++)
                    title = "..." + title;
                if (node["visible"] == false.ToString())
                    title += " (invisible)";
                if (node["pageId"] == website.HomepageId)
                    title += " (Homepage)";
                lstTargetPage.Items.Add(new ListItem(title, node["pageId"]));
                populateTargetPageList(node, level + 1);
            }
        }
    }

    private bool showButton()
    {
        return (lstSourcePage.SelectedItem != null && lstPageSections.SelectedItem != null && lstTargetPage.SelectedItem != null);
    }

    protected void lstSourcePage_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        WebPage page = new WebPage(((ListBox)sender).SelectedValue);
        SourcePageId = page.PageId;

        // Update section listbox 
        lstPageSections.Items.Clear();
        SectionLoader loader = SectionLoader.GetInstance();
        foreach (string id in page.SectionIds)
        {
            ISection s = loader.LoadSection(id);
            if(s != null)
                lstPageSections.Items.Add(new ListItem(s.GetType().Name, id));
        }

        // Update target listbox to exclude the source page
        populateTargetPageList(SiteMap.RootNode, 0);
        lstTargetPage.Items.Remove(lstSourcePage.SelectedItem);

        lstPageSections.Visible = true;
        thSection.Visible = true;
        btnMove.Visible = false;

    }

    protected void lstPageSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Remember Section ID
        SelectedSectionId = ((ListBox)sender).SelectedValue;
        lstTargetPage.Visible = true;
        thTargetPage.Visible = true;
        btnMove.Visible = showButton();
    }

    protected void lstTargetPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        WebPage page = new WebPage(((ListBox)sender).SelectedValue);
        TargetPageId = page.PageId;
        //enable the move button
        btnMove.Visible = true;
    }

    protected void btnMoveSection_Click(object sender, EventArgs e)
    {

        WebPage source = new WebPage(SourcePageId);
        WebPage target = new WebPage(TargetPageId);

        source.RemoveSection(SelectedSectionId);
        target.AddSection(SelectedSectionId);

        //update the sidebar, if the section made part of it
        if (Sidebar.GetInstance().ContainsSection(SelectedSectionId, SourcePageId))
        {
            Sidebar.GetInstance().RemoveSection(SelectedSectionId, SourcePageId);
            Sidebar.GetInstance().AddSection(SelectedSectionId, TargetPageId);
        }

        //reload the the sections for this page:
        WebPage page = new WebPage(SourcePageId);
        SourcePageId = page.PageId;
        lstPageSections.Items.Clear();
        SectionLoader loader = SectionLoader.GetInstance();
        foreach (string id in page.SectionIds)
        {
            ISection s = loader.LoadSection(id);
            lstPageSections.Items.Add(new ListItem(s.GetType().Name, id));
        }

        //hide the move button
        btnMove.Visible = false;

    }
}

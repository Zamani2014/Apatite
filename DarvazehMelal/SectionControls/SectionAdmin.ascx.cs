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
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;

public partial class SectionControls_SectionAdmin : System.Web.UI.UserControl, ISectionAdmin
{
    private SectionControlBaseClass _section;
    private WebPage _page;

    public SectionControlBaseClass SectionControl
    {
        set { _section = value; }
    }

    public WebPage WebPage
    {
        set { _page = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        btnSendToSidebar.Visible = !(Sidebar.GetInstance().ContainsSection(_section.Section.SectionId, _page.PageId)) && (_section.Section is ISidebarObject);
        btnRemoveFromSidebar.Visible = Sidebar.GetInstance().ContainsSection(_section.Section.SectionId, _page.PageId);
        btnToggleViewMode.Visible = _section.HasAdminView;

        // PowerUser Management
        if (!Page.User.IsInRole(RoleNames.Administrators.ToString()))
        {
            btnDeleteSection.Visible = false;
        }

        if (_section.InfoUrl.Equals(""))
        {
            btnInfo.Visible = false;
        }
        else
        {
            btnInfo.OnClientClick = string.Format("window.open('{0}','InfoPopUp', 'height=780,width=800,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no');return false;", _section.InfoUrl);
        }
        if (!IsPostBack)
        {
			litSectionName.Text = _section.Section.GetLocalizedSectionName();
            string WarningText = string.Format(Resources.StringsRes.ResourceManager.GetString("ctl_SectionAdmin_DeleteWarning"), litSectionName.Text);
            btnDeleteSection.OnClientClick = "if(!confirm('" + WarningText + "')) return false;";
        }
    }


    #region Button Events
    protected void btnDeleteSection_Click(object sender, EventArgs e)
    {
        _page.RemoveSection(_section.Section.SectionId);
        _section.Section.Delete();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnMoveUp_Click(object sender, EventArgs e)
    {
        _page.MoveSectionUp(_section.Section.SectionId);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnMoveDown_Click(object sender, EventArgs e)
    {
        _page.MoveSectionDown(_section.Section.SectionId);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSendToSidebar_Click(object sender, EventArgs e)
    {
        Sidebar.GetInstance().AddSection(_section.Section.SectionId, _page.PageId);
    }

    protected void btnRemoveFromSidebar_Click(object sender, EventArgs e)
    {
        Sidebar.GetInstance().RemoveSection(_section.Section.SectionId, _page.PageId);
    }

    protected void btnToggleViewMode_Click(object sender, EventArgs e)
    {
        if (_section.ViewMode == ViewMode.Edit)
        {
            _section.ViewMode = ViewMode.Readonly;
            btnToggleViewMode.Text = Resources.StringsRes.glb__EditMode;
        }
        else
        {
            _section.ViewMode = ViewMode.Edit;
            btnToggleViewMode.Text = Resources.StringsRes.glb__ViewMode;
        }
    }

    #endregion
}
//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.Collections.Generic;

public partial class _Default : PageBaseClass
{
    private WebPage _page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.IsInRole(RoleNames.Administrators.ToString())) // only admin can create new sections
            {
				List<ISection> sectionList = new List<ISection>();
				foreach (Type t in _page.GetType().Assembly.GetTypes())
				{
					//find all classes that implement ISection and are not abstract
					if (t.IsClass && (t.GetInterface("ISection") != null) && (!t.IsAbstract))
					{
						//create a empty section-object for each found type
						ISection section = t.GetConstructor(new Type[] { }).Invoke(new object[] { }) as ISection;
						sectionList.Add(section);						
					}
				}

				//sort the list alphabetically by their localized names
				sectionList.Sort(new Comparison<ISection>(sectionComparer));

				foreach (ISection section in sectionList)
				{
					cmbSections.Items.Add(new ListItem(section.GetLocalizedSectionName(), section.GetType().AssemblyQualifiedName));
				}
            }
            else
            {
                divPageSettings.Visible = false;
            }
        }
    }

	private static int sectionComparer(ISection a, ISection b)
	{
		return a.GetLocalizedSectionName().CompareTo(b.GetLocalizedSectionName());
	}

    protected void btnNewSection_Click(object sender, EventArgs e)
    {
        ISection newSection = (ISection)Activator.CreateInstance(Type.GetType(cmbSections.SelectedValue));
        newSection.SaveData();

        _page.AddSection(newSection.SectionId);
        _page.SaveData();

        Response.Redirect(Request.RawUrl);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request.QueryString["pg"] != null)
        {
            _page = new WebPage(Request.QueryString["pg"]);

            if ((_page.AllowAnonymousAccess == false) && (User.Identity.IsAuthenticated == false))
                _page = new WebPage(_website.HomepageId);

            if (_page.Visible == false)
            {
                if (!((User.Identity.IsAuthenticated) && ((User.IsInRole(RoleNames.Administrators.ToString())) || User.IsInRole(RoleNames.PowerUsers.ToString()))))
                    _page = new WebPage(_website.HomepageId);
            }
        }
        else
        {
            Context.RewritePath(string.Format("{0}?pg={1}", Request.Path, _website.HomepageId));
            _page = new WebPage(_website.HomepageId);
        }

        Session["Homepage"] = (_page.PageId == _website.HomepageId);

        phSections.Controls.Clear();

        foreach (ISection section in _page.Sections)
        {
            SectionControlBaseClass ctl = (SectionControlBaseClass)LoadControl(section.UserControl);

            if (User.Identity.IsAuthenticated && PowerUserExt.HasEditRights(Page.User, _page)) // admin and power user can edit sections (PowerUser Management))
            {
                UserControl admin = (UserControl)LoadControl("~/SectionControls/SectionAdmin.ascx");
                ((ISectionAdmin)admin).SectionControl = ctl;
                ((ISectionAdmin)admin).WebPage = _page;
                phSections.Controls.Add(admin);
            }

            HtmlAnchor anchor = new HtmlAnchor();
            anchor.Name = section.SectionId;
            phSections.Controls.Add(anchor);

            phSections.Controls.Add(ctl);
            ctl.Section = section;
        }

        if (_page.Title.Trim() != string.Empty)
            Title = _page.Title;
        else
            Title = _website.WebSiteTitle;

         /* SEO Tags management */
        if (_page.Description.Trim() != string.Empty)
            Header.Controls.Add(WebPage.generateMeta("description", _page.Description));
        else if (_website.Description.Trim() != string.Empty)
            Header.Controls.Add(WebPage.generateMeta("description", _website.WebSiteTitle));

        if (_page.Keywords.Trim() != string.Empty)
            Header.Controls.Add(WebPage.generateMeta("keywords", _page.Keywords));
        else if (_website.Keywords.Trim() != string.Empty)
            Header.Controls.Add(WebPage.generateMeta("keywords", _website.Keywords));

        Header.Controls.Add(WebPage.generateMeta("generator", "My Web Pages Starter Kit " + ConfigurationManager.AppSettings["Version"] + " (from Codeplex)"));
    }
}

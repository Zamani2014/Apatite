//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;
using System.Web.UI;

public partial class SectionControls_Subpages : SectionControlBaseClass
{
    private ISection _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink lnkSubpages;
        if (SiteMap.CurrentNode != null)
		{
			foreach (SiteMapNode node in SiteMap.CurrentNode.ChildNodes)
			{
				lnkSubpages = new HyperLink();
				lnkSubpages.Text = node.Title;
				lnkSubpages.NavigateUrl = node.Url;
				plcSubpages.Controls.Add(lnkSubpages);
				plcSubpages.Controls.Add(new LiteralControl("<br />"));
			}
		}
    }

    public override ISection Section
    {
        set
        {
            if (value is Subpages)
                _section = value;
            else
                throw new ArgumentException("Section must be of type Subpages");
        }
        get { return _section; }
    }

    public override bool HasAdminView
    {
        get { return false; }
    }

    public override string InfoUrl
    {
        get
        {
            string lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (File.Exists(HttpContext.Current.Server.MapPath(string.Format("Documentation/" + lang + "/quick_guide.html"))))
            {
                return "Documentation/" + lang + "/quick_guide.html#subpages";
            }
            else
            { return "Documentation/en/quick_guide.html#subpages"; }
        }
    }
}

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
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;
using System.Web.SessionState;

public partial class SectionControls_HtmlContent : SectionControlBaseClass
{
    private HtmlContent _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Text = Resources.StringsRes.glb__Save;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
		_section.Html = txtHtml.Text.Trim();
        _section.SaveData();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
    }

    public override ISection Section
    {
        set
        {
            if (value is HtmlContent)
                _section = (HtmlContent)value;
            else
                throw new ArgumentException("Section must be of type HtmlContent");
        }
        get { return _section; }
    }

    public override bool HasAdminView
    {
        get { return true; }
    }

    public override string InfoUrl
    {
        get
        {
            string lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (File.Exists(HttpContext.Current.Server.MapPath(string.Format("Documentation/" + lang + "/quick_guide.html"))))
            {
                return "Documentation/" + lang + "/quick_guide.html#html-content";
            }
            else
            { return "Documentation/en/quick_guide.html#html-content"; }
        }
    }

    private void updateViews()
    {
        /// uploaded images get the placeholder {0} for the pageID. With the pageID the user access to the image can be evaluated.
        string pageId = Request.QueryString["pg"];

        if (ViewMode == ViewMode.Edit)
        {
            multiview.SetActiveView(editView);
            txtHtml.Text = _section.Html;
        }
        else
        {
            multiview.SetActiveView(readonlyView);
            litContent.Text = _section.Html.Replace("{0}", pageId);
        }
    }

}

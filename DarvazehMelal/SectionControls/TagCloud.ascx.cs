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
using System.IO;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;

public partial class SectionControls_TagCloud : SectionControlBaseClass
{
    private MyWebPagesStarterKit.TagCloud _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnsureID();
        }
        else
        {
            lbConfigSaved.Visible = false;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
    }

    protected void btnSaveConfiguration_Click(object sender, EventArgs e)
    {
        DataRow row = _section.ConfigurationData.Rows[0];
        row["Title"] = txtTagCloudTitle.Text.Trim();
        _section.ConfigurationData.AcceptChanges();
        _section.SaveData();

        //Reload TagCloud
        MyWebPagesStarterKit.TagCloud.reloadTagCloud();
        lbConfigSaved.Visible = true;
    }

     public override ISection Section
    {
        set
        {
            if (value is MyWebPagesStarterKit.TagCloud)
                _section = (MyWebPagesStarterKit.TagCloud)value;
            else
                throw new ArgumentException("Section must be of type TagCloud");
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
                return "Documentation/" + lang + "/quick_guide.html#tagcloud";
            }
            else
            { return "Documentation/en/quick_guide.html#tagcloud"; }
        }
    }

    private void updateViews()
    {
        if (ViewMode == ViewMode.Edit)
        {
            multiview.SetActiveView(editView);
        }
        else
        {
            //create tag cloud from all blogs
            if (_section.getCloudTitle() != string.Empty)
                tagCloud.CloudTitle = _section.getCloudTitle();

            tagCloud.DataSource = _section.TagsMerged();
            tagCloud.DataBind();

            multiview.SetActiveView(readonlyView);
        }
       
    }
  
}

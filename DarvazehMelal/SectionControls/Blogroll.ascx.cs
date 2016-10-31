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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;
using System.Web;

public partial class SectionControls_Blogroll : SectionControlBaseClass
{
    private Blogroll _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            valUrlRegex.ValidationExpression = Validation.UrlRegex;
            valFeedUrlRegex.ValidationExpression = Validation.UrlRegex;

            EnsureID();
            valUrlRequired.ValidationGroup = ID;
            valUrlRegex.ValidationGroup = ID;
            valFeedUrlRegex.ValidationGroup = ID;
            btnSaveDetails.ValidationGroup = ID;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //check if blogroll is visible
        if (ViewMode == ViewMode.Readonly && !(bool)_section.getConfiguration("Visible"))
            return;

        updateViews();
        if (RssCapable(this._section.GetType()) && _section.BlogrollEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);
            this.DisplayOPMLButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
    }

    protected void btnEditConfiguration_Click(object sender, EventArgs e)
    {
        ViewState["Configuration"] = "edit";
        lbConfigSaved.Visible = false;
    }

    protected void btnSaveConfiguration_Click(object sender, EventArgs e)
    {
        DataRow row = _section.ConfigurationData.Rows[0];
        row["Updated"] = DateTime.Now;
        row["Visible"] = ckbVisible.Checked;
        row["Title"] = txtBlogrollTitle.Text.Trim();
        _section.ConfigurationData.AcceptChanges();
        _section.SaveData();

        lbConfigSaved.Visible = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["Configuration"] = null;
    }

    protected void gvBlogrollEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "entry_edit":
                ViewState["Edit"] = gvBlogrollEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                break;
            case "entry_delete":
                _section.BlogrollEntries.Rows.Remove(_section.GetBlogrollEntry(gvBlogrollEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString()));
                _section.BlogrollEntries.AcceptChanges();
                _section.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
                _section.ConfigurationData.AcceptChanges();
                _section.SaveData();
                break;
        }
    }

    protected void btnNewEntry_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = "new";
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        DataRow row = null;
        if (ViewState["Edit"].ToString() == "new")
        {
            row = _section.BlogrollEntries.NewRow();
            row["Guid"] = Guid.NewGuid();
            row["Created"] = DateTime.Now;
            _section.BlogrollEntries.Rows.Add(row);
        }
        else
        {
            row = _section.GetBlogrollEntry(ViewState["Edit"].ToString());
        }

        if (row != null)
        {
            row["Title"] = txtTitle.Text.Trim();

            string url = txtUrl.Text.Trim();
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }
            txtUrl.Text = url;
            row["Url"] = url;
            if (txtFeedUrl.Text.Trim() == string.Empty)
                row["Feed"] = string.Empty;
            else
            {
                url = txtFeedUrl.Text.Trim();
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "http://" + url;
                }
                txtFeedUrl.Text = url;
                row["Feed"] = url;
            }
            row["Description"] = txtDescription.Text.Trim();
            if (txtOrder.Text.Trim() != string.Empty)
                row["Order"] = txtOrder.Text.Trim();
            else
                row["Order"] = 0;
                 
            row.AcceptChanges();
            
            //Date Updated
            _section.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
            _section.ConfigurationData.AcceptChanges();

            _section.SaveData();
        }

        ViewState["Edit"] = null;
    }

    protected void btnCancelDetails_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = null;
    }

    public override ISection Section
    {
        set
        {
            if (value is Blogroll)
                _section = (Blogroll)value;
            else
                throw new ArgumentException("Section must be of type Blogroll");
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
                return "Documentation/" + lang + "/quick_guide.html#blogroll";
            }
            else
            { return "Documentation/en/quick_guide.html#blogroll"; }
        }
    }

    private void updateViews()
    {
        if (ViewMode == ViewMode.Edit)
        {
            if (ViewState["Edit"] != null)
            {
                multiview.SetActiveView(editDetailsView);

                if (ViewState["Edit"].ToString() == "new")
                {
                    txtTitle.Text = string.Empty;
                    txtUrl.Text = "http://";
                    txtFeedUrl.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    txtOrder.Text = "0";
                }
                else
                {
                    DataRow entry = _section.GetBlogrollEntry(ViewState["Edit"].ToString());
                    txtTitle.Text = (string)entry["Title"];
                    txtUrl.Text = (string)entry["Url"];
                    txtFeedUrl.Text = (string)entry["Feed"];
                    txtDescription.Text = (string)entry["Description"];
                    txtOrder.Text = entry["Order"].ToString();
                }
            }
            else if (ViewState["Configuration"] != null)
            {
                ckbVisible.Checked = System.Convert.ToBoolean(_section.getConfiguration("Visible"));
                txtBlogrollTitle.Text = System.Convert.ToString(_section.getConfiguration("Title"));
                multiview.SetActiveView(editConfigurationView);
            }
            else
            {
                gvBlogrollEdit.DataSource = _section.GetBlogrollEntries();
                gvBlogrollEdit.DataBind();
                multiview.SetActiveView(editView);
            }
        }
        else
        {
            gvBlogroll.DataSource = _section.GetBlogrollEntries();
            gvBlogroll.DataBind();
            multiview.SetActiveView(readonlyView);
        }
    }

    protected string buildTitle(string title, string url)
    {
        if (title.Trim() != string.Empty)
            return title;
        else
            return url;
    }
}
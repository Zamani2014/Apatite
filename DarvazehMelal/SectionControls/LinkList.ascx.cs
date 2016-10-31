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

public partial class SectionControls_LinkList : SectionControlBaseClass
{
    private LinkList _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            valUrlRegex.ValidationExpression = Validation.UrlRegex;

            EnsureID();
            valUrlRequired.ValidationGroup = ID;
            valUrlRegex.ValidationGroup = ID;
            btnSaveDetails.ValidationGroup = ID;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        if (RssCapable(this._section.GetType()) && _section.LinkEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
    }

    protected void gvLinkListEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "entry_edit":
                ViewState["Edit"] = gvLinkListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                break;
            case "entry_delete":
                _section.LinkEntries.Rows.Remove(_section.GetLinkEntry(gvLinkListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString()));
                _section.LinkEntries.AcceptChanges();
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
            row = _section.LinkEntries.NewRow();
            row["Guid"] = Guid.NewGuid();
            _section.LinkEntries.Rows.Add(row);
        }
        else
        {
            row = _section.GetLinkEntry(ViewState["Edit"].ToString());
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
            row["Target"] = cmbTarget.SelectedValue;
            row["Comment"] = txtComment.Text.Trim();
            row.AcceptChanges();

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
            if (value is LinkList)
                _section = (LinkList)value;
            else
                throw new ArgumentException("Section must be of type LinkList");
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
                return "Documentation/" + lang + "/quick_guide.html#link-list";
            }
            else
            { return "Documentation/en/quick_guide.html#link-list"; }
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
                    cmbTarget.SelectedValue = "_blank";
                    txtComment.Text = string.Empty;
                }
                else
                {
                    DataRow entry = _section.GetLinkEntry(ViewState["Edit"].ToString());
                    txtTitle.Text = (string)entry["Title"];
                    txtUrl.Text = (string)entry["Url"];
                    cmbTarget.SelectedValue = (string)entry["Target"];
                    txtComment.Text = (string)entry["Comment"];
                }
            }
            else
            {
                gvLinkListEdit.DataSource = _section.GetLinkEntries();
                gvLinkListEdit.DataBind();
                multiview.SetActiveView(editView);
            }
        }
        else
        {
            gvLinkList.DataSource = _section.GetLinkEntries();
            gvLinkList.DataBind();
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
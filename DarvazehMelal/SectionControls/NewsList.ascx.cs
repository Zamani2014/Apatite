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

public partial class SectionControls_NewsList : SectionControlBaseClass
{
    private NewsList _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnsureID();
            valHeadlineRequired.ValidationGroup = ID;
            btnSaveDetails.ValidationGroup = ID;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        if (RssCapable(this._section.GetType()) && _section.NewsEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
    }

    protected void gvNewsListEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit_entry":
                ViewState["Edit"] = gvNewsListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                break;
            case "delete_entry":
                _section.NewsEntries.Rows.Remove(_section.GetNewsEntry(gvNewsListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString()));
                _section.NewsEntries.AcceptChanges();
                _section.SaveData();
                break;
        }
    }

    protected void gvNewsList_RowCommand(object source, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "show_details")
        {
            ViewState["detail"] = gvNewsList.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
        }
    }

    protected void srcNewsList_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = _section;
    }

    protected void btnNewEntry_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = "new";
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        DataRow row;
        if (ViewState["Edit"].ToString() == "new")
        {
            row = _section.NewsEntries.NewRow();
            row["Guid"] = Guid.NewGuid();
            _section.NewsEntries.Rows.Add(row);
        }
        else
        {
            row = _section.GetNewsEntry(ViewState["Edit"].ToString());
        }

        if (row != null)
        {
            row["Headline"] = txtHeadline.Text.Trim();
            row["Newstext"] = txtNewsHtml.Text.Trim();

            //adding datetime (only for correct sorting of news (Time is not shown)
            DateTime ShowFrom = new DateTime(dteDateFrom.DateValue.Year, dteDateFrom.DateValue.Month, dteDateFrom.DateValue.Day);
            ShowFrom = ShowFrom.AddHours(DateTime.Now.Hour);
            ShowFrom = ShowFrom.AddMinutes(DateTime.Now.Minute);
            ShowFrom = ShowFrom.AddSeconds(DateTime.Now.Second);

            row["NewsDate"] = ShowFrom;
            if (txtDateUntil.Text != string.Empty)
                row["ShowUntil"] = dteDateUntil.DateValue;
            else
                row["ShowUntil"] = DBNull.Value;
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
            if (value is NewsList)
                _section = (NewsList)value;
            else
                throw new ArgumentException("Section must be of type NewsList");
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
                return "Documentation/" + lang + "/quick_guide.html#news-list";
            }
            else
            { return "Documentation/en/quick_guide.html#news-list"; }
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
                    dteDateFrom.DateValue = DateTime.Today;
                    txtHeadline.Text = string.Empty;
                    txtNewsHtml.Text = string.Empty;
                }
                else
                {
                    DataRow entry = _section.GetNewsEntry(ViewState["Edit"].ToString());
                    dteDateFrom.DateValue = (DateTime)entry["NewsDate"];
                    if (entry["ShowUntil"] != DBNull.Value)
                        dteDateUntil.DateValue = (DateTime)entry["ShowUntil"];
                    txtHeadline.Text = (string)entry["Headline"];
					txtNewsHtml.Text = (string)entry["Newstext"];
                }
            }
            else
            {
                gvNewsListEdit.DataSource = _section.GetNewsEntriesEdit();
                gvNewsListEdit.DataBind();
                multiview.SetActiveView(editView);
            }
        }
        else
        {
            multiview.SetActiveView(readonlyView);

            gvNewsList.DataSource = _section.GetNewsEntriesReadonly();
            gvNewsList.DataBind();

            DataRow entry = null;

            if ((!IsPostBack) && (Request.QueryString["detail"] != null))
            {
                entry = _section.GetNewsEntry(Request.QueryString["detail"]);
                if (entry != null)
                {
                    ViewState["detail"] = Request.QueryString["detail"];
                }
            }

            if ((entry == null) && (ViewState["detail"] != null))
            {
                entry = _section.GetNewsEntry(ViewState["detail"].ToString());
            }

            if (entry != null)
            {
                litNewsDate.Text = ((DateTime)entry["NewsDate"]).ToShortDateString();
                litNewsTitle.Text = (string)entry["Headline"];
                litNewsContent.Text = (string)entry["Newstext"];
                divNewsDetails.Visible = true;
            }
            else
            {
                divNewsDetails.Visible = false;
                litNewsDate.Text = string.Empty;
                litNewsTitle.Text = string.Empty;
                litNewsContent.Text = string.Empty;
            }
        }
    }
}
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

public partial class SectionControls_Blog : SectionControlBaseClass
{
    private Blog _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnsureID();

            btnSaveDetails.ValidationGroup = ID;
            valContentRequired.ValidationGroup = ID;
            valTitleRequired.ValidationGroup = ID;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        if (RssCapable(this._section.GetType()) && _section.BlogEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
        //adding script for tags popup
        if (ViewMode == ViewMode.Edit)
        {
            HtmlGenericControl tags_script = new HtmlGenericControl("script");
            tags_script.Attributes.Add("type", "text/javascript");
            tags_script.InnerHtml = Environment.NewLine +
                                        " function openTags() " + Environment.NewLine +
                                        " { " + Environment.NewLine +
                                        " var tagsPopup = window.open('" + getTagsPopupLink() + "','EditTags','scrollbars=0,resizable=1,height=100,width=100,top=400,left=500,dependent=1'); " + Environment.NewLine +
                                        " tagsPopup.focus(); " + Environment.NewLine +
                                        " return false; " + Environment.NewLine +
                                        " } ";
            this.Page.Header.Controls.Add(tags_script);
        }

		string fullUrl = WebSite.GetInstance().GetFullWebsiteUrl(Context);

		Page.Header.Controls.Add(
			new LiteralControl(
				string.Format(
					"<link href=\"{0}\" title=\"RSD\" rel=\"edituri\" type=\"application/rsd+xml\" />", 
					fullUrl + "/MetablogInfo.ashx?type=rsd&blogID=" + _section.SectionId
				)
			)
		);

		Page.Header.Controls.Add(
			new LiteralControl(
				string.Format(
					"<link href=\"{0}\" rel=\"wlwmanifest\" type=\"application/wlwmanifest+xml\" />",
					fullUrl + "/MetablogInfo.ashx?type=wlwmanifest&blogID=" + _section.SectionId
				)
			)
		);
    }

    #region Blog Entries

    protected void lstBlog_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetails")
        {
            ViewState["detail"] = lstBlogEntries.DataKeys[e.Item.ItemIndex].ToString();
        }
    }

    protected void lstBlogEdit_ItemCommand(object source, DataListCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "entry_edit":
                ViewState["Edit"] = lstBlogEntriesEdit.DataKeys[e.Item.ItemIndex].ToString();
                break;
            case "entry_delete":

                //update tag number information
                DataRow blogEntry = _section.GetBlogEntry(lstBlogEntriesEdit.DataKeys[e.Item.ItemIndex].ToString());
                ArrayList entryTags = (ArrayList)blogEntry["Tags"];
                for (int i = 0; i < entryTags.Count; i++)
                {
                    DataRow blogTag = _section.GetBlogTag(entryTags[i].ToString());
                    int intNumber = (int)blogTag["Number"];
                    if (intNumber > 0)
                    {
                        blogTag["Number"] = intNumber - 1;
                    }
                    blogTag.AcceptChanges();
                }

                //remove blog entry
                _section.BlogEntries.Rows.Remove(_section.GetBlogEntry(lstBlogEntriesEdit.DataKeys[e.Item.ItemIndex].ToString()));
                _section.BlogEntries.AcceptChanges();

                ///blog updated
                _section.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
                _section.ConfigurationData.AcceptChanges();
                /// 

                _section.SaveData();
                pagBlogEdit.CurrentPageIndex = 0;
                break;
        }
    }

    protected void lstBlog_ItemBound(object source, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnShowComments = (LinkButton)e.Item.FindControl("btnShowComments");
            DataTable comments = (DataTable)DataBinder.Eval(e.Item.DataItem, "Comments");
            btnShowComments.Text = comments.Rows.Count.ToString() + " " + Resources.StringsRes.ctl_Blog_Comments;
        }
    }

    protected void btnNewEntry_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = "new";

        //Tags selected in Tag.aspx (popup) are safed in Session for new entry
        Session["EntryTags"] = null;
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        this.Page.Validate();
        if (valContentRequired.IsValid)
        {
            DataRow row = null;
            //new entry
            if (ViewState["Edit"].ToString() == "new")
            {
                row = _section.BlogEntries.NewRow();
                row["Guid"] = Guid.NewGuid();
                row["Created"] = DateTime.Now;
                row["Comments"] = _section.CommentsStructure();

                //saving selected tags from Tag Popup:
                if (Session["EntryTags"] != null)
                {
                    ArrayList tagsSelected = (ArrayList)Session["EntryTags"];
                    row["Tags"] = tagsSelected;

                    //safe tag number information
                    for (int i = 0; i < tagsSelected.Count; i++)
                    {
                        DataRow blogTag = _section.GetBlogTag(tagsSelected[i].ToString());
                        int intNumber = (int)blogTag["Number"];
                        blogTag["Number"] = intNumber + 1;
                        blogTag.AcceptChanges();
                    }

                    //Reload TagCloud
                    MyWebPagesStarterKit.TagCloud.reloadTagCloud();
                }
                else
                {
                    row["Tags"] = new ArrayList();
                }
                _section.BlogEntries.Rows.Add(row);
            }
            else
            {
                row = _section.GetBlogEntry(ViewState["Edit"].ToString());
            }

            if (row != null)
            {
                row["Title"] = txtTitle.Text.Trim();
                row["Content"] = txtBlogPostHtml.Text.Trim();
                row["Updated"] = DateTime.Now;
                row.AcceptChanges();

                ///blog updated
                _section.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
                _section.ConfigurationData.AcceptChanges();

                _section.SaveData();

                //ping weblogs:
                sendPing();
            }

            ViewState["Edit"] = null;
        }
        else
        {
            ViewState["Edit"] = "restore";
        }
    }

    protected void btnCancelDetails_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = null;
    }

    protected void valContentRequired_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        if (txtBlogPostHtml.Text.Trim() == string.Empty)
        {
            args.IsValid = false;
        }
    }

    #endregion

    #region Blog Comments

    protected void gvEntryCommentsEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "delete_comment":
                DataTable comments = _section.GetComments(ViewState["Edit"].ToString());
                if (comments.Rows.Count > 0)
                {
                    comments.Rows.Remove(_section.GetBlogEntryComment(comments, gvEntryCommentsEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString()));
                    comments.AcceptChanges();
                    _section.SaveData();
                }
                break;
        }
    }

    protected void btnNewComment_Click(object sender, EventArgs e)
    {
        Page.Validate(ID);
        if (Page.IsValid)
        {
            DataRow row = _section.GetComments(ViewState["detail"].ToString()).NewRow();
            row["Guid"] = Guid.NewGuid();
            row["Title"] = Server.HtmlEncode(txtCommentTitle.Text.Trim());
            row["Content"] = Server.HtmlEncode(txtCommentText.Text.Trim());
            row["Created"] = DateTime.Now;
            row["Author"] = Server.HtmlEncode(txtCommentAuthor.Text.Trim());
            _section.GetComments(ViewState["detail"].ToString()).Rows.Add(row);
            _section.GetComments(ViewState["detail"].ToString()).AcceptChanges();
            _section.SaveData();

            //reset Form
            txtCommentTitle.Text = string.Empty;
            txtCommentText.Text = string.Empty;
            txtCommentAuthor.Text = string.Empty;
            txtAntiBotImage.Text = string.Empty;

        }
    }

    protected string getCommentsFooter(Object EntryComments)
    {
        DataTable comments = (DataTable)EntryComments;
        return comments.Rows.Count.ToString() + " " + Resources.StringsRes.ctl_Blog_Comments;
    }

    protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
    }

    #endregion

    #region Blog Tags

    private string getTagsPopupLink()
    {
        return ResolveUrl(string.Format("~/keywords.aspx?pg={0}&section={1}&entry={2}", Request.QueryString["pg"], _section.SectionId, ViewState["Edit"]));
    }

    protected string getTagsFooter(Object EntryTags)
    {
        string strTags = string.Empty;
        ArrayList tags = (ArrayList)EntryTags;
        if (tags != null && tags.Count > 0)
        {
            strTags = "&nbsp;{" + Resources.StringsRes.ctl_Blog_Tags + ": ";
            for (int i = 0; i < tags.Count; i++)
            {
                string guid = tags[i].ToString();
                if (i > 0) strTags += ", ";
                strTags += _section.GetBlogTag(guid)["Name"].ToString();
            }
            strTags += "}";
        }
        return strTags;
    }

    protected void btnEditTags_Click(object sender, EventArgs e)
    {
        ViewState["Tags"] = "show_list";
    }

    protected void gvTagsEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit_tag":
                ViewState["Tags"] = gvTagsEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();

                break;
            case "delete_tag":
                string id = gvTagsEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();

                //remove tag from tag collection
                _section.BlogTags.Rows.Remove(_section.GetBlogTag(id));
                _section.BlogTags.AcceptChanges();

                //remove tag from blog entries
                for (int i = 0; i < _section.GetBlogEntries().Rows.Count; i++)
                {
                    DataRow entry = _section.GetBlogEntries().Rows[i];
                    ArrayList tags = (ArrayList)entry["Tags"];
                    if (tags.Contains(id))
                    {
                        tags.Remove(id);
                    }
                    entry.AcceptChanges();
                }

                ///blog updated
                _section.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
                _section.ConfigurationData.AcceptChanges();

                _section.SaveData();

                //Reload TagCloud
                MyWebPagesStarterKit.TagCloud.reloadTagCloud();

                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["Tags"] = null;
    }

    protected void btnNewTag_Click(object sender, EventArgs e)
    {
        ViewState["Tags"] = "new";
    }

    protected void btnCancelTag_Click(object sender, EventArgs e)
    {
        ViewState["Tags"] = "show_list";
    }

    protected void btnSaveTag_Click(object sender, EventArgs e)
    {
        this.Page.Validate();
        if (this.Page.IsValid)
        {
            DataRow row = null;
            if (ViewState["Tags"].ToString() == "new")
            {
                row = _section.BlogTags.NewRow();
                row["Guid"] = Guid.NewGuid();
                row["Number"] = 0;
                _section.BlogTags.Rows.Add(row);
            }
            else
            {
                row = _section.GetBlogTag(ViewState["Tags"].ToString());
            }

            if (row != null)
            {
                row["Name"] = txtTag.Text.Trim();
                row["Description"] = txtTagDescription.Text.Trim();
                row.AcceptChanges();

                ///blog updated
                _section.ConfigurationData.Rows[0]["Updated"] = DateTime.Now;
                _section.ConfigurationData.AcceptChanges();

                _section.SaveData();

                //Reload TagCloud
                MyWebPagesStarterKit.TagCloud.reloadTagCloud();
            }
            ViewState["Tags"] = "show_list";
        }
    }

    #endregion

    #region Configuration

    protected void btnConfig_Click(object sender, EventArgs e)
    {
        ViewState["Configuration"] = "edit";
        lbConfigSaved.Visible = false;
    }

    protected void btnBackConfig_Click(object sender, EventArgs e)
    {
        ViewState["Configuration"] = null;
    }
    protected void btnSaveConfiguration_Click(object sender, EventArgs e)
    {
        DataRow row = _section.ConfigurationData.Rows[0];
        row["Updated"] = DateTime.Now;
        row["SendPings"] = ckbSendPings.Checked;
        row["BlogName"] = txtBlogName.Text.Trim();
        _section.ConfigurationData.AcceptChanges();
        _section.SaveData();

        lbConfigSaved.Visible = true;
    }

    private void sendPing()
    {
        if (System.Convert.ToBoolean(_section.getConfiguration("SendPings")))
        {
            //ping weblogs:
            string blogName = string.Empty;
            try { blogName = _section.getConfiguration("BlogName").ToString(); }
            catch { }
            if (blogName == string.Empty) blogName = WebSite.GetInstance().WebSiteTitle + " " + Resources.StringsRes.ResourceManager.GetString("ctl_Blog_RssTitle");
            WebLogPing.PingWeblogsCom(Context, blogName, Request.QueryString["pg"]);
        }
    }

    #endregion

    #region Calendar

    protected void calendarBlog_SelectionChanged(object sender, EventArgs e)
    {
        ViewState["CalendarSelection"] = calendarBlog.SelectedDate;

        //reset the pager to the first page
        pagBlog.CurrentPageIndex = 0;
    }

    protected void calendarBlog_DayRender(object sender, DayRenderEventArgs e)
    {
        if (!e.Day.IsSelected)
        {
            e.Cell.Controls.Clear();
            e.Cell.Text = e.Day.DayNumberText;
        }
    }

    protected void calendarBlogDetail_SelectionChanged(object sender, EventArgs e)
    {
        ViewState["CalendarSelection"] = calendarBlogDetail.SelectedDate;

        //reset the pager to the first page
        pagBlog.CurrentPageIndex = 0;

        //escape the detail view in updateViews()
        ViewState["detail"] = null;
    }

    protected void calendarBlogDetail_DayRender(object sender, DayRenderEventArgs e)
    {
        if (!e.Day.IsSelected)
        {
            e.Cell.Controls.Clear();
            e.Cell.Text = e.Day.DayNumberText;
        }
    }


    #endregion

    public override ISection Section
    {
        set
        {
            if (value is Blog)
                _section = (Blog)value;
            else
                throw new ArgumentException("Section must be of type Blog");
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
                return "Documentation/" + lang + "/quick_guide.html#blog";
            }
            else
            { return "Documentation/en/quick_guide.html#blog"; }
        }
    }

    private void updateViews()
    {
        if (ViewMode == ViewMode.Edit)
        {
            //configuration
            if (ViewState["Configuration"] != null)
            {
                ckbSendPings.Checked = System.Convert.ToBoolean(_section.getConfiguration("SendPings"));
                txtBlogName.Text = System.Convert.ToString(_section.getConfiguration("BlogName"));
                multiview.SetActiveView(editConfigurationView);
            }
            //tags:
            else if (ViewState["Tags"] != null)
            {
                if (ViewState["Tags"].ToString() == "show_list")
                {
                    //bind tags to gridview
                    gvTagsEdit.DataSource = _section.GetBlogTagsSorted();
                    gvTagsEdit.DataBind();
                    multiview.SetActiveView(editTagsView);
                }
                else if (ViewState["Tags"].ToString() == "new")
                {
                    txtTag.Text = string.Empty;
                    txtTagDescription.Text = string.Empty;
                    multiview.SetActiveView(editTagsDetailsView);
                }
                else
                {
                    DataRow tag = _section.GetBlogTag(ViewState["Tags"].ToString());
                    txtTag.Text = tag["Name"].ToString();
                    txtTagDescription.Text = tag["Description"].ToString();
                    multiview.SetActiveView(editTagsDetailsView);
                }
            }
            else //Blog entries
            {
                if (ViewState["Edit"] != null)
                {
                    multiview.SetActiveView(editDetailsView);
                    pnEditComments.Visible = false;

                    if (ViewState["Edit"].ToString() == "new")
                    {
                        txtTitle.Text = string.Empty;
						txtBlogPostHtml.Text = string.Empty;
                    }
                    else if (ViewState["Edit"].ToString() == "restore")
                    {
                        //restoring values from viewstate
                        ViewState["Edit"] = "new";
                    }
                    else
                    {
                        //blog entry
                        DataRow entry = _section.GetBlogEntry(ViewState["Edit"].ToString());
                        txtTitle.Text = (string)entry["Title"];
						txtBlogPostHtml.Text = (string)entry["Content"];

                        //entry comments
                        DataTable comments = _section.GetComments(ViewState["Edit"].ToString());
                        if (comments.Rows.Count > 0)
                        {
                            pnEditComments.Visible = true;
                            gvEntryCommentsEdit.DataSource = _section.GetCommentsSorted(ViewState["Edit"].ToString());
                            gvEntryCommentsEdit.DataBind();
                        }
                    }
                }
                else
                {
                    lstBlogEntriesEdit.DataSource = _section.GetBlogEntriesSorted("Updated desc");
                    lstBlogEntriesEdit.DataBind();
                    multiview.SetActiveView(editView);
                }
            }
        }
        else
        {
            DataRow entry = null;

            if ((!IsPostBack) && (Request.QueryString["detail"] != null))
            {
                entry = _section.GetBlogEntry(Request.QueryString["detail"]);
                if (entry != null)
                {
                    ViewState["detail"] = Request.QueryString["detail"];
                }
            }

            if ((entry == null) && (ViewState["detail"] != null))
            {
                entry = _section.GetBlogEntry(ViewState["detail"].ToString());
            }

            if (entry != null)
            {
                //Blog Entry
                lbTitle.Text = (string)entry["Title"];
                ltContent.Text = (string)entry["Content"];

                //Footer
                lbFooter.Text = "{" + System.Convert.ToDateTime(entry["Updated"]).ToString("g") + "}" + getTagsFooter(entry["Tags"]);

                //comments
                if (_section.GetComments(entry["Guid"].ToString()).Rows.Count > 0)
                {
                    pnComments.Visible = true;
                    lbCommentsTitle.Text = Resources.StringsRes.ctl_Blog_Comments + ":";
                    lstEntryComments.DataSource = _section.GetComments(entry["Guid"].ToString());
                    lstEntryComments.DataBind();
                }
                else
                {
                    pnComments.Visible = false;
                    lbCommentsTitle.Text = Resources.StringsRes.ctl_Blog_NoComments;
                }

                //calendar selection
                foreach (DataRow row in _section.GetBlogEntries().Rows)
                {
                    calendarBlogDetail.SelectedDates.Add((DateTime)row["Updated"]);
                }

                multiview.SetActiveView(readonlyDetailsView);

                //show new Comment Panel
                pnNewComment.Visible = true;
            }
            else
            {
                if (ViewState["CalendarSelection"] != null)
                {
                    lstBlogEntries.DataSource = _section.GetBlogEntriesFiltered((DateTime)ViewState["CalendarSelection"], "Updated desc");
                }
                else
                {
                    lstBlogEntries.DataSource = _section.GetBlogEntriesSorted("Updated desc");
                }
                lstBlogEntries.DataBind();

                //calendar selection
                foreach (DataRow row in _section.GetBlogEntries().Rows)
                {
                    calendarBlog.SelectedDates.Add((DateTime)row["Updated"]);
                }

                multiview.SetActiveView(readonlyView);

                //hide new Comment Panel
                pnNewComment.Visible = false;
            }
        }
    }
}

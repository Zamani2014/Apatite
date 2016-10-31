using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.Diagnostics;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Web.Security;

public partial class SectionControls_Forum : SectionControlBaseClass
{
    private Forum _section;

	private Guid currentThreadID
	{
		get 
		{
			return (ViewState["currentThreadID"] != null) ? (Guid)ViewState["currentThreadID"] : Guid.Empty; 
		}
		set
		{
			ViewState["currentThreadID"] = value;
			if ((value != Guid.Empty) && (value != (Guid)Session["currentThreadID"]))
				_section.IncrementHitCount(value);
			Session["currentThreadID"] = value;
		}
	}

	#region Page Events
	protected void Page_Init(object sender, EventArgs e)
	{
		if (Session["currentThreadID"] == null)
			Session["currentThreadID"] = Guid.Empty;

		Page.ClientScript.RegisterClientScriptBlock(
			this.GetType(), 
			"forumScriptDialogVariables",
			string.Format(
				@"
				var forumDeleteThreadConfirmation = ""{0}"";
				var forumDeletePostConfirmation = ""{1}"";
				", 
				GetGlobalResourceObject("Forum", "DeleteThreadConfirmation").ToString().Replace("\"", "\\\""),
				GetGlobalResourceObject("Forum", "DeletePostConfirmation").ToString().Replace("\"", "\\\"")
			),
			true
		);
	}

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!string.IsNullOrEmpty(Request.QueryString["thread"]))
		{
			currentThreadID = _section.FindThreadIDByPermalink(Request.QueryString["thread"].Trim());
		}

		bool isAuthenticated = Page.User.Identity.IsAuthenticated || _section.AllowAnonymousPosts;
		btnCreateNewThread.Visible = isAuthenticated;
		btnReply.Visible = isAuthenticated;
    }

	protected void Page_PreRender(object sender, EventArgs e)
	{
		updateViews();
	}
	#endregion

	#region Thread methods
	protected void btnCreateNewThread_Click(object sender, EventArgs e)
    {
		txtSubject.Text = string.Empty;
		txtBody.Text = string.Empty;
		pnlNewThread.Visible = true;
		btnCreateNewThread.Visible = false;
    }

	protected void btnNewThreadSave_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser();
        string author = (user != null) ? user.UserName : "anonymous";
		Guid threadID = _section.AddNewThread(txtSubject.Text, txtBody.Text, author);

		if (Page.User.Identity.IsAuthenticated && Page.User.IsInRole(RoleNames.Administrators.ToString()))
		{
			List<Guid> threadIDs = new List<Guid>();
			threadIDs.Add(threadID);
			_section.ApproveThreads(threadIDs);
		}
        
		pnlNewThread.Visible = false;
		btnCreateNewThread.Visible = true;
    }

	protected void btnNewThreadCancel_Click(object sender, EventArgs e)
	{
		pnlNewThread.Visible = false;
		btnCreateNewThread.Visible = true;
	}

	protected void gvThreads_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		_section.DeleteThread((Guid)gvThreads.DataKeys[e.RowIndex].Value);
		pagThreads.CurrentPageIndex = 0;
	}

	protected void gvThreads_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow) && (Page.User.IsInRole(RoleNames.Administrators.ToString())))
			((WebControl)e.Row.Cells[0].Controls[0]).Attributes.Add("onclick", "return confirm(forumDeleteThreadConfirmation);");
	}

	protected string createThreadDeeplink(object permalink)
	{
		if(Request.RawUrl.Contains("?"))
			return Request.RawUrl.Substring(0, Request.RawUrl.IndexOf('?')) + QueryString.Current.Add("thread", permalink.ToString()) + "#" + _section.SectionId.ToString();
		else
			return Request.RawUrl + QueryString.Current.Add("thread", permalink.ToString()) + "#" + _section.SectionId.ToString();
	}
	#endregion

	#region Post methods
	protected void btnReply_Click(object sender, EventArgs e)
    {
		txtNewPostBody.Text = string.Empty;
		pnlNewPost.Visible = true;
		btnReply.Visible = false;
    }

	protected void btnSaveNewPost_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser();
        string author = (user != null) ? user.UserName : "anonymous";
		Guid postID = _section.AddNewPost(currentThreadID, txtNewPostBody.Text, author);

		if (Page.User.Identity.IsAuthenticated && Page.User.IsInRole(RoleNames.Administrators.ToString()))
		{
			List<Guid> postIDs = new List<Guid>();
			postIDs.Add(postID);
			_section.ApprovePosts(postIDs);
		}

		pnlNewPost.Visible = false;
		btnReply.Visible = true;

		//jump to the last page, where the new post will be visible
		double postCount = _section.GetPostsByThreadID(currentThreadID).Count;
		int currentPage = (int)Math.Ceiling(postCount / pagPosts.PageSize);
		pagPosts.CurrentPageIndex = currentPage - 1;
	}

	protected void btnCancelNewPost_Click(object sender, EventArgs e)
    {
		pnlNewPost.Visible = false;
		btnReply.Visible = true;
    }

	protected void gvPosts_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		_section.DeletePost((Guid)gvPosts.DataKeys[e.RowIndex].Value, currentThreadID);
		pagPosts.CurrentPageIndex = 0;
	}

	protected void gvPosts_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow) && (Page.User.IsInRole(RoleNames.Administrators.ToString())))
			((WebControl)e.Row.Cells[0].Controls[0]).Attributes.Add("onclick", "return confirm(forumDeletePostConfirmation);");
	}
	#endregion

	#region Edit view methods
	protected void btnSaveSettings_Click(object sender, EventArgs e)
	{
		_section.AllowAnonymousPosts = chkAllowAnonymousPosts.Checked;
		_section.EnableModeration = chkEnableModeration.Checked;
		_section.SaveData();
	}

	protected void btnApprovePosts_Click(object sender, EventArgs e)
	{
		List<Guid> postIDs = new List<Guid>();
		foreach (GridViewRow row in gvPostsToApprove.Rows)
		{
			if (
				(row.RowType == DataControlRowType.DataRow)
				&&
				((row.FindControl("chkApprove") as CheckBox).Checked)
			)
			{
				postIDs.Add(new Guid((row.FindControl("hdfPostID") as HiddenField).Value));
			}
		}
		if (postIDs.Count > 0)
			_section.ApprovePosts(postIDs);
	}

	protected void btnApproveThreads_Click(object sender, EventArgs e)
	{
		List<Guid> threadIDs = new List<Guid>();
		foreach (GridViewRow row in gvThreadsToApprove.Rows)
		{
			if (
				(row.RowType == DataControlRowType.DataRow)
				&&
				((row.FindControl("chkApprove") as CheckBox).Checked)
			)
			{
				threadIDs.Add(new Guid((row.FindControl("hdfThreadID") as HiddenField).Value));
			}
		}
		if (threadIDs.Count > 0)
			_section.ApproveThreads(threadIDs);
	}
	#endregion

	#region helper methods
	private void updateViews()
	{
		if (Page.User.IsInRole(RoleNames.Administrators.ToString()))
		{
			gvThreads.AutoGenerateDeleteButton = true;
			gvPosts.AutoGenerateDeleteButton = true;
		}

		if (ViewMode == ViewMode.Edit)
		{
			chkAllowAnonymousPosts.Checked = _section.AllowAnonymousPosts;
			chkEnableModeration.Checked = _section.EnableModeration;
			pnlApproval.Visible = _section.EnableModeration;

			if (_section.EnableModeration)
			{
				gvThreadsToApprove.DataSource = _section.GetThreadsToApprove();
				gvThreadsToApprove.DataBind();

				gvPostsToApprove.DataSource = _section.GetPostsToApprove();
				gvPostsToApprove.DataBind();
			}

			mvForum.SetActiveView(vwEdit);
		}
		else
		{
			if (currentThreadID != Guid.Empty)
			{
				mvForum.SetActiveView(vwPosts);

				DataView posts = _section.GetPostsByThreadID(currentThreadID);
				
				if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["post"]))
				{
					Guid postIdToFind = new Guid(Request.QueryString["post"]);

					int rowIndex = -1;
					for (int i = 0; rowIndex < posts.Count; i++)
					{
						if (((Guid)posts[i]["PostID"]) == postIdToFind)
						{
							rowIndex = i;
							break;
						}
					}
					
					if (rowIndex >= 0)
					{
						pagPosts.CurrentPageIndex = (int)Math.Floor((double)rowIndex / pagPosts.PageSize);
					}
				}

				gvPosts.DataSource = posts;
				gvPosts.DataBind();

				litThreadTitle.Text = _section.GetThreadTitle(currentThreadID);

				string backLink;
				if (Request.RawUrl.Contains("?"))
					backLink = string.Format("document.location.href = '{0}#{1}'; return false;", Request.RawUrl.Substring(0, Request.RawUrl.IndexOf('?')) + QueryString.Current.Remove("thread").ToString(), _section.SectionId);
				else
					backLink = string.Format("document.location.href = '{0}#{1}'; return false;", Request.RawUrl + QueryString.Current.Remove("thread").ToString(), _section.SectionId);

				btnBackToThreads1.OnClientClick = backLink;
				btnBackToThreads2.OnClientClick = backLink;
			}
			else
			{
				mvForum.SetActiveView(vwThreads);
				gvThreads.DataSource = _section.GetThreads();
				gvThreads.DataBind();
			}
		}
	}

	public string formatBody(string unformattedText)
	{
		string formattedText = TextFormatter.HtmlFormatNewlines(unformattedText);
		formattedText = TextFormatter.FormatUrls(formattedText);
		formattedText = TextFormatter.CreateEmbedsFromYoutubeLinks(formattedText, 300, 225);
		formattedText = EmoticonFormatter.FormatEmoticons(formattedText, ResolveUrl("~/"));
		return formattedText;
	}
	#endregion

	#region Section control stuff
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
				return "Documentation/" + lang + "/quick_guide.html#forum";
			}
			else
			{ return "Documentation/en/quick_guide.html#forum"; }
		}
	}

	public override ISection Section
	{
		set
		{
			if (value is Forum)
				_section = (Forum)value;
			else
				throw new ArgumentException("Section must be of type Forum");
		}
		get { return _section; }
	}
	#endregion
}

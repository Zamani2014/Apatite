using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;
using System.Text.RegularExpressions;

public partial class SectionControls_RssDisplay : SectionControlBaseClass
{
	private RssDisplay _section;

	protected void Page_Init(object sender, EventArgs e)
	{
		try
		{
			//check if http connections are allowed (they are by default disallowed in medium trust)
			System.Net.WebPermission p = new System.Net.WebPermission(System.Net.NetworkAccess.Connect, "http://www.google.com");
			p.Demand();
		}
		catch
		{
			multiview.SetActiveView(mediumTrustErrorView);
		}

		lstRssContent.ItemDataBound += new DataListItemEventHandler(lstRssContent_ItemDataBound);
	}

	void lstRssContent_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
		{
			Literal litDescription = e.Item.FindControl("litDescription") as Literal;

			//do not remove flickr images
			if (!_section.FeedUrl.ToLower().Contains(".flickr.com/"))
			{
				//remove any images that might be present in the description of the feed (they could break the layout when too wide)
				litDescription.Text = Regex.Replace(litDescription.Text, "<img[^>]*>", string.Empty);
			}
			//add target="_blank" to any links in the description (if they not already have a target="_blank")
			litDescription.Text = Regex.Replace(litDescription.Text, "<a[^>]*>", new MatchEvaluator(makeAnchorsTargetBlank));
		}
	}

	private string makeAnchorsTargetBlank(Match m)
	{
		if (m.Success)
		{
			if (!m.Value.ToLower().Contains("target=\"_blank\""))
			{
				return m.Value.Insert(m.Value.Length - 1, " target=\"_blank\"");
			}
		}
		return m.Value;
	}

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
		_section.FeedUrl = txtFeedUrl.Text.Trim();

		int maxItems = 10;
		int.TryParse(txtMaxItems.Text.Trim(), out maxItems);
		_section.MaxItems = maxItems;
		_section.SaveData();

		rssDataSource.Url = _section.FeedUrl;
		rssDataSource.MaxItems = _section.MaxItems;

		lstRssContent.DataBind();
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{

	}

	public override ISection Section
	{
		set
		{
			if (value is RssDisplay)
				_section = (RssDisplay)value;
			else
				throw new ArgumentException("Section must be of type RssDisplay");
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
				return "Documentation/" + lang + "/quick_guide.html#rssfeeds";
			}
			else
			{ return "Documentation/en/quick_guide.html#rssfeeds"; }
		}
	}

	private void updateViews()
	{
		//if http connections are not allowed, do not change views
		if (multiview.GetActiveView() == mediumTrustErrorView)
			return;

		if (ViewMode == ViewMode.Edit)
		{
			multiview.SetActiveView(editView);
			txtFeedUrl.Text = _section.FeedUrl;
			txtMaxItems.Text = _section.MaxItems.ToString();
		}
		else
		{
			multiview.SetActiveView(readonlyView);
			if (!string.IsNullOrEmpty(_section.FeedUrl))
			{
				rssDataSource.Url = _section.FeedUrl;
				rssDataSource.MaxItems = _section.MaxItems;
				lstRssContent.Visible = true;
			}
			else
			{
				lstRssContent.Visible = false;
			}
		}
	}
}

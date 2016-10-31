using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit;
using System.IO;
using MyWebPagesStarterKit.Controls;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Xml.Xsl;

public partial class SectionControls_TwitterStatusFeed : SectionControlBaseClass
{
	private TwitterStatusFeed _section;

	protected void Page_Load(object sender, EventArgs e)
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
		_section.Username = txtUsername.Text.Trim();

		int maxItems = 5;
		int.TryParse(txtMaxItems.Text.Trim(), out maxItems);
		_section.MaxItemsToShow = maxItems;
		_section.XslTemplateName = cmbTemplate.SelectedValue;
		_section.SaveData();

		updateXmlSettings(true);
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{

	}

	public override ISection Section
	{
		set
		{
			if (value is TwitterStatusFeed)
				_section = (TwitterStatusFeed)value;
			else
				throw new ArgumentException("Section must be of type TwitterStatusFeed");
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
				return "Documentation/" + lang + "/quick_guide.html#twitter";
			}
			else
			{ return "Documentation/en/quick_guide.html#twitter"; }
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
			txtUsername.Text = _section.Username;
			txtMaxItems.Text = _section.MaxItemsToShow.ToString();

			if (cmbTemplate.Items.Count == 0)
			{
				foreach (string file in Directory.GetFiles(Server.MapPath("~/TwitterStatusFeedTemplates"), "*.xsl", SearchOption.TopDirectoryOnly))
				{
					cmbTemplate.Items.Add(Path.GetFileName(file));
				}
			}

			if (cmbTemplate.Items.FindByValue(_section.XslTemplateName) != null)
				cmbTemplate.SelectedValue = _section.XslTemplateName;
		}
		else
		{
			multiview.SetActiveView(readonlyView);
			if (!string.IsNullOrEmpty(_section.Username) && !string.IsNullOrEmpty(_section.XslTemplateName))
			{
				updateXmlSettings(false);
				xmlTwitter.Visible = true;
			}
			else
			{
				xmlTwitter.Visible = false;
			}
		}
	}

	private void updateXmlSettings(bool invalidateCache)
	{
		xmlTwitter.TransformSource = "~/TwitterStatusFeedTemplates/" + _section.XslTemplateName;
		xmlTwitter.XPathNavigator = _section.GetTwitterXml(invalidateCache);
		xmlTwitter.TransformArgumentList = new XsltArgumentList();

		xmlTwitter.TransformArgumentList.AddParam("label_website", string.Empty, GetGlobalResourceObject("stringsRes", "ctl_TwitterStatusFeed_LabelWebsite"));
		xmlTwitter.TransformArgumentList.AddParam("label_twitterPage", string.Empty, GetGlobalResourceObject("stringsRes", "ctl_TwitterStatusFeed_LabelTwitterPage"));
		xmlTwitter.TransformArgumentList.AddParam("label_followers", string.Empty, GetGlobalResourceObject("stringsRes", "ctl_TwitterStatusFeed_LabelFollowers"));
		xmlTwitter.TransformArgumentList.AddParam("label_friends", string.Empty, GetGlobalResourceObject("stringsRes", "ctl_TwitterStatusFeed_LabelFriends"));
		xmlTwitter.TransformArgumentList.AddParam("label_tweets", string.Empty, GetGlobalResourceObject("stringsRes", "ctl_TwitterStatusFeed_LabelTweets"));
	}
}

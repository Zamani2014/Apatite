//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;
using System.Web;

public partial class SectionControls_Guestbook : SectionControlBaseClass
{
	private Guestbook _section;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			//Session["EntryMade"] = null;
			Session["antibotimage"] = generateRandomString(4).ToUpper();

			EnsureID();
			valAntiBotImage.ValidationGroup = ID;
			valAntiBotImageRequired.ValidationGroup = ID;
			valAuthorRequired.ValidationGroup = ID;
			valTextRequired.ValidationGroup = ID;
			btnNewEntry.ValidationGroup = ID;
		}
	}

	protected void Page_PreRender(object sender, EventArgs e)
	{
		updateViews();
		if (RssCapable(this._section.GetType()) && _section.GuestbookEntries.Rows.Count > 0)
		{
			WebPage page = new WebPage(Request.QueryString["pg"]);
			this.DisplayRssButton(page);

			//adding link in html-head for rss detection in the browswer
			HtmlLink link = this.MetaLink(page);
			this.Page.Header.Controls.Add(link);
		}


	}

	protected void gvGuestbookEdit_RowCommand(object source, GridViewCommandEventArgs e)
	{
		switch (e.CommandName)
		{
			case "entry_delete":
				_section.GuestbookEntries.Rows.Remove(_section.GetGuestbookEntry(gvGuestbookEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString()));
				_section.GuestbookEntries.AcceptChanges();
				_section.SaveData();
				break;
		}
	}

	protected void srcGuestbook_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
	{
		e.ObjectInstance = _section;
	}

	protected void btnNewEntry_Click(object sender, EventArgs e)
	{
		Page.Validate(ID);
		if (Page.IsValid)
		{
			DataRow row = _section.GuestbookEntries.NewRow();
			row["Guid"] = Guid.NewGuid();
			row["EntryDate"] = DateTime.Now;
			row["Author"] = Server.HtmlEncode(txtAuthor.Text.Trim());
			row["Text"] = Server.HtmlEncode(txtText.Text.Trim());
			_section.GuestbookEntries.Rows.Add(row);
			_section.GuestbookEntries.AcceptChanges();
			_section.SaveData();

			//reset Form
			txtAuthor.Text = string.Empty;
			txtText.Text = string.Empty;
			txtAntiBotImage.Text = string.Empty;

			Session["antibotimage"] = generateRandomString(4).ToUpper();
			//Session["EntryMade"] = true;
		}
	}

	public override ISection Section
	{
		set
		{
			if (value is Guestbook)
				_section = (Guestbook)value;
			else
				throw new ArgumentException("Section must be of type Guestbook");
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
				return "Documentation/" + lang + "/quick_guide.html#guestbook";
			}
			else
			{ return "Documentation/en/quick_guide.html#guestbook"; }
		}
	}

	private void updateViews()
	{
		if (ViewMode == ViewMode.Edit)
		{
			gvGuestbookEdit.DataSource = _section.GetGuestbookEntries();
			gvGuestbookEdit.DataBind();
			multiview.SetActiveView(editView);
		}
		else
		{
			//phEntryForm.Visible = (Session["EntryMade"] == null);
			DataView entriesView = _section.GetGuestbookEntries();
			if (entriesView.Count > 0)
			{
				repGuestbook.DataSource = entriesView;
				repGuestbook.DataBind();
				pnlEntries.Visible = true;
			}
			else
			{
				pnlEntries.Visible = false;
			}
			multiview.SetActiveView(readonlyView);
		}
	}

	private string generateRandomString(int size)
	{
		StringBuilder builder = new StringBuilder();
		Random random = new Random();
		char ch;
		for (int i = 0; i < size; i++)
		{
			ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
			builder.Append(ch);
		}
		return builder.ToString();
	}

	protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
	{
		args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
	}
}
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
using System.Collections.Generic;

public partial class SectionControls_Gallery : SectionControlBaseClass
{
    private Gallery _section;
    private int _intNumberOfUploads;

    protected string getSectionId()
    {
        return _section.SectionId;
    }

    protected string getPageId()
    {
        return Request.QueryString["pg"];
    }

    protected string getGalleryRssUrl()
    {
        return ResolveUrl("~/SectionRss.ashx?psid=" + Sidebar.CreatePageSectionKey(Section.SectionId, getPageId()));
    }

	public string SilverlightDataUrl
	{
		get 
		{
			return ResolveUrl(
				string.Format(
					"~/Silverlight/Gallery/GalleryData.ashx?pg={0}&section={1}",
					Request.QueryString["pg"],
					_section.SectionId
				)
			);
		}
	}

	public string SilverlightConfigurationUrl
	{
		get { return ResolveUrl("~/Silverlight/Gallery/Configuration.xml"); }
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnsureID();
            btnSaveDetails.ValidationGroup = ID;

            //See if there are any files that need importing
            CheckForPendingImports();

            //count the number of GalleryUploads in the aspx
            foreach (Control ctl in editDetailsView.Controls)
            {
                if (ctl.GetType().BaseType == typeof(GalleryUpload))
                {
                    _intNumberOfUploads += 1;
                }
            }
            ViewState.Add("NumberOfUploads", _intNumberOfUploads);
            if (Request.QueryString["silverlight"] != null)
                mvHtmlSilverlight.SetActiveView(vwSilverlight);
            else
                mvHtmlSilverlight.SetActiveView(vwHtml);
        }
        else
        {
            _intNumberOfUploads = System.Convert.ToInt32(ViewState["NumberOfUploads"]);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        if (RssCapable(this._section.GetType()) && _section.GalleryEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
    }

    protected void lstGalleryEdit_ItemCommand(object source, DataListCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "entry_edit":
                ViewState["Edit"] = lstGalleryEdit.DataKeys[e.Item.ItemIndex].ToString();
                break;
            case "entry_delete":
                DataRow row = _section.GetGalleryEntry(lstGalleryEdit.DataKeys[e.Item.ItemIndex].ToString());
                try
                {
                    string oldFile = Server.MapPath(_section.UploadDirectory) + "/" + (string)row["Filename"];
                    if (File.Exists(oldFile))
                        File.Delete(oldFile);
                }
                catch { }
                _section.GalleryEntries.Rows.Remove(row);
                _section.GalleryEntries.AcceptChanges();
                _section.SaveData();
                break;
        }
    }

    protected void lstGallery_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetails")
        {
            ViewState["detail"] = lstGallery.DataKeys[e.Item.ItemIndex].ToString();
        }
    }

    protected void btnNewEntry_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = "new";
    }

    protected void btnImportEntries_Click(object sender, EventArgs e)
    {
        ImportFilesIntoSection();
    }

    protected void btnShowSilverlightVersion_Click(object sender, EventArgs e)
    {
        mvHtmlSilverlight.SetActiveView(vwSilverlight);
    }

    protected void btnShowHtmlVersion_Click(object sender, EventArgs e)
    {
        mvHtmlSilverlight.SetActiveView(vwHtml);
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        DataRow row = null;
        string path;

        if (ViewState["Edit"].ToString() == "new")
        {
            for (int i = 1; i <= _intNumberOfUploads; i++)
            {
                GalleryUpload objUpload = (GalleryUpload)editDetailsView.FindControl("Upload" + i.ToString());

                if (objUpload.FilePosted.ContentLength > 0)
                {
                    row = _section.GalleryEntries.NewRow();
                    row["Guid"] = Guid.NewGuid();
                    _section.GalleryEntries.Rows.Add(row);

                    path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + Path.GetFileName(objUpload.FilePosted.FileName));
                    objUpload.FilePosted.SaveAs(path);
                    row["Filename"] = Path.GetFileName(path);
                    row["Title"] = objUpload.Title;
                    row["Comment"] = objUpload.Comment;

                    row.AcceptChanges();
                    _section.SaveData();
                }

            }
        }
        else
        {
            row = _section.GetGalleryEntry(ViewState["Edit"].ToString());
            if (Upload1.FilePosted.ContentLength > 0)
            {
                try
                {
                    string oldFile = Server.MapPath(_section.UploadDirectory) + "/" + (string)row["Filename"];
                    if (File.Exists(oldFile))
                        File.Delete(oldFile);
                }
                catch { }

                path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + Path.GetFileName(Upload1.FilePosted.FileName));
                Upload1.FilePosted.SaveAs(path);

                row["Filename"] = Path.GetFileName(path);
            }

            row["Title"] = Upload1.Title;
            row["Comment"] = Upload1.Comment;

            row.AcceptChanges();
            _section.SaveData();
        }

        ViewState["Edit"] = null;
    }

    protected void btnCancelDetails_Click(object sender, EventArgs e)
    {
        ViewState["Edit"] = null;
    }

    /// <summary>
    /// robs: Check to see if there are any files that need importing.
    /// If there are, display the import button
    /// </summary>
    private void CheckForPendingImports()
    {
        string[] filesToImport = Directory.GetFiles(HttpContext.Current.Server.MapPath(_section.ImportDirectory));

        if (filesToImport != null && filesToImport.Length > 0)
        {
            if (this.btnImportEntries == null)
            {
                throw new Exception(string.Format(@"Couldn't find <asp:button> with id=btnImportEntries, you'll need to add one to SectionControls/Gallery.ascx.cs if you want to import images.\r\n<asp:Button Visible=""false"" runat=""server"" ID=""btnImportEntries"" OnClick=""btnImportEntries_Click"" Text=""<%$ Resources:stringsRes, glb__import %>"" CausesValidation=""false"" />"));
            }
            this.btnImportEntries.Visible = true;
        }
    }

    /// <summary>
    /// robs: Import all files from _GalleryImports
    /// </summary>
    private void ImportFilesIntoSection()
    {
        string[] filesToImport = Directory.GetFiles(HttpContext.Current.Server.MapPath(_section.ImportDirectory));

        if (filesToImport != null && filesToImport.Length > 0)
        {
            string currentImport = null;

            try
            {
                for (int i = 0; i < filesToImport.Length; i++)
                {
                    currentImport = filesToImport[i];

                    FileInfo fileInfo = new FileInfo(currentImport);

                    ImportGalleryEntry(fileInfo, Path.GetFileNameWithoutExtension(fileInfo.Name), string.Empty);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Exception thrown during gallery import of file '{0}'", currentImport), ex);
            }
        }
    }

    /// <summary>
    /// robs: Create Gallery entries from individual files
    /// </summary>
    /// <param name="ImageToAdd">The image file to import</param>
    /// <param name="Title">The Tile of the Gallery entry</param>
    /// <param name="Comment">The Comment for the Gallery entry</param>
    private void ImportGalleryEntry(FileInfo ImageToAdd, string Title, string Comment)
    {
        if (ImageToAdd == null)
        {
            return;
        }

        DataRow row = _section.GalleryEntries.NewRow();
        row["Guid"] = Guid.NewGuid();
        _section.GalleryEntries.Rows.Add(row);

        string path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + ImageToAdd.Name);
        ImageToAdd.MoveTo(path);
        row["Filename"] = Path.GetFileName(path);

        path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + ImageToAdd.Name);
        ImageToAdd.CopyTo(path);

        row["Title"] = Title;
        row["Comment"] = Comment;

        row.AcceptChanges();
        _section.SaveData();
    }

    public override ISection Section
    {
        set
        {
            if (value is Gallery)
                _section = (Gallery)value;
            else
                throw new ArgumentException("Section must be of type Gallery");
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
                return "Documentation/" + lang + "/quick_guide.html#gallery";
            }
            else
            { return "Documentation/en/quick_guide.html#gallery"; }
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
                    for (int i = 1; i <= _intNumberOfUploads; i++)
                    {
                        GalleryUpload objUpload = (GalleryUpload)editDetailsView.FindControl("Upload" + i.ToString());
                        objUpload.Visible = true;

                        if (i == 1)
                        {
                            objUpload.UploadRequired = true;
                        }
                        objUpload.Title = string.Empty;
                        objUpload.Comment = string.Empty;
                    }
                }
                else
                {
                    for (int i = 1; i <= _intNumberOfUploads; i++)
                    {
                        GalleryUpload objUpload = (GalleryUpload)editDetailsView.FindControl("Upload" + i.ToString());
                        if (i == 1)
                        {
                            objUpload.UploadRequired = false;
                            DataRow entry = _section.GetGalleryEntry(ViewState["Edit"].ToString());
                            objUpload.Title = (string)entry["Title"];
                            objUpload.Comment = (string)entry["Comment"];
                        }
                        else
                        {
                            objUpload.Visible = false;
                        }
                    }
                }
            }
            else
            {
                lstGalleryEdit.DataSource = _section.GetGalleryEntries();
                lstGalleryEdit.DataBind();

                multiview.SetActiveView(editView);
            }
        }
        else
        {
            DataRow entry = null;

            if ((!IsPostBack) && (Request.QueryString["detail"] != null))
            {
                entry = _section.GetGalleryEntry(Request.QueryString["detail"]);
                if (entry != null)
                {
                    ViewState["detail"] = Request.QueryString["detail"];
                }
            }

            if ((entry == null) && (ViewState["detail"] != null))
            {
                entry = _section.GetGalleryEntry(ViewState["detail"].ToString());
            }

            if (entry != null)
            {
                litTitle.Text = (string)entry["Title"];
                imgGalleryBigView.SectionId = _section.SectionId;
                imgGalleryBigView.PageId = Request.QueryString["pg"];
                imgGalleryBigView.ImageUrl = (string)entry["Filename"];
                litComment.Text = (string)entry["Comment"];
                lnkDownload.NavigateUrl = ResolveUrl(string.Format("~/DownloadHandler.ashx?pg={0}&section={1}&file={2}", Request.QueryString["pg"], _section.SectionId, HttpUtility.UrlEncode((string)entry["Filename"])));
                multiview.SetActiveView(readonlyDetailsView);

                string previousId = _section.GetPreviousEntryId((Guid)entry["Guid"]);
                if (previousId != string.Empty)
                {
                    btnPrevious.CommandArgument = previousId;
                    btnPrevious.Visible = true;
                }
                else
                {
                    btnPrevious.Visible = false;
                }

                string nextId = _section.GetNextEntryId((Guid)entry["Guid"]);
                if (nextId != string.Empty)
                {
                    btnNext.CommandArgument = nextId;
                    btnNext.Visible = true;
                }
                else
                {
                    btnNext.Visible = false;
                }
            }
            else
            {
                lstGallery.DataSource = _section.GetGalleryEntries();
                lstGallery.DataBind();
                multiview.SetActiveView(readonlyView);
            }
        }
    }

    protected void DetailView_Command(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "move":
                ViewState["detail"] = e.CommandArgument.ToString();
                break;
            case "up":
                ViewState["detail"] = null;
                break;
        }
    }

    private string makeFilenameUnique(string path)
    {
        if (File.Exists(path))
        {
            int i = 1;
            while (true)
            {
                string temppath = path.Insert(path.LastIndexOf('.'), string.Format("[{0}]", i++));
                if (!File.Exists(temppath))
                    return temppath;
            }

        }
        else
        {
            return path;
        }
    }
}
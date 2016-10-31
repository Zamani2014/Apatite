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

public partial class SectionControls_DownloadList : SectionControlBaseClass
{
    private DownloadList _section;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnsureID();
            valDataRequired.ValidationGroup = ID;
            btnSaveDetails.ValidationGroup = ID;
        }
    }
 
    protected void Page_PreRender(object sender, EventArgs e)
    {
        updateViews();
        if (RssCapable(this._section.GetType()) && _section.DownloadEntries.Rows.Count > 0)
        {
            WebPage page = new WebPage(Request.QueryString["pg"]);
            this.DisplayRssButton(page);

            //adding link in html-head for rss detection in the browswer
            HtmlLink link = this.MetaLink(page);
            this.Page.Header.Controls.Add(link);
        }
    }

    protected void gvDownloadListEdit_RowCommand(object source, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "entry_edit":
                ViewState["Edit"] = gvDownloadListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                break;
            case "entry_delete":
                DataRow row = _section.GetDownloadEntry(gvDownloadListEdit.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                try
                {
                    string oldFile = Server.MapPath(_section.UploadDirectory) + "/" + (string)row["Filename"];
                    if (File.Exists(oldFile))
                        File.Delete(oldFile);
                }
                catch { }
                _section.DownloadEntries.Rows.Remove(row);
                _section.DownloadEntries.AcceptChanges();
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
            row = _section.DownloadEntries.NewRow();
            row["Guid"] = Guid.NewGuid();
            _section.DownloadEntries.Rows.Add(row);

            string path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + Path.GetFileName(filData.PostedFile.FileName));
            filData.PostedFile.SaveAs(path);

            row["Title"] = txtTitle.Text.Trim();
            row["Filename"] = Path.GetFileName(path);
            row["Size"] = formatFileSize(filData.PostedFile.ContentLength);
            row["Comment"] = txtComment.Text.Trim();
            
            row.AcceptChanges();
            _section.SaveData();
        }
        else
        {
            row = _section.GetDownloadEntry(ViewState["Edit"].ToString());

            if (filData.PostedFile.ContentLength > 0)
            {
                try
                {
                    string oldFile = Server.MapPath(_section.UploadDirectory) + "/" + (string)row["Filename"];
                    if (File.Exists(oldFile))
                        File.Delete(oldFile);
                }
                catch { }

                string path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + Path.GetFileName(filData.PostedFile.FileName));
                filData.PostedFile.SaveAs(path);
                
                row["Filename"] = Path.GetFileName(path);
                row["Size"] = formatFileSize(filData.PostedFile.ContentLength);
            }

            row["Title"] = txtTitle.Text.Trim();
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
            if (value is DownloadList)
                _section = (DownloadList)value;
            else
                throw new ArgumentException("Section must be of type DownloadList");
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
            if(File.Exists(HttpContext.Current.Server.MapPath(string.Format("Documentation/" + lang + "/quick_guide.html"))))
            {
                return "Documentation/" + lang + "/quick_guide.html#download-list";
            }
            else
            { return "Documentation/en/quick_guide.html#download-list"; }
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
                    txtComment.Text = string.Empty;
                    valDataRequired.Enabled = true;
                }
                else
                {
                    DataRow entry = _section.GetDownloadEntry(ViewState["Edit"].ToString());
                    txtTitle.Text = (string)entry["Title"];
                    txtComment.Text = (string)entry["Comment"];
                    valDataRequired.Enabled = false;
                }
            }
            else
            {
                gvDownloadListEdit.DataSource = _section.GetDownloadEntries();
                gvDownloadListEdit.DataBind();
                multiview.SetActiveView(editView);
            }
        }
        else
        {
            gvDownloadList.DataSource = _section.GetDownloadEntries();
            gvDownloadList.DataBind();
            multiview.SetActiveView(readonlyView);
        }
    }

    protected string getDownloadLink(string filename)
    {
        return ResolveUrl(Server.HtmlEncode(string.Format("~/DownloadHandler.ashx?pg={0}&section={1}&file={2}", Request.QueryString["pg"], _section.SectionId, HttpUtility.UrlEncode(filename))));
    }

    protected string getFiletypeIcon(string filename)
    {
        string ext;
        switch (Path.GetExtension(filename).ToLower().Replace(".", string.Empty))
        {
            case "bmp":
                ext = "bmp";
                break;
            case "doc":
            case "dot":
                ext = "doc";
                break;
            case "gif":
                ext = "gif";
                break;
            case "jpg":
            case "jpe":
            case "jpeg":
                ext = "jpg";
                break;
            case "pdf":
                ext = "pdf";
                break;
            case "png":
                ext = "png";
                break;
            case "pps":
                ext = "pps";
                break;
            case "ppt":
            case "ppo":
                ext = "ppt";
                break;
            case "rtf":
                ext = "rtf";
                break;
            case "txt":
                ext = "txt";
                break;
            case "xls":
                ext = "xls";
                break;
            case "zip":
            case "rar":
            case "ace":
            case "bz2":
            case "tar":
            case "gz":
                ext = "zip";
                break;
            case "eps":
                ext = "eps";
                break;
            case "exe":
                ext = "exe";
                break;
            case "htm":
            case "html":
                ext = "html";
                break;
            case "psd":
                ext = "psd";
                break;
            case "swf":
            case "fla":
                ext = "swf";
                break;
            case "tif":
            case "tiff":
                ext = "tiff";
                break;
            case "3gp":
            case "asf":
            case "asx":
            case "avi":
            case "divx":
            case "mov":
            case "mp4":
            case "mpg":
            case "mpeg":
            case "mpe":
            case "qt":
            case "rm":
            case "avs":
            case "wmv":
            case "wmf":
                ext = "video";
                break;
            case "aac":
            case "aif":
            case "iff":
            case "m3u":
            case "mid":
            case "midi":
            case "mpa":
            case "mpega":
            case "mp1":
            case "mp2":
            case "mp2a":
            case "mp3":
            case "ra":
            case "ram":
            case "wav":
            case "wma":
                ext = "audio";
                break;
            case "docm":
                ext = "docm";
                break;
            case "docx":
                ext = "docx";
                break;
            case "dotm":
                ext = "dotm";
                break;
            case "dotx":
                ext = "dotx";
                break;
            case "potm":
                ext = "potm";
                break;
            case "potx":
                ext = "potx";
                break;
            case "ppam":
                ext = "ppam";
                break;
            case "ppsm":
            case "ppsx":
                ext = "ppsx";
                break;
            case "pptm":
                ext = "pptm";
                break;
            case "pptx":
                ext = "pptx";
                break;
            case "xlam":
                ext = "xlam";
                break;
            case "xlsb":
                ext = "xlsb";
                break;
            case "xlsm":
                ext = "xlsm";
                break;
            case "xlsx":
                ext = "xlsx";
                break;
            case "xltx":
                ext = "xltx";
                break;
            default:
                ext = string.Empty;
                break;
        }

        if(ext != string.Empty)
            return string.Format("<img src=\"{0}\" alt=\"\" />", ResolveUrl(string.Format("~/Images/filetype_icons/icon_{0}.gif", ext)));
        else
            return string.Format("<img src=\"{0}\" alt=\"\" />", ResolveUrl("~/Images/filetype_icons/icon_none.gif"));
    }

    protected string buildTitle(string title, string filename)
    {
        if (title.Trim() != string.Empty)
            return title;
        else
            return filename;
    }

    private string formatFileSize(long size)
    {
        double newSize;

        if (size < 500)
            return size.ToString() + " B";
        else if (size < 90000)
        {
            newSize = size / 1024.0;
            return newSize.ToString("0.0") + " KB";
        }
        else
        {
            newSize = size / 1048576.0;
            return newSize.ToString("0.0") + " MB";
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

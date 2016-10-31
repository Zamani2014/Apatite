using System;
using System.Web;
using MyWebPagesStarterKit.Controls;
using MyWebPagesStarterKit;
using System.IO;
using System.Web.SessionState;
using System.Configuration;
using System.Web.Configuration;
using System.Runtime.InteropServices.ComTypes;


public partial class SectionControls_VideoContent : SectionControlBaseClass
{

    private VideoContent _section;

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
        /// if there is a new image uploaded, the placeholder [XXX] needs to be replaced by the pageID
        /// -> to enforce a secure download of the image with authentication of the user
        string pageId = Request.QueryString["pg"];

        if (filData.PostedFile.FileName != "")
        {
            string path = makeFilenameUnique(Server.MapPath(_section.UploadDirectory) + "/" + Path.GetFileName(filData.PostedFile.FileName));
            filData.PostedFile.SaveAs(path);
            _section.Filename = Path.GetFileName(filData.PostedFile.FileName);
            _section.Size = formatFileSize(filData.PostedFile.ContentLength);
        }

        _section.AutoPlay = cbxAutoPlay.Checked;
        _section.Muted = cbxMuted.Checked;
        _section.Keywords = txtKeywords.Text.Trim();

		_section.SaveData();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    public override ISection Section
    {
        set
        {
            if (value is VideoContent)
                _section = (VideoContent)value;
            else
                throw new ArgumentException("Section must be of type VideoContent");
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
                return "Documentation/" + lang + "/quick_guide.html#html-content";
            }
            else
            { return "Documentation/en/quick_guide.html#html-content"; }
        }
    }

    private int getMaxRequestLength()
    {
        // presume that the section is not defined in the web.config
        int result = 4096;

        HttpRuntimeSection section =
        ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
        if (section != null) result = section.MaxRequestLength;

        return result * 1000;
    }


    private void updateViews()
    {
        if (ViewMode == ViewMode.Edit)
        {
            multiview.SetActiveView(editView);
            cbxAutoPlay.Checked = _section.AutoPlay;
            cbxMuted.Checked = _section.Muted;
            txtKeywords.Text = _section.Keywords;
            if (_section.Filename != "")
            {
                hlVideoFile.Text = _section.Filename + " (" + _section.Size + ")";
                hlVideoFile.NavigateUrl = getDownloadLink();
            } // if
            else
            {
                hlVideoFile.Text = String.Format("Please upload a video file. (Maximum size: {0})", formatFileSize(getMaxRequestLength()));
                hlVideoFile.NavigateUrl = "";
            } // else
        }
        else
        {
            multiview.SetActiveView(readonlyView);
        }
    }

    protected string getFileName()
    {
        return _section.Filename;
    }


    protected string getVolume()
    {
        return Convert.ToString(_section.Volume / 10);
    }

    protected string getDownloadLink()
    {

		return Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port != 80) ? ":" + Request.Url.Port : "") +
			ResolveUrl(
				string.Format("~/DownloadHandler.ashx?pg={0}&section={1}&file={2}", 
				Request.QueryString["pg"], 
				_section.SectionId, 
				HttpUtility.UrlEncode(_section.Filename)
			)
		);
    }


    protected bool getAutoPlay()
    {
        return _section.AutoPlay;
    }

    int id = 0;


    protected int getId()
    {
        return id++;
    }


    protected bool getMuted()
    {
        return _section.Muted;
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
    } // formatFileSize

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
    } // makeFilenameUnique

}

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

public partial class GalleryUpload : System.Web.UI.UserControl
{

    public bool UploadRequired
    {
        set { valFileRequired.Enabled = value; }
    }

    public string Title
    {
        set { txtTitle.Text = value; }
        get { return txtTitle.Text.Trim(); }
    }

    public string Comment
    {
        set { txtComment.Text = value; }
        get { return txtComment.Text.Trim(); }
    }

    public HttpPostedFile FilePosted
    {
        get { return fileData.PostedFile; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            valFileRequired.Enabled = false;
            valFileRequired.ValidationGroup = this.NamingContainer.ID;
        }
    }
}

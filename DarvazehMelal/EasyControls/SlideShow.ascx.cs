using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
/*
 Created on 10162009 By Bryian Tan
 * ASP.NET slide-show Control with jQuery and XML
 */
public partial class SlideShowControl_SlideShow : System.Web.UI.UserControl
{
    //slide div
    public string WrapperID
    {
        get { return this.ClientID + "Div"; }
    }

    //xPath
    private string _xPath = "site";

    [DefaultValue("site")]
    public string xPath
    {
        get { return this._xPath; }
        set { this._xPath = value; }
    }

    //xml file
    private string _xmlSource = "~/xml/sites.xml";
    [UrlProperty]
    [Bindable(true)]
    [DefaultValue("~/xml/sites.xml")]
    public string XMLSource
    {
        get { return this._xmlSource; }
        set { this._xmlSource = value; }
    }

    //width of the slide
    private int _width =728;
    [DefaultValue(728)]
    public int Width
    {
        set { this._width = value; }
        get { return this._width; }
    }

    //height of the slide
    private int _height = 95;
    [DefaultValue(95)]
    public int Height
    {
        set { this._height = value; }
        get { return this._height; }
    }
  
    /// <summary>
    /// autoplay true|false
    /// </summary>
    /// 
    private bool _autoPlay = true;
    [DefaultValue(true)]
    public bool AutoPlay
    {
        get
        {
            return this._autoPlay;
        }
        set
        {
            this._autoPlay = value;
        }
    }

    /// <summary>
    /// Show Navigation Control true|false
    /// </summary>
    /// 
    private bool _showNavigation = true;
    [DefaultValue(true)]
    public bool ShowNavigation
    {
        get
        {
            return this._showNavigation;
        }
        set
        {
            this._showNavigation = value;
        }
    }

    private int _delay_btw_slide = 10000;
    /// <summary>
    /// delay between slide in miliseconds
    /// </summary>
    /// 
    [DefaultValue(10000)]
    public int Delay_btw_slide
    {
        set { this._delay_btw_slide = value; }
        get { return this._delay_btw_slide; }
    }

    private int _fadeDuration = 2000;
    /// <summary>
    /// transition duration (milliseconds)
    /// </summary>
    /// 
    [DefaultValue(2000)]
    public int FadeDuration
    {
        set { this._fadeDuration = value; }
        get { return this._fadeDuration; }
    }

    private int _cycles_before_stopping = 99;
    /// <summary>
    /// cycles befote stopping
    /// </summary>
    [DefaultValue(99)]
    public int Cycles_before_stopping
    {
        set { this._cycles_before_stopping = value; }
        get { return this._cycles_before_stopping; }
    }

    /// <summary>
    /// previous button
    /// </summary>
    /// 
    private string _btnPrevious = "~/images/previous.gif";
    [Category("Appearance")]
    [Localizable(true)]
    [Description("Previous button")]
    [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [UrlProperty]
    [Bindable(true)]
    [DefaultValue("~/images/previous.gif")]
    public string BtnPrevious
    {
        get
        {
            return this._btnPrevious;
        }
        set
        {
            this._btnPrevious = value;
        }
    }

    /// <summary>
    /// Next button
    /// </summary>
    /// 
    private string _btnNext = "~/images/next.gif";
    [Category("Appearance")]
    [Localizable(true)]
    [Description("Next button")]
    [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [UrlProperty]
    [Bindable(true)]
    [DefaultValue("~/images/next.gif")]
    public string BtnNext
    {
        get
        {
            return this._btnNext;
        }
        set
        {
            this._btnNext = value;
        }
    }

    /// <summary>
    /// Play button
    /// </summary>
    /// 
    private string _btnPlay = "~/images/play.gif";
    [Category("Appearance")]
    [Localizable(true)]
    [Description("Play button")]
    [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [UrlProperty]
    [Bindable(true)]
    [DefaultValue("~/images/play.gif")]
    public string BtnPlay
    {
        get
        {
            return this._btnPlay;
        }
        set
        {
            this._btnPlay = value;
        }
    }

    /// <summary>
    /// Play button
    /// </summary>
    /// 
    private string _btnPause = "~/images/pause.gif";
    [Category("Appearance"), Localizable(true)]
    [Description("Pause button")]
    [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [UrlProperty]
    [Bindable(true)]
    [DefaultValue("~/images/pause.gif")]
    public string BtnPause
    {
        get
        {
            return this._btnPause;
        }
        set
        {
            this._btnPause = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    { 
        LoadJScript();
        CreateScript();
        CreateDiv();
    }

    //create the client script to read the xml dynamically and initialize var
    void CreateScript()
    {
        StringBuilder ssScript = new StringBuilder(string.Empty);
        string arrName = "myArray" + this.WrapperID;

        //read XML
        ssScript.Append("var " + arrName+ "= [];");
        ssScript.Append("$(document).ready(function() {");
        ssScript.Append(" $.ajax({");
        ssScript.Append("type: \"GET\",");
        ssScript.Append("url: '" + ResolveUrl(XMLSource) + "',");
        ssScript.Append("cache: true,");
        ssScript.Append("dataType: \"xml\",");
        ssScript.Append("success: function(xml) {");
        ssScript.Append("var count = 0;");
        ssScript.Append("$(xml).find('" + xPath + "').each(function() {");

        ssScript.Append(" var url = $(this).find('url').text();");
        ssScript.Append("var target = $(this).find('target').text();");
        ssScript.Append("var imageURL = $(this).find('imageURL').text();");
        ssScript.Append("var alt = $(this).find('alt').text();");

        ssScript.Append(arrName + "[parseInt(count)] = new Array(imageURL, url, target, alt); ");
        ssScript.Append("count++;");
        ssScript.Append("});");

        //slide-shows
        ssScript.Append(" var mygallery"+this.WrapperID+" = new simpleGallery({");
        ssScript.Append(" wrapperid: '" + this.ClientID + "_" + this.WrapperID + "',");
        ssScript.Append("dimensions: [" + Width.ToString() + ","+ Height.ToString()+"],"); //width/height of gallery in pixels. Should reflect dimensions of the images exactly
        ssScript.Append("imagearray: "+arrName+","); //array of images
        ssScript.Append("navimages: ['" + ResolveUrl(BtnPrevious) + "', '" + ResolveUrl(BtnPlay) + "', '" + ResolveUrl(BtnNext) + "', '" + ResolveUrl(BtnPause) + "'],");
        ssScript.Append("showpanel: '" + ShowNavigation.ToString().ToLower() + "',");
        ssScript.Append(" autoplay: [" + AutoPlay.ToString().ToLower() + "," + Delay_btw_slide.ToString() + "," + Cycles_before_stopping.ToString() + "],"); //[auto_play_boolean, delay_btw_slide_millisec, cycles_before_stopping_int]
        ssScript.Append(" persist: true,");
        ssScript.Append(" fadeduration:" + FadeDuration.ToString() + ","); //transition duration (milliseconds)
        ssScript.Append(" oninit: function() {"); //event that fires when gallery has initialized/ ready to run
        ssScript.Append("  },");
        ssScript.Append("  onslide: function(curslide, i) {"); //event that fires after each slide is shown
        //curslide: returns DOM reference to current slide's DIV (ie: try alert(curslide.innerHTML)
        //i: integer reflecting current image within collection being shown (0=1st image, 1=2nd etc)
        ssScript.Append("   }");
        ssScript.Append("  })");
        ssScript.Append("  }");
        ssScript.Append("   });");

        ssScript.Append(" });");

        ClientScriptManager jScript = Page.ClientScript;
        jScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), ssScript.ToString(), true);

    }

    //internal string ResolveLink(string link)
    //{
    //    if (!link.Contains("http://"))
    //    {
    //        link = ResolveUrl("~/" + link.Replace("~/", ""));
    //    }

    //    return link;
    //}

    void CreateDiv()
    {
        System.Web.UI.HtmlControls.HtmlGenericControl ssDivWrapper = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
        ssDivWrapper.ID = this.WrapperID;

        ssDivWrapper.Style.Add("background", "white none repeat scroll 0% 0%");
        ssDivWrapper.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");
        ssDivWrapper.Style.Add(HtmlTextWriterStyle.Position, "relative");
        ssDivWrapper.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
        ssDivWrapper.Style.Add("-moz-background-clip", "border");
        ssDivWrapper.Style.Add("-moz-background-origin", "padding");
        ssDivWrapper.Style.Add("-moz-background-inline-policy", "continuous");
        ssDivWrapper.Style.Add("-webkit-background-clip", "border");
        ssDivWrapper.Style.Add("-webkit-background-origin", "padding");
        ssDivWrapper.Style.Add("-webkit-background-inline-policy", "continuous");
        ssDivWrapper.Style.Add("background-clip", "border");
        ssDivWrapper.Style.Add("background-origin", "padding");
        ssDivWrapper.Style.Add("background-inline-policy", "continuous");

        this.Controls.Add(ssDivWrapper);
    }

    //load the javascript
    internal void LoadJScript()
    {
        ClientScriptManager script = Page.ClientScript;
        //prevent duplicate script
        if (!script.IsStartupScriptRegistered(this.GetType(), "JQuerySlideShowJS"))
        {
            script.RegisterClientScriptBlock(this.GetType(), "JQuerySlideShowJS",
            "<script type='text/javascript' src='" + ResolveUrl("~/EasyControls/js/jquery-1.3.2.min.js") + "'></script>");
        }

        if (!script.IsStartupScriptRegistered(this.GetType(), "SimpleGalleryJS"))
        {
            script.RegisterClientScriptBlock(this.GetType(), "SimpleGalleryJS",
            "<script type='text/javascript' src='" + ResolveUrl("~/EasyControls/js/simplegallery.js") + "'></script>");
        }
    }

}

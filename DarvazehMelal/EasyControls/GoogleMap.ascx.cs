//===============================================================================================
//
// The Gadget can be downloaded on:
// http://www.google.com/ig/directory?synd=open&source=gghp&num=24&url=http://ralph.feedback.googlepages.com/googlemap.xml&q=&start=0
// Gadgets powered by Google (Open Source)
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

public partial class EasyControls_GoogleMap : System.Web.UI.UserControl
{
    protected string _script;
    private string _address = "1 Central Park W";
    private string _city = "NEW YORK  NY";
    private string _country = "US";
    private int _width = 320;
    private int _height = 200;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strLocation;

        strLocation = _address + ", " + _city + ", " + _country;
        _script = "<script src=\"http://gmodules.com/ig/ifr?url=http://ralph.feedback.googlepages.com/googlemap.xml&amp;up_locname=Google&amp;up_loc=" + Server.UrlEncode(strLocation) + "&amp;up_zoom=Street&amp;up_view=Map&amp;synd=open&amp;w=" + _width.ToString() + "&amp;h=" + _height.ToString() + "&amp;title=&amp;border=%23ffffff%7C3px%2C1px+solid+%23999999&amp;output=js\"></script>";
    }
}

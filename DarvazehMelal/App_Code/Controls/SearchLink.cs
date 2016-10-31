using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Web.Configuration;

/// <summary>
/// Summary description for SearchLink
/// </summary>
namespace MyWebPagesStarterKit.Controls
{

    public class SearchLink : HyperLink
    {
        public SearchLink()
            : base()
        {
            CssClass = "sel";
            NavigateUrl = "~/Search.aspx";
            Text = Resources.StringsRes.glb__Search;
        }
    }
}
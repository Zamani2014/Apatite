//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Xsl;

namespace MyWebPagesStarterKit.Controls
{
    /// <summary>
    /// Sidebar control to display a vertical bar with various RSS items/channels
    /// </summary>
    public class SidebarControl : WebControl, INamingContainer
    {
        /// <summary>
        /// Reference to sidebar data object
        /// </summary>
        protected Sidebar _sidebar;

        /// <summary>
        /// Constructor, initializes sidebar data
        /// </summary>
        public SidebarControl(): base()
	    {
            _sidebar = Sidebar.GetInstance();
	    }

        /// <summary>
        /// Adds controls to the sidebar depending on the Sidebar data object
        /// </summary>
        protected override void CreateChildControls()
        {
            string Xml = _sidebar.GetRSSFeed(HttpContext.Current.User.Identity);

            if (Xml != string.Empty)
            {
                //for Valid XHTML 1.0 Transitional the div-tag cannot be placed directly within span-tag
                //span-tag is created automatically by the .net control

                //--> <object> not working with safari !!
                //HtmlGenericControl obj = new HtmlGenericControl("object");
                //Controls.Add(obj);

                HtmlGenericControl divTop = new HtmlGenericControl("div");
                divTop.Attributes.Add("class", "sidebartop");
                Controls.Add(divTop);
                //obj.Controls.Add(divTop);
                
                HtmlGenericControl divSidebar = new HtmlGenericControl("div");
                divSidebar.Attributes.Add("class", "sidebar");
                Controls.Add(divSidebar);
                //obj.Controls.Add(divSidebar);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Xml);
                
                foreach (XmlNode link in doc.SelectNodes("//link"))
                {
                    link.InnerText = ResolveUrl(link.InnerText);
                    if (link.InnerText.IndexOf("http://") == 0 || link.InnerText.IndexOf("https://") == 0)
                    {
                        XmlAttribute target = doc.CreateAttribute("target");
                        target.Value = "_blank";
                        link.Attributes.Append(target);
                    }
                }

                Xml ctrlFeed = new Xml();
                divSidebar.Controls.Add(ctrlFeed);
                ctrlFeed.ID = "ctrlSidebarFeed";
                
                ctrlFeed.XPathNavigator = doc.CreateNavigator();
                ctrlFeed.TransformSource = Page.Server.MapPath("~/App_Themes/" + Page.Theme + "/sidebar.xsl");
                ctrlFeed.DataBind();

                /* it is a known issue that the html encoding is not correct with the Xml control and XSLT. The alternative is to create the string manually and render it.
                 * See http://weblogs.asp.net/justin_rogers/archive/2004/08/04/208464.aspx
                 */

                HtmlGenericControl divBottom = new HtmlGenericControl("div");
                divBottom.Attributes.Add("class", "sidebarbottom");
                Controls.Add(divBottom);
                //obj.Controls.Add(divBottom);
                //Controls.Add(obj);
            }
        }
    }
}
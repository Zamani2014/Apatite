//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MyWebPagesStarterKit.Controls
{
    public abstract class SectionControlBaseClass : UserControl
    {
        /// <summary>
        /// Indicates whether a section control 
        /// </summary>
        public abstract bool HasAdminView
        {
            get;
        }
        public abstract string InfoUrl
        {
            get;
        }
        public abstract ISection Section
        {
            set;
            get;
        }

        public ViewMode ViewMode
        {
            get
            {
                object o = ViewState["ViewMode"];
                if (o != null)
                    return (ViewMode)o;
                else
                    return ViewMode.Readonly;
            }
            set
            {
                ViewState["ViewMode"] = value;
            }
        }


        # region Section Rss Methods
        public bool RssCapable(Type t)
        {
            return (typeof(ISidebarObject).IsAssignableFrom(t));
        }

        /// <summary>
        /// Renders an RSS button to display at the end of the control
        /// </summary>
        /// <param name="page"></param>
        protected void DisplayRssButton(WebPage page)
        {
            if (Request.QueryString["pg"] != null && WebSite.GetInstance().EnableSectionRss)
            {
                HyperLink lnkRss = new HyperLink();
                Image img = new Image();
                img.Width = Unit.Pixel(1);
                img.ImageUrl = ResolveUrl("~/Images/spacer.gif");
                img.Height = Unit.Pixel(20);
                
                lnkRss.NavigateUrl = ResolveUrl("~/SectionRss.ashx?psid=" + Sidebar.CreatePageSectionKey(Section.SectionId, page.PageId));
                lnkRss.ImageUrl = ResolveUrl("~/Images/rss.gif");
                lnkRss.ToolTip = "Rss";
                lnkRss.Text = "Rss";
                Controls.AddAt(Controls.Count, lnkRss);
                Controls.AddAt(Controls.Count, img);

            }
        }

        /// <summary>
        ///  header-tag for rss detection in the browswer
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected HtmlLink MetaLink(WebPage page)
        {
            HtmlLink link = new HtmlLink();
            string sURL = "http://" + Request.Url.Authority + ResolveUrl("~/SectionRss.ashx?psid=" + Sidebar.CreatePageSectionKey(Section.SectionId, page.PageId));
            link.Attributes.Add("rel", "alternate");
            link.Attributes.Add("type", "application/rss+xml");
            link.Attributes.Add("title", page.Title + ": " + Section.GetType().Name.ToString());
            link.Attributes.Add("href", sURL);
            return link;
        }
        #endregion

        #region Section OPML Methods
        public bool OpmlCapable(Type t)
        {
            return (typeof(ISidebarObject).IsAssignableFrom(t));
        }

        /// <summary>
        /// Renders an RSS button to display at the end of the control
        /// </summary>
        /// <param name="page"></param>
        protected void DisplayOPMLButton(WebPage page)
        {
            if (Request.QueryString["pg"] != null && WebSite.GetInstance().EnableSectionRss)
            {
                HyperLink lnkOPML = new HyperLink();
                Image img = new Image();
                img.Width = Unit.Pixel(1);
                img.ImageUrl = ResolveUrl("~/Images/spacer.gif");
                img.Height = Unit.Pixel(20);

                lnkOPML.NavigateUrl = ResolveUrl("~/SectionOpml.ashx?psid=" + Sidebar.CreatePageSectionKey(Section.SectionId, page.PageId));
                lnkOPML.ImageUrl = ResolveUrl("~/Images/opml.png");
                lnkOPML.ToolTip = "OPML";
                lnkOPML.Text = "OPML";
                Controls.AddAt(Controls.Count, lnkOPML);
                Controls.AddAt(Controls.Count, img);
            }
        }
        #endregion


    }
}
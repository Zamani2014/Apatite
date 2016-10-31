//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit;
using System.IO;
using System.Text;
using System.Globalization;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Utility class to edit the web.sitemap file in the App_Data folder. If this class is instanciated for the first time and no web.sitemap file is present, a new one is generated.
    /// </summary>
    public class SitemapEditor
    {
        private XmlDocument _sitemap;
        private XmlNamespaceManager _nsManager;

        /// <summary>
        /// Creates a instance of SitemapEditor. This class uses caching to reduce filesystem operations.
        /// </summary>
        public SitemapEditor()
        {
            object o = HttpContext.Current.Cache["Sitemap"];
            if (o != null)
            {
                _sitemap = (XmlDocument)o;
            }
            else
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/Web.sitemap")))
                {
                    _sitemap = new XmlDocument();
                    _sitemap.Load(HttpContext.Current.Server.MapPath("~/App_Data/Web.sitemap"));
                }
                else
                {
                    _sitemap = createInitialSitemap();
                    Save();
                }
                HttpContext.Current.Cache.Insert("Sitemap", _sitemap);
            }
            
            _nsManager = new XmlNamespaceManager(_sitemap.NameTable);
            _nsManager.AddNamespace("sm", _sitemap.DocumentElement.NamespaceURI);
        }

        /// <summary>
        /// Checks, if there are subnodes present for the given page
        /// </summary>
        /// <param name="pageId">Id of the page to check for subnodes</param>
        /// <returns>true, if at leas one subnode was found, otherwise false</returns>
        public bool HasChildNodes(string pageId)
        {
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", pageId), _nsManager);
            return ((node != null) && (node.HasChildNodes));
        }

        /// <summary>
        /// Persists the data to the web.sitemap file
        /// </summary>
        public void Save()
        {
            _sitemap.Save(HttpContext.Current.Server.MapPath("~/App_Data/Web.sitemap"));
            HttpContext.Current.Cache.Insert("Sitemap", _sitemap);
        }

        /// <summary>
        /// Moves a node down in the sitemap
        /// </summary>
        /// <param name="pageId">Id of the page to move down</param>
        public void MoveDown(string pageId)
        {
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", pageId), _nsManager);
            if ((node != null) && (node.NextSibling != null))
            {
                node.ParentNode.InsertAfter(node, node.NextSibling);
            }
        }

        /// <summary>
        /// Moves a node up in the sitemap
        /// </summary>
        /// <param name="pageId">Id of the page to move up</param>
        public void MoveUp(string pageId)
        {
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", pageId), _nsManager);
            if (
                (node != null) &&
                (node.PreviousSibling != null) &&
                (node.PreviousSibling.Attributes["pageId"] != null)
                )
            {
                node.ParentNode.InsertBefore(node, node.PreviousSibling);
            }
        }

        /// <summary>
        /// Moves a page a level down (makes it a sibling of the preceding node)
        /// </summary>
        /// <param name="pageId">Id of the page to move a level down</param>
        public void MoveLevelUp(string pageId)
        {
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", pageId), _nsManager);
            if (
                (node != null) &&
                (node.ParentNode != null) &&
                (node.ParentNode.ParentNode != null) &&
                (node.ParentNode.ParentNode.Name == "siteMapNode")
                )
            {
                node.ParentNode.ParentNode.InsertAfter(node, node.ParentNode);
            }
        }

        /// <summary>
        /// Moves a page a level up (puts it on the same level as its ParentNode)
        /// </summary>
        /// <param name="pageId">Id of the page to move a level up</param>
        public void MoveLevelDown(string pageId)
        {
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", pageId), _nsManager);
            if (
                (node != null) &&
                (node.ParentNode != null) &&
                (node.ParentNode.Name == "siteMapNode") &&
                (node.PreviousSibling != null) &&
                (node.PreviousSibling.Attributes["pageId"] != null)
                )
            {
                node.PreviousSibling.AppendChild(node);
            }
        }

        /// <summary>
        /// Inserts the given page as a new node in the sitemap (at the very bottom)
        /// </summary>
        /// <param name="page">WebPage to insert</param>
        public void InsertPage(WebPage page, string previousPageId)
        {
            //create the new node
            XmlNode node = _sitemap.CreateNode(XmlNodeType.Element, "siteMapNode", _sitemap.DocumentElement.NamespaceURI);

            //assign title attribute (displayed text in the navigation)
            //if no NavigationName is specified, the Title of the page is used as node-title
            XmlAttribute title = _sitemap.CreateAttribute("title");
            if (page.NavigationName != string.Empty)
                title.Value = page.NavigationName;
            else
                title.Value = page.Title;
            node.Attributes.Append(title);

            //assign the url attribute (link to the page)
            //if a virtual path is specified for the page, this path is written as url, otherwise a link to "~/default.aspx?pg=..." is made with the page's id as querystring parameter
            XmlAttribute url = _sitemap.CreateAttribute("url");
            if (page.VirtualPath != string.Empty)
                url.Value = page.VirtualPath.ToLower();
            else
                url.Value = string.Format("~/default.aspx?pg={0}", page.PageId);
            node.Attributes.Append(url);

            //assign the visible attribute (specifies if the page is visible for users)
            XmlAttribute visible = _sitemap.CreateAttribute("visible");
            visible.Value = page.Visible.ToString();
            node.Attributes.Append(visible);

            //assign the pageId attribute (used to refind the page by id)
            XmlAttribute pageId = _sitemap.CreateAttribute("pageId");
            pageId.Value = page.PageId;
            node.Attributes.Append(pageId);

            //add the new node right after the selected page
            if (previousPageId != string.Empty)
            {
                XmlNode previousNode = _sitemap.DocumentElement.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", previousPageId), _nsManager);
                if (previousNode != null)
                {
                    previousNode.ParentNode.InsertAfter(node, previousNode);
                }
            }
            else
            {
                _sitemap.DocumentElement.FirstChild.AppendChild(node);
            }
        }

        /// <summary>
        /// Updates a sitemap-entry of a page
        /// </summary>
        /// <param name="page">WebPage for which to update the node</param>
        public void UpdatePage(WebPage page)
        {
            //find the node
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", page.PageId), _nsManager);
            if (node != null)
            {
                //update title attribute
                if (node.Attributes["title"] == null)
                    node.Attributes.Append(_sitemap.CreateAttribute("title"));
                if (page.NavigationName != string.Empty)
                    node.Attributes["title"].Value = page.NavigationName;
                else
                    node.Attributes["title"].Value = page.Title;

                //update url attribute
                if (node.Attributes["url"] == null)
                    node.Attributes.Append(_sitemap.CreateAttribute("url"));
                if (page.VirtualPath != string.Empty)
                    node.Attributes["url"].Value = page.VirtualPath.ToLower();
                else
                    node.Attributes["url"].Value = string.Format("~/default.aspx?pg={0}", page.PageId);

                //update visible attribute
                if (node.Attributes["visible"] == null)
                    node.Attributes.Append(_sitemap.CreateAttribute("visible"));
                node.Attributes["visible"].Value = page.Visible.ToString();
            }
        }

        /// <summary>
        /// Updates all administration page titles (when changing the application language)
        /// </summary>
        /// <param name="page">WebPage for which to update the node</param>
        public void UpdateAdministrationTitles(System.Globalization.CultureInfo culture)
        {
            XmlNode node;

            Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Administration",culture);
            //find the node default.aspx
            node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@url='{0}']", "~/administration/default.aspx"), _nsManager);
            if (node != null)
            {
                //update title attribute
                if (Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Administration", culture) != string.Empty)
                    node.Attributes["title"].Value = Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Administration", culture);
            }
            //find the node website.aspx
            node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@url='{0}']", "~/administration/website.aspx"), _nsManager);
            if (node != null)
            {
                //update title attribute
                if (Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_CMSSetup", culture) != string.Empty)
                    node.Attributes["title"].Value = Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_CMSSetup", culture);
            }
            //find the node sections.aspx
            node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@url='{0}']", "~/administration/sections.aspx"), _nsManager);
            if (node != null)
            {
                //update title attribute
                if (Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Sections", culture) != string.Empty)
                    node.Attributes["title"].Value = Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Sections", culture);
            }
            //find the node navigation.aspx
            node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@url='{0}']", "~/administration/navigation.aspx"), _nsManager);
            if (node != null)
            {
                //update title attribute
                if (Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_SectionsPages", culture) != string.Empty)
                    node.Attributes["title"].Value = Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_SectionsPages", culture);
            }
            //find the node users.aspx
            node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@url='{0}']", "~/administration/users.aspx"), _nsManager);
            if (node != null)
            {
                //update title attribute
                if (Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Management", culture) != string.Empty)
                    node.Attributes["title"].Value = Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Management", culture);
            }
        }

        /// <summary>
        /// Removes a page from the sitemap
        /// </summary>
        /// <param name="pageId">Id of the WebPage to remove from the sitemap</param>
        public void DeletePage(string pageId)
        {
            XmlNode node = _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@pageId='{0}']", pageId), _nsManager);
            node.ParentNode.RemoveChild(node);
        }

        /// <summary>
        /// Searches for a SiteMapNode with the given virtual path as url. Returns null if the virtual path does not match a node.
        /// </summary>
        public XmlNode FindNodeByVirtualPath(string virtualPath)
        {
            return _sitemap.SelectSingleNode(string.Format("//sm:siteMapNode[@url='{0}']", virtualPath), _nsManager);
        }

        /// <summary>
        /// Creates the initial sitemap with all the necessary nodes for the administration-pages
        /// </summary>
        /// <returns>SiteMap as XmlDocument</returns>
        private XmlDocument createInitialSitemap()
        {
            XmlDocument sitemap = new XmlDocument();
            StringBuilder xml = new StringBuilder();
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            xml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml.Append("<siteMap xmlns=\"http://schemas.microsoft.com/AspNet/SiteMap-File-1.0\">");
            xml.Append("  <siteMapNode>");
            xml.Append("    <siteMapNode url=\"~/administration/default.aspx\" title=\"" + Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Administration",culture) + "\">");
            xml.Append("      <siteMapNode url=\"~/administration/website.aspx\" title=\"" + Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_CMSSetup",culture) + "\"/>");
            xml.Append("      <siteMapNode url=\"~/administration/sections.aspx\" title=\"" + Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Sections",culture) + "\"/>");
            xml.Append("      <siteMapNode url=\"~/administration/navigation.aspx\" title=\"" + Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_SectionsPages",culture) + "\"/>");
            xml.Append("      <siteMapNode url=\"~/administration/users.aspx\" title=\"" + Resources.StringsRes.ResourceManager.GetString("cls_SitemapEditor_Management", culture) + "\"/>");
            xml.Append("    </siteMapNode>");
            xml.Append("  </siteMapNode>");
            xml.Append("</siteMap>");
            
            sitemap.LoadXml(xml.ToString());
            return sitemap;
        }
    }
}

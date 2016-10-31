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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MyWebPagesStarterKit.Providers
{
    /// <summary>
    /// Specialized XmlSiteMapProvider that implements some special behaviour
    /// </summary>
    public class CustomXmlSitemapProvider : XmlSiteMapProvider
    {
        /// <summary>
        /// Special IsAccessibleToUser method (see code to get further explanations)
        /// </summary>
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            //if SecurityTrimming is disabled, ALL nodes are accessible by everyone
            if (!SecurityTrimmingEnabled)
                return true;

            //Administrators have permissions on all nodes, so we can always return true
            if ((context.User != null) && (context.User.Identity.IsAuthenticated) && context.User.IsInRole(RoleNames.Administrators.ToString()))
                return true;

            //The RootNode must be accessible for everyone
            if (node.ParentNode == null) //if there is no parent-node, the node must be the root node
                return true;

            //special behaviour for nodes that have a pageId attribute (content-pages)
            if (node["pageId"] != null)
            {
                //first we create the WebPage for the node
                WebPage page = new WebPage(node["pageId"]);
                //now check, if we have a logged in user (in role Users)
                if ((context.User != null) && context.User.Identity.IsAuthenticated)
                {
                    //authenticated power users can see the page, when the page is visible or marked as editable for power users
                    if(context.User.IsInRole(RoleNames.PowerUsers.ToString()))
                    {
                        return (page.Visible || page.EditPowerUser);
                    }
                    //normal users can see the page when it is visible
                    else
                    {
                        return page.Visible;
                    }
                }
                else
                {
                    //anonymous user -> we must check if the page is visible, and if it allows anonymous access
                    return ((page.Visible) && (page.AllowAnonymousAccess));
                }
            }
            else
            {
                //if there is no pageId attribute on the node, it is a administration-node
                //therefore it must not be shown to non-admin users (admin-users are handled further up)
                return false;
            }
        }

        /// <summary>
        /// Find a SiteMapNode by a give pageId. This method is used recursive.
        /// </summary>
        /// <param name="key">The pageId</param>
        /// <param name="node">Starting node</param>
        /// <returns></returns>
        public SiteMapNode FindSiteMapNodeFromPageId(string key, SiteMapNode node)
        {
            //Get all ChildNodes of the starting node
            foreach (SiteMapNode n in GetChildNodes(node))
            {
                //if tha pageId attribute matches the key, we're done and can return the found node
                if (n["pageId"] == key)
                {
                    return n;
                }
                //otherwise we try to find the SiteMapNode in the childnodes of the current node
                else
                {
                    SiteMapNode candidate = FindSiteMapNodeFromPageId(key, n);
                    if (candidate != null)
                        return candidate;
                }
            }
            //if no node with the given pageId is found, we return null
            return null;
        }

        /// <summary>
        /// Resolve eventhandler to get the SiteMapNode corresponding to the currently displayed page
        /// </summary>
        public static SiteMapNode Resolve(object sender, SiteMapResolveEventArgs e)
        {
            //check, if a CurrentNode could already be determined by the default XmlSiteMapProvider (base class).
            //if this is not the case and we have a pg-element in the querystring, we must call the FindSiteMapNodeFromPageId-method to find the CurrentNode by pageId
            //this eventhandler is registered in global.asax, in the "Application_Start" Event
            if ((e.Provider.CurrentNode == null) && (e.Context.Request.QueryString["pg"] != null))
            {
                return ((CustomXmlSitemapProvider)e.Provider).FindSiteMapNodeFromPageId(e.Context.Request.QueryString["pg"], SiteMap.RootNode);
            }
            else
            {
                return e.Provider.CurrentNode;
            }
        }
    }
}

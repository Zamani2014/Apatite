<%@ WebHandler Language="C#" Class="DownloadHandler" %>

//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web;
using System.IO;
using MyWebPagesStarterKit;

/// <summary>
/// This handler serves files from the "~/App_Data/_Downloads/" folder (which is by default not accessible over the web).
/// </summary>
public class DownloadHandler : IHttpHandler
{
    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            //get the sectionId
            string section = context.Request.QueryString["section"];
            //get the filename
            string file = context.Request.QueryString["file"];
            //build the path
            string path = context.Server.MapPath(string.Format("~/App_Data/_Downloads/{0}/{1}", section, file));

            //FIX: Never allow the download of a .config file, also check for ".." in the path to prevent attackers from navigating to a different directory
            if (path.Contains(".config") || path.Contains("..") || path.Contains("%2E%2E"))
            {
                throw new Exception("Access forbidden.");
            }

            //get the pageId for the data security
            string pageId = context.Request.QueryString["pg"];

            //data security: controlling file access ({0} is placeholder for pageId in "Image Properties in FCKEditor)
            WebPage _page = new WebPage(pageId);
            //logged in
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                if (_page.Visible == false)
                {
                    //power users: page not visible and not marked as editable for power users
                    if (context.User.IsInRole(RoleNames.PowerUsers.ToString()) && _page.EditPowerUser == false)
                        throw new Exception("User is not allowed to view this file");

                    //normal users: page is not visible
                    if (context.User.IsInRole(RoleNames.Users.ToString()))
                        throw new Exception("User is not allowed to view this file");
                }
            }
            else //logged out
            {
                //page is not visible or not accessible for anonymous visitors
                if (_page.AllowAnonymousAccess == false || _page.Visible == false)
                    throw new Exception("User is not allowed to view this file");
            }

            if (File.Exists(path))
            {
                context.Response.Clear();
                context.Response.AddHeader("Pragma", "public");
                context.Response.AddHeader("Expires", "0");
                context.Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
                context.Response.AddHeader("Content-Type", "application/force-download");
                context.Response.AddHeader("Content-Type", "application/octet-stream");
                context.Response.AddHeader("Content-Type", "application/download");
                context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", file));
                context.Response.AddHeader("Content-Transfer-Encoding", "binary");
                context.Response.AddHeader("Content-Length", new FileInfo(path).Length.ToString());

                context.Response.WriteFile(path);
            }
        }
        catch
        {
            context.Response.Clear();
            context.Response.End();
        }
    }
    #endregion
}

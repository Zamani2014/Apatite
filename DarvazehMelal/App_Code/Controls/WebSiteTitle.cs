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

namespace MyWebPagesStarterKit.Controls
{
    public class WebSiteTitle : Control
    {
        public WebSiteTitle() { }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(WebSite.GetInstance().WebSiteTitle);
        }
    }
}
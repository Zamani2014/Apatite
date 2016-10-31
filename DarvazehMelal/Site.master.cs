//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using MyWebPagesStarterKit;
using System.Web.UI.HtmlControls;
using System;
using GoogleMap;


public partial class Site : System.Web.UI.MasterPage
{
	protected override void OnLoad(System.EventArgs e)
	{
		// This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
		// All webpages displaying an ASP.NET menu control must inherit this class.
		if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
			Page.ClientTarget = "uplevel";

		base.OnLoad(e);

        //Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        //Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        //Response.AppendHeader("Expires", "0"); // Proxies.

        Response.ClearHeaders();
        Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
        Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1 
        Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1 
        Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1 
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0 
        Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.0
	}

	protected override void OnPreRender(System.EventArgs e)
	{
		//if configured, render out the metatag that sets IE8 to IE7 compatibility mode
		//this meta-tag MUST BE THE FIRST tag inside the head-element of the page, otherwise it won't have any effect
		if (WebSite.GetInstance().EnableIE8CompatibilityMetatag)
		{
			HtmlMeta ie8compatMeta = new HtmlMeta();
			ie8compatMeta.HttpEquiv = "X-UA-Compatible";
			ie8compatMeta.Content = "IE=7";
			Page.Header.Controls.AddAt(0, ie8compatMeta); 
		}

		base.OnPreRender(e);
	}
}

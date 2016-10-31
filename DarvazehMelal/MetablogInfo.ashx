<%@ WebHandler Language="C#" Class="MetablogInfo" %>

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

public class MetablogInfo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
	{
		if (!string.IsNullOrEmpty(context.Request.QueryString["type"]) && !string.IsNullOrEmpty(context.Request.QueryString["blogID"]))
		{
			string websiteUrl = MyWebPagesStarterKit.WebSite.GetInstance().GetFullWebsiteUrl(context);

			switch (context.Request.QueryString["type"].ToLower())
			{
				case "rsd":
					context.Response.ClearHeaders();
					context.Response.ClearContent();
					context.Response.Clear();
					context.Response.ContentType = "application/rsd+xml";
					string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
					<rsd xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" version=""1.0"" xmlns=""http://archipelago.phrasewise.com/rsd"">
						<service xmlns="""">
							<engineName>MyWebPagesStarterkit Blog</engineName>
							<engineLink>http://mywebpagesstarterkit.codeplex.com/</engineLink>
							<homePageLink>{0}</homePageLink>
							<apis>
								<api name=""MetaWeblog"" blogID=""{1}"" preferred=""true"" apiLink=""{0}/Metablog.ashx""/>
							</apis>
						</service>
					</rsd>";
					context.Response.Write(string.Format(xml, websiteUrl, context.Request.QueryString["blogID"]));
					break;
				case "wlwmanifest":
					context.Response.Clear();
					context.Response.ContentType = "application/wlwmanifest+xml";
					string manifest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
					<manifest xmlns=""http://schemas.microsoft.com/wlw/manifest/weblog\"">
						<weblog>
							<ServiceName>MyWebPagesStarterKit Metaweblog</ServiceName>
						</weblog>
						<options>
							<clientType>Metaweblog</clientType>
							<supportsPostAsDraft>No</supportsPostAsDraft>
							<supportsCustomDate>No</supportsCustomDate>
							<supportsCategoriesInline>No</supportsCategoriesInline>
							<supportsEmptyTitles>No</supportsEmptyTitles>
							<supportsScripts>No</supportsScripts>
							<supportsEmbeds>No</supportsEmbeds>
						</options>
						<views>
							<default>WebLayout</default>
							<view type=""WebLayout"" src=""{0}/MetablogPreview.aspx"" />
							<!--view type=""WebPreview"" src=""webpreview.htm"" /-->
						</views>
					</manifest>";
					context.Response.Write(string.Format(manifest, websiteUrl));
					break;
			}
		}
	}
 
    public bool IsReusable { get { return true; } }
}
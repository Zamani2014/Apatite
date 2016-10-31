<%@ WebHandler Language="C#" Class="GalleryData" %>

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
using System.Data;
using System.Xml;
using MyWebPagesStarterKit;

public class GalleryData : IHttpHandler {

	private const string cThumbnailUrl = "{0}ImageHandler.ashx?pg={1}&amp;section={2}&amp;image={3}&amp;height=100&amp;width=100";
	private const string cFullsizeUrl = "{0}ImageHandler.ashx?pg={1}&amp;section={2}&amp;image={3}&UploadedFile=true";
    
    public void ProcessRequest (HttpContext context) {
		HttpResponse response = context.Response;

		// Set the content-type
		response.ContentType = "text/xml";
		response.ContentEncoding = System.Text.Encoding.UTF8;

		Gallery gallery = new Gallery(context.Request.QueryString["section"]);
		XmlDocument xml = new XmlDocument();
		
		xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><data transition=\"CrossFadeTransition\" startalbumindex=\"0\"><album title=\"\" description=\"\" source=\"\"></album></data>");
		XmlNode album = xml.SelectSingleNode("//album");
		
		foreach(DataRow image in gallery.GetGalleryEntries().Rows)
		{
			string baseUrl = WebSite.GetInstance().GetFullWebsiteUrl(context);
			string filename = image["Filename"].ToString();
			string imageUrl = string.Format(cFullsizeUrl, baseUrl, context.Request.QueryString["pg"], gallery.SectionId, HttpUtility.UrlEncode(filename));
			string thumbnailUrl = string.Format(cThumbnailUrl, baseUrl, context.Request.QueryString["pg"], gallery.SectionId, HttpUtility.UrlEncode(filename));
			
			XmlNode slide = xml.CreateNode(XmlNodeType.Element, "slide", xml.NamespaceURI);
			
			XmlAttribute title = xml.CreateAttribute("title");
			title.Value = image["Title"].ToString();
			slide.Attributes.Append(title);

			XmlAttribute description = xml.CreateAttribute("description");
			description.Value = image["Comment"].ToString();
			slide.Attributes.Append(description);

			XmlAttribute source = xml.CreateAttribute("source");
			source.Value = imageUrl;
			slide.Attributes.Append(source);

			XmlAttribute thumbnail = xml.CreateAttribute("thumbnail");
			thumbnail.Value = imageUrl;
			slide.Attributes.Append(thumbnail);

			album.AppendChild(slide);
		}

		response.Write(xml.OuterXml);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
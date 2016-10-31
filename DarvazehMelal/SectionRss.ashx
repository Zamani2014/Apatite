<%@ WebHandler Language="C#" Class="SectionRss" %>

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
using System.Xml;
using System.Text;
using MyWebPagesStarterKit;

public class SectionRss : IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        HttpResponse response = context.Response;
        
        // Set the content-type
        response.ContentType = "text/xml";
        response.ContentEncoding = Encoding.UTF8;

        string pageSectionKey = context.Request.QueryString["psid"];
        if (pageSectionKey != null) {
            //if (context.Cache["RssFeed" + pageSectionKey] == null || context.Request["Refresh"] == "1")
            //{
                string feed = GetFeedXml(pageSectionKey);
                string method = "http";
                string port = string.Empty;
                if (context.Request.ServerVariables["SERVER_PORT_SECURE"] == "1")
                    method = "https";

                if (context.Request.ServerVariables["SERVER_PORT"] != string.Empty)
                    port = ":" + context.Request.ServerVariables["SERVER_PORT"];
                
                feed = feed.Replace("~", method + "://" + context.Request.ServerVariables["SERVER_NAME"] + port + context.Request.ApplicationPath);
                
                // save the string in the cache (cache for 1.5 hours)
                context.Cache.Insert("RssFeed" + pageSectionKey, feed, null, DateTime.Now.AddHours(1.5), TimeSpan.Zero);
            //}
            response.Write(context.Cache["RssFeed" + pageSectionKey]);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string GetFeedXml(string pageSectionId)
    {
        System.IO.StringWriter rssHtml = new System.IO.StringWriter();
        XmlTextWriter rss = new XmlTextWriter(rssHtml);

        if (pageSectionId != null)
        {
            string sectionId = string.Empty;
            string pageId = string.Empty;
            Sidebar.DecodePageSectionKey(pageSectionId, ref sectionId, ref pageId);

            rss.WriteStartElement("rss");
            rss.WriteAttributeString("version", "2.0");

            SectionLoader oSection = SectionLoader.GetInstance();
            ISection section = (ISection)oSection.LoadSection(sectionId);
            if (section != null)
                Sidebar.GetSectionRss((XmlWriter)rss, section, pageId, WebSite.GetInstance().WebSiteTitle);
        }
        
        rss.Close();

        return rssHtml.ToString();
    }

}
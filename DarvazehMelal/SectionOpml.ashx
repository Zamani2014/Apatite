<%@ WebHandler Language="C#" Class="SectionOpml" %>

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
using System.Collections.Generic;
using MyWebPagesStarterKit;

public class SectionOpml : IHttpHandler
{
    /// <summary>
    /// this handler is only working with the blogroll section
    /// the opml structure is meeting the requirements of OPML 2.0 (http://www.opml.org/)
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest (HttpContext context) {
        HttpResponse response = context.Response;
        
        // Set the content-type
        response.ContentType = "text/xml";
        response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");

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
                context.Cache.Insert("OpmlFeed" + pageSectionKey, feed, null, DateTime.Now.AddHours(1.5), TimeSpan.Zero);
            //}
                response.Write(context.Cache["OpmlFeed" + pageSectionKey]);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string GetFeedXml(string pageSectionId)
    {
        //Creating XML-Document:
        XmlDocument opml = new XmlDocument();

        //Xml Declaration:
        opml.AppendChild(opml.CreateXmlDeclaration("1.0", "ISO-8859-1", null));
        
        //root element
        XmlElement rootElem = opml.CreateElement("opml");
        rootElem.SetAttribute("version", "2.0");
        opml.AppendChild(rootElem);
        
        if (pageSectionId != null)
        {
            string sectionId = string.Empty;
            string pageId = string.Empty;
            Sidebar.DecodePageSectionKey(pageSectionId, ref sectionId, ref pageId);
            
            //load blogroll and elements
            SectionLoader oSection = SectionLoader.GetInstance();
            Blogroll roll = (Blogroll)oSection.LoadSection(sectionId);
            ChannelData elements = roll.GetOpmlElements(pageId);
            
            //append head and header information
            XmlElement headElem = opml.CreateElement("head");
            rootElem.AppendChild(headElem);
            
            //title tag
            string title = roll.getConfiguration("Title").ToString() == string.Empty ? "Blogroll" : roll.getConfiguration("Title").ToString();
            XmlElement titleElem = opml.CreateElement("title");
            titleElem.AppendChild(opml.CreateTextNode(title));
            headElem.AppendChild(titleElem);
            
            //created and modified tag (convert date to GMT time and format as RFC 822)
            DateTime gmtCreated = System.Convert.ToDateTime(roll.getConfiguration("Created")).ToUniversalTime();
            string created = String.Format("{0:r}", gmtCreated);
            XmlElement createdElem = opml.CreateElement("dateCreated");
            createdElem.AppendChild(opml.CreateTextNode(created));
            headElem.AppendChild(createdElem);

            DateTime gmtModified = System.Convert.ToDateTime(roll.getConfiguration("Updated")).ToUniversalTime();
            string modified = String.Format("{0:r}", gmtCreated);
            XmlElement modifiedElem = opml.CreateElement("dateModified");
            modifiedElem.AppendChild(opml.CreateTextNode(modified));
            headElem.AppendChild(modifiedElem);

            //Owner tag
            if (roll.getConfiguration("Owner").ToString() != string.Empty)
            {
                XmlElement ownerElem = opml.CreateElement("ownerName");
                ownerElem.AppendChild(opml.CreateTextNode(roll.getConfiguration("Owner").ToString()));
                headElem.AppendChild(ownerElem);
            }

            //OwnerEmail tag
            if (roll.getConfiguration("OwnerEmail").ToString() != string.Empty)
            {
                XmlElement ownerEmailElem = opml.CreateElement("ownerEmail");
                ownerEmailElem.AppendChild(opml.CreateTextNode(roll.getConfiguration("OwnerEmail").ToString()));
                headElem.AppendChild(ownerEmailElem);
            }
            
            //append body
            XmlElement bodyElem = opml.CreateElement("body");
            rootElem.AppendChild(bodyElem);
            
            // Iterate through all blogroll links and render each element to a XML string
            foreach (Dictionary<string, string> item in elements.ChannelItems)
            {
                //append outline

                if (item["xmlUrl"] != string.Empty)
                {
                    XmlElement outlineElem = opml.CreateElement("outline");
                    outlineElem.SetAttribute("text", item["text"]);
                    outlineElem.SetAttribute("description", item["description"]);
                    outlineElem.SetAttribute("type", item["type"]);
                    outlineElem.SetAttribute("xmlUrl", item["xmlUrl"]);
                    bodyElem.AppendChild(outlineElem);
                }
            }
        }
        return opml.OuterXml;
    }
}
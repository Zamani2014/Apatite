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
using MyWebPagesStarterKit;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Manages sidebar sections including adding/removing them and returns sidebar rss data feed
    /// </summary>
    [Serializable]
    public class Sidebar : MyWebPagesStarterKit.Persistable<Sidebar.SidebarData>
    {
        #region Attributes
        /// <summary>
        /// Holds static reference to one and only sidebar instance
        /// </summary>
        private static Sidebar _instance;
        #endregion

        #region Private Singleton Methods
        /// <summary>
        /// Private constructor for the singleton instance
        /// </summary>
        private Sidebar() : base()
        {
            
        }

        /// <summary>
        /// Gets the one and only instance of sidebar, as it is a singleton
        /// </summary>
        /// <returns>Singleton sidebar instance</returns>
        public static Sidebar GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Sidebar();
                if (File.Exists(HttpContext.Current.Server.MapPath(_instance.GetDataFilename())))
                {
                    _instance.LoadData();
                }
            }
            return _instance;
        }
        #endregion

        /// <summary>
        /// Adds a section to the sidebar for displaying
        /// </summary>
        /// <param name="sectionId">ID of the section to add</param>
        /// <param name="senderPageId">Page Id the section belongs to (for permissions)</param>
        /// <returns>True if section was successfully added</returns>
        public bool AddSection(string sectionId, string senderPageId)
        {
            if (!ContainsSection(sectionId, senderPageId))
            {
                _data.SidebarSections.Add(CreatePageSectionKey(sectionId, senderPageId));
                SaveData();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a section from the sidebar
        /// </summary>
        /// <param name="sectionId">ID of the section to remove</param>
        /// <param name="senderPageId">Page the section belongs to (for permissions)</param>
        /// <returns>Returns true if section could successfully be added.</returns>
        public bool RemoveSection(string sectionId, string senderPageId) 
        {
            if (ContainsSection(sectionId, senderPageId))
            {
                _data.SidebarSections.Remove(CreatePageSectionKey(sectionId, senderPageId));
                this.SaveData();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the sections from the sidebar belonging to a specific page
        /// </summary>
        /// <param name="senderPageId">Page the section belongs to (for permissions)</param>
        /// <returns>Returns true if section could successfully be added.</returns>
        public bool RemoveSection(string senderPageId)
        {
            string sectionId = null;
            string pageId = null;
            bool SectionDeleted = false;
            ArrayList sectionIds = new ArrayList();

            foreach (string id in _data.SidebarSections)
            {
                string[] pageSectionID = id.Split('~');
                sectionId = pageSectionID[1];
                pageId = pageSectionID[0];

                if (pageId == senderPageId)
                {
                    sectionIds.Add(sectionId);
                    SectionDeleted = true;
                }
            }
            if (sectionIds.Count > 0)
            {
                for (int i = 0; i < sectionIds.Count; i++)
                {
                    _data.SidebarSections.Remove(CreatePageSectionKey(sectionIds[0].ToString(), senderPageId));
                }
                this.SaveData();
            }
            return SectionDeleted;
        }

        /// <summary>
        /// Checks if the section is already in the sidebar
        /// </summary>
        /// <param name="sectionId">ID of the section to remove</param>
        /// <param name="senderPageId">Page the section belongs to (for permissions)</param>
        /// <returns>true if the section has been added to the sidebar</returns>
        public bool ContainsSection(string sectionId, string senderPageId)
        {
            return _data.SidebarSections.Contains(CreatePageSectionKey(sectionId, senderPageId));
        }

        /// <summary>
        /// Returns true if a page (a section always belongs to) would be visible to the user)
        /// </summary>
        /// <param name="ParentPage">Page the section belongs to</param>
        /// <param name="identity">Identity/User that requests page access</param>
        /// <returns>True if user is permitted </returns>
        public bool IsSectionPageViewable(WebPage ParentPage, System.Security.Principal.IIdentity identity)
        {
            return ParentPage.Visible && (ParentPage.AllowAnonymousAccess || identity.IsAuthenticated);
        }

        /// <summary>
        /// Returns file name for data file
        /// </summary>
        /// <returns>string containting path to sidebar data file</returns>
        protected override string GetDataFilename()
        {
            return "~/App_Data/Sidebar.config";
        }

        /// <summary>
        /// Delete function to remote data file - not implemented.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            throw new NotSupportedException("Sidebar cannot be deleted!");
        }

        /// <summary>
        /// Returns xml for Sidebar html control
        /// http://www.rssboard.org/rss-specification
        /// </summary>
        /// <param name="Webuser"></param>
        /// <returns></returns>
        public string GetRSSFeed(System.Security.Principal.IIdentity Webuser)
        {
            SectionLoader oSection = SectionLoader.GetInstance();
            ISection sidebarSection = null;
            string sectionId = null;
            string pageId = null;
            bool foundElements = false;
            ArrayList orphanedSections = new ArrayList();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CheckCharacters = true;

            StringWriter rssHtml = new StringWriter();
            XmlWriter rss = XmlWriter.Create(rssHtml, settings);

            rss.WriteStartDocument();
            rss.WriteStartElement("rss");
            rss.WriteAttributeString("version", "2.0");


            foreach (string id in _data.SidebarSections)
            {
                WebPage SectionPage = null;
                string[] pageSectionID = id.Split('~');
                sectionId = pageSectionID[1];
                pageId = pageSectionID[0];

                sidebarSection = oSection.LoadSection(pageSectionID[1]);
                // Delete section if it couldn't be loaded from the page
                if (sidebarSection == null)
                {
                    orphanedSections.Add(id);
                }
                else
                {
                    SectionPage = new WebPage(pageId);
                }
  
                // Check permissions of user
                if (sidebarSection != null && IsSectionPageViewable(SectionPage, Webuser) )
                {
                    if (GetSectionRss(rss, sidebarSection, pageId, string.Empty))
                        foundElements = true;
                }
            }

            rss.WriteEndDocument();
            rss.Close();
            
            foreach (string id in orphanedSections)
            {
                RemoveSection(id.Split('~')[0], id.Split('~')[1]);
            }

            if (foundElements)
                return rssHtml.ToString();
            else
                return string.Empty;
        }

        #region Static Helper Methods
        /// <summary>
        /// Adds rss items of a section to an xml writer
        /// No page permission check is being done here
        /// </summary>
        /// <param name="rss">XmlWriter to add rss to</param>
        /// <param name="section">Source Section to generate the feed for</param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public static bool GetSectionRss(XmlWriter rss, ISection section, string pageId, string titlePrefix)
        {
            if (section != null)
            {
                ChannelData SectionChannel = ((ISidebarObject)section).GetSidebarRss(pageId);
                // In whatever namespace the sections are, extract only the typename for the Title Resource ID
                string TypeName = section.GetType().ToString();
                string[] arrTypeName = TypeName.Split('.');
                
				/*string ResourceID = "ctl_" + arrTypeName[arrTypeName.GetLength(0) - 1] + "_RssTitle";

                if(SectionChannel.Title == string.Empty)
                    SectionChannel.Title = Resources.StringsRes.ResourceManager.GetString(ResourceID);
                if (TitlePrefix != string.Empty)
                    SectionChannel.Title = TitlePrefix + " " + SectionChannel.Title;*/

				if (string.IsNullOrEmpty(SectionChannel.Title))
				{
					if (titlePrefix != string.Empty)
						SectionChannel.Title = titlePrefix + " " + section.GetLocalizedSectionName();
					else
						SectionChannel.Title = section.GetLocalizedSectionName();
				}


                // generate feed and return wether items where found for this section
                return SectionChannel.GetXml(ref rss);
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Creates the unique sidebar key for a specific section
        /// </summary>
        /// <param name="sectionId">ID of the section to remove</param>
        /// <param name="senderPageId">Page the section belongs to (for permissions)</param>
        /// <returns>string ID</returns>
        public static string CreatePageSectionKey(string sectionId, string senderPageId)
        {
            return senderPageId + "~" + sectionId;
        }

        /// <summary>
        /// Decodes the unique page-section key
        /// </summary>
        /// <param name="pageSectionKey">Page Section Key</param>
        /// <param name="sectionId">string to fill in section Id</param>
        /// <param name="pageId">string to fill in page Id</param>
        public static void DecodePageSectionKey(string pageSectionKey, ref string sectionId, ref string pageId)
        {
            string[] pageSectionID = pageSectionKey.Split('~');
            sectionId = pageSectionID[1];
            pageId = pageSectionID[0];
        }
        #endregion

        /// <summary>
        /// Sidebar data class, containing sections to query and Title
        /// </summary>
        public class SidebarData 
        {
            public SidebarData()
            {
                SidebarSections = new List<string>();
                Title = "Unnamed sidebar";
            }

            // Title of this sidebar
            public string Title;
            // List of sections and the page they are on to display in the sidebar
            public List<string> SidebarSections;
        }
    }

    /// <summary>
    /// Represents one RSS channel containing several RSS news items
    /// </summary>
    public class ChannelData
    {
        public string Title = string.Empty;
        public string Link = string.Empty;
        public string Description = string.Empty;
        public string Language = string.Empty;
        public string PubDate = string.Empty;
        public string LastBuildDate = string.Empty;
        public string Docs = string.Empty;
        public string Generator = string.Empty;
        public string ManagingDirector = string.Empty;
        public string Webmaster = string.Empty;
       
        public List<Dictionary<string, string>> ChannelItems = new List<Dictionary<string, string>>();

        /// <summary>
        /// Adds channel XML to rss
        /// </summary>
        /// <param name="rss"></param>
        /// <returns>Wether items were present in the channel or not</returns>
        public bool GetXml(ref XmlWriter rss)
        {
            bool ItemsFound = false;
            bool BuildFullRSS = false;
            if (ChannelItems.Count > 0)
            {
                rss.WriteStartElement("channel");
                rss.WriteElementString("title",Title);
                if (BuildFullRSS)
                {
                    rss.WriteElementString("link", HttpContext.Current.Server.HtmlEncode(Link));
                    rss.WriteElementString("description", Description);
                    rss.WriteElementString("language", "");
                }

                // Iterate through all channels and render each element to an rss-conform XML string
                foreach (Dictionary<string, string> item in ChannelItems)
                {
                    rss.WriteStartElement("item");
                    foreach (string key in item.Keys)
                    {
                        if (key != null && item[key] != null)
                        {
                            rss.WriteStartElement(key);
                            rss.WriteCData(item[key]);
                            rss.WriteEndElement();
                        }
                    }
                    rss.WriteEndElement();
                    ItemsFound = true;
                }

                // close channel
                rss.WriteEndElement();
            }
            return ItemsFound;
        }
    }

    /// <summary>
    /// Defines RSS channel elements that are available at the moment
    /// </summary>
    public enum RssElements { title, link, description };
}
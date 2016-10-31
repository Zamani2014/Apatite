//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// This class encapsulates the logic and properties of a single page
    /// </summary>
    [Serializable]
    public class WebPage : Persistable<WebPage.WebPageData>
    {
        private string _pageId;

        /// <summary>
        /// Create a WebPage for a given id
        /// </summary>
        /// <param name="id">Guid of the WebPage to create as string</param>
        public WebPage(string id)
        {
            _pageId = id;
            LoadData();
        }

        /// <summary>
        /// Creates a new empty WebPage and generates a new guid for it
        /// </summary>
        public WebPage() : base()
        {
            _pageId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Guid of the WebPage as string
        /// </summary>
        public string PageId
        {
            get { return _pageId; }
        }

        /// <summary>
        /// Text of the navigation-node for this WebPage
        /// </summary>
        public string NavigationName
        {
            get { return _data.NavigationName; }
            set { _data.NavigationName = value; }
        }

        /// <summary>
        /// Flag to make the WebPage visible (if false, the WebPage does not appear in the navigation)
        /// </summary>
        public bool Visible
        {
            get { return _data.Visible; }
            set { _data.Visible = value; }
        }

        /// <summary>
        /// Flag to allow anonymous access to the WebPage (if false, only authenticated users in the Roles Administrators or Users can see the WebPage)
        /// </summary>
        public bool AllowAnonymousAccess
        {
            get { return _data.AllowAnonymousAccess; }
            set { _data.AllowAnonymousAccess = value; }
        }

        /// <summary>
        /// Flag to allow editing of the WebPage by a Power User (PowerUser Management)
        /// </summary>
        public bool EditPowerUser
        {
            get { return _data.EditPowerUser; }
            set { _data.EditPowerUser = value; }
        }

        /// <summary>
        /// Virtual path for the WebPage. This property is used for URL-Rewriting (see global.asax).
        /// You can specify a virtual path (like "~/MyCustomDir/MyCustomPage.aspx") under which the page can be accessed.
        /// </summary>
        public string VirtualPath
        {
            get { return _data.VirtualPath; }
            set { _data.VirtualPath = value; }
        }

        /// <summary>
        /// Title of the page (shown in the browser's title-bar and in bookmarks).
        /// </summary>
        public string Title
        {
            get { return _data.Title; }
            set { _data.Title = value; }
        }

        /// <summary> 
        /// SEO Tags management
        /// The meta tag description of the page. 
        /// </summary> 
        public string Description
        {
            get { return _data.Description; }
            set { _data.Description = value; }
        } 
        
        /// <summary> 
        /// SEO Tags management
        /// The meta tag description of the page. 
        /// </summary> 
        public string Keywords
        {
            get { return _data.Keywords; }
            set { _data.Keywords = value; }
        } 

        /// <summary>
        /// List of all section-ids contained in the WebPage
        /// </summary>
        public List<string> SectionIds
        {
            get { return _data.Sections; }
        }

        /// <summary>
        /// List of all sections contained in the WebPage
        /// </summary>
        public List<ISection> Sections
        {
            get 
            {
                List<ISection> retVal = new List<ISection>();
                foreach (string id in _data.Sections)
                {
                    ISection section = SectionLoader.GetInstance().LoadSection(id);
                    if (section != null)
                        retVal.Add(section);
                }
                return retVal; 
            }
        }

        /// <summary>
        /// Adds a section to the WebPage
        /// </summary>
        /// <param name="id">Id of the section to add</param>
        public void AddSection(string id)
        {
            _data.Sections.Add(id);
            SaveData();
        }

        /// <summary>
        /// Removes a section from the WebPage
        /// </summary>
        /// <param name="id">Id of the section to remove</param>
        public void RemoveSection(string id)
        {
            _data.Sections.Remove(id);
            SaveData();
        }

        /// <summary>
        /// Moves a section up (if it is not already at the top).
        /// </summary>
        /// <param name="id">Id of the section to move up</param>
        public void MoveSectionUp(string id)
        {
            int index = _data.Sections.IndexOf(id);
            if (index > 0)
            {
                string temp = _data.Sections[index - 1];
                _data.Sections[index - 1] = id;
                _data.Sections[index] = temp;
                SaveData();
            }
        }

        /// <summary>
        /// Moves a section down (if it is not already at the bottom).
        /// </summary>
        /// <param name="id">Id of the section to move down</param>
        public void MoveSectionDown(string id)
        {
            int index = _data.Sections.IndexOf(id);
            if (index < _data.Sections.Count -1)
            {
                string temp = _data.Sections[index + 1];
                _data.Sections[index + 1] = id;
                _data.Sections[index] = temp;
                SaveData();
            }
        }

        /// <summary>
        /// Path to the data-file of the WebPage
        /// </summary>
        /// <returns>Path to the data-file of the WebPage</returns>
        protected override string GetDataFilename()
        {
            return string.Format("~/App_Data/Pages/{0}.config", _pageId);
        }

        /// <summary>
        /// Deletes the WebPage and all the sections contained in it
        /// </summary>
        /// <returns>true, if deletion was successful</returns>
        public override bool Delete()
        {
            foreach (ISection section in Sections)
            {
                section.Delete();
            }
            return base.Delete();
        }

        /* SEO Tags management */
        /// <summary>
        /// Return a META Tag according to the supplied parameters
        /// </summary>
        /// <param name="name">Supply the name of the meta tag to generate</param>
        /// <param name="content">Supply the content of the meta tag</param>
        /// <returns>HtmlMeta object</returns>
        public static HtmlMeta generateMeta(string name, string content)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Name = name;
            meta.Content = content;

            return meta;
        }
        /* End SEO changes */ 

        /// <summary>
        /// Data-Class for the WebPage
        /// </summary>
        public class WebPageData
        {
            public WebPageData()
            {
                Visible = true;
                AllowAnonymousAccess = false;
                VirtualPath = string.Empty;
                NavigationName = string.Empty;
                Title = string.Empty;
                Description = string.Empty;
                Keywords = string.Empty; 
                Sections = new List<string>();
            }

            public bool Visible ;
            public bool AllowAnonymousAccess;
            public bool EditPowerUser; // (for PowerUser Management)
            public string VirtualPath;
            public string NavigationName;
            public string Title;
            public string Description;
            public string Keywords; 
            public List<string> Sections;
        }
    }
}

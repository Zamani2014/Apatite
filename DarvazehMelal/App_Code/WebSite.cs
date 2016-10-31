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
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.IO;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// This class is designed as a singleton, there can only be one instance of it. It contains website's core settings like language and culture or title, etc.
    /// </summary>
    [Serializable]
    public class WebSite : Persistable<WebSite.WebSiteData>
    {
        private WebSite() : base() { }

        /// <summary>
        /// Guid of the current homepage
        /// </summary>
        public string HomepageId
        {
            get { return _data.HomepageId; }
            set { _data.HomepageId = value; }
        }

        /// <summary>
        /// The name of the currently active theme (in folder App_Themes)
        /// </summary>
        public string Theme
        {
            get { return _data.Theme; }
            set { _data.Theme = value; }
        }

        /// <summary>
        /// The current locale in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;", where &lt;languagecode2&gt; is a lowercase two-letter code derived from ISO 639-1 and &lt;country/regioncode2&gt; is an uppercase two-letter code derived from ISO 3166. 
        /// </summary>
        public string LocaleID
        {
            get { return _data.Language; }
            set
            {
                _data.Language = value;
            }
        }

        /// <summary>
        /// Name or IP-Address of the smtp-server that will be used for sending mails (contactform)
        /// </summary>
        public string SmtpServer
        {
            get { return _data.SmtpServer; }
            set { _data.SmtpServer = value; }
        }

        /// <summary>
        /// username that will be used for sending mails, when provider requires SMTP-Auth (contactform)
        /// </summary>
        public string SmtpUser
        {
            get { return _data.SmtpUser; }
            set { _data.SmtpUser = value; }
        }

        /// <summary>
        /// password that will be used for sending mails, when provider requires SMTP-Auth (contactform)
        /// </summary>
        public string SmtpPassword
        {
            get { return _data.SmtpPassword; }
            set { _data.SmtpPassword = value; }
        }

        /// <summary>
        /// domain that will be used for sending mails, when provider requires SMTP-Auth (contactform)
        /// </summary>
        public string SmtpDomain
        {
            get { return _data.SmtpDomain; }
            set { _data.SmtpDomain = value; }
        }

        /// <summary>
        /// Email Address that will be used for sending mails (contactform)
        /// </summary>
        public string MailSenderAddress
        {
            get { return _data.MailSenderAddress; }
            set { _data.MailSenderAddress = value; }
        }

        /// <summary>
        /// Text that appears in the footer of every page
        /// </summary>
        public string FooterText
        {
            get { return _data.FooterText; }
            set { _data.FooterText = value; }
        }

        /// <summary>
        /// Title of the website
        /// </summary>
        public string WebSiteTitle
        {
            get { return _data.WebSiteTitle; }
            set { _data.WebSiteTitle = value; }
        }

        /// <summary>
        /// SEO Extension
        /// Keywords for the Meta-Tags
        /// </summary>
        public string Keywords
        {
            get { return _data.Keywords; }
            set { _data.Keywords = value; }
        }

        /// <summary>
        /// SEO Extension
        /// Description for the Meta-Tags
        /// </summary>
        public string Description
        {
            get { return _data.Description; }
            set { _data.Description = value; }
        }

        /// <summary>
        /// Enable Section Rss feeds
        /// </summary>
        public bool EnableSectionRss
        {
            get { return _data.EnableSectionRss; }
            set { _data.EnableSectionRss = value; }
        }

		/// <summary>
		/// Write a X-UA-Compatible metatag on every page to set IE8 to IE7 compatibility mode
		/// </summary>
		public bool EnableIE8CompatibilityMetatag
		{
			get { return _data.EnableIE8CompatibilityMetatag; }
			set { _data.EnableIE8CompatibilityMetatag = value; }
		}

		/// <summary>
		/// Enables the user registration for anonymous users (self-service user creation)
		/// </summary>
		public bool AllowUserSelfRegistration
		{
			get { return _data.AllowUserSelfRegistration; }
			set { _data.AllowUserSelfRegistration = value; }
		}

		public bool EnableVersionChecking
		{
			get { return _data.EnableVersionChecking; }
			set { _data.EnableVersionChecking = value; }
		}

        /// <summary>
        /// Indicates wether the website is ready to use
        /// </summary>
        public bool ReadyToUse
        {
            get { 
                return CheckEnvironmentSetup();
            }
        }
        
        /// <summary>
        /// Iterates through all pages that the current user can see (according to his login-state and the page's visibility). For each page that is accessible, the search method is called for all the sections of that page.
        /// </summary>
        /// <param name="searchString">The string to search for</param>
        /// <returns>A list of SearchResults</returns>
        public List<SearchResult> Search(string searchString)
        {
            List<string> lstVisiblePages = new List<string>();
            GetAllPages(ref lstVisiblePages, SiteMap.RootNode);
            List<SearchResult> foundSections = new List<SearchResult>();

            foreach (string id in lstVisiblePages)
            {
                WebPage page = new WebPage(id);
                if (!page.Visible)
                    continue;
                if ((!page.AllowAnonymousAccess) && (!HttpContext.Current.User.Identity.IsAuthenticated))
                    continue;

                foreach (ISection section in page.Sections)
                    foundSections.AddRange(section.Search(searchString, page));
            }
            return foundSections;
        }

        /// <summary>
        /// returns the PageId of a given section
        /// </summary>
        /// <param name="sectionId">The sectionId to search for</param>
        public string getPageId(string sectionId)
        {
            string pageId = string.Empty;
            List<string> lstVisiblePages = new List<string>();

            GetAllPages(ref lstVisiblePages, SiteMap.RootNode);
            foreach (string id in lstVisiblePages)
            {
                WebPage page = new WebPage(id);
                if (!page.Visible)
                    continue;
                if ((!page.AllowAnonymousAccess) && (!HttpContext.Current.User.Identity.IsAuthenticated))
                    continue;

                foreach (ISection section in page.Sections)
                    if (section.SectionId == sectionId)
                    {
                        return page.PageId;
                    }
            }
            return null;
        }

        /// <summary>
        /// A WebSite cannot be deleted. Therefore this method throws a NotSupportedException.
        /// </summary>
        /// <returns>false</returns>
        public override bool Delete()
        {
            throw new NotSupportedException("WebSite cannot be deleted!");
        }

        #region singleton
        private static WebSite _instance;

        /// <summary>
        /// Singleton method to get the instance of the WebSite.
        /// </summary>
        /// <returns>Instance of WebSite</returns>
        public static WebSite GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WebSite();
                string dataFilePath = HttpContext.Current.Server.MapPath(_instance.GetDataFilename());

                if (File.Exists(dataFilePath))
                {
                    _instance.LoadData();
                }
                else // new website, first use
                {
					if (_instance.CheckEnvironmentSetup())
					{
						_instance.ResetWebSite();
					}
                }
            }
            return _instance;
        }
        #endregion

        /// <summary>
        /// Path to the data-file of the WebSite
        /// </summary>
        /// <returns>Path to the data-file of the WebSite</returns>
        protected override string GetDataFilename()
        {
            return "~/App_Data/WebSite.config";
        }

        /// <summary>
        /// This method is recursively called to get all the WebPages of the WebSite (used in the Search method)
        /// </summary>
        private void GetAllPages(ref List<string> lstPages, SiteMapNode parentNode)
        {
            foreach (SiteMapNode node in parentNode.ChildNodes)
            {
                if (node["pageId"] != null)
                {
                    if (node["visible"] != false.ToString())
                        lstPages.Add(node["pageId"]);
                    GetAllPages(ref lstPages, node);
                }
            }
        }

        /// <summary>
        /// This method resets the website. It deletes all the data-files, pages, sections, etc.
        /// </summary>
        public void ResetWebSite()
        {
            //recursive delete of all the directories in App_Data
            foreach (string dir in Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/App_Data")))
            {
                try { Directory.Delete(dir, true); }
                catch { }
            }

            //delete all remaining files in App_Data
            foreach (string file in Directory.GetFiles(HttpContext.Current.Server.MapPath("~/App_Data")))
            {
                try 
                {
                    if (!file.EndsWith("Roles.config") && !file.EndsWith("Users.config"))
                        File.Delete(file); 
                }
                catch { }
            }

            //create a new empty WebSiteData
            _data = new WebSiteData();

            //create a new empty sitemap
            HttpContext.Current.Cache.Remove("Sitemap");
            SitemapEditor editor = new SitemapEditor();

			//create a new empty page (the new homepage)
            WebPage rootPage = new WebPage();
            rootPage.VirtualPath = "~/Homepage.aspx";
            rootPage.NavigationName = "صفحه اصلی";
            rootPage.AllowAnonymousAccess = true;
            rootPage.Visible = true;
			rootPage.SaveData();

			//create a html block with first steps introduction text
			HtmlContent introductionSection = new HtmlContent();
			introductionSection.Html = @"<h1>Welcome to the My Web Pages Starter Kit</h1><p>To start editing content, please log in with the following credentials:<br /><br /><strong>Username: admin<br />Password: admin<br /><br />Please change the admin password as soon as possible!</strong></p>";
			introductionSection.SaveData();

			rootPage.AddSection(introductionSection.SectionId);

            //set the new page as homepage
            _instance.HomepageId = rootPage.PageId;
            _instance.SaveData();

            editor.InsertPage(rootPage,string.Empty);
            editor.Save();

            try
            {
                foreach (string role in Roles.GetAllRoles())
                {
                    foreach (string user in Roles.GetUsersInRole(role))
                    {
                        Roles.RemoveUserFromRole(user, role);
                    }
                    Roles.DeleteRole(role);
                }

                foreach (MembershipUser user in Membership.GetAllUsers())
                {
                    Membership.DeleteUser(user.UserName);
                }
            }
            catch { }

            //create the "admin" user
            MembershipCreateStatus status;
            Membership.CreateUser("admin", "admin", "you@your-domain.xyz", "Answer with yes", "yes", true, Guid.NewGuid(), out status);

            //create all roles (Administrators and Users)
            try
            {
                Roles.CreateRole(RoleNames.Administrators.ToString());
                Roles.CreateRole(RoleNames.PowerUsers.ToString());   // additional role "Power User (PowerUser Management)"
                Roles.CreateRole(RoleNames.Users.ToString());
            }
            catch { }

            //add the "admin" user to the Administrators role
            Roles.AddUserToRole("admin", RoleNames.Administrators.ToString());
        }

        public bool CheckEnvironmentSetup()
        {
            string PhysicalPath = HttpContext.Current.Server.MapPath("~/App_Data");
            bool Result;
            
            if (! Directory.Exists(PhysicalPath))
            {
                Result = false;
            } else
            {
                Result = true;
            }
            return Result;
            
        }
        
        public string DomainName (HttpContext c)
        {
            return c.Request.ServerVariables["SERVER_NAME"];
        }

		public string GetFullWebsiteUrl(HttpContext context)
		{
			string method = "http";
			string port = string.Empty;
			if (context.Request.ServerVariables["SERVER_PORT_SECURE"] == "1")
				method = "https";

			if (context.Request.ServerVariables["SERVER_PORT"] != string.Empty)
				port = ":" + context.Request.ServerVariables["SERVER_PORT"];

			return string.Format(
				"{0}://{1}{2}{3}",
				method, //{0}
				context.Request.ServerVariables["SERVER_NAME"], //{1}
				port, //{2}
				context.Request.ApplicationPath //{3}
			);
		}

        /// <summary>
        /// Data-Class for WebSite
        /// </summary>
        public class WebSiteData
        {
            public string HomepageId = string.Empty;
            public string Theme = string.Empty;
            public string Language = string.Empty;
            public string SmtpServer = string.Empty;
            public string SmtpUser = string.Empty;
            public string SmtpPassword = string.Empty;
            public string SmtpDomain = string.Empty;
            public string FooterText = string.Empty;
            public string WebSiteTitle = string.Empty;
            public string Keywords = string.Empty;
            public string Description = string.Empty;
            public bool EnableSectionRss = true;
            public string MailSenderAddress = string.Empty;
			public bool EnableIE8CompatibilityMetatag = false;
			public bool AllowUserSelfRegistration = false;
			public bool EnableVersionChecking = true;
        }

    }
    
    public class WebsiteNotReadyException : Exception
    {
        public WebsiteNotReadyException(string Message) : base(Message)
        {
        }
    }


    
}
   

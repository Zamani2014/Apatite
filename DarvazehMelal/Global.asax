<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        try {
            MyWebPagesStarterKit.WebSite.GetInstance();
            SiteMap.Provider.SiteMapResolve += new SiteMapResolveEventHandler(MyWebPagesStarterKit.Providers.CustomXmlSitemapProvider.Resolve);
        } catch (Exception ex)
        {
            throw ex;
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        MyWebPagesStarterKit.Log log = new MyWebPagesStarterKit.Log();
        Exception ex = Server.GetLastError();
        log.AddLogEntry(Request, ex);
        log.SaveData();
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Request_Start(object sender, EventArgs e)
    {

    }


    // because url rewriting is combined with forms authentication, the rewriting must take place in Application_AuthorizeRequest, so that User.Identity is available
    void Application_AuthorizeRequest(object sender, EventArgs e)
    {
        
        string currentURL = Request.Path.ToLower();

		if (currentURL.EndsWith(".aspx"))
		{
			//checks to see if the file does not exsists.
			if (!System.IO.File.Exists(Server.MapPath(currentURL)))
			{
				string queryString = Request.ServerVariables["QUERY_STRING"];
				string defaultPage = Request.ApplicationPath + "/default.aspx?pg=";

				string pageId = null;
				foreach (SiteMapNode node in SiteMap.RootNode.GetAllNodes())
				{
					if (node.Url.ToLower() == currentURL)
					{
						pageId = node["pageId"];
						break;
					}
				}

				if (pageId == null)
					pageId = MyWebPagesStarterKit.WebSite.GetInstance().HomepageId;

				// Rewrites the path
				if (queryString != string.Empty)
					Context.RewritePath(defaultPage + pageId + "&" + queryString, false);
				else
					Context.RewritePath(defaultPage + pageId, false);

			}
		}
    }
      
</script>

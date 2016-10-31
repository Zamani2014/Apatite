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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Net;
using System.IO;
using System.Xml;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// RSS feed display section
    /// This section displays the items of an RSS Channel feed
    /// The content of the feed is serialized to disk in a DataSet and reloaded only 
    /// according to the given 'refresh interval'
    /// </summary>
    public class RSSFeed : Section<RSSFeed.RSSFeedData>, ISidebarObject
    {

        /// <summary>
        /// The constructor will initialize an empty data set
        /// </summary>
        public RSSFeed()
        {
            _data.Items = new DataSet("RSSFeedSet");
            _data.Items.Tables.Add(createTable());

        }

        /// <summary>
        /// Constructor is calling the base class constructor and automatically load the feed data
        /// </summary>
        /// <param name="id"></param>
        public RSSFeed(string id) : base(id) { }


 
        /// <summary>
        /// This RSSItems property will return the current state of the dataset from disk with all items of the channel.
        /// Before returning the latest data it will check if a refresh is needed according to the refresh interval
        /// </summary>
        public DataTable RSSItems
        {
            get 
            {
                // The RefreshIntervalMin must be greater than 0. We don't want to refresh it with every call!
                if (_data.RefreshIntervalMin == 0)
                    _data.RefreshIntervalMin = 30;

                TimeSpan refreshInterval = new TimeSpan(0, _data.RefreshIntervalMin, 0);

                // Check if refresh is due
                if (URL != null && URL != "" && (DateTime.Now - refreshInterval >= LastUpdate || _data.Items.Tables["RSSFeedItems"].Rows.Count == 0))
                {
                    // We only download a maximum of 100 items per feed
                    if (_data.MaxNumberOfItems > 100)
                        _data.MaxNumberOfItems = 100;

                    // Try download the latest XML data of the channel.
                    DataTable freshFeed;
                    if ((freshFeed = this.loadChannel()) != null)
                    {
                        // If download was successful we replace the current table with the new one
                        _data.Items.Tables.Clear();
                        _data.Items.Tables.Add(freshFeed);

                        // Set the last update time
                        _data.LastUpdate = DateTime.Now;

                        // Serialize the channel items to disk
                        SaveData();
                    }
                } // if

                // The dataset either contains old or new data. Depending on refresh interval and download problems.
                return _data.Items.Tables["RSSFeedItems"];
            }
        }

  

        /// <summary>
        /// Returns a DataView with all items
        /// </summary>
        public DataView GetRSSItems()
        {
            return new DataView(RSSItems, string.Empty, "Date DESC", DataViewRowState.CurrentRows);
        }

        
        /// <summary>
        /// Returns the DataRow for the given Guid
        /// </summary>
        /// <param name="id">Guid of the record to return</param>
        public DataRow GetRSSItem (string id)
        {
            DataRow row = null;
            DataRow[] foundRows = RSSItems.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }



        /// <summary>
        /// This method is called in order to find data if user is searching information
        /// </summary>
        /// <param name="searchString">search string</param>
        /// <param name="page">Current web page where the feed is currently shown (parent)</param>
        /// <returns></returns>
        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            string DateNow = DateTime.Today.ToString("MM/dd/yyyy");
            List<SearchResult> foundResults = new List<SearchResult>();
            DataRow[] foundRows = RSSItems.Select(string.Format("(Title LIKE '%{0}%' OR Description LIKE '%{0}%') AND Date <= #{1}#", searchString.Replace("'", "''"), DateNow));
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format("~/Default.aspx?pg={0}&detail={1}#{2}", page.PageId, row["Guid"], SectionId),
                        (string)row["Title"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)row["Title"] + (string)row["Description"]), searchString)
                    )
                );
            }
            return foundResults;
        }



        /// <summary>
        /// Here we 'publish' all the properties of the core data class - RSSFeedData
        /// </summary>
        #region PublicProperties

        public int MaxNumberOfItems
        {
            get
            {
                return _data.MaxNumberOfItems;
            }
            set
            {
                _data.MaxNumberOfItems = value;
            }
        }

        public int RefreshIntervalMin
       {
            get
            {
                return _data.RefreshIntervalMin > 0 ? _data.RefreshIntervalMin : 30;
            }
            set
            {
                _data.RefreshIntervalMin = value;
            }
        }
       
        public string URL
               {
            get
            {
                return _data.URL;
            }
            set
            {
                _data.URL = value;
                _data.Items.Tables["RSSFeedItems"].Clear();
            }
        }

        public DateTime LastUpdate
       {
            get
            {
                return _data.LastUpdate;
            }

        }


        public string ChannelTitle
        {
            get
            {
                return _data.ChannelTitle;
            }
 
        }


        public string ChannelLink
        {
            get
            {
                return _data.ChannelLink;
            }
  
        }

        public string ChannelDescription
        {
            get
            {
                return _data.ChannelDescription;
            }
 
        }


        public string ChannelMessage
        {
            get
            {
                return _data.ChannelMessage;
            }
        }


        public int NumberOfItems
        {
            get
            {
                return _data.NumberOfItems;
            }
        }

        #endregion

  

        #region PrivateHelperMethods

        /// <summary>
        /// Download the stream of one channel.
        /// </summary>
        /// <param name="channelRow"></param>
        private DataTable loadChannel()
        {
            int newItems = 0;

            try
            {
                WebClient client = new WebClient();

                // Make sure the admins can identify the requesting application in their logfiles 
                client.Headers.Add("User-Agent", "MWPSK");

                using (Stream inStream = client.OpenRead(URL))
                {
                    XmlReader reader = new XmlTextReader(inStream);
                    XmlDocument xmlDoc = new XmlDocument();

                    System.Diagnostics.Debug.WriteLine(URL);

                    xmlDoc.Load(reader);

                    string rssNamespaceUri = String.Empty;

                    if (xmlDoc.DocumentElement.LocalName.Equals("RDF") &&
                        xmlDoc.DocumentElement.NamespaceURI.Equals("http://www.w3.org/1999/02/22-rdf-syntax-ns#"))
                    { //RSS 0.9 or 1.0

                        //figure out if 0.9 or 1.0 by peeking at namespace URI of <channel> element 
                        if ((xmlDoc.DocumentElement.ChildNodes.Count > 0) &&
                            (xmlDoc.DocumentElement.FirstChild.Name.Equals("channel")))
                        {
                            rssNamespaceUri = xmlDoc.DocumentElement.FirstChild.NamespaceURI;
                        } // if
                        else
                        { //no channel, just assume RSS 1.0 
                            rssNamespaceUri = "http://purl.org/rss/1.0/";
                        } // else
                    } // if
                    else if (xmlDoc.DocumentElement.LocalName.Equals("rss"))
                    { //RSS 0.91 or 2.0 

                        rssNamespaceUri = xmlDoc.DocumentElement.NamespaceURI;
                    } // else if
                    else
                    { // This XML document does not look like an RSS feed
                        throw new ApplicationException("This XML document does not look like an RSS feed");
                    } // eles


                    XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    nsMgr.AddNamespace("rss", rssNamespaceUri);
                    nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
                    nsMgr.AddNamespace("content", "http://purl.org/rss/1.0/modules/content/");


                    XmlNode channelNode = xmlDoc.SelectSingleNode("//rss:channel", nsMgr);

                    XmlNode temp;

                    _data.ChannelTitle = ((temp = channelNode.SelectSingleNode("rss:title", nsMgr)) != null) ? temp.InnerText : null;
                    _data.ChannelLink = ((temp = channelNode.SelectSingleNode("rss:link", nsMgr)) != null) ? temp.InnerText : null;
                    _data.ChannelDescription = ((temp = channelNode.SelectSingleNode("rss:description", nsMgr)) != null) ? temp.InnerText : null;


                    XmlNodeList itemNodes = xmlDoc.SelectNodes("//rss:item", nsMgr);

                    DataTable channelTable = createTable();
                    _data.ChannelMessage = "";
                    _data.NumberOfItems = 0;

                    foreach (XmlNode item in itemNodes)
                    {

                        string description = ((temp = item.SelectSingleNode("rss:description", nsMgr)) != null) ? temp.InnerText : null;
                        string link = ((temp = item.SelectSingleNode("rss:link", nsMgr)) != null) ? temp.InnerText : null;
                        string title = ((temp = item.SelectSingleNode("rss:title", nsMgr)) != null) ? temp.InnerText : null;
                        string subject = ((temp = item.SelectSingleNode("dc:subject", nsMgr)) != null) ? temp.InnerText : null;
                        DateTime date = DateTime.Now;

                        //some Radio Userland feeds use <guid> instead of <link> so check 
                        if (link == null)
                        {
                            link = ((temp = item.SelectSingleNode("rss:guid", nsMgr)) != null) ? temp.InnerText : null;
                        } // if

                        //some Radio Userland feeds use <category> instead of <dc:subject> so check 
                        if (subject == null)
                        {
                            subject = ((temp = item.SelectSingleNode("rss:category", nsMgr)) != null) ? temp.InnerText : null;
                        } // if

                        //Infoworld feeds use <dc:description>> instead of <description> so check 
                        if (description == null)
                        {
                            description = ((temp = item.SelectSingleNode("dc:description", nsMgr)) != null) ? temp.InnerText : null;
                        } // if

                        //some people like Gary Burd only use content:encoded 
                        if (description == null)
                        {
                            description = ((temp = item.SelectSingleNode("content:encoded", nsMgr)) != null) ? temp.InnerText : null;
                        } // if


                        try
                        {
                            if ((temp = item.SelectSingleNode("dc:date", nsMgr)) != null)
                            {
                                date = XmlConvert.ToDateTime(temp.InnerText);
                            } // if
                            else if ((temp = item.SelectSingleNode("rss:pubDate", nsMgr)) != null)
                            {
                                date = DateTime.Parse(temp.InnerText);
                            } // else
                        } // try
                        catch (FormatException fe)
                        { /* date was improperly formated*/
                            ;
                            //errorPanel.Text = "ERROR: " + fe.ToString();				
                        } // catch


                        string linkWithoutSpaces = link.Replace(" ", "");


                        DataRow itemRow = channelTable.NewRow();
                        itemRow["Guid"] = Guid.NewGuid();
                        itemRow["Title"] = title;
                        itemRow["Link"] = link;
                        itemRow["Description"] = description;
                        itemRow["Date"] = date;
                        itemRow["Subject"] = subject;

                        channelTable.Rows.Add(itemRow);
                        channelTable.AcceptChanges();

                        _data.NumberOfItems++;

                        if (_data.NumberOfItems == _data.MaxNumberOfItems)
                            break;

                    } // foreach

                    return channelTable;
                } // using

            }
            catch (Exception ex)
            {
                _data.ChannelMessage = ex.Message;
                 return null;
           } // catch


        } // loadChannel



        private DataTable createTable()
        {
            DataTable newTable = new DataTable("RSSFeedItems");

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(string));
            primaryKeyColumn.Unique = true;

            newTable.Columns.Add(primaryKeyColumn);
            newTable.PrimaryKey = new DataColumn[] { primaryKeyColumn };

            newTable.Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("Subject", typeof(string)),
                    new DataColumn("Link", typeof(string)),
                    new DataColumn("Date", typeof(DateTime))
                }
           );

            return newTable;

        } // createTable

        #endregion

        #region RssMethods
        public ChannelData GetSidebarRss(string PageId)
        {
            string DateNow = DateTime.Today.ToString("MM/dd/yyyy");
            ChannelData elements = new ChannelData();
            DataRow[] foundRows = RSSItems.Select();
            //string.Format("Date <= #{0}#", DateNow));
            int iPosition = 0;
            foreach (DataRow row in foundRows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                iPosition = 0;
                foreach (string rsskey in Enum.GetNames(typeof(RssElements)))
                {
                    insertRssKeyValue(rsskey, row, PageId, ref item);
                    iPosition++;
                }
                elements.ChannelItems.Add(item);
            }
            return elements;
        }

        private void insertRssKeyValue(string RssKey, DataRow row, string PageId, ref Dictionary<string, string> elements)
        {
            string key = string.Empty;
            string value = string.Empty;
            switch (RssKey)
            {
                case "title":
                    value = (string)row["Title"];
                    break;
                case "description":
                    value = row["Description"] != DBNull.Value ? (string)row["Description"] : "";
                    break;
                case "link":
                    value = string.Format("~/Default.aspx?pg={0}&detail={1}#{2}", PageId, row["Guid"], SectionId);
                    break;
                case "date":
                    value = ((DateTime)row["Date"]).ToString();
                    break;
                case "comment":
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
         #endregion
       }



        /// <summary>
        /// This class serves as the core element that will be persisted to disk. It holds the dataset with all items 
        /// and all other key elements required to represent a RSS Channel.
        /// </summary>
        public struct RSSFeedData
        {
            // The link of the feed.
            public string URL;

            // Refresh interval in minutes
            public int RefreshIntervalMin;

            // Maximum number of items. 0 means all items (but not more than 100 at a time)
            public int MaxNumberOfItems;

            // Data set holding all the individual items
            public DataSet Items;

            // Seperate field to represent the number of items. This is used instead of the .Rows.Count properties.
            public int NumberOfItems;

            // When the last update has happened
            public DateTime LastUpdate;

            // Title of the cannel <- coming from the rss feed
            public string ChannelTitle;

            // Link of the channel (web page) <- coming from the rss feed
            public string ChannelLink;

            // Description of the channel <- coming from the rss feed
            public string ChannelDescription;

            // Last error message if the download of the feed fails
            public string ChannelMessage;
        }
    }
}

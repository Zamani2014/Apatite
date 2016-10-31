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

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// News List Section
    /// This section displays a list of news
    /// Only news with a showuntil date that lies in the future are displayed
    /// </summary>
    public class NewsList : Section<NewsList.NewsListData>, ISidebarObject
    {
        public NewsList()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("NewsListEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["NewsListEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["NewsListEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["NewsListEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Headline", typeof(string)),
                    new DataColumn("Newstext", typeof(string)),
                    new DataColumn("NewsDate", typeof(DateTime)),
                    new DataColumn("ShowUntil", typeof(DateTime))
                }
           );
        }
        public NewsList(string id) : base(id) { }

        /// <summary>
        /// The DataTable with all news
        /// </summary>
        public DataTable NewsEntries
        {
            get { return _data.Entries.Tables["NewsListEntries"]; }
        }

        /// <summary>
        /// Returns a DataView with all news that have a ShowUntil-Date in the future and NewsDate in the past
        /// </summary>
        public DataView GetNewsEntriesReadonly()
        {
            string DateNow = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
            string DateShowUntil = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            return new DataView(NewsEntries, string.Format("NewsDate <= #{0}# AND (ShowUntil >= #{1}# OR ShowUntil IS NULL)", DateNow, DateShowUntil), "NewsDate DESC", DataViewRowState.CurrentRows);
        }

        /// <summary>
        /// Returns a DataView with all news (no matter what values ShowUntil and/or NewsDate have)
        /// </summary>
        public DataView GetNewsEntriesEdit()
        {
            return new DataView(NewsEntries, string.Empty, "NewsDate DESC", DataViewRowState.CurrentRows);
        }

        /// <summary>
        /// Returns the DataRow for the given Guid
        /// </summary>
        /// <param name="id">Guid of the record to return</param>
        public DataRow GetNewsEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = NewsEntries.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            string url = string.Empty;
            if (page.VirtualPath != string.Empty)
                url = page.VirtualPath.ToLower() + "?detail={1}#{2}";
            else
                url = "~/default.aspx?pg={0}&detail={1}#{2}";

            string DateShowUntil = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            string DateNow = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
            List<SearchResult> foundResults = new List<SearchResult>();
            foreach (DataRow row in NewsEntries.Rows)
            {
                row["Newstext"] = HttpUtility.HtmlDecode(SearchResult.RemoveHtml(row["Newstext"].ToString()));
            }
			DataRow[] foundRows = NewsEntries.Select(string.Format("(Headline LIKE '%{0}%' OR Newstext LIKE '%{0}%') AND (ShowUntil >= #{2}# OR ShowUntil Is Null) AND Newsdate <= #{1}#", searchString.Replace("'", "''"), DateNow, DateShowUntil), "NewsDate DESC");
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, row["Guid"], SectionId),
                        (string)row["Headline"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)row["Newstext"]), searchString)
                    )
                );
            }
            return foundResults;
        }

        #region RssMethods
        public ChannelData GetSidebarRss(string PageId)
        {
            string DateNow = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
            string DateShowUntil = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            ChannelData elements = new ChannelData();
            DataRow[] foundRows = NewsEntries.Select(string.Format("Newsdate <= #{0}# AND (ShowUntil >= #{1}# OR ShowUntil Is Null)", DateNow, DateShowUntil), "Newsdate DESC");
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
                    value = (string)row["Headline"];
                    break;
                case "link":
                    value = string.Format("~/Default.aspx?pg={0}&detail={1}#{2}", PageId, row["Guid"], SectionId);
                    break;
                case "comment":
                    break;
                case "description":
                    value = "<b>" + ((DateTime)row["NewsDate"]).ToShortDateString() + "</b><br />" + HttpContext.Current.Server.HtmlDecode((string)row["Newstext"]);
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }
        #endregion

        public struct NewsListData
        {
            public DataSet Entries;
        }
    }
}

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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Collections.Generic;

namespace MyWebPagesStarterKit
{
    public class Blogroll : Section<Blogroll.BlogrollData>, ISidebarObject
    {
        public Blogroll()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("BlogrollEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["BlogrollEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["BlogrollEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["BlogrollEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Created", typeof(DateTime)),
                    new DataColumn("Url", typeof(string)),
                    new DataColumn("Feed", typeof(string)),
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("Order", typeof(int))
                }
           );

            //configuration
            _data.Entries.Tables.Add(new DataTable("BlogrollConfiguration"));
            _data.Entries.Tables["BlogrollConfiguration"].Columns.AddRange(
              new DataColumn[] 
                {
                    new DataColumn("Created", typeof(DateTime)),
                    new DataColumn("Updated", typeof(DateTime)),
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Owner", typeof(string)),
                    new DataColumn("OwnerEmail", typeof(string)),
                    new DataColumn("Visible", typeof(bool)),
                    new DataColumn("PageSize", typeof(int)),
                    new DataColumn("Sorting", typeof(SortingTypes))
                }
             );

            //save default configuration
            DataRow row = null;
            row = ConfigurationData.NewRow();
            row["Created"] = DateTime.Now;
            row["Updated"] = DateTime.Now;
            row["Visible"] = true;
            ConfigurationData.Rows.Add(row);
            ConfigurationData.AcceptChanges();

        }

        public enum SortingTypes { Priority = 1, Random, Alphabetically, Recent };

        public Blogroll(string id) : base(id) { }

        public DataView GetBlogrollEntries()
        {
            return new DataView(BlogrollEntries, string.Empty, "Order, Title ASC", DataViewRowState.CurrentRows);
        }

        public DataTable BlogrollEntries
        {
            get { return _data.Entries.Tables["BlogrollEntries"]; }
        }

        public DataRow GetBlogrollEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = BlogrollEntries.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

        public DataTable ConfigurationData
        {
            get { return _data.Entries.Tables["BlogrollConfiguration"]; }
        }

        public object getConfiguration(string key)
        {
            return ConfigurationData.Rows[0][key];
        }

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            string url = string.Empty;
            if (page.VirtualPath != string.Empty)
                url = page.VirtualPath.ToLower() + "#{1}";
            else
                url = "~/Default.aspx?pg={0}#{1}";

            //keine deep-links
            List<SearchResult> foundResults = new List<SearchResult>();

            DataRow[] foundRows = BlogrollEntries.Select(string.Format("Title LIKE '%{0}%' OR Url LIKE '%{0}%' OR Description LIKE '%{0}%' ", searchString.Replace("'", "''")), "Title ASC");
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, SectionId),
                        (string)row["Title"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)row["Description"]), searchString)
                    )
                );
            }

            return foundResults;
        }

        #region RssMethods
        public ChannelData GetSidebarRss(string PageId)
        {
            ChannelData elements = new ChannelData();
            elements.Title = getConfiguration("Title").ToString();

            int iPosition = 0;
            foreach (DataRowView row in GetBlogrollEntries())
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

        private void insertRssKeyValue(string RssKey, DataRowView row, string PageId, ref Dictionary<string, string> elements)
        {
            string key = string.Empty;
            string value = string.Empty;
            switch (RssKey)
            {
                case "title":
                    value = (string)row["Title"];
                    break;
                case "link":
                    value = (string)row["Url"];
                    break;
                case "description":
                    value = (string)row["Description"];
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }

        #endregion

        #region OpmlMethods
        public ChannelData GetOpmlElements(string PageId)
        {
            ChannelData elements = new ChannelData();
            elements.Title = getConfiguration("Title").ToString();

            int iPosition = 0;
            foreach (DataRowView row in GetBlogrollEntries())
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                iPosition = 0;
                foreach (string opmlkey in Enum.GetNames(typeof(OpmlElements)))
                {
                    insertOpmlKeyValue(opmlkey, row, PageId, ref item);
                    iPosition++;
                }
                elements.ChannelItems.Add(item);
            }
            return elements;
        }

        private void insertOpmlKeyValue(string RssKey, DataRowView row, string PageId, ref Dictionary<string, string> elements)
        {
            string key = string.Empty;
            string value = string.Empty;
            switch (RssKey)
            {
                case "type":
                    value = "rss";
                    break;
                case "text":
                    value = (string)row["Title"];
                    break;
                case "xmlUrl":
                    value = (string)row["Feed"];
                    break;
                case "description":
                    value = (string)row["Description"];
                    break;
            }
            elements.Add(RssKey, value);
        }

        /// <summary>
        /// Defines OPML channel elements that are available at the moment
        /// </summary>
        public enum OpmlElements { type, text, xmlUrl, description };

        #endregion

        public struct BlogrollData
        {
            public DataSet Entries;
        }
    }
}

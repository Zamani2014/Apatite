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
    public class LinkList : Section<LinkList.LinkListData>, ISidebarObject
    {
        public LinkList()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("LinkListEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["LinkListEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["LinkListEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["LinkListEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Url", typeof(string)),
                    new DataColumn("Target", typeof(string)),
                    new DataColumn("Comment", typeof(string))
                }
           );
        }

        public LinkList(string id) : base(id) { }

        public DataView GetLinkEntries()
        {
            return new DataView(LinkEntries, string.Empty, "Title ASC", DataViewRowState.CurrentRows); 
        }

        public DataTable LinkEntries
        {
            get { return _data.Entries.Tables["LinkListEntries"]; }
        }

        public DataRow GetLinkEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = LinkEntries.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
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

            DataRow[] foundRows = LinkEntries.Select(string.Format("Title LIKE '%{0}%' OR Url LIKE '%{0}%' OR Comment LIKE '%{0}%' ", searchString.Replace("'", "''")), "Title ASC");
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, SectionId),
                        (string)row["Title"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)row["Comment"]), searchString)
                    )
                );
            }

            return foundResults;
        }

        #region RssMethods
        public ChannelData GetSidebarRss(string PageId)
        {
            ChannelData elements = new ChannelData();
            int iPosition = 0;
            foreach (DataRowView row in GetLinkEntries())
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
					if(!string.IsNullOrEmpty(row["Title"] as string))
						value = (string)row["Title"];
					else
						value = (string)row["Url"];
                    break;
                case "link":
                    value = (string)row["Url"];
                    break;
                case "description":
                    value = (string)row["Comment"];
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }

        #endregion

        public struct LinkListData
        {
            public DataSet Entries;
        }
    }
}

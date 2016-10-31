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
    public class Guestbook : Section<Guestbook.GuestbookData>, ISidebarObject
    {
        public Guestbook()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("GuestbookEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["GuestbookEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["GuestbookEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["GuestbookEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Author", typeof(string)),
                    new DataColumn("Text", typeof(string)),
                    new DataColumn("EntryDate", typeof(DateTime))
                }
           );
        }

        public Guestbook(string id) : base(id) { }

        public DataView GetGuestbookEntries()
        {
            return new DataView(GuestbookEntries, string.Empty, "EntryDate DESC", DataViewRowState.CurrentRows);
        }

        public DataTable GuestbookEntries
        {
            get { return _data.Entries.Tables["GuestbookEntries"]; }
        }

        public DataRow GetGuestbookEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = GuestbookEntries.Select(string.Format("Guid = '{0}'", id));
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

            //keine deep-links
            List<SearchResult> foundResults = new List<SearchResult>();
            DataRow[] foundRows = GuestbookEntries.Select(string.Format("Author LIKE '%{0}%' OR Text LIKE '%{0}%'", HttpUtility.HtmlEncode(searchString.Replace("'", "''"))), "EntryDate DESC");
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, row["Guid"], SectionId),
                        (string)row["Author"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)row["Text"]), HttpUtility.HtmlEncode(searchString))
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
            foreach (DataRowView row in GetGuestbookEntries())
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
                    value = (string)row["Author"];
                    break;
                case "link":
                    value = string.Format("~/Default.aspx?pg={0}#{1}", PageId, row["Guid"].ToString());
                    break;
                case "comment":
                    break;
                case "description":
                    value = "<b>" + ((DateTime)row["EntryDate"]).ToShortDateString() + "</b><br />" + HttpContext.Current.Server.HtmlDecode((string)row["Text"]);
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }
        #endregion

        public struct GuestbookData
        {
            public DataSet Entries;
        }
    }
}

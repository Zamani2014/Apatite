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
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Collections.Generic;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Download List Section
    /// This section displays a list of downloadable files
    /// </summary>
    public class DownloadList : Section<DownloadList.DownloadListData>, ISidebarObject
    {
        public DownloadList()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("DownloadListEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["DownloadListEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["DownloadListEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["DownloadListEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Filename", typeof(string)),
                    new DataColumn("Size", typeof(string)),
                    new DataColumn("Comment", typeof(string))
                }
           );
        }

        public DownloadList(string id) : base(id) { }

        public DataView GetDownloadEntries()
        {
            return new DataView(DownloadEntries, string.Empty, "Title ASC", DataViewRowState.CurrentRows);
        }

        public DataTable DownloadEntries
        {
            get { return _data.Entries.Tables["DownloadListEntries"]; }
        }

        public DataRow GetDownloadEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = DownloadEntries.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

        /// <summary>
        /// Directory to which files are uploaded
        /// </summary>
        public string UploadDirectory
        {
            get 
            {
                string virtualdirectory = string.Format("~/App_Data/_Downloads/{0}", SectionId);
                string physicaldirectory = HttpContext.Current.Server.MapPath(virtualdirectory);
                if(!Directory.Exists(physicaldirectory))
                    Directory.CreateDirectory(physicaldirectory);
                return virtualdirectory;
           }
        }

        public override bool Delete()
        {
            try
            {
                Directory.Delete(HttpContext.Current.Server.MapPath(UploadDirectory), true);
            }
            catch
            {
                return false;
            }

            return base.Delete();
        }

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            string url = string.Empty;
            if (page.VirtualPath != string.Empty)
                url = page.VirtualPath.ToLower() + "#{1}";
            else
                url = "~/Default.aspx?pg={0}#{1}";

            //no deep-links, as soon as one matching entry is found, return the link to the WebPage
            List<SearchResult> foundResults = new List<SearchResult>();
            DataRow[] foundRows = _data.Entries.Tables["DownloadListEntries"].Select(string.Format("Title LIKE '%{0}%' OR Filename LIKE '%{0}%' OR Comment LIKE '%{0}%'", searchString.Replace("'", "''")), "Title ASC");

            if (foundRows.Length > 0)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, SectionId),
                        (string)foundRows[0]["Title"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)foundRows[0]["Comment"]), searchString)
                    )
                );
            }

            return foundResults;
        }

        public ChannelData GetSidebarRss(string PageId)
        {
            ChannelData elements = new ChannelData();
            foreach (DataRowView row in GetDownloadEntries())
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                int iPosition = 0;
                foreach (string rsskey in Enum.GetNames(typeof(RssElements)))
                {
                    InsertRssKeyValue(rsskey, row, PageId, ref item);
                    iPosition++;
                }

                elements.ChannelItems.Add(item);
            }
            return elements;
        }

        private void InsertRssKeyValue(string RssKey, DataRowView row, string PageId, ref Dictionary<string, string> elements)
        {
            string key = string.Empty;
            string value = string.Empty;
            switch (RssKey)
            {
                case "title":
                    value = row["Title"] + " (" + row["Size"] + ")";
                    break;
                case "link":
                    value = string.Format("~/DownloadHandler.ashx?pg={0}&section={1}&file={2}", PageId , SectionId, HttpUtility.UrlEncode((string)row["filename"]));
                    break;
                case "description":
                    value = (string)row["Comment"];
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }

        public struct DownloadListData
        {
            public DataSet Entries;
        }
    }
}

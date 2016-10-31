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
using System.Collections;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Gallery section
    /// Displays a gallery with user-uploaded pictures
    /// </summary>
    public class Gallery : Section<Gallery.GalleryData>, ISidebarObject
    {
        public Gallery()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("GalleryEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["GalleryEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["GalleryEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["GalleryEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Filename", typeof(string)),
                    new DataColumn("Comment", typeof(string))
                }
           );
        }

        public Gallery(string id) : base(id) { }

        public DataTable GetGalleryEntries()
        {
            return GalleryEntries;
        }

        public DataTable GalleryEntries
        {
            get { return _data.Entries.Tables["GalleryEntries"]; }
        }

        public DataRow GetGalleryEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = GalleryEntries.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

        public string GetPreviousEntryId(Guid id)
        {
            DataRow row = GalleryEntries.Rows.Find(id);
            int index = GalleryEntries.Rows.IndexOf(row);
            if (index > 0)
                return GalleryEntries.Rows[index - 1]["Guid"].ToString();
            else
                return string.Empty;
        }

        public string GetNextEntryId(Guid id)
        {
            DataRow row = GalleryEntries.Rows.Find(id);
            int index = GalleryEntries.Rows.IndexOf(row);
            if (index < GalleryEntries.Rows.Count - 1)
                return GalleryEntries.Rows[index + 1]["Guid"].ToString();
            else
                return string.Empty;
        }

        private string _UploadDirectory;
        public string UploadDirectory
        {
            get
            {
                if (_UploadDirectory == null)
                {
                    string virtualdirectory = string.Format("~/App_Data/_Downloads/{0}", SectionId);
                    string physicaldirectory = HttpContext.Current.Server.MapPath(virtualdirectory);
                    if (!Directory.Exists(physicaldirectory))
                        Directory.CreateDirectory(physicaldirectory);
                    _UploadDirectory = virtualdirectory;
                }
                return _UploadDirectory;
            }
        }

        private string _ImportDirectory;
        public string ImportDirectory
        {
            get
            {
                if (_ImportDirectory == null)
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/App_Data/_GalleryImports")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Data/_GalleryImports"));
                    }

                    string virtualdirectory = string.Format("~/App_Data/_GalleryImports/{0}", SectionId);
                    string physicaldirectory = HttpContext.Current.Server.MapPath(virtualdirectory);
                    if (!Directory.Exists(physicaldirectory))
                        Directory.CreateDirectory(physicaldirectory);
                    _ImportDirectory = virtualdirectory;
                }
                return _ImportDirectory;
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
                url = page.VirtualPath.ToLower() + "?detail={1}#{2}";
            else
                url = "~/default.aspx?pg={0}&detail={1}#{2}";

            List<SearchResult> foundResults = new List<SearchResult>();
            DataRow[] foundRows = GalleryEntries.Select(string.Format("Title LIKE '%{0}%' OR Comment LIKE '%{0}%'", searchString.Replace("'", "''")));
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, row["Guid"], SectionId),
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
            DataRowCollection foundRows = GalleryEntries.Rows;
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
                case "link":
                    value = string.Format("~/Default.aspx?pg={0}&detail={1}#{2}", PageId, row["Guid"], SectionId);
                    break;
                case "description":
                    string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string imageUrl = string.Format("{0}{1}/DownloadHandler.ashx?pg={2}&section={3}&file={4}",
                        host,
                        appPath,
                        PageId,
                        SectionId,
                        HttpUtility.UrlEncode((string)row["Filename"])
                        );
                    value = string.Format("<img src=\"{0}\"/><br/>", imageUrl) + (string)row["Comment"];
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }
        #endregion

        public struct GalleryData
        {
            public DataSet Entries;
        }
    }
}

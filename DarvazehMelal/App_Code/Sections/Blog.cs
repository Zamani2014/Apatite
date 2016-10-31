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
    /// Blog section
    /// Displays a Blog with blog entries and user comments
    /// </summary>
    public class Blog : Section<Blog.BlogData>, ISidebarObject
    {
        #region constructor

        public Blog()
        {
            //Blog Entries
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("BlogEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["BlogEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["BlogEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["BlogEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Content", typeof(string)),
                    new DataColumn("Created", typeof(DateTime)),
                    new DataColumn("Updated", typeof(DateTime)),
                    new DataColumn("Comments",typeof(DataTable)),
                    new DataColumn("Tags",typeof(ArrayList)),
                }
           );

            //Tags
            _data.Entries.Tables.Add(new DataTable("BlogTags"));

            DataColumn primaryKeyColumnK = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumnK.Unique = true;

            _data.Entries.Tables["BlogTags"].Columns.Add(primaryKeyColumnK);
            _data.Entries.Tables["BlogTags"].PrimaryKey = new DataColumn[] { primaryKeyColumnK };

            _data.Entries.Tables["BlogTags"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("Number", typeof(int)),
                }
           );

            //configuration
            _data.Entries.Tables.Add(new DataTable("BlogConfiguration"));
            _data.Entries.Tables["BlogConfiguration"].Columns.AddRange(
              new DataColumn[] 
                {
                    new DataColumn("Created", typeof(DateTime)),
                    new DataColumn("Updated", typeof(DateTime)),
                    new DataColumn("BlogName", typeof(string)),
                    new DataColumn("Owner", typeof(string)),
                    new DataColumn("SendPings", typeof(bool)),
                    new DataColumn("Visible", typeof(bool)),
                    new DataColumn("PageSize", typeof(int)),
                }
             );

            //save default configuration
            DataRow row = null;
            row = ConfigurationData.NewRow();
            row["Created"] = DateTime.Now;
            row["Updated"] = DateTime.Now;
            row["SendPings"] = true;
            row["Visible"] = true;
            ConfigurationData.Rows.Add(row);
            ConfigurationData.AcceptChanges();
        }

        public Blog(string id) : base(id) { }

        #endregion

        #region BlogEntries

        public DataView GetBlogEntriesSorted(string sortExpression)
        {
            return new DataView(GetBlogEntries(), null, sortExpression, DataViewRowState.CurrentRows);
        }

        public DataView GetBlogEntriesFiltered(DateTime day, string sortExpression)
        {
            return new DataView(GetBlogEntries(), string.Format("Updated >= #{0}# And Updated <= #{1}#", day.ToString("MM/dd/yyyy"), day.AddDays(1).ToString("MM/dd/yyyy")), sortExpression, DataViewRowState.CurrentRows);
        }

        public DataTable GetBlogEntries()
        {
            return BlogEntries;
        }

        public DataTable BlogEntries
        {
            get { return _data.Entries.Tables["BlogEntries"]; }
        }

        public DataRow GetBlogEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = BlogEntries.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

        public string GetPreviousEntryId(Guid id)
        {
            DataRow row = BlogEntries.Rows.Find(id);
            int index = BlogEntries.Rows.IndexOf(row);
            if (index > 0)
                return BlogEntries.Rows[index - 1]["Guid"].ToString();
            else
                return string.Empty;
        }

        public string GetNextEntryId(Guid id)
        {
            DataRow row = BlogEntries.Rows.Find(id);
            int index = BlogEntries.Rows.IndexOf(row);
            if (index < BlogEntries.Rows.Count - 1)
                return BlogEntries.Rows[index + 1]["Guid"].ToString();
            else
                return string.Empty;
        }

        public static DataTable BlogEntriesSearchedStructure()
        {
            DataTable entries = new DataTable("BlogEntriesSearched");

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            entries.Columns.Add(primaryKeyColumn);
            entries.PrimaryKey = new DataColumn[] { primaryKeyColumn };
            entries.Columns.AddRange(
                new DataColumn[]
            {
                new DataColumn("SectionId", typeof(string)),
                new DataColumn("PageId", typeof(string)),
                new DataColumn("Title", typeof(string)),
                new DataColumn("Content", typeof(string)),
                new DataColumn("Created", typeof(DateTime)),
                new DataColumn("Comments", typeof(DataTable)),
                new DataColumn("Tags", typeof(ArrayList)),
            }
            );

            return entries;
        }

        #endregion

        #region EntryComments

        public DataView GetCommentsSorted(string id)
        {
            DataRow entry = GetBlogEntry(id);
            return new DataView((DataTable)entry["Comments"], null, "Created desc", DataViewRowState.CurrentRows);
        }

        public DataTable GetComments(string id)
        {
            DataRow entry = GetBlogEntry(id);
            return (DataTable)entry["Comments"];
        }

        public DataTable CommentsStructure()
        {
            DataTable comments = new DataTable("EntryComments");

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            comments.Columns.Add(primaryKeyColumn);
            comments.PrimaryKey = new DataColumn[] { primaryKeyColumn };
            comments.Columns.AddRange(
                new DataColumn[]
            {
                new DataColumn("Title", typeof(string)),
                new DataColumn("Content", typeof(string)),
                new DataColumn("Created", typeof(DateTime)),
                new DataColumn("Author", typeof(string)),
            }
            );

            return comments;
        }

        public DataRow GetBlogEntryComment(DataTable comments, string commentId)
        {
            DataRow row = null;
            DataRow[] foundRows = comments.Select(string.Format("Guid = '{0}'", commentId));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

        #endregion

        #region BlogTags

        public DataView GetBlogTagsSorted()
        {
            return new DataView(GetBlogTags(), null, "Name", DataViewRowState.CurrentRows);
        }

        public DataTable GetBlogTags()
        {
            return BlogTags;
        }

        public DataTable BlogTags
        {
            get { return _data.Entries.Tables["BlogTags"]; }
        }

        public DataRow GetBlogTag(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = BlogTags.Select(string.Format("Guid = '{0}'", id));
            if (foundRows.Length > 0)
                row = foundRows[0];
            return row;
        }

		public DataRow GetBlogTagByName(string tagName)
		{
			DataRow row = null;
			DataRow[] foundRows = BlogTags.Select(string.Format("Name = '{0}'", tagName));
			if (foundRows.Length > 0)
				row = foundRows[0];
			return row;
		}

        public ArrayList GetTags(string id)
        {
            DataRow entry = GetBlogEntry(id);
            return (ArrayList)entry["Tags"];
        }

		public string[] GetTagsAsTextForBlogPostId(string blogPostId)
		{
			List<string> tagTexts = new List<string>();
			foreach(string tagGuid in GetTags(blogPostId))
			{
				tagTexts.Add(GetBlogTag(tagGuid)["Name"].ToString());
			}
			return tagTexts.ToArray();
		}

        #endregion

        #region Configuration

        public DataTable ConfigurationData
        {
            get { return _data.Entries.Tables["BlogConfiguration"]; }
        }

        public object getConfiguration(string key)
        {
            return ConfigurationData.Rows[0][key];
        }

        #endregion

        #region functions

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            string url = string.Empty;
            if (page.VirtualPath != string.Empty)
                url = page.VirtualPath.ToLower() + "?detail={1}#{2}";
            else
                url = "~/default.aspx?pg={0}&detail={1}#{2}";
            

            List<SearchResult> foundResults = new List<SearchResult>();
            DataRow[] foundRows = BlogEntries.Select(string.Format("Title LIKE '%{0}%' OR Content LIKE '%{0}%'", searchString.Replace("'", "''")));
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(
                    new SearchResult(
                        string.Format(url, page.PageId, row["Guid"], SectionId),
                        (string)row["Title"],
                        SearchResult.CreateExcerpt(SearchResult.RemoveHtml((string)row["Content"]), searchString)
                    )
                );
            }

            //tags search
            string guid = string.Empty;

            //get ids of searched tag
            foundRows = BlogTags.Select(string.Format("Name = '{0}'", searchString));
            if (foundRows.Length > 0)
            {
                guid = foundRows[0]["guid"].ToString();
            }
            if (guid != string.Empty)
            {
                foreach (DataRow row in BlogEntries.Rows)
                {
                    ArrayList tags = (ArrayList)row["Tags"];
                    if (tags.Contains(guid))
                    {
                        foundResults.Add(
                            new SearchResult(
                                string.Format("~/Default.aspx?pg={0}&detail={1}#{2}", page.PageId, row["Guid"], SectionId),
                                (string)row["Title"],"{" + searchString + "}"
                            )
                        );
                    }
                }
            }

            return foundResults;
        }

        private bool IsRenderedToSidebar()
        {
            // Get the current stack frame, and skip the first two frames.
            System.Diagnostics.StackFrame frame = new System.Diagnostics.StackFrame(3, false);
            return frame.GetMethod().Name.Equals("GetRSSFeed"); // -> Method, which returns xml for Sidebar html control (App_Code/Sidebar.cs)
        }

        #endregion

        #region RssMethods

        public ChannelData GetSidebarRss(string PageId)
        {
            ChannelData elements = new ChannelData();
            elements.Title = getConfiguration("BlogName").ToString();

            DataRow[] foundRows = BlogEntries.Select(null, "Updated desc");

            int iPosition = 0;
            foreach (DataRow row in foundRows)
            {
                //show top 5 entries in Sidebar, or all in RSS Feed
                if (!IsRenderedToSidebar() || iPosition < 5)
                {
                    Dictionary<string, string> item = new Dictionary<string, string>();
                    foreach (string rsskey in Enum.GetNames(typeof(RssElements)))
                    {
                        insertRssKeyValue(rsskey, row, PageId, ref item);
                    }
                    elements.ChannelItems.Add(item);
                }
                if (IsRenderedToSidebar()) iPosition++;
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
                    value = HttpContext.Current.Server.HtmlDecode((string)row["Content"]);
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }

        #endregion

        public struct BlogData
        {
            public DataSet Entries;
        }
    }
}

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
    /// Summary description for TagCloud
    /// </summary>
    public class TagCloud : Section<TagCloud.TagCloudData>, ISidebarObject
    {
        #region constructor

        private int _cacheMinutes = 60;

        public TagCloud()
        {
            //configuration
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("TagCloud"));
            _data.Entries.Tables["TagCloud"].Columns.AddRange(
              new DataColumn[] 
                {
                    new DataColumn("Title", typeof(string)),
                }
             );

            //set the default value
            DataRow row = null;
            row = ConfigurationData.NewRow();
            row["Title"] = Resources.StringsRes.ctl_TagCloud_DefaultTitle;
            ConfigurationData.Rows.Add(row);
            ConfigurationData.AcceptChanges();

        }

        private ArrayList Blogs
        {
            get
            {
                ArrayList blogs = new ArrayList();
                //looks for all files in the blog folder
                string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/App_Data/Blog/"));
                if (files.Length != 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string id = files[i].Substring(files[i].LastIndexOf("\\") + 1);
                        id = id.Substring(0, id.LastIndexOf("."));
                        Blog blog = (Blog)Activator.CreateInstance(typeof(Blog), id);
                        blogs.Add(blog);
                    }
                }
                return blogs;
            }
        }

        public TagCloud(string id) : base(id) { }

        #endregion

        #region Configuration

        public DataTable ConfigurationData
        {
            get { return _data.Entries.Tables["TagCloud"]; }
        }

        public string getCloudTitle()
        {
            return ConfigurationData.Rows[0]["Title"].ToString();
        }

        #endregion

        public List<DataRow> TagsMerged()
        {
            List<DataRow> listTags = new List<DataRow>();

            if (HttpContext.Current.Cache["TagCloudList"] != null)
            {
                listTags = (List<DataRow>)HttpContext.Current.Cache["TagCloudList"];
            }
            else
            {
                DataTable Tags = new DataTable("Tags");
                ArrayList arrTagsStored = new ArrayList();
                int intTagNumberTotal;

                foreach (Blog blog in Blogs)
                {
                    Tags.Merge(blog.BlogTags);
                }

                if (Tags.Rows.Count > 0)
                {
                    for (int y = 0; y < Tags.Rows.Count; y++)
                    {
                        //check if the Tag has already been added to the generic list
                        if (!arrTagsStored.Contains(Tags.Rows[y]["Name"]))
                        {
                            intTagNumberTotal = 0;
                            DataRow[] rowsFound = Tags.Select(string.Format("Name = '{0}'", Tags.Rows[y]["Name"]));
                            for (int i = 0; i < rowsFound.Length; i++)
                            {
                                intTagNumberTotal += (int)rowsFound[i]["Number"];
                            }
                            if (intTagNumberTotal > 0)
                            {
                                Tags.Rows[y]["Number"] = intTagNumberTotal;
                                listTags.Add(Tags.Rows[y]);
                                arrTagsStored.Add(Tags.Rows[y]["Name"]);
                            }
                        }
                    }
                }

                //insert the data into the cache
                HttpContext.Current.Cache.Insert("TagCloudList", listTags, null, DateTime.Now.AddMinutes(_cacheMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return listTags;
        }

        #region functions

        public static void reloadTagCloud()
        {
            HttpContext.Current.Cache.Remove("TagCloudList");
            HttpContext.Current.Cache.Remove("TagCloud");
        }

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            return new List<SearchResult>();
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
            elements.Title = getCloudTitle();

            List<DataRow> listTags = TagsMerged();

            //sorting the TagCloud desc
            //listTags.Sort(delegate(DataRow row1, DataRow row2)
            //    {
            //        return row2["Number"].ToString().CompareTo(row1["Number"].ToString());
            //    }
            //);

            int iPosition = 0;
            foreach (DataRow row in listTags)
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
                    value = (string)row["Name"];
                    break;
                case "link":
                    value = string.Format("~/BlogSearch.aspx?tag={0}", row["Name"]);
                    break;
                case "description":
                    value = HttpContext.Current.Server.HtmlDecode((string)row["Description"]);
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }

        #endregion

        public struct TagCloudData
        {
            public DataSet Entries;
        }
    }
}
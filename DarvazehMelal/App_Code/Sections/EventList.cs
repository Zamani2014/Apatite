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
using System.Globalization;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Event List Section
    /// This section displays a list of events
    /// Only events with a display date that lies in the past are displayed
    /// </summary>
    public class EventList : Section<EventList.EventListData>, ISidebarObject
    {
        public EventList()
        {
            _data.Entries = new DataSet();
            _data.Entries.Tables.Add(new DataTable("EventListEntries"));

            DataColumn primaryKeyColumn = new DataColumn("Guid", typeof(Guid));
            primaryKeyColumn.Unique = true;

            _data.Entries.Tables["EventListEntries"].Columns.Add(primaryKeyColumn);
            _data.Entries.Tables["EventListEntries"].PrimaryKey = new DataColumn[] { primaryKeyColumn };

            _data.Entries.Tables["EventListEntries"].Columns.AddRange(
                new DataColumn[] 
                {
                    new DataColumn("EventName", typeof(string)),
                    new DataColumn("EventText", typeof(string)),
                    new DataColumn("EventLocation", typeof(string)),
                    new DataColumn("EventDate", typeof(DateTime)),
                    new DataColumn("DisplayDate", typeof(DateTime))
                }
           );
        }
        public EventList(string id) : base(id) { }

        /// <summary>
        /// The DataTable with all events
        /// </summary>
        public DataTable EventEntries
        {
            get { return _data.Entries.Tables["EventListEntries"]; }
        }

        /// <summary>
        /// Returns a DataView with all events that have a DisplayDate in the past and EventDate in the future (you can change behaviour by using the withPastEvents-parameter)
        /// </summary>
        /// <param name="withPastEvents">If true, this method also list events with EventDate in the past</param>
        public DataView GetEventEntriesReadonly(bool withPastEvents)
        {
            string DateNow = DateTime.Today.ToString("MM/dd/yyyy");

            if (withPastEvents)
                return new DataView(EventEntries, string.Format("DisplayDate <= #{0}#", DateNow) , "EventDate ASC", DataViewRowState.CurrentRows);
            else
                return new DataView(EventEntries, string.Format("EventDate >= #{0}# AND DisplayDate <= #{0}#", DateNow), "EventDate ASC", DataViewRowState.CurrentRows);
     
        }

        /// <summary>
        /// Returns a DataView with all events (no matter if EventDate and/or DisplayDate lies in the past)
        /// </summary>
        public DataView GetEventEntriesEdit()
        {
            return new DataView(EventEntries, string.Empty, "EventDate ASC", DataViewRowState.CurrentRows);
        }

        /// <summary>
        /// Returns the DataRow for the given Guid
        /// </summary>
        /// <param name="id">Guid of the record to return</param>
        public DataRow GetEventEntry(string id)
        {
            DataRow row = null;
            DataRow[] foundRows = EventEntries.Select(string.Format("Guid = '{0}'", id));
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

            //DataTable.Select does not always 
            string DateNow = DateTime.Today.ToString("MM/dd/yyyy");

            //displaydate must be in the past
            List<SearchResult> foundResults = new List<SearchResult>();
            foreach (DataRow row in EventEntries.Rows)
            {
                row["EventText"] = HttpUtility.HtmlDecode(SearchResult.RemoveHtml(row["EventText"].ToString()));
            }
            DataRow[] foundRows = EventEntries.Select(string.Format("(EventName LIKE '%{0}%' OR EventText LIKE '%{0}%' OR EventLocation LIKE '%{0}%') AND EventDate >= #{1}#", searchString.Replace("'", "''"), DateNow), "EventDate ASC");
            foreach (DataRow row in foundRows)
            {
                foundResults.Add(new SearchResult(
                    string.Format(url, page.PageId, SectionId),
                    (string)row["EventName"],
                    SearchResult.CreateExcerpt((string)row["EventText"], searchString)
                    )
                );
            }
            return foundResults;
        }

        public ChannelData GetSidebarRss(string PageId)
        {
            ChannelData elements = new ChannelData();

            string DateNow = DateTime.Today.ToString("MM/dd/yyyy");

            DataRow[] foundRows = EventEntries.Select(string.Format("EventDate >= #{0}# AND DisplayDate <= #{0}#", DateNow), "EventDate ASC");
            int iPosition = 0;
            foreach (DataRow row in foundRows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                iPosition = 0;
                foreach (string rsskey in Enum.GetNames(typeof(RssElements)))
                {
                    InsertRssKeyValue(rsskey, row, PageId,  ref item);
                    iPosition++;
                }

                elements.ChannelItems.Add(item);
            }
            return elements;
        }

        private void InsertRssKeyValue(string RssKey, DataRow row, string PageId, ref Dictionary<string, string> elements)
        {
            string value = string.Empty;
            
            switch (RssKey)
            {
                case "title":
                    value = (string)row["EventName"];
                    break;
                 case "link":
                    value = string.Format("~/Default.aspx?pg={0}&detail={1}#{2}", PageId, row["Guid"], SectionId);
                    break;
                case "comment":
                    break;
                case "description":
                    value = "<b>" + ((DateTime)row["EventDate"]).ToShortDateString() + ", " + (string)row["EventLocation"] + "</b><br />" +   HttpContext.Current.Server.HtmlDecode((string)row["EventText"]);
                    break;
            }

            if (value != string.Empty)
                elements.Add(RssKey, value);
        }

        public struct EventListData
        {
            public DataSet Entries;
        }
    }
}

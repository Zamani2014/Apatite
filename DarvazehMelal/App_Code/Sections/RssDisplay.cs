//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using MyWebPagesStarterKit;
using System.Xml;

namespace MyWebPagesStarterKit
{
	/// <summary>
	/// RSS Display Section
	/// This section displays a rss feed
	/// </summary>
	public class RssDisplay : Section<RssDisplay.RssDisplayData>
	{
		public RssDisplay() : base() { }
		public RssDisplay(string id) : base(id) { }

		public string FeedUrl
		{
			get { return _data.FeedUrl; }
			set { _data.FeedUrl = value; }
		}

		public int MaxItems
		{
			get { return _data.MaxItems; }
			set { _data.MaxItems = value; }
		}

		public override List<SearchResult> Search(string searchString, WebPage page)
		{
			return new List<SearchResult>();
		}

		public class RssDisplayData
		{
			public RssDisplayData()
			{
				FeedUrl = string.Empty;
				MaxItems = 10;
			}

			public string FeedUrl;
			public int MaxItems;
		}
	}
}
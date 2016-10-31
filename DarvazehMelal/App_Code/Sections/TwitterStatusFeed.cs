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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Xml.XPath;

namespace MyWebPagesStarterKit
{
	/// <summary>
	/// TwitterStatusFeed Section
	/// This section displays the status updates (tweets) for a Twitter user.
	/// </summary>
	public class TwitterStatusFeed : Section<TwitterStatusFeed.TwitterStatusFeedData>, ISidebarObject
	{
		public TwitterStatusFeed() : base() { }
		public TwitterStatusFeed(string id) : base(id) { }

		/// <summary>
		/// Twitter-Username for which the tweets should be polled.
		/// </summary>
		public string Username
		{
			get { return _data.Username; }
			set { _data.Username = value; }
		}

		/// <summary>
		/// The xsl template to use for conversion of the Twitter-API-XML to HTML.
		/// Templates are located in the "TwitterStatusFeedTemplates" folder in the website's root.
		/// </summary>
		public string XslTemplateName
		{
			get { return _data.XslTemplateName; }
			set { _data.XslTemplateName = value; }
		}

		/// <summary>
		/// Maximum amount of tweets to return
		/// </summary>
		public int MaxItemsToShow
		{
			get { return _data.MaxItemsToShow; }
			set { _data.MaxItemsToShow = value; }
		}

		/// <summary>
		/// Gets the latest tweets from the Twitter-API for the provided Username.
		/// </summary>
		/// <param name="invalidateCache">The returned XmlDocument is cached for five minutes. If you want to invalidate the cache and get the latest xml from the Twitter-API pass true here.</param>
		public XPathNavigator GetTwitterXml(bool invalidateCache)
		{
			string xmlUrl = string.Format("http://twitter.com/statuses/user_timeline.xml?screen_name={0}&count={1}", Username, MaxItemsToShow);

			if (!invalidateCache && (HttpContext.Current.Cache[xmlUrl] != null))
			{
				return ((XmlDocument)HttpContext.Current.Cache[xmlUrl]).CreateNavigator();
			}
			else
			{
				XmlDocument document = new XmlDocument();
				XPathNavigator navigator;

				using (XmlTextReader reader = new XmlTextReader(xmlUrl))
				{
					document.Load(reader);
					navigator = document.CreateNavigator();
					formatXmlDocument(navigator);
				}
				HttpContext.Current.Cache.Add(xmlUrl, document, null, DateTime.MaxValue, TimeSpan.FromMinutes(5), System.Web.Caching.CacheItemPriority.Normal, null);
				return navigator;
			}
		}

		/// <summary>
		/// Formats the xml document returned from the twitter-API.
		/// Date is converted to a localized string, Usernames and URLs are surrounded with corresponding anchor-tags.
		/// </summary>
		private void formatXmlDocument(XPathNavigator doc)
		{
			foreach (XPathNavigator node in doc.Select("//created_at"))
			{
				DateTime createdAt = DateTime.ParseExact(node.Value, "ddd MMM dd HH:mm:ss zzzzz yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
				node.SetValue(createdAt.ToShortDateString() + "&nbsp;" + createdAt.ToShortTimeString());
			}

			foreach (XPathNavigator node in doc.Select("//text"))
			{
				node.SetValue(formatUsernames(formatUrls(node.Value)));
			}
		}

		/// <summary>
		/// Creates links (anchor-tags) for substrings of input which are possibly twitter-usernames (start with @, followed by characters or underscores)
		/// </summary>
		private string formatUsernames(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			return Regex.Replace(
				input,
				@"(?<=@)[A-z0-9_]+",
				delegate(Match m)
				{
					if (m.Success)
					{
						return m.Result(
							string.Format(
								"<a href=\"http://twitter.com/{0}\" target=\"_blank\" class=\"twitterUsername\">{0}</a>",
								m.Value
							)
						);
					}
					return m.Result(m.Value);
				},
				RegexOptions.Compiled | RegexOptions.IgnoreCase
			);
		}

		/// <summary>
		/// Puts anchor-tags with target=_blank around all substrings of input that look like URLs (start with http:// or https://)
		/// </summary>
		private string formatUrls(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			return Regex.Replace(
				input,
				@"(?<!href="")(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?",
				delegate(Match m)
				{
					if (m.Success)
					{
						return m.Result(
							string.Format(
								"<a href=\"{0}\" target=\"_blank\" class=\"twitterUrl\">{0}</a>",
								m.Value
							)
						);
					}
					return m.Result(m.Value);
				},
				RegexOptions.Compiled | RegexOptions.IgnoreCase
			);
		}

		#region ISidebarObject Members
		public ChannelData GetSidebarRss(string PageId)
		{
			ChannelData elements = new ChannelData();
			XPathNavigator twitterDoc = GetTwitterXml(false);
			int i = 0;
			foreach (XPathNavigator status in twitterDoc.Select("/statuses/status"))
			{
				string text = status.SelectSingleNode("text").Value;
				text = Regex.Replace(text, @"<[^>]+>", string.Empty); //remove all tags
				text = Regex.Replace(text, @"http[s]?://[\S]*", string.Empty); //remove all tags

				Dictionary<string, string> item = new Dictionary<string, string>();
				item.Add(RssElements.link.ToString(), string.Format("~/Default.aspx?pg={0}#{1}", PageId, SectionId));
				item.Add(RssElements.title.ToString(), text);
				item.Add(RssElements.description.ToString(), string.Empty);
				elements.ChannelItems.Add(item);

				i++;
				if (i >= 3)
					break;
			}
			return elements;
		}
		#endregion

		public override List<SearchResult> Search(string searchString, WebPage page)
		{
			return new List<SearchResult>();
		}

		public class TwitterStatusFeedData
		{
			public TwitterStatusFeedData()
			{
				Username = string.Empty;
				XslTemplateName = string.Empty;
				MaxItemsToShow = 5;
			}

			public string Username;
			public string XslTemplateName;
			public int MaxItemsToShow;
		}
	}
}
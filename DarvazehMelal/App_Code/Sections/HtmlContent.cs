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
    public class HtmlContent : Section<HtmlContent.HtmlContentData>
    {
        public HtmlContent() : base() { }
        public HtmlContent(string id) : base(id) { }

        public string Html
        {
            get { return _data.Html; }
            set { _data.Html = value; }
        }

		public override List<SearchResult> Search(string searchString, WebPage page)
        {
            string url = string.Empty;
            if (page.VirtualPath != string.Empty)
                url = page.VirtualPath.ToLower() + "#{1}";
            else
                url = "~/Default.aspx?pg={0}#{1}";

            List<SearchResult> foundResults = new List<SearchResult>();
            if ((HttpUtility.HtmlDecode(SearchResult.RemoveHtml(Html))).IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase) > -1)
                foundResults.Add(new SearchResult(
                    string.Format(url, page.PageId, SectionId),
                    page.Title,
                    SearchResult.CreateExcerpt((HttpUtility.HtmlDecode(SearchResult.RemoveHtml(Html))), searchString)
                    )
                );

            return foundResults;
        }

        public class HtmlContentData
        {
            public HtmlContentData()
            {
                Html = string.Empty;
            }

            public string Html;
        }
    }
}

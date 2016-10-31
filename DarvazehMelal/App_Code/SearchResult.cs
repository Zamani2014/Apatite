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
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// This class is used for the search-functionality. A search returns a List of SearchResults
    /// </summary>
    public class SearchResult
    {
        private string _url;
        private string _title;
        private string _excerpt;

        /// <summary>
        /// Creates a new SearchResult with the given parameters
        /// </summary>
        /// <param name="url">Url of the found page/section</param>
        /// <param name="title">Title of the found page/section</param>
        /// <param name="excerpt">Excerpt of the found text</param>
        public SearchResult(string url, string title, string excerpt)
        {
            _url = url;
            _title = title;
            _excerpt = excerpt;
        }

        /// <summary>
        /// Url of the found page/section
        /// </summary>
        public string Url
        {
            get { return _url; }
        }

        /// <summary>
        /// Title of the found page/section
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// Excerpt of the found text
        /// </summary>
        public string Excerpt
        {
            get { return _excerpt; }
        }

        /// <summary>
        /// Utility method to generate the excerpt of a SearchResult
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="keyword">The search term</param>
        /// <returns>A string that contains the keyword an 20 characters of the source before and after the source. Also the source is highlighted by using the &lt;strong&gt; tag</returns>
        public static string CreateExcerpt(string source, string keyword)
        {
            string excerpt = string.Empty;
            int charsBeforeAndAfter = 20;
            //find the first occurence of the keyword
            int index = source.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase);
            if (index >= 0)
            {
                int excerptStartIndex = 0;
                int excerptEndIndex = source.Length - 1;

                // Move start index back to include the amount of charsBeforeAndAfter characters
                if (index > (charsBeforeAndAfter - 1))
                    excerptStartIndex = index - charsBeforeAndAfter;
                if ((index + keyword.Length + charsBeforeAndAfter) < (source.Length - 1))
                    excerptEndIndex = index + keyword.Length + charsBeforeAndAfter;

                excerpt = source.Substring(excerptStartIndex, excerptEndIndex - excerptStartIndex + 1);

                //highlight the keyword with <strong> tag
                index = excerpt.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase);
                excerpt = excerpt.Insert(index + keyword.Length, "</strong>");
                excerpt = excerpt.Insert(index, "<strong>");
                excerpt = string.Format("...{0}...", excerpt);

            }
            return excerpt;
        }

        /// <summary>
        /// Strips all html-tags from the given source and returns the "clean" text
        /// </summary>
        /// <param name="source">Source-string that could contain html-tags</param>
        /// <returns>Html-tag-free string</returns>
        public static string RemoveHtml(string source)
        {
            if (source == null)
                return string.Empty;

            MatchEvaluator myEvaluator = new MatchEvaluator(removeTags);
            source = Regex.Replace(source, "<[^>]*>", myEvaluator).Trim();
            while (source.IndexOf("  ") >= 0)
            {
                source = source.Replace("  ", " ");
            }
            return source;
        }

        private static string removeTags(Match m)
        {
            return " ";
        }
    }
}

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
using System.Text;
using System.Text.RegularExpressions;

namespace MyWebPagesStarterKit
{
	/// <summary>
	/// Provides methods for formatting text (useful for cleaning user generated content)
	/// </summary>
	public static class TextFormatter
	{
		private static Dictionary<string, string> _slugDictionary;

		static TextFormatter()
		{
			buildUrlSlugReplacementDictionary();
		}

		#region HTML Formatting
		/// <summary>
		/// Replaces Environment.Newline with BR-Tags and removes repeated occurrences of br tags. 
		/// A maximum of two br tags in direct sequence is allowed.
		/// </summary>
		/// <param name="input">input text</param>
		/// <returns>the formatted input text</returns>
		public static string HtmlFormatNewlines(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			//replace newlines with html linebreaks
			input = Regex.Replace(input, @"\r\n|\n|\r", "<br/>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			//replace any sequence of at least 3 <br/> tags with two single <br/>s
			input = Regex.Replace(input, @"(\<br\s*/?\>\s*){3,999}", "<br/><br/>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			return input;
		}

		/// <summary>
		/// Removes all HTML Tags (but not their content) from the given string
		/// </summary>
		/// <param name="input">The string from which you want the tags to be removed</param>
		/// <param name="tagsToPreserve">A list of tag names (like "a", "strong", "em" etc.) which should NOT be removed</param>
		/// <returns>The HTML-Tag-Free string</returns>
		public static string RemoveHtmlTags(string input, params string[] tagsToPreserve)
		{
			string expression = @"<[^>]*>";

			if (string.IsNullOrEmpty(input))
				return string.Empty;

			if (tagsToPreserve.Length == 0)
			{
				return Regex.Replace(input, expression, string.Empty);
			}
			else
			{
				return Regex.Replace(
					input,
					expression,
					delegate(Match m)
					{
						if (m.Success)
						{
							foreach (string tagName in tagsToPreserve)
							{
								if (Regex.IsMatch(m.Value, string.Format(@"^<\/?{0}(?=\s|/|>)", tagName), RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase))
									return m.Result(m.Value);
							}
							return m.Result(string.Empty);
						}
						return m.Result(m.Value);
					}
				);
			}
		}

		/// <summary>
		/// Replaces all urls of the format http://link or https://link with an anchor tag and target="_blank"
		/// </summary>
		/// <param name="input">input text</param>
		/// <returns>input text with anchor tags around the urls</returns>
		public static string FormatUrls(string input)
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
								"<a href=\"{0}\" target=\"_blank\">{0}</a>",
								m.Value
							)
						);
					}
					return m.Result(m.Value);
				},
				RegexOptions.Compiled | RegexOptions.IgnoreCase
			);

		}
		#endregion

		#region Url Formatting
		/// <summary>
		/// Generates a url slug for a permalink (e.g. replaces ä with ae, removes all non-url-conform characters). 
		/// After creating the slug for a permalink, you might want to check, if the returned slug is already used 
		/// for another document in your project (e.g. by looking up the existing slugs in the database), so that the permalink is unique.
		/// </summary>
		/// <param name="input">the text to be converted to a slug</param>
		/// <returns>a unique url slug</returns>
		public static string GeneratePermalinkUrl(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			input = input.Trim();
			//first do all known replacements
			foreach (string key in _slugDictionary.Keys)
			{
				input = input.Replace(key, _slugDictionary[key]);
			}

			//remove all remaining special characters
			input = Regex.Replace(input, @"[^A-z0-9\-]", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			//remove multiple dashes
			input = Regex.Replace(input, @"[-]{2,999}", "-", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			//remove any single-character parts (-a-)
			input = Regex.Replace(input, @"\-.\-", "-", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			// Ensure that the beginning and the end of the "input" string does not have dashes
			while (input.StartsWith("-"))
				input = input.Substring(1);
			while (input.EndsWith("-"))
				input = input.Substring(0, input.Length - 1);

			return input;
		}
		#endregion

		#region 3rd Party Services Formatting
		/// <summary>
		/// Replace Youtube URLs (http://www.youtube.com/watch?v=TuWRJrDsKMM) with a corresponding object/embed tag
		/// </summary>
		/// <param name="input">input text</param>
		/// <param name="width">width of the movie</param>
		/// <param name="height">height of the movie</param>
		/// <returns>input text with replaced youtube links</returns>
		public static string CreateEmbedsFromYoutubeLinks(string input, int width, int height)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			input = Regex.Replace(
				input,
				@"[^\s>]+\.youtube\.com/watch.+v=([A-z0-9\-_]+)[^< ]*",
				delegate(Match m)
				{
					if (m.Success && m.Groups.Count == 2)
					{
						return m.Result(
							string.Format(@"<object width=""{1}"" height=""{2}"">
								<param name=""movie"" value=""http://www.youtube.com/v/{0}&hl=en&fs=1&rel=0""></param>
								<param name=""allowFullScreen"" value=""true""></param>
								<param name=""wmode"" value=""opaque""></param>
								<embed src=""http://www.youtube.com/v/{0}&hl=en&fs=1&rel=0"" type=""application/x-shockwave-flash"" allowfullscreen=""true"" width=""{1}"" height=""{2}"" wmode=""opaque""></embed>
							</object>", m.Groups[1], width, height)
						);
					}
					return m.Result(m.Value);
				},
				RegexOptions.Compiled | RegexOptions.IgnoreCase
			);

			return input;
		}
		#endregion

		#region private helpers
		private static void buildUrlSlugReplacementDictionary()
		{
			_slugDictionary = new Dictionary<string, string>();
			_slugDictionary.Add("Š", "S");
			_slugDictionary.Add("Ž", "Z");
			_slugDictionary.Add("š", "s");
			_slugDictionary.Add("ž", "z");
			_slugDictionary.Add("Ÿ", "Y");
			_slugDictionary.Add("À", "A");
			_slugDictionary.Add("Á", "A");
			_slugDictionary.Add("Â", "A");
			_slugDictionary.Add("Ã", "A");
			_slugDictionary.Add("Ä", "Ae");
			_slugDictionary.Add("Å", "A");
			_slugDictionary.Add("Ç", "C");
			_slugDictionary.Add("È", "E");
			_slugDictionary.Add("É", "E");
			_slugDictionary.Add("Ê", "E");
			_slugDictionary.Add("Ë", "E");
			_slugDictionary.Add("Ì", "I");
			_slugDictionary.Add("Í", "I");
			_slugDictionary.Add("Î", "I");
			_slugDictionary.Add("Ï", "I");
			_slugDictionary.Add("Ñ", "N");
			_slugDictionary.Add("Ò", "O");
			_slugDictionary.Add("Ó", "O");
			_slugDictionary.Add("Ô", "O");
			_slugDictionary.Add("Õ", "O");
			_slugDictionary.Add("Ö", "Oe");
			_slugDictionary.Add("Ø", "O");
			_slugDictionary.Add("Ù", "U");
			_slugDictionary.Add("Ú", "U");
			_slugDictionary.Add("Û", "U");
			_slugDictionary.Add("Ü", "Ue");
			_slugDictionary.Add("Ý", "Y");
			_slugDictionary.Add("à", "a");
			_slugDictionary.Add("á", "a");
			_slugDictionary.Add("â", "a");
			_slugDictionary.Add("ã", "a");
			_slugDictionary.Add("ä", "ae");
			_slugDictionary.Add("å", "a");
			_slugDictionary.Add("ç", "c");
			_slugDictionary.Add("è", "e");
			_slugDictionary.Add("é", "e");
			_slugDictionary.Add("ê", "e");
			_slugDictionary.Add("ë", "e");
			_slugDictionary.Add("ì", "i");
			_slugDictionary.Add("í", "i");
			_slugDictionary.Add("î", "i");
			_slugDictionary.Add("ï", "i");
			_slugDictionary.Add("ñ", "n");
			_slugDictionary.Add("ò", "o");
			_slugDictionary.Add("ó", "o");
			_slugDictionary.Add("ô", "o");
			_slugDictionary.Add("õ", "o");
			_slugDictionary.Add("ö", "oe");
			_slugDictionary.Add("ø", "o");
			_slugDictionary.Add("ù", "u");
			_slugDictionary.Add("ú", "u");
			_slugDictionary.Add("û", "u");
			_slugDictionary.Add("ü", "ue");
			_slugDictionary.Add("ý", "y");
			_slugDictionary.Add("ÿ", "y");
			_slugDictionary.Add("Þ", "");
			_slugDictionary.Add("þ", "");
			_slugDictionary.Add("Ð", "");
			_slugDictionary.Add("ð", "");
			_slugDictionary.Add("ß", "ss");
			_slugDictionary.Add("Œ", "Oe");
			_slugDictionary.Add("œ", "oe");
			_slugDictionary.Add("Æ", "Ae");
			_slugDictionary.Add("æ", "ae");
			_slugDictionary.Add("µ", "");
			_slugDictionary.Add("_", "-");
			_slugDictionary.Add("€", "euro");
			_slugDictionary.Add("$", "dollar");
			_slugDictionary.Add("£", "pound");
			_slugDictionary.Add(" ", "-");
		}
		#endregion
	}
}

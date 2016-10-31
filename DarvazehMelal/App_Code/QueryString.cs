﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Collections;

namespace MyWebPagesStarterKit
{
	/// <summary>
	/// A chainable query string helper class.
	/// Example usage :
	/// string strQuery = QueryString.Current.Add("id", "179").ToString();
	/// string strQuery = new QueryString().Add("id", "179").ToString();
	/// </summary>
	public class QueryString : NameValueCollection
	{
		public QueryString() { }

		public QueryString(string queryString)
		{
			FillFromString(queryString);
		}

		/// <summary>
		/// The querystring of the current page (works only if HttpContext is available)
		/// </summary>
		public static QueryString Current
		{
			get
			{
				return new QueryString().FromCurrent();
			}
		}

		/// <summary>
		/// extracts a querystring from a full URL
		/// </summary>
		/// <param name="s">the string to extract the querystring from</param>
		/// <returns>a string representing only the querystring</returns>
		public string ExtractQuerystring(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				if (s.Contains("?"))
					return s.Substring(s.IndexOf("?") + 1);
			}
			return s;
		}

		/// <summary>
		/// returns a querystring object based on a string
		/// </summary>
		/// <param name="s">the string to parse</param>
		/// <returns>the QueryString object </returns>
		public QueryString FillFromString(string s)
		{
			base.Clear();
			if (string.IsNullOrEmpty(s)) return this;
			foreach (string keyValuePair in ExtractQuerystring(s).Split('&'))
			{
				if (string.IsNullOrEmpty(keyValuePair)) continue;
				string[] split = keyValuePair.Split('=');
				base.Add(split[0],
					split.Length == 2 ? split[1] : "");
			}
			return this;
		}

		/// <summary>
		/// returns a QueryString object based on the current querystring of the request
		/// </summary>
		/// <returns>the QueryString object </returns>
		public QueryString FromCurrent()
		{
			if (HttpContext.Current != null)
			{
				if (HttpContext.Current.Request.RawUrl.LastIndexOf('?') >= 0)
					return FillFromString(HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.LastIndexOf('?')));
				else
					return FillFromString(string.Empty);
			}
			base.Clear();
			return this;
		}

		/// <summary>
		/// add a name value pair to the collection
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="value">the value associated to the name</param>
		/// <returns>the QueryString object </returns>
		public new QueryString Add(string name, string value)
		{
			return Add(name, value, false);
		}

		/// <summary>
		/// adds a name value pair to the collection
		/// </summary>
		/// <param name="name">the name</param>
		/// <param name="value">the value associated to the name</param>
		/// <param name="isUnique">true if the name is unique within the querystring. This allows us to override existing values</param>
		/// <returns>the QueryString object </returns>
		public QueryString Add(string name, string value, bool isUnique)
		{
			string existingValue = base[name];
			if (string.IsNullOrEmpty(existingValue))
				base.Add(name, HttpUtility.UrlEncodeUnicode(value));
			else if (isUnique)
				base[name] = HttpUtility.UrlEncodeUnicode(value);
			else
				base[name] += "," + HttpUtility.UrlEncodeUnicode(value);
			return this;
		}

		/// <summary>
		/// removes a name value pair from the querystring collection
		/// </summary>
		/// <param name="name">name of the querystring value to remove</param>
		/// <returns>the QueryString object</returns>
		public new QueryString Remove(string name)
		{
			string existingValue = base[name];
			if (!string.IsNullOrEmpty(existingValue))
				base.Remove(name);
			return this;
		}

		/// <summary>
		/// Remove a name value pair from the querystring collection (works also for parameters with multiple values)
		/// </summary>
		/// <param name="name">name of the querystring parameter</param>
		/// <param name="value">value of the querystring parameter</param>
		/// <returns>the QueryString object</returns>
		public QueryString Remove(string name, string value)
		{
			string existingValue = base[name];
			if (!string.IsNullOrEmpty(existingValue))
			{
				List<string> valueList = new List<string>(existingValue.Split(','));
				if (valueList.Contains(HttpUtility.UrlEncodeUnicode(value)))
					valueList.Remove(HttpUtility.UrlEncodeUnicode(value));
				if (valueList.Count > 0)
					base[name] = string.Join(",", valueList.ToArray());
				else
					base.Remove(name);
			}
			return this;
		}

		/// <summary>
		/// clears the collection
		/// </summary>
		/// <returns>the QueryString object </returns>
		public QueryString Reset()
		{
			base.Clear();
			return this;
		}

		/// <summary>
		/// overrides the default
		/// </summary>
		/// <param name="name"></param>
		/// <returns>the associated decoded value for the specified name</returns>
		public new string this[string name]
		{
			get
			{
				return HttpUtility.UrlDecode(base[name]);
			}
		}

		/// <summary>
		/// overrides the default indexer
		/// </summary>
		/// <param name="index"></param>
		/// <returns>the associated decoded value for the specified index</returns>
		public new string this[int index]
		{
			get
			{
				return HttpUtility.UrlDecode(base[index]);
			}
		}

		/// <summary>
		/// checks if a name already exists within the query string collection
		/// </summary>
		/// <param name="name">the name to check</param>
		/// <returns>a boolean if the name exists</returns>
		public bool Contains(string name)
		{
			string existingValue = base[name];
			return !string.IsNullOrEmpty(existingValue);
		}

		/// <summary>
		/// outputs the querystring object to a string
		/// </summary>
		/// <returns>the encoded querystring as it would appear in a browser</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < base.Keys.Count; i++)
			{
				if (!string.IsNullOrEmpty(base.Keys[i]))
				{
					foreach (string val in base[base.Keys[i]].Split(','))
						builder.Append((builder.Length == 0) ? "?" : "&").Append(HttpUtility.UrlEncodeUnicode(base.Keys[i])).Append("=").Append(val);
				}
			}
			return builder.ToString();
		}
	}
}

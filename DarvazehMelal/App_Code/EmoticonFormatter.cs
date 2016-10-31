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
using System.Xml;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for EmoticonFormatter
/// </summary>
public static class EmoticonFormatter
{
	private static Dictionary<string, Regex> emoticonMapping;

	static EmoticonFormatter()
	{
		emoticonMapping = new Dictionary<string, Regex>();

		XmlDocument emoticonConfig = new XmlDocument();
		emoticonConfig.Load(HttpContext.Current.Server.MapPath("~/Images/emoticons/emoticons.xml"));
		
		foreach (XmlNode emoticon in emoticonConfig.SelectNodes("/emoticons/emoticon"))
		{
			emoticonMapping.Add(emoticon.Attributes["image"].Value, new Regex(emoticon.Attributes["regex"].Value, RegexOptions.IgnoreCase));
		}
	}

	public static string FormatEmoticons(string input, string rootPrefix)
	{
		string formattedText = input;
		foreach (string image in emoticonMapping.Keys)
		{
			formattedText = emoticonMapping[image].Replace(formattedText, string.Format(@"<img src=""{0}Images/emoticons/{1}"" alt=""""/>", rootPrefix, image));
		}
		return formattedText;
	}
}

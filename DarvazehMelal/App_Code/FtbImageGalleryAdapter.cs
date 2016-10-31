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
using System.Web.UI;
using System.IO;
using System.Web.UI.Adapters;
using System.Text.RegularExpressions;

namespace MyWebPagesStarterKit
{
	public class FtbImageGalleryAdapter : ControlAdapter
	{
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			string userImageDirectory = HttpContext.Current.Server.MapPath("~/App_Data/UserImages/Image");
			if (!Directory.Exists(userImageDirectory))
				Directory.CreateDirectory(userImageDirectory);

			using (StringWriter sw = new StringWriter())
			{
				using (HtmlTextWriter tw = new HtmlTextWriter(sw))
				{
					base.Render(tw);

					string html = sw.ToString();

					string imagePath = VirtualPathUtility.ToAbsolute("~/App_Data/UserImages/Image");
					string newPath = VirtualPathUtility.ToAbsolute("~/ImageHandler.ashx") + "?UploadedFile=true&image=~/App_Data/UserImages/Image";

					html = Regex.Replace(html, @"(?<!FTB_GoToFolder\(.*?)" + imagePath, newPath);

					writer.Write(html);
				}
			}
		}
	}
}
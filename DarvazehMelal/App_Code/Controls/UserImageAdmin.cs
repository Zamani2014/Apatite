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
using FreeTextBoxControls;
using System.Collections;

namespace MyWebPagesStarterKit.Controls
{
	/// <summary>
	/// MWPSK makes use of the FreeTextBox control (http://www.freetextbox.com) and the corresponding image gallery for managing the user images.
	/// As all the user images are stored in ~/App_Data/UserImages/Image/ the starter kit uses an HTTP-handler to display the uploaded images because
	/// files in App_Data cannot be accessed directly from the web.
	/// Unfortunately, FreeTextBox does not support this behaviour by default. The UserImageAdmin control derives from the FreeTextBox's ImageGallery
	/// and alters the GetImages method, so that the method returns images with an imagehandler-path.
	/// 
	/// For some reason, the embedded resources of FreeTextBox (images and javascript) do not work anymore in a derived class.
	/// Therefore the UserImageAdmin sets the ResourceLocation to External (files are Located in the FTB folder in the root).
	/// </summary>
	public class UserImageAdmin : ImageGallery
	{
		protected override void OnInit(EventArgs e)
		{
			this.UtilityImagesLocation = ResourceLocation.ExternalFile;
			this.JavaScriptLocation = ResourceLocation.ExternalFile;
			this.SupportFolder = ResolveUrl("~/FTB/");
			base.OnInit(e);
		}

		

		public override System.Collections.ArrayList GetImages()
		{
			ArrayList imgs = base.GetImages();
			//loop through the images and correct the path if necessary
			foreach (ImageInfo info in imgs)
			{
				if (!info.AbsoluteWebPath.ToLower().Contains("imagehandler.ashx"))
				{
					string newPath = info.AbsoluteWebPath.Substring(ResolveUrl("~/").Length);
					info.AbsoluteWebPath = ResolveUrl("~/ImageHandler.ashx?UploadedFile=true&image=~/" + newPath);
				}

				if (!info.ThumbnailAbsoluteWebPath.ToLower().Contains("imagehandler.ashx"))
				{
					string newThumbnailPath = info.ThumbnailAbsoluteWebPath.Substring(ResolveUrl("~/").Length);
					info.ThumbnailAbsoluteWebPath = ResolveUrl("~/ImageHandler.ashx?UploadedFile=true&image=~/" + newThumbnailPath);
				}
			}
			return imgs;
		}

		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
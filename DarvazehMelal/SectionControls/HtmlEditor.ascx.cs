using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.IO;
using FreeTextBoxControls;

public partial class SectionControls_HtmlEditor : System.Web.UI.UserControl
{
	protected override void OnInit(EventArgs e)
	{
		string galleryServerPath = Server.MapPath("~/App_Data/UserImages/Image");
		string galleryWebPath = ResolveUrl("~/App_Data/UserImages/Image");

		if (!Directory.Exists(galleryServerPath))
			Directory.CreateDirectory(galleryServerPath);
		
		ftbEditor.ImageGalleryUrl = string.Format(ResolveUrl("~/ftb.imagegallery.aspx?rif={0}&cif={0}"), galleryWebPath);
		ftbEditor.ImageGalleryPath = galleryWebPath;
		ftbEditor.TextDirection = Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft ? TextDirection.RightToLeft : TextDirection.LeftToRight;
		
		//the FreeTextBox expects a custom culture (e.g. en-US for english).
		//As MWPSK has many specific cultures for the "same" language (e.g. en-US, en-GB, en-ZA) and FreeTextBox only supports one specific culture per language,
		//the following code does a replacement of the passed culture: according to the two-letter language name (country is ignored), the custom culture which
		//FreeTextBox supports is passed to the control. So for all custom cultures of MWPSK there is a unique "match" in the languages supported by FreeTextBox.
		switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToLower())
		{
			case "ar":
				ftbEditor.Language = "ar-SA";
				break;
			case "ca":
				ftbEditor.Language = "ca-CA";
				break;
			case "cs":
				ftbEditor.Language = "cs-CZ";
				break;
			case "da":
				ftbEditor.Language = "da-DK";
				break;
			case "de":
				ftbEditor.Language = "de-DE";
				break;
			case "el":
				ftbEditor.Language = "el-GR";
				break;
			case "es":
				ftbEditor.Language = "es-ES";
				break;
			case "fr":
				ftbEditor.Language = "fr-FR";
				break;
			case "he":
				ftbEditor.Language = "he-IL";
				break;
			case "hu":
				ftbEditor.Language = "hu-HU";
				break;
			case "it":
				ftbEditor.Language = "it-IT";
				break;
			case "ja":
				ftbEditor.Language = "ja-JP";
				break;
			case "ko":
				ftbEditor.Language = "ko-KR";
				break;
			case "nb":
				ftbEditor.Language = "nb-NO";
				break;
			case "nl":
				ftbEditor.Language = "nl-NL";
				break;
			case "pl":
				ftbEditor.Language = "pl-PL";
				break;
			case "pt":
				ftbEditor.Language = "pt-PT";
				break;
			case "ro":
				ftbEditor.Language = "ro-RO";
				break;
			case "ru":
				ftbEditor.Language = "ru-RU";
				break;
			case "sv":
				ftbEditor.Language = "sv-SE";
				break;
			case "sk":
				//use czech for now, as FTB does not contain a localization for slovak at the moment.
				ftbEditor.Language = "cs-CZ";
				//ftbEditor.Language = "sk-SK";
				break;
			case "zh":
				if (Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.ToLower() == "zh-tw")
					ftbEditor.Language = "zh-TW";
				else
					ftbEditor.Language = "zh-CN";
				break;
			default:
				ftbEditor.Language = "en-US";
				break;
		}
		
		base.OnInit(e);
	}

	public string Text
	{
		get { return ftbEditor.Text; }
		set { ftbEditor.Text = value; }
	}
}

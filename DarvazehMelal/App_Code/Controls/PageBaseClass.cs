//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Web.UI;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace MyWebPagesStarterKit.Controls
{
    public abstract class PageBaseClass : Page
    {
        public WebSite _website;

        public PageBaseClass(){}

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            _website = WebSite.GetInstance();

            if((_website.Theme == string.Empty) || (!Directory.Exists(Server.MapPath(string.Format("~/App_Themes/{0}", _website.Theme))))){
                string appThemesPath = Server.MapPath("~/App_Themes");
                string[] themes = Directory.GetDirectories(appThemesPath);
                if (themes.Length > 0)
                {
                    //if theme TravelDiary, select as default, else choose first theme
                    bool blnTravelDiary = false;
                    foreach (string theme in themes)
                    {
                        if (Path.GetFileName(theme) == "TravelDiary")
                            blnTravelDiary = true;
                    }
                    _website.Theme = blnTravelDiary == true ? "TravelDiary" : Path.GetFileName(themes[0]);
                }
                else
                {
                    _website.Theme = string.Empty;
                }
                _website.SaveData();
            }
            Theme = _website.Theme;
        }

        protected override void InitializeCulture()
        {
            // Initialize Resource Manager - with no effect if it's already been initialized
            string LocaleID = WebSite.GetInstance().LocaleID;
            UICulture = LocaleID;
            Culture = LocaleID;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LocaleID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LocaleID);
			//due to an error of freetextbox, all the cultures must use a dot as NumberDecimalSeparator			
			Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            
			base.InitializeCulture();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
			CultureInfo info = Thread.CurrentThread.CurrentUICulture;
			if (info != null && info.TextInfo.IsRightToLeft)
			{
                HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("body1");
				body.Attributes.Add("dir", "rtl");
            }
        }
    }
}

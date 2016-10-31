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
using System.ComponentModel;

namespace MyWebPagesStarterKit.Controls
{
    [ToolboxData("<{0}:ResizedImage runat=server></{0}:ResizedImage>")]
    public class ResizedImage : Image
    {
        public string SectionId
        {
            get { return (ViewState["SectionId"] == null) ? string.Empty : (string)ViewState["SectionId"]; }
            set { ViewState["SectionId"] = value; }
        }

        public string PageId
        {
            get { return (ViewState["PageId"] == null) ? string.Empty : (string)ViewState["PageId"]; }
            set { ViewState["PageId"] = value; }
        }

        public int MaxWidth
        {
            get { return (ViewState["MaxWidth"] == null) ? 50 : (int)ViewState["MaxWidth"]; }
            set { ViewState["MaxWidth"] = value; }
        }

        public int MaxHeight
        {
            get { return (ViewState["MaxHeight"] == null) ? 50 : (int)ViewState["MaxHeight"]; }
            set { ViewState["MaxHeight"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            GenerateEmptyAlternateText = true;

            if (SectionId != string.Empty)
            {
                ImageUrl = string.Format(
                    "~/ImageHandler.ashx?pg={0}&section={1}&image={2}&height={3}&width={4}",
                    PageId,
                    SectionId,
                    HttpUtility.UrlEncode(ImageUrl),
                    MaxHeight,
                    MaxWidth
                );
            }
            else
            {
                ImageUrl = string.Format(
                    "~/ImageHandler.ashx?image={0}&height={1}&width={2}",
                    HttpUtility.UrlEncode(ImageUrl),
                    MaxHeight,
                    MaxWidth
                );
            }
            
        }
    }
}
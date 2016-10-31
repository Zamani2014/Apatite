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
using System.IO;
using System.Collections.Generic;

namespace MyWebPagesStarterKit
{
    public class VideoContent : Section<VideoContent.VideoContentData>
    {
        public VideoContent() : base() { }
        public VideoContent(string id) : base(id) { }

        public bool AutoPlay
        {
            get { return _data.AutoPlay; }
            set { _data.AutoPlay = value; }
        }

        public bool Muted
        {
            get { return _data.Muted; }
            set { _data.Muted = value; }
        }

        public string Keywords
        {
            get { return _data.Keywords; }
            set { _data.Keywords = value; }
        }


        public string Size
        {
            get { return _data.Size; }
            set { _data.Size = value; }
        }

        public string Filename
        {
            get { return _data.Filename; }
            set { _data.Filename = value; }
        }

        public int Volume
        {
            get { return _data.Volume; }
            set { _data.Volume = value; }
        }


        private string txtData
        {
            get { return _data.Keywords; }
        }


        /// <summary>
        /// Directory to which files are uploaded
        /// </summary>
        public string UploadDirectory
        {
            get
            {
                string virtualdirectory = string.Format("~/App_Data/_Downloads/{0}", SectionId);
                string physicaldirectory = HttpContext.Current.Server.MapPath(virtualdirectory);
                if (!Directory.Exists(physicaldirectory))
                    Directory.CreateDirectory(physicaldirectory);
                return virtualdirectory;
            }
        }


        public override bool Delete()
        {
            try
            {
                Directory.Delete(HttpContext.Current.Server.MapPath(UploadDirectory), true);
            }
            catch
            {
                return false;
            }

            return base.Delete();
        }




        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            List<SearchResult> foundResults = new List<SearchResult>();
            if ((HttpUtility.HtmlDecode(SearchResult.RemoveHtml(this.txtData))).IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase) > -1)
                foundResults.Add(new SearchResult(
                    string.Format("~/Default.aspx?pg={0}#{1}", page.PageId, SectionId),
                    page.Title,
                    SearchResult.CreateExcerpt((HttpUtility.HtmlDecode(SearchResult.RemoveHtml(this.txtData))), searchString)
                    )
                );

            return foundResults;
        }

        public class VideoContentData
        {
            public VideoContentData()
            {
                AutoPlay = true;
                Muted = false;
                Keywords = string.Empty;
                Filename = string.Empty;
                Size = string.Empty;
                Volume = 10;
            }

            public bool AutoPlay;
            public bool Muted;
            public string Filename;
            public string Size;
            public string Keywords;
            public int Volume;

        }
    }
}

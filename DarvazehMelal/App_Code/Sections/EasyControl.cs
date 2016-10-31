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
using System.Collections.Generic;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Easy Control Section
    /// This section displays a UserControl from the folder "~/EasyControls"
    /// </summary>
    public class EasyControl : Section<EasyControl.EasyControlData>
    {
        public EasyControl(){}
        public EasyControl(string id) : base(id) { }

        /// <summary>
        /// Name of the UserControl (e.g. MyControl.ascx)
        /// </summary>
        public string ControlName
        {
            get { return _data.ControlName; }
            set { _data.ControlName = value; }
        }

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            return new List<SearchResult>();
        }

        public class EasyControlData
        {
            public EasyControlData()
            {
                ControlName = string.Empty;
            }

            public string ControlName;
        }

        #region Rss Methods
        
        #endregion
    }
}

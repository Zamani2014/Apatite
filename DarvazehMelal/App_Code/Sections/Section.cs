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

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Base class for all sections. Encapsulates the general section properties and methods.
    /// </summary>
    /// <typeparam name="T">Type of the section-data class (this type is just passed to Persistable&lt;T&gt;)
    public abstract class Section<T> : Persistable<T>, ISection
    {
        private string _sectionId;

        /// <summary>
        /// Creates a new section with a new section id (guid)
        /// </summary>
        public Section() : base()
        {
            _sectionId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Creates a section with the given section id. The data is loaded form the filesystem
        /// </summary>
        /// <param name="id">Guid of the section</param>
        public Section(string id)
        {
            _sectionId = id;
            LoadData();
        }

        /// <summary>
        /// The unique section identifier (guid)
        /// </summary>
        public string SectionId
        {
            get { return _sectionId.ToString(); }
        }

        public abstract List<SearchResult> Search(string searchString, WebPage page);

        /// <summary>
        /// Returns a path to the section's UserControl that is used to display the section in the WebPage
        /// </summary>
        public string UserControl
        {
            get { return string.Format("~/SectionControls/{0}.ascx", GetType().Name); }
        }

        /// <summary>
        /// Path to the data-file.
        /// </summary>
        /// <returns>The path to the data-file of the current section</returns>
        protected override string GetDataFilename()
        {
            //the path has always the format "~/App_Data/{sectiontypename}/{sectionid}.config"
            return string.Format("~/App_Data/{0}/{1}.config", GetType().Name, _sectionId);
        }

		public virtual string GetLocalizedSectionName()
		{
			return Resources.StringsRes.ResourceManager.GetString("ctl_" + GetType().Name + "_RssTitle") ?? string.Empty;
		}
    }
}

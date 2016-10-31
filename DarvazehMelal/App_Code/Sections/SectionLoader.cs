//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Reflection;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Utility class to load sections independent of their type. Implemented as singleton.
    /// </summary>
    public class SectionLoader
    {
        private static SectionLoader _instance;

        private SectionLoader() { }

        public static SectionLoader GetInstance()
        {
            if (_instance == null)
                _instance = new SectionLoader();
            return _instance;
        }

        /// <summary>
        /// Searches through the whole App_Data folder for the given section and tries to create a instance of ISection with the found data.
        /// </summary>
        /// <param name="id">SectionId</param>
        /// <returns>ISection</returns>
        public ISection LoadSection(string id)
        {
            //looks for all files matching {id}.config in the App_Data directory
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/App_Data"), string.Format("{0}.config", id), SearchOption.AllDirectories);
            if (files.Length != 0)
            {
                //As the directories for the sections are named like the class-names of their corresponding section, the type of the section can be acquired by using the directory-name.
                //after the next two lines, dir contains the type-name of the section for which the data-file was found.
                string dir = Path.GetDirectoryName(files[0]);
                dir = "MyWebPagesStarterKit." + dir.Substring(dir.LastIndexOf("\\") + 1);

                try
                {
                    //here we try to create a instance of the type-name contained in the variable "dir" and return it as ISection.
                    return (ISection)Activator.CreateInstance(this.GetType().Assembly.GetType(dir), id);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

		/// <summary>
		/// Loads all sections of the given type that are present in App_Data
		/// </summary>
		/// <typeparam name="T">Type of the sections to load</typeparam>
		/// <returns></returns>
		public List<T> LoadAllSectionsOfType<T>() where T : ISection
		{
			List<T> sections = new List<T>();

			string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath(string.Format("~/App_Data/{0}", typeof(T).Name)), "*.config", SearchOption.TopDirectoryOnly);
			if (files.Length != 0)
			{
				foreach(string file in files)
				{
					sections.Add((T)Activator.CreateInstance(typeof(T), Path.GetFileNameWithoutExtension(file)));
				}
			}
			return sections;
		}
    }
}

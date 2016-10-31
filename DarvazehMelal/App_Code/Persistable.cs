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
using System.IO;
using System.Xml.Serialization;
using System.Web.Caching;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Generic class for a perssitable object. Encapsulates the logic to load/save data from/to the filesystem. To speed up the acces, caching is used.
    /// </summary>
    /// <typeparam name="T">class or struct with all the data-fields that must be persisted</typeparam>
    public abstract class Persistable<T>
    {
        private String _path;

        protected T _data;

        /// <summary>
        /// Creates a instance of Persistable. Also creates a instance of T
        /// </summary>
        public Persistable()
        {
            _data = (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// Loads the data from the filesystem. For deserialization a XmlSeralizer is used.
        /// </summary>
        protected void LoadData()
        {
            _path = HttpContext.Current.Server.MapPath(GetDataFilename());
            lock (_path)
            {
                //first check, if the object is maybe already in the cache
                object o = HttpContext.Current.Cache[_path];
                if (o != null)
                {
                    _data = (T)o;
                }
                else
                {
                    //if nothing was found in the cache, the data must be loaded from the disk
                    //load and deserialize the data from the filesystem
                    using (FileStream reader = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        _data = (T)serializer.Deserialize(reader);
                    }
                    HttpContext.Current.Cache.Insert(_path, _data);
                }
            }
        }

        /// <summary>
        /// Persists the data back to the filesystem
        /// </summary>
        public void SaveData()
        {
            _path = HttpContext.Current.Server.MapPath(GetDataFilename());
            lock (_path)
            {
                //insert the data into the cache
                HttpContext.Current.Cache.Insert(_path, _data, null, DateTime.MaxValue, TimeSpan.FromHours(1), CacheItemPriority.Normal, null);

                //if the given path does not exist yet, create it
                if (!Directory.Exists(Path.GetDirectoryName(_path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(_path));
                 
                //serialize and store the data to the filesystem
                using (FileStream writer = File.Create(_path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, _data);
                }
            }
        }

        //Deletes the data from the cache and filesystem
        public virtual bool Delete()
        {
            bool success = true;

            if (File.Exists(_path))
            {
                lock (_path)
                {
                    try
                    {
                        File.Delete(_path);
                        HttpContext.Current.Cache.Remove(_path);
                    }
                    catch { success = false; }
                }
            }
            return success;
        }

        protected abstract string GetDataFilename();
    }
}

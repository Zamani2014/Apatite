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
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace MyWebPagesStarterKit.Controls
{
    [DefaultProperty("Nothing"), ToolboxData("<{0}:TagCloud runat=server></{0}:TagCloud>")]
    public class TagCloud : System.Web.UI.UserControl
    {
        private IEnumerable _dataSource = null;
        private TagCloudData[] finalizedData = null;

        private int _averageCloudSize = 0;
        private int _fontSizeRange = 0;
        private string _cloudTitle = string.Empty;

        private bool _cacheOutput = true;
        private int _cacheMinutes = 60;
        private int minInstances = 100000000;
        private int maxInstances = 0;

        private int tagCount = 0;

        public void Page_Load()
        {

        }

        public string Nothing
        {
            get { return null; }
            set { }
        }

        public IEnumerable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public int AverageCloudSize
        {
            get { return _averageCloudSize; }
            set
            {
                if (value > 0)
                    _averageCloudSize = value;
                else
                    _averageCloudSize = 0;
            }
        }

        public int CloudSizeRange
        {
            get { return _fontSizeRange; }
            set
            {
                if (value > 0)
                    _fontSizeRange = value;
                else
                    _fontSizeRange = 0;
            }
        }
        public string CloudTitle
        {
            get { return _cloudTitle; }
            set { _cloudTitle = value; }
        }

        public bool CacheOutput
        {
            get { return _cacheOutput; }
            set { _cacheOutput = value; }
        }

        public int CacheMinutes
        {
            get { return _cacheMinutes; }
            set
            {
                if (value > 0)
                    _cacheMinutes = value;
                else
                    _cacheMinutes = 0;
            }
        }

        public override void DataBind()
        {
            List<TagCloudData> transferData = new List<TagCloudData>();

            foreach (object obj in _dataSource)
            {
                if (obj.GetType() == typeof(DataRow))
                {
                    DataRow row = (DataRow)obj;
                    transferData.Add(new TagCloudData(row["Name"].ToString(), (int)row["Number"]));

                    if (transferData[transferData.Count - 1].Instances < minInstances)
                        minInstances = transferData[transferData.Count - 1].Instances;
                    if (transferData[transferData.Count - 1].Instances > maxInstances)
                        maxInstances = transferData[transferData.Count - 1].Instances;

                    tagCount++;
                }
                else
                    throw new ArgumentException("Argument must contain only TagCloudData objects.", "DataSource");
            }

            finalizedData = transferData.ToArray();
            _dataSource = null;
        }

        public bool HasCache()
        {
            return HttpContext.Current.Cache[GetCacheName()] != null;
        }

        protected override void Render(HtmlTextWriter output)
        {
            RenderTagCloud(output);
            base.Render(output);
        }

        protected void RenderTagCloud(HtmlTextWriter output)
        {
            if (_averageCloudSize <= 0)
                throw new ArgumentException("Argument must be non-zero and non-negative.", "AverageCloudSize");
            if (_fontSizeRange <= 0)
                throw new ArgumentException("Argument must be non-zero and non-negative.", "CloudSizeRange");
            if (_cacheOutput && _cacheMinutes <= 0)
                throw new ArgumentException("Argument must be non-zero and non-negative when caching is enabled.", "CacheMinutes");

            //caching
            if (_cacheOutput && HasCache())
            {
                output.Write((string)HttpContext.Current.Cache[GetCacheName()]);
                return;
            }

            if (finalizedData == null || finalizedData.Length == 0)
                return;

            CloudHasher cloudHasher = new CloudHasher(_fontSizeRange, new Pair(minInstances, maxInstances));
            int baseCloudSize = _averageCloudSize - (int)Math.Floor((double)(_fontSizeRange - 1) / 2);

            StringBuilder tagCloud = new StringBuilder();
            string htmlCloud = 
                "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"tagcloud\">" + Environment.NewLine + 
                "<tr><td class=\"title\">" + _cloudTitle +  "</td></tr>" +
                "<tr><td class=\"tags\">" + Environment.NewLine + "<ul class=\"tagcloudlist\">" + Environment.NewLine;
            tagCloud.Append(htmlCloud);

            foreach (TagCloudData tagCloudData in finalizedData)
            {
                int cloudSize = baseCloudSize + cloudHasher.GetHashCode(tagCloudData);

                string instanceString = tagCloudData.Instances + " Tag" + ((tagCloudData.Instances != 1) ? "s" : "");
                tagCloud.AppendFormat("<li class=\"cloud{3}\"><a href=\"{2}\" title=\"{1}\">{0}</a></li>" + Environment.NewLine, tagCloudData.Text, instanceString, "BlogSearch.aspx?tag=" + Server.UrlEncode(tagCloudData.Text), cloudSize);
            }
            tagCloud.Append("</ul></td></tr></table><div style=\"padding-top: 10px\"></div>");

            output.Write(tagCloud.ToString());

            if (_cacheOutput)
                HttpContext.Current.Cache.Insert(GetCacheName(), tagCloud.ToString(), null, DateTime.Now.AddMinutes(_cacheMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        private string GetCacheName()
        {
            return "TagCloud";
        }
    }


    public class TagCloudData
    {
        private int _instances = 0;
        private string _text = "";

        public TagCloudData() { }

        public TagCloudData(string text, int instances)
        {
            this.Text = text;
            this.Instances = instances;
        }

        public int Instances
        {
            get { return _instances; }
            set
            {
                if (value > 0)
                    _instances = value;
                else
                    _instances = value;
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value != null)
                    _text = value;
                else
                    _text = "";
            }
        }
    }

    public class CloudHasher : IEqualityComparer
    {
        private int buckets = 0;
        private Pair range = null;
        private double bucketWidth = 0;

        public CloudHasher(int _buckets, Pair _range)
        {
            if (_range == null)
                throw new ArgumentException("Range cannot by null.", "_range");
            if (_range.First.GetType() != typeof(System.Int32) || _range.Second.GetType() != typeof(System.Int32))
                throw new ArgumentException("Range must be an ordered pair of 32-bit integers.", "_range");
            if (_buckets < 1)
                throw new ArgumentException("Must have at least one bucket.", "_bucket");

            this.buckets = _buckets;
            this.range = _range;

            this.range.Second = ((int)this.range.Second) + 1;

            bucketWidth = ComputeBucketWidth();
        }

        private double ComputeBucketWidth()
        {
            int rangeWidth = (int)range.Second - (int)range.First;
            return (double)rangeWidth / buckets;
        }

        public new bool Equals(object x, object y)
        {
            if (x.GetType() != typeof(DataRow))
                throw new ArgumentException("This function can only compare objects of type TagCloudData.", "x");
            if (y.GetType() != typeof(DataRow))
                throw new ArgumentException("This function can only compare objects of type TagCloudData.", "y");

            TagCloudData tcX = x as TagCloudData;
            TagCloudData tcY = y as TagCloudData;

            if (tcX == null && tcY == null)
                return true;
            else if (tcX == null || tcY == null)
                return false;

            if (tcX.Text == tcY.Text && tcX.Instances == tcY.Instances)
                return true;

            return false;
        }

        public int GetHashCode(object obj)
        {
            if (obj == null)
                throw new ArgumentException("This function cannot provide a hash code for a null object.", "obj");
            if (obj.GetType() != typeof(TagCloudData))
                throw new ArgumentException("This function can only provide hash codes for objects of type TagCloudData.", "obj");

            TagCloudData tcObj = obj as TagCloudData;

            int zeroedInstances = tcObj.Instances - (int)range.First;
            int bucketNumber = (int)Math.Floor(zeroedInstances / bucketWidth);

            return bucketNumber;
        }
    }
}
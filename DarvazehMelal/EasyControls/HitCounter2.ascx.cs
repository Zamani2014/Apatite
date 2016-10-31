// -------------------------------------------------
// -- Easy Control: Hit Counter 2                 --
// -- Author:       Raimund Neumüller             --
// -- Web:          http://raimundo.dotnethost.at --
// -------------------------------------------------
//
// Version history: 
//    10.06.2008    Version 1.0 - Initial release
//    24.08.2008    Version 1.1 - Backup of XML file before updating
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using MyWebPagesStarterKit;
using System.Security;

public partial class EasyControls_HitCounter2 : System.Web.UI.UserControl
{
    private void DoTracking()
    {
        DoTracking(-1);
    }

    private void DoTracking(int test)
    {
        HitDataExt hitData = HitManager2.HitTracker(Page.Request, test);

        panelAgent.Visible = hitData.Settings.ViewAgent;
        panelHits.Visible = hitData.Settings.ViewHitCount;
        panelLastVisit.Visible = hitData.Settings.ViewLastVisit;
        panelSpacer.Visible = hitData.Settings.ViewSpacer;
        panelVisitors.Visible = hitData.Settings.ViewVisitorCount;
        //panelYourHostname.Visible = hitData.Settings.ViewHostName;
        panelYourIP.Visible = hitData.Settings.ViewIP;

        lblNumberOfHits.Text = hitData.HitCount.ToString();
        lblNumberOfVisitors.Text = hitData.VisitorData.Count.ToString();
        lblLastVisit.Text = hitData.LastVisit.ToString();
        if (hitData.CurrentVisitorItem != null)
        {
            VisitorDataItem visitorItem = hitData.CurrentVisitorItem;
            lblYourIP.Text = hitData.CurrentVisitorIP; //visitorItem.IP;
            //lblYourIP2.Text = Request.UserHostName;
            lblLastVisit.Text = visitorItem.LastVisit.ToString();
            lblAgent.Text = visitorItem.Agent;
        }        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool isAdminMode = false;
        try
        {
            isAdminMode = PowerUserExt.HasAdminRights(Page.User, null);
        }
        catch (Exception) { }

        panelAdmin.Visible = isAdminMode;

        if (isAdminMode)
        {
            HitCounterSettings settings = HitManager2.LoadSettings();
            chkViewAgent.Checked = settings.ViewAgent;
            chkViewHitCount.Checked = settings.ViewHitCount;
            chkViewIP.Checked = settings.ViewIP;
            chkViewLastVisit.Checked = settings.ViewLastVisit;
            chkViewSpacer.Checked = settings.ViewSpacer;
            chkViewVisitorCount.Checked = settings.ViewVisitorCount;
        }

        if (Page.IsPostBack)
            return;

        this.DoTracking();

        //for (int i = 0; i < 2000; i++)
        //{
        //    this.DoTracking(i);
        //}
    }
    protected void btnSaveSettings_Click(object sender, EventArgs e)
    {
        HitManager2.ApplySettings(chkViewSpacer.Checked, chkViewVisitorCount.Checked, chkViewHitCount.Checked, chkViewLastVisit.Checked, chkViewIP.Checked, chkViewAgent.Checked);
        this.DoTracking();
    }
}

[Serializable]
public class VisitorDataList : SerializableDictionary<string, VisitorDataItem> //SortedList<string, VisitorDataItem>
{
    public VisitorDataList() : base() { }
}

[Serializable]
public class VisitorDataItem
{
    public VisitorDataItem()
    {
    }
    public VisitorDataItem(/*string IP, string hostName, */string agent, DateTime lastVisit)
    {
        //this.IP = IP;
        //this.HostName = hostName;
        this.Agent = agent;
        this.LastVisit = lastVisit;
    }

    //public string IP;
    //public string HostName;
    public DateTime LastVisit;
    public string Agent;
}

[Serializable]
public class HitCounterSettings
{
    public HitCounterSettings()
    {
    }
    public bool ViewSpacer = true;
    public bool ViewVisitorCount = true;
    public bool ViewHitCount = true;
    public bool ViewLastVisit = true;
    public bool ViewIP = true;
    //public bool ViewHostName = true;
    public bool ViewAgent = true;
}

[Serializable]
public class HitDataExt
{
    public HitDataExt()
    {
    }
    public HitDataExt(int hitCount, DateTime lastVisit,VisitorDataList visitorData)
    {
        this.HitCount = hitCount;
        this.LastVisit = lastVisit;
        this.VisitorData = visitorData;
    }

    public HitCounterSettings Settings = new HitCounterSettings();
    public int HitCount;
    public DateTime LastVisit;
    public VisitorDataList VisitorData;

    [NonSerialized]
    public VisitorDataItem CurrentVisitorItem = null;
    [NonSerialized]
    public string CurrentVisitorIP = null;
}


public static class HitManager2
{ 
    private static object mutex = new object();

    private static string GetUserAgentString(HttpRequest Request)
    {
        return Request.UserAgent; //Request.Browser.Browser + " " + Request.Browser.Version;
    }

    private static string GetFilePath()
    {
        return HttpContext.Current.Server.MapPath(string.Format("~/App_Data/HITS2.config"));
    }

    public static HitCounterSettings LoadSettings()
    {
        HitDataExt hitData;        

        lock (mutex)
        {
            string _path = GetFilePath();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HitDataExt));

            if (File.Exists(_path))
            {
                StreamReader streamReader = new StreamReader(_path);
                hitData = xmlSerializer.Deserialize(streamReader) as HitDataExt;
                streamReader.Close();
            }
            else
            {
                hitData = new HitDataExt(0, DateTime.Now, new VisitorDataList());
            }
        }
        return hitData.Settings;
    }

    public static void ApplySettings(bool ViewSpacer, bool ViewVisitorCount, bool ViewHitCount, bool ViewLastVisit, bool ViewIP, bool ViewAgent)
    {
        HitDataExt hitData;

        lock (mutex)
        {
            string _path = GetFilePath();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HitDataExt));

            if (File.Exists(_path))
            {
                StreamReader streamReader = new StreamReader(_path);
                hitData = xmlSerializer.Deserialize(streamReader) as HitDataExt;
                streamReader.Close();
            }
            else
            {
                hitData = new HitDataExt(0, DateTime.Now, new VisitorDataList());
            }

            hitData.Settings.ViewAgent = ViewAgent;
            hitData.Settings.ViewHitCount = ViewHitCount;
            hitData.Settings.ViewIP = ViewIP;
            hitData.Settings.ViewLastVisit = ViewLastVisit;
            hitData.Settings.ViewSpacer = ViewSpacer;
            hitData.Settings.ViewVisitorCount = ViewVisitorCount;

            StreamWriter streamWriter = new StreamWriter(_path, false);
            xmlSerializer.Serialize(streamWriter, hitData);
            streamWriter.Close();
        }
    }

    public static HitDataExt HitTracker(HttpRequest Request)
    {
        return HitTracker(Request, -1);
    }

    public static HitDataExt HitTracker(HttpRequest Request, int test)
    {
        HitDataExt hitData;
        VisitorDataItem newVisitorItem;

        lock (mutex)
        {
            string _path = GetFilePath();
            
            //CultureInfo culture = new CultureInfo("EN-US");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HitDataExt));
            string curIP = Request.UserHostAddress;
            if (test >= 0)
            {
                curIP = "127." + ((int)(test / 65535)).ToString() + "." + ((int)(test / 255)).ToString() + "." + ((int)test % 255).ToString();
            }

            if (File.Exists(_path))
            {
                // [rn] 24.08.2008 - create backup of xml
                string _pathBackup = _path + ".bak";
                try
                {
                    if (File.Exists(_pathBackup))
                    {
                        File.Delete(_pathBackup);
                    }
                    File.Copy(_path, _pathBackup);
                }
                catch (Exception /*ex*/)
                {   // optionally write an error log etc.                    
                }
                // --------------------------------------

                //textReader = File.OpenText(_path);
                StreamReader streamReader = new StreamReader(_path);
                hitData = xmlSerializer.Deserialize(streamReader) as HitDataExt;
                streamReader.Close();
                //hitData.HitCount = Convert.ToInt32(textReader.ReadLine());
                //hitData.LastVisit = Convert.ToDateTime(textReader.ReadLine(), culture);
                //textReader.Close();
            }
            else
            {
                hitData = new HitDataExt(0, DateTime.Now, new VisitorDataList());
            }

            if (hitData.VisitorData.ContainsKey(curIP))
            {
                newVisitorItem = hitData.VisitorData[curIP];
                // overwrite agent (e.g. if client is using a different browser now)
                newVisitorItem.Agent = GetUserAgentString(Request); //Request.UserAgent; 
                //newVisitorItem.HostName = Request.UserHostName;
            }
            else
            {
                newVisitorItem =
                    new VisitorDataItem(GetUserAgentString(Request), DateTime.Now);
                hitData.VisitorData.Add(curIP, newVisitorItem);
            }

            hitData.CurrentVisitorIP = curIP;
            hitData.CurrentVisitorItem
                = new VisitorDataItem(newVisitorItem.Agent, newVisitorItem.LastVisit);

            hitData.HitCount++;
            hitData.LastVisit = DateTime.Now;
            newVisitorItem.LastVisit = DateTime.Now;

            StreamWriter streamWriter = new StreamWriter(_path, false);
            xmlSerializer.Serialize(streamWriter, hitData);
            streamWriter.Close();
            //textWriter = File.CreateText(_path);
            //textWriter.WriteLine(hitData.HitCount);
            //textWriter.WriteLine(DateTime.Now.ToString(culture));
            //textWriter.Close();

        } // lock
     
        return hitData;

    } // HitTracker

} // HitManager



// Serializable Dictionary taken from : http://weblogs.asp.net/pwelter34/archive/2006/05/03/444961.aspx
[XmlRoot("dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    #region IXmlSerializable Members

    public System.Xml.Schema.XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(System.Xml.XmlReader reader)
    {
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        bool wasEmpty = reader.IsEmptyElement;
        reader.Read();

        if (wasEmpty)
            return;

        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
        {
            reader.ReadStartElement("item");
            reader.ReadStartElement("key");
            TKey key = (TKey)keySerializer.Deserialize(reader);
            reader.ReadEndElement();
            reader.ReadStartElement("value");
            TValue value = (TValue)valueSerializer.Deserialize(reader);
            reader.ReadEndElement();
            this.Add(key, value);
            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
    }



    public void WriteXml(System.Xml.XmlWriter writer)
    {
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        foreach (TKey key in this.Keys)
        {
            writer.WriteStartElement("item");
            writer.WriteStartElement("key");
            keySerializer.Serialize(writer, key);
            writer.WriteEndElement();
            writer.WriteStartElement("value");
            TValue value = this[key];
            valueSerializer.Serialize(writer, value);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }

    #endregion

}
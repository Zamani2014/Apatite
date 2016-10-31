using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;

public partial class EasyControls_HitCounter : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            return;

        HitData hitData = HitManager.HitTracker();
        lblNumberOfHitsCounter.Text = hitData.HitCount.ToString();
        lblLastVisit.Text = hitData.LastVisit.ToString();
    }
}

public class HitData
{
    public HitData(int hitCount, DateTime lastVisit)
    {
        HitCount = hitCount;
        LastVisit = lastVisit;
    } // HitData

    public int HitCount;
    public DateTime LastVisit;
}



public static class HitManager
{
 
    private static object mutex = new object();
    
    public static HitData HitTracker()
    {
        HitData hitData = new HitData(0, DateTime.Now);

        lock (mutex)
        {
            string _path = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/HITS.config"));
            TextReader textReader = null;
            TextWriter textWriter = null;
            CultureInfo culture = new CultureInfo("EN-US");

            if (File.Exists(_path))
            {
                textReader = File.OpenText(_path);
                hitData.HitCount = Convert.ToInt32(textReader.ReadLine());
                hitData.LastVisit = Convert.ToDateTime(textReader.ReadLine(), culture);
                textReader.Close();
            } // if

            hitData.HitCount++;

            textWriter = File.CreateText(_path);
            textWriter.WriteLine(hitData.HitCount);
            textWriter.WriteLine(DateTime.Now.ToString(culture));
            textWriter.Close();

        } // lock

        return hitData;

    } // HitTracker

} // HitManager
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Web.Configuration;
using GoogleMap;
using System.Collections.Generic;

/// <summary>
/// Summary description for MapData
/// </summary>
public class MapData
{
    private MapData()
    {
        
    }

    public static List<Position> RouteMapInfo
    {
        get
        {
            if (routeMapInfo == null)
                LoadRouteMapInfo();

            return routeMapInfo;
        }
    }

    private static List<Position> routeMapInfo = null;

    private static void LoadRouteMapInfo()
    {
        routeMapInfo = new List<Position>();

        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["MapDataFileLocation"]));

        XmlNodeList rtMapInfo = doc.GetElementsByTagName("routemapinfo");

        System.Globalization.CultureInfo ciTR = new System.Globalization.CultureInfo("fa-IR");

        foreach (XmlNode rtMapInfoParamList in rtMapInfo)
        {
            foreach (XmlNode paramNode in rtMapInfoParamList)
            {
                if (paramNode.Name == "point")
                {
                    string latstr = paramNode.Attributes["lat"].InnerText;
                    string lngstr = paramNode.Attributes["lng"].InnerText;

                    if (!string.IsNullOrEmpty(latstr) && !string.IsNullOrEmpty(lngstr))
                    {
                        Position p = new Position();

                        p.latitude = double.Parse(latstr.Replace('.', ','), ciTR.NumberFormat);
                        p.longtitude = double.Parse(lngstr.Replace('.', ','), ciTR.NumberFormat);

                        routeMapInfo.Add(p);
                    }
                }
            }
        }
    }
}

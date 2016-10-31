using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleMap;
using MyWebPagesStarterKit.Controls;

public partial class test : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GoogleMap1.MapInfo.Latitude = 41.0191048232402;
            GoogleMap1.MapInfo.Longtitude = 28.997393828418;
            GoogleMap1.MapInfo.Zoom = 12;
            GoogleMap1.MapInfo.MapType = MapTypes.ROADMAP;
            GoogleMap1.Width = 700;
            GoogleMap1.Height = 500;
        }

        //GoogleMap1.MapCenterChanged += new GoogleMap.MapCenterChangedEventHandler(GoogleMap1_MapCenterChanged);
        GoogleMap1.MapClicked += new MapClickedEventHandler(GoogleMap1_MapClicked);
    }

    void GoogleMap1_MapClicked(double Latitude, double Longtitude)
    {
        GoogleMap.Marker m = new GoogleMap.Marker(Guid.NewGuid().ToString(), Latitude, Longtitude, "Images/flag1.ico");
        m.Draggable = true;
        m.InfoWindowOnClick = true;
        m.InfoWindowContentHtml = "New Marker Content Html, Added at: " + DateTime.Now.ToString();
        WebMsgBox.WebMsgBox.Show(Latitude.ToString() + "  " + Longtitude.ToString());
        GoogleMap1.Markers.Add(m);
    }

}
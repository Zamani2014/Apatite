using System;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

/// <summary>
/// Summary description for WebLogPing
/// </summary>
public class WebLogPing
{
    public WebLogPing()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void PingWeblogsCom(HttpContext context, string blogName, string pageId)
    {
        string url = string.Empty;
        string port = string.Empty;
        string method = "http";

        if (context.Request.ServerVariables["SERVER_PORT_SECURE"] == "1")
            method = "https";
        if (context.Request.ServerVariables["SERVER_PORT"] != string.Empty)
            port = ":" + context.Request.ServerVariables["SERVER_PORT"];
        url = method + "://" + context.Request.ServerVariables["SERVER_NAME"] + port + string.Format("/Default.aspx?pg={0}", pageId);

        HttpWebRequest httpRequest = (HttpWebRequest)
        WebRequest.Create("http://rpc.weblogs.com/pingSiteForm?name=" + blogName + "&url=" + url);
        httpRequest.Method = "POST";

        //response (try catch to avoid error when the server is not available)
        try
        {
            HttpWebResponse WebResp = (HttpWebResponse)httpRequest.GetResponse();
            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            string answer = _Answer.ReadToEnd();
        }
        catch { }
    }
}

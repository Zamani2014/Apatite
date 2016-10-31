<%@ WebHandler Language="C#" Class="ImageHandler" %>

//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;
using System.Web.Caching;
using System.Drawing.Drawing2D;
using MyWebPagesStarterKit;

/// <summary>
/// This handler serves (resized) images out of the "~/App_Data/_Downloads/" folder.
/// </summary>
public class ImageHandler : IHttpHandler
{
    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        try
        {
			//the silverlight-gallery (Vertigo.SlideShow.xap) uses a xml-file for fetching the gallery's images.
			//in xml, the ampersand (&) character is not allowed in attributes -> has to be encoded to &amp;
			//therefore, some requests to the ImageHandler might come with a "wrong" querystring which makes it necessary
			//to ask for both kinds of querystring-item-keys: key and amp;key.
			
            //get the sectionId
			string section = context.Request.QueryString["section"] ?? context.Request.QueryString["amp;section"];
            //get the filename
			string file = context.Request.QueryString["image"] ?? context.Request.QueryString["amp;image"];
            string path = string.Empty;

            //get the pageId for the data security
			string pageId = context.Request.QueryString["pg"] ?? context.Request.QueryString["amp;pg"];

            //whenever there is a pageid in the url we can verify, if the user status allows to show the pic or not
            //{0} is placeholder for pageId in "Image Properties in FCKEditor
            if(pageId != null && pageId != "{0}")
            {
                WebPage _page = new WebPage(pageId);
                //logged in
                if (context.User != null && context.User.Identity.IsAuthenticated)
                {
                    if (_page.Visible == false)
                    {
                        //power users: page not visible and not marked as editable for power users
                        if (context.User.IsInRole(RoleNames.PowerUsers.ToString()) && _page.EditPowerUser == false)
                            throw new Exception("User is not allowed to view this file");

                        //normal users: page is not visible
                        if (context.User.IsInRole(RoleNames.Users.ToString()))
                            throw new Exception("User is not allowed to view this file");     
                    }
                }
                else //logged out
                {
                    //page is not visible or not accessible for anonymous visitors
                    if (_page.AllowAnonymousAccess == false || _page.Visible == false)
                        throw new Exception("User is not allowed to view this file");    
                }
            }

            if (section != null)
            {
                //create the path (when sectionId is present)
                path = context.Server.MapPath(string.Format("~/App_Data/_Downloads/{0}/{1}", section, file));
            }
            else
            {
                //create the path (when no sectionId is present)
                path = context.Request.MapPath(file);
            }

            string ext = Path.GetExtension(path).ToLower();

			string uploadedFile = context.Request.QueryString["UploadedFile"] ?? context.Request.QueryString["amp;UploadedFile"];
            if (string.IsNullOrEmpty(uploadedFile))
            {
                //get the desired maximum width/height of the image
                int width = int.Parse(context.Request.QueryString["width"]);
                int height = int.Parse(context.Request.QueryString["height"]);

                RenderImage(context, path, ext, width, height, true);
            }
            else
            {
                RenderImage(context, path, ext);
            }
        }
        catch
        {
            context.Response.Clear();
            context.Response.End();
        }
    }


    /// <summary>
    /// Render image with no resize
    /// </summary>
    /// <param name="context"></param>
    /// <param name="path"></param>
    /// <param name="ext"></param>
    private void RenderImage(HttpContext context, string path, string ext)
    {
        RenderImage(context, path, ext, -1, -1, false);
    }

    /// <summary>
    /// Render image including possible resize
    /// </summary>
    /// <param name="context"></param>
    /// <param name="path"></param>
    /// <param name="ext"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="doResize">Do an image resize to specified width and height</param>
    private void RenderImage(HttpContext context, string path, string ext, int width, int height, bool doResize)
    {
        context.Response.Clear();

        if (
            File.Exists(path)
            &&
            (
                (ext == ".jpg") ||
                (ext == ".jpe") ||
                (ext == ".jpeg") ||
                (ext == ".gif") ||
                (ext == ".png") ||
                (ext == ".bmp")
            )
            )
        {

            using (Stream imgStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Bitmap b = (doResize) ? resize(imgStream, width, height) : new Bitmap(imgStream);

                switch (ext)
                {
                    case ".jpg":
                    case ".jpe":
                    case ".jpeg":
                    case ".bmp":
                        context.Response.ContentType = "image/jpeg";
                        b.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                        break;
                    case ".gif":
                        context.Response.ContentType = "image/gif";
                        b.Save(context.Response.OutputStream, ImageFormat.Gif);
                        break;
                    case ".png":
                        context.Response.ContentType = "image/png";
                        //Bitmap Save() method does not work with a "non-seekable" stream
                        //PNG image format requires that the stream can seek
                        MemoryStream memStream = new MemoryStream();
                        b.Save(memStream, ImageFormat.Png);
                        memStream.WriteTo(context.Response.OutputStream);
                        break;
                }

                b.Dispose();
            }
        }
    }
    #endregion

    #region UploadedFilesHandling
    private void ReturnUploadedFile(HttpContext c)
    {

        string imagePath = c.Request["Path"];
        if (imagePath != null)
        {
            string ext = Path.GetExtension(imagePath).ToLower();
            RenderImage(c, imagePath, ext, -1, -1, false);
        }

    }
    #endregion

    private Bitmap resize(Stream SourceImage, int MaxWidth, int MaxHeight)
    {
        Bitmap b = null;

        using (Image i = Image.FromStream(SourceImage))
        {
            int _maxWidth = (MaxWidth > 0) ? MaxWidth : i.Width;
            int _maxHeight = (MaxHeight > 0) ? MaxHeight : i.Height;
            double _scaleWidth = (double)_maxWidth / (double)i.Width;
            double _scaleHeight = (double)_maxHeight / (double)i.Height;
            double _scale = (_scaleHeight < _scaleWidth) ? _scaleHeight : _scaleWidth;
            _scale = (_scale > 1) ? 1 : _scale;

            int _newWidth = (int)(_scale * i.Width);
            int _newHeight = (int)(_scale * i.Height);

            b = new Bitmap(_newWidth, _newHeight);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(i, new Rectangle(0, 0, _newWidth, _newHeight));
                g.Save();
            }
        }
        return b;
    }
}
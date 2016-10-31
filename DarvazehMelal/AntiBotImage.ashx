<%@ WebHandler Language="C#" Class="AntiBotImage" %>

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
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

/// <summary>
/// This Handler renders out a so called Anti-Bot image. It is used to prevent bots from making automatic entries in the guestbook.
/// The random text for the image is taken out of the session-variable "antibotimage". If this value is not present in the session, a empty gif is returned.
/// </summary>
public class AntiBotImage : IHttpHandler, IRequiresSessionState
{
    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        if(context.Session["antibotimage"] == null)
        {
            context.Session["antibotimage"] = generateRandomString(4).ToUpper();
        }
        
        GenerateImage(context.Session["antibotimage"].ToString(), 100, 20, "Arial").Save(context.Response.OutputStream, ImageFormat.Jpeg);
    }
    
    #endregion

    private Bitmap GenerateImage(string text, int width, int height, string fontFamily)
    {
        Random random = new Random();

        // Create a new 32-bit bitmap image.
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        // Create a graphics object for drawing.
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(0, 0, width, height);

        // Fill in the background.
        HatchBrush hatchBrush = new HatchBrush(HatchStyle.Wave, Color.LightGray, Color.White);
        g.FillRectangle(hatchBrush, rect);

        // Set up the text font.
        SizeF size;
        float fontSize = rect.Height + 1;
        Font font;
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;

        // Adjust the font size until the text fits within the image.
        do
        {
            fontSize--;
            font = new Font(fontFamily, fontSize, FontStyle.Bold);
            size = g.MeasureString(text, font, new SizeF(width, height), format);
        } while (size.Width > rect.Width);

        // Create a path using the text and warp it randomly.
        GraphicsPath path = new GraphicsPath();
        path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, format);
        float v = 4F;
        PointF[] points =
			{
				new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v)
			};
        Matrix matrix = new Matrix();
        matrix.Translate(0F, 0F);
        path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

        // Draw the text.
        hatchBrush = new HatchBrush(HatchStyle.DashedUpwardDiagonal, Color.DarkGray, Color.Black);
        g.FillPath(hatchBrush, path);

        // Add some random noise.
        int m = Math.Max(rect.Width, rect.Height);
        for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
        {
            int x = random.Next(rect.Width);
            int y = random.Next(rect.Height);
            int w = random.Next(m / 50);
            int h = random.Next(m / 50);
            g.FillEllipse(hatchBrush, x, y, w, h);
        }

        // Clean up.
        font.Dispose();
        hatchBrush.Dispose();
        g.Dispose();

        return bitmap;
    }

    private string generateRandomString(int size)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        return builder.ToString();
    }
}

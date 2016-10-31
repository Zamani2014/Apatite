//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using System.IO;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// This is to cope with postbacks and the URL rewriter - without it the urls revert to 
    /// their original form on postback and this can cause viewstate errors
    /// </summary>
    public class FormRewriterControlAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        { 
            base.Render(new RewriteFormHtmlTextWriter(writer));
        }
    }

    public class RewriteFormHtmlTextWriter : HtmlTextWriter
    {
        public RewriteFormHtmlTextWriter(HtmlTextWriter writer)
            : base(writer)
        {
            InnerWriter = writer.InnerWriter;
        }
        public RewriteFormHtmlTextWriter(TextWriter writer)
            : base(writer)
        {
            InnerWriter = writer;
        }

        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            // if the attribute we are writing is the "action" attribute, and we are not on a sub-control, 
            // then replace the value to write with the raw URL of the request - which ensures that we'll
            // preserve the PathInfo value on postback scenarios
            if (name == "action")
            {
                HttpContext Context = HttpContext.Current;

                if (Context.Items["ActionAlreadyWritten"] == null)
                {

                    // Use of the original url
                    value = Context.Request.RawUrl;

                    //-------------------------------------------------------------------------------------
                    //hack for LoginStatus Login/Logout to maintain virtual url (e.g. .../GuestBook.aspx)
                    //save the original url in session object

                    //Login: the auto generated ReturnUrl of the LoginStatus does not regard virutal urls -> 
                    //in the OnLoggedIn Event we redirect to the stored virtual url  

                    //Logout: before login out we overwirte the LogoutPageUrl of the LoginStatus control with the stored virtual url

                    //Keep in mind: if the url is not virutal nothing changes
                    
                    Context.Session["OriginalURL"] = value;

                    //-------------------------------------------------------------------------------------

                    // Indicate that we//ve already rewritten the <form>//s action attribute to prevent
                    // us from rewriting a sub-control under the <form> control
                    Context.Items["ActionAlreadyWritten"] = true;
                }
            }

            base.WriteAttribute(name, value, fEncode);
        }
    }
}
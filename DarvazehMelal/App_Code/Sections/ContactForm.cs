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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

namespace MyWebPagesStarterKit
{
    /// <summary>
    /// Contact Form Section
    /// This section displays a contact form
    /// </summary>
    public class ContactForm : Section<ContactForm.ContactFormData>
    {
        public ContactForm() : base() { }
        public ContactForm(string id) : base(id) { }

        /// <summary>
        /// E-Mail address, the contact-emails get sent to
        /// </summary>
        public string EmailTo
        {
            get { return _data.EmailTo; }
            set { _data.EmailTo = value; }
        }

        /// <summary>
        /// E-Mail address, the contact-emails get sent to as a copy (optional)
        /// </summary>
        public string EmailCc
        {
            get { return _data.EmailCc; }
            set { _data.EmailCc = value; }
        }

        /// <summary>
        /// Subject of the contact-mail
        /// </summary>
        public string Subject
        {
            get { return _data.Subject; }
            set { _data.Subject = value; }
        }

        /// <summary>
        /// Text displayed above the contact-form
        /// </summary>
        public string Introtext
        {
            get { return _data.Introtext; }
            set { _data.Introtext = value; }
        }

        /// <summary>
        /// Text displayed after the user has submitted the contact-form
        /// </summary>
        public string Thankyoutext
        {
            get { return _data.Thankyoutext; }
            set { _data.Thankyoutext = value; }
        }

        public override List<SearchResult> Search(string searchString, WebPage page)
        {
            List<SearchResult> foundResults = new List<SearchResult>();
            string IntroTextDecoded = HttpUtility.HtmlDecode(SearchResult.RemoveHtml(Introtext));
            if (IntroTextDecoded.IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                foundResults.Add(new SearchResult(
                    string.Format("~/Default.aspx?pg={0}#{1}", page.PageId, SectionId),
                    "Contact-Form",
                    SearchResult.CreateExcerpt(IntroTextDecoded, searchString)
                    )
                );
            }
            return foundResults;
        }

        public class ContactFormData
        {
            public ContactFormData()
            {
                EmailTo = string.Empty;
                EmailCc = string.Empty;
                Subject = string.Empty;
                Introtext = string.Empty;
                Thankyoutext = string.Empty;
            }

            public string EmailTo;
            public string EmailCc;
            public string Subject;
            public string Introtext;
            public string Thankyoutext;
        }
    }
}

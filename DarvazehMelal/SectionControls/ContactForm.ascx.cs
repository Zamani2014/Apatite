//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Net.Mail;
using System.Text;
using MyWebPagesStarterKit;
using MyWebPagesStarterKit.Controls;
using System.IO;
using System.Web;
using System.Net;
using System.Web.UI.WebControls;

public partial class SectionControls_ContactForm : SectionControlBaseClass
{
	private ContactForm _section;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			Session["ContactFormSent"] = null;
			Session["antibotimage"] = generateRandomString(4).ToUpper();

			valEmailToRegex.ValidationExpression = Validation.EmailRegex;
			valEmailFromRegex.ValidationExpression = Validation.EmailRegex;
			valEmailCcRegex.ValidationExpression = Validation.EmailRegex;

			EnsureID();
			valEmailFromRegex.ValidationGroup = ID;
			valEmailFromRequired.ValidationGroup = ID;
			valEmailToRegex.ValidationGroup = ID;
			valEmailToRequired.ValidationGroup = ID;
			valEmailCcRegex.ValidationGroup = ID;
			valSubjectRequired.ValidationGroup = ID;
			valMessageRequired.ValidationGroup = ID;
			valAntiBotImage.ValidationGroup = ID;
			valAntiBotImageRequired.ValidationGroup = ID;
			btnSave.ValidationGroup = ID;
			btnSubmit.ValidationGroup = ID;
		}
	}

	protected void Page_PreRender(object sender, EventArgs e)
	{
		updateViews();
	}

	protected void btnSave_Click(object sender, EventArgs e)
	{
		_section.EmailTo = txtEmailTo.Text.Trim();
		_section.EmailCc = txtEmailCc.Text.Trim();
		_section.Subject = txtSubject.Text.Trim();
		_section.Introtext = txtIntroTextHtml.Text.Trim();
		_section.Thankyoutext = txtThankYouMessageHtml.Text.Trim();
		_section.SaveData();
	}

	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		if (Session["ContactFormSent"] == null)
		{
			Page.Validate(ID);
			if (Page.IsValid)
			{
				WebSite _website = WebSite.GetInstance();
				MailMessage mail;
				if (txtName.Text.Trim() != string.Empty)
				{
					mail = new MailMessage(new MailAddress(txtEmailFrom.Text.Trim(), txtName.Text.Trim()), new MailAddress(_section.EmailTo, _section.EmailTo));
				}
				else
				{
					mail = new MailMessage(txtEmailFrom.Text.Trim(), _section.EmailTo);
				}

				if (_section.EmailCc.Trim() != string.Empty)
					mail.CC.Add(_section.EmailCc);
				mail.SubjectEncoding = Encoding.UTF8;
				mail.Subject = _section.Subject;
				mail.BodyEncoding = Encoding.UTF8;
				mail.IsBodyHtml = false;
				mail.Body = txtMessage.Text.Trim();
				SmtpClient client = new SmtpClient(_website.SmtpServer);

				//when Smtp user/password/domain is given, SMTP-Authentication has to be used
				if ((!string.IsNullOrEmpty(_website.SmtpUser)) && (!string.IsNullOrEmpty(_website.SmtpPassword)))
				{
					if (string.IsNullOrEmpty(_website.SmtpDomain))
					{
						client.UseDefaultCredentials = false;
						client.Credentials = new NetworkCredential(_website.SmtpUser, _website.SmtpPassword);
					}
					else
					{
						client.UseDefaultCredentials = false;
						client.Credentials = new NetworkCredential(_website.SmtpUser, _website.SmtpPassword, _website.SmtpDomain);
					}
				}

				client.Send(mail);

				Session["ContactFormSent"] = true;
			}
		}
	}

	public override ISection Section
	{
		set
		{
			if (value is ContactForm)
				_section = (ContactForm)value;
			else
				throw new ArgumentException("Section must be of type ContactForm");
		}
		get { return _section; }
	}

	public override bool HasAdminView
	{
		get { return true; }
	}

	public override string InfoUrl
	{
		get
		{
			string lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			if (File.Exists(HttpContext.Current.Server.MapPath(string.Format("Documentation/" + lang + "/quick_guide.html"))))
			{
				return "Documentation/" + lang + "/quick_guide.html#contact-form";
			}
			else
			{ return "Documentation/en/quick_guide.html#contact-form"; }
		}
	}

	private void updateViews()
	{
		WebSite _website = WebSite.GetInstance();
		multiview.Visible = true;

		if (ViewMode == ViewMode.Edit)
		{
			if (_website.SmtpServer.Trim() == string.Empty)
			{
				multiview.SetActiveView(noMailserverView);
			}
			else
			{
				multiview.SetActiveView(editView);
				txtEmailTo.Text = _section.EmailTo;
				txtEmailCc.Text = _section.EmailCc;
				txtSubject.Text = _section.Subject;
				txtIntroTextHtml.Text = _section.Introtext;
				txtThankYouMessageHtml.Text = _section.Thankyoutext;
			}
		}
		else
		{
			if ((_website.SmtpServer.Trim() == string.Empty) || (_section.EmailTo == string.Empty) || (_section.Subject == string.Empty))
			{
				multiview.Visible = false;
			}
			else
			{
				if (Session["ContactFormSent"] == null)
				{
					multiview.SetActiveView(formView);
					litIntrotext.Text = _section.Introtext;
				}
				else
				{
					multiview.SetActiveView(thankyouView);
					litThankyoutext.Text = _section.Thankyoutext;
				}
			}
		}
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

	protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
	{
		args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
	}
}

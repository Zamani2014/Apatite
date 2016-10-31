//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit.Controls;
using System.Resources;
using MyWebPagesStarterKit;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

public partial class UserRegistration : PageBaseClass
{
	protected void Page_Init(object sender, EventArgs e)
	{
		if (!_website.AllowUserSelfRegistration)
			Response.Redirect("~/Login.aspx", true);

		createUserWizard.CreatedUser += new EventHandler(createUserWizard_CreatedUser);
	}

	void createUserWizard_CreatedUser(object sender, EventArgs e)
	{
		//Send email to user for verifying account
		MembershipUser user = Membership.GetUser(createUserWizard.UserName);
		string url = string.Concat("http://", Request.Url.Authority, Response.ApplyAppPathModifier("~/Login.aspx?activate="));

		MailMessage mail = new MailMessage(_website.MailSenderAddress, user.Email);
		mail.SubjectEncoding = System.Text.Encoding.UTF8;
		mail.Subject = string.Format(Resources.StringsRes.pge_UserRegistration_ActivationEmailSubject, _website.WebSiteTitle);
		mail.BodyEncoding = System.Text.Encoding.UTF8;
		mail.IsBodyHtml = false;
		mail.Body = string.Format(Resources.StringsRes.pge_UserRegistration_ActivationEmailBody, _website.WebSiteTitle, url, user.ProviderUserKey.ToString());

		SmtpClient client = new SmtpClient(_website.SmtpServer);

		//when Smtp user/password/domain is given, SMTP-Authentication has to be used
		if (_website.SmtpUser != "" && _website.SmtpPassword != "" && _website.SmtpDomain != "")
		{
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential(_website.SmtpUser, _website.SmtpPassword, _website.SmtpDomain);
		}

		client.Send(mail);
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			(createUserStep.ContentTemplateContainer.FindControl("EmailRegexValidator") as RegularExpressionValidator).ValidationExpression = Validation.EmailRegex;
		}
	}
}
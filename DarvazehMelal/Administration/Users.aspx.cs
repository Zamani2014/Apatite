//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.IO;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MyWebPagesStarterKit.Controls;
using System.Web;
using System.Net;

public partial class Administration_Users : PageBaseClass
{
    protected string lang;

    #region Properties
    /// <summary>
    /// Username of the currently selected user to edit
    /// </summary>
    protected string SelectedUserName
    {
        get
        {
            return SelectedUser.UserName;
        }
        set 
        { 
            SelectedUser = Membership.GetUser(value);
        }
    }
    
    protected bool ForcePasswordChange
    {
        get
        {
            return !Membership.EnablePasswordRetrieval;
        }   
    }

    /// <summary>
    /// User that was selected to edit
    /// </summary>
    protected MembershipUser SelectedUser
    {
        get
        {
            return (MembershipUser)ViewState["SelectedUserName"];
        }
        set 
        {
            ViewState["SelectedUserName"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //define language for the documentation path
        lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        if (!File.Exists(HttpContext.Current.Server.MapPath(string.Format("~/Documentation/" + lang + "/quick_guide.html"))))
        {
            lang = "en";
        }
        if (!IsPostBack)
        {
            if (_website.MailSenderAddress == string.Empty)
                RecoverPasswordWizard.Visible = false;
            else
            {
                trMessage.Visible = false;
                RecoverPasswordWizard.HeaderText = Resources.StringsRes.ctl_RecoverPasswordWizard_HeaderText;

                if (!ForcePasswordChange)
                {
                    ListItemCollection c = new ListItemCollection();
                    c.Add(new ListItem(Resources.StringsRes.clt_RecoverPasswordWizard_ResetPassword, "0"));
                    c.Add(new ListItem(Resources.StringsRes.clt_RecoverPasswordWizard_KeepPassword, "1"));
                    //ddlResetPassword.DataSource = c;
                    //ddlResetPassword.DataValueField = "Value";
                    //ddlResetPassword.DataTextField = "Text";
                    //ddlResetPassword.DataBind();
                } else
                {
                }
            }

        }
        lblMessage.Text = string.Empty;


    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        trMessage.Visible = (lblMessage.Text != string.Empty);
        trUpdateUser.Visible = (SelectedUser != null);
        trCreateUser.Visible = (SelectedUser == null);
        trSendUserPassword.Visible = (_website.MailSenderAddress != string.Empty);
    }

    #endregion
    
    #region Recover Password Wizard
    protected void RecoverPasswordWizard_OnFinish(object sender, WizardNavigationEventArgs e)
    {
        RecoverPasswordWizard_OnNext(sender, e);
    }
    
    protected void RecoverPasswordWizard_OnNext(object sender, WizardNavigationEventArgs e)
    {
        WizardStepBase upcomingStep = RecoverPasswordWizard.WizardSteps[e.NextStepIndex];
        
        if (upcomingStep.StepType == WizardStepType.Complete)
        {
            // Preparing finish step display
            // Send mail
            MembershipUser u = Membership.GetUser(txtUsername.Text);

			if (u == null)
			{
				lblResult.Text = Resources.StringsRes.ctl_RecoverPasswordWizard_ResultUserNotFound;
			}
			else
			{
				string password = u.ResetPassword();

				// Send mail
				try
				{
					string url = string.Concat("http://", Request.Url.Authority, Response.ApplyAppPathModifier("~/Login.aspx"));

					MailMessage mail = new MailMessage(_website.MailSenderAddress, u.Email);
					mail.SubjectEncoding = System.Text.Encoding.UTF8;
					mail.Subject = Resources.StringsRes.ctl_RecoverPasswordWizard_PasswordMailSubject;
					mail.BodyEncoding = System.Text.Encoding.UTF8;
					mail.IsBodyHtml = false;
					mail.Body = string.Format(Resources.StringsRes.ctl_RecoverPasswordWizard_PasswordMailBody, url, password, System.Environment.NewLine);

					SmtpClient client = new SmtpClient(_website.SmtpServer);

					//when Smtp user/password/domain is given, SMTP-Authentication has to be used
					if (_website.SmtpUser != "" && _website.SmtpPassword != "" && _website.SmtpDomain != "")
					{
						client.UseDefaultCredentials = false;
						client.Credentials = new NetworkCredential(_website.SmtpUser, _website.SmtpPassword, _website.SmtpDomain);
					}

					client.Send(mail);
				}
				catch
				{
					lblResult.Text = Resources.StringsRes.ctl_RecoverPasswordWizard_PasswordMailFailed;
				}

				lblResult.Text = Resources.StringsRes.ctl_RecoverPasswordWizard_PasswordMailSent;
			}
        } else if (upcomingStep.StepType == WizardStepType.Step)
        {
            if (upcomingStep.ID == "stpKeepPassword" && ForcePasswordChange)
            {
                // skip that part as the question can not be asked due to the provider not implementing password retrieval
                RecoverPasswordWizard.ActiveStepIndex = e.NextStepIndex + 1;
            }
        }
    }
    #endregion

    #region User Overview
    /// <summary>
    /// Populate User Edit Table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EditUser_PreRender(object sender, EventArgs e)
    {
        chkAdmin.Checked = GetAdminChecked();
        chkPowerUser.Checked = GetPowerUserChecked();
		chkIsApproved.Checked = GetIsApproved();
        txtEmail.Text = SelectedUser.Email;
        txtComment.Text = SelectedUser.Comment;
        litUsername.Text = SelectedUserName;
    }

    protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton deleteButton = (LinkButton)e.Row.Controls[e.Row.Controls.Count - 1].Controls[0];
            if (deleteButton.CommandName == "delete_user")
            {
                deleteButton.ToolTip = Resources.StringsRes.adm_Users_DeleteUser;
                deleteButton.OnClientClick = "return confirm('" + Resources.StringsRes.adm_Users_DeleteWarning + "');";
            }
        }
    }
    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SelectedUserName = gvUsers.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
        // check which command has been sent by the Grid 
        switch (e.CommandName)
        {
            case "edit_details":
                // Read Username to edit from control and redirect again to this page, including the username to edit

                //Reset Password Recover
                if (RecoverPasswordWizard.Visible)
                {
                    RecoverPasswordWizard.ActiveStepIndex = RecoverPasswordWizard.WizardSteps.IndexOf(this.stpUsername);
                    txtUsername.Text = "";
                }
                break;
            case "delete_user":
                //make sure there is at least one admin in the system
                if (!Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Administrators.ToString()))
                {
                    //if not an administrator
                    DeleteUser();
                }
                else
                {
                    //make sure there is at least one administrator
                    if (Roles.GetUsersInRole(MyWebPagesStarterKit.RoleNames.Administrators.ToString()).Length > 1)
                    {
                        DeleteUser();
                    }
                    else
                    {
                        lblMessage.Text += Resources.StringsRes.adm_Users_LastAdminRemains;
                        SelectedUser = null;
                    }
                }
                break;
        }
    }

    private void DeleteUser()
    {
        Membership.DeleteUser(SelectedUserName, true);
        SelectedUser = null;
        Response.Redirect(Request.ServerVariables["SCRIPT_NAME"], true);
    }
    protected void btnUpdateCancel_Click(object sender, EventArgs e)
    {
        SelectedUser = null;
    }

    #endregion

    #region Create User Wizard
    void CreateUserWizard_UserCreated(object sender, EventArgs e)
    {
        SelectedUser = null;
        gvUsers.DataBind();
    }


    /// <summary>
    /// Update the selected user with the values that were entered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSave_Click(object sender, EventArgs e)
    {
        MembershipUser memUser = SelectedUser;
        memUser.Email = txtEmail.Text;
        memUser.Comment = txtComment.Text;

        try
        {
            Membership.UpdateUser(memUser);

            // extented user role (PowerUser Management)
            if (chkAdmin.Checked && !Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Administrators.ToString()))
            {
                Roles.AddUserToRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Administrators.ToString());
                if (Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString()))
                {
                    Roles.RemoveUserFromRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString());
                }
                else
                {
                    Roles.RemoveUserFromRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Users.ToString());
                }
                chkPowerUser.Checked = false;
            }
            else if (!chkAdmin.Checked && Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Administrators.ToString()))
            {
                // Remove admin Role from selected user
                if (Roles.GetUsersInRole(MyWebPagesStarterKit.RoleNames.Administrators.ToString()).GetLength(0) > 1)
                {
                    Roles.RemoveUserFromRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Administrators.ToString());
                    if (chkPowerUser.Checked)
                    {
                        Roles.AddUserToRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString());
                    }
                    else
                    {
                        Roles.AddUserToRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Users.ToString());
                    }
                }
                else
                {
                    lblMessage.Text += Resources.StringsRes.adm_Users_LastAdminRemains;
                }
            }
            // new role PowerUser
            else if (!chkAdmin.Checked && chkPowerUser.Checked && !Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString()))
            {
                Roles.AddUserToRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString());
                Roles.RemoveUserFromRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Users.ToString());
            }
            else if (!chkAdmin.Checked && !chkPowerUser.Checked && Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString()))
            {
                Roles.RemoveUserFromRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString());
                Roles.AddUserToRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Users.ToString());
            }

			if (SelectedUser.IsApproved != chkIsApproved.Checked)
			{
				SelectedUser.IsApproved = chkIsApproved.Checked;
				Membership.UpdateUser(SelectedUser);
			}

            SelectedUser = null;
        }
        catch (Exception ex)
        {
            Response.Write("<div>" + Resources.StringsRes.adm_Users_Exception + "<font color='red'> " + ex.Message + "</font></div>");
        }
        gvUsers.DataBind();
    }

    /// <summary>
    /// Hide Create User Wizard once it's done
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CreateUserWizard1_Done(object sender, EventArgs e)
    {
        //insert role
        Roles.AddUserToRole(((CreateUserWizard)sender).UserName, MyWebPagesStarterKit.RoleNames.Users.ToString());
        trCreateUser.Visible = false;
        gvUsers.DataBind();
    }
    
    protected void CreateUserWizard_DoneClick(object sender, EventArgs e)
    {
        trCreateUser.Visible = false;
        gvUsers.DataBind();
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// See if "admin" Edit-User checkbox should be checked
    /// </summary>
    /// <returns>Admin yes/no</returns>
    protected bool GetAdminChecked()
    {
        if (SelectedUser != null && Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.Administrators.ToString()))
            return true;
        else
            return false;
    }

    //PowerUser Management
    protected bool GetPowerUserChecked()
    {
        if (SelectedUser != null && Roles.IsUserInRole(SelectedUserName, MyWebPagesStarterKit.RoleNames.PowerUsers.ToString()))
            return true;
        else
            return false;
    }

    //PowerUser Management
    protected string ShowPowerUser(string Username)
    {
        string Result = "";
        if (Roles.IsUserInRole(Eval("UserName", "{0}"), MyWebPagesStarterKit.RoleNames.PowerUsers.ToString()))
            Result = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x&nbsp;&nbsp;&nbsp;";

        return Result;
    }

    protected string ShowAdmin(string Username)
    {
        string Result = "";
        if (Roles.IsUserInRole(Eval("UserName", "{0}"), MyWebPagesStarterKit.RoleNames.Administrators.ToString()))
            Result = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x&nbsp;&nbsp;&nbsp;";

        return Result;
    }

	protected bool GetIsApproved()
	{
		if (SelectedUser != null && SelectedUser.IsApproved)
			return true;
		else
			return false;
	}
    #endregion


}



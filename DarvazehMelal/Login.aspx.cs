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
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebPagesStarterKit;
using MyWebPagesStarterKit.Controls;
using System.Web.Security;

public partial class NLogin : PageBaseClass
{
    private string _returnURL = string.Empty;

	protected void Page_Init(object sender, EventArgs e)
	{
		if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["activate"]))
		{
			Guid userId = new Guid(Request.QueryString["activate"]);
			MembershipUser user = Membership.GetUser(userId);
			user.IsApproved = true;
			Membership.UpdateUser(user);

			Login1.UserName = user.UserName;

			Login1.FindControl("trActivationSuccess").Visible = true;
		}

		Login1.FindControl("trCreateUser").Visible = _website.AllowUserSelfRegistration;
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Control ctl = Login1.FindControl("UserName");
            if (ctl != null)
            {
                (ctl).Focus();
            }
            
            //hack for maintaining the virtual url
            if (Session["OriginalURL"] != null)
                ViewState["ReturnURL"] = Session["OriginalURL"].ToString();
        }
        else
        {
            //hack for maintaining the virtual url
            if(ViewState["ReturnURL"] != null)
                _returnURL = ViewState["ReturnURL"].ToString();
        }
    }

    protected void OnLoggingIn(object sender, EventArgs e)
    {
        //hack for upper and lower case: the asp login control is case sensitive: Admin <> admin
        //in order to make it not case sensitive, we rewrite the UserName (as it should be) to the login control
        TextBox txtUserName = (TextBox)Login1.FindControl("UserName");
        foreach (MembershipUser user in Membership.GetAllUsers())
        {
            if (string.Compare(user.UserName, txtUserName.Text, true) == 0)
            {
                Login1.UserName = user.UserName;
                return;
            }
        }
    }

    /// <summary>
    /// hack for maintaining the virtual url
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OnLoggedIn(object sender, EventArgs e)
    {
        if(_returnURL != string.Empty)
            Response.Redirect(_returnURL);
    }
}

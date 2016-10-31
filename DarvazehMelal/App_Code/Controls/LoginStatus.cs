using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Web.Configuration;

/// <summary>
/// Summary description for LoginStatus
/// </summary>
namespace MyWebPagesStarterKit.Controls
{
    public class LoginStatus : System.Web.UI.WebControls.LoginStatus
    {
        public LoginStatus()
            : base()
        {
            LoginText = Resources.StringsRes.ctl_LoginAdmin_LogIn;
            LogoutText = Resources.StringsRes.ctl_LoginAdmin_LogOut;
        }

        protected override void OnLoggingOut(LoginCancelEventArgs e)
        {
            //hack to maintain the virtual url after login out (session from App_Code/Form.cs)
            if (base.Page.Session["OriginalURL"] != null)
            {
                LogoutPageUrl = base.Page.Session["OriginalURL"].ToString();
                LogoutAction = LogoutAction.Redirect;
            }
            base.OnLoggingOut(e);
        }

    }

    public class NCreateUserWizard : System.Web.UI.WebControls.CreateUserWizard
    {
        public NCreateUserWizard()
            : base()
        {
        }
    }

    public class NPasswordRecovery : System.Web.UI.WebControls.PasswordRecovery
    {
        public NPasswordRecovery()
            : base()
        {
        }
    }
}
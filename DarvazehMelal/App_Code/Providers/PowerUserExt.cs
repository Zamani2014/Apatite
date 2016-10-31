using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MyWebPagesStarterKit
{

    /// <summary>
    /// Extended PowerUser Management
    /// </summary>
    public class PowerUserExt
    {
        public PowerUserExt()
        {
        }

        public static bool HasAdminRights(System.Security.Principal.IPrincipal user, MyWebPagesStarterKit.WebPage pageProperties)
        {
            return (user.IsInRole(RoleNames.Administrators.ToString()));
        }

        public static bool HasEditRights(System.Security.Principal.IPrincipal user, MyWebPagesStarterKit.WebPage pageProperties)
        {
            if (user.IsInRole(RoleNames.Administrators.ToString()))
            {
                return true;
            }
            else if (user.IsInRole(RoleNames.PowerUsers.ToString()))
            {
                return pageProperties != null && pageProperties.EditPowerUser;
            }
            return false;
        }
    }
}
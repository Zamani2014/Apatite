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
using System.Configuration.Provider;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;

namespace MyWebPagesStarterKit.Providers
{
    /// <summary>
    /// Specialized RoleProvider that uses a file (Roles.config) to store its data.
    /// </summary>
    public class CustomRoleProvider : RoleProvider
    {
        private String rolesfile;
        private const string _cProviderName = "CustomRoleProvider";
        private const string _cRolesFilename = "Roles.config";

        private string _applicationName;
        private List<RoleData> _roles;
        
        public CustomRoleProvider() 
        {
            rolesfile = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/{0}", _cRolesFilename));
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {

            if (!WebSite.GetInstance().ReadyToUse)
                throw new WebsiteNotReadyException("This website is not yet ready to use. Please verify that the folder App_Data exists.");
                
            if (config == null)
                throw new ArgumentNullException("config");

            name = _cProviderName;

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Xml membership provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            _applicationName = getConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            //load/create the rolesfile
            if (File.Exists(rolesfile))
            {
                lock (rolesfile)
                {
                    using (FileStream reader = File.Open(rolesfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<RoleData>));
                        _roles = (List<RoleData>)serializer.Deserialize(reader);
                    }
                }
            }
            else
            {
                _roles = new List<RoleData>();
                save();
            }
       }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string rolename in roleNames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                if (username.IndexOf(',') > 0)
                {
                    throw new ArgumentException("User names cannot contain commas.");
                }

                foreach (string rolename in roleNames)
                {
                    if (IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is already in role.");
                    }
                }
            }

            foreach (string username in usernames)
            {
                foreach (string rolename in roleNames)
                {
                    RoleData data = getRoleData(rolename);
                    if (data != null)
                        data.Users.Add(username);
                }
            }
            save();
        }

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public override void CreateRole(string roleName)
        {
            if (roleName.IndexOf(',') > 0)
            {
                throw new ArgumentException("Role names cannot contain commas.");
            }

            if (RoleExists(roleName))
            {
                throw new ProviderException("Role name already exists.");
            }
            RoleData rd = new RoleData();
            rd.RoleName = roleName;
            rd.Users = new List<string>();
            _roles.Add(rd);
            save();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (!RoleExists(roleName))
            {
                throw new ProviderException("Role does not exist.");
            }

            if (GetUsersInRole(roleName).Length > 0)
            {
                throw new ProviderException("Cannot delete a populated role.");
            }

            RoleData data = getRoleData(roleName);
            
            if (data != null)
            {
                _roles.Remove(data);
                save();
                return true;
            }
            else
            {
                return false;
            }            
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            if (!RoleExists(roleName))
            {
                throw new ProviderException("Role does not exist.");
            }

            RoleData data = getRoleData(roleName);
            if (data != null)
            {
                return data.Users.ToArray();
            }
            else
            {
                return new string[] { };
            }
        }

        public override string[] GetAllRoles()
        {
            string[] roles = new string[_roles.Count];
            for (int i = 0; i < _roles.Count; i++)
            {
                roles[i] = _roles[i].RoleName;
            }
            return roles;
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> foundRoles = new List<string>();
            foreach (RoleData data in _roles)
            {
                if (data.Users.Contains(username))
                    foundRoles.Add(data.RoleName);
            }
            return foundRoles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            if (!RoleExists(roleName))
            {
                //hack -> for older versions than 1.2.0
                if (roleName == "PowerUsers")
                {
                    Roles.CreateRole(RoleNames.PowerUsers.ToString());
                }
                else
                {
                    throw new ProviderException("Role does not exist.");
                }
            }
            return getRoleData(roleName).Users.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (!RoleExists(roleName))
            {
                //hack -> for older versions than 1.2.0
                if (roleName == "PowerUsers")
                {
                    Roles.CreateRole(RoleNames.PowerUsers.ToString());
                }
                else
                {
                    throw new ProviderException("Role does not exist.");
                }
            }
            return getRoleData(roleName).Users.Contains(username);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string rolename in roleNames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string rolename in roleNames)
            {
                RoleData data = getRoleData(rolename);
                foreach (string username in usernames)
                {
                    data.Users.Remove(username);
                }
            }
            save();
        }

        public override bool RoleExists(string roleName)
        {
            foreach (RoleData data in _roles)
            {
                if (data.RoleName.Equals(roleName, StringComparison.CurrentCulture)) 
                    return true;
            }
            return false;
        }

        private string getConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;
            else
                return configValue;
        }

        private void save()
        {
            lock (rolesfile)
            {
                using (FileStream writer = File.Create(rolesfile))
                {
                    XmlSerializer serializer = new XmlSerializer(_roles.GetType());
                    serializer.Serialize(writer, _roles);
                }
            }
        }

        private RoleData getRoleData(string rolename)
        {
            RoleData found = null;
            foreach(RoleData data in _roles)
            {
                if (data.RoleName == rolename)
                {
                    found = data;
                    break;
                }
            }
            return found;
        }

        public class RoleData
        {
            public string RoleName;
            public List<string> Users;
        }
    }
}
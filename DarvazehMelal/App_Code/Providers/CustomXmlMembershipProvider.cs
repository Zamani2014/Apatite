//===============================================================================================
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
//
//===============================================================================================

using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;

namespace MyWebPagesStarterKit.Providers
{
    /// <summary>
    /// Specialized MembershipProvider that uses a file (Users.config) to store its data.
    /// Passwords for the users are always stored as a salted hash (see: http://msdn.microsoft.com/msdnmag/issues/03/08/SecurityBriefs/)
    /// </summary>
    public class CustomXmlMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private string _passwordStrengthRegularExpression;
        private bool _enablePasswordReset;
        private bool _requiresUniqueEmail;

        private DataTable _users;

        private const string _cUserFilename = "Users.config";
        private const string _cProviderName = "CustomXmlMembershipProvider";
        private String _path = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/{0}", _cUserFilename));

        public override void Initialize(string name, NameValueCollection config)
        {
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
            _maxInvalidPasswordAttempts = Convert.ToInt32(getConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _passwordAttemptWindow = Convert.ToInt32(getConfigValue(config["passwordAttemptWindow"], "10"));
            _minRequiredNonAlphanumericCharacters = Convert.ToInt32(getConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(getConfigValue(config["minRequiredPasswordLength"], "7"));
            _passwordStrengthRegularExpression = Convert.ToString(getConfigValue(config["passwordStrengthRegularExpression"], ""));
            _enablePasswordReset = Convert.ToBoolean(getConfigValue(config["enablePasswordReset"], bool.TrueString));
            _requiresUniqueEmail = Convert.ToBoolean(getConfigValue(config["requiresUniqueEmail"], bool.TrueString));
            
            //load/create the usertable
            if (File.Exists(_path))
            {
                lock (_path)
                {
                    _users = new DataTable("UserTable");
                    _users.ReadXml(_path);
                }
            }
            else
            {
                _users = new DataTable("UserTable");
                _users.Columns.AddRange(new DataColumn[] {
                    new DataColumn("PKID", typeof(Guid)),
                    new DataColumn("Username", typeof(string)),
                    new DataColumn("ApplicationName", typeof(string)),
                    new DataColumn("Email", typeof(string)),
                    new DataColumn("Comment", typeof(string)),
                    new DataColumn("Salt", typeof(string)),
                    new DataColumn("Password", typeof(string)),
                    new DataColumn("IsApproved", typeof(bool)),
                    new DataColumn("LastActivityDate", typeof(DateTime)),
                    new DataColumn("LastLoginDate", typeof(DateTime)),
                    new DataColumn("LastPasswordChangedDate", typeof(DateTime)),
                    new DataColumn("CreationDate", typeof(DateTime)),
                    new DataColumn("IsOnLine", typeof(bool)),
                    new DataColumn("IsLockedOut", typeof(bool)),
                    new DataColumn("LastLockedOutDate", typeof(DateTime)),
                    new DataColumn("FailedPasswordAttemptCount", typeof(int)),
                    new DataColumn("FailedPasswordAttemptWindowStart", typeof(DateTime)),
                    new DataColumn("FailedPasswordAnswerAttemptCount", typeof(int)),
                    new DataColumn("FailedPasswordAnswerAttemptWindowStart", typeof(DateTime))
                    }
                );
                _users.AcceptChanges();
                save();
            }
        }



        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {
                ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, false);
                OnValidatingPassword(args);
                if (args.Cancel)
                {
                    if(args.FailureInformation != null)
                        throw args.FailureInformation;
                    else
                        throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
                }
                DataRow row = _users.Select(string.Format("Username='{0}'", username))[0];

                SaltedHash sh = SaltedHash.Create(newPassword);
                row["Salt"] = sh.Salt;
                row["Password"] = sh.Hash;
                row["LastPasswordChangedDate"] = DateTime.Now;
                row.AcceptChanges();
                save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (username == string.Empty)
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && GetUserNameByEmail(email) != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            SaltedHash sh = SaltedHash.Create(password);
            
            MembershipUser u = GetUser(username, false);
            if (u == null)
            {
                _users.Rows.Add(
                    Guid.NewGuid(), //PKID
                    username, //Username
                    ApplicationName,//ApplicationName
                    email, //Email
                    string.Empty, //Comment
                    sh.Salt, //salt for the password
                    sh.Hash, //password hash
                    isApproved, //IsApproved
                    DateTime.Now, //LastActivityDate
                    DateTime.Now, //LastLoginDate
                    DateTime.Now, //LastPasswordChangedDate
                    DateTime.Now, //CreationDate
                    false, //IsOnLine
                    false, //IsLockedOut
                    DateTime.MinValue, //LastLockedOutDate
                    0, //FailedPasswordAttemptCount
                    DateTime.MinValue, //FailedPasswordAttemptWindowStart
                    0, //FailedPasswordAnswerAttemptCount
                    DateTime.MinValue //FailedPasswordAnswerAttemptWindowStart
                    );
                _users.AcceptChanges();
                save();
                status = MembershipCreateStatus.Success;

                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if (rows.Length > 0)
            {
                if (deleteAllRelatedData)
                {
                    string[] roles = Roles.GetRolesForUser(username);
                    if (roles.Length > 0)
                        Roles.RemoveUserFromRoles(username, roles);
                }

                _users.Rows.Remove(rows[0]);
                _users.AcceptChanges();
                save();
                return true;
            }
            else
            {
                return false;
            }
        } 

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            if (string.IsNullOrEmpty(emailToMatch))
                throw new ArgumentException("emailToMatch is null or empty", "emailToMatch");
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex must be 0 or greater", "pageIndex");
            if (pageSize < 1)
                throw new ArgumentException("pageSize must be greater than 0", "pageSize");
            
            MembershipUserCollection coll = new MembershipUserCollection();
            DataRow[] rows = _users.Select(string.Format("Email LIKE '{0}'", emailToMatch),"Username ASC");

            for (int i = pageIndex * pageSize; (i < (pageIndex + 1) * pageSize) && (i < rows.Length); i++)
            {
                coll.Add(createMembershipUser(rows[i]));
            }
            totalRecords = rows.Length;
            return coll;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            if (string.IsNullOrEmpty(usernameToMatch))
                throw new ArgumentException("usernameToMatch is null or empty", "usernameToMatch");
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex must be 0 or greater", "pageIndex");
            if (pageSize < 1)
                throw new ArgumentException("pageSize must be greater than 0", "pageSize");

            MembershipUserCollection coll = new MembershipUserCollection();
            DataRow[] rows = _users.Select(string.Format("Username = '{0}'", usernameToMatch), "Username ASC");

            for (int i = pageIndex * pageSize; (i < (pageIndex + 1) * pageSize) && (i < rows.Length); i++)
            {
                coll.Add(createMembershipUser(rows[i]));
            }
            totalRecords = rows.Length;
            return coll;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex must be 0 or greater", "pageIndex");
            if (pageSize < 1)
                throw new ArgumentException("pageSize must be greater than 0", "pageSize");

            MembershipUserCollection coll = new MembershipUserCollection();
            DataRow[] rows = _users.Select(string.Empty, "Username ASC");

            for (int i = pageIndex * pageSize; (i < (pageIndex + 1) * pageSize) && (i < rows.Length); i++)
            {
                coll.Add(createMembershipUser(rows[i]));
            }
            totalRecords = rows.Length;
            return coll;
        }

        public override int GetNumberOfUsersOnline()
        {
            DateTime compareTime = DateTime.Now.Subtract(new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0));
            int usersOnline = 0;
            foreach (DataRow row in _users.Select())
            {
                if (((DateTime)row["LastActivityDate"]) > compareTime)
                    usersOnline++;
            }
            return usersOnline;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if (rows.Length > 0)
            {
                if (userIsOnline)
                {
                    rows[0]["LastActivityDate"] = DateTime.Now;
                    rows[0]["IsOnline"] = true;
                    rows[0].AcceptChanges();
                    save();
                }
                return createMembershipUser(rows[0]);
            }
            else
            {
                return null;
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            foreach(DataRow row in _users.Rows)
            {
                if (row["PKID"].Equals(providerUserKey))
                {
                    if (userIsOnline)
                    {
                        row["LastActivityDate"] = DateTime.Now;
                        row["IsOnline"] = true;
                        row.AcceptChanges();
                        save();
                    }
                    return createMembershipUser(row);
                }                    
            }
            return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            DataRow[] rows = _users.Select(string.Format("Email='{0}'", email));
            if (rows.Length > 0)
                return (string)rows[0]["Email"];
            else
                return null;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotSupportedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
                throw new NotSupportedException();

            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if (rows.Length > 0)
            {
                string newPassword = Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);
                SaltedHash sh = SaltedHash.Create(newPassword);
                rows[0]["Salt"] = sh.Salt;
                rows[0]["Password"] = sh.Hash;
                rows[0]["LastPasswordChangedDate"] = DateTime.Now;
                rows[0].AcceptChanges();
                save();
                return newPassword;
            }
            else
            {
                return null;
            }
        }

        public override bool UnlockUser(string userName)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", userName));
            if (rows.Length > 0)
            {
                rows[0]["IsLockedOut"] = false;
                rows[0].AcceptChanges();
                save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void UpdateUser(MembershipUser user)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", user.UserName));
            if (rows.Length > 0)
            {
                rows[0]["Email"] = user.Email;
                rows[0]["Comment"] = user.Comment;
                rows[0]["IsApproved"] = user.IsApproved;
                rows[0]["IsLockedOut"] = user.IsLockedOut;
                rows[0]["CreationDate"] = user.CreationDate;
                rows[0]["LastLoginDate"] = user.LastLoginDate;
                rows[0]["LastActivityDate"] = user.LastActivityDate;
                rows[0]["LastPasswordChangedDate"] = user.LastPasswordChangedDate;
                rows[0]["LastLockedOutDate"] = user.LastLockoutDate;
                rows[0]["IsOnline"] = user.IsOnline;
                rows[0]["IsLockedOut"] = user.IsLockedOut;
                rows[0].AcceptChanges();
                save();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if ((rows.Length > 0) && ((bool)rows[0]["IsApproved"] == true))
            {
                SaltedHash sh = SaltedHash.Create((string)rows[0]["Salt"], (string)rows[0]["Password"]);
                if (sh.Verify(password))
                {
                    rows[0]["LastLoginDate"] = DateTime.Now;
                    rows[0]["LastActivityDate"] = DateTime.Now;
                    rows[0].AcceptChanges();
                    save();
                    return true;
                }
            }
            return false;
        }

        #region private methods
        private MembershipUser createMembershipUser(DataRow row)
        {
            return new MembershipUser(
                        _cProviderName,
                        (string)row["Username"],
                        (Guid)row["PKID"],
                        (string)row["Email"],
                        string.Empty,
                        (string)row["Comment"],
                        (bool)row["IsApproved"],
                        (bool)row["IsLockedOut"],
                        (DateTime)row["CreationDate"],
                        (DateTime)row["LastLoginDate"],
                        (DateTime)row["LastActivityDate"],
                        (DateTime)row["LastPasswordChangedDate"],
                        (DateTime)row["LastLockedOutDate"]
                    );
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
            lock (_path)
            {
                _users.WriteXml(_path, XmlWriteMode.WriteSchema);
            }
        }
        #endregion
    }

    #region SaltedHash Class
    public sealed class SaltedHash
    {
        private readonly string _salt;
        private readonly string _hash;
        private const int saltLength = 6;

        public string Salt { get { return _salt; } }
        public string Hash { get { return _hash; } }

        public static SaltedHash Create(string password)
        {
            string salt = _createSalt();
            string hash = _calculateHash(salt, password);
            return new SaltedHash(salt, hash);
        }

        public static SaltedHash Create(string salt, string hash)
        {
            return new SaltedHash(salt, hash);
        }

        public bool Verify(string password)
        {
            string h = _calculateHash(_salt, password);
            return _hash.Equals(h);
        }

        private SaltedHash(string s, string h)
        {
            _salt = s;
            _hash = h;
        }

        private static string _createSalt()
        {
            byte[] r = _createRandomBytes(saltLength);
            return Convert.ToBase64String(r);
        }

        private static byte[] _createRandomBytes(int len)
        {
            byte[] r = new byte[len];
            new RNGCryptoServiceProvider().GetBytes(r);
            return r;
        }

        private static string _calculateHash(string salt, string password)
        {
            byte[] data = _toByteArray(salt + password);
            byte[] hash = _calculateHash(data);
            return Convert.ToBase64String(hash);
        }

        private static byte[] _calculateHash(byte[] data)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(data);
        }

        private static byte[] _toByteArray(string s)
        {
            return System.Text.Encoding.UTF8.GetBytes(s);
        }
    }
    #endregion
}

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Security;

namespace OnlineShop.Logic
{
    public class MyRoleProvider : RoleProvider
    {
        private string _ApplicationName;
        private readonly AutorizeRepository _repository = new AutorizeRepository();

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            _repository.AddUsersToRoles(usernames, roleNames);
        }

        public override string ApplicationName
        {
            get { return _ApplicationName; }
            set { _ApplicationName = value; }
        }

        public override void CreateRole(string roleName)
        {
            _repository.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (throwOnPopulatedRole)
            {
                return false;
            }
            _repository.DeleteRole(roleName);
            return true;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return _repository.FindUsersInRole(roleName, usernameToMatch).Select(x => x.Email).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return _repository.GetAllRole().Select(x => x.RoleName).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _repository.GetAllUsers().FirstOrDefault(x => x.Email == username);
            var roles = _repository.GetRolesForUser(user.Id);
            return roles.Select(x => x.RoleName).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = _repository.GetRole(roleName);
            var usernames = _repository.GetUsersForRole(role.Id);
            return usernames.Select(x => x.Email).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var users = GetUsersInRole(roleName);
            if (users.Contains(username))
            {
                return true;
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            _repository.RemoveUsersFromRoles(usernames, roleNames);
        }

        public override bool RoleExists(string roleName)
        {
            if (_repository.GetRole(roleName) != null)
            {
                return true;
            }
            return false;
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "CustomRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Role Provider");
            }

            base.Initialize(name, config);

            _ApplicationName = GetConfigValue(config["applicationName"],
                System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        }
    }
}
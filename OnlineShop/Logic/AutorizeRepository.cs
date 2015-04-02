using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using OnlineShop.Logic.Entity;
using Membership = OnlineShop.Logic.Entity.Membership;

namespace OnlineShop.Logic
{
    public class AutorizeRepository
    {
        private readonly Context _context = new Context();

        public void CreateRole(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(x => x.RoleName == roleName);
            if (role == null)
            {
                role = new Role { RoleName = roleName };
                _context.Roles.Add(role);
                SaveData();
            }
        }

        public void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (var username in usernames)
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == username);
                if (user != null)
                {
                    foreach (var rolename in rolenames)
                    {
                        var role = GetRole(rolename);
                        if (role != null)
                        {
                            if (!user.Roles.Contains(role))
                            {
                                user.Roles.Add(role);
                            }
                        }
                    }
                }
                SaveData();
            }

        }

        public void DeleteRole(string roleName)
        {
            var role = GetRole(roleName);
            _context.Roles.Remove(role);
            SaveData();
        }

        public List<User> FindUsersInRole(string roleName, string usernameToMatch)
        {
            var role = GetRole(roleName);
            return role.Users.ToList();
        }

        public List<Role> GetAllRole()
        {
            return _context.Roles.ToList();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public List<Role> GetRolesForUser(int id)
        {
            return _context.Users.First(x => x.Id == id).Roles.ToList();
        }

        public Role GetRole(string roleName)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleName == roleName);
        }

        public List<User> GetUsersForRole(int id)
        {
            return _context.Roles.First(x => x.Id == id).Users.ToList();
        }


        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == username);
                if (user != null)
                {
                    foreach (var rolename in roleNames)
                    {
                        var role = GetRole(rolename);
                        if (role != null)
                        {
                            if (user.Roles.Contains(role))
                            {
                                user.Roles.Remove(role);
                            }
                        }
                    }
                }
                SaveData();
            }
        }

        private void SaveData()
        {
            _context.SaveChanges();
        }

        #region Membership logic

        /*
                 public string CreatePasswordHash(string password)
        {
            string hashedPwd =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                    password, "sha1");
            return hashedPwd;
        }


        public MyMembershipUser GetUser(string username)
        {
            var users = _context.Users.FirstOrDefault(x => x.Email == username);
            if (users == null) return null;
            string _username = users.Email;
            int _providerUserKey = users.Id;
            string _email = users.Email;
            string _passwordQuestion = "";
            string _comment = "";
            bool _isApproved = false;
            bool _isLockedOut = false;
            DateTime _creationDate = (DateTime)users.Membership.CreateDate;
            DateTime _lastLoginDate = DateTime.Now;
            DateTime _lastActivityDate = DateTime.Now;
            DateTime _lastPasswordChangedDate = DateTime.Now;
            DateTime _lastLockedOutDate = DateTime.Now;
            MyMembershipUser user = new MyMembershipUser("CustomMembershipProvider",
                                                         _username,
                                                         _providerUserKey,
                                                         _email,
                                                         _passwordQuestion,
                                                         _comment,
                                                         _isApproved,
                                                         _isLockedOut,
                                                         _creationDate,
                                                         _lastLoginDate,
                                                         _lastActivityDate,
                                                         _lastPasswordChangedDate,
                                                         _lastLockedOutDate);
            return user;
        }


        public void RemoveUser(string username)
        {
            var user = _context.Users.First(x => x.Email == username);
            _context.Users.Remove(user);
            SaveData();
        }

        public string GetPassword(string username)
        {
            return _context.Users.First(x => x.Email == username).Membership.Password ?? "";
        }

        public string GetUserNameByEmail(string email)
        {
            var user = GetUser(email);
            if (user == null)
            {
                return "";
            }
            return user.FirstName + " " + user.LastName;
        }


        public void CreateUser(string username, string passwordQuestion, string password, string email)
        {
            var user = new User();
            user.Email = email;
            user.FirstName = username;
            user.LastName = passwordQuestion;
            var membership = new Membership { CreateDate = DateTime.Now, Password = CreatePasswordHash(password) };
            user.Membership = membership;
            _context.Users.Add(user);
            SaveData();
        }
         */

        #endregion

    }
}
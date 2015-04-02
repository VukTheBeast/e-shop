using System;
using System.Linq;
using System.Web.Security;
using OnlineShop.Logic.Entity;
using Membership = OnlineShop.Logic.Entity.Membership;

namespace OnlineShop.Infrastructure.Autorization
{
    public static class Autorization
    {
        public static AutorizationEnum CreateUser(string firstName, string lastName, string email, string password)
        {
            var result = AutorizationEnum.Success;
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    user=new User();
                    user.Email = email;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    var membership = new Membership { CreateDate = DateTime.Now, Password = CreateHash(password) };
                    user.Membership = membership;
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    result = AutorizationEnum.DuplicateEmail;
                }
            }
            return result;
        }

        public static AutorizationEnum CheckPassword(string email, string password)
        {
            var result = AutorizationEnum.Success;
            using (var context = new Context())
            {
                //context.Configuration.LazyLoadingEnabled = true;
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    result = AutorizationEnum.InvalidEmail;
                }
                else
                {
                    if (user.Membership.Password != CreateHash(password))
                    {
                        result = AutorizationEnum.InvalidPassword;
                    }
                }
            }
            return result;
        }

        public static AutorizationEnum ChangePassword(string email, string password, string newPassword)
        {
            var result = AutorizationEnum.Success;
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    result = AutorizationEnum.InvalidEmail;
                }
                else
                {
                    if (user.Membership.Password == CreateHash(password))
                    {
                        user.Membership.Password = CreateHash(newPassword);
                        context.SaveChanges();
                    }
                    else
                    {
                        result = AutorizationEnum.InvalidPassword;
                    }
                }
            }
            return result;
        }

        public static AutorizationEnum RestoreToken(string email, string token)
        {
            var result = AutorizationEnum.Success;
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    result = AutorizationEnum.InvalidEmail;

                }
                else
                {
                    user.Membership.PasswordVerificationToken = token;
                    context.SaveChanges();
                }
            }
            return result;
        }

        public static AutorizationEnum RestorePassword(string email, string token, string newPassword)
        {
            var result = AutorizationEnum.Success;
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    result = AutorizationEnum.InvalidEmail;
                }
                else
                {
                    if (user.Membership.PasswordVerificationToken == token)
                    {
                        user.Membership.Password = CreateHash(newPassword);
                        user.Membership.PasswordVerificationToken = string.Empty;
                        context.SaveChanges();
                    }
                    else
                    {
                        result=AutorizationEnum.InvalidToken;
                    }
                }
            }
            return result;
        }

        private static string CreateHash(string password)
        {
            string hashedPwd =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                    password, "sha1");
            return hashedPwd;
        }

        private static string CreateString(int length)
        {
            Random random = new Random();
            int size = length;
            char[] buff = new char[size];
            for (int i = 0; i < size; i++)
            {
                buff[i] = (char)random.Next(65, 122);
            }
            return new string(buff);
        }

        public static string GeneratePasswordResetToken(int length)
        {
            return CreateHash(CreateString(length));
        }
    }

    public enum AutorizationEnum
    {
        DuplicateEmail,
        InvalidEmail,
        InvalidPassword,
        InvalidToken,
        Success
    }
}
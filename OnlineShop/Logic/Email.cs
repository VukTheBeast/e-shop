using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using OnlineShop.Logic.Entity;
using OnlineShop.Models;

namespace OnlineShop.Logic
{
    public class Email : IEmail
    {
        private readonly string Adress;
        private readonly string Password;
        private readonly string SmtpServer;
        private readonly string UserName;

        public Email()
        {
            SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            UserName = ConfigurationManager.AppSettings["UserName"];
            Password = ConfigurationManager.AppSettings["Password"];
            Adress = ConfigurationManager.AppSettings["Address"];
        }


        public bool Register(string email, string password)
        {
            string title = "Register on the site";
            var message = new StringBuilder()
                .AppendLine("You registed online e-shop")
                .AppendLine("------------------------------------------------")
                .AppendLine()
                .AppendLine("Your email: " + email)
                .AppendLine("Your password: " + password)
                .AppendLine("------------------------------------------------")
                .AppendLine("Thank you for registering!");
            return SendMessage(email, title, message.ToString());
        }

        public bool ChangePassword(string email, string newPassword)
        {
            string title = "change password";
            var message = new StringBuilder()
                .AppendLine("You have changed your password")
                .AppendLine("------------------------------------------------")
                .AppendLine()
                .AppendLine("Your new password: " + newPassword)
                .AppendLine("------------------------------------------------");
            return SendMessage(email, title, message.ToString());
        }

        public bool ResetPassword(string email, string token)
        {
            string title = "Password recovery";
            var message = new StringBuilder()
                .AppendLine("Password recovery")
                .AppendLine("-------------------------------------------------------")
                .AppendLine()
                .AppendLine("The key for password recovery: " + token)
                .AppendLine("-------------------------------------------------------");
            return SendMessage(email, title, message.ToString());

        }

        public bool RestorePassword(string email, string newPassword)
        {
            string title = "Password recovery";
            var message = new StringBuilder()
            .AppendLine("Password recovery")
                .AppendLine("-------------------------------------------------------")
                .AppendLine()
                .AppendLine("Your new password: " + newPassword)
                .AppendLine("-------------------------------------------------------")
                .AppendLine("You can change your password in your profile on the site.");
            return SendMessage(email, title, message.ToString());
        }

        public bool NewOrder(string userName, string email, Cart cart, Address address)
        {
            string title = "New order";
            var message = new StringBuilder()
                .AppendLine("The order is formed")
                .AppendLine("-----------------------------")
                .AppendLine("" + userName + ", you ordered:");

            foreach (var line in cart.Lines)
            {
                int subtotal = line.Product.Price * line.Quantity;
                message.AppendFormat("{0} x {1} price: {2:c}", line.Quantity,
                    line.Product.Name,
                    subtotal);
                message.AppendLine();
            }
            message.AppendFormat("Only: {0:c}", cart.ComputeTotalValue())
                .AppendLine()
                .AppendLine("-----------------------------")
                .AppendLine("Shipping Address:")
                .AppendLine(address.ContactName)
                .AppendLine(address.Street)
                .AppendLine(address.City + " " + address.State + " " + address.PostalCode)
                .AppendLine(address.Country)
                .AppendLine("Phone: " + address.Telefon)
                .AppendLine("-----------------------------");
            return SendMessage(email, title, message.ToString());
        }

        public bool OrderProcess(string userName, string email, Order order)
        {
            var address = order.Address;
            int sum = 0;
            string title = "Order shipped";
            var message = new StringBuilder()
                .AppendLine("Order shipped")
                .AppendLine("-----------------------------")
                .AppendLine(" " + userName + ",you ordered:");

            foreach (var line in order.OrderProducts)
            {
                int subtotal = line.Product.Price * line.Count;
                sum += subtotal;
                message.AppendFormat("{0} x {1} price: {2:c}", line.Count,
                    line.Product.Name,
                    subtotal);
            }
            message.AppendFormat("Only: {0:c}", sum)
                .AppendLine("-----------------------------")
                .AppendLine("Shipping Address:")
                .AppendLine(address.ContactName)
                .AppendLine(address.Street)
                .AppendLine(address.City + " " + address.State + " " + address.PostalCode)
                .AppendLine(address.Country)
                .AppendLine("Phone: " + address.Telefon)
                .AppendLine("-----------------------------");
            return SendMessage(email, title, message.ToString());
        }

        private bool SendMessage(string email, string title, string message)
        {
            try
            {
                var smtp = new SmtpClient(SmtpServer)
                {
                    Credentials = new NetworkCredential(UserName, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Adress);
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = title;
                mailMessage.Body = message;
                smtp.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
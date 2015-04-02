using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value \"{0}\" must contain at least {2} symbols.", MinimumLength = 6)
        ]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [RegularExpression(
            @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-z0-9])?",
            ErrorMessage = "Wrong Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password can not be less than 6 and more than 100 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare("Password", ErrorMessage = "passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(20, ErrorMessage = "The name can not be more than 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(20, ErrorMessage = "The last name can not be more than 20 characters.")]
        public string LastName { get; set; }
    }

    public class EmailModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [RegularExpression(
            @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-z0-9])?",
            ErrorMessage = "Wrong Email")]
        public string Email { get; set; }
    }

    public class RestoreModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [RegularExpression(
            @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-z0-9])?",
            ErrorMessage = "Wrong Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Token")]
        public string Token { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password can not be less than 3 and more than 100 characters", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare("NewPassword", ErrorMessage = "passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
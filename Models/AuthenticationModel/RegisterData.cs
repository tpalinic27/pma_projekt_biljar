using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace biljar.Models.AuthenticationModel
{
    public class LoginModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

    }
    public class RegisterData
    {
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [PasswordPropertyText] public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}

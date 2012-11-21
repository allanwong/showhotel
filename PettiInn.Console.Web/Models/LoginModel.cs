using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PettiInn.Console.Web.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Captcha { get; set; }

        public bool RememberMe { get; set; }
    }
}
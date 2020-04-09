using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FAViews.Models
{
    public class Register
    {
        [Required(ErrorMessage = "*")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "*")]

        public string Lastname { get; set; }
        [Required(ErrorMessage = "*")]

        public string Email { get; set; }
        [Required(ErrorMessage = " *")]

        public string Username { get; set; }
        public string Password { get; set; }

    }
}
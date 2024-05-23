using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAndAuthorization.Models
{
    public class SignUp
    {
        [Key]
        public int Id { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Name Required")]
        public String Name { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(100, ErrorMessage = " The {0} must be at least {2} character long.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
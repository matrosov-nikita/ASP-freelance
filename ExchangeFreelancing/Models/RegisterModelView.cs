using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeFreelancing.Models
{
    public class RegisterModelView
    {
        [Required]
        [Display(Name = "Логин")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
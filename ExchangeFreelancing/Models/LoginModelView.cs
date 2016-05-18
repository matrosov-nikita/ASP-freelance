using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeFreelancing.Models
{
    public class LoginModelView
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ExchangeFreelancing.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]

        public string Surname { get; set; }
        [Display(Name = "Возраст")]

        public int Age { get; set; }
        [Display(Name = "Страна")]

        public string Country { get; set; }
        [Display(Name = "Город")]

        public string Town { get; set; }
        [Display(Name = "Дополнительная информация")]
        [DataType(DataType.MultilineText)]
        public string ExtraInformation { get; set; }
        [Required]
        [Display(Name = "Рейтинг")]
        public double Rating { get; set; }
        [Required]

        public int PositiveMarks { get; set; }
        [Required]

        public int NeutralMarks { get; set; }
        [Required]

        public int NegativeMarks { get; set; }
        [Required]
        public double AmountOfMoney { get; set; }

        public ApplicationUser()
        {

        }
    }
}
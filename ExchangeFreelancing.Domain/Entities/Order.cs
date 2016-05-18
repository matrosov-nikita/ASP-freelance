using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Entities
{
    public class Order
    {

        public int Id { get; set; }
        [Display(Name = "Категория")]
        [Required]
        public string Category { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Заголовок")]
        public string Header { get; set; }
        [DataType(DataType.MultilineText)]
        [Required, MaxLength(4000)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public DateTime DateAdd { get; set; }
        [Required]
        [Display(Name = "Срок сдачи задания")]
        public DateTime ExecutingTime { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Custom_Id { get; set; }
        [Required]
        public string  Custom_name { get; set; }
        public string Executer_Id { get; set; }
        [DataType(DataType.MultilineText)]
        [MaxLength(4000)]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }
    }

}

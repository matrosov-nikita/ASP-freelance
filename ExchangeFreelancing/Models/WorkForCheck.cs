using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeFreelancing.Models
{
    public class WorkForCheck
    {
        [DataType(DataType.MultilineText)]
        [Display(Name="Сообщение")]
        public string Message { get; set; }
        [Required]
        public int num_order { get; set; }
        public WorkForCheck(int num_order )
        {
            this.num_order = num_order;
        }
        public WorkForCheck()
        {

        }

    }
}
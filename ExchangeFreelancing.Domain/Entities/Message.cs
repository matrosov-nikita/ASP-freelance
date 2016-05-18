using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Entities
{
    public class Message   
   {
        [Key]
        public int message_id { get; set; }
        [Required]
        public string author { get; set; } 
        [DataType(DataType.MultilineText)]
        [Required]
        public string message { get; set; } 
        [Required]
        public int order_number { get; set; } 
    }
}

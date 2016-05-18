using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Entities
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string text { get; set; }
        [Required]
        public int order { get; set; }
        
        public string executer { get; set; }
        public DateTime DateAdd { get; set; }
    }
}

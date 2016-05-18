using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Entities
{
   public class Claim
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Entities
{
    public class File
    {
      
        [Required]
        [Key]
        public int File_Id { get; set; }
        [Required]
        public string path { get; set; }
        [Required]
        public string order_number { get; set; }

        public string extension { get; set; }
    }
}

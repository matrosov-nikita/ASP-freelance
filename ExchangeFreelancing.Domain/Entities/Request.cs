using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int Order_ID { get; set; }
        public string Excecuter_Id { get; set; }
        public string Customer_Id { get; set; }

    }
}

using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Abstract
{
   public interface IRequest
    {
       IQueryable<Request> Requests { get; }
       void Add(Request request);
       void Delete(int order_id, string ex_id=null);
      
    }
}

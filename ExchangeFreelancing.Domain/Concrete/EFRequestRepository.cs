using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Concrete
{
   public class EFRequestRepository: IRequest
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Request> Requests
        {
            get
            {
                return context.Requests;
            }
        }
        public void Add(Request request)
        {
            context.Requests.Add(request);
            context.SaveChanges();
        }


        public void Delete(int order_id, string ex_id=null)
        {
            if (ex_id != null)
            {
                Request del_request = context.Requests.FirstOrDefault(x => x.Order_ID == order_id && x.Excecuter_Id == ex_id);
                context.Requests.Remove(del_request);
            }
            else
            {
                IEnumerable<Request> requests = context.Requests.Where(x=>x.Order_ID==order_id);
                context.Requests.RemoveRange(requests);
            }
            context.SaveChanges();

        }
    }
}

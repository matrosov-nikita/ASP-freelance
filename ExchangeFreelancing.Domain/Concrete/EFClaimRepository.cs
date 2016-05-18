using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFClaimRepository:IClaim
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Claim> Claims
        {
            get
            {
                return context.Claims;
            }
        }

        public void Add(Claim claim)
        {
            context.Claims.Add(claim);
            context.SaveChanges();

        }
    }
}

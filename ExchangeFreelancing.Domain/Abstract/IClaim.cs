using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Abstract
{
    public interface IClaim
    {
        IQueryable<Claim> Claims { get; }
        void Add(Claim claim);

    }
}

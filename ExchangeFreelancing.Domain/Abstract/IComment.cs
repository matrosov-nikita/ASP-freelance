using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Abstract
{
   public interface IComment
    {
       IQueryable<Comment> Comments { get; }
       void Add(Comment comment);
    }
}

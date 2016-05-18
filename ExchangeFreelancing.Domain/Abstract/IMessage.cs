using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Abstract
{
    public interface IMessage
    {
        IQueryable<Message> Messages { get; }
        void Add(Message message);
    }
}

using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFMessageRepository: IMessage
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Message> Messages
        {
            get
            {
                return context.Messages;
            }
        }

        public void Add(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
        }
    }
}

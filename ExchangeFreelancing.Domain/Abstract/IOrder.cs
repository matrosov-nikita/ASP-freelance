using ExchangeFreelancing.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeFreelancing.Domain.Abstract
{
    public interface IOrder
    {
        IQueryable<Order> Orders { get; }
        void Add(Order order);

        Order Delete(int orderId);
        void AddExecuter(int order_id, string ex_id);
        void AddMessage(int order_id, string message);
        void ChangeState(int order_id, string state);
    }
}

using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFOrderRepository : IOrder
    {
        EFDbContext context = new EFDbContext();
        public IQueryable<Order> Orders
        {
            get
            {
                return context.Orders;
            }

        }
        public void ChangeState(int order_id, string state)
        {
            context.Orders.FirstOrDefault(x => x.Id == order_id).State = state;
            context.SaveChanges();
        }
        public void Add(Order order)
        {
            order.DateAdd = DateTime.Now;
            order.State = "Поиск исполнителей";
            context.Orders.Add(order);
            context.SaveChanges();

        }

        public Order Delete(int orderId)
        {
            Order find_Order = context.Orders.Find(orderId);
            if (find_Order != null)
            {
                context.Orders.Remove(find_Order);
                context.SaveChanges();
            }
            return find_Order;
        }


        public void AddExecuter(int order_id, string ex_id)
        {
            context.Orders.FirstOrDefault(x => x.Id == order_id).Executer_Id = ex_id;
            context.Orders.FirstOrDefault(x => x.Id == order_id).State = "В работе";
            context.SaveChanges();
        }
        public void AddMessage(int order_id, string message)
        {
            context.Orders.FirstOrDefault(x => x.Id == order_id).Message = message;
            context.Orders.FirstOrDefault(x => x.Id == order_id).State = "Выполнен";
            context.SaveChanges();


        }
    }
}

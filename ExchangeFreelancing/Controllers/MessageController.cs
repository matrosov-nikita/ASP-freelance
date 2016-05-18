using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ExchangeFreelancing.Controllers
{

    [Authorize]

    public class MessageController : Controller
    {
        private IMessage messages;
        public MessageController(IMessage messages)
        {
            this.messages = messages;
        }
        // GET: Message
        /// <summary>
        /// метод для отправки сообщения
        /// </summary>
        /// <param name="_order">айди заказа</param>
        /// <param name="user">пользователь</param>
        /// <param name="Message">сообщение</param>
        /// <returns>html-представления сообщения</returns>
        public ActionResult AddMessage(int _order, string user, string Message)
        {
            Message mes = new Message  { author = user, order_number = _order, message = Message };
            messages.Add(mes);
            return PartialView("_Message", mes);
        }

        /// <summary>
        /// получения всех сообщений по данному заказу
        /// </summary>
        /// <param name="order">айди заказа</param>
        /// <returns></returns>
        public ActionResult GetMessages(int order)
        {
            var result = messages.Messages.Where(x => x.order_number == order);
            return PartialView(result);
        }
    }
}
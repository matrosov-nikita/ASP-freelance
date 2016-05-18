using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ExchangeFreelancing.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ExchangeFreelancing.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        //
        // GET: /Request/

        private IRequest request_manager;
        private IOrder order_manager;
        ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationContext()));

        public RequestController(IRequest request, IOrder order)
        {
            request_manager = request;
            order_manager = order;
        }
        /// <summary>
        /// добавление заявки 
        /// </summary>
        /// <param name="order_id">номер заказа</param>
        /// <returns></returns>
        public ActionResult AddRequest(int order_id)
        {
            string id = User.Identity.GetUserId();
            Request req = request_manager.Requests.FirstOrDefault(x => x.Order_ID == order_id &&  x.Excecuter_Id == id);
            if (req == null)
            {
                ExchangeFreelancing.Domain.Entities.Request request = new Request();
                request.Customer_Id = order_manager.Orders.FirstOrDefault(x => x.Id == order_id).Custom_Id;
                request.Order_ID = order_id;
                request.Excecuter_Id = id;
                request_manager.Add(request);
                return View("Success", null, "Заявка успешно отправлена заказчику");
            }
            return View("Success", null, "Вы уже отправляли заявку на выполнение этого задания");
        }

        /// <summary>
        /// мето для подтверждения заявки
        /// </summary>
        /// <param name="order_id">номер заказа</param>
        /// <param name="ex_id">айди исполнителя</param>
        /// <returns></returns>
        public ActionResult ConfirmRequest(int order_id, string ex_id)
        {
            request_manager.Delete(order_id);          
            order_manager.AddExecuter(order_id, ex_id);
            return RedirectToRoute(new { controller = "Order", action = "GiveOrder" });
        }
        /// <summary>
        /// метод отклонения заявки
        /// </summary>
        /// <param name="order_id">айди заказ</param>
        /// <param name="ex_id">айди исполнителя</param>
        /// <returns></returns>
        public RedirectToRouteResult RefuseRequest(int order_id, string ex_id)
        {
            request_manager.Delete(order_id, ex_id);
            return RedirectToRoute(new {controller = "Order",action="GiveOrder"});
        }

        /// <summary>
        /// получить коллекцию ключ-заявка/значение - коллекция исполнителей, подавших заявки
        /// </summary>
        /// <param name="customer_name">имя заказчика</param>
        /// <returns></returns>
        public PartialViewResult GetExecuterRequest(string customer_name)
        {
            Dictionary<int, List<ApplicationUser>> dict = new Dictionary<int, List<ApplicationUser>>();
            string customer_id = manager.FindByName(customer_name).Id;
            foreach (var item in request_manager.Requests.Where(x => x.Customer_Id == customer_id))
            {               
                int order_id= order_manager.Orders.FirstOrDefault(x => x.Id == item.Order_ID).Id;
                ApplicationUser user = manager.FindById(item.Excecuter_Id);
               if (!dict.Keys.Contains(order_id)) dict.Add(order_id, new List<ApplicationUser>());
                dict[order_id].Add(user);
            }
            return PartialView(dict);

        }

        /// <summary>
        /// метод для получения списка собственных заявок
        /// </summary>
        /// <param name="name">имя пользователя</param>
        /// <returns></returns>
        public PartialViewResult GetOwnRequests(string name)
        {
            string executer_id = manager.FindByName(name).Id;
            List<Order> orderList = new List<Order>();
            foreach (var item in request_manager.Requests.Where(x => x.Excecuter_Id == executer_id))
            {
                orderList.Add(order_manager.Orders.FirstOrDefault(x => x.Id == item.Order_ID));
            }
            return PartialView(orderList);
        }
        /// <summary>
        /// получить информацию о пользователе
        /// </summary>
        /// <param name="user_id">айди пользователя</param>
        /// <returns></returns>
        public ActionResult GetInfoAboutExecuter(string user_id)
        {
            var user = manager.FindById(user_id);
            return View(user);
        }

    }
}

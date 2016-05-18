using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using ExchangeFreelancing.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace ExchangeFreelancing.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        //

        // GET: /Admin/
        private IOrder orders;
        private IClaim claims;
        private IMessage messages;
        private IFile files;
        private IComment comments;
        ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationContext()));

        public AdminController(IOrder orders, IClaim claims, IMessage messages, IFile files, IComment comments)
        {
            this.orders = orders;
            this.claims = claims;
            this.messages = messages;
            this.files = files;
            this.comments = comments;
        }
        /// <summary>
        /// отображение главной страницы панели администратора
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View();
        }
        /// <summary>
        /// метод для удаления пользоватея
        /// </summary>
        /// <param name="orderId">айдишник пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int orderId)
        {
            Order deleteOrder = orders.Delete(orderId);
            if (deleteOrder != null)
            {
                TempData["Message"] = string.Format("{0} was deleted", deleteOrder.Header);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// метод получения всех заказов в системе
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _GetOrders()
        {
            return PartialView(orders.Orders);

        }
        /// <summary>
        /// метод для получения всех пользваотелей в системе
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _GetUsers()
        {
            ApplicationContext context = new ApplicationContext();
           return PartialView(context.Users.ToList());
           
        }
        /// <summary>
        /// отображение страницы арбитража
        /// </summary>
        /// <returns></returns>
       
        public PartialViewResult _GetClaims()
        {
            return PartialView(claims.Claims);
        }

        /// <summary>
        /// детальная информация о заказе, переписки, файлах..
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ActionResult GetArbitrationInfo( int order)
        {
            List<object> list = new List<object>();
            var search_order = orders.Orders.FirstOrDefault(x => x.Id == order);
            list.Add(search_order);
            var search_messages = messages.Messages.Where(x => x.order_number == order);
            list.Add(search_messages);
            var search_files = files.Files.Where(x => x.order_number == order.ToString() && x.path.IndexOf(@"\App_Data\ExecuterFiles\") != -1);
            list.Add(search_files);
            var search_claim = claims.Claims.FirstOrDefault(x => x.order == order);
            list.Add(search_claim);
            return PartialView(list);
        }

        /// <summary>
        /// метод для подтверждения работы администартором
        /// </summary>
        /// <param name="order_id">айди заказа</param>
        /// <param name="message">сообщение от админа</param>
        /// <param name="Executer">исполнитель</param>
        /// <param name="Mark">оценка от админнистратора</param>
        /// <returns></returns>
        public ActionResult ConfirmWork(int order_id, string message, string Executer, string Mark, bool fromAdmin)
        {
            var user = manager.FindById(Executer);

            if (!fromAdmin)
            {
                user.Rating += (double)orders.Orders.FirstOrDefault(x => x.Id == order_id).Price;
            }
            string messageFromAdmin = message;
            message = claims.Claims.FirstOrDefault(x => x.order == order_id).Message + Environment.NewLine + "Арбитраж. " + messageFromAdmin;
            switch (Mark)
            {
                case "Положительная": user.PositiveMarks++; break;
                case "Нейтральная": user.NeutralMarks++; break;
                case "Отрицательная": user.NegativeMarks++; break;
            }
            manager.Update(user);
            comments.Add(new Comment { executer = Executer, text = message, order = order_id,DateAdd=DateTime.Now });
            orders.ChangeState(order_id, "Подтверждён");
            return View("Success", null, "Работа прошла проверку.");
        }
    }
}

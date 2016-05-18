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
    [Authorize]
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        private IOrder order;
        private IFile files;
        private IClaim claims;
        private IComment comments;
        ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationContext()));

        public OrderController(IFile files, IOrder order, IClaim claims, IComment comments)
        {
            this.order = order;
            this.files = files;
            this.claims = claims;
            this.comments = comments;
        }

        /// <summary>
        /// Метод отображает форму для размещения заказа
        /// </summary>
        /// <returns>html-форма размещеия заказа</returns>

        public ViewResult GiveOrder()
        {
            ViewBag.Current_role = "Customer";
            return View(new Order());
        }
        /// <summary>
        /// Сохранение коллеции файлов
        /// </summary>
        /// <param name="collection"> коллекция файлов</param>
        /// <param name="directory">директория</param>
        /// <param name="order_id">Id заказчика</param>
        public void SaveFile(IEnumerable<HttpPostedFileBase> collection, string directory, string order_id)
        {
            if (collection.First() != null)
            {
                DirectoryInfo info = Directory.CreateDirectory(Server.MapPath(string.Format("~/App_Data/{0}/{1}", directory, order_id)));
                foreach (HttpPostedFileBase item in collection)
                {
                    var file_path = Path.Combine(Server.MapPath(string.Format("~/App_Data/{0}/{1}", directory, order_id)), Path.GetFileName(item.FileName));
                    item.SaveAs(file_path);
                    files.Add(new ExchangeFreelancing.Domain.Entities.File { order_number = order_id, path = file_path, extension = Path.GetExtension(file_path) });
                }
            }
        }
        /// <summary>
        /// метод работает по post-запросу, добавляя в БД новый заказ
        /// </summary>
        /// <param name="_order"> новый заказ</param>
        /// <param name="collection">коллеция файлов заказа</param>
        /// <returns>html-уведомления о добавлении</returns>
        [HttpPost]
        public ViewResult GiveOrder(Order _order, IEnumerable<HttpPostedFileBase> collection)
        {

            _order.Custom_Id = User.Identity.GetUserId();
            _order.Custom_name = User.Identity.Name;
            this.order.Add(_order);

            SaveFile(collection, "CustomFiles", _order.Id.ToString());


            return View("Success", null, "Заказ успешно добавлен");
        }
        /// <summary>
        /// Метод для скачиваия файлов
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <param name="order">номер заказа</param>
        /// <param name="directory">имя директории</param>
        /// <returns>файл</returns>
        public ActionResult Download(string fileName, int order, string directory)
        {
            string path = Server.MapPath(string.Format("~/App_Data/{0}/{1}/{2}", directory, order, fileName));
            if (System.IO.File.Exists(path))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            return HttpNotFound();
        }

        /// <summary>
        /// получение заказов, размещенных в системе
        /// </summary>
        /// <param name="category">категория заказа</param>
        /// <param name="page">номер страницы</param>
        /// <returns> коллеция заказов</returns>
        public ViewResult GetOrders(string category, int? page)
        {
            ViewBag.Current_role = "Executer";
            ViewBag.Category = false;
            IEnumerable<Order> result = null;
            if (category != null)
            {
                result = order.Orders.Where(x => x.Category == category && x.State == "Поиск исполнителей").OrderByDescending(x => x.DateAdd);
                ViewBag.Category = true;
            }
            else
                result = order.Orders.Where(x => x.State == "Поиск исполнителей").OrderByDescending(x => x.DateAdd);

            int pageSize = 13;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Получить заказ, которые находятся в работе
        /// </summary>
        /// <param name="Role">роль заказчик/ислнитель</param>
        /// <param name="State">состояние заказа</param>
        /// <returns>возвращает представление с искомыми заказами</returns>
        public PartialViewResult _GetOrdersInWork(string Role, string State)
        {
            IEnumerable<Order> set = null;
            if (Role == "Customer")
            {
                set = order.Orders.Where(x => x.Custom_name == User.Identity.Name).Where(x => x.State == State);
            }
            else
            {
                string ex_id = User.Identity.GetUserId();
                set = order.Orders.Where(x => x.Executer_Id == ex_id).Where(x => x.State == State);
            }
            ViewBag.Current_role = Role;
            if (State == "В работе") return PartialView("_GetOrdersInWork", set);
            return PartialView("_GetCompletedOrders", set);
        }
        /// <summary>
        /// Отобразить форму для отправки работы
        /// </summary>
        /// <param name="num_order">номер заказа</param>
        /// <return>html-форма</returns>
        [HttpGet]
        public ActionResult SendWorkForCheck(int num_order)
        {
            return View(new WorkForCheck(num_order));
        }

        /// <summary>
        /// Отправка выполненой работы на проверку
        /// </summary>
        /// <param name="work">работа</param>
        /// <param name="collection">коллекция файлов</param>
        /// <param name="num_order">номер заказа</param>
        /// <returns>уведомлении о статусе отправки</returns>
        [HttpPost]
        public ActionResult SendWorkForCheck(WorkForCheck work, IEnumerable<HttpPostedFileBase> collection, int num_order)
        {
            order.AddMessage(num_order, work.Message);
            SaveFile(collection, "ExecuterFiles", num_order.ToString());
            return View("Success", null, "Работа успешно отправлена");
        }

        /// <summary>
        /// Просмотр выполненных заказов
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <returns>html-представления с выполненными заказами</returns>
        public ActionResult ViewCompletedOrder(int id)
        {
            return View(order.Orders.FirstOrDefault(x => x.Id == id));
        }
        /// <summary>
        /// метод для подтверждения работы
        /// </summary>
        /// <param name="order_id">номер заказа</param>
        /// <param name="message">сообщение от заказчика</param>
        /// <param name="Executer">исполнитель</param>
        /// <param name="Mark">оценка</param>
        /// <returns>уведомлении о подтверждении</returns>
        public ActionResult ConfirmWork(int order_id, string message, string Executer, string Mark)
        {       
            var user = manager.FindById(Executer);
             user.Rating += (double)order.Orders.FirstOrDefault(x => x.Id == order_id).Price;
            user.AmountOfMoney += (double)order.Orders.FirstOrDefault(x => x.Id == order_id).Price;

            switch (Mark)
            {
                case "Положительная": user.PositiveMarks++; break;
                case "Нейтральная": user.NeutralMarks++; break;
                case "Отрицательная": user.NegativeMarks++; break;
            }
            manager.Update(user);
            comments.Add(new Comment { executer = Executer, text = message, order = order_id,DateAdd=DateTime.Now });
            order.ChangeState(order_id, "Подтверждён");
            return View("Success", null, "Работа прошла проверку.Деньги переведены заказчику");
        }

        /// <summary>
        /// Метод для отправки работы в арбитраж
        /// </summary>
        /// <param name="order_id">номер заказа</param>
        /// <param name="message">сообщение</param>
        /// <returns></returns>
        public ActionResult RefuseWork(int order_id, string message)
        {
            claims.Add(new Claim() { order = order_id, Author = User.Identity.GetUserId(), Message = message });
            order.ChangeState(order_id, "Арбитраж");
            return View("Success", null, "Работа передана в арбитраж");
        }

    }
}

using ExchangeFreelancing.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web;
using ExchangeFreelancing.Domain.Abstract;
using System.IO;
using System.Linq;
using PagedList.Mvc;
using PagedList;
using System.IO.Compression;
namespace ExchangeFreelancing.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationContext()));
        private IFile file_manager;
        private IComment comments;
        public ProfileController(IFile file, IComment comments)
        {
            file_manager = file;
            this.comments = comments;
        }
        /// <summary>
        /// доавбление файлов в портфолио
        /// </summary>
        /// <param name="collection">коллекция файлов</param>
        /// <param name="directory">имя директории</param>
        /// <param name="custom_id">айли заказчика</param>
        public void SaveFile(IEnumerable<HttpPostedFileBase> collection, string directory, string custom_id, string folder_name)
        {
            if (collection.First() != null)
            {
                DirectoryInfo info = Directory.CreateDirectory(Server.MapPath(string.Format("~/App_Images/{0}/{1}/{2}", directory, custom_id, folder_name)));
                foreach (HttpPostedFileBase item in collection)
                {
                    var file_path = Path.Combine(Server.MapPath(string.Format("~/App_Images/{0}/{1}/{2}", directory, custom_id, folder_name)), Path.GetFileName(item.FileName));
                    item.SaveAs(file_path);
                    file_manager.Add(new ExchangeFreelancing.Domain.Entities.File { order_number = custom_id, path = file_path, extension = Path.GetExtension(file_path) });
                }
            }
        }
        /// <summary>
        /// скачивание работы с портфолио
        /// </summary>
        /// <param name="folder">путь к папке</param>
        /// <returns>zip-архив с файлами</returns>
        public ActionResult Download(string folder)
        {
            string full_parent_name = Directory.GetParent(folder).FullName;
            string short_parent_name = Directory.GetParent(folder).Name;
            string fileDownloadName = string.Format("{0}_Work.zip",short_parent_name);
            string destination_path = full_parent_name + "\\" + fileDownloadName;
            ZipFile.CreateFromDirectory(folder, destination_path);

            byte[] fileBytes = System.IO.File.ReadAllBytes(destination_path);
            FileContentResult content = File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileDownloadName);
            System.IO.File.Delete(destination_path);
            return content;         
        }

        /// <summary>
        /// отображение информации о пользователе
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public PartialViewResult _UserInfo(string user_id)
        {
            ApplicationUser user = null;
            if (user_id == null)
            {
                 user = manager.FindById(User.Identity.GetUserId());
            }
            else
            {
                user = manager.FindById(user_id);
            }
            return PartialView(user);
        }
        /// <summary>
        /// отображеие работ из портфолио
        /// </summary>
        /// <param name="user_id">айди пользвателя</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _Portfolio(string user_id)
        {
            string id = null;
            string[] directories = null;
            if (user_id == null)
            {
                id = User.Identity.GetUserId();
                ViewBag.UserId = id;
            }
            else
            {
                id = user_id;
                ViewBag.UserId = user_id;
            }
            string path = Server.MapPath(string.Format("~/App_Images/UserFiles/" + id));
            if (Directory.Exists(path))
            {
                directories = Directory.GetDirectories(path);
            }
            return PartialView(directories);

        }
        /// <summary>
        /// сохрание работ в портфолио
        /// </summary>
        /// <param name="collection">коллекция файлов</param>
        /// <param name="name">имя работы</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _Portfolio(IEnumerable<HttpPostedFileBase> collection, string name)
        {
          SaveFile(collection, "UserFiles", User.Identity.GetUserId(),name);
          string id = User.Identity.GetUserId();
          ViewBag.UserId = id;
          return PartialView("_Work", Server.MapPath(string.Format("~/App_Images/UserFiles/{0}/{1}", id, name)));

        }

        /// <summary>
        /// метод для редактирования профиля пользователя
        /// </summary>
        /// <param name="user">юзер</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _UserInfo(ApplicationUser user)
        {
            ApplicationUser model = manager.FindById(User.Identity.GetUserId());
            model.UserName = user.UserName;
            model.Name = user.Name;
            model.Surname = user.Surname;
            model.Email = user.Email;
            model.Country = user.Country;
            model.Town = user.Town;
            model.Age = user.Age;
            model.ExtraInformation = user.ExtraInformation;
            manager.Update(model);
            return View("Success",null,"Изменения успешно сохранены");
        }

        /// <summary>
        /// получение комментариев о пользователе
        /// </summary>
        /// <param name="user_id">айди пользователя</param>
        /// <returns></returns>
        public PartialViewResult _RatingInfo(string user_id)
        {
            
            string user = null;
            if (user_id == null)
            {
                user = User.Identity.GetUserId();
            }
            else
            {
                user = user_id;
            }
            ViewBag.Rating = manager.FindById(user).Rating;
            var result = comments.Comments.Where(x => x.executer == user).OrderBy(x => x.DateAdd);
          
            return PartialView(result);
        }

    }
}

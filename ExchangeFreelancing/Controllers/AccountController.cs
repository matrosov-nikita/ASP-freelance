using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExchangeFreelancing.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using ExchangeFreelancing.Domain.Concrete;

namespace ExchangeFreelancing.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        /// <summary>
        /// Отображеия формы для авторизации
        /// </summary>
        /// <param name="returnUrl">адрес перенаправления после авторизации </param>
        /// <returns>форма авторизации</returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// метод авторизации пользователя
        /// </summary>
        /// <param name="model"> данные из формы авторизации</param>
        /// <param name="returnUrl">куда перенаправить</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModelView model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user =  UserManager.Find(model.Username, model.Password);           
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {                 
                    ClaimsIdentity claim =  UserManager.CreateIdentity(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                   
                     return RedirectToAction("List", "Home");                                                                 
                }
            }
            return View(model);
        }
        /// <summary>
        /// метод выхода из система(разлогиниться)
        /// </summary>
        /// <returns>перенаправдение на форму автризации</returns>
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// отображение формы регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
   
        public ActionResult Register()
        {
            return View();
        }
        

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
     
        /// <summary>
        /// метод для регистрации нового пользваотеля
        /// </summary>
        /// <param name="model"> данные из формы регистрации</param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModelView model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                           .GetUserManager<ApplicationUserManager>();
                    // если создание прошло успешно, то добавляем роль пользователя
                 
                    userManager.AddToRole(user.Id, "user");

                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("List", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
    }

}



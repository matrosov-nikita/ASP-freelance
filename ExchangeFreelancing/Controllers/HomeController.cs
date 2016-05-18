using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ExchangeFreelancing.Models;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ExchangeFreelancing.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
       
      /// <summary>
      /// отображение главной страницы
      /// </summary>
      /// <returns></returns>
        public ViewResult List()
        {
           return View();
        }
      
    }
}

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
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        private ICategory cat;
        public MenuController(ICategory categ)
        {
            cat = categ;
        }

        /// <summary>
        /// отображение категорий в левой части экрана
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public PartialViewResult _Nav(string category =null)
        {
            ViewBag.SelectedCategory = category;
          
            return PartialView(cat.Categories);
        }

    }
}

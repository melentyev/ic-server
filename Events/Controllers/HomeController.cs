using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Events.Filters;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        [CheckModelForNull]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Events.Abstract;
using Events.Concrete;
using Events.Filters;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        private IEventsRepository eventsRepository = new EFEventsRepository();
        public ActionResult Index()
        {
            //var tmp = eventsRepository.Objects.First();
            //var nm = tmp.EventId;
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Spatial;

using Events.Abstract;
using Events.Concrete;
using Events.Filters;
using Events.Models;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        private IEventsRepository eventsRepository = new EFEventsRepository();
        public ActionResult Index()
        {
            var tmp = eventsRepository.Objects.FirstOrDefault();
            if (tmp != null)
            {
                var nm = tmp.EventId;
            }
            return View();
        }
        public async Task<string> AddTestData()
        {
            AppUserManager UserManager = Startup.UserManagerFactory();
            ApplicationUser usr1 = new ApplicationUser
            {
                UserName = "user"
            };

            IdentityResult result = await UserManager.CreateAsync(usr1, "123456");
            List<Event> evs = new List<Event> {
                new Event 
                { 
                    DateCreate = DateTime.UtcNow, UserId = usr1.Id, 
                    Description = "aaa bbb ccc",
                    Location = DbGeography.FromText(String.Format("POINT({0} {1})", "-122.335197", "47.646711")) 
                },
                new Event { DateCreate = DateTime.UtcNow, UserId = usr1.Id, Description = "a11a11a b22bb Событие ccc" }
            };
            var tasks = evs.Select(e => eventsRepository.SaveInstance(e)).ToArray();
            Task.WaitAll(tasks);
            return "OK DA";
        }
    }
}

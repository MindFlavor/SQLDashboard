using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQLDashboard.Controllers
{
    public class mssqlController : Controller
    {
        public ActionResult Index(Guid guid)
        {
            return View();
        }
    }
}
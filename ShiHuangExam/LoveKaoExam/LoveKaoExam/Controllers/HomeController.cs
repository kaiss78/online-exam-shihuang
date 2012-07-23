using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoveKaoExam.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult IndexTeacher()
        {
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult IndexStudent()
        {
            return View();
        }
    }
}

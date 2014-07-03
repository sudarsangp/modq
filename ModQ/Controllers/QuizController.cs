using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModQ.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        // GET: Quiz
        public ActionResult LoginLanding()
        {
            return View();
        }
    }
}
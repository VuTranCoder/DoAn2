using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class TinTucController : Controller
    {
        // GET: TinTuc
        public ActionResult TinTuc()
        {
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View("TinTuc");
        }
    }
}
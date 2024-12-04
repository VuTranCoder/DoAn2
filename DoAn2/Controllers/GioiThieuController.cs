using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class GioiThieuController : Controller
    {
        // GET: GioiThieu
        public ActionResult GioiThieu()
        {
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View("GioiThieu");
        }
    }
}
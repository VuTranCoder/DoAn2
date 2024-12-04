using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult SanPham()
        {
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View("SanPham");
        }
    }
}
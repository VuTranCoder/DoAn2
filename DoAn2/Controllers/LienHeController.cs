﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        public ActionResult LienHe()
        {
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View("LienHe");
        }
    }
}
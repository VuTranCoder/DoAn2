﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class DanhGiavaGopYController : Controller
    {
        // GET: DanhGiavaGopY
        public ActionResult DanhGiavaGopY()
        {
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
    }
}
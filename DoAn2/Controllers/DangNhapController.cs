using DoAn2.Model;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class DangNhapController : Controller
    {
        DoAn2Entities4 db = new DoAn2Entities4();

        // GET: DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: DangNhap
        [HttpPost]
        public ActionResult DangNhap(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = db.TAI_KHOAN.FirstOrDefault(u => u.TEN_TK == email && u.MK == password);
                if (user != null)
                {
                    Session["UserID"] = user.ID_TK;
                    Session["UserEmail"] = user.TEN_TK;

                    if (user.ID_TK == "1")
                    {
                        return RedirectToAction("IndexAdmin", "HomeAdmin", new { area = "admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Thông tin đăng nhập không chính xác.";
                }
            }
            return View();
        }

        // GET: DangXuat
        public ActionResult DangXuat()
        {
            // Xóa tất cả thông tin trong session
            Session.Clear();
            return RedirectToAction("DangNhap", "DangNhap");
        }
    }
}

using DoAn2.Model;
using System.Linq;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class KhachHangController : Controller
    {
        DoAn2Entities4 db = new DoAn2Entities4();

        // GET: KhachHang/ThemKhachHang
        public ActionResult ThemKhachHang()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemKhachHang(string tenKh, string soDienThoai, string diaChi)
        {
            if (ModelState.IsValid)
            {
                // Get max ID and increment by 1
                int maxId = db.KHACH_HANG.Any() ? db.KHACH_HANG.Max(u => u.MA_KH) : 0;
                int newId = maxId + 1;

                // Get user info from session
                string userId = Session["UserID"] as string;
                string userEmail = Session["UserEmail"] as string;

                // Create new customer
                var newKhachHang = new KHACH_HANG
                {
                    MA_KH = newId,
                    TEN_KH = tenKh,
                    SDT = int.Parse(soDienThoai),
                    DIACHI = diaChi,
                    ID_TK = userId,
                    EMAIL = userEmail
                };

                db.KHACH_HANG.Add(newKhachHang);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Khách hàng đã được thêm thành công!";

                // Redirect to login page
                return RedirectToAction("DangNhap", "DangNhap");
            }

            return View();
        }
    }
}

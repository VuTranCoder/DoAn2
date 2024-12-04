using DoAn2.Model;
using System.Linq;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class DangKyController : Controller
    {
        DoAn2Entities4 db = new DoAn2Entities4();

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(string email, string password, string diachi)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.TAI_KHOAN.FirstOrDefault(u => u.TEN_TK == email);
                if (existingUser == null)
                {
                    // Generate new ID
                    string maxId = db.TAI_KHOAN.Max(u => u.ID_TK);
                    int newIdNumber = 1;

                    if (!string.IsNullOrEmpty(maxId))
                    {
                        int startIndex = maxId.IndexOf("TK") + 2;
                        if (int.TryParse(maxId.Substring(startIndex), out int currentNumber))
                        {
                            newIdNumber = currentNumber + 1;
                        }
                    }

                    string newId = "TK" + newIdNumber.ToString("D5");

                    // Create new user
                    var newUser = new TAI_KHOAN
                    {
                        ID_TK = newId,
                        TEN_TK = email,
                        MK = password
                    };
                    db.TAI_KHOAN.Add(newUser);
                    db.SaveChanges();

                    // Save user info in session
                    Session["UserID"] = newUser.ID_TK;
                    Session["UserEmail"] = newUser.TEN_TK;
                    Session["UserAddress"] = diachi;

                    return RedirectToAction("ThemKhachHang", "KhachHang");
                }
                else
                {
                    ViewBag.ErrorMessage = "Email đã tồn tại.";
                }
            }
            return View();
        }
    }
}

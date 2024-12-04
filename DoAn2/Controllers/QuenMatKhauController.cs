using DoAn2.Model;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class QuenMatKhauController : Controller
    {
        // GET: QuenMatKhau
        public ActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendVerificationCode(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Vui lòng nhập email." });
            }

            // Generate a 4-digit verification code
            Random random = new Random();
            int verificationCode = random.Next(1000, 9999);

            // Save the code in the session
            Session["VerificationCode"] = verificationCode;
            Session["ResetEmail"] = email;

            // Send the code via email
            var isEmailSent = SendEmail(email, verificationCode);

            if (isEmailSent)
            {
                return Json(new { success = true, message = "Mã xác nhận đã được gửi tới email của bạn." });
            }
            else
            {
                return Json(new { success = false, message = "Gửi email thất bại. Vui lòng thử lại." });
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(string verificationCode, string password, string passwordConfirm)
        {
            if (string.IsNullOrEmpty(verificationCode) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ thông tin.";
                return View("QuenMatKhau");
            }

            // Get the code and email from the session
            int storedCode = (int)Session["VerificationCode"];
            string email = (string)Session["ResetEmail"];

            if (storedCode.ToString() != verificationCode)
            {
                ViewBag.ErrorMessage = "Mã xác nhận không đúng.";
                return View("QuenMatKhau");
            }

            // Reset the password in the database
            using (var db = new DoAn2Entities4())
            {
                var user = db.TAI_KHOAN.FirstOrDefault(u => u.TEN_TK == email);
                if (user != null)
                {
                    user.MK = password;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Không tìm thấy tài khoản.";
                    return View("QuenMatKhau");
                }
            }

            ViewBag.SuccessMessage = "Mật khẩu đã được đặt lại thành công.";
            return View("QuenMatKhau");
        }

        private bool SendEmail(string toEmail, int verificationCode)
        {
            try
            {
                var fromEmail = "youremail@example.com";
                var fromPassword = "yourpassword";

                var message = new MailMessage();
                message.To.Add(toEmail);
                message.From = new MailAddress(fromEmail);
                message.Subject = "Mã xác nhận đặt lại mật khẩu";
                message.Body = $"Mã xác nhận của bạn là: {verificationCode}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, fromPassword)
                };

                smtp.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

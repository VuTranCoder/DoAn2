using DoAn2.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace DoAn2.Controllers
{
    public class TungLoaiSanPhamController : Controller
    {
        DoAn2Entities4 db=new DoAn2Entities4();
        // GET: TungLoaiSanPham
        public ActionResult ComHeoQuay()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Heo Quay");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult ComChienHeoQuay()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Chiên Heo Quay");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult ComHeoQuayDuaCai()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Heo Quay Dưa Cải");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult ComXaXiuTTN()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Xá Xíu Thập Tam Nương");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult ComGaKhoGung()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Gà Kho Gừng");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult BoSotTieuDen()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Bò Sốt Tiêu Đen");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult BoXaoBongHe()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Bò Xào Bông Hẹ");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult BoXaoDauRong()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Bò Xào Đậu Rồng");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult BoXaoKhoQua()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Bò Xào Khổ Qua");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult BoXaoOtChuong()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Cơm Bò Xào Ớt Chuông");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult CanhCaiNgotThit()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Canh CảiNgọt Thịt");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult CanhRongBienThit()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Canh Rong Biển Thịt");
            ViewBag.Product = product; 
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult KhoQuaXaoTrung()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Khổ Qua Xào Trứng");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        public ActionResult TrungQuay()
        {
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == "Trứng Quậy");
            ViewBag.Product = product;
            ViewBag.IsLoggedIn = AuthHelper.IsLoggedIn(Session);
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(string productName, int quantity)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu
            SANPHAM product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == productName);
            if (product == null)
            {
                // Xử lý khi không tìm thấy sản phẩm
                return HttpNotFound();
            }

            // Lấy giá của sản phẩm
            decimal price = product.GIAMON ?? 0;

            // Lấy giỏ hàng từ session
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }

            // Kiểm tra sản phẩm có trong giỏ hàng chưa
            CartItem existingItem = cart.FirstOrDefault(i => i.ProductName == productName);
            if (existingItem != null)
            {
                // Nếu có rồi thì tăng số lượng
                existingItem.Quantity += quantity;
            }
            else
            {
                // Nếu chưa có thì thêm mới vào giỏ hàng
                cart.Add(new CartItem { ProductName = productName, Quantity = quantity, Price = price });
            }

            // Lưu giỏ hàng vào session
            Session["Cart"] = cart;

            // Chuyển hướng về trang giỏ hàng
            return RedirectToAction("GioHang", "GioHang");
        }
    }
}
using DoAn2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAn2.Controllers
{
    public class GioHangController : Controller
    {
        DoAn2Entities4 db = new DoAn2Entities4();

        public ActionResult GioHang()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            string userId = Session["UserID"].ToString();
            var khachHang = db.KHACH_HANG.FirstOrDefault(kh => kh.ID_TK == userId);

            if (khachHang == null)
            {
                TempData["Message"] = "Không tìm thấy thông tin khách hàng.";
                return RedirectToAction("GioHang");
            }

            ViewBag.CustomerName = khachHang.TEN_KH;
            ViewBag.PhoneNumber = khachHang.SDT;
            ViewBag.Address = khachHang.DIACHI;

            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            decimal totalValue = 0;
            if (cart != null)
            {
                totalValue = cart.Sum(item => item.Quantity * item.Price);
            }

            int itemCount = cart != null ? cart.Sum(item => item.Quantity) : 0;
            decimal shippingFee = itemCount >= 5 ? 0 : 20000;
            decimal totalOrderAmount = totalValue + shippingFee;

            ViewBag.TotalValue = totalValue;
            ViewBag.ShippingFee = shippingFee;
            ViewBag.TotalOrderAmount = totalOrderAmount;

            return View("GioHang", cart);
        }

        [HttpPost]
        public ActionResult IncreaseQuantity(string productName)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                CartItem item = cart.FirstOrDefault(i => i.ProductName == productName);
                if (item != null)
                {
                    item.Quantity++;
                }
            }
            return RedirectToAction("GioHang");
        }

        [HttpPost]
        public ActionResult DecreaseQuantity(string productName)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                CartItem item = cart.FirstOrDefault(i => i.ProductName == productName);
                if (item != null && item.Quantity > 1)
                {
                    item.Quantity--;
                }
            }
            return RedirectToAction("GioHang");
        }

        [HttpPost]
        public ActionResult DatHang()
        {
            if (Session["UserID"] == null)
            {
                TempData["Message"] = "Vui lòng đăng nhập trước khi đặt hàng.";
                return RedirectToAction("DangNhap", "DangNhap");
            }

            string userId = Session["UserID"].ToString();
            var khachHang = db.KHACH_HANG.FirstOrDefault(kh => kh.ID_TK == userId);

            if (khachHang == null)
            {
                TempData["Message"] = "Không tìm thấy thông tin khách hàng.";
                return RedirectToAction("GioHang");
            }

            int newOrderId = db.DON_DAT_HANG.Any() ? db.DON_DAT_HANG.Max(d => d.MA_DH) + 1 : 1;

            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            decimal totalValue = cart != null ? cart.Sum(item => item.Quantity * item.Price) : 0;
            int itemCount = cart != null ? cart.Sum(item => item.Quantity) : 0;
            decimal shippingFee = itemCount >= 5 ? 0 : 20000;
            decimal totalOrderAmount = totalValue + shippingFee;

            var newOrder = new DON_DAT_HANG
            {
                MA_DH = newOrderId,
                MA_KH = khachHang.MA_KH,
                MA_NV = 2,
                DIACHIGIAOHANG = khachHang.DIACHI,
                NGAY_GIODAT = DateTime.Now,
                TRANGTHAI_DH = "Chưa hoàn thành",
                PT_THANHTOAN = "Thanh toán khi nhận hàng",
                ShippingFee = shippingFee
            };

            db.DON_DAT_HANG.Add(newOrder);
            db.SaveChanges();

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    var product = db.SANPHAMs.FirstOrDefault(p => p.TEN_SP == item.ProductName && p.GIAMON == item.Price);
                    if (product != null)
                    {
                        var orderDetail = new CTDH
                        {
                            MA_DH = newOrderId,
                            MA_SP = product.MA_SP,
                            TEN_MON = item.ProductName,
                            SOLUONG = item.Quantity
                        };
                        db.CTDHs.Add(orderDetail);
                    }
                }
                db.SaveChanges();
            }

            Session["Cart"] = null;
            DateTime deliveryTime = DateTime.Now.AddMinutes(30);
            TempData["Message"] = "Bạn đã đặt hàng thành công! Mã đơn hàng: " + newOrderId;
            TempData["DeliveryTime"] = deliveryTime;

            return RedirectToAction("GioHang");
        }
    }

    public class CartItem
    {
        public int MaSP { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public decimal ShippingFee { get; set; }
    }
}

using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn2.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace DoAn2.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    public class HomeAdminController : Controller
    {
        DoAn2Entities4 db=new DoAn2Entities4();
        [Route("")]
        [Route("index")]
        public ActionResult IndexAdmin()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            return View();
        }
        [Route("danhmucsanpham")]
        public ActionResult DanhMucSanPham()
        {
            var lstSanPham=db.SANPHAMs.ToList();
            return View(lstSanPham);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public ActionResult ThemSanPhamMoi()
        {
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPhamMoi(SANPHAM sanPham)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra sự tồn tại của mã sản phẩm
                var existingProduct = db.SANPHAMs.FirstOrDefault(sp => sp.MA_SP == sanPham.MA_SP);
                if (existingProduct != null)
                {
                    ModelState.AddModelError("MaSp", "Mã sản phẩm này đã tồn tại.");
                    return View(sanPham);
                }

                db.SANPHAMs.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }//Sửa Sản Phẩm
        [Route("SuaSanPham")]
        [HttpGet]
        public ActionResult SuaSanPham(string maSanPham)
        {
            if (maSanPham == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int sanPhamId;
            if (!int.TryParse(maSanPham, out sanPhamId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sanPham = db.SANPHAMs.Find(sanPhamId);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham(SANPHAM sanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // Lấy đường dẫn thư mục lưu hình ảnh
                    var uploadDir = "~/Uploads/Images";
                    var uploadPath = Server.MapPath(uploadDir);

                    // Kiểm tra và tạo thư mục nếu không tồn tại
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var imagePath = Path.Combine(uploadPath, file.FileName);
                    var imageUrl = Path.Combine(uploadDir, file.FileName);

                    // Lưu hình ảnh lên server
                    file.SaveAs(imagePath);

                    // Cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu
                    sanPham.HINHANH = imageUrl;
                }

                db.Entry(sanPham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }
        [Route("XoaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaSanPham(string maSanPham)
        {
            if (string.IsNullOrEmpty(maSanPham))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int maSanPhamInt;
            if (!int.TryParse(maSanPham, out maSanPhamInt))
            {
                TempData["ErrorMessage"] = "Mã sản phẩm không hợp lệ.";
                return RedirectToAction("DanhMucSanPham");
            }

            var sanPham = db.SANPHAMs.Find(maSanPhamInt);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem sản phẩm có tồn tại trong chi tiết hóa đơn không
            bool chiTietHoaDonTonTai = db.CTDHs.Any(ct => ct.MA_SP == maSanPhamInt);
            if (chiTietHoaDonTonTai)
            {
                TempData["ErrorMessage"] = "Không thể xóa sản phẩm này vì đã tồn tại trong chi tiết hóa đơn.";
                return RedirectToAction("DanhMucSanPham");
            }

            db.SANPHAMs.Remove(sanPham);
            db.SaveChanges();

            return RedirectToAction("DanhMucSanPham");
        }
        [Route("ChiTietSanPham")]
        public ActionResult ChiTietSanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SANPHAM sanPham = db.SANPHAMs.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            return View(sanPham);
        }
        //Loại món ăn
        [Route("LoaiMonAn")]
        public ActionResult LoaiMonAn()
        {
            var lstLoaiMonAn = db.LOAI_MONAN.ToList();
            return View(lstLoaiMonAn);
        }

        // Thêm loại sản phẩm
        [Route("ThemLoaiSanPham")]
        [HttpGet]
        public ActionResult ThemLoaiSanPham()
        {
            return View();
        }

        [Route("ThemLoaiSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemLoaiSanPham(LOAI_MONAN loaiMonAn)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem mã loại đã tồn tại hay chưa
                var existingLoai = db.LOAI_MONAN.FirstOrDefault(l => l.MALOAI == loaiMonAn.MALOAI);
                if (existingLoai != null)
                {
                    ModelState.AddModelError("Maloai", "Mã loại này đã tồn tại.");
                    return View(loaiMonAn);
                }

                db.LOAI_MONAN.Add(loaiMonAn);
                db.SaveChanges();
                return RedirectToAction("LoaiMonAn");
            }
            return View(loaiMonAn);
        }

        // Sửa loại sản phẩm
        [Route("SuaLoaiSanPham")]
        [HttpGet]
        public ActionResult SuaLoaiSanPham(int? maLoai)
        {
            if (maLoai == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loaiMonAn = db.LOAI_MONAN.Find(maLoai);
            if (loaiMonAn == null)
            {
                return HttpNotFound();
            }
            return View(loaiMonAn);
        }

        [Route("SuaLoaiSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaLoaiSanPham(LOAI_MONAN loaiMonAn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiMonAn).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoaiMonAn");
            }
            return View(loaiMonAn);
        }

        // Xóa loại sản phẩm
        [Route("XoaLoaiSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaLoaiSanPham(int? maLoai)
        {
            if (maLoai == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loaiMonAn = db.LOAI_MONAN.Find(maLoai);
            if (loaiMonAn == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem có sản phẩm nào thuộc loại sản phẩm này không
            var hasSanpham = db.SANPHAMs.Any(sp => sp.MALOAI == maLoai);
            if (hasSanpham)
            {
                TempData["ErrorMessage"] = "Không thể xóa loại sản phẩm này vì có sản phẩm thuộc loại sản phẩm này.";
                return RedirectToAction("LoaiMonAn");
            }

            db.LOAI_MONAN.Remove(loaiMonAn);
            db.SaveChanges();

            return RedirectToAction("LoaiMonAn");
        }
        // Xem chi tiết loại sản phẩm
        [Route("ChiTietLoaiSanPham")]
        public ActionResult ChiTietLoaiSanPham(int? maLoai)
        {
            if (maLoai == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var loaiMonAn = db.LOAI_MONAN.Find(maLoai);
            if (loaiMonAn == null)
            {
                return HttpNotFound();
            }

            return View(loaiMonAn);
        }
        //NHANVIEN
        [Route("DanhMucNhanVien")]
        public ActionResult DanhMucNhanVien()
        {
            var lstNhanvien = db.NHAN_VIEN.ToList();
            return View(lstNhanvien);
        }

        [Route("ThemNhanVienMoi")]
        [HttpGet]
        public ActionResult ThemNhanVienMoi()
        {
            return View();
        }

        [Route("ThemNhanVienMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhanVienMoi(NHAN_VIEN nhanvien)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra sự tồn tại của mã nhân viên
                var existingEmployee = db.NHAN_VIEN.FirstOrDefault(nv => nv.MA_NV == nhanvien.MA_NV);
                if (existingEmployee != null)
                {
                    ModelState.AddModelError("MaNv", "Mã nhân viên này đã tồn tại.");
                    return View(nhanvien);
                }

                // Kiểm tra sự tồn tại của ID_TK trong bảng TAI_KHOAN
                var existingAccount = db.TAI_KHOAN.Find(nhanvien.ID_TK);
                if (existingAccount == null)
                {
                    ModelState.AddModelError("IdTk", "ID_TK này không tồn tại trong bảng TAI_KHOAN.");
                    return View(nhanvien);
                }

                db.NHAN_VIEN.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("DanhMucNhanVien");
            }
            return View(nhanvien);
        }

        [Route("SuaNhanVien")]
        [HttpGet]
        public ActionResult SuaNhanVien(int? maNV)
        {
            if (maNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nhanvien = db.NHAN_VIEN.Find(maNV);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaNhanVien(NHAN_VIEN nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucNhanVien");
            }
            return View(nhanvien);
        }

        [Route("XoaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaNhanVien(int? maNV)
        {
            if (maNV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nhanvien = db.NHAN_VIEN.Find(maNV);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }

            db.NHAN_VIEN.Remove(nhanvien);
            db.SaveChanges();

            return RedirectToAction("DanhMucNhanVien");
        }

        [Route("ChiTietNhanVien")]
        public ActionResult ChiTietNhanVien(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nhanvien = db.NHAN_VIEN.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }

            return View(nhanvien);
        }

        //KHACHHANG
        [Route("DanhMucKhachHang")]
        public ActionResult DanhMucKhachHang()
        {
            var lstKhachHang = db.KHACH_HANG.ToList();
            return View(lstKhachHang);
        }

        [Route("ChiTietKhachHang")]
        public ActionResult ChiTietKhachHang(int? maKh)
        {
            if (maKh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var khachhang = db.KHACH_HANG.Find(maKh);
            if (khachhang == null)
            {
                return HttpNotFound();
            }

            return View(khachhang);
        }
        [Route("DonDatHang")]
        public ActionResult DonDatHang()
        {
            var orders = db.DON_DAT_HANG.Include("KHACH_HANG").ToList();
            return View(orders);
        }
        [Route("ChiTietDatHang")]
        public ActionResult ChiTietDatHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.DON_DAT_HANG
                .Include("KHACH_HANG")
                .Include("CTDHs.SANPHAM")  // Include chi tiết đơn hàng và sản phẩm
                .FirstOrDefault(o => o.MA_DH == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Tính tổng giá trị đơn hàng không bao gồm phí ship
            decimal totalAmountWithoutShipping = order.CTDHs.Sum(item => (item.SOLUONG ?? 0) * (item.SANPHAM.GIAMON ?? 0));
            // Giả định phí ship là 20,000 VND nếu tổng số lượng sản phẩm ít hơn 5, nếu không thì miễn phí ship
            decimal shippingFee = order.CTDHs.Sum(item => item.SOLUONG ?? 0) < 5 ? 20000 : 0;
            decimal totalOrderAmount = totalAmountWithoutShipping + shippingFee;

            ViewBag.TotalAmountWithoutShipping = totalAmountWithoutShipping;
            ViewBag.ShippingFee = shippingFee;
            ViewBag.TotalOrderAmount = totalOrderAmount;

            return View(order);
        }


        [Route("SuaDonDatHang")]
        [HttpGet]
        public ActionResult SuaDonDatHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.DON_DAT_HANG.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [Route("SuaDonDatHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaDonDatHang(DON_DAT_HANG donDatHang)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = db.DON_DAT_HANG.Find(donDatHang.MA_DH);
                if (existingOrder != null)
                {
                    existingOrder.TRANGTHAI_DH = donDatHang.TRANGTHAI_DH;
                    db.SaveChanges();
                    return RedirectToAction("DonDatHang");
                }
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // Xóa đơn đặt hàng
        [Route("XoaDonDatHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaDonDatHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.DON_DAT_HANG.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            db.DON_DAT_HANG.Remove(order);
            db.SaveChanges();

            return RedirectToAction("DonDatHang");
        }
        [Route("DoanhThuNgay")]
        public ActionResult DoanhThuNgay()
        {
            var doanhThuTheoNgay = db.DON_DAT_HANG
        .Where(ddh => ddh.NGAY_GIODAT.HasValue)
        .AsEnumerable()
        .GroupBy(ddh => ddh.NGAY_GIODAT.Value.Date)
        .Select(g => new
        {
            Ngay = g.Key,
            TongDoanhThu = g.Sum(ddh => ddh.CTDHs.Sum(ct => (ct.SOLUONG ?? 0) * ct.SANPHAM.GIAMON) + ddh.ShippingFee)
        })
        .ToList();

            ViewBag.DoanhThuNgay = JsonConvert.SerializeObject(doanhThuTheoNgay);
            return View();
        }

        [Route("DoanhThuThang")]
        public ActionResult DoanhThuThang()
        {
            var doanhThuTheoThang = db.DON_DAT_HANG
         .Where(ddh => ddh.NGAY_GIODAT.HasValue)
         .AsEnumerable()
         .GroupBy(ddh => new { ddh.NGAY_GIODAT.Value.Year, ddh.NGAY_GIODAT.Value.Month })
         .Select(g => new
         {
             Thang = g.Key.Month,
             Nam = g.Key.Year,
             TongDoanhThu = g.Sum(ddh => ddh.CTDHs.Sum(ct => (ct.SOLUONG ?? 0) * ct.SANPHAM.GIAMON) + ddh.ShippingFee)
         })
         .ToList();

            ViewBag.DoanhThuThang = JsonConvert.SerializeObject(doanhThuTheoThang);
            return View();
        }

        [Route("DoanhThuNam")]
        public ActionResult DoanhThuNam()
        {
            var doanhThuTheoNam = db.DON_DAT_HANG
        .Where(ddh => ddh.NGAY_GIODAT.HasValue)
        .AsEnumerable()
        .GroupBy(ddh => ddh.NGAY_GIODAT.Value.Year)
        .Select(g => new
        {
            Nam = g.Key,
            TongDoanhThu = g.Sum(ddh => ddh.CTDHs.Sum(ct => (ct.SOLUONG ?? 0) * ct.SANPHAM.GIAMON) + ddh.ShippingFee)
        })
        .ToList();

            ViewBag.DoanhThuNam = JsonConvert.SerializeObject(doanhThuTheoNam);
            return View();
        }

        [Route("SanPhamBanChay")]
        public ActionResult SanPhamBanChay()
        {
            var sanPhamBanChay = db.CTDHs
        .GroupBy(ct => ct.SANPHAM.TEN_SP)
        .Select(g => new
        {
            SanPham = g.Key,
            SoLuongBan = g.Sum(ct => ct.SOLUONG ?? 0)
        })
        .OrderByDescending(x => x.SoLuongBan)
        .Take(10)
        .ToList();

            ViewBag.SanPhamBanChay = JsonConvert.SerializeObject(sanPhamBanChay);
            return View();
        }
        [Route("GopY")]
        public ActionResult GopY(string ten, string email, int dienthoai, string sanpham, int danhgia, string nhanxet, string ykien)
        {
            if (ModelState.IsValid)
            {
                // Tìm hoặc tạo khách hàng
                KHACH_HANG khachHang = db.KHACH_HANG
                    .FirstOrDefault(kh => kh.EMAIL == email && kh.SDT == dienthoai);

                if (khachHang == null)
                {
                    khachHang = new KHACH_HANG
                    {
                        TEN_KH = ten,
                        EMAIL = email,
                        SDT = dienthoai
                    };
                    db.KHACH_HANG.Add(khachHang);
                    db.SaveChanges(); // Lưu khách hàng mới vào cơ sở dữ liệu
                }

                // Tạo mới góp ý
                GOP_Y gopY = new GOP_Y
                {
                    MA_KH = khachHang.MA_KH,
                    NOIDUNG = ykien
                };

                db.GOP_Y.Add(gopY);
                db.SaveChanges();

                return RedirectToAction("IndexAdmin"); // Hoặc bất kỳ hành động nào sau khi xử lý thành công
            }
            return View(); // Hoặc bất kỳ view nào để xử lý lỗi
        }
    }
}
 



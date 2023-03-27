using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanCCnMT.Controllers
{
    public class HomeController : Controller
    {
        dbNongSanZenoDataContext data = new dbNongSanZenoDataContext();

        private List<tbSanPham> Laysanpham(int count)
        {
            return data.tbSanPhams.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult TrangChu()
        {
            //Lay top  san pham 
            var sanpham = Laysanpham(42);
            sanpham = data.tbSanPhams.OrderByDescending(a => a.NgayCapNhat).Take(42).Where(w => w.TenSP).ToList();
            return View(sanpham);
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult AnToanThucPham()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(FormCollection collection)
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "LoginUser");
            }
            else
            {
                tbPhanHoiKH ht = new tbPhanHoiKH();
                tbKhachHang kh = (tbKhachHang)Session["Taikhoan"];
                ht.MaKH = kh.MaKH;
                ht.Email = kh.Email;
                string lydo = collection["LyDo"];
                ht.LyDo = lydo;
                if (lydo == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    data.tbPhanHoiKHs.InsertOnSubmit(ht);
                    data.SubmitChanges();
                    return RedirectToAction("LienHe", "Home");
                }
            }
        }
    }
}
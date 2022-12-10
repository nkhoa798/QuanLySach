using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySach.Models;
using PagedList;

namespace QuanLySach.Controllers
{
    public class LoaiSPController : Controller
    {
        // GET: LoaiSP
        QuanLySachEntities db = new QuanLySachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XemLoaiSP(int? ma, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            
            var LoaiSP = db.SanPhams.Where(x => x.MaLoai == ma).OrderBy(s => s.Gia).ToPagedList(pageNumber, pageSize);
            if (LoaiSP.Count() >  0)
            {
                ViewBag.Ma = ma;
                ViewBag.TheLoai = db.SanPhams.Where(x => x.MaLoai == ma).FirstOrDefault().LoaiSP.Ten;
                ViewBag.count = db.SanPhams.Where(x => x.MaLoai == ma).ToList().Count();
            }
            else
            {
                LoaiSP = db.SanPhams.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize);
            }
            ViewBag.LoaiSP = db.LoaiSPs.ToList();
            return View(LoaiSP);
        }

        
        public ActionResult SapXepSanPham(int? ma, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            PagedList.IPagedList<SanPham> LoaiSP;
            switch (ma)
            {
                case 1: // A-z
                    LoaiSP = db.SanPhams.OrderBy(s => s.Ten).ToPagedList(pageNumber, pageSize);
                    break;
                case 2: //Z-a
                    LoaiSP = db.SanPhams.OrderByDescending(s => s.Ten).ToPagedList(pageNumber, pageSize);
                    break;
                case 3: //thap-cao
                    LoaiSP = db.SanPhams.OrderBy(s => s.Gia).ToPagedList(pageNumber, pageSize);
                    break;
                case 4: // cao-thap
                    LoaiSP = db.SanPhams.OrderByDescending(s => s.Gia).ToPagedList(pageNumber, pageSize);
                    break;
                default:
                    LoaiSP = db.SanPhams.OrderBy(s => s.Ten).ToPagedList(pageNumber, pageSize);
                    break;
            }
            if (LoaiSP.Count() > 0)
            {
                ViewBag.Ma = ma;
                ViewBag.TheLoai = db.SanPhams.FirstOrDefault().LoaiSP.Ten;
                ViewBag.count = db.SanPhams.Where(x => x.MaLoai == ma).ToList().Count();
            }
            else
            {
                LoaiSP = db.SanPhams.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize);
            }
            ViewBag.LoaiSP = db.LoaiSPs.ToList();
            return View(LoaiSP);
        }
    }
}
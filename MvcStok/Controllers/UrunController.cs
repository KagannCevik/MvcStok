using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Mvc50Ders_ProjeEntities db = new Mvc50Ders_ProjeEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem>degerler=(from x in db.TBL_KATEGORILER.ToList()
                                          select new SelectListItem
                                          {
                                              Text=x.KATEGORIAD,
                                              Value=x.KATEGORIID.ToString()
                                          }).ToList();
            ViewBag.dgr = degerler;    
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBL_KATEGORILER = ktg;

            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("INDEX");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from x in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.KATEGORIAD,
                                                 Value = x.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.URUNMARKA = p.URUNMARKA;
            urun.URUNSTOK = p.URUNSTOK;
            urun.URUNFIYAT= p.URUNFIYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI= ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var query = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                query = query.Where(y => y.UrunAd.Contains(p));
            }
            return View(query.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);

        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urun = c.Uruns.Find(p.UrunID);
            urun.AlisFiyat = p.AlisFiyat;
            urun.Durum = p.Durum;
            urun.SatisFiyat = p.SatisFiyat;
            urun.Stok = p.Stok;
            urun.kategoriid = p.kategoriid;
            urun.Marka = p.Marka;
            urun.UrunAd = p.UrunAd;
            urun.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var query = c.Uruns.ToList();
            return View(query);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> query4 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.vb = query4;
            var sorgu = c.Uruns.Find(id);
            ViewBag.vb2 = sorgu.UrunID;
            ViewBag.vb3 = sorgu.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p); ;
            c.SaveChanges();

            return RedirectToAction("Index","Satis");
        }
    }
}
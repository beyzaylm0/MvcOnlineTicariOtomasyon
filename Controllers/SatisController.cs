using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index()
        {
            var query = c.SatisHarekets.ToList();
            return View(query);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> query2 = (from x in c.Uruns.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.UrunAd,
                                              Value = x.UrunID.ToString()
                                          }).ToList();
            List<SelectListItem> query3 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> query4 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
           
            ViewBag.vb2 = query2;
            ViewBag.vb3 = query3;
            ViewBag.vb4 = query4;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);;
            c.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> query2 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            List<SelectListItem> query3 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> query4 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();

            ViewBag.vb2 = query2;
            ViewBag.vb3 = query3;
            ViewBag.vb4 = query4;
            var query = c.SatisHarekets.Find(id);
            return View("SatisGetir", query);

        }
        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var query = c.SatisHarekets.Find(p.SatisID);
            query.urunid = p.urunid;
            query.cariid = p.cariid;
            query.personelid = p.personelid;
            query.Adet = p.Adet;
            query.ToplamTutar = p.ToplamTutar;
            query.Tarih = p.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }
    }
}
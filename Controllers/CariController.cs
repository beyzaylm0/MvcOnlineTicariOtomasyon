using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()

        {
            var query = c.Carilers.Where(x=>x.Durum==true).ToList();

            return View(query);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var query = c.Carilers.Find(id);
            return View("CariGetir", query);
        }
        public ActionResult CariGuncelle(Cariler p)
        { if(!ModelState.IsValid)
            {
                return View("CariGetir");
            }

            var query2 = c.Carilers.Find(p.CariID);
            query2.CariAd = p.CariAd;
            query2.CariSoyad = p.CariSoyad;
            query2.CariSehir = p.CariSehir;
            query2.CariMail = p.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var query3 = c.SatisHarekets.Where(x => x.cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View("MusteriSatis", query3);
        }
    }
}
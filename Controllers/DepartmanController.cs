using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
       
        public ActionResult Index()
        {
            var deger = c.Departmans.Where(x=>x.Durum==true).ToList();
            return View(deger);
        }
        [Authorize(Roles = "A")]
        [HttpGet]
       
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult DepartmanEkle(Departman p)
        {
            c.Departmans.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var deger = c.Departmans.Find(id);
            deger.Durum = false;
            c.SaveChanges();
         return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var deger2 = c.Departmans.Find(id);
            return View("DepartmanGetir", deger2);

        }
        public ActionResult DepartmanGuncelle(Departman p)
        {
            var dept = c.Departmans.Find(p.DepartmanID);
            dept.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = c.Departmans.Where(x => x.DepartmanID == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;
            return View(degerler);
            
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            
            var per = c.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dp = per;
            var degerler = c.SatisHarekets.Where(x => x.personelid == id).ToList();
            return View(degerler);
        }
    }
}
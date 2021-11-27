using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];

            var query = c.Mesajlars.Where(x => x.Alıcı == mail).ToList();
            ViewBag.vb = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.m1 = mailid;
            var toplamsatis = c.SatisHarekets.Where(x => x.cariid == mailid).Count();
            ViewBag.m2 = toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.m3 = toplamtutar;
            var toplamurun = c.SatisHarekets.Where(x => x.cariid == mailid).Sum(y => y.Adet);
            ViewBag.m4 = toplamurun;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;

            return View(query);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var query = c.SatisHarekets.Where(x => x.cariid == id).ToList();
            return View(query);
        }
        [Authorize]
        public ActionResult GelenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesaj = c.Mesajlars.Where(x=>x.Alıcı==mail).OrderByDescending(x=>x.MesajId).ToList();
            var gelensayi = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelensayi;
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            return View(mesaj);
            
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesaj = c.Mesajlars.Where(x => x.Gonderici == mail).ToList();
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            var gelensayi = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelensayi;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar p)
        {
            var mail = (string)Session["CariMail"];
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.Gonderici = mail;
            c.Mesajlars.Add(p);

            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult GidenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesaj = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x=>x.MesajId).ToList();
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            var gelensayi = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelensayi;
            return View(mesaj);

        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var query = c.Mesajlars.Where(x => x.MesajId == id).ToList();
            var mail = (string)Session["CariMail"];
            var gelensayi = c.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelensayi;
            var gidensayi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayi;
            return View(query);
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var query = from x in c.KargoDetays select x;
            
                query = query.Where(y => y.TakipKodu.Contains(p));
            
            return View(query.ToList());
     
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {

            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(degerler);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();

            var caribul = c.Carilers.Find(id);
         
            return PartialView("Partial1",caribul);
        }
        public PartialViewResult Partial2()
        {
            var duyuru = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(duyuru);
        }
        public ActionResult CariBilgiGuncelle(Cariler k)
        {
            var query = c.Carilers.Find(k.CariID);
            query.CariAd = k.CariAd;
            query.CariSoyad = k.CariSoyad;
            query.CariSifre = k.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");



        }
    }
}
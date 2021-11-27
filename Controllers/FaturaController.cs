using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();  
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar p)
        {
            c.Faturalars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var query = c.Faturalars.Find(id);
            return View("FaturaGetir", query);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var query = c.Faturalars.Find(f.FaturaID);
            query.FaruraSeriNo = f.FaruraSeriNo;
            query.FaturaSıraNo = f.FaturaSıraNo;
            query.VergiDairesi = f.VergiDairesi;
            query.Tarih = f.Tarih;
            query.Saat = f.Saat;
            query.TeslimEden = f.TeslimEden;
            query.TeslimAlan = f.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");

              
        }
        public ActionResult FaturaDetay(int id)
        {

            var query = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(query);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem k)
        {
            c.FaturaKalems.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            Class3 cs = new Class3();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaruraSeriNo,string FaturaSıraNo,string VergiDairesi,DateTime Tarih,string Saat,string TeslimEden,string TeslimAlan,string Toplam,
            FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaruraSeriNo = FaruraSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimAlan = TeslimAlan;
            f.TeslimEden = TeslimEden;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Açıklama = x.Açıklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemID;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);

        }
    }

}
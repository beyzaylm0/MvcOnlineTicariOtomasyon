using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class İstatistikController : Controller
    {
        // GET: İstatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var query = c.Carilers.Count().ToString();
            ViewBag.d1 = query;
            var query2 = c.Uruns.Count().ToString();
            ViewBag.d2 = query2; 
            var query3 = c.Personels.Count().ToString();
            ViewBag.d3 = query3;
            var query4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = query4;
            var query5 = c.Uruns.Sum(x=>x.Stok).ToString();
            ViewBag.d5 = query5;
            var query6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = query6;
            var query7 = c.Uruns.Count(x=>x.Stok <=20).ToString();
            ViewBag.d7 = query7;
            var query8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = query8;
            var query9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = query9;
            var query10 = c.Uruns.Count(x=>x.UrunAd=="Buzdolabı").ToString();
            ViewBag.d10 = query10;
            var query11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = query11;
            var query12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = query12;
            var query13 = c.Uruns.Where(u => u.UrunID == (c.SatisHarekets.GroupBy(x => x.urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).
                Select(y => y.UrunAd).FirstOrDefault();
            ViewBag.d13 = query13;
            var query14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = query14;
            DateTime bugun = DateTime.Today;
            var query15 = c.SatisHarekets.Count(x =>x.Tarih==bugun).ToString();
            ViewBag.d15 = query15;
            var query16 = c.SatisHarekets.Where(x=>x.Tarih==bugun).Sum(y=>(decimal?)y.ToplamTutar).ToString();
            ViewBag.d16 = query16;
            return View();



        }
        public ActionResult KolayTablolar()
        {
            var query = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(query.ToList());
        }
        public PartialViewResult Partial1()
        {
            var query2 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(query2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var query3 = c.Carilers.ToList();
            return PartialView(query3);
        }
        public PartialViewResult Partial3()
        { var query4 = c.Uruns.ToList();
            return PartialView(query4);
        }
        public PartialViewResult Partial4()
        {
            var query5 = from x in c.Uruns
                         group x by x.Marka into g
                         select new GrupSinif3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(query5.ToList());
        }
    }
}
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori-Ürün Stok Sayısı").AddLegend("Stok").AddSeries
                ("Degerler", xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuc = c.Uruns.ToList();
            sonuc.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuc.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(width: 500, height: 500)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

        }
        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(urunlistesi(), JsonRequestBehavior.AllowGet);
        }
        public List<sinif1> urunlistesi()
        {
            List<sinif1> snf = new List<sinif1>();
            snf.Add(new sinif1()
            {
                urunad = "Bilgisayar",
                stok = 123

            });
            
            snf.Add(new sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 122

            }); 
            snf.Add(new sinif1()
            {
                urunad = "Mobilya",
                stok = 125

            });
            snf.Add(new sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 120

            });
            snf.Add(new sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 127

            });
            return snf;
        }
        public ActionResult Index5()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult2()
        {
            return Json(urunlistesi2(), JsonRequestBehavior.AllowGet);
        }
        public List<sınıf2> urunlistesi2()
        {
            List<sınıf2> snf = new List<sınıf2>();
            using(var context=new Context())
            {
                snf = c.Uruns.Select(x => new sınıf2
                {
                    urun = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }
            return snf;
        }
    }
}
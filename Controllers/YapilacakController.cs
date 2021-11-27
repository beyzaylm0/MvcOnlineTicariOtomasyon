using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context c = new Context();
        public ActionResult Index()
        {
            var query = c.Carilers.Count().ToString();
            ViewBag.d1 = query;
            var query2 = c.Uruns.Count().ToString();
            ViewBag.d2 = query2;
            var query3 = c.Kategoris.Count().ToString();
            ViewBag.d3 = query3;
            var query4 = (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = query4;

            var yapilacak = c.Yapilacaks.ToList();
            return View(yapilacak);
        }
    }
}
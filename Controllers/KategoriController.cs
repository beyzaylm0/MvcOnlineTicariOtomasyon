﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Controllers;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Models.Sınıflar
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int page=1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(page, 4);

            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir", kategori);

        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class2 cs = new Class2();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunID", "UrunAd");
            return View(cs);

        }
        public JsonResult UrunGetir(int p)
        {
            var urunliste = (from x in c.Uruns
                             join y in c.Kategoris
                             on x.Kategori.KategoriID equals y.KategoriID
                             where x.Kategori.KategoriID == p
                             select new
                             {
                                 Text = x.UrunAd,
                                 Value = x.UrunID.ToString()
                             }).ToList();
            return Json(urunliste, JsonRequestBehavior.AllowGet);
        }
    }
}
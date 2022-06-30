using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using newsProject.Models;

namespace newsProject.Controllers
{
    public class NewsController : Controller
    {
        BlogContext db = new BlogContext();
        // GET: News
        public ActionResult add()
        {
            if (Session["userid"] == null)
            {
                RedirectToAction("login","User");
            }
            SelectList st = new SelectList(db.catalogs.ToList(),"id","name");
            ViewBag.cat = st;
            return View();
        }
        [HttpPost]
        public ActionResult add(news n,HttpPostedFileBase img)
        {
            //upload photo
            img.SaveAs(Server.MapPath($"~/attach/newsphoto/{img.FileName}"));
            //edit object
            //add photo
            //add userid
            //add date

            n.photo = $"/attach/newsphoto/{img.FileName}";
            n.user_id = (int)Session["userid"];
            n.date = DateTime.Now;
            db.news.Add(n);
            db.SaveChanges();
            return RedirectToAction("mynews");

        }
        public ActionResult mynews()
        {
            int userid = (int)Session["userid"];
            List<news> ns = db.news.Where(n => n.user_id == userid).ToList();
            return View(ns);
        }
        public ActionResult delete(int id)
        {
            news s = db.news.Where(n => n.id == id).FirstOrDefault();
            db.news.Remove(s);
            db.SaveChanges();
            return RedirectToAction("mynews");
        }

        public ActionResult edit(int id)
        {
            SelectList cats = new SelectList(db.catalogs.ToList(), "ID", "name");
            ViewBag.cats = cats;
            news s = db.news.Where(n => n.id == id).FirstOrDefault();
            return View(s);
        }
        public ActionResult update(news nn)
        {
            news ns = db.news.Where(n => n.id == nn.id).First();
            ns.title = nn.title;
            ns.bref = nn.bref;
            ns.desc = nn.desc;
            ns.photo = nn.photo;
            ns.catalog = nn.catalog;
            ns.cat_id = nn.cat_id;
            ns.user_id = nn.user_id;
            db.SaveChanges();
            //db.Entry(nn).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();

            return RedirectToAction("mynews");
        }

        public ActionResult details(int id)
        {
            news s = db.news.Where(n => n.id == id).FirstOrDefault();
            return View(s);
        }

        public ActionResult allnews()
        {
            return View(db.news.ToList()) ;
        }
    }
}
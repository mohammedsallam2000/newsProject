using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using newsProject.Models;

namespace newsProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        BlogContext db = new BlogContext();
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(user s, HttpPostedFileBase img)
        {
            user d = db.users.Where(n => n.email == s.email).FirstOrDefault();
            if (d != null)
            {
                ViewBag.status = "Email already exist !";
                return View();
            }
            //upload photo
            img.SaveAs(Server.MapPath("~/attach/" + img.FileName));
            //modefiy user object (store photo path)
            s.photo = img.FileName;
            //save user
            
            if (ModelState.IsValid)
            {
                db.users.Add(s);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View();
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(user s)
        {
            user d = db.users.Where(n => n.username == s.username && n.password == s.password).FirstOrDefault();
            if (d != null)
            {
                //login
                Session.Add("userid", d.Id);
                return RedirectToAction("profile"); 
            }
            else
            {
                //not login
                ViewBag.status = "incorrect username or password  !";

                return View();
            }
            return View();
        }
        public ActionResult profile()
        {
            if (Session["userid"] == null)
            {
                RedirectToAction("login");
            }
            int id = (int)Session["userid"];
            user d = db.users.Where(n => n.Id == id).FirstOrDefault();
            return View(d);
        }

        public ActionResult delete(int id)
        {
            user s = db.users.Where(n => n.Id == id).FirstOrDefault();
            db.users.Remove(s);
            db.SaveChanges();
            return RedirectToAction("login");
        }

        public ActionResult edit(int id)
        {
            user s = db.users.Where(n => n.Id == id).FirstOrDefault();
            return View(s);
        }

        public ActionResult update(user nn)
        {

            user nu = db.users.Where(n => n.Id == nn.Id).First();
            nu.username = nn.username;
            nu.email = nn.email;
            nu.password = nn.password;
            nu.age = nn.age;
            nu.photo = nn.photo;
            nu.address = nn.address;
            db.SaveChanges();
            //db.Entry(nn).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("profile");
        }
        public ActionResult editPhoto(int id)
        {
            user s = db.users.Where(n => n.Id == id).FirstOrDefault();
            return View(s);
        }
        public ActionResult updatePhoto(user nn, HttpPostedFileBase img)
        {
            //upload photo
            img.SaveAs(Server.MapPath("~/attach/" + img.FileName));
            //modefiy user object (store photo path)
            nn.photo = img.FileName;
            user nu = db.users.Where(n => n.Id == nn.Id).First();
            nu.photo = nn.photo;
            db.SaveChanges();
            //db.Entry(nn).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("profile");
        }
        public ActionResult logout()
        {
            Session["userid"] = null;
            return RedirectToAction("login");
        }
    } 
}
using LedtinEShop.Filters;
using LedtinEShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LedtinEShop.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            var cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                ViewBag.Id = cookie.Values["Id"];
                ViewBag.Password = cookie.Values["Password"];
                ViewBag.Remember = true;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(String Id, String Password, bool Remember)
        {
            var user = db.Customers.Find(Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username !");
            }
            else if (user.Password != Password)
            {
                ModelState.AddModelError("", "Invalid password !");
            }
            else if (user.Activated == false)
            {
                ModelState.AddModelError("", "Account is not activated !");
            }
            else
            {
                ModelState.AddModelError("", "Login successfully !");
                Session["User"] = user;
                Session["Id"] = Id;
                // Xu ly Remember me
                var cookie = new HttpCookie("User");
                if (Remember == true)
                {
                    cookie.Values["Id"] = Id;
                    cookie.Values["Password"] = Password;
                    cookie.Expires = DateTime.Now.AddMonths(1);
                }
                else
                {
                    cookie.Expires = DateTime.Now;
                }
                Response.Cookies.Add(cookie);

                //
                var RequestUrl = Session["RequestUrl"] as String;
                if (RequestUrl != null)
                {
                    return Redirect(RequestUrl);
                }
            }
            return View();
        }

        [Authenticate]
        public ActionResult Logoff()
        {
            Session.Remove("User");
            Session.Remove("Id");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            var model = new Customer();
            model.Activated = false;
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(Customer model)
        {
            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {

                    model.Photo = DateTime.Now.Ticks + "-" + f.FileName;
                    var path = "~/images/customers/" + model.Photo;
                    f.SaveAs(Server.MapPath(path));
                }
                else
                {
                    model.Photo = "User.jpg";
                }
                // dawng ky
                db.Customers.Add(model);
                db.SaveChanges();
                ModelState.AddModelError("", "Register successfully !");

                // Send welcome mail
                var Uri = Request.Url.AbsoluteUri.Replace("Register", "Activate/" + model.Id.ToBase64());
                String body = "Please click to activate your account. <a href='" + Uri + "'>Activate</a>";
                XMail.Send(model.Email, "Welcome mail", body);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        public ActionResult Activate(String Id)
        {
            var user = db.Customers.Find(Id.FromBase64());
            user.Activated = true;
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(String Id, String Email)
        {
            var user = db.Customers.Find(Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username");
            }
            else if (Email != user.Email)
            {
                ModelState.AddModelError("", "Invalid email");
            }
            else
            {
                ModelState.AddModelError("", "Your password was sent to your inbox");
                XMail.Send(Email, "Forgot password", "Password:" + user.Password);
            }
            return View();
        }

        [Authenticate]
        public ActionResult Change()
        {
            return View();
        }

        [Authenticate]
        [HttpPost]
        public ActionResult Change(String Id, String CurrentPassword, String NewPassword, String ConfirmNewPassword)
        {
            if (NewPassword != ConfirmNewPassword)
            {
                ModelState.AddModelError("", "Confirm new password is not match");
            }
            else
            {
                var user = db.Customers.Find(Id);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username");
                }
                else if (user.Password != CurrentPassword)
                {
                    ModelState.AddModelError("", "Invalid current password");
                }
                else
                {
                    user.Password = NewPassword;
                    db.SaveChanges();
                    ModelState.AddModelError("", "Change password successfully");
                }
            }


            return View();
        }

        [Authenticate]
        public ActionResult Edit()
        {
            var model = Session["User"] as Customer;
            return View(model);
        }

        [Authenticate]
        [HttpPost]
        public ActionResult Edit(Customer model)
        {
            // upload hinh
            var f = Request.Files["UpPhoto"];
            if (f.ContentLength > 0)
            {
                if (model.Photo != "User.jpg")
                {
                    var path = "~/images/customers/" + model.Photo;
                    System.IO.File.Delete(Server.MapPath(path));
                }
                model.Photo = DateTime.Now.Ticks + "-" + f.FileName;
                var newPath = "~/images/customers/" + model.Photo;
                f.SaveAs(Server.MapPath(newPath));
            }
            // cap nhat
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                Session["User"] = model;

                ModelState.AddModelError("", "Updated successfully !!!");
            }
            catch
            {
                ModelState.AddModelError("", "Error !!! Please try again !!!");
            }
            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LedtinEShop.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            //Nếu ko có cookie thì chuyển về trang Admin/Admin/Index
            var cookie = Request.Cookies["Admin"];
            if (cookie == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            //Nếu có cookie thì chuyển về trang Admin/Admin/Index
            var cookie = Request.Cookies["Admin"];
            if (cookie != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(String Id, String Password, bool Remember)
        {
            var user = db.Admins.Find(Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Id !");
            }
            else if (user.Password != Password)
            {
                ModelState.AddModelError("", "Invalid password !");
            }
            else
            {
                ModelState.AddModelError("", "Login successfully !");
                Session["Admin"] = user;
                // Xu ly Remember me
                var cookie = new HttpCookie("Admin");
                if (Remember == true)
                {
                    cookie.Values["AdminId"] = Id;
                    cookie.Values["AdminPassword"] = Password;
                    cookie.Expires = DateTime.Now.AddMonths(1);
                }
                else
                {
                    cookie.Expires = DateTime.Now;
                }
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Remove("Admin");
            //Xóa cookie
            var cookie = Request.Cookies["Admin"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login", "Admin");
        }
    }
}
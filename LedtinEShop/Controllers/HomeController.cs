using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace LedtinEShop.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? page)
        {
            Session["BackUrl"] = "/Home/Index";
            var model = db.Products
                        .Where(p => p.Available)
                        .OrderByDescending(p => p.OrderDetails.Sum(d => d.Quantity))
                        .Take(18)
                        .ToList();
            int pagesize = 6;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult GetPaging()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _Category()
        {
            var model = db.Categories.OrderBy(c => c.Name).ToList();
            return PartialView(model);
        }
        public ActionResult _Supplier()
        {
            var model = db.Suppliers.OrderBy(s => s.Name).ToList();
            return PartialView(model);
        }
    }
}
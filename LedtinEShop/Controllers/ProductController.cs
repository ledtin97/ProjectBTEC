using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LedtinEShop.Models;
using PagedList;
using System.Data.Entity;

namespace LedtinEShop.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetPaging()
        {
            return View();
        }


        public ActionResult ListByCategory(int Id, int? page)
        {
            Session["BackUrl"] = "/Product/ListByCategory/" + Id;
            var model = db.Products.Where(p => p.CategoryId == Id && p.Available && p.Quantity > 0).ToList();
            //return View("List", model);
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult ListBySupplier(String Id, int? page)
        {
            Session["BackUrl"] = "/Product/ListBySupplier/" + Id;
            var model = db.Products.Where(p => p.SupplierId == Id && p.Available && p.Quantity > 0).ToList();
            //return View("List", model);
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult ListBySpecial(String Id, int? page)
        {
            Session["BackUrl"] = "/Product/ListBySpecial/" + Id;
            List<Product> model;
            switch (Id)
            {
                case "BEST":
                    model = db.Products.
                        Where(p => p.Available && p.Quantity > 0)
                        .OrderByDescending(p => p.OrderDetails.Sum(d => d.Quantity))
                        .Take(15)
                        .ToList();
                    break;
                case "LATEST":
                    model = db.Products.
                        Where(p => p.Latest && p.Available && p.Quantity > 0).
                        ToList();
                    break;
                case "SPECIAL":
                    model = db.Products
                        .Where(p => p.Special && p.Available && p.Quantity > 0)
                        .ToList();
                    break;
                case "VIEWS":
                    model = db.Products
                        .Where(p => p.Views > 0 && p.Available && p.Quantity > 0)
                        .OrderByDescending(p => p.Views)
                        .Take(12).ToList();
                    break;
                case "PROMO":
                    model = db.Products
                        .Where(p => p.Discount > 0 && p.Available && p.Quantity > 0)
                        .OrderByDescending(p => p.Discount)
                        .ToList();
                    break;
                default:
                    model = db.Products.ToList();
                    break;
            }
            //return View("List", model);
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult Search(String Keywords, int? page)
        {
            Session["BackUrl"] = "/Product/Search/" + Keywords;
            var model = db.Products
                .Where(p => (p.Name.Contains(Keywords) ||
                    p.Category.Name.Contains(Keywords) ||
                    p.Supplier.Name.Contains(Keywords))
                    && p.Available && p.Quantity > 0)
                .ToList();
            //return View("List", model);
            int pagesize = 9;
            int pagenumber = (page ?? 1);
            return View(model.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult Detail(int Id)
        {
            //tăng view
            Product product = db.Products.Find(Id);
            product.Views += 1;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();


            //Ghi nhớ sản phẩm đã xem
            var cookie = Request.Cookies["Views"];
            if (cookie == null)
            {
                cookie = new HttpCookie("Views");
            }
            cookie.Values[Id.ToString()] = Id.ToString();
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);

            var Ids = cookie.Values.AllKeys
                .Select(k => int.Parse(k))
                .ToList();
            ViewBag.Views = db.Products
                .Where(p => Ids.Contains(p.Id))
                .ToList();

            var model = db.Products.Find(Id);
            return View(model);
        }
    }
}
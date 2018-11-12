using LedtinEShop.Filters;
using LedtinEShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LedtinEShop.Controllers
{
    [Authenticate]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int Id)
        {
            var model = db.Orders.Find(Id);
            return View(model);
        }
        public ActionResult Checkout()
        {
            var user = Session["User"] as Customer;

            var model = new Order();
            model.OrderDate = DateTime.Now;
            model.Amount = ShoppingCart.Cart.Amount;
            model.CustomerId = user.Id;
            model.Receiver = user.Fullname;
            model.RequireDate = DateTime.Now.AddDays(2);

            return View(model);
        }
        [HttpPost]
        public ActionResult Checkout(Order model)
        {
            var user = Session["User"] as Customer;
            try
            {
                db.Orders.Add(model);
                foreach (var p in ShoppingCart.Cart.Items)
                {
                    var detail = new OrderDetail
                    {
                        Order = model,
                        ProductId = p.Id,
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice,
                        Discount = p.Discount
                    };
                    db.OrderDetails.Add(detail);
                }
                db.SaveChanges();

                ModelState.AddModelError("", "Order successfully !!!");
                ShoppingCart.Cart.Clear();
                return RedirectToAction("Detail", new { Id = model.Id });
            }
            catch
            {
                ModelState.AddModelError("", "Order failed !!!");
            }

            return View(model);
        }

        public ActionResult List()
        {

            var user = Session["User"] as Customer;
            var model = db.Orders
                .Where(o => o.CustomerId == user.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            return View(model);
        }
        public ActionResult Items()
        {
            var user = Session["User"] as Customer;
            var model = db.OrderDetails
                .Where(d => d.Order.CustomerId == user.Id)
                .GroupBy(d => d.Product)
                .Select(g => new { Product = g.Key, Count = g.Count() })
                .OrderByDescending(gg => gg.Count)
                .Select(p => p.Product)
                .ToList();
            return View("../Product/List", model);
        }
    }
}
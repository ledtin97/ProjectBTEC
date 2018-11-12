using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LedtinEShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int Id)
        {
            ShoppingCart.Cart.Add(Id);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Amount = ShoppingCart.Cart.Amount
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(int Id)
        {
            ShoppingCart.Cart.Remove(Id);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Amount = ShoppingCart.Cart.Amount
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(int Id, int newqty)
        {
            ShoppingCart.Cart.Update(Id, newqty);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Amount = ShoppingCart.Cart.Amount
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clear()
        {
            ShoppingCart.Cart.Clear();
            return View("Index");
        }
    }
}
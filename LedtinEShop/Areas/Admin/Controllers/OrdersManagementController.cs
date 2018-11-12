using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LedtinEShop.Models;

namespace LedtinEShop.Areas.Admin.Controllers
{
    public class OrdersManagementController : AdminBaseController
    {

        // GET: Admin/OrdersManagement
        public ActionResult Index()
        {
            Session["OrderBackUrl"] = "/Admin/OrdersManagement/Index";
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Admin/OrdersManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.a = "";

            if (order.Description.Length >= 29)
            {
                ViewBag.a = order.Description.Substring(order.Description.Length - 29);
            }
            return View(order);
        }

        public ActionResult Delivered (int? id)
        {
            try
            {
                Order order = db.Orders.Find(id);
                order.Description = order.Description + "\n-DeliveredByLedtinOnlineShop-";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                

                ModelState.AddModelError("", "Updated successfully !!!");
                
            }
            catch
            {
                ModelState.AddModelError("", "Error !!! Please try again !!!");
            }
            return RedirectToAction("Index");
        }

        public ActionResult CancelDelivered(int? id)
        {
            try
            {
                Order order = db.Orders.Find(id);
                order.Description = order.Description.Substring(0, order.Description.Length - 30);
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                ModelState.AddModelError("", "Updated successfully !!!");
            }
            catch
            {
                ModelState.AddModelError("", "Error !!! Please try again !!!");
            }
            return RedirectToAction("Index");
        }

        public ActionResult OrdersDelivered()
        {
            Session["OrderBackUrl"] = "/Admin/OrdersManagement/OrdersDelivered";
            var orders = db.Orders.Include(o => o.Customer).Where(o => o.Description.Length >= 29
                && o.Description.Substring(o.Description.Length - 29).Equals("-DeliveredByLedtinOnlineShop-"));
            return View(orders.ToList());
        }

        public ActionResult CancelDelivered2(int id)
        {
            Order order = db.Orders.Find(id);
            order.Description = order.Description.Substring(0, order.Description.Length - 30);
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return null;
        }

        public ActionResult OrdersNotDelivered()
        {
            Session["OrderBackUrl"] = "/Admin/OrdersManagement/OrdersNotDelivered";
            var orders = db.Orders.Include(o => o.Customer).Where(o => o.Description.Length < 29
                || !o.Description.Substring(o.Description.Length - 29).Equals("-DeliveredByLedtinOnlineShop-"));
            return View(orders.ToList());
        }

        public ActionResult Delivered2(int id)
        {
       
                Order order = db.Orders.Find(id);
                order.Description = order.Description + "\n-DeliveredByLedtinOnlineShop-";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return null;
        }
    }
}

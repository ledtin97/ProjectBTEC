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
    public class CustomersManagementController : AdminBaseController
    {
        // GET: Admin/CustomersManagement
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Admin/CustomersManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/CustomersManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CustomersManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Password,Fullname,Email,Photo,Activated")] Customer customer)
        {
            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {
                    customer.Photo = DateTime.Now.Ticks + "-" + f.FileName;
                    var path = "~/images/customers/" + customer.Photo;
                    f.SaveAs(Server.MapPath(path));
                }
                else
                {
                    customer.Photo = "User.jpg";
                }

                //tạo mới
                if (customer.Id == null)
                {
                    ModelState.AddModelError("", "You must input Id !!!");
                }
                if (customer.Id != null)
                {
                    Customer c = db.Customers.Find(customer.Id);
                    if (c != null)
                    {
                        ModelState.AddModelError("", "This Id is available, please input another !!!");
                    }
                }
                if (customer.Password == null)
                {
                    ModelState.AddModelError("", "You must input password !!!");
                }
                if (customer.Fullname == null)
                {
                    ModelState.AddModelError("", "You must input name !!!");
                }
                if (customer.Email == null)
                {
                    ModelState.AddModelError("", "You must input email !!!");
                }
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            

            return View(customer);
        }

        // GET: Admin/CustomersManagement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/CustomersManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Password,Fullname,Email,Photo,Activated")] Customer customer)
        {
            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {
                    if (customer.Photo != "User.jpg")
                    {
                        var path = "~/images/customers/" + customer.Photo;
                        System.IO.File.Delete(Server.MapPath(path));
                    }
                    customer.Photo = DateTime.Now.Ticks + "-" + f.FileName;
                    var newPath = "~/images/customers/" + customer.Photo;
                    f.SaveAs(Server.MapPath(newPath));
                }
                //cập nhập
                if (customer.Password == null)
                {
                    ModelState.AddModelError("", "You must input password !!!");
                }
                if (customer.Fullname == null)
                {
                    ModelState.AddModelError("", "You must input name !!!");
                }
                if (customer.Email == null)
                {
                    ModelState.AddModelError("", "You must input email !!!");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            
            return View(customer);
        }

        // GET: Admin/CustomersManagement/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/CustomersManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

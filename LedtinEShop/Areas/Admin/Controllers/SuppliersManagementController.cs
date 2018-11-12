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
    public class SuppliersManagementController : AdminBaseController
    {

        // GET: Admin/SuppliersManagement
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        // GET: Admin/SuppliersManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/SuppliersManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SuppliersManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Logo,Email,Phone")] Supplier supplier)
        {
            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {
                    //đổi image name = mã nhà cung cấp viết thường + phần mở rộng của image
                    supplier.Logo = Convert.ToString(supplier.Name.ToLower()) + System.IO.Path.GetExtension(f.FileName);
                    var path = "~/images/suppliers/" + supplier.Logo;
                    f.SaveAs(Server.MapPath(path));
                }
                else
                {
                    supplier.Logo = "logo.png";
                }

                //tạo mới
                if (supplier.Id == null)
                {
                    ModelState.AddModelError("", "You must input Id !!!");
                }
                if (supplier.Id != null)
                {
                    Supplier s = db.Suppliers.Find(supplier.Id);
                    if (s != null)
                    {
                        ModelState.AddModelError("", "This Id is available, please input another !!!");
                    }
                }
                if (supplier.Name == null)
                {
                    ModelState.AddModelError("", "You must input name !!!");
                }
                if (supplier.Email == null)
                {
                    ModelState.AddModelError("", "You must input email !!!");
                }
                if (supplier.Phone == null)
                {
                    ModelState.AddModelError("", "You must input phone !!!");
                }
                if (ModelState.IsValid)
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(supplier);
        }

        // GET: Admin/SuppliersManagement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/SuppliersManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Logo,Email,Phone")] Supplier supplier)
        {

            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {
                    if (supplier.Logo != "logo.png")
                    {
                        var path = "~/images/suppliers/" + supplier.Logo;
                        System.IO.File.Delete(Server.MapPath(path));
                    }

                    //đổi image name = mã nhà cung cấp viết thường + phần mở rộng của image
                    supplier.Logo = Convert.ToString(supplier.Name.ToLower()) + System.IO.Path.GetExtension(f.FileName);
                    var newPath = "~/images/suppliers/" + supplier.Logo;
                    f.SaveAs(Server.MapPath(newPath));
                }
                //cập nhập
                if (supplier.Name == null)
                {
                    ModelState.AddModelError("", "You must input name !!!");
                }
                if (supplier.Email == null)
                {
                    ModelState.AddModelError("", "You must input email !!!");
                }
                if (supplier.Phone == null)
                {
                    ModelState.AddModelError("", "You must input phone !!!");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(supplier).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(supplier);
        }

        // GET: Admin/SuppliersManagement/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/SuppliersManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Supplier supplier = db.Suppliers.Find(id);
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }           
            return RedirectToAction("Index");
        }
    }
}

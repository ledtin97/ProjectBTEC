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
    public class ProductsManagementController : AdminBaseController
    {
        // GET: Admin/ProductsManagement
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Admin/ProductsManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/ProductsManagement/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        // POST: Admin/ProductsManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //dữ liệu content của nó không còn là text thuần túy mà đây là HTML,
        //.NET nhận ra và báo đây là điều nguy hiểm đối với chúng ta trong vấn đề bảo mật.
        //Để tắt cơ chế bảo mật này đi chúng ta thêm vào [ValidateInput(false)] trong controller
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,UnitBrief,UnitPrice,Image,ProductDate,Available,Description,CategoryId,SupplierId,Quantity,Discount,Special,Latest,Views")] Product product)
        {
            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {
                    //đổi image name = mã sản phẩm + phần mở rộng của image
                    product.Image = Convert.ToString(product.Id.ToString()) + System.IO.Path.GetExtension(f.FileName);
                    var path = "~/images/products/" + product.Image;
                    f.SaveAs(Server.MapPath(path));
                }
                else
                {
                    product.Image = "product.png";
                }
                //tạo mới
                if (product.Name == null)
                {
                    ModelState.AddModelError("", "You must input name !!!");
                }
                if (product.UnitBrief == null)
                {
                    ModelState.AddModelError("", "You must input unit brief !!!");
                }
                if (product.ProductDate == null)
                {
                    ModelState.AddModelError("", "You must input date !!!");
                }
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
                ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            
            return View(product);
        }

        // GET: Admin/ProductsManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Admin/ProductsManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // dữ liệu content của nó không còn là text thuần túy mà đây là HTML,
        //.NET nhận ra và báo đây là điều nguy hiểm đối với chúng ta trong vấn đề bảo mật.
        //Để tắt cơ chế bảo mật này đi chúng ta thêm vào [ValidateInput(false)] trong controller
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,UnitBrief,UnitPrice,Image,ProductDate,Available,Description,CategoryId,SupplierId,Quantity,Discount,Special,Latest,Views")] Product product)
        {
            try
            {
                // upload hinh
                var f = Request.Files["UpPhoto"];
                if (f.ContentLength > 0)
                {
                    if (product.Image != "product.png")
                    {
                        var path = "~/images/products/" + product.Image;
                        System.IO.File.Delete(Server.MapPath(path));
                    }
                    //đổi image name = mã sản phẩm + phần mở rộng của image
                    product.Image = Convert.ToString(product.Id.ToString()) + System.IO.Path.GetExtension(f.FileName);
                    var newPath = "~/images/products/" + product.Image;
                    f.SaveAs(Server.MapPath(newPath));
                }

                //cập nhập
                if (product.Name == null)
                {
                    ModelState.AddModelError("", "You must input name !!!");
                }
                if (product.UnitBrief == null)
                {
                    ModelState.AddModelError("", "You must input unit brief !!!");
                }
                if (product.ProductDate == null)
                {
                    ModelState.AddModelError("", "You must input date !!!");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
                ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", product.SupplierId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
           
            return View(product);
        }

        // GET: Admin/ProductsManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/ProductsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
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

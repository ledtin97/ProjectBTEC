using ClosedXML.Excel;
using LedtinEShop.Areas.Admin.Utils;
using LedtinEShop.Models;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LedtinEShop.Areas.Admin.Controllers
{
    public class ReportController : AdminBaseController
    {
        public ActionResult Inventory()
        {
            var model = new ListReports().Inventory();
            return View(model);
        }

        public JsonResult InventoryJS()
        {
            var model = new ListReports().Inventory();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportInventory()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("Quantity") });

            var model = new ListReports().Inventory();

            foreach (var rp in model)
            {
                dt.Rows.Add(rp.Id, rp.Name, rp.Quantity);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Inventory - " + DateTime.Now + ".xlsx");
                }
            }
        }

        public ActionResult SalesOfEachProduct()
        {
            Session["Start"] = "";
            Session["End"] = "";
            var model = new ListReports().SalesOfEachProduct();
            return View(model.OrderBy(p => p.Quantity));
        }

        [HttpPost]
        public ActionResult SalesOfEachProduct(DateTime? daterangepicker_start, DateTime? daterangepicker_end)
        {
            if (daterangepicker_start == null || daterangepicker_end == null)
            {
                ViewBag.Error = "You must input date !!!";
            }
            else
            {
                Session["Start"] = daterangepicker_start;
                Session["End"] = daterangepicker_end;
                var model = new ListReports().SalesOfEachProduct(daterangepicker_start, daterangepicker_end);
                ViewBag.Date = daterangepicker_start + " - " + daterangepicker_end;
                return View(model.OrderBy(p => p.Quantity));
            }

            
            return RedirectToAction("SalesOfEachProduct");
        }

        public JsonResult SalesOfEachProductJS()
        {
            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
               var model1 = new ListReports().SalesOfEachProduct((DateTime)Session["Start"], (DateTime)Session["End"]);
               return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachProduct();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachProduct()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Amount $"),
                                            new DataColumn("Min $"),
                                            new DataColumn("Max $"),
                                            new DataColumn("AVG $") });

            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachProduct((DateTime)Session["Start"], (DateTime)Session["End"]);

                foreach (var rp in model1)
                {
                    dt.Rows.Add(rp.Id, rp.Name, rp.Quantity, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                            "SalesOfEachProduct from " + Session["Start"].ToString() + " to " + Session["End"].ToString() + " - " + DateTime.Now + ".xlsx");
                    }
                }
            }

            var model = new ListReports().SalesOfEachProduct();

            foreach (var rp in model)
            {
                dt.Rows.Add(rp.Id, rp.Name, rp.Quantity, rp.Amount, rp.Min, rp.Max, rp.AVG);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachProduct - " + DateTime.Now + ".xlsx");
                }
            }
        }

        public ActionResult SalesOfEachCategory()
        {
            Session["Start"] = "";
            Session["End"] = "";
            var model = new ListReports().SalesOfEachCategory();
            return View(model);
        }

        [HttpPost]
        public ActionResult SalesOfEachCategory(DateTime? daterangepicker_start, DateTime? daterangepicker_end)
        {
            if (daterangepicker_start == null || daterangepicker_end == null)
            {
                ViewBag.Error = "You must input date !!!";
            }
            else
            {
                Session["Start"] = daterangepicker_start;
                Session["End"] = daterangepicker_end;
                var model = new ListReports().SalesOfEachCategory(daterangepicker_start, daterangepicker_end);
                ViewBag.Date = daterangepicker_start + " - " + daterangepicker_end;
                return View(model);
            }
            return RedirectToAction("SalesOfEachCategory");
            
        }

        public JsonResult SalesOfEachCategoryJS()
        {
            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachCategory((DateTime)Session["Start"], (DateTime)Session["End"]);
                return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachCategory();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachCategory()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Amount $"),
                                            new DataColumn("Min $"),
                                            new DataColumn("Max $"),
                                            new DataColumn("AVG $") });

            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachCategory((DateTime)Session["Start"], (DateTime)Session["End"]);

                foreach (var rp in model1)
                {
                    dt.Rows.Add(rp.Id, rp.Name, rp.Quantity, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "SalesOfEachCategory from " + Session["Start"].ToString() + " to " + Session["End"].ToString() + " - " + DateTime.Now + ".xlsx");
                    }
                }
            }

            var model = new ListReports().SalesOfEachCategory();

            foreach (var rp in model)
            {
                dt.Rows.Add(rp.Id, rp.Name, rp.Quantity, rp.Amount, rp.Min, rp.Max, rp.AVG);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachCategory - " + DateTime.Now + ".xlsx");
                }
            }
        }

        public ActionResult SalesOfEachSupplier()
        {
            Session["Start"] = "";
            Session["End"] = "";
            var model = new ListReports().SalesOfEachSupplier();
            return View(model);
        }

        [HttpPost]
        public ActionResult SalesOfEachSupplier(DateTime? daterangepicker_start, DateTime? daterangepicker_end)
        {
            if (daterangepicker_start == null || daterangepicker_end == null)
            {
                ViewBag.Error = "You must input date !!!";
            }
            else
            {
                Session["Start"] = daterangepicker_start;
                Session["End"] = daterangepicker_end;
                var model = new ListReports().SalesOfEachSupplier(daterangepicker_start, daterangepicker_end);
                ViewBag.Date = daterangepicker_start + " - " + daterangepicker_end;
                return View(model);
            }
            return RedirectToAction("SalesOfEachSupplier");      
        }

        public JsonResult SalesOfEachSupplierJS()
        {
            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachSupplier((DateTime)Session["Start"], (DateTime)Session["End"]);
                return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachSupplier();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachSupplier()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Amount $"),
                                            new DataColumn("Min $"),
                                            new DataColumn("Max $"),
                                            new DataColumn("AVG $") });

            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachSupplier((DateTime)Session["Start"], (DateTime)Session["End"]);

                foreach (var rp in model1)
                {
                    dt.Rows.Add(rp.Id, rp.Name, rp.Quantity, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "SalesOfEachSupplier from " + Session["Start"].ToString() + " to " + Session["End"].ToString() + " - " + DateTime.Now + ".xlsx");
                    }
                }
            }

            var model = new ListReports().SalesOfEachSupplier();

            foreach (var rp in model)
            {
                dt.Rows.Add(rp.Id, rp.Name, rp.Quantity, rp.Amount, rp.Min, rp.Max, rp.AVG);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachSupplier - " + DateTime.Now + ".xlsx");
                }
            }
        }

        public ActionResult SalesOfEachCustomer()
        {
            Session["Start"] = "";
            Session["End"] = "";
            var model = new ListReports().SalesOfEachCustomer();
            return View(model);
        }

        [HttpPost]
        public ActionResult SalesOfEachCustomer(DateTime? daterangepicker_start, DateTime? daterangepicker_end)
        {
            if (daterangepicker_start == null || daterangepicker_end == null)
            {
                ViewBag.Error = "You must input date !!!";
            }
            else
            {
                Session["Start"] = daterangepicker_start;
                Session["End"] = daterangepicker_end;
                var model = new ListReports().SalesOfEachCustomer(daterangepicker_start, daterangepicker_end);
                ViewBag.Date = daterangepicker_start + " - " + daterangepicker_end;
                return View(model);
            }
            return RedirectToAction("SalesOfEachSupplier");
        }

        public JsonResult SalesOfEachCustomerJS()
        {
            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachCustomer((DateTime)Session["Start"], (DateTime)Session["End"]);
                return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachCustomer();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachCustomer()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("Amount $") });

            if (Session["Start"].ToString() != "" && Session["End"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachCustomer((DateTime)Session["Start"], (DateTime)Session["End"]);

                foreach (var rp in model1)
                {
                    dt.Rows.Add(rp.Id, rp.Name, rp.Amount);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "SalesOfEachCustomer from " + Session["Start"].ToString() + " to " + Session["End"].ToString() + " - " + DateTime.Now + ".xlsx");
                    }
                }
            }

            var model = new ListReports().SalesOfEachCustomer();

            foreach (var rp in model)
            {
                dt.Rows.Add(rp.Id, rp.Name, rp.Amount);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachCustomer - " + DateTime.Now + ".xlsx");
                }
            }
        }

        public ActionResult SalesOfEachMonth()
        {
            Session["date"] = "";
            var model = new ListReports().SalesOfEachMonth();
            return View(model);
        }

        [HttpPost]
        public ActionResult SalesOfEachMonth(DateTime? date)
        {

            if (date.GetValueOrDefault().Month == 0)
            {
                Session["date"] = "";
                ModelState.AddModelError("", "You must input month !!!");
            }
            else
            {
                Session["date"] = date;
                var model = new ListReports().SalesOfEachMonth(date.GetValueOrDefault());
                ViewBag.Date = date.GetValueOrDefault().Month + " / " + date.GetValueOrDefault().Year;
                return View(model);
            }
            return View();
        }


        public JsonResult SalesOfEachMonthJS()
        {
            if (Session["date"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachMonth((DateTime)Session["date"]);
                return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachMonth();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachMonth()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Key"),
                                            new DataColumn("Amount $"),
                                            new DataColumn("Min $"),
                                            new DataColumn("Max $"),
                                            new DataColumn("AVG $") });

            try
            {
                if (Session["date"].ToString() != "")
                {
                    DateTime date = (DateTime)Session["date"];

                    var model1 = new ListReports().SalesOfEachMonth(date);

                    foreach (var rp in model1)
                    {
                        dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "SalesOfEachMonth - month " + date.Month + " - year " + date.Year + " - " + DateTime.Now + ".xlsx");
                        }
                    }
                }

                var model = new ListReports().SalesOfEachMonth();

                foreach (var rp in model)
                {
                    dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachMonth - " + DateTime.Now + ".xlsx");
                    }
                }
            }
            catch (Exception)
            {
                var model = new ListReports().SalesOfEachMonth();

                foreach (var rp in model)
                {
                    dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachMonth - " + DateTime.Now + ".xlsx");
                    }
                }
            }
            
        }

        public ActionResult SalesOfEachQuarter()
        {
            Session["date"] = "";
            var model = new ListReports().SalesOfEachQuarter();
            return View(model);
        }

        [HttpPost]
        public ActionResult SalesOfEachQuarter(DateTime? date)
        {
            if (date.GetValueOrDefault().Month == 0)
            {
                ModelState.AddModelError("", "You must input month !!!");
            }
            else
            {
                ListReports list = new ListReports();
                Session["date"] = date;
                var model = list.SalesOfEachQuarter(date.GetValueOrDefault());
                ViewBag.Date = list.FirstDayOfQuarter(date.GetValueOrDefault()) + " - " + list.LastDayOfQuarter(date.GetValueOrDefault());
                return View(model);
            }
            return View();
        }

        public JsonResult SalesOfEachMQuarterJS()
        {
            if (Session["date"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachQuarter((DateTime)Session["date"]);
                return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachQuarter();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachQuarter()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Key"),
                                            new DataColumn("Amount $"),
                                            new DataColumn("Min $"),
                                            new DataColumn("Max $"),
                                            new DataColumn("AVG $") });

            try
            {
                if (Session["date"].ToString() != "")
                {
                    ListReports list = new ListReports();
                    DateTime date = (DateTime)Session["date"];

                    var model1 = list.SalesOfEachQuarter(date);

                    foreach (var rp in model1)
                    {
                        dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "SalesOfEachQuarter from " + list.FirstDayOfQuarter(date) + " to " + list.LastDayOfQuarter(date) + " - " + DateTime.Now + ".xlsx");
                        }
                    }
                }

                var model = new ListReports().SalesOfEachQuarter();

                foreach (var rp in model)
                {
                    dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachQuarter - " + DateTime.Now + ".xlsx");
                    }
                }
            }
            catch (Exception)
            {
                var model = new ListReports().SalesOfEachQuarter();

                foreach (var rp in model)
                {
                    dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachQuarter - " + DateTime.Now + ".xlsx");
                    }
                }
            }

        }

        public ActionResult SalesOfEachYear()
        {
            Session["date"] = "";
            var model = new ListReports().SalesOfEachYear();
            return View(model);
        }

        [HttpPost]
        public ActionResult SalesOfEachYear(DateTime? date)
        {
            if (date.GetValueOrDefault().Year == 0)
            {
                ModelState.AddModelError("", "You must input year !!!");
            }
            else
            {
                Session["date"] = date;
                var model = new ListReports().SalesOfEachYear(date.GetValueOrDefault());
                ViewBag.Date = date.GetValueOrDefault().Year;
                return View(model);
            }
            return View();
            
        }

        public JsonResult SalesOfEachYearJS()
        {
            if (Session["date"].ToString() != "")
            {
                var model1 = new ListReports().SalesOfEachYear((DateTime)Session["date"]);
                return Json(model1, JsonRequestBehavior.AllowGet);
            }

            var model = new ListReports().SalesOfEachYear();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSalesOfEachYear()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Key"),
                                            new DataColumn("Amount $"),
                                            new DataColumn("Min $"),
                                            new DataColumn("Max $"),
                                            new DataColumn("AVG $") });

            try
            {
                if (Session["date"].ToString() != "")
                {
                    DateTime date = (DateTime)Session["date"];

                    var model1 = new ListReports().SalesOfEachYear(date);

                    foreach (var rp in model1)
                    {
                        dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "SalesOfEachYear year " + date.Year + " - " + DateTime.Now + ".xlsx");
                        }
                    }
                }

                var model = new ListReports().SalesOfEachYear();

                foreach (var rp in model)
                {
                    dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachYear - " + DateTime.Now + ".xlsx");
                    }
                }
            }
            catch (Exception)
            {
                var model = new ListReports().SalesOfEachYear();

                foreach (var rp in model)
                {
                    dt.Rows.Add(rp.Id, rp.Amount, rp.Min, rp.Max, rp.AVG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOfEachYear - " + DateTime.Now + ".xlsx");
                    }
                }
            }

        }
    }
}
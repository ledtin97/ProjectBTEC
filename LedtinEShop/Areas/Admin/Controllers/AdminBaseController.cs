using LedtinEShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LedtinEShop.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        // GET: Admin/AdminBase
        protected LedtinEShopEntities db = new LedtinEShopEntities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
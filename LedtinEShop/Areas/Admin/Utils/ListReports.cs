using LedtinEShop.Areas.Admin.Models;
using LedtinEShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedtinEShop.Areas.Admin.Utils
{
    public class ListReports
    {
        protected LedtinEShopEntities db = new LedtinEShopEntities();

        public List<Report> Inventory()
        {
            var query = (from p in db.Products
                         where (p.Available && p.Quantity > 0)
                         select new
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Quantity = p.Quantity
                         }).AsEnumerable().Select(p => new Report()
                         {
                             Id = p.Id.ToString(),
                             Name = p.Name,
                             Quantity = p.Quantity
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachProduct()
        {
            var query = (from o in db.OrderDetails
                         group o by o.ProductId into p
                         select new
                         {
                             Id = p.Key,
                             Name = p.FirstOrDefault().Product.Name,
                             Quantity = p.Sum(d => d.Quantity),
                             Amount = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Sum(d => d.Quantity), 2),
                             Min = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Min(d => d.Quantity), 2),
                             Max = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Max(d => d.Quantity), 2),
                             AVG = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Average(d => d.Quantity), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Quantity = r.Quantity,
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachProduct(DateTime? start, DateTime? end)
        {
            var query = (from o in db.OrderDetails
                         where (o.Order.OrderDate >= start && o.Order.OrderDate <= end)
                         group o by o.ProductId into p
                         select new
                         {
                             Id = p.Key,
                             Name = p.FirstOrDefault().Product.Name,
                             Quantity = p.Sum(d => d.Quantity),
                             Amount = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Sum(d => d.Quantity), 2),
                             Min = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Min(d => d.Quantity), 2),
                             Max = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Max(d => d.Quantity), 2),
                             AVG = Math.Round((p.FirstOrDefault().UnitPrice * (1 - p.FirstOrDefault().Discount)) * p.Average(d => d.Quantity), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Quantity = r.Quantity,
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachCategory()
        {
            var query = (from o in db.OrderDetails
                         group o by o.Product.CategoryId into g
                         select new
                         {
                             Id = g.Key,
                             Name = g.FirstOrDefault().Product.Category.Name,
                             Quantity = g.Sum(x => x.Quantity),
                             Amount = Math.Round(g.Sum(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Min = Math.Round(g.Min(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Max = Math.Round(g.Max(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             AVG = Math.Round(g.Average(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2) 
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Quantity = r.Quantity,
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachCategory(DateTime? start, DateTime? end)
        {
            var query = (from o in db.OrderDetails
                         where (o.Order.OrderDate >= start && o.Order.OrderDate <= end)
                         group o by o.Product.CategoryId into g
                         select new
                         {
                             Id = g.Key,
                             Name = g.FirstOrDefault().Product.Category.Name,
                             Quantity = g.Sum(x => x.Quantity),
                             Amount = Math.Round(g.Sum(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Min = Math.Round(g.Min(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Max = Math.Round(g.Max(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             AVG = Math.Round(g.Average(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Quantity = r.Quantity,
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachSupplier()
        {
            var query = (from o in db.OrderDetails
                         group o by o.Product.SupplierId into g
                         select new
                         {
                             Id = g.Key,
                             Name = g.FirstOrDefault().Product.Supplier.Name,
                             Quantity = g.Sum(x => x.Quantity),
                             Amount = Math.Round(g.Sum(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Min = Math.Round(g.Min(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Max = Math.Round(g.Max(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             AVG = Math.Round(g.Average(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Quantity = r.Quantity,
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachSupplier(DateTime? start, DateTime? end)
        {
            var query = (from o in db.OrderDetails
                         where (o.Order.OrderDate >= start && o.Order.OrderDate <= end)
                         group o by o.Product.SupplierId into g
                         select new
                         {
                             Id = g.Key,
                             Name = g.FirstOrDefault().Product.Supplier.Name,
                             Quantity = g.Sum(x => x.Quantity),
                             Amount = Math.Round(g.Sum(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Min = Math.Round(g.Min(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             Max = Math.Round(g.Max(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2),
                             AVG = Math.Round(g.Average(x => x.Quantity) * (g.FirstOrDefault().UnitPrice * (1 - g.FirstOrDefault().Discount)), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Quantity = r.Quantity,
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.OrderBy(p => p.Quantity).ToList();
        }

        public List<Report> SalesOfEachCustomer()
        {
            var query = (from o in db.Orders
                         group o by o.CustomerId into g
                         select new
                         {
                             Id = g.Key,
                             Name = g.FirstOrDefault().Customer.Fullname,
                             Amount = g.Sum(x => x.Amount)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Amount = r.Amount
                         });
            return query.OrderBy(p => p.Amount).ToList();
        }

        public List<Report> SalesOfEachCustomer(DateTime? start, DateTime? end)
        {
            var query = (from o in db.Orders
                         where (o.OrderDate >= start && o.OrderDate <= end)
                         group o by o.CustomerId into g
                         select new
                         {
                             Id = g.Key,
                             Name = g.FirstOrDefault().Customer.Fullname,
                             Amount = g.Sum(x => x.Amount)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Name = r.Name,
                             Amount = r.Amount
                         });
            return query.OrderBy(p => p.Amount).ToList();
        }

        public List<Report> SalesOfEachMonth()
        {
            var query = (from o in db.Orders
                         group o by o.OrderDate.Month into g
                         select new
                         {
                             Id = "Month " + g.Key,
                             Amount = Math.Round(g.Sum(x => x.Amount), 2),
                             Min = Math.Round(g.Min(x => x.Amount), 2),
                             Max = Math.Round(g.Max(x => x.Amount), 2),
                             AVG = Math.Round(g.Average(x => x.Amount), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Amount = r.Amount ,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.ToList();
        }

        public List<Report> SalesOfEachMonth(DateTime date)
        {
            var query = (from o in db.Orders
                         where (o.OrderDate.Month == date.Month)
                         group o by o.OrderDate into g
                         select new
                         {
                             Id = g.Key,
                             Amount = Math.Round(g.Sum(x => x.Amount), 2),
                             Min = Math.Round(g.Min(x => x.Amount), 2),
                             Max = Math.Round(g.Max(x => x.Amount), 2),
                             AVG = Math.Round(g.Average(x => x.Amount), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.ToList();
        }

        public DateTime FirstDayOfQuarter(DateTime DateIn)
        {
            // Calculate first day of DateIn quarter,
            // with quarters starting at the beginning of Jan/Apr/Jul/Oct
            int intQuarterNum = (DateIn.Month - 1) / 3 + 1;
            return new DateTime(DateIn.Year, 3 * intQuarterNum - 2, 1);
        }
 
        public DateTime LastDayOfQuarter(DateTime DateIn)
        {
            // Calculate last day of DateIn quarter,
            // with quarters ending at the end of Mar/Jun/Sep/Dec
            int intQuarterNum = (DateIn.Month - 1) / 3 + 1;
            int lastDay = 0;
            if (3 * intQuarterNum == 3 || 3 * intQuarterNum == 12)
            {
                lastDay = 31;
            }
            else
            {
                lastDay = 30;
            }
            return new DateTime(DateIn.Year, 3 * intQuarterNum, lastDay);
        }

        public List<Report> SalesOfEachQuarter()
        {
            var query = (from o in db.Orders
                         group o by (o.OrderDate.Month - 1)/3 into g
                         select new
                         {
                             Id = "Quarter " + (g.Key + 1),
                             Amount = Math.Round(g.Sum(x => x.Amount), 2),
                             Min = Math.Round(g.Min(x => x.Amount), 2),
                             Max = Math.Round(g.Max(x => x.Amount), 2),
                             AVG = Math.Round(g.Average(x => x.Amount), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.ToList();
        }

        public List<Report> SalesOfEachQuarter(DateTime date)
        {
            DateTime start = FirstDayOfQuarter(date);
            DateTime end = LastDayOfQuarter(date);

            var query = (from o in db.Orders
                         where (o.OrderDate >= start && o.OrderDate <= end)
                         group o by o.OrderDate into g
                         select new
                         {
                             Id = g.Key,
                             Amount = Math.Round(g.Sum(x => x.Amount), 2),
                             Min = Math.Round(g.Min(x => x.Amount), 2),
                             Max = Math.Round(g.Max(x => x.Amount), 2),
                             AVG = Math.Round(g.Average(x => x.Amount), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.ToList();
        }

        public List<Report> SalesOfEachYear()
        {
            var query = (from o in db.Orders
                         group o by o.OrderDate.Year into g
                         select new
                         {
                             Id = "Year " + g.Key,
                             Amount = Math.Round(g.Sum(x => x.Amount), 2),
                             Min = Math.Round(g.Min(x => x.Amount), 2),
                             Max = Math.Round(g.Max(x => x.Amount), 2),
                             AVG = Math.Round(g.Average(x => x.Amount), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.ToList();
        }

        public List<Report> SalesOfEachYear(DateTime date)
        {
            var query = (from o in db.Orders
                         where o.OrderDate.Year == date.Year
                         group o by o.OrderDate into g
                         select new
                         {
                             Id = g.Key,
                             Amount = Math.Round(g.Sum(x => x.Amount), 2),
                             Min = Math.Round(g.Min(x => x.Amount), 2),
                             Max = Math.Round(g.Max(x => x.Amount), 2),
                             AVG = Math.Round(g.Average(x => x.Amount), 2)
                         }).AsEnumerable().Select(r => new Report()
                         {
                             Id = r.Id.ToString(),
                             Amount = r.Amount,
                             Min = r.Min,
                             Max = r.Max,
                             AVG = r.AVG
                         });
            return query.ToList();
        }
    }
}
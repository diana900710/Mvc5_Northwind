using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Mvc5_Northwind.Models;
using Mvc5_Northwind.ViewModels;

namespace Mvc5_Northwind.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityDBEntities _context;
        public HomeController() 
        {
            _context = new IdentityDBEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Product> Products = _context.Products.ToList();
            List<Employee> Employees = _context.Employees.ToList();
            MixedViewModel mixedVM = new MixedViewModel()
            {
                Products = Products,
                Employees = Employees
            };

            if (Products.Count == 0)
            {
                return HttpNotFound();
            }

            return View(mixedVM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
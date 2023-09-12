using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Mvc5_Northwind.Models;

namespace Mvc5_Northwind.Controllers
{
    public class ProdsController : Controller
    {
        private readonly string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Northwind;Integrated Security=true";

        private readonly IdentityDBEntities _context;
        public ProdsController()
        {
            _context = new IdentityDBEntities();
        }

        // GET: Employees
        public ActionResult Index()
        {
            string queryString = "SELECT ProductID, UnitPrice, ProductName from dbo.products "
                + "WHERE UnitPrice > @pricePoint "
                + "ORDER BY UnitPrice DESC;";

            int paramValue = 5;
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@pricePoint", paramValue);

                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();                    

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ProductID"]);
                        string name = reader["ProductName"].ToString();
                        decimal price = (decimal)reader["UnitPrice"];

                        products.Add(new Product() { ProductID = id, ProductName = name, UnitPrice = price });
                    }
                }
                catch (Exception ex)
                {

                }
            }

            

            return View(products);
        }

        public ActionResult CreateData() 
        {
           Product p1 = new Product { ProductName = "Car", UnitPrice = 100000, UnitsInStock = 1, UnitsOnOrder = 10 };

            _context.Products.Add(p1);
            _context.SaveChanges();

            return Content($"ProductID : {p1.ProductID}");
        }

        public ActionResult EditData()
        {
            int id = 78;
            var product = _context.Products.Find(id);
            if(product != null)
            {
                product.UnitPrice = 120000;
                _context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            
            return Content($"ProductID : {product.ProductID}, Price : {product.UnitPrice}");
        }
        public ActionResult DeleteData()
        {
            var p = _context.Products.Find(78);
            _context.Products.Remove(p);
            int result =  _context.SaveChanges();

            return Content($"異動筆數 : {result}");
        }
    }
}
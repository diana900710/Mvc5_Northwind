using Mvc5_Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5_Northwind.ViewModels
{
    public class MixedViewModel
    {
        public List<Product> Products { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
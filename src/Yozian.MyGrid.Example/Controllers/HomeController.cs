using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yozian.MyGrid.Example.Models;

namespace Yozian.MyGrid.Example.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

           var vm = new List<Book>()
            {
                new Book(){
                    Id =1,
                    Name ="Advanced C# Programing",
                    CreatedAt = new DateTime(2019,01,02)
                },
                new Book(){
                      Id =1,
                    Name ="Javascript Programing",
                    CreatedAt = new DateTime(2019,07,31)
                }

            };

            return View(vm);
        }
    }
}

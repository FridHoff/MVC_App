using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_App.Models;
using ControllersApp.Util;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MVC_App.Controllers
{
    public class HomeController : HelloBaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            //return RedirectToRoute("default", new { controller = "Home", action = "Area", height = 2, altitude = 20 });
            //return RedirectToAction("Area", "Home", new { altitude = 10, height = 3 });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region 9.1
        [ActionName("Welcome")]
        public string Hello()
        {
            return "Hello ASP.NET";
        }
        #endregion
        #region 9.2
        public string Hello(int id)
        {
            return $"id= {id}";
        }
        public string Square(int a = 3, int h = 10)
        {
            double s = a * h / 2;
            return $"Площадь треугольника с основанием {a} и высотой {h} равна {s}";
        }
        [HttpPost]
        public string Area(Geometry geometry)
        {
            return $"Площадь треугольника с основанием {geometry.Altitude} и высотой {geometry.Height} равна {geometry.GetArea()}";
        }
        public IActionResult Area(int altitude, int height)
        {
            double area = altitude * height / 2;
            return Content($"Площадь треугольника с основанием {altitude} и высотой {height} равна {area}");
        }
        public string Sum(int[] nums)
        {
            return $"Сумма чисел равна {nums.Sum()}";
        }
        public string Sum(Geometry[] geoms)
        {
            return $"Сумма площадей равна {geoms.Sum(g => g.GetArea())}";
        }
        #endregion
        #region 9.3
        public HtmlResult GetHtml()
        {
            return new HtmlResult("<h2>Привет ASP.NET 5</h2>");
        }
        #endregion
        #region 9.4
        public JsonResult GetName()
        {
            string name = "Tom";
            return Json(name);
        }
        public JsonResult GetUser()
        {
            User user = new User { Name = "Tom", Age = 28 };
            return Json(user);
        }
        #endregion
        #region 9.6
        //public IActionResult Index(string str)
        //{
        //    int num;
        //    if (String.IsNullOrEmpty(str))
        //        return BadRequest("Не указаны параметры запроса");
        //    if (!Int32.TryParse(str, out num))
        //        return BadRequest("Указаны некорректные параметры запроса");
        //    else if (num < 18)
        //        return Unauthorized(new Error { Message = "параметр age содержит недействительное значение" });
        //    return View();
        //}
        #endregion
        #region 9.7
        private readonly IWebHostEnvironment _appEnvironment;
        //public HomeController(IWebHostEnvironment appEnvironment)
        //{
        //    _appEnvironment = appEnvironment;
        //}
        public IActionResult GetFile()
        {
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "book.pdf";
            return PhysicalFile(file_path, file_type, file_name);
        }
        public FileResult GetBytes()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/pdf";
            string file_name = "book2.pdf";
            return File(mas, file_type, file_name);
        }
        public FileResult GetStream()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "book3.pdf";
            return File(fs, file_type, file_name);
        }
        public VirtualFileResult GetVirtualFile()
        {
            var filepath = Path.Combine("~/Files", "hello.txt");
            return File(filepath, "text/plain", "hello.txt");
        }
        #endregion
    }
    public class Geometry
    {
        public int Altitude { get; set; }
        public int Height { get; set; } 

        public double GetArea()
        {
            return Altitude * Height / 2;
        }
    }
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Error
    {
        public string Message { get; set; }
    }
}

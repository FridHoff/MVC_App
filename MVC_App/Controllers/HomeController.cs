using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_App.Models;
using MVC_App.ViewModels;
using ControllersApp.Util;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MVC_App.Controllers
{
    public class HomeController : HelloBaseController
    {

        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _appEnvironment;
        List<Phone> phones;
        List<Company> companies;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            Company apple = new Company { Id = 1, Name = "Apple", Country = "США" };
            Company microsoft = new Company { Id = 2, Name = "Samsung", Country = "Республика Корея" };
            Company google = new Company { Id = 3, Name = "Google", Country = "США" };
            companies = new List<Company> { apple, microsoft, google };

            phones = new List<Phone>
            {
                new Phone { Id=1, Manufacturer= apple, Name="iPhone X", Price=56000 },
                new Phone { Id=2, Manufacturer= apple, Name="iPhone XZ", Price=41000 },
                new Phone { Id=3, Manufacturer= microsoft, Name="Galaxy 9", Price=9000 },
                new Phone { Id=4, Manufacturer= microsoft, Name="Galaxy 10", Price=40000 },
                new Phone { Id=5, Manufacturer= google, Name="Pixel 2", Price=30000 },
                new Phone { Id=6, Manufacturer= google, Name="Pixel XL", Price=50000 }
            };
        }
        User admin = new User { Login = "admin", Password = "1111" };
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(admin.Name))
                return View();
            else
                return RedirectToAction("Login");
            //return RedirectToRoute("default", new { controller = "Home", action = "Area", height = 2, altitude = 20 });            
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
        #region 9.9
        //public void Index()
        //{
        //    string table = "";
        //    foreach (var header in Request.Headers)
        //    {
        //        table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
        //    }
        //    Response.WriteAsync($"<table>{table}</table>");
        //}
        #endregion
        #region 9.10
        //private readonly ITimeService _timeService;
        //public HomeController(ITimeService timeServ)
        //{
        //    _timeService = timeServ;
        //}
        //public string Index([FromServices] ITimeService timeService)
        //{
        //    return timeService.Time;
        //}
        //public string Index()
        //{
        // //   return _timeService.Time;
        //    ITimeService timeService = HttpContext.RequestServices.GetService<ITimeService>();
        //    return timeService?.Time;
        //}
        #endregion
        #region 10.6    
        public ActionResult GetMessage()
        {
            return PartialView("_GetMessage");
        }
        #endregion
        #region 10.8
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            //string authData = $"Login: {login}   Password: {password}";
            //return Content(authData);
            if (login == "admin" && password == "1111")
            {
                admin.Name = "admin";
                return RedirectToAction("Index");
            }
            else
                return Content("Неверно указаны имя пользователя или пароль");
        }
        #endregion
        #region 12.1
        //public IActionResult Phones()
        //{
        //    return View(phones);
        //}
        #endregion
        #region 12.2
        public IActionResult Phones(int? companyId)
        {
            List<CompanyModel> compModels = companies
                .Select(c => new CompanyModel { Id = c.Id, Name = c.Name })
                .ToList();
            // добавляем на первое место
            compModels.Insert(0, new CompanyModel { Id = 0, Name = "Все" });

            IndexViewModel ivm = new IndexViewModel { Companies = compModels, Phones = phones };

            // если передан id компании, фильтруем список
            if (companyId != null && companyId > 0)
                ivm.Phones = phones.Where(p => p.Manufacturer.Id == companyId);

            return View(ivm);
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
        public string Login { get; set; }
        public string Password { get; set; }
    }
    class Error
    {
        public string Message { get; set; }
    }
}

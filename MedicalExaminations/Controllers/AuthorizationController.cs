using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedicalExaminations.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly AppDbContext context;

        //public AuthorizationController(ILogger<AuthorizationController> logger)
        //{
        //    _logger = logger;
        //}

        public AuthorizationController(AppDbContext context) => this.context = context;

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Authorize(string login, string password)
        {
            var authorizer = new Authorizer(context);
            
            if(authorizer.Authorize(login, password))
            {
                ViewBag.Message = "Успешно";
                return RedirectToAction("Index", "Mainform");
            }
            else
            {
                ViewBag.Message = "Неверный логин или пароль";
                return View("Index");
            }
        }
    }
}
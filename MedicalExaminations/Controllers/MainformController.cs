using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalExaminations.Controllers
{
    public class MainformController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.CanViewAnimalsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewAnimalsRegistry;
            ViewBag.CanViewOrganizationsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewOrganizationsRegistry;
            ViewBag.CanViewContractsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewContractsRegistry;
            return View();
        }
    }
}

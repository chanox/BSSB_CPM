using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class CommonViewsController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Error_One()
        {
            return View();
        }

        public IActionResult Error_Two()
        {
            return View();
        }

        public IActionResult LockScreen()
        {
            return View();
        }

        public IActionResult PasswordRecovery()
        {
            return View();
        }

    }
}
using CPM_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CPM_Project.Controllers
{
    public class DashboardController : Controller
    {
        private LoginData LoginData = new LoginData();
        private AppDBContext db = new AppDBContext();

        GlobalVariable _GlobalVariable = new();

        private void GetGlobalVariable()
        {
            try
            {
                LoginData = JsonConvert.DeserializeObject<LoginData>(HttpContext.Session.GetString("LoginData"));
            }
            catch
            {
                LoginData = null;
            }
        }

        public IActionResult DashboardBPDP()
        {
            ViewBag.Header = "Transaksi InHouse";
            ViewBag.Title = "Monitoring";
            ViewBag.Keterangan = "Menampilkan seluruh informasi transaksi InHouse";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            return View();
        }
        
        public IActionResult Index()
        {
            ViewBag.Header = "Dashboard";
            ViewBag.Title = "Dashboard";
            ViewBag.Keterangan = "Meampilkan Informasi";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            return View();
        }
    }
}
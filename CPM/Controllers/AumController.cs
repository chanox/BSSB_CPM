using CPM_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CPM_Project.Controllers
{
    public class AumController : Controller
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

        public IActionResult Index()
        {
            ViewBag.Header = "Asset Under Management";
            ViewBag.Title = "List Nasabah Prioritas";
            ViewBag.Keterangan = "List Nasabah Prioritas";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            ViewBag.ListCabang = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return View();
        }

        public IActionResult DetailNasabah (string nik)
        {
            ViewBag.Header = "Asset Under Management";
            ViewBag.Title = "Detail Nasabah Prioritas";
            ViewBag.Keterangan = "Detail Nasabah Prioritas";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }
    }
}

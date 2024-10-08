using CPM_Project.Models;
using CPM_Project.Models.BPDP;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolsEngine;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CPM_Project.Controllers
{
    public class BPDPController : Controller
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

        public IActionResult DaftarInHouse()
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

        [HttpPost]
        public async Task<IActionResult> GelListInhouse(int TOT_REC)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return StatusCode(500);
            }

            List<TB_BPDP_Trans>? resultData = new();

            try
            {
                if (LoginData.JENIS_USER == "Kanpus" && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/GetInhouseTrans?KD_KNTR=" + "&TOT_REC=" + TOT_REC + "@TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<List<TB_BPDP_Trans>>(TMP.ToString());
                }
                else if (LoginData.JENIS_USER == "User" && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/GetInhouseTrans?KD_KNTR=" + LoginData.KD_UNITKER + "&TOT_REC=" + TOT_REC + "@TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<List<TB_BPDP_Trans>>(TMP.ToString());
                }

                return Json(resultData);
            }
            catch (Exception ex)
            {
                return Json(resultData);
            }
        }

        public IActionResult DaftarRTGS()
        {
            ViewBag.Header = "Transaksi RTGS";
            ViewBag.Title = "Monitoring";
            ViewBag.Keterangan = "Menampilkan seluruh informasi transaksi RTGS";

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

        [HttpPost]
        public async Task<IActionResult> GelListRTGS(int TOT_REC)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return StatusCode(500);
            }

            List<TB_BPDP_RTGS_Trans>? resultData = new();

            try
            {
                if (LoginData.JENIS_USER == "Kanpus" && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/GetRTGSTrans?KD_KNTR=" + "&TOT_REC=" + TOT_REC + "@TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<List<TB_BPDP_RTGS_Trans>>(TMP.ToString());
                }
                else if (LoginData.JENIS_USER == "User" && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/GetRTGSTrans?KD_KNTR=" + LoginData.KD_UNITKER + "&TOT_REC=" + TOT_REC + "@TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<List<TB_BPDP_RTGS_Trans>>(TMP.ToString());
                }

                return Json(resultData);
            }
            catch (Exception ex)
            {
                return Json(resultData);
            }
        }

        public IActionResult DaftarSKN()
        {
            ViewBag.Header = "Transaksi SKN / Kliring";
            ViewBag.Title = "Monitoring";
            ViewBag.Keterangan = "Menampilkan seluruh informasi transaksi SKN";

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

        [HttpPost]
        public async Task<IActionResult> GelListSKN(int TOT_REC)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return StatusCode(500);
            }

            List<TB_BPDP_SKN_Trans>? resultData = new();

            try
            {
                if (LoginData.JENIS_USER == "Kanpus" && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/GetSKNTrans?KD_KNTR=" + "&TOT_REC=" + TOT_REC + "@TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<List<TB_BPDP_SKN_Trans>>(TMP.ToString());
                }
                else if (LoginData.JENIS_USER == "User" && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/GetSKNTrans?KD_KNTR=" + LoginData.KD_UNITKER + "&TOT_REC=" + TOT_REC + "@TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<List<TB_BPDP_SKN_Trans>>(TMP.ToString());
                }

                return Json(resultData);
            }
            catch (Exception ex)
            {
                return Json(resultData);
            }
        }

        public class DashboardInfo
        {
            public int totInHouse { get; set; }
            public int totRTGS { get; set; }
            public int totSKN { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return StatusCode(500);
            }

            DashboardInfo? resultData = new();

            try
            {
                if (LoginData.JENIS_USER == "Kanpus"  && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/Dashboard?KD_KNTR=" + "&TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<DashboardInfo>(TMP.ToString());
                }
                else if (LoginData.JENIS_USER == "User"  && LoginData.GRUP_USER == "BPDP")
                {
                    object? TMP = await JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/bpdp/Dashboard?KD_KNTR=" + LoginData.KD_UNITKER + "&TGL_LIMIT=");
                    resultData = JsonConvert.DeserializeObject<DashboardInfo>(TMP.ToString());
                }

                return Json(resultData);
            }
            catch (Exception ex)
            {
                return Json(resultData);
            }
        }
    }
}


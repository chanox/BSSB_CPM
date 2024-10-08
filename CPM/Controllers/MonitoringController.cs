using CPM_Project.Authorize;
using CPM_Project.Models;
using CPM_Project.Models.BPDP;
using CPM_Project.Models.CORE;
using CPM_Project.Models.SERVICE;
using CPM_Project.Models.SRAK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ToolsEngine;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CPM_Project.Controllers
{
    [AuthorizeAccess(Level = "Kanpus")]
    public class MonitoringController : Controller
    {
        private LoginData LoginData = new LoginData();
        private AppDBContext db = new AppDBContext();

        OtherTools Tools = new OtherTools();
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

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            return View();
        }

        [HttpPost]
        public async Task<List<TB_BPDP_Trans>?> GelListInhouse(int TOT_REC)
        {
            GetGlobalVariable();

            List<TB_BPDP_Trans>? resultData = new();

            try
            {
                object? TMP = await JSONTools.GetJSON("https://api.banksulselbar.co.id/bpdp/GetInhouseTrans?KD_KNTR=" + LoginData.KD_UNITKER + "&TOT_REC=" + TOT_REC);
                resultData = JsonConvert.DeserializeObject<List<TB_BPDP_Trans>>(TMP.ToString());

                return resultData;
            }
            catch (Exception ex)
            {
                return resultData;
            }
        }

        public IActionResult DaftarRTGS()
        {
            ViewBag.Header = "Transaksi RTGS";
            ViewBag.Title = "Monitoring";
            ViewBag.Keterangan = "Menampilkan seluruh informasi transaksi RTGS";

            GetGlobalVariable();

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            return View();
        }

        [HttpPost]
        public async Task<List<TB_BPDP_RTGS_Trans>?> GelListRTGS(int TOT_REC)
        {
            GetGlobalVariable();

            List<TB_BPDP_RTGS_Trans>? resultData = new();

            try
            {
                object? TMP = await JSONTools.GetJSON("https://api.banksulselbar.co.id/bpdp/GetRTGSTrans?KD_KNTR=" + LoginData.KD_UNITKER + "&TOT_REC=" + TOT_REC);
                resultData = JsonConvert.DeserializeObject<List<TB_BPDP_RTGS_Trans>>(TMP.ToString());

                return resultData;
            }
            catch (Exception ex)
            {
                return resultData;
            }
        }

        public IActionResult DaftarSKN()
        {
            ViewBag.Header = "Transaksi SKN / Kliring";
            ViewBag.Title = "Monitoring";
            ViewBag.Keterangan = "Menampilkan seluruh informasi transaksi SKN";

            GetGlobalVariable();

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            return View();
        }

        [HttpPost]
        public async Task<List<TB_BPDP_SKN_Trans>?> GelListSKN(int TOT_REC)
        {
            GetGlobalVariable();

            List<TB_BPDP_SKN_Trans>? resultData = new();

            try
            {
                object? TMP = await JSONTools.GetJSON("https://api.banksulselbar.co.id/bpdp/GetSKNTrans?KD_KNTR=" + LoginData.KD_UNITKER + "&TOT_REC=" + TOT_REC);
                resultData = JsonConvert.DeserializeObject<List<TB_BPDP_SKN_Trans>>(TMP.ToString());

                return resultData;
            }
            catch (Exception ex)
            {
                return resultData;
            }
        }

        public IActionResult RekeningKoranHome()
        {
            ViewBag.Header = "Rekening Koran";
            ViewBag.Title = "Rekening Koran";
            ViewBag.Keterangan = "Menampilkan rincian transaksi rekening koran.";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            ViewBag.FIRST_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");

            ViewBag.ListCabang = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return View();
        }

        public IActionResult getRekeningKoran(DateTime tglAwal, DateTime tglAkhir, string noRek)
        {
            GetGlobalVariable();

            var resultData = new RekeningKoran.RekeningGridResponse()
            {
                HISTORY_TRANS = new List<RekeningKoran.HistoryGrid>()
            };

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                RekeningKoran.RekeningRequest reqData = new RekeningKoran.RekeningRequest()
                {
                    TGL_AWAL = tglAwal.Date,
                    TGL_AKHIR = tglAkhir.Date,
                    NO_REK = noRek,
                    OPSI = 1,
                    JUM_TRANS = 0
                };

                if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run(() => JSONTools.PostJSON(_GlobalVariable.ServiceAddress, "/CORE/RekeningKoranGrid", reqData)).Result;
                    resultData = JsonConvert.DeserializeObject<RekeningKoran.RekeningGridResponse>(tmp.ToString());
                }

                return Json(resultData);
            }
            catch
            {
                return Json(resultData);
            }
        }

        public IActionResult InfoSaldoHome()
        {
            ViewBag.Header = "Saldo Akhir";
            ViewBag.Title = "Daftar Saldo Akhir";
            ViewBag.Keterangan = "Menampilkan saldo akhir dari daftar rekening yang akan di monitoring.";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            ViewBag.FIRST_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");

            ViewBag.ListCabang = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return View();
        }

        public decimal getSaldoAkhir(string accNo)
        {
            GetGlobalVariable();

            var reqData = new reqInfoSaldo()
            {
                NO_REK = accNo
            };

            var tmp = Task.Run(() => JSONTools.PostJSON(_GlobalVariable.ServiceAddress, "/CORE/Infosaldo", reqData)).Result;
            var resultData = JsonConvert.DeserializeObject<resInfoSaldo>(tmp.ToString());

            return resultData.RESPONSE_CODE == "00" ? resultData.SALDO_AKHIR : 0;
        }

        public List<TB_SRAK_Saldo> getSaldoList()
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return new List<TB_SRAK_Saldo>();
            }

            var listSaldo = db.TB_SRAK_Saldo.ToList();

            try
            {
                foreach (var items in listSaldo)
                {
                    items.SALDO_AKHIR = getSaldoAkhir(items.NO_REK);
                }

                return listSaldo;
            }
            catch
            {
                return listSaldo;
            }
        }
    }
}

using System.Net;
using CPM_Project.Authorize;
using CPM_Project.Models;
using CPM_Project.Models.RAS;
using CPM_Project.Models.SRAK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ToolsEngine;

namespace CPM_Project.Controllers
{
    [AuthorizeAccess(Level = "Kanpus")]
    public class RASController : Controller
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

        public IActionResult RASHome()
        {
            ViewBag.Header = "Monitoring Risiko";
            ViewBag.Title = "Monitoring Seluruh Risiko";
            ViewBag.Keterangan = "Informasi lengkap hasil perhitungan risiko RAS.";

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

        public IActionResult GetDataRAS(int TAHUN, string JENIS)
        {
            GetGlobalVariable();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            dynamic resultData;
            //
            // if (kdKNTR == "Semua Cabang")
            // {
            //     kdKNTR = "";
            // }

            try
            {
                // if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                // {
                //     var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/SRAK/GetDataSRAK?tglData={tglData.Date:yyyy-MM-dd}&kdKNTR={kdKNTR}&jenisProduk={jenisProduk}")).Result;
                //     resultData = JsonConvert.DeserializeObject<List<TB_SRAK>>(tmp.ToString());
                // }
                // else if (LoginData is { JENIS_USER: "User", GRUP_USER: "SRAK" })
                // {
                //     var tmp = Task.Run((() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/SRAK/GetDataSRAK?tglData={tglData.Date:yyyy-MM-dd}&kdKNTR={LoginData.KD_UNITKER}&jeniProduk={jenisProduk}"))).Result;
                //     resultData = JsonConvert.DeserializeObject<List<TB_SRAK>>(tmp.ToString());
                // }

                if (JENIS.ToUpper() == "KREDIT")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoKredit?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Kredit>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "PASAR")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoPasar?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Pasar>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "LIKUIDITAS")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoLikuiditas?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Likuiditas>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "OPERASIONAL")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoOperasional?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Operasional>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "HUKUM")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoHukum?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Hukum>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "STRATEGIK")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoStrategik?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Strategik>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "KEPATUHAN")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoKepatuhan?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Kepatuhan>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "REPUTASI")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoReputasi?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Reputasi>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "IMBALHASIL")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoImbalHasil?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Imbal_Hasil>>(tmp.ToString());   
                }
                else if (JENIS.ToUpper() == "INVESTASI")
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/RAS/getResikoInvestasi?tahun={TAHUN}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_RAS_Resiko_Investasi>>(tmp.ToString());   
                }
                else
                {
                    resultData = new List<TB_RAS_Resiko_Kredit>();
                }

                return Json(resultData);
            }
            catch (Exception ex)
            {
                resultData = new List<TB_RAS_Resiko_Kredit>();
                return Json(resultData);
            }
        }
        
        public IActionResult GetDataSRAK(DateTime tglData, string kdKNTR, string jenisProduk)
        {
            GetGlobalVariable();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            List<TB_SRAK>? resultData = new();

            if (kdKNTR == "Semua Cabang")
            {
                kdKNTR = "";
            }

            try
            {
                if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/SRAK/GetDataSRAK?tglData={tglData.Date:yyyy-MM-dd}&kdKNTR={kdKNTR}&jenisProduk={jenisProduk}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_SRAK>>(tmp.ToString());
                }
                else if (LoginData is { JENIS_USER: "User", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run((() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/SRAK/GetDataSRAK?tglData={tglData.Date:yyyy-MM-dd}&kdKNTR={LoginData.KD_UNITKER}&jeniProduk={jenisProduk}"))).Result;
                    resultData = JsonConvert.DeserializeObject<List<TB_SRAK>>(tmp.ToString());
                }

                return Json(resultData);
            }
            catch (Exception ex)
            {
                return Json(resultData);
            }
        }

        public IActionResult GetPercentSetting()
        {
            GetGlobalVariable();

            List<SRAK_Bunga> resultData = new List<SRAK_Bunga>();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            try
            {
                if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/SRAK/GetDataBungaSRAK")).Result;
                    resultData = JsonConvert.DeserializeObject<List<SRAK_Bunga>>(tmp.ToString());
                }

                return Json(resultData);
            }
            catch
            {
                return Json(resultData);
            }
        }

        [HttpPost, HttpGet]
        public IActionResult UpdateBunga()
        {
            var request = HttpContext.Request;

            List<string> listData = new List<string>();

            foreach (var key in request.Form.Keys)
            {
                var value = request.Form[key];
                listData.Add(value);
            }

            var req = new reqUpdateBunga
            {
                ID_SRAK_BUNGA = Convert.ToInt32(listData[0]),
                BUNGA = Convert.ToDecimal(listData[1])
            };

            GetGlobalVariable();

            var resultData = new resAPI();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            try
            {
                if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run(() => JSONTools.PostJSON(_GlobalVariable.ServiceAddress, "/SRAK/UpdateDataBungaSRAK", req)).Result;
                    resultData = JsonConvert.DeserializeObject<resAPI>(tmp.ToString());
                }
                else
                {
                    resultData = null;
                }

                return Json(resultData);
            }
            catch
            {
                return Json(resultData);
            }
        }

        [HttpPost]
        public IActionResult reGenerateData(string tglData)
        {
            var req = new reqRegenerateData()
            {
                tglData = Convert.ToDateTime(tglData)
            };

            GetGlobalVariable();

            var resultData = new resAPI();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            try
            {
                if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run(() => JSONTools.PostJSON(_GlobalVariable.ServiceAddress, "/SRAK/ReGenerateDataSRAK", req)).Result;
                    resultData = JsonConvert.DeserializeObject<resAPI>(tmp.ToString());
                }
                else
                {
                    resultData = new resAPI()
                    {
                        code = "99",
                        message = "No access.."
                    };
                }

                return Json(resultData);
            }
            catch
            {
                return Json(resultData);
            }
        }

        public IActionResult BungaSRAKHome()
        {
            ViewBag.Header = "Pengaturan Bunga";
            ViewBag.Title = "Pengaturan Bunga";
            ViewBag.Keterangan = "Pengaturan bunga perhitungan SRAK.";

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

        public string getHeader(string KD_KNTR)
        {
            GetGlobalVariable();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            try
            {
                var resultData = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/KUR/getHeaderDataKUR?KD_KNTR=" + KD_KNTR)).Result;

                return resultData.ToString();
            }
            catch
            {
                return "[]";
            }
        }

        public string getDetail(string NO_REK)
        {
            GetGlobalVariable();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            try
            {
                var resultData = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + "/KUR/getDetailDataKUR?NO_REK=" + NO_REK)).Result;

                return resultData.ToString();
            }
            catch
            {
                return "[]";
            }
        }

        public IActionResult AkumulasiHome()
        {
            ViewBag.Header = "Akumulasi Perhitungan SRAK";
            ViewBag.Title = "Hasi Akumulasi Perhitungan SRAK";
            ViewBag.Keterangan = "Informasi lengkap hasil akumulasi perhitungan SRAK seluruh cabang.";

            GetGlobalVariable();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            ViewBag.FIRST_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).ToString("yyyy-MM-dd");

            ViewBag.ListCabang = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return View();
        }

        public IActionResult getDataAkumulasi(DateTime tglAwal, DateTime tglAkhir, string kdKNTR, string jenisProduk)
        {
            GetGlobalVariable();

            var resultData = new List<dataAkumulasiSRAK>();

            // if (LoginData == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            try
            {
                if (kdKNTR == "Semua Cabang")
                {
                    kdKNTR = "";
                }

                if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
                {
                    var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/SRAK/GetDataAkumulasiSRAK?tglAwal={tglAwal.Date:yyyy-MM-dd}&tglAkhir={tglAkhir.Date:yyyy-MM-dd}&jenisProduk={jenisProduk}&kdKNTR={kdKNTR}")).Result;
                    resultData = JsonConvert.DeserializeObject<List<dataAkumulasiSRAK>>(tmp.ToString());
                }

                return Json(resultData);
            }
            catch
            {
                return Json(resultData);
            }
        }
    }
}

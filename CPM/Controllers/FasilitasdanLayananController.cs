using CPM_Project.Authorize;
using CPM_Project.Models;
using CPM_Project.Models.CPM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CPM_Project.Controllers
{
    public class FasilitasdanLayananController : Controller
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


        // Safe Deposit Box
		public IActionResult SdbHome()
		{
			ViewBag.Header = "Fasilitas dan Layanan";
			ViewBag.Title = "Safe Deposit Box";
			ViewBag.Keterangan = "Safe Deposit Box";

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

        [HttpGet]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateSdb()
        {
            TB_Sdb _tbSdb = new TB_Sdb();

            //ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User" });
            //ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return PartialView("_CreateSdb", _tbSdb);
        }

        [HttpPost]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateSdb(TB_Sdb _tbSdb)
        {
            if (ModelState.IsValid)
            {
                db.tb_Sdb.Add(_tbSdb);
                db.SaveChanges();

                return Content("0");
            }

            return PartialView("_CreateSdb");
        }

        public async Task<IActionResult> GetListSdb()
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            //if (LoginData.JENIS_USER != "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "User");
            //}

            var SdbList = (from a in await db.tb_Sdb.ToListAsync()
                            join b in db.msUnitKerja on a.KD_UNITKER equals b.KD_UNITKER
                            select new
                            {
                                ID_SDB = a.ID_SDB,
                                NIK = a.NIK,
                                NM_NAS = a.NM_NAS,
                                CABANG = b.NM_UNITKER,
                                NO_SDB = a.NO_SDB,
                                UKURAN_SDB = a.UKURAN_SDB,
                                TGL_PKS = a.TGL_PKS,
                                TGL_EXPIRED = a.TGL_EXPIRED,
                                BIAYA = a.BIAYA
                            }).ToList();

            return Json(SdbList);
        }

        [HttpPost]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult DeleteSdb(int ID_SDB)
        {
            try
            {
                TB_Sdb dataSdb = db.tb_Sdb.SingleOrDefault(m => m.ID_SDB == ID_SDB);
                db.tb_Sdb.Remove(dataSdb);
                db.SaveChanges();

                return Content("0");
            }
            catch
            {
                return Content("9");
            }
        }

        [HttpGet]
        //[AuthorizeAccess(Level = "Admin")]
        public async Task<IActionResult> EditSdb(int? ID_SDB)
        {
            var _tbSdb = await db.tb_Sdb.SingleOrDefaultAsync(m => m.ID_SDB == ID_SDB);

            //ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User" });
            //ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return PartialView("_EditSdb", _tbSdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult EditSdb(TB_Sdb _tbSdb)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            //if (LoginData.JENIS_USER != "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "User");
            //}

            //ModelState.Remove("USER_NAME");
            if (ModelState.IsValid)
            {
                try
                {
                    db.tb_Sdb.Update(_tbSdb);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("9");
                }
                return Content("0");
            }
            return View(_tbSdb);
        }


        // Special Gift
        public IActionResult SpecialGiftHome()
        {
            ViewBag.Header = "Fasilitas dan Layanan";
            ViewBag.Title = "Special Gift";
            ViewBag.Keterangan = "Special Gift";

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

        public async Task<IActionResult> GetListGift()
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            //if (LoginData.JENIS_USER != "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "User");
            //}

            var GiftList = (from a in await db.tb_Gift.ToListAsync()
                           join b in db.msUnitKerja on a.KD_UNITKER equals b.KD_UNITKER
                           select new
                           {
                               ID_GIFT = a.ID_GIFT,
                               NIK = a.NIK,
                               NM_NAS = a.NM_NAS,
                               CABANG = b.NM_UNITKER,
                               TUJUAN_GIFT = a.TUJUAN_GIFT,
                               TGL_GIFT = a.TGL_GIFT,
                               BIAYA = a.BIAYA
                           }).ToList();

            return Json(GiftList);
        }

        [HttpGet]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateGift()
        {
            TB_Gift _tbGift = new TB_Gift();

            //ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User" });
            //ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            
            var TujuanGift = new List<SelectListItem>
            {
                new SelectListItem { Value = "Ulang Tahun", Text = "Ulang Tahun" },
                new SelectListItem { Value = "Hari Raya Keagamaan", Text = "Hari Raya Keagamaan" },
                new SelectListItem { Value = "Pernikahan", Text = "Pernikahan" },
                new SelectListItem { Value = "Kedukaan", Text = "Kedukaan" },
                new SelectListItem { Value = "Peresmiaan Usaha Baru", Text = "Peresmiaan Usaha Baru" }
            };
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");
            ViewBag.ListTujuanGift = TujuanGift;

            return PartialView("_CreateGift", _tbGift);
        }

        [HttpPost]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateGift(TB_Gift _tbGift)
        {
            if (ModelState.IsValid)
            {
                db.tb_Gift.Add(_tbGift);
                db.SaveChanges();

                return Content("0");
            }

            return PartialView("_CreateGift");
        }

        [HttpPost]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult DeleteGift(int ID_GIFT)
        {
            try
            {
                TB_Gift dataGift = db.tb_Gift.SingleOrDefault(m => m.ID_GIFT == ID_GIFT);
                db.tb_Gift.Remove(dataGift);
                db.SaveChanges();

                return Content("0");
            }
            catch
            {
                return Content("9");
            }
        }

        [HttpGet]
        //[AuthorizeAccess(Level = "Admin")]
        public async Task<IActionResult> EditGift(int? ID_GIFT)
        {
            var _tbGift = await db.tb_Gift.SingleOrDefaultAsync(m => m.ID_GIFT == ID_GIFT);

            //ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User" });
            //ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            var TujuanGift = new List<SelectListItem>
            {
                new SelectListItem { Value = "Ulang Tahun", Text = "Ulang Tahun" },
                new SelectListItem { Value = "Hari Raya Keagamaan", Text = "Hari Raya Keagamaan" },
                new SelectListItem { Value = "Pernikahan", Text = "Pernikahan" },
                new SelectListItem { Value = "Kedukaan", Text = "Kedukaan" },
                new SelectListItem { Value = "Peresmiaan Usaha Baru", Text = "Peresmiaan Usaha Baru" }
            };
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");
            ViewBag.ListTujuanGift = TujuanGift;

            return PartialView("_EditGift", _tbGift);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult EditGift(TB_Gift _tbGift)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            //if (LoginData.JENIS_USER != "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "User");
            //}

            //ModelState.Remove("USER_NAME");
            if (ModelState.IsValid)
            {
                try
                {
                    db.tb_Gift.Update(_tbGift);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("9");
                }
                return Content("0");
            }
            return View(_tbGift);
        }

        // Airport Lounge
        public IActionResult AirportLoungeHome()
        {
            ViewBag.Header = "Fasilitas dan Layanan";
            ViewBag.Title = "Airport Lounge";
            ViewBag.Keterangan = "Airport Lounge";

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

        public async Task<IActionResult> GetListAplounge()
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            //if (LoginData.JENIS_USER != "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "User");
            //}

            var AploungeList = (from a in await db.tb_Aplounge.ToListAsync()
                            join b in db.msUnitKerja on a.KD_UNITKER equals b.KD_UNITKER
                            select new
                            {
                                ID_APLOUNGE = a.ID_APLOUNGE,
                                NIK = a.NIK,
                                NM_NAS = a.NM_NAS,
                                CABANG = b.NM_UNITKER,
                                TGL_PENGGUNAAN = a.TGL_PENGGUNAAN,
                                JUMLAH_PAX = a.JUMLAH_PAX,
                                NM_AIRPORT = a.NM_AIRPORT,
                                BIAYA = a.BIAYA
                            }).ToList();

            return Json(AploungeList);
        }

        [HttpGet]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateAplounge()
        {
            TB_Aplounge _tbAplounge = new TB_Aplounge();

            //ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User" });
            //ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return PartialView("_CreateAplounge", _tbAplounge);
        }

        [HttpPost]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateAplounge(TB_Aplounge _tbAplounge)
        {
            if (ModelState.IsValid)
            {
                db.tb_Aplounge.Add(_tbAplounge);
                db.SaveChanges();

                return Content("0");
            }

            return PartialView("_CreateAplounge");
        }

        [HttpPost]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult DeleteAplounge(int ID_APLOUNGE)
        {
            try
            {
                TB_Aplounge dataAplounge = db.tb_Aplounge.SingleOrDefault(m => m.ID_APLOUNGE == ID_APLOUNGE);
                db.tb_Aplounge.Remove(dataAplounge);
                db.SaveChanges();

                return Content("0");
            }
            catch
            {
                return Content("9");
            }
        }

        [HttpGet]
        //[AuthorizeAccess(Level = "Admin")]
        public async Task<IActionResult> EditAplounge(int? ID_APLOUNGE)
        {
            var _tbAplounge = await db.tb_Aplounge.SingleOrDefaultAsync(m => m.ID_APLOUNGE == ID_APLOUNGE);

            //ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User" });
            //ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return PartialView("_EditAplounge", _tbAplounge);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeAccess(Level = "Admin")]
        public IActionResult EditAplounge(TB_Aplounge _tbAplounge)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            //if (LoginData.JENIS_USER != "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "User");
            //}

            //ModelState.Remove("USER_NAME");
            if (ModelState.IsValid)
            {
                try
                {
                    db.tb_Aplounge.Update(_tbAplounge);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("9");
                }
                return Content("0");
            }
            return View(_tbAplounge);
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

using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using CPM_Project.Authorize;
using CPM_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using ToolsEngine;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CPM_Project.Controllers
{
    public class UserController : Controller
    {
        private LoginData LoginData = new LoginData();
        private AppDBContext db = new AppDBContext();

        OtherTools Tools = new OtherTools();
        GlobalVariable _GlobalVariable = new();

        static int BlockLimit = 0;

        private static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^(?=[^\s]*?[0-9])(?=[^\s]*?[a-zA-Z])[a-zA-Z0-9\!@#$%^&*()]*$");
            return rg.IsMatch(strToCheck) == true ? true : false;
        }

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

        // -- LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Header = "SPPD Online";
            ViewBag.Title = "Login";

            if (HttpContext.Session.GetString("LoginData") != "")
            {
                GetGlobalVariable();

                // TNT LOG
                TNT_LOG(2, "LOGOUT");

                HttpContext.Session.SetString("LoginData", "");
                HttpContext.Session.SetString("Token", "");
            }

            return View();
        }

        [HttpGet]
        [Route("Login")]
        public PartialViewResult MenuLoginUser()
        {
            return PartialView("_MenuLoginUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidasiLogin(string CryptUSER_NAME, string CryptPASSWORD)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            IDataReader dr = null;

            LoginUser LoginUser = new LoginUser();

            try
            {
                LoginUser.USER_NAME = Tools.RSADecrypt(CryptUSER_NAME, HttpContext.Session.GetString("PrivateKey"));
                LoginUser.PASSWORD = Tools.RSADecrypt(CryptPASSWORD, HttpContext.Session.GetString("PrivateKey"));
            }
            catch (Exception ex)
            {
                ViewBag.Kode = "9";
                return PartialView("_MenuLoginUser", LoginUser);
            }

            try
            {
                con = new MySqlConnection(_GlobalVariable.ConnectionDB);
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandTimeout = 0;

                cmd.CommandText = "SELECT tbUsers.*, msUnitKerja.NM_UNITKER FROM tbUsers LEFT JOIN msUnitKerja ON tbUsers.KD_UNITKER = msUnitKerja.KD_UNITKER WHERE USER_NAME = @USER AND PASSWORD = @PASS";
                cmd.Parameters.AddWithValue("@USER", LoginUser.USER_NAME);
                cmd.Parameters.AddWithValue("@PASS", Tools.SHA512String(LoginUser.PASSWORD).ToLower());

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // BLOCK 
                    tbUsers User = db.tbUsers.SingleOrDefault(x => x.USER_NAME == LoginUser.USER_NAME);

                    if (User.JAM_BLOCK.Date > DateTime.Now.Date)
                    {
                        User.STS_BLOCK = 0;
                        //db.Entry(User).State = EntityState.Modified;
                        db.tbUsers.Update(User);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (User.JAM_BLOCK.Date == DateTime.Now.Date)
                        {
                            if (DateTime.Now >= User.JAM_BLOCK)
                            {
                                User.STS_BLOCK = 0;
                                //db.Entry(User).State = EntityState.Modified;
                                db.tbUsers.Update(User);
                                db.SaveChanges();
                            }
                            else
                            {
                                //ViewBag.Kode = "6";

                                return Content("6"); //PartialView("_MenuLoginUser", LoginUser);
                            }
                        }
                    }

                    // SUKSES
                    BlockLimit = 0;

                    LoginData.ID_USER = Convert.ToInt32(dr["ID_USER"].ToString());

                    LoginData.USER_NAME = dr["USER_NAME"].ToString();
                    LoginData.NAMA_LENGKAP = dr["NAMA_LENGKAP"].ToString();
                    LoginData.PASSWORD = dr["PASSWORD"].ToString();

                    //LoginData.NM_TEMPAT = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["NM_TEMPAT"].ToString().ToLower()); //dr["NM_SKPD"].ToString();

                    LoginData.JENIS_USER = dr["JENIS_USER"].ToString();
                    LoginData.GRUP_USER = dr["GRUP_USER"].ToString();
                    LoginData.AKTIF = (bool)dr["AKTIF"];

                    LoginData.KD_UNITKER = dr["KD_UNITKER"].ToString();
                    LoginData.NM_UNITKER = dr["NM_UNITKER"].ToString();

                    // LAST LOGIN
                    try
                    {
                        tbUserLog LAST_LOGIN = db.tbUserLog.Where(x => x.NAMA_USER == LoginUser.USER_NAME && x.STATUS == 1).OrderByDescending(x => x.TANGGAL_JAM).Take(1).SingleOrDefault();
                        LoginData.LAST_LOGIN = LAST_LOGIN.TANGGAL_JAM;
                    }
                    catch (Exception ex)
                    {
                        LoginData.LAST_LOGIN = DateTime.Now;
                    }

                    // CHECK USER STATUS
                    if (LoginData.AKTIF == false)
                    {
                        //ViewBag.Kode = "8";

                        return Content("9"); //PartialView("_MenuLoginUser", LoginUser);
                    }

                    // SAVE TO SESSION
                    HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(LoginData));

                    // TNT LOG
                    TNT_LOG(1, "LOGIN");

                    // PASSWORD STANDART
                    if (LoginUser.PASSWORD == "12345")
                    {
                        return Content("7");
                    }

                    if (LoginData.JENIS_USER == "User" && LoginData.GRUP_USER == "-")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("Index", "Dashboard"));
                    }
                    else if (LoginData.JENIS_USER == "Supervisor" && LoginData.GRUP_USER == "-")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("Index", "Dashboard"));
                    }
                    else if (LoginData.JENIS_USER == "Admin" && LoginData.GRUP_USER == "-")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("UserHome", "User"));
                    }
                    else if (LoginData.JENIS_USER == "User" && LoginData.GRUP_USER == "BPDP")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("DashboardBPDP", "Dashboard"));
                    }
                    else if (LoginData.JENIS_USER == "Kanpus" && LoginData.GRUP_USER == "BPDP")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("DashboardBPDP", "Dashboard"));
                    }
                    else if (LoginData.JENIS_USER == "User" && LoginData.GRUP_USER == "SRAK")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("Index", "Dashboard"));
                    }
                    else if (LoginData.JENIS_USER == "Kanpus" && LoginData.GRUP_USER == "SRAK")
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        //ViewBag.Kode = Url.Action("Index", "Dashboard");
                        return Content(Url.Action("Index", "Dashboard"));
                    }
                    else
                    {
                        dr.Close();
                        cmd.Dispose();
                        con.Close();

                        return Content("0");
                    }
                }
                else
                {
                    if (BlockLimit != 2)
                    {
                        BlockLimit++;
                    }
                    else
                    {
                        tbUsers User = db.tbUsers.SingleOrDefault(x => x.USER_NAME == LoginUser.USER_NAME);

                        User.STS_BLOCK = 1;
                        User.JAM_BLOCK = DateTime.Now.AddMinutes(5);

                        //db.Entry(User).State = EntityState.Modified;
                        db.tbUsers.Update(User);
                        db.SaveChanges();

                        BlockLimit = 0;

                        dr.Close();
                        con.Close();

                        //ViewBag.Kode = "9";
                        return Content("9"); //PartialView("_MenuLoginUser", LoginUser);
                    }

                    dr.Close();
                    con.Close();

                    //ViewBag.Kode = "9";
                    return Content("9"); //PartialView("_MenuLoginUser", LoginUser);
                }
            }
            catch (Exception ex)
            {
                con.Close();

                //ViewBag.Kode = "9";

                return Content("9"); //PartialView("_MenuLoginUser", LoginUser);
            }

            //ViewBag.Kode = "10";
            //return PartialView("_LoginUser", LoginUser);
        }
        // -- END LOGIN

        // -- GANTI PASSWORD
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GantiPassword()
        {
            ViewBag.Title = "Ganti Password";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidasiPassword(string OldPassword, string NewPassword, string KonfirmasiPassword)
        {
            try
            {
                GetGlobalVariable();

                OldPassword = Tools.RSADecrypt(OldPassword, HttpContext.Session.GetString("PrivateKey"));
                NewPassword = Tools.RSADecrypt(NewPassword, HttpContext.Session.GetString("PrivateKey"));
                KonfirmasiPassword = Tools.RSADecrypt(KonfirmasiPassword, HttpContext.Session.GetString("PrivateKey"));

                if (LoginData == null)
                {
                    return Content("Anda Sehat..????");
                }

                if (NewPassword.Length < 6)
                {
                    return Content("4"); //password kurang dari 6 karakter
                }

                if (isAlphaNumeric(NewPassword) && (NewPassword != "banksulselbar"))
                {
                    if (NewPassword != KonfirmasiPassword)
                    {
                        return Content("8"); // Konfirmasi password tidak sesuai dengan password baru
                    }

                    string TmpOldPassword = Tools.SHA512String(OldPassword).ToLower();

                    // UPDATE KE DATABASE
                    tbUsers User = db.tbUsers.Find(LoginData.ID_USER);

                    if (TmpOldPassword != User.PASSWORD)
                    {
                        return Content("7"); // Password lama tidak sesuai dengan database
                    }
                    else
                    {
                        if (OldPassword == NewPassword)
                        {
                            return Content("6"); // password lama sama dengan password baru
                        }
                    }

                    User.PASSWORD = NewPassword;
                    //db.Entry(User).State = EntityState.Modified;
                    db.tbUsers.Update(User);
                    db.SaveChanges();

                    // TNT LOG
                    TNT_LOG(7, "CHANGE PASSWORD : " + JsonConvert.SerializeObject(User));

                    return Content("0");
                }
                else
                {
                    return Content("5"); // Password harus berisi huruf & angka
                }
            }
            catch
            {
                return Content("9");
            }
        }
        // -- END GANTI PASSWORD

        // UPLOAD FOTO PROFILE
        [HttpPost]
        public async Task<string> UploadImage(IFormFile FILE_IMAGE)
        {
            var ImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp" };

            if (FILE_IMAGE != null)
            {
                var ext = Path.GetExtension(FILE_IMAGE.FileName).ToLower();

                if (ImageExtensions.Contains(ext))
                {
                    GetGlobalVariable();
                    
                    if (LoginData == null)
                    {
                        return "Anda Sehat..????";
                    }

                    var fileName = Path.GetFileName(FILE_IMAGE.FileName);
                    var path = Path.Combine(_GlobalVariable.WebRoot, "Images"); 

                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await FILE_IMAGE.CopyToAsync(stream);
                    }

                    string filename_x = LoginData.ID_USER + ".bmp";

                    // RESIZE IMAGE
                    try
                    {
                        Bitmap objImage = new Bitmap(Path.Combine(path, fileName));
                        Bitmap objBitmap = new Bitmap(objImage, new Size(76, 76));

                        System.IO.File.Delete(Path.Combine(path, filename_x));

                        objBitmap.Save(Path.Combine(path, filename_x));

                        objBitmap.Dispose();
                        objImage.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //
                    }

                    System.IO.File.Delete(Path.Combine(path, fileName));

                    return @"\Images\" + filename_x;
                }
                else
                {
                    return "ERR";
                }
            }
            else
            {
                return "ERR";
            }
        }

        // Profile
        [HttpGet]
        public IActionResult GetBase64Image()
        {
            GetGlobalVariable();
            
            if (LoginData == null)
            {
                return Content("Anda Sehat..????");
            }

            byte[] data;

            try
            {
                string fileName = LoginData.ID_USER.ToString() + ".bmp";
                var ImagePath = Path.Combine(_GlobalVariable.WebRoot, "images"); // _hostingEnvironment.WebRootPath + "\\Images\\";
                string path = Path.Combine(ImagePath, fileName);

                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                data = new byte[(int)fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                fileStream.Dispose();
            }
            catch
            {
                string fileName = "default3.png"; //"default.bmp";
                var ImagePath = Path.Combine(_GlobalVariable.WebRoot, "images"); //_hostingEnvironment.WebRootPath + "\\Images\\";
                string path = Path.Combine(ImagePath, fileName);

                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                data = new byte[(int)fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                fileStream.Dispose();
            }

            return Json(new { base64imgage = Convert.ToBase64String(data) });
        }

        //// -- UPLOAD EXCEL FILE
        //[HttpPost]
        //[DisableRequestSizeLimit]
        //[RequestSizeLimit(100000000)]
        //public string UploadFile(IFormFile FILE)
        //{
        //    GetGlobalVariable();

        //    var DocExtensions = new[] { ".xls", ".xlsx" };

        //    if (FILE != null)
        //    {
        //        var ext = Path.GetExtension(FILE.FileName).ToLower();

        //        if (DocExtensions.Contains(ext))
        //        {
        //            //var fileName = Path.GetFileName(FILE.FileName);
        //            //var path = Server.MapPath("~/FileUpload");

        //            var fileName = Path.GetFileName(FILE.FileName);
        //            var path = Path.Combine(Startup.ContentRoot, "file_upload");

        //            // -- SAVE FILE
        //            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        //            {
        //                FILE.CopyToAsync(stream);
        //            }

        //            //using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        //            //{
        //            //    await f.CopyToAsync(stream);
        //            //    result.Add(f.FileName);
        //            //}

        //            DataSet ds = new DataSet();
        //            List<MS_Pegawai> TMP_Pegawai = new List<MS_Pegawai>();

        //            try
        //            {
        //                //var fileName = Path.GetFileName("DaftarPegawai.xlsx");
        //                //var path = Path.Combine(_hostingEnvironment.WebRootPath, "file_upload");

        //                string LokasiFile = Path.Combine(path, fileName);

        //                using (var stream = new FileStream(LokasiFile, FileMode.Open, FileAccess.Read))
        //                {

        //                    // Auto-detect format, supports:
        //                    //  - Binary Excel files (2.0-2003 format; *.xls)
        //                    //  - OpenXml Excel files (2007 format; *.xlsx)
        //                    using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                    {
        //                        ds = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                        {
        //                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                            {
        //                                UseHeaderRow = true
        //                            }
        //                        });

        //                        reader.Close();
        //                    }

        //                    stream.Close();
        //                }

        //                var dataTable = ds.Tables[0];

        //                for (var i = 0; i < dataTable.Rows.Count; i++)
        //                {
        //                    try
        //                    {
        //                        TMP_Pegawai.Add(new MS_Pegawai
        //                        {
        //                            NPP = Convert.ToString(dataTable.Rows[i][1]),
        //                            NAMA_PEGAWAI = (string)dataTable.Rows[i][2],
        //                            JG = Convert.ToString(dataTable.Rows[i][3]),
        //                            NAMA_JABATAN = (string)dataTable.Rows[i][4],
        //                            UNIT_KERJA = (string)dataTable.Rows[i][5],
        //                            NO_REK = (string)dataTable.Rows[i][6]
        //                        });
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                        TMP_Pegawai.Add(new MS_Pegawai
        //                        {
        //                            NPP = Convert.ToString(dataTable.Rows[i][1]),
        //                            NAMA_PEGAWAI = (string)dataTable.Rows[i][2],
        //                            JG = Convert.ToString(dataTable.Rows[i][3]),
        //                            NAMA_JABATAN = (string)dataTable.Rows[i][4],
        //                            UNIT_KERJA = (string)dataTable.Rows[i][5]
        //                        });
        //                    }
        //                }

        //                var DataRemove = (from d in db.MS_Pegawai select d);
        //                db.MS_Pegawai.RemoveRange(DataRemove);
        //                db.SaveChanges();

        //                db.MS_Pegawai.AddRange(TMP_Pegawai);
        //                db.SaveChanges();

        //                return "0";
        //            }
        //            catch (Exception ex)
        //            {
        //                return ex.Message; //"9";
        //            }
        //        }
        //        else
        //        {
        //            return "8";
        //        }
        //    }
        //    else
        //    {
        //        return "File Yang Di Upload Tidak Ada";
        //    }
        //}


        //[HttpGet]
        //public string TestingReadEXCEL()
        //{
        //    DataSet ds = new DataSet();
        //    List<MS_Pegawai> TMP_Pegawai = new List<MS_Pegawai>();

        //    try
        //    {
        //        var fileName = Path.GetFileName("DaftarPegawai.xlsx");
        //        var path = Path.Combine(_hostingEnvironment.WebRootPath, "file_upload");

        //        string LokasiFile = Path.Combine(path, fileName);

        //        using (var stream = new FileStream(LokasiFile, FileMode.Open, FileAccess.Read))
        //        {

        //            // Auto-detect format, supports:
        //            //  - Binary Excel files (2.0-2003 format; *.xls)
        //            //  - OpenXml Excel files (2007 format; *.xlsx)
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                {
        //                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                    {
        //                        UseHeaderRow = true
        //                    }
        //                });

        //                reader.Close();
        //            }

        //            stream.Close();
        //        }

        //        var dataTable = ds.Tables[0];

        //        for (var i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            try
        //            {
        //                TMP_Pegawai.Add(new MS_Pegawai
        //                {
        //                    NPP = Convert.ToString(dataTable.Rows[i][1]),
        //                    NAMA_PEGAWAI = (string)dataTable.Rows[i][2],
        //                    JG = Convert.ToString(dataTable.Rows[i][3]),
        //                    NAMA_JABATAN = (string)dataTable.Rows[i][4],
        //                    UNIT_KERJA = (string)dataTable.Rows[i][5],
        //                    NO_REK = (string)dataTable.Rows[i][6]
        //                });
        //            }
        //            catch
        //            {

        //                TMP_Pegawai.Add(new MS_Pegawai
        //                {
        //                    NPP = Convert.ToString(dataTable.Rows[i][1]),
        //                    NAMA_PEGAWAI = (string)dataTable.Rows[i][2],
        //                    JG = Convert.ToString(dataTable.Rows[i][3]),
        //                    NAMA_JABATAN = (string)dataTable.Rows[i][4],
        //                    UNIT_KERJA = (string)dataTable.Rows[i][5]
        //                });
        //            }
        //        }

        //        var DataRemove = (from d in db.MS_Pegawai select d);
        //        db.MS_Pegawai.RemoveRange(DataRemove);
        //        db.SaveChanges();

        //        db.MS_Pegawai.AddRange(TMP_Pegawai);
        //        db.SaveChanges();

        //        return JsonConvert.SerializeObject(TMP_Pegawai);
        //    }
        //    catch (Exception ex)
        //    {
        //        return "9";
        //    }
        //}


        [HttpGet]
        public IActionResult ErrorPage()
        {
            return View();
        }

        [HttpGet]
        public string KeyGenerator()
        {
            var RSAKey = Tools.RSAKeyGeneratorCore().Split('|');

            var PublicKey = RSAKey[0];
            var PrivateKey = RSAKey[1];

            HttpContext.Session.SetString("PrivateKey", PrivateKey);
            return PublicKey;
        }

        // 1. Login
        // 2. Logout
        // 3. Transaksi
        // 4. Approval
        // 5. Proses Transaksi
        // 6. Creater
        // 7. Change Password
        // 8. Delete
        // 9. Edit
        public void TNT_LOG(int STATUS, string KET)
        {
            GetGlobalVariable();

            // LOGIN LOG
            if (LoginData != null)
            {
                tbUserLog tbUserLog = new tbUserLog();
                tbUserLog.NAMA_USER = LoginData.USER_NAME;
                tbUserLog.TANGGAL_JAM = DateTime.Now;
                tbUserLog.IP_ADDRESS = "--";
                tbUserLog.STATUS = STATUS;
                tbUserLog.KET = KET;

                db.tbUserLog.Add(tbUserLog);
                db.SaveChanges();
            }
        }

        // -- GANTI PASSWORD USER
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GantiPasswordUser()
        {
            ViewBag.Title = "Ganti Password";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        // -- MANAJEMEN USER HOME
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult UserHome()
        {
            ViewBag.Header = "Manajemen User";
            ViewBag.Title = "Users";
            ViewBag.Keterangan = "Pembuatan master data untuk user / pengguna.";

            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (LoginData.JENIS_USER != "Admin")
            {
                return RedirectToAction("ErrorPage", "User");
            }

            ViewBag.NAMA_LENGKAP = LoginData.NAMA_LENGKAP;
            ViewBag.NM_UNITKER = LoginData.NM_UNITKER;
            ViewBag.LAST_LOGIN = LoginData.LAST_LOGIN.ToString("dd-MM-yyyy HH:mm:dd");

            return View();
        }
        // -- END MANAJEMEN USER HOME

        // -- MANAJEMEN USER
        [AuthorizeAccess(Level = "Admin")]
        public async Task<IActionResult> GetListUser()
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (LoginData.JENIS_USER != "Admin")
            {
                return RedirectToAction("ErrorPage", "User");
            }
            
            var UserList =  (from u in await db.tbUsers.ToListAsync()
                            join mk in db.msUnitKerja on u.KD_UNITKER equals mk.KD_UNITKER
                            select new
                            {
                                ID_USER = u.ID_USER,
                                USER_NAME = u.USER_NAME,
                                NAMA_LENGKAP = u.NAMA_LENGKAP,
                                JENIS_USER = u.JENIS_USER,
                                GRUP_USER = u.GRUP_USER,
                                KD_UNITKER = mk.KD_UNITKER,
                                NM_UNITKER = mk.NM_UNITKER,
                                AKTIF = u.AKTIF
                            }).ToList();

            return Json(UserList);
        }

        [HttpGet]
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateUser()
        {
            tbUsers _tbUsers = new tbUsers();

            ViewBag.ListJenisUser = new SelectList(new string[] { "Admin", "Kanpus", "User"});
            ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return PartialView("_CreateUser", _tbUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult CreateUser(tbUsers _tbUsers)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (LoginData.JENIS_USER != "Admin")
            {
                return RedirectToAction("ErrorPage", "User");
            }
            
            _tbUsers.AKTIF = true;
            if (ModelState.IsValid)
            {
                db.tbUsers.Add(_tbUsers);
                db.SaveChanges();

                return Content("0");
            }
            return PartialView("_CreateUser");
        }

        [HttpGet]
        [AuthorizeAccess(Level = "Admin")]
        public async Task<IActionResult> EditUser(int? ID_USER)
        {
            var _tbUsers = await db.tbUsers.SingleOrDefaultAsync(m => m.ID_USER == ID_USER);

            ViewBag.ListJenisUser = new SelectList(new string[] {  "Admin", "Kanpus", "User" });
            ViewBag.ListGrupUser = new SelectList(new string[] { "SRAK" });
            ViewBag.ListUnitKerja = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");

            return PartialView("_EditUser", _tbUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult EditUser(tbUsers _tbUsers)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (LoginData.JENIS_USER != "Admin")
            {
                return RedirectToAction("ErrorPage", "User");
            }
            
            ModelState.Remove("USER_NAME");
            if (ModelState.IsValid)
            {
                try
                {
                    db.tbUsers.Update(_tbUsers);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("9");
                }
                return Content("0");
            }
            return View(_tbUsers);
        }

        [HttpPost]
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult DeleteUser(int ID_USER)
        {
            try
            {
                tbUsers dataUsers = db.tbUsers.SingleOrDefault(m => m.ID_USER == ID_USER);
                db.tbUsers.Remove(dataUsers);
                db.SaveChanges();

                return Content("0");
            }
            catch
            {
                return Content("9");
            }
        }

        [HttpPost]
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult IsCreateUserExists(string USER_NAME)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (LoginData.JENIS_USER != "Admin")
            {
                return RedirectToAction("ErrorPage", "User");
            }
            
            return Json(!db.tbUsers.Any(x => x.USER_NAME == USER_NAME));
        }

        [HttpPost]
        [AuthorizeAccess(Level = "Admin")]
        public IActionResult ResetPasswordUser(int ID_USER)
        {
            GetGlobalVariable();

            if (LoginData == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (LoginData.JENIS_USER != "Admin")
            {
                return RedirectToAction("ErrorPage", "User");
            }
            
            var _tbUsers = db.tbUsers.SingleOrDefault(x => x.ID_USER == ID_USER);
            _tbUsers.PASSWORD = "12345";

            try
            {
                db.tbUsers.Update(_tbUsers);
                db.SaveChanges();

                return Content("0");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Content("9");
            }
        }
        // -- END MANAJEMEN USER
    }
}

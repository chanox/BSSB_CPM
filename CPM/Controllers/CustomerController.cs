using CPM_Project.Models;
using CPM_Project.Models.CPM;
using CPM_Project.Models.SRAK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ToolsEngine;

namespace CPM_Project.Controllers;

public class CustomerController : Controller
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
    
    public IActionResult CustomerHome()
    {
        ViewBag.Header = "Data Customer";
        ViewBag.Title = "Data Customer";
        ViewBag.Keterangan = "Infomrasi seluruh customer priority.";

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
    
    public resInfoCPM GetDataCustomer(string NIK)
    {
        GetGlobalVariable();

        resInfoCPM resultData = new resInfoCPM();

        try
        {
            if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "RAS" })
            {
                var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={NIK}")).Result;
                resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());
            }

            return resultData;
        }
        catch
        {
            return resultData;
        }
    }
    
    public IActionResult GetDataCustomerView(string NIK)
    {
        GetGlobalVariable();

        resInfoCPM resultData = new resInfoCPM();

        try
        {
            if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
            {
                var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={NIK}")).Result;
                resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());
            }
            
            ViewBag.listRM = new SelectList(db.Set<TB_Master_RM>(), "KODE_RM", "NAMA_RM");
            ViewBag.listKantor = new SelectList(db.Set<msUnitKerja>(), "KD_UNITKER", "NM_UNITKER");
            ViewBag.listKelompok = new SelectList(new string[] { "Umum", "Khusus"});
            ViewBag.listTier = new SelectList(new string[] { "Tier 1", "Tier 2", "Tier 3" });
            
            return PartialView("_ViewDataCustomer", resultData.INFO_NASABAH);
        }
        catch (Exception ex)
        {
            return Content($"#Exception Errot: " + ex.Message);
        }
    }
    
    public resInfoCPM GetDataCustomerSaldo(string NIK)
    {
        GetGlobalVariable();

        resInfoCPM resultData = new resInfoCPM();

        try
        {
            if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "SRAK" })
            {
                var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={NIK}")).Result;
                resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());
            }

            return resultData;  //Content($"{string.Format("{0:N0}", resultData.TOTAL_TABUNGAN)}#{string.Format("{0:N0}", resultData.TOTAL_PINJAMAN)}$");
        }
        catch (Exception ex)
        {
            return resultData; //Content($"#Exception Errot: " + ex.Message);
        }
    }
    
    public List<TB_Customer> GetCustomerList()
    {
        GetGlobalVariable();

        var resultData = new List<TB_Customer>();

        try
        {
            // if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "RAS" })
            // {
            //     //var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={NIK}")).Result;
            //     //resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());
            //
            //     resultData = db.TB_CUSTOMERS.ToList();
            // }

            resultData = db.TB_CUSTOMERS.ToList();
            return resultData;
        }
        catch (Exception ex)
        {
            return resultData;
        }
    }
    

    [HttpPost]
    public string SimpanCustomer(TB_Customer data)
    {
        GetGlobalVariable();

        try
        {
            var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={data.NO_IDENT}")).Result;
            var resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());

            var dataCustomer = new TB_Customer()
            {
                RT = resultData.INFO_NASABAH.RT,
                RW = resultData.INFO_NASABAH.RW,
                NEG = resultData.INFO_NASABAH.NEG,
                SID = data.SID,
                NO_HP = resultData.INFO_NASABAH.NO_HP,
                TIER = data.TIER,
                KD_POS = resultData.INFO_NASABAH.KD_POS,
                KD_VAL = resultData.INFO_NASABAH.KD_VAL,
                NM_NAS = resultData.INFO_NASABAH.NM_NAS,
                NO_KTP = resultData.INFO_NASABAH.NO_KTP,
                NO_NAS = resultData.INFO_NASABAH.NO_NAS,
                SALDO = resultData.INFO_NASABAH.SALDO,
                KODE_RM = data.KODE_RM,
                NEGARA = resultData.INFO_NASABAH.NEG,
                ALMT_NAS = resultData.INFO_NASABAH.ALMT_NAS,
                KD_AGAMA = resultData.INFO_NASABAH.KD_AGAMA,
                NM_ALIAS = resultData.INFO_NASABAH.NM_ALIAS,
                NO_IDENT = resultData.INFO_NASABAH.NO_IDENT,
                SALDORP = resultData.INFO_NASABAH.SALDORP,
                SLD_RATA = resultData.INFO_NASABAH.SLD_RATA,
                KD_JNS_KEL = resultData.INFO_NASABAH.KD_JNS_KEL,
                KD_WRG_NEG = resultData.INFO_NASABAH.KD_WRG_NEG,
                NO_TLP_NAS = resultData.INFO_NASABAH.NO_TLP_NAS,
                PROPINSI = resultData.INFO_NASABAH.PROPINSI,
                REKENING = resultData.INFO_NASABAH.REKENING,
                TGL_LAHIR = resultData.INFO_NASABAH.TGL_LAHIR,
                KABUPATEN = resultData.INFO_NASABAH.KABUPATEN,
                KD_UNITKER = data.KD_UNITKER,
                KDKNTRCIS = resultData.INFO_NASABAH.KDKNTRCIS,
                KECAMATAN = resultData.INFO_NASABAH.KECAMATAN,
                KELURAHAN = resultData.INFO_NASABAH.KELURAHAN,
                KET_STS_NAS = resultData.INFO_NASABAH.KET_STS_NAS,
                NM_LENGKAP = resultData.INFO_NASABAH.NM_LENGKAP,
                NMKNTRCIS = resultData.INFO_NASABAH.NMKNTRCIS,
                NMKNTRREK = resultData.INFO_NASABAH.NMKNTRREK,
                TMPT_LAHIR = resultData.INFO_NASABAH.TMPT_LAHIR,
                KETERANGAN = resultData.INFO_NASABAH.KETERANGAN,
                NM_GADIS_IBU = resultData.INFO_NASABAH.NM_GADIS_IBU,
                NO_CUSTOMER = resultData.INFO_NASABAH.NO_IDENT,
                KDKNTRINDUKCIS = resultData.INFO_NASABAH.KDKNTRINDUKCIS,
                NMKNTRINDUKCIS = resultData.INFO_NASABAH.NMKNTRINDUKCIS,
                NMKNTRINDUKREK = resultData.INFO_NASABAH.NMKNTRINDUKREK,
                KELOMPOK_NASABAH = data.KELOMPOK_NASABAH
            };
            
            db.TB_CUSTOMERS.Add(dataCustomer);
            db.SaveChanges();

            return "0";
        }
        catch (Exception ex)
        {
            return "#" + ex.Message;
        }
    }
    public IActionResult PengusulanHome()
    {
        ViewBag.Header = "Pengusulan Nasabah Priority";
        ViewBag.Title = "Pengusulan Nasabah Priority";
        ViewBag.Keterangan = "Mengusulkan nasabah yang akan di masukan sebagai nasabah priority.";

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

    public List<TB_Aum> GetDataBySID(string SID)
    {
        /* GetGlobalVariable();

         var resultData = new List<TB_Aum>();

         try
         {
             if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "RAS" })
             {
                 //var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={NIK}")).Result;
                 //resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());

                 resultData = db.tb_aum.ToList();
             }

             return Json(resultData);
         }
         catch (Exception ex)
         {
             return Json(resultData);
         }*/
        //var SID = "IDD2201HO440825";
        var listData = db.tb_aum.Where(a => a.SID == SID).ToList();
        return listData;
    }
}
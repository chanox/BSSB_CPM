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
            if (LoginData is { JENIS_USER: "Kanpus", GRUP_USER: "RAS" })
            {
                //var tmp = Task.Run(() => JSONTools.GetJSON(_GlobalVariable.ServiceAddress + $"/CPM/GetInfoNasabah?NIK={NIK}")).Result;
                //resultData = JsonConvert.DeserializeObject<resInfoCPM>(tmp.ToString());

                resultData = db.TB_CUSTOMERS.ToList();
            }

            return resultData;
        }
        catch (Exception ex)
        {
            return resultData;
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
}
using System.Collections.Generic;

namespace CPM_Project.Models.CPM;

public class resInfoCPM
{
    public resInfoNasabah INFO_NASABAH { get; set; }
    public decimal TOTAL_TABUNGAN { get; set; }
    public decimal TOTAL_PINJAMAN { get; set; }
    public List<SLD_LIST> LIST_SALDO { get; set; }
}

public class SLD_LIST
{
    public decimal SALDO { get; set; }
    public decimal SALDORP { get; set; }
    public decimal SLD_RATA { get; set; }
    public string JENISPRODUK { get; set; }
    public string PRODUK { get; set; }
}
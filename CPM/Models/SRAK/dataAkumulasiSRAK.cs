using System;

namespace CPM_Project.Models.SRAK;

public class dataAkumulasiSRAK
{
    public int ID_SRAK { get; set; }
    public DateTime? TGL_DATA { get; set; }
    public string KD_KNTR { get; set; }
    public string KD_PRODUK { get; set; }
    public string KD_SUB_PRODUK { get; set; }
    public string KD_VAL { get; set; }
    public string KD_JNS_PINJ { get; set; }
    public decimal TOT_BUNGA { get; set; }
    public decimal BUNGA_SRAK { get; set; }
    public decimal TOT_NOMINAL_SRAK { get; set; }
    public string KETERANGAN { get; set; }
    public string PRODUK { get; set; }
    public string NMKANTOR { get; set; }
    public string KD_KNTR_INDUK { get; set; }
    public string KDKNTR { get; set; }
    public string PRODUK_CABANG { get; set; }
    public decimal TOT_NOMINAL { get; set; }
}
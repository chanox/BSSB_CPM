using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.RAS;

[Table("tb_ras_resiko_kredit_detail")]
public class TB_RAS_Resiko_Kredit_Detail
{
    [Key]
    public int ID_RAS_RESIKO_KREDIT_DETAIL { get; set; }
    public DateTime? TANGGAL { get; set; }
    public int BULAN { get; set; }
    public int TAHUN { get; set; }
    public string RISK_METRIC { get; set; }
    public string RISK_APP_1 { get; set; }
    public string RISK_APP_2 { get; set; }
    public string RISK_TOL_1 { get; set; }
    public string RISK_TOL_2 { get; set; }
    public decimal PERSEN { get; set; }
    public decimal TOTAL_KREDIT { get; set; }
    public decimal TOTAL_PEMBIAYAAN { get; set; }
    public decimal NPL_GROSS_KONV { get; set; }
    public decimal NPL_NET_KONV { get; set; }
    public decimal NPF_SYR { get; set; }
    public decimal CISLNBSR_TERKAIT { get; set; }
    public decimal CISLNBSR_TIDAK_TERKAIT { get; set; }
    public decimal LN50BSR_TERKAIT { get; set; }
    public decimal LN50BSR_TIDAK_TERKAIT { get; set; }
    public decimal TOTAL_LAR_KONV { get; set; }
    public decimal TOTAL_LAR_SYR { get; set; }
    public decimal LAR_KONSOLIDASI { get; set; }
    public decimal MODAL_INTI { get; set; }
    public decimal MODAL_TAMBAH { get; set; }
    public decimal TOTAL_MODAL { get; set; }
}
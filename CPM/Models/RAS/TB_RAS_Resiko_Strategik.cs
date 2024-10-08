using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.RAS;

[Table("tb_ras_resiko_strategik")]
public class TB_RAS_Resiko_Strategik
{
    [Key]
    public int ID_RAS_RESIKO_STRATEGIK { get; set; }
    public DateTime? TANGGAL { get; set; }
    public int BULAN { get; set; }
    public int TAHUN { get; set; }
    public string RISK_METRIC { get; set; }
    public string RISK_APP_1 { get; set; }
    public string RISK_APP_2 { get; set; }
    public string RISK_TOL_1 { get; set; }
    public string RISK_TOL_2 { get; set; }
    public decimal M01 { get; set; }
    public decimal M01_MODAL_INTI { get; set; }
    public decimal M01_MODAL_TAMBAH { get; set; }
    public decimal M01_TOTAL_MODAL { get; set; }
    public decimal M02 { get; set; }
    public decimal M02_MODAL_INTI { get; set; }
    public decimal M02_MODAL_TAMBAH { get; set; }
    public decimal M02_TOTAL_MODAL { get; set; }
    public decimal M03 { get; set; }
    public decimal M03_MODAL_INTI { get; set; }
    public decimal M03_MODAL_TAMBAH { get; set; }
    public decimal M03_TOTAL_MODAL { get; set; }
    public decimal M04 { get; set; }
    public decimal M04_MODAL_INTI { get; set; }
    public decimal M04_MODAL_TAMBAH { get; set; }
    public decimal M04_TOTAL_MODAL { get; set; }
    public decimal M05 { get; set; }
    public decimal M05_MODAL_INTI { get; set; }
    public decimal M05_MODAL_TAMBAH { get; set; }
    public decimal M05_TOTAL_MODAL { get; set; }
    public decimal M06 { get; set; }
    public decimal M06_MODAL_INTI { get; set; }
    public decimal M06_MODAL_TAMBAH { get; set; }
    public decimal M06_TOTAL_MODAL { get; set; }
    public decimal M07 { get; set; }
    public decimal M07_MODAL_INTI { get; set; }
    public decimal M07_MODAL_TAMBAH { get; set; }
    public decimal M07_TOTAL_MODAL { get; set; }
    public decimal M08 { get; set; }
    public decimal M08_MODAL_INTI { get; set; }
    public decimal M08_MODAL_TAMBAH { get; set; }
    public decimal M08_TOTAL_MODAL { get; set; }
    public decimal M09 { get; set; }
    public decimal M09_MODAL_INTI { get; set; }
    public decimal M09_MODAL_TAMBAH { get; set; }
    public decimal M09_TOTAL_MODAL { get; set; }
    public decimal M10 { get; set; }
    public decimal M10_MODAL_INTI { get; set; }
    public decimal M10_MODAL_TAMBAH { get; set; }
    public decimal M10_TOTAL_MODAL { get; set; }
    public decimal M11 { get; set; }
    public decimal M11_MODAL_INTI { get; set; }
    public decimal M11_MODAL_TAMBAH { get; set; }
    public decimal M11_TOTAL_MODAL { get; set; }
    public decimal M12 { get; set; }
    public decimal M12_MODAL_INTI { get; set; }
    public decimal M12_MODAL_TAMBAH { get; set; }
    public decimal M12_TOTAL_MODAL { get; set; }
}
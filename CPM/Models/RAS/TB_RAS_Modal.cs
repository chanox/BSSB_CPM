using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.RAS;

[Table("tb_ras_modal")]
public class TB_RAS_Modal
{
    [Key]
    public int ID_RAS_MODAL { get; set; }
    public DateTime? TANGGAL { get; set; }
    public int BULAN { get; set; }
    public int TAHUN { get; set; }
    public decimal MODAL_INTI { get; set; }
    public decimal MODAL_TAMBAH { get; set; }
    public decimal TOTAL_MODAL { get; set; }
}
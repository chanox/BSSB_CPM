using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.CPM;

[Table("tb_customer")]
public class TB_Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? ID_CUSTOMER { get; set; }
    public string? NO_CUSTOMER { get; set; }
    public string? NO_NAS { get; set; }
    public string? NM_NAS { get; set; }
    public string? NO_IDENT { get; set; }
    public string? NM_ALIAS { get; set; }
    public string? NO_KTP { get; set; }
    public string? NM_LENGKAP { get; set; }
    public int? KD_WRG_NEG { get; set; }
    public string? KD_JNS_KEL { get; set; }
    public string? KD_AGAMA { get; set; }
    public DateTime? TGL_LAHIR { get; set; }
    public string? NM_GADIS_IBU { get; set; }
    public string? TMPT_LAHIR { get; set; }
    public string? KETERANGAN { get; set; }
    public string? NO_HP { get; set; }
    public string? NO_TLP_NAS { get; set; }
    public string? ALMT_NAS { get; set; }
    public string? RT { get; set; }
    public string? RW { get; set; }
    public string? KELURAHAN { get; set; }
    public string? KECAMATAN { get; set; }
    public string? KABUPATEN { get; set; }
    public string? PROPINSI { get; set; }
    public string? KD_POS { get; set; }
    public string? NEGARA { get; set; }
    public string? NEG { get; set; }
    public string? KDKNTRCIS { get; set; }
    public string? NMKNTRCIS { get; set; }
    public string? KDKNTRINDUKCIS { get; set; }
    public string? NMKNTRINDUKCIS { get; set; }
    public string? REKENING { get; set; }
    public string? KD_VAL { get; set; }
    public decimal? SALDO { get; set; }
    public decimal? SALDORP { get; set; }
    public decimal? SLD_RATA { get; set; }
    public string? KET_STS_NAS { get; set; }
    public string? NMKNTRREK { get; set; }
    public string? NMKNTRINDUKREK { get; set; }
    
    public string? KODE_RM { get; set; }
    public string? KD_UNITKER { get; set; }
    public string? KELOMPOK_NASABAH { get; set; }
    public string? SID { get; set; }
    public string? TIER { get; set; }
}
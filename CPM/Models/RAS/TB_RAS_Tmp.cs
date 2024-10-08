using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CPM_Project.Models.RAS
{
    [Table("tb_ras_tmp")]
    public class TB_RAS_Tmp
    {
        [Key]
        public int ID_RAS_TMP { get; set; }
        public DateTime? TGL_DATA { get; set; }
        public DateTime? TGL_SALDO { get; set; }
        public string NO_NAS { get; set; }
        public string NM_NAS { get; set; }
        public string KD_KNTR { get; set; }
        public string KD_PRODUK { get; set; }
        public string KD_SUB_PRODUK { get; set; }
        public string NO_REK { get; set; }
        public string KD_VAL { get; set; }
        public string KD_STS_REK { get; set; }
        public decimal SALDOOS { get; set; }
        public decimal SALDOARUSKAS { get; set; }
        public int KD_KOL { get; set; }
        public decimal CKPN { get; set; }
        public string KDHUB { get; set; }
        public string KET_HUB { get; set; }
        public int KDSIFAT { get; set; }
        public string KETERANGAN { get; set; }
        public int KD_GRP_NAS { get; set; }
        public string NM_GRP_NAS { get; set; }
        public string SANDIGOLP { get; set; }
        public string NAMAGOLP { get; set; }
        public int FLAG_TIPE_NAS { get; set; }
        public string NM_TIPE_NAS { get; set; }
        public string UNIT { get; set; }
    }
}

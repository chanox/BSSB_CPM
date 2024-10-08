using System.ComponentModel.DataAnnotations;

namespace CPM_Project.Models.SRAK
{
    public class TB_SRAK
    {
        [Key]
        public int ID_SRAK { get; set; }
        public DateTime? TGL_DATA { get; set; }
        public string KD_KNTR { get; set; }
        public string KD_PRODUK { get; set; }
        public string KD_SUB_PRODUK { get; set; }
        public string KD_VAL { get; set; }
        public decimal KURSTENGAH { get; set; }
        public string KD_JNS_PINJ { get; set; }
        public decimal BUNGACR { get; set; }
        public decimal BUNGASR { get; set; }
        public decimal NOMINAL { get; set; }
        public decimal NOMINALRP { get; set; }
        public decimal BUNGA { get; set; }
        public decimal BUNGARP { get; set; }
        public decimal BUNGA_SRAK { get; set; }
        public decimal NOMINAL_SRAK { get; set; }
        public string KETERANGAN { get; set; }
        public string PRODUK { get; set; }
        public string NMKANTOR { get; set; }
        public string KD_KNTR_INDUK { get; set; }
        public string KDKNTR { get; set; }
    }
}

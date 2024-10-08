using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CPM_Project.Models.SRAK
{
    public class SRAK_Bunga
    {
        [Key]
        public int ID_SRAK_BUNGA { get; set; }
        public string KD_PRODUK { get; set; }
        public string KD_SUB_PRODUK { get; set; }
        public string KD_VAL { get; set; }
        public string KD_JNS_PINJ { get; set; }
        public string JENIS_BUNGA { get; set; }
        public decimal BUNGA { get; set; }
    }
}

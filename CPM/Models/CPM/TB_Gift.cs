using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.CPM
{
    [Table("tb_gift")]
    public class TB_Gift
    {
        [Key]
        public int ID_GIFT { get; set; }

        [Display(Name = "NIK")]
        [Required(ErrorMessage =  "Tidak boleh kosong")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Wajib 16 Digit")]
        public string NIK { get; set; }

        [Display(Name = "Nama Lengkap")]
        [Required(ErrorMessage = "Tidak boleh kosong")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Minimal 2 Karakter")]
        public string NM_NAS { get; set; }

        [Display(Name = "Cabang")]
        public string KD_UNITKER { get; set; }

        [Display(Name = "Tujuan Pemberian")]
        public string TUJUAN_GIFT {  get; set; }

        [Display(Name = "Tanggal Pemberian")]
        public DateTime TGL_GIFT { get; set; }

        [Display(Name = "Biaya")]
        public decimal BIAYA {  get; set; }


    }
}

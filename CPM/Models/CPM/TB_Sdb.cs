using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CPM_Project.Models.CPM
{
    public class TB_Sdb
    {
        [Key]
        public int ID_SDB { get; set; }

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

        [Display(Name = "Nomor SDB")]
        public string NO_SDB {  get; set; }

        [Display(Name = "Ukuran SDB")]
        public string UKURAN_SDB { get; set; }

        [Display(Name = "Tanggal PKS")]
        public DateTime TGL_PKS { get; set; }

        [Display(Name = "Tanggal Expired PKS")]
        public DateTime TGL_EXPIRED { get;set; }

        [Display(Name = "Biaya")]
        public decimal BIAYA {  get; set; }


    }
}

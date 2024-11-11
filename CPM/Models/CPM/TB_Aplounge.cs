using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CPM_Project.Models.CPM
{
    public class TB_Aplounge
    {
        [Key]
        public int ID_APLOUNGE { get; set; }

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

        [Display(Name = "Tanggal Penggunaan")]
        public DateTime TGL_PENGGUNAAN { get; set; }

        [Display(Name = "Jumlah Pax")]
        public int JUMLAH_PAX { get; set; }

        [Display(Name = "Nama Airport")]
        public string NM_AIRPORT {  get; set; }

        [Display(Name = "Frekuensi Penggunaan")]
        public int FREKUENSI { get;set; }

        [Display(Name = "Biaya")]
        public decimal BIAYA {  get; set; }


    }
}

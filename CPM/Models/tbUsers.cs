using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Models
{
    public partial class tbUsers
    {
        [Key]
        public int ID_USER { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Harus di isi..")]
        [Remote("IsCreateUserExists", "User", HttpMethod = "POST", ErrorMessage = "User sudah terdaftar..")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimal 2 karakter..")]
        public string? USER_NAME { get; set; }

        [Display(Name = "Nama Lengkap")]
        [Required(ErrorMessage = "Harus di isi..")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Minimal 2 karakter..")]
        public string? NAMA_LENGKAP { get; set; }

        [Display(Name = "Password")]
        //[Required(ErrorMessage = "Harus di isi..3")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Minimal 2 karakter..")]
        public string? PASSWORD { get; set; }

        [Display(Name = "Jenis User")]
        public string? JENIS_USER { get; set; }

        [Display(Name = "Grup User")]
        public string? GRUP_USER { get; set; }

        [Display(Name = "Kantor")]
        public string? KD_UNITKER { get; set; }

        [Display(Name = "Aktif")]
        public bool AKTIF { get; set; }

        public Int16 STS_BLOCK { get; set; }
        public DateTime JAM_BLOCK { get; set; }
    }
}

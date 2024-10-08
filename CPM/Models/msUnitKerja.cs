using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Models
{
    public partial class msUnitKerja
    {
        [Key]
        public int ID_UNITKER { get; set; }

        [Display(Name = "Kode Unit")]
        [Required(ErrorMessage = "Harus di isi..")]
        [Remote("IsCreateKantorExists", "User", HttpMethod = "POST", ErrorMessage = "Kode Kantor sudah terdaftar..")]
        public string? KD_UNITKER { get; set; }

        [Display(Name = "Nama Unit Kerja")]
        [Required(ErrorMessage = "Harus di isi..")]
        public string? NM_UNITKER { get; set; }

        [Display(Name = "Alamat")]
        [Required(ErrorMessage = "Harus di isi..")]
        public string? ALAMAT { get; set; }

        [Display(Name = "Kota")]
        [Required(ErrorMessage = "Harus di isi..")]
        public string? NM_KOTA { get; set; }
    }
}

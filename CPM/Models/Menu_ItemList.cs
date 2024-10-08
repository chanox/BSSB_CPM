using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models
{
    public partial class Menu_ItemList
    {
        public uint ID_MENU { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? NM_MENU { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? NM_CONTROLLER { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? NM_ACTION { get; set; }
        [Column(TypeName = "int(2)")]
        public int? KD_MENU { get; set; }
        [Column(TypeName = "int(2)")]
        public int? MASTER_MENU { get; set; }
        [Column(TypeName = "int(2)")]
        public int CHILD { get; set; }
        [Column(TypeName = "int(2)")]
        public int? PENGATURAN { get; set; }
        [Column(TypeName = "int(2)")]
        public int? DASHBOARD { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? ICON { get; set; }
        [Column(TypeName = "int(2)")]
        public int? ADMIN { get; set; }
        [Column(TypeName = "int(2)")]
        public int? USER { get; set; }
    }
}

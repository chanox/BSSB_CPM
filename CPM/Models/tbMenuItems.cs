using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models
{
    public partial class tbMenuItems
    {
        [Key]
        public uint ID_MENU { get; set; }
        public string? NM_MENU { get; set; }
        public string? NM_CONTROLLER { get; set; }
        public string? NM_ACTION { get; set; }
        public int? KD_MENU { get; set; }
        public int? MASTER_MENU { get; set; }
        public int CHILD { get; set; }
        public int? PENGATURAN { get; set; }
        public int? DASHBOARD { get; set; }
        public string? ICON { get; set; }
        public int? ADMIN { get; set; }
        public int? USER { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models
{
    public partial class tbUserLog
    {
        [Key]
        public uint ID_LOGIN { get; set; }
        public DateTime TANGGAL_JAM { get; set; }
        public string? NAMA_USER { get; set; }
        public string? IP_ADDRESS { get; set; }
        public int STATUS { get; set; }
        public string? KET { get; set; }
    }
}

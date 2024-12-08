using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.CPM;

[Table("tb_master_rm")]
public class TB_Master_RM
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_MASTER_RM { get; set; }
    public string? KODE_RM { get; set; }
    public string? NAMA_RM { get; set; }
    public string? NO_HP { get; set; }
    public string? E_MAIL { get; set; }
}
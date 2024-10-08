using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.SRAK;

[Table("tb_srak_saldo")]
public class TB_SRAK_Saldo
{
    [Key]
    public int ID_SRAK_SALDO { get; set; }
    public string NO_REK { get; set; }
    public string NM_REK { get; set; }
    public decimal SALDO_AKHIR { get; set; }
}
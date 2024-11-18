using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPM_Project.Models.CPM
{

    [Table("tb_aum")]
    public class TB_Aum
    {
        [Key]
        public int ID_AUM { get; set; }
        public DateTime? Date { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string BankAccountNo { get; set; }
        public string StockID { get; set; }
        public string StockName { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyID { get; set; }
        public string? SavingsID { get; set; }
        public string? RDNBankID { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string SID { get; set; }
        public string SRE { get; set; }
        public string ReferralID { get; set; }
        public string BankID { get; set; }
        public string BankAccountName { get; set; }
        public DateTime? MaturityDate { get; set; }
        public Decimal CouponPercent { get; set; }
        public string SalesID { get; set; }
        public string SalesName { get; set; }
    }
}

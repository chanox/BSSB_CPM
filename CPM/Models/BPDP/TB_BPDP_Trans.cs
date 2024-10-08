namespace CPM_Project.Models.BPDP
{
    public class TB_BPDP_Trans
    {
        public int ID_BPDP_TRANS { get; set; }
        public string NO_TRANS { get; set; }

        public DateTime? TANGGAL { get; set; }
        public string NAMA_PENERIMA { get; set; }

        // DEBET
        public string NOREK_DEBET { get; set; }
        public decimal NOMINAL_DEBET { get; set; }
        //public string KET_DEBET { get; set; }

        // KREDIT
        public string NOREK_KREDIT { get; set; }
        public decimal NOMINAL_KREDIT { get; set; }
        public string KETERANGAN { get; set; }
    }
}


﻿namespace CPM_Project.Models.BPDP
{
    public class TB_BPDP_RTGS_Trans
    {
        public int ID_BPDP_RTGS_TRANS { get; set; }

        public string NO_TRANS { get; set; }
        public DateTime? TANGGAL { get; set; }
        public string NAMA_PENGIRIM { get; set; }
        public string NOREK_PENGIRIM { get; set; }
        public string BANK_PENERIMA { get; set; }
        public string NAMA_PENERIMA { get; set; }
        public string NOREK_PENERIMA { get; set; }
        public decimal NOMINAL { get; set; }
        public decimal BIAYA { get; set; }
        public string NO_REFF { get; set; }
        public string KETERANGAN { get; set; }
        public string JENIS_TRANSAKSI { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPM_Project.Models.SERVICE
{
    public class RekeningKoran
    {
        public class RekeningRequest
        {
            public string NO_REK { get; set; }
            public DateTime TGL_AWAL { get; set; }
            public DateTime TGL_AKHIR { get; set; }
            public int OPSI { get; set; }
            public int JUM_TRANS { get; set; }
        }

        public class RekeningResponse
        {
            public string RESPONSE_CODE { get; set; }
            public decimal SALDO_AWAL { get; set; }
            public decimal SALDO_AKHIR { get; set; }
            public List<History> HISTORY_TRANS { get; set; }
        }

        public class History
        {
            public DateTime TGL_TRANS { get; set; }
            public DateTime TGL_VAL { get; set; }
            public string NO_DOK { get; set; }
            public string KD_TRAN { get; set; }
            public string KET_TRANS { get; set; }
            public string STATUS { get; set; }
            public decimal NOMINAL { get; set; }
            public decimal SALDO_AKHIR { get; set; }
        }

        public class RekeningGridResponse
        {
            public string RESPONSE_CODE { get; set; }
            public decimal SALDO_AWAL { get; set; }
            public decimal SALDO_AKHIR { get; set; }
            public List<HistoryGrid> HISTORY_TRANS { get; set; }
        }

        public class HistoryGrid
        {
            public DateTime TGL_TRANS { get; set; }
            public DateTime TGL_VAL { get; set; }
            public string NO_DOK { get; set; }
            public string KD_TRAN { get; set; }
            public string KET_TRANS { get; set; }
            public string STATUS { get; set; }
            public decimal DEBET { get; set; }
            public decimal KREDIT { get; set; }
            public decimal SALDO_AKHIR { get; set; }
        }
    }
}
namespace CPM_Project.Models
{
    public class LoginData
    {
        public int ID_USER { get; set; }

        public string? USER_NAME { get; set; }
        public string? NAMA_LENGKAP { get; set; }
        public string? PASSWORD { get; set; }

        public string? NM_TEMPAT { get; set; }
        
        public string? JENIS_USER { get; set; }
        public string? GRUP_USER { get; set; }
        public string? KD_UNITKER { get; set; }
        public string? NM_UNITKER { get; set; }
        public bool AKTIF { get; set; }

        public DateTime LAST_LOGIN { get; set; }
    }
}
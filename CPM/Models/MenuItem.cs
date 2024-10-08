namespace CPM_Project.Models
{
    public class MenuItem
    {
        public int ID_MENU { get; set; }
        public string? NM_MENU { get; set; }
        public string? NM_CONTROLLER { get; set; }
        public string? NM_ACTION { get; set; }
        public int KD_MENU { get; set; }
        public int MASTER_MENU { get; set; }
        public int CHILD { get; set; }
        public int PENGATURAN { get; set; }
        public int DASHBOARD { get; set; }
        public string? ICON { get; set; }
    }

    public class MultiMenuItem
    {
        public List<MenuItem>? MenuHeader { get; set; }
        public List<MenuItem>? ItemMenu { get; set; }
    }
}
using System.Data;
using MySqlConnector;
using Newtonsoft.Json;

namespace CPM_Project.Models
{
    public class MenuItemLayers
    {
        GlobalVariable _GlobalVariable = new();
        LoginData? LoginData = new();

        public void GetGlobalVariable(string SessionString)
        {
            //LoginData.USER_NAME = HttpContext.Current.Application["USER_NAME"] as string;
            //LoginData.NAMA_LENGKAP = HttpContext.Current.Application["NAMA_LENGKAP"] as string;
            //LoginData.NO_REK = HttpContext.Current.Application["NO_REK"] as string;
            //LoginData.KD_KNTR = HttpContext.Current.Application["KD_KNTR"] as string;
            //LoginData.KD_SKPD = HttpContext.Current.Application["KD_SKPD"] as string;
            //LoginData.NM_SKPD = HttpContext.Current.Application["NM_SKPD"] as string;
            //LoginData.JENIS_USER = HttpContext.Current.Application["JENIS_USER"] as string;
            //LoginData.AKTIF = Convert.ToInt32(HttpContext.Current.Application["AKTIF"] as string);
            //LoginData.LIMIT_TRANS = Convert.ToDecimal(HttpContext.Current.Application["LIMIT_TRANS"] as string);
            //LoginData.NO_REK_RTGS = HttpContext.Current.Application["NO_REK_RTGS"] as string;
            //LoginData.NO_REK_KLIRING = HttpContext.Current.Application["NO_REK_KLIRING"] as string;

            //try
            //{
            //    LoginData = (LoginData)HttpContext.Current.Session["LoginData"];
            //}
            //catch
            //{
            //    LoginData = null;
            //}

            var test = SessionString;

            try
            {
                LoginData = JsonConvert.DeserializeObject<LoginData>(SessionString);
            }
            catch
            {
                //LoginData = null;
                LoginData = new LoginData() { JENIS_USER = "User" };
            }

            //LoginData = new LoginData();
            //LoginData.JENIS_USER = "User";
        }

        public List<MenuItem> GetHeaderMenu(string SessionString)
        {
            MySqlConnection? con = null;
            MySqlCommand? cmd = null;
            MySqlDataAdapter? da = null;
            DataTable? dt = null;

            try
            {
                LoginData = JsonConvert.DeserializeObject<LoginData>(SessionString);
            }
            catch
            {
                LoginData = null;
            }

            try
            {
                con = new MySqlConnection(_GlobalVariable.ConnectionDB);
                con.Open();

                // -- HEADER MENU
                cmd = con.CreateCommand();
                cmd.CommandTimeout = 0;

                //string Target = "Admin"; //HttpContext.Current.Request.Cookies["eMIS"]["Target"].ToString();

                //if (Target == "Admin")
                //{
                //    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1)  ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                //    cmd.ExecuteScalar();
                //}
                //else
                //{
                //    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE NM_CONTROLLER = :target AND (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1)  ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                //    cmd.Parameters.AddWithValue("target", Target);
                //    cmd.ExecuteScalar();
                //}

                if (LoginData?.JENIS_USER == "Admin" && LoginData?.GRUP_USER == "-")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND ADMIN = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "User" && LoginData?.GRUP_USER == "-")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND USER = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "User" && LoginData?.GRUP_USER == "BPDP")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND BPDP_USER = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "Kanpus" && LoginData?.GRUP_USER == "BPDP" )
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND BPDP_KANPUS = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "User" && LoginData?.GRUP_USER == "SRAK")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND SRAK_USER = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "Kanpus" && LoginData?.GRUP_USER == "SRAK" )
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND SRAK_KANPUS = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                    cmd.ExecuteScalar();
                }
                // else if (LoginData?.GRUP_USER == "BPDP")
                // {
                //     cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND BPDP = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                //     cmd.ExecuteScalar();
                // }
                // else if (LoginData?.GRUP_USER == "Tunai")
                // {
                //     cmd.CommandText = "SELECT * FROM tbMenuItems WHERE /*NM_CONTROLLER = :target AND*/ (KD_MENU = MASTER_MENU OR DASHBOARD = 1 OR PENGATURAN = 1) AND TUNAI = 1 ORDER BY MASTER_MENU, DASHBOARD DESC, PENGATURAN ASC"; //"SELECT * FROM tbMenuItems WHERE KD_MENU = MASTER_MENU ORDER BY KD_MENU";
                //     cmd.ExecuteScalar();
                // }

                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                dt = new DataTable();
                da.Fill(dt);

                List<MenuItem> ListMenuHeader = new List<MenuItem>();

                ListMenuHeader = (from DataRow dr in dt.Rows
                                  select new MenuItem()
                                  {
                                      ID_MENU = Convert.ToInt32(dr["ID_MENU"]),
                                      NM_MENU = dr["NM_MENU"].ToString(),
                                      NM_CONTROLLER = dr["NM_CONTROLLER"].ToString(),
                                      NM_ACTION = dr["NM_ACTION"].ToString(),
                                      KD_MENU = Convert.ToInt32(dr["KD_MENU"]),
                                      MASTER_MENU = Convert.ToInt32(dr["MASTER_MENU"]),
                                      CHILD = Convert.ToInt32(dr["CHILD"]),
                                      PENGATURAN = Convert.ToInt32(dr["PENGATURAN"]),
                                      DASHBOARD = Convert.ToInt32(dr["DASHBOARD"]),
                                      ICON = dr["ICON"].ToString()
                                  }).ToList();

                dt.Dispose();
                cmd.Dispose();
                con.Close();

                return ListMenuHeader;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }

        public List<MenuItem> GetItemMenu(string SessionString)
        {
            MySqlConnection? con = null;
            MySqlCommand? cmd = null;
            MySqlDataAdapter? da = null;
            DataTable? dt = null;

            try
            {
                con = new MySqlConnection(_GlobalVariable.ConnectionDB);
                con.Open();

                // -- ITEM MENU
                cmd = con.CreateCommand();
                cmd.CommandTimeout = 0;

                try
                {
                    LoginData = JsonConvert.DeserializeObject<LoginData>(SessionString);
                }
                catch
                {
                    LoginData = null;
                }

                //string Target = "Admin"; //HttpContext.Current.Request.Cookies["eMIS"]["Target"].ToString(); //HttpContext.Current.Session["Target"] as String;

                //if (Target == "Admin")
                //{
                //    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                //    //cmd.Parameters.Add("target", Target);
                //    cmd.ExecuteScalar();
                //}
                //else
                //{
                //    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND NM_CONTROLLER = :target ORDER BY MASTER_MENU, KD_MENU";
                //    cmd.Parameters.AddWithValue("target", Target);
                //    cmd.ExecuteScalar();
                //}

                if (LoginData?.JENIS_USER == "Admin" && LoginData?.GRUP_USER == "-")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND ADMIN = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "User" && LoginData?.GRUP_USER == "-") {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND USER = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "User" && LoginData?.GRUP_USER == "BPDP") {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND BPDP_USER = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "Kanpus" && LoginData?.GRUP_USER == "BPDP")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND BPDP_KANPUS = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "User" && LoginData?.GRUP_USER == "SRAK") {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND SRAK_USER = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                    cmd.ExecuteScalar();
                }
                else if (LoginData?.JENIS_USER == "Kanpus" && LoginData?.GRUP_USER == "SRAK")
                {
                    cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND SRAK_KANPUS = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                    cmd.ExecuteScalar();
                }
                // else if (LoginData?.GRUP_USER == "BPDP")
                // {
                //     cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND BPDP = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                //     cmd.ExecuteScalar();
                // }
                // else if (LoginData?.GRUP_USER == "Tunai")
                // {
                //     cmd.CommandText = "SELECT * FROM tbMenuItems WHERE KD_MENU <> MASTER_MENU AND PENGATURAN IN (0,2) AND DASHBOARD = 0 AND TUNAI = 1 /*AND NM_CONTROLLER = :target*/ ORDER BY MASTER_MENU, KD_MENU";
                //     cmd.ExecuteScalar();
                // }


                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                dt = new DataTable();
                da.Fill(dt);

                List<MenuItem> ListMenuItem = new List<MenuItem>();

                ListMenuItem = (from DataRow dr in dt.Rows
                                select new MenuItem()
                                {
                                    ID_MENU = Convert.ToInt32(dr["ID_MENU"]),
                                    NM_MENU = dr["NM_MENU"].ToString(),
                                    NM_CONTROLLER = dr["NM_CONTROLLER"].ToString(),
                                    NM_ACTION = dr["NM_ACTION"].ToString(),
                                    KD_MENU = Convert.ToInt32(dr["KD_MENU"]),
                                    MASTER_MENU = Convert.ToInt32(dr["MASTER_MENU"]),
                                    CHILD = Convert.ToInt32(dr["CHILD"]),
                                    PENGATURAN = Convert.ToInt32(dr["PENGATURAN"]),
                                    DASHBOARD = Convert.ToInt32(dr["DASHBOARD"]),
                                    ICON = dr["ICON"].ToString()
                                }).ToList();

                dt.Dispose();
                cmd.Dispose();
                con.Close();

                return ListMenuItem;
            }
            catch
            {
                con.Close();
                return null;
            }
        }
    }
}
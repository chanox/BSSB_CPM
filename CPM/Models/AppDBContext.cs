using CPM_Project.Models.CPM;
using CPM_Project.Models.SRAK;
using Microsoft.EntityFrameworkCore;

namespace CPM_Project.Models
{
    public partial class AppDBContext : DbContext
    {
        GlobalVariable _GlobalVariable = new();

        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql( _GlobalVariable.ConnectionDB, new MariaDbServerVersion(ServerVersion.AutoDetect(_GlobalVariable.ConnectionDB)));
            }
        }

        public virtual DbSet<msUnitKerja> msUnitKerja { get; set; }
        public virtual DbSet<tbMenuItems> tbMenuItems { get; set; }
        public virtual DbSet<tbUserLog> tbUserLog { get; set; }
        public virtual DbSet<tbUsers> tbUsers { get; set; }
        public virtual DbSet<TB_SRAK_Saldo> TB_SRAK_Saldo { get; set; }
        public virtual DbSet<TB_Customer> TB_CUSTOMERS { get; set; }
    }
}

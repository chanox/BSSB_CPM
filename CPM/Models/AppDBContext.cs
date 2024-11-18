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
            //Database.Migrate();
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

        public DbSet<msUnitKerja> msUnitKerja { get; set; }
        public DbSet<tbMenuItems> tbMenuItems { get; set; }
        public DbSet<tbUserLog> tbUserLog { get; set; }
        public DbSet<tbUsers> tbUsers { get; set; }
        public DbSet<TB_Customer> TB_CUSTOMERS { get; set; }
        public DbSet<TB_Aum> tb_aum { get; set; }
        public DbSet<TB_Sdb> tb_Sdb { get; set; }
        public DbSet<TB_Gift> tb_Gift { get; set; }
        public DbSet<TB_Aplounge> tb_Aplounge { get; set; }
        public DbSet<TB_Master_RM> TB_Master_RM { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -- TB_MASTER_RM
            modelBuilder.Entity<TB_Master_RM>(entity =>
            {
                entity.ToTable("tb_master_rm");
                entity.HasKey(e => e.ID_MASTER_RM).HasName("PRIMARY");
                entity.Property(e => e.ID_MASTER_RM).HasColumnType("int(11) unsigned"); //.ValueGeneratedOnAddOrUpdate();
                entity.HasIndex(e => e.KODE_RM, "KODE_RM_index").IsUnique();
                entity.Property(e => e.KODE_RM).HasColumnType("varchar(20)");
                entity.Property(e => e.NAMA_RM).HasColumnType("varchar(255)");
                entity.Property(e => e.NO_HP).HasColumnType("varchar(25)");
                entity.Property(e => e.E_MAIL).HasColumnType("varchar(150)");
                
                entity.HasData(
                    new TB_Master_RM { ID_MASTER_RM = -1, KODE_RM = "123", NAMA_RM = "Data 1", NO_HP = "1234567890", E_MAIL = "-"},
                    new TB_Master_RM { ID_MASTER_RM = -2, KODE_RM = "456", NAMA_RM = "Data 2" , NO_HP = "1234567890", E_MAIL = "-"}
                );
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

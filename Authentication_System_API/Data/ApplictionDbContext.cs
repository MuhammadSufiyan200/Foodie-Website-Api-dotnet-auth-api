using Authentication_System_API.model;
using Microsoft.EntityFrameworkCore;

namespace Authentication_System_API.Data
{
    public class ApplictionDbContext : DbContext
    {
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options)
        {     
        }

        public DbSet<User> Tbl_User { get; set; }
        public DbSet<ActiveSession> Tbl_ActiveSessions { get; set; }
        public DbSet<SystemLog> Tbl_SystemLogs { get; set; }


       // Inventory Management
        public DbSet<ItemCategory> Tbl_ItemCategories { get; set; }
        public DbSet<Unit> Tbl_Units { get; set; }
        public DbSet<InventoryItem> Tbl_InventoryItems { get; set; }

        public DbSet<PurchaseRoot> Tbl_PurchaseRoots { get; set; }
        public DbSet<PurchaseDetail> Tbl_PurchaseDetails { get; set; }

        public DbSet<SaleRoot> Tbl_SaleRoots { get; set; }
        public DbSet<SaleDetail> Tbl_SaleDetails { get; set; }

        public DbSet<PurchaseReturnRoot> Tbl_PurchaseReturnRoots { get; set; }
        public DbSet<PurchaseReturnDetail> Tbl_PurchaseReturnDetails { get; set; }

        public DbSet<SaleReturnRoot> Tbl_SaleReturnRoots { get; set; }
        public DbSet<SaleReturnDetail> Tbl_SaleReturnDetails { get; set; }
        public DbSet<StockLedger> StockLedger { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Foreign key config (already present)
            modelBuilder.Entity<StockLedger>()
                .HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.InventoryItemId);

            // Decimal precision config (NEW)
            modelBuilder.Entity<InventoryItem>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseDetail>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseReturnDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseReturnDetail>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseReturnRoot>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseRoot>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SaleReturnDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SaleReturnDetail>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SaleRoot>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SaleReturnRoot>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StockLedger>()
                .Property(p => p.Rate)
                .HasPrecision(18, 2);
        }


    }
}

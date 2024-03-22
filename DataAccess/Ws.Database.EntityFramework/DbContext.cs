using Ws.Database.EntityFramework.Entities.Ref.Claims;
using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Database.EntityFramework.Entities.Ref.PluResources;
using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Database.EntityFramework.Entities.Ref.StorageMethods;
using Ws.Database.EntityFramework.Entities.Ref.Templates;
using Ws.Database.EntityFramework.Entities.Ref.Users;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.Database.EntityFramework.Entities.Ref.ZplResources;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.Database.EntityFramework.Extensions;

namespace Ws.Database.EntityFramework;

public class WsDbContext : DbContext
{
    public DbSet<ZplResourceEntity> ZplResources { get; set; }
    public DbSet<PalletManEntity> PalletMen { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<ProductionSiteEntity> ProductionSites { get; set; }
    public DbSet<StorageMethodEntity> StorageMethods { get; set; }
    public DbSet<ClaimEntity> Claims { get; set; }
    public DbSet<TemplateEntity> Templates { get; set; }
    public DbSet<BoxEntity> Boxes { get; set; }
    public DbSet<ClipEntity> Clips { get; set; }
    public DbSet<BundleEntity> Bundles { get; set; }
    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PrinterEntity> Printers { get; set; }
    public DbSet<LineEntity> Lines { get; set; }
    public DbSet<PluEntity> Plus { get; set; }
    public DbSet<PluResourceEntity> PluResources { get; set; }
    public DbSet<PluNestingEntity> PlusNestings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=172.16.2.5;Database=WEIGHT;User Id=Developer;Password=Hz&V5Gnq4d4584;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIpAddressConversion();
        modelBuilder.UseEnumStringConversion();
        modelBuilder.MapCreateOrChangeDt();

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(e => e.Claims)
                .WithMany(e => e.Users)
                .UsingEntity(
                "USERS_СLAIMS_FK",
                l => l.HasOne(typeof(ClaimEntity))
                    .WithMany()
                    .HasForeignKey("CLAIM_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(ClaimEntity.Id)),
                r => r.HasOne(typeof(UserEntity))
                    .WithMany()
                    .HasForeignKey("USER_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(UserEntity.Id)),
                j => j.HasKey("CLAIM_UID", "USER_UID"));
        });

        modelBuilder.Entity<PluResourceEntity>(entity =>
        {
            entity.HasOne<PluEntity>()
                .WithOne(p => p.Resource)
                .HasForeignKey<PluResourceEntity>(pr => pr.Id);
        });

        modelBuilder.Entity<LineEntity>(entity =>
        {
            entity.HasOne(l => l.Warehouse)
                .WithMany()
                .HasForeignKey("WAREHOUSE_UID")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.Printer)
                .WithMany()
                .HasForeignKey("PRINTER_UID")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Plus)
                .WithMany()
                .UsingEntity(
                "LINES_PLUS_FK",
                l => l.HasOne(typeof(PluEntity))
                    .WithMany()
                    .HasForeignKey("PLU_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(PluEntity.Id)),
                r => r.HasOne(typeof(LineEntity))
                    .WithMany()
                    .HasForeignKey("LINE_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(LineEntity.Id)),
                j => j.HasKey("PLU_UID", "LINE_UID"));
        });

        modelBuilder.Entity<PluNestingEntity>(entity =>
        {
            entity.HasIndex(pn => new { pn.PluEntityId, pn.IsDefault })
                .IsUnique()
                .HasDatabaseName($"UQ_{SqlTables.PluNesting}_IS_DEFAULT_TRUE_ON_PLU")
                .HasFilter("[IS_DEFAULT] = 1");
        });
    }
}
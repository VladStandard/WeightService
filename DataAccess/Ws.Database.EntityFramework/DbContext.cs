using System.Reflection.PortableExecutable;
using Microsoft.Extensions.Logging;
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
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.Database.EntityFramework.Extensions;
using Ws.Database.EntityFramework.Interceptors;
using Ws.Shared.Utils;

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
    public DbSet<NestingEntity> Nestings { get; set; }
    public DbSet<CharacteristicEntity> Characteristics { get; set; }

    private static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=CREATIO\\INS1;Database=WEIGHT;User Id=DEVOLOPER;Password=Hz&V5Gnq4d4584;TrustServerCertificate=true;");
        optionsBuilder.AddInterceptors(new ChangeDtInterceptor());

        if (ConfigurationUtil.IsDevelop)
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIpAddressConversion();
        modelBuilder.UseEnumStringConversion();
        modelBuilder.MapCreateOrChangeDt();

        modelBuilder.MapLine();
        modelBuilder.MapUser();
        modelBuilder.MapPluResource();
        modelBuilder.MapNesting();
        modelBuilder.MapCharacteristic();
        // modelBuilder.MapCharacteristic();
    }
}
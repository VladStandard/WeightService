using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.Database.EntityFramework.Entities.Print.Pallets;
using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Database.EntityFramework.Entities.Ref.Users;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.Database.EntityFramework.Entities.Zpl.Templates;
using Ws.Database.EntityFramework.Entities.Zpl.ZplResources;
using Ws.Database.EntityFramework.Views.Diag.DatabaseTables;

namespace Ws.Database.EntityFramework;

public class WsDbContext : DbContext
{
    #region DbSet

    public DbSet<DatabaseTableView> DatabaseTables { get; set; }

    public DbSet<ZplResourceEntity> ZplResources { get; set; }
    public DbSet<PalletManEntity> PalletMen { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<ProductionSiteEntity> ProductionSites { get; set; }
    public DbSet<TemplateEntity> Templates { get; set; }
    public DbSet<BoxEntity> Boxes { get; set; }
    public DbSet<ClipEntity> Clips { get; set; }
    public DbSet<BundleEntity> Bundles { get; set; }
    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<PrinterEntity> Printers { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<LineEntity> Lines { get; set; }
    public DbSet<PluEntity> Plus { get; set; }
    public DbSet<NestingEntity> Nestings { get; set; }
    public DbSet<CharacteristicEntity> Characteristics { get; set; }
    public DbSet<LabelEntity> Labels { get; set; }
    public DbSet<PalletEntity> Pallets { get; set; }

    #endregion

    private static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        SqlSettingsModel sqlCfg = LoadJsonConfig();

        optionsBuilder.UseSqlServer(sqlCfg.GetConnectionString());
        optionsBuilder.AddInterceptors(new ChangeDtInterceptor());

        if (ConfigurationUtil.IsDevelop && sqlCfg.IsShowSql)
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetDefaultTypeForString();
        modelBuilder.UseIpAddressConversion();
        modelBuilder.UseEnumStringConversion();
        modelBuilder.MapCreateOrChangeDt();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static SqlSettingsModel LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();

        SqlSettingsModel sqlSettingsModel = new();
        sqlConfiguration.GetSection("SqlSettings").Bind(sqlSettingsModel);
        return sqlSettingsModel;
    }
}
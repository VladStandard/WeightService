// using Microsoft.EntityFrameworkCore;
// using Ws.Database.EntityFramework.Models.Ready;
//
// namespace Ws.Database.EntityFramework.Models;
//
// public partial class ScalesContext : DbContext
// {
//     public ScalesContext()
//     {
//     }
//
//     public ScalesContext(DbContextOptions<ScalesContext> options)
//         : base(options)
//     {
//     }
//
//     public virtual DbSet<Box> Boxes { get; set; }
//
//     public virtual DbSet<Brand> Brands { get; set; }
//
//     public virtual DbSet<Bundle> Bundles { get; set; }
//
//     public virtual DbSet<Claim> Claims { get; set; }
//
//     public virtual DbSet<Clip> Clips { get; set; }
//
//     public virtual DbSet<Host> Hosts { get; set; }
//
//     public virtual DbSet<Label> Labels { get; set; }
//
//     public virtual DbSet<Line> Lines { get; set; }
//
//     public virtual DbSet<LogsWebService> LogsWebServices { get; set; }
//
//     public virtual DbSet<Pallet> Pallets { get; set; }
//
//     public virtual DbSet<PalletMan> PalletMen { get; set; }
//
//     public virtual DbSet<Plu> Plus { get; set; }
//
//     public virtual DbSet<PlusLine> PlusLines { get; set; }
//
//     public virtual DbSet<PlusNestingFk> PlusNestingFks { get; set; }
//
//     public virtual DbSet<PlusTemplate> PlusTemplates { get; set; }
//
//     public virtual DbSet<PlusTemplatesFk> PlusTemplatesFks { get; set; }
//
//     public virtual DbSet<Printer> Printers { get; set; }
//
//     public virtual DbSet<ProductionSite> ProductionSites { get; set; }
//
//     public virtual DbSet<StorageMethod> StorageMethods { get; set; }
//
//     public virtual DbSet<Template> Templates { get; set; }
//
//     public virtual DbSet<User> Users { get; set; }
//
//     public virtual DbSet<UsersClaimsFk> UsersClaimsFks { get; set; }
//
//     public virtual DbSet<ViewLabel> ViewLabels { get; set; }
//
//     public virtual DbSet<ViewPallet> ViewPallets { get; set; }
//
//     public virtual DbSet<ViewTablesSize> ViewTablesSizes { get; set; }
//
//     public virtual DbSet<Warehouse> Warehouses { get; set; }
//
//     public virtual DbSet<ZplResource> ZplResources { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=CREATIO\\INS1;Database=SCALES;User Id=scale01;Password=scale01;TrustServerCertificate=true");
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder
//             .HasDefaultSchema("db_scales")
//             .UseCollation("Cyrillic_General_100_CI_AS");
//
//         modelBuilder.Entity<Box>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_BOXES_UID");
//
//             entity.ToTable("BOXES", "REF_1C");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(128)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//             entity.Property(e => e.Weight)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT");
//         });
//
//         modelBuilder.Entity<Brand>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_BRANDS_UID");
//
//             entity.ToTable("BRANDS", "REF_1C");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(100)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//         });
//
//         modelBuilder.Entity<Bundle>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_BUNDLES_UID");
//
//             entity.ToTable("BUNDLES", "REF_1C");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(128)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//             entity.Property(e => e.Weight)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT");
//         });
//
//         modelBuilder.Entity<Claim>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_CLAIMS_UID");
//
//             entity.ToTable("CLAIMS", "REF");
//
//             entity.HasIndex(e => e.Name, "UQ_CLAIMS_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(50)
//                 .HasColumnName("NAME");
//         });
//
//         modelBuilder.Entity<Clip>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_CLIPS_UID");
//
//             entity.ToTable("CLIPS", "REF_1C");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(128)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//             entity.Property(e => e.Weight)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT");
//         });
//
//         modelBuilder.Entity<Host>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_HOSTS_UID");
//
//             entity.ToTable("HOSTS", "REF");
//
//             entity.HasIndex(e => e.Ip, "UQ_HOSTS_IP").IsUnique();
//
//             entity.HasIndex(e => e.Name, "UQ_HOSTS_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Ip)
//                 .HasMaxLength(15)
//                 .IsUnicode(false)
//                 .HasColumnName("IP");
//             entity.Property(e => e.LoginDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("LOGIN_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(128)
//                 .HasColumnName("NAME");
//         });
//
//         modelBuilder.Entity<Label>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_LABELS_UID");
//
//             entity.ToTable("LABELS", "PRINT");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.BarcodeBottom)
//                 .HasMaxLength(128)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE_BOTTOM");
//             entity.Property(e => e.BarcodeRight)
//                 .HasMaxLength(128)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE_RIGHT");
//             entity.Property(e => e.BarcodeTop)
//                 .HasMaxLength(128)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE_TOP");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.ExpirationDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("EXPIRATION_DT");
//             entity.Property(e => e.Kneading).HasColumnName("KNEADING");
//             entity.Property(e => e.LineUid).HasColumnName("LINE_UID");
//             entity.Property(e => e.PalletUid).HasColumnName("PALLET_UID");
//             entity.Property(e => e.PluUid).HasColumnName("PLU_UID");
//             entity.Property(e => e.ProdDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("PROD_DT");
//             entity.Property(e => e.WeightNetto)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT_NETTO");
//             entity.Property(e => e.WeightTare)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT_TARE");
//             entity.Property(e => e.Zpl)
//                 .IsUnicode(false)
//                 .HasColumnName("ZPL");
//
//             entity.HasOne(d => d.LineU).WithMany(p => p.Labels)
//                 .HasForeignKey(d => d.LineUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_LABELS_LINE_UID");
//
//             entity.HasOne(d => d.PalletU).WithMany(p => p.Labels)
//                 .HasForeignKey(d => d.PalletUid)
//                 .HasConstraintName("FK_LABELS_PALLET_UID");
//
//             entity.HasOne(d => d.PluU).WithMany(p => p.Labels)
//                 .HasForeignKey(d => d.PluUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_LABELS_PLU_UID");
//         });
//
//         modelBuilder.Entity<Line>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_LINES_UID");
//
//             entity.ToTable("LINES", "REF");
//
//             entity.HasIndex(e => e.Number, "UQ_LINES_NUMBER").IsUnique();
//
//             entity.HasIndex(e => e.PcName, "UQ_LINES_PC_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.Counter).HasColumnName("COUNTER");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(64)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Number).HasColumnName("NUMBER");
//             entity.Property(e => e.PcName)
//                 .HasMaxLength(32)
//                 .IsUnicode(false)
//                 .HasColumnName("PC_NAME");
//             entity.Property(e => e.PrinterUid).HasColumnName("PRINTER_UID");
//             entity.Property(e => e.Type)
//                 .HasMaxLength(16)
//                 .IsUnicode(false)
//                 .HasColumnName("TYPE");
//             entity.Property(e => e.Version)
//                 .HasMaxLength(16)
//                 .HasColumnName("VERSION");
//             entity.Property(e => e.WarehouseUid).HasColumnName("WAREHOUSE_UID");
//
//             entity.HasOne(d => d.PrinterU).WithMany(p => p.Lines)
//                 .HasForeignKey(d => d.PrinterUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_LINES_PRINTER_UID");
//
//             entity.HasOne(d => d.WarehouseU).WithMany(p => p.Lines)
//                 .HasForeignKey(d => d.WarehouseUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_LINES_WAREHOUSE_UID");
//         });
//
//         modelBuilder.Entity<LogsWebService>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_LOGS_WEB_SERVICES_UID");
//
//             entity.ToTable("LOGS_WEB_SERVICES", "diag", tb => tb.HasComment("LOGS_WEB_SERVICES reference"));
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.CountAll).HasColumnName("COUNT_ALL");
//             entity.Property(e => e.CountError).HasColumnName("COUNT_ERROR");
//             entity.Property(e => e.CountSuccess).HasColumnName("COUNT_SUCCESS");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.DataRequest).HasColumnName("DATA_REQUEST");
//             entity.Property(e => e.DataResponse).HasColumnName("DATA_RESPONSE");
//             entity.Property(e => e.StampDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("STAMP_DT");
//             entity.Property(e => e.Url)
//                 .HasMaxLength(256)
//                 .HasColumnName("URL");
//             entity.Property(e => e.Version)
//                 .HasMaxLength(12)
//                 .HasColumnName("VERSION");
//         });
//
//         modelBuilder.Entity<Pallet>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_PALLETS_UID");
//
//             entity.ToTable("PALLETS", "PRINT");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.Barcode)
//                 .HasMaxLength(32)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE");
//             entity.Property(e => e.Counter)
//                 .ValueGeneratedOnAdd()
//                 .HasColumnName("COUNTER");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.PalletManUid).HasColumnName("PALLET_MAN_UID");
//             entity.Property(e => e.ProdDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("PROD_DT");
//             entity.Property(e => e.Weight)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT");
//
//             entity.HasOne(d => d.PalletManU).WithMany(p => p.Pallets)
//                 .HasForeignKey(d => d.PalletManUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PALLETS_MAN_UID");
//         });
//
//         modelBuilder.Entity<PalletMan>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_PALLET_MEN_UID");
//
//             entity.ToTable("PALLET_MEN", "REF");
//
//             entity.HasIndex(e => new { e.Name, e.Surname, e.Patronymic }, "UQ_PALLET_MEN_FIO").IsUnique();
//
//             entity.HasIndex(e => e.Uid1c, "UQ_PALLET_MEN_UID_1C").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(64)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Password)
//                 .HasMaxLength(4)
//                 .IsUnicode(false)
//                 .HasColumnName("PASSWORD");
//             entity.Property(e => e.Patronymic)
//                 .HasMaxLength(64)
//                 .HasColumnName("PATRONYMIC");
//             entity.Property(e => e.Surname)
//                 .HasMaxLength(64)
//                 .HasColumnName("SURNAME");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//         });
//
//         modelBuilder.Entity<Plu>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK__PLUS__C5B1960225F6BA1F");
//
//             entity.ToTable("PLUS", tb => tb.HasComment("PLUS reference"));
//
//             entity.HasIndex(e => e.ChangeDt, "IX_PLUS_CHANGE_DT");
//
//             entity.HasIndex(e => e.CreateDt, "IX_PLUS_CREATE_DT");
//
//             entity.HasIndex(e => e.IsCheckWeight, "IX_PLUS_IS_CHECK_WEIGHT");
//
//             entity.HasIndex(e => e.Name, "IX_PLUS_NAME");
//
//             entity.HasIndex(e => e.Uid1c, "IX_PLUS_UID_1C");
//
//             entity.HasIndex(e => e.Number, "UQ_PLU_NUMBER").IsUnique();
//
//             entity.HasIndex(e => e.Uid1c, "UQ_PLU_UID_1C").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.BrandUid).HasColumnName("BRAND_UID");
//             entity.Property(e => e.BundleUid).HasColumnName("BUNDLE_UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.ClipUid).HasColumnName("CLIP_UID");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
//             entity.Property(e => e.Ean13)
//                 .HasMaxLength(13)
//                 .IsUnicode(false)
//                 .HasColumnName("EAN13");
//             entity.Property(e => e.FullName).HasColumnName("FULL_NAME");
//             entity.Property(e => e.IsCheckWeight).HasColumnName("IS_CHECK_WEIGHT");
//             entity.Property(e => e.Itf14)
//                 .HasMaxLength(14)
//                 .IsUnicode(false)
//                 .HasColumnName("ITF14");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(150)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Number).HasColumnName("NUMBER");
//             entity.Property(e => e.ShelfLifeDays).HasColumnName("SHELF_LIFE_DAYS");
//             entity.Property(e => e.StorageMethodUid).HasColumnName("STORAGE_METHOD_UID");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//
//             entity.HasOne(d => d.BrandU).WithMany(p => p.Plus)
//                 .HasForeignKey(d => d.BrandUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("PLUS_BRAND_FK");
//
//             entity.HasOne(d => d.BundleU).WithMany(p => p.Plus)
//                 .HasForeignKey(d => d.BundleUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_BUNDLE_UID");
//
//             entity.HasOne(d => d.ClipU).WithMany(p => p.Plus)
//                 .HasForeignKey(d => d.ClipUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_CLIP_UID");
//
//             entity.HasOne(d => d.StorageMethodU).WithMany(p => p.Plus)
//                 .HasForeignKey(d => d.StorageMethodUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_STORAGE_METHOD_UID");
//         });
//
//         modelBuilder.Entity<PlusLine>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_PLUS_LINES_UID");
//
//             entity.ToTable("PLUS_LINES", "REF");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.LineUid).HasColumnName("LINE_UID");
//             entity.Property(e => e.PluUid).HasColumnName("PLU_UID");
//
//             entity.HasOne(d => d.LineU).WithMany(p => p.PlusLines)
//                 .HasForeignKey(d => d.LineUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_LINES_LINE_UID");
//
//             entity.HasOne(d => d.PluU).WithMany(p => p.PlusLines)
//                 .HasForeignKey(d => d.PluUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_LINES_PLU_UID");
//         });
//
//         modelBuilder.Entity<PlusNestingFk>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK__PLUS_NES__C5B19602D44AF6CD");
//
//             entity.ToTable("PLUS_NESTING_FK", tb => tb.HasComment("PLUS_NESTING_FK reference"));
//
//             entity.HasIndex(e => e.ChangeDt, "IX_PLUS_NESTING_FK_CHANGE_DT");
//
//             entity.HasIndex(e => e.CreateDt, "IX_PLUS_NESTING_FK_CREATE_DT");
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.BoxUid).HasColumnName("BOX_UID");
//             entity.Property(e => e.BundleCount).HasColumnName("BUNDLE_COUNT");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.PluUid).HasColumnName("PLU_UID");
//             entity.Property(e => e.Uid1c).HasColumnName("UID_1C");
//
//             entity.HasOne(d => d.BoxU).WithMany(p => p.PlusNestingFks)
//                 .HasForeignKey(d => d.BoxUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_NESTING_FK_BOX_UID");
//
//             entity.HasOne(d => d.PluU).WithMany(p => p.PlusNestingFks)
//                 .HasForeignKey(d => d.PluUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_NESTING_FK_PLU_UID");
//         });
//
//         modelBuilder.Entity<PlusTemplate>(entity =>
//         {
//             entity
//                 .HasNoKey()
//                 .ToTable("PLUS_TEMPLATES", "REF");
//
//             entity.Property(e => e.PluUid).HasColumnName("PLU_UID");
//             entity.Property(e => e.StorageMethodUid).HasColumnName("STORAGE_METHOD_UID");
//             entity.Property(e => e.TemplateUid).HasColumnName("TEMPLATE_UID");
//             entity.Property(e => e.Uid).HasColumnName("UID");
//         });
//
//         modelBuilder.Entity<PlusTemplatesFk>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK__PLUS_TEM__C5B19602F248E10F");
//
//             entity.ToTable("PLUS_TEMPLATES_FK", tb => tb.HasComment("PLUS_TEMPLATES_FK reference"));
//
//             entity.HasIndex(e => e.ChangeDt, "IX_PLUS_TEMPLATES_FK_CHANGE_DT");
//
//             entity.HasIndex(e => e.CreateDt, "IX_PLUS_TEMPLATES_FK_CREATE_DT");
//
//             entity.HasIndex(e => e.PluUid, "IX_PLUS_TEMPLATES_FK_PLU_UID");
//
//             entity.HasIndex(e => new { e.PluUid, e.TemplateUid }, "UQ_PLUS_TEMPLATE").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.PluUid).HasColumnName("PLU_UID");
//             entity.Property(e => e.TemplateUid).HasColumnName("TEMPLATE_UID");
//
//             entity.HasOne(d => d.TemplateU).WithMany(p => p.PlusTemplatesFks)
//                 .HasForeignKey(d => d.TemplateUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PLUS_TEMPLATES_FK_TEMPLATE_UID");
//         });
//
//         modelBuilder.Entity<Printer>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_PRINTERS_UID");
//
//             entity.ToTable("PRINTERS", "REF");
//
//             entity.HasIndex(e => e.Ip, "UQ_PRINTERS_IP").IsUnique();
//
//             entity.HasIndex(e => e.Name, "UQ_PRINTERS_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Ip)
//                 .HasMaxLength(15)
//                 .IsUnicode(false)
//                 .HasColumnName("IP");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(50)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Port).HasColumnName("PORT");
//             entity.Property(e => e.ProductionSiteUid).HasColumnName("PRODUCTION_SITE_UID");
//             entity.Property(e => e.Type)
//                 .HasMaxLength(15)
//                 .HasColumnName("TYPE");
//
//             entity.HasOne(d => d.ProductionSiteU).WithMany(p => p.Printers)
//                 .HasForeignKey(d => d.ProductionSiteUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_PRINTERS_PRODUCTION_SITE_UID");
//         });
//
//         modelBuilder.Entity<ProductionSite>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_PRODUCTION_SITES_UID");
//
//             entity.ToTable("PRODUCTION_SITES", "REF");
//
//             entity.HasIndex(e => e.Address, "UQ_PRODUCTION_SITES_ADDRESS").IsUnique();
//
//             entity.HasIndex(e => e.Name, "UQ_PRODUCTION_SITES_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.Address)
//                 .HasMaxLength(512)
//                 .HasColumnName("ADDRESS");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(150)
//                 .HasColumnName("NAME");
//         });
//
//         modelBuilder.Entity<StorageMethod>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_STORAGE_METHODS_UID");
//
//             entity.ToTable("STORAGE_METHODS", "REF");
//
//             entity.HasIndex(e => e.Name, "UQ_STORAGE_METHODS_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(32)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Zpl)
//                 .HasMaxLength(1024)
//                 .IsUnicode(false)
//                 .HasColumnName("ZPL");
//         });
//
//         modelBuilder.Entity<Template>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_TEMPLATES_UID");
//
//             entity.ToTable("TEMPLATES", "REF");
//
//             entity.HasIndex(e => e.Name, "UQ_TEMPLATES_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.Body).HasColumnName("BODY");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(64)
//                 .HasColumnName("NAME");
//         });
//
//         modelBuilder.Entity<User>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_USERS_UID");
//
//             entity.ToTable("USERS", "REF");
//
//             entity.HasIndex(e => e.Name, "UQ_USERS_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.LoginDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("LOGIN_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(128)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.ProductionSiteUid).HasColumnName("PRODUCTION_SITE_UID");
//
//             entity.HasOne(d => d.ProductionSiteU).WithMany(p => p.Users)
//                 .HasForeignKey(d => d.ProductionSiteUid)
//                 .HasConstraintName("FK_USERS_PRODUCTION_SITE_UID");
//         });
//
//         modelBuilder.Entity<UsersClaimsFk>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_USERS_CLAIMS_FK_UID");
//
//             entity.ToTable("USERS_CLAIMS_FK", "REF");
//
//             entity.HasIndex(e => new { e.UserUid, e.ClaimUid }, "UQ_USERS_CLAIMS_FK_USER_CLAIM_UID").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ClaimUid).HasColumnName("CLAIM_UID");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.UserUid).HasColumnName("USER_UID");
//
//             entity.HasOne(d => d.ClaimU).WithMany(p => p.UsersClaimsFks)
//                 .HasForeignKey(d => d.ClaimUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_USERS_CLAIMS_FK_CLAIM_UID");
//
//             entity.HasOne(d => d.UserU).WithMany(p => p.UsersClaimsFks)
//                 .HasForeignKey(d => d.UserUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_USERS_CLAIMS_FK_USER_UID");
//         });
//
//         modelBuilder.Entity<ViewLabel>(entity =>
//         {
//             entity
//                 .HasNoKey()
//                 .ToView("VIEW_LABELS", "PRINT");
//
//             entity.Property(e => e.BarcodeBottom)
//                 .HasMaxLength(128)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE_BOTTOM");
//             entity.Property(e => e.BarcodeRight)
//                 .HasMaxLength(128)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE_RIGHT");
//             entity.Property(e => e.BarcodeTop)
//                 .HasMaxLength(128)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE_TOP");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.IsCheckWeight).HasColumnName("IS_CHECK_WEIGHT");
//             entity.Property(e => e.Line)
//                 .HasMaxLength(64)
//                 .HasColumnName("LINE");
//             entity.Property(e => e.PluName)
//                 .HasMaxLength(150)
//                 .HasColumnName("PLU_NAME");
//             entity.Property(e => e.PluNumber).HasColumnName("PLU_NUMBER");
//             entity.Property(e => e.ProdDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("PROD_DT");
//             entity.Property(e => e.Uid).HasColumnName("UID");
//             entity.Property(e => e.Warehouse)
//                 .HasMaxLength(128)
//                 .HasColumnName("WAREHOUSE");
//         });
//
//         modelBuilder.Entity<ViewPallet>(entity =>
//         {
//             entity
//                 .HasNoKey()
//                 .ToView("VIEW_PALLETS", "PRINT");
//
//             entity.Property(e => e.Barcode)
//                 .HasMaxLength(32)
//                 .IsUnicode(false)
//                 .HasColumnName("BARCODE");
//             entity.Property(e => e.Counter).HasColumnName("COUNTER");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.LabelCount).HasColumnName("LABEL_COUNT");
//             entity.Property(e => e.LineName)
//                 .HasMaxLength(64)
//                 .HasColumnName("LINE_NAME");
//             entity.Property(e => e.PalletMan)
//                 .HasMaxLength(194)
//                 .HasColumnName("PALLET_MAN");
//             entity.Property(e => e.PluName)
//                 .HasMaxLength(156)
//                 .HasColumnName("PLU_NAME");
//             entity.Property(e => e.ProdDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("PROD_DT");
//             entity.Property(e => e.Uid).HasColumnName("UID");
//             entity.Property(e => e.WarehouseName)
//                 .HasMaxLength(128)
//                 .HasColumnName("WAREHOUSE_NAME");
//             entity.Property(e => e.WeightBrut)
//                 .HasColumnType("decimal(12, 3)")
//                 .HasColumnName("WEIGHT_BRUT");
//             entity.Property(e => e.WeightNetto)
//                 .HasColumnType("decimal(10, 3)")
//                 .HasColumnName("WEIGHT_NETTO");
//         });
//
//         modelBuilder.Entity<ViewTablesSize>(entity =>
//         {
//             entity
//                 .HasNoKey()
//                 .ToView("VIEW_TABLES_SIZES", "diag");
//
//             entity.Property(e => e.Filename)
//                 .HasMaxLength(128)
//                 .HasColumnName("FILENAME");
//             entity.Property(e => e.RowsCount).HasColumnName("ROWS_COUNT");
//             entity.Property(e => e.Schema)
//                 .HasMaxLength(128)
//                 .HasColumnName("SCHEMA");
//             entity.Property(e => e.Table)
//                 .HasMaxLength(128)
//                 .HasColumnName("TABLE");
//             entity.Property(e => e.Uid).HasColumnName("UID");
//             entity.Property(e => e.UsedSpaceMb).HasColumnName("USED_SPACE_MB");
//         });
//
//         modelBuilder.Entity<Warehouse>(entity =>
//         {
//             entity.HasKey(e => e.Uid).HasName("PK_WAREHOUSES_UID");
//
//             entity.ToTable("WAREHOUSES", "REF");
//
//             entity.HasIndex(e => e.Name, "UQ_WAREHOUSES_NAME").IsUnique();
//
//             entity.Property(e => e.Uid)
//                 .ValueGeneratedNever()
//                 .HasColumnName("UID");
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(128)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.ProductionSitesUid).HasColumnName("PRODUCTION_SITES_UID");
//
//             entity.HasOne(d => d.ProductionSitesU).WithMany(p => p.Warehouses)
//                 .HasForeignKey(d => d.ProductionSitesUid)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_WAREHOUSES_PRODUCTION_SITE_UID");
//         });
//
//         modelBuilder.Entity<ZplResource>(entity =>
//         {
//             entity
//                 .HasNoKey()
//                 .ToTable("ZPL_RESOURCES", "PRINT");
//
//             entity.Property(e => e.ChangeDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CHANGE_DT");
//             entity.Property(e => e.CreateDt)
//                 .HasColumnType("datetime")
//                 .HasColumnName("CREATE_DT");
//             entity.Property(e => e.Name)
//                 .HasMaxLength(64)
//                 .HasColumnName("NAME");
//             entity.Property(e => e.Uid).HasColumnName("UID");
//             entity.Property(e => e.Zpl).HasColumnName("ZPL");
//         });
//
//         OnModelCreatingPartial(modelBuilder);
//     }
//
//     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
// }

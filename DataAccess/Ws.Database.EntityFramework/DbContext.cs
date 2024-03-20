using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework;

public class WsDbContext : DbContext
{
    public DbSet<ZplResource> ZplResources { get; set; }
    public DbSet<PalletMan> PalletMen { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<ProductionSite> ProductionSites { get; set; }
    public DbSet<StorageMethod> StorageMethods { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<Box> Boxes { get; set; }
    public DbSet<Clip> Clips { get; set; }
    public DbSet<Bundle> Bundles { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=172.16.2.5;Database=WEIGHT;User Id=Developer;Password=Hz&V5Gnq4d4584;TrustServerCertificate=true;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Claims)
            .WithMany(e => e.Users)
            .UsingEntity(
            "USERS_СLAIMS_FK",
            l => l.HasOne(typeof(Claim))
                .WithMany()
                .HasForeignKey("CLAIM_UID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(nameof(Claim.Uid)),
            r => r.HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("USER_UID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(nameof(User.Uid)),
            j => j.HasKey("CLAIM_UID", "USER_UID"));

    }
}
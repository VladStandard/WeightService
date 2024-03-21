namespace Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

[Table(SqlTables.ProductionSites)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.ProductionSites}_NAME", IsUnique = true)]
[Index(nameof(Address), Name = $"UQ_{SqlTables.ProductionSites}_ADDRESS", IsUnique = true)]
public sealed class ProductionSiteEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 64 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("ADDRESS")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 128 characters")]
    public string Address { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    // public virtual ICollection<PrinterEntity> Printers { get; set; } = new List<PrinterEntity>();
    //
    // public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    //
    // public virtual ICollection<WarehouseEntity> Warehouses { get; set; } = new List<WarehouseEntity>();
}

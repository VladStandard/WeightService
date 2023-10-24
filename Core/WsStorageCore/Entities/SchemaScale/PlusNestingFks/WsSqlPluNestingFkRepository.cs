using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PlusNestingFks;

public sealed class WsSqlPluNestingFkRepository : WsSqlTableRepositoryBase<WsSqlPluNestingFkEntity>
{
    private WsSqlBoxRepository ContextBox { get; } = new();
    private WsSqlPluRepository ContextPlu { get; } = new();
    
    public WsSqlPluNestingFkEntity GetNewItem()
    {
        WsSqlPluNestingFkEntity item = SqlCore.GetItemNewEmpty<WsSqlPluNestingFkEntity>();
        item.Box = ContextBox.GetNewItem();
        item.Plu = ContextPlu.GetNewItem();
        return item;
    }

    public WsSqlViewPluNestingModel GetNewView() => new();

    public IEnumerable<WsSqlPluNestingFkEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerable<WsSqlPluNestingFkEntity>(sqlCrudConfig).OrderBy(x => x.Plu.Number);
    }
    
    public IEnumerable<WsSqlPluNestingFkEntity> GetEnumerableByPluUidActual(WsSqlPluEntity plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudActual();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluNestingFkEntity.Plu), plu));
        return SqlCore.GetEnumerable<WsSqlPluNestingFkEntity>(sqlCrudConfig);
    }

    public WsSqlPluNestingFkEntity GetByPluAndUid1C(WsSqlPluEntity plu, Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlPluNestingFkEntity.Uid1C), uid1C),
            SqlRestrictions.EqualFk(nameof(WsSqlPluNestingFkEntity.Plu), plu)
        });
        
        return SqlCore.GetItemByCrud<WsSqlPluNestingFkEntity>(sqlCrudConfig);
    }
    public WsSqlPluNestingFkEntity GetDefaultByPlu(WsSqlPluEntity plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlPluNestingFkEntity.IsDefault), true),
            SqlRestrictions.EqualFk(nameof(WsSqlPluTemplateFkEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<WsSqlPluNestingFkEntity>(sqlCrudConfig);
    }

    public WsSqlPluNestingFkEntity GetByAttachmentsCountAndPlu(WsSqlPluEntity plu, short attachmentsCount)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlPluNestingFkEntity.BundleCount), attachmentsCount),
            SqlRestrictions.EqualFk(nameof(WsSqlPluNestingFkEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<WsSqlPluNestingFkEntity>(sqlCrudConfig);
    }
}
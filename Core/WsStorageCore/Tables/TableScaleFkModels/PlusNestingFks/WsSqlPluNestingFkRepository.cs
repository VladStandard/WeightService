namespace WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

public sealed class WsSqlPluNestingFkRepository : WsSqlTableRepositoryBase<WsSqlPluNestingFkModel>
{
    private WsSqlBoxRepository ContextBox { get; } = new();
    private WsSqlPluRepository ContextPlu { get; } = new();
    
    public WsSqlPluNestingFkModel GetNewItem()
    {
        WsSqlPluNestingFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluNestingFkModel>();
        item.Box = ContextBox.GetNewItem();
        item.Plu = ContextPlu.GetNewItem();
        return item;
    }

    public WsSqlViewPluNestingModel GetNewView() => new();

    public IEnumerable<WsSqlPluNestingFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerableNotNullable<WsSqlPluNestingFkModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlPluNestingFkModel> GetEnumerableByPluUidActual(WsSqlPluModel plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudActual();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluNestingFkModel.Plu), plu));
        return SqlCore.GetEnumerableNotNullable<WsSqlPluNestingFkModel>(sqlCrudConfig);
    }

    public WsSqlPluNestingFkModel GetByPluAndUid1C(WsSqlPluModel plu, Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlPluNestingFkModel.Uid1C), uid1C),
            SqlRestrictions.EqualFk(nameof(WsSqlPluNestingFkModel.Plu), plu)
        });
        
        return SqlCore.GetItemByCrud<WsSqlPluNestingFkModel>(sqlCrudConfig);
    }
    public WsSqlPluNestingFkModel GetDefaultByPlu(WsSqlPluModel plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlPluNestingFkModel.IsDefault), true),
            SqlRestrictions.EqualFk(nameof(WsSqlPluTemplateFkModel.Plu), plu)
        });
        return SqlCore.GetItemByCrud<WsSqlPluNestingFkModel>(sqlCrudConfig);
    }

    public WsSqlPluNestingFkModel GetByAttachmentsCountAndPlu(WsSqlPluModel plu, short attachmentsCount)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlPluNestingFkModel.BundleCount), attachmentsCount),
            SqlRestrictions.EqualFk(nameof(WsSqlPluNestingFkModel.Plu), plu)
        });
        return SqlCore.GetItemByCrud<WsSqlPluNestingFkModel>(sqlCrudConfig);
    }
}
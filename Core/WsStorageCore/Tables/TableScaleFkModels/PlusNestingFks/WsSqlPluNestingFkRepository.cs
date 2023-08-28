namespace WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

public sealed class WsSqlPluNestingFkRepository : WsSqlTableRepositoryBase<WsSqlPluNestingFkModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlBoxRepository ContextBox { get; } = new();
    private WsSqlPluRepository ContextPlu { get; } = new();
    private WsSqlPluBundleFkRepository ContextPluBundle { get; } = new();

    #endregion

    #region Public and private methods

    public WsSqlPluNestingFkModel GetNewItem()
    {
        WsSqlPluNestingFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluNestingFkModel>();
        item.Box = ContextBox.GetNewItem();
        item.PluBundle = ContextPluBundle.GetNewItem();
        return item;
    }

    public WsSqlViewPluNestingModel GetNewView() => new();

    /// <summary>
    /// Force update list.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="sqlCrudConfig"></param>
    public IEnumerable<WsSqlPluNestingFkModel> UpdatePluNestingFks(WsSqlCrudConfigModel sqlCrudConfig, 
        out IEnumerable<WsSqlPluNestingFkModel> pluNestingFks) =>
        pluNestingFks = GetEnumerable(sqlCrudConfig);

    /// <summary>
    /// Get item by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public WsSqlPluNestingFkModel GetPluNestingFk(List<WsSqlPluNestingFkModel> pluNestingFks, WsSqlPluModel plu, WsSqlBundleModel bundle, WsSqlBoxModel box)
    {
        WsSqlPluNestingFkModel pluNestingFk = pluNestingFks.Find(item => Equals(item.PluBundle.Plu, plu) &&
                                                                    Equals(item.PluBundle.Bundle, bundle) && Equals(item.Box, box));
        return pluNestingFk.IsExists ? pluNestingFk : new();
    }

    /// <summary>
    /// Get item by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(List<WsSqlPluNestingFkModel> pluNestingFks, WsSqlPluModel plu, WsSqlBundleModel bundle, WsSqlBoxModel box) =>
        GetPluNestingFk(pluNestingFks, plu, bundle, box).BundleCount;

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFk"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(WsSqlPluNestingFkModel pluNestingFk) => pluNestingFk.BundleCount;

    public IEnumerable<WsSqlPluNestingFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPluNestingFkModel> items = new ();
        if (string.IsNullOrEmpty(sqlCrudConfig.NativeQuery)) sqlCrudConfig.NativeQuery = GetQuery(false);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(sqlCrudConfig);
        foreach (object obj in objects)
        {
            if (obj is object[] { Length: 45 } item)
            {
                if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                {
                    WsSqlPluBundleFkModel pluBundle = new();
                    // -- [DB_SCALES].[PLUS_BUNDLES_FK] | 11 - 16
                    if (Guid.TryParse(Convert.ToString(item[11]), out Guid pluBundleUid))
                    {
                        pluBundle.IdentityValueUid = pluBundleUid;
                        pluBundle.CreateDt = Convert.ToDateTime(item[12]);
                        pluBundle.ChangeDt = Convert.ToDateTime(item[13]);
                        pluBundle.IsMarked = Convert.ToBoolean(item[14]);
                    }
    
                    // -- [DB_SCALES].[PLUS] | 17 - 30
                    if (Guid.TryParse(Convert.ToString(item[17]), out Guid pluUid))
                    {
                        pluBundle.Plu.IdentityValueUid = pluUid;
                        pluBundle.Plu.CreateDt = Convert.ToDateTime(item[18]);
                        pluBundle.Plu.ChangeDt = Convert.ToDateTime(item[19]);
                        pluBundle.Plu.IsMarked = Convert.ToBoolean(item[20]);
                        pluBundle.Plu.Number = Convert.ToInt16(item[21]);
                        pluBundle.Plu.Name = Convert.ToString(item[22]);
                        pluBundle.Plu.FullName = Convert.ToString(item[23]);
                        pluBundle.Plu.Description = Convert.ToString(item[24]);
                        pluBundle.Plu.ShelfLifeDays = Convert.ToByte(item[25]);
                        pluBundle.Plu.Gtin = Convert.ToString(item[26]);
                        pluBundle.Plu.Ean13 = Convert.ToString(item[27]);
                        pluBundle.Plu.Itf14 = Convert.ToString(item[28]);
                        pluBundle.Plu.IsCheckWeight = Convert.ToBoolean(item[29]);
                    }
    
                    // -- [DB_SCALES].[BUNDLES] | 30 - 35
                    if (Guid.TryParse(Convert.ToString(item[30]), out Guid bundleUid))
                    {
                        pluBundle.Bundle.IdentityValueUid = bundleUid;
                        pluBundle.Bundle.CreateDt = Convert.ToDateTime(item[31]);
                        pluBundle.Bundle.ChangeDt = Convert.ToDateTime(item[32]);
                        pluBundle.Bundle.IsMarked = Convert.ToBoolean(item[33]);
                        pluBundle.Bundle.Name = Convert.ToString(item[34]);
                        pluBundle.Bundle.Weight = Convert.ToDecimal(item[35]);
                    }
    
                    WsSqlBoxModel box = new();
                    // -- [DB_SCALES].[BOXES] | 36 - 41
                    if (Guid.TryParse(Convert.ToString(item[36]), out Guid boxUid))
                    {
                        box.IdentityValueUid = boxUid;
                        box.CreateDt = Convert.ToDateTime(item[37]);
                        box.ChangeDt = Convert.ToDateTime(item[38]);
                        box.IsMarked = Convert.ToBoolean(item[39]);
                        box.Name = Convert.ToString(item[40]);
                        box.Weight = Convert.ToDecimal(item[41]);
                    }
    
                    // -- UID_1C | 42 - 44
                    if (Guid.TryParse(Convert.ToString(item[42]), out Guid pluUid1C))
                        pluBundle.Plu.Uid1C = pluUid1C;
                    if (Guid.TryParse(Convert.ToString(item[43]), out Guid boxUid1C))
                        box.Uid1C = boxUid1C;
                    if (Guid.TryParse(Convert.ToString(item[44]), out Guid bundleUid1C))
                        pluBundle.Bundle.Uid1C = bundleUid1C;
                    // All.
                    items.Add(new()
                    {
                        IdentityValueUid = uid,
                        CreateDt = Convert.ToDateTime(item[1]),
                        ChangeDt = Convert.ToDateTime(item[2]),
                        IsMarked = Convert.ToBoolean(item[3]),
                        IsDefault = Convert.ToBoolean(item[4]),
                        BundleCount = Convert.ToInt16(item[5]),
                        WeightMax = Convert.ToDecimal(item[6]),
                        WeightMin = Convert.ToDecimal(item[7]),
                        WeightNom = Convert.ToDecimal(item[8]),
                        PluBundle = pluBundle,
                        Box = box
                    });
                }
            }
            else
                throw new($"Exception length in {nameof(GetEnumerable)} for native query!");
        }
        return items;
    }
    
    public IEnumerable<WsSqlPluNestingFkModel> GetEnumerableByPluUid(Guid? uid)
    {
        uid ??= Guid.Empty;
        WsSqlCrudConfigModel sqlCrudConfig = new()
        {
            NativeParameters = new() { new("P_UID", uid) },
            NativeQuery = GetQuery(true)
        };
        return GetEnumerable(sqlCrudConfig);
    }

    public IEnumerable<WsSqlPluNestingFkModel> GetEnumerableByPluNumber(short number) => 
        GetEnumerableByPluUid(ContextPlu.GetItemByNumber(number).IdentityValueUid);
    
    
    private static string GetQuery(bool isSetPluUid) => WsSqlQueries.TrimQuery(@$"
-- PLUS_NESTING_FK SELECT AS OBJECTS
SELECT 
-- [DB_SCALES].[PLUS_NESTING_FK] | 0 - 10
 [PNFK].[UID]
,[PNFK].[CREATE_DT]
,[PNFK].[CHANGE_DT]
,[PNFK].[IS_MARKED]
,[PNFK].[IS_DEFAULT]
,[PNFK].[BUNDLE_COUNT]
,[PNFK].[WEIGHT_MAX]
,[PNFK].[WEIGHT_MIN]
,[PNFK].[WEIGHT_NOM]
,[PNFK].[PLU_BUNDLE_FK]
,[PNFK].[BOX_UID]
-- [DB_SCALES].[PLUS_BUNDLES_FK] | 11 - 16
,[PBFK].[UID] [PLU_BUNDLE_FK_UID]
,[PBFK].[CREATE_DT] [PLU_BUNDLE_FK_CREATE_DT]
,[PBFK].[CHANGE_DT] [PLU_BUNDLE_FK_CHANGE_DT]
,[PBFK].[IS_MARKED] [PLU_BUNDLE_FK_IS_MARKED]
,[PBFK].[PLU_UID] [PLU_BUNDLE_FK_PLU_UID]
,[PBFK].[BUNDLE_UID] [PLU_BUNDLE_FK_BUNDLE_UID]
-- [DB_SCALES].[PLUS] | 17 - 30
,[P].[UID] [PLU_UID]
,[P].[CREATE_DT][PLU_CREATE_DT]
,[P].[CHANGE_DT] [PLU_CHANGE_DT]
,[P].[IS_MARKED] [PLU_IS_MARKED]
,[P].[NUMBER] [PLU_NUMBER]
,[P].[NAME] [PLU_NAME]
,[P].[FULL_NAME] [PLU_FULL_NAME]
,[P].[DESCRIPTION] [PLU_DESCRIPTION]
,[P].[SHELF_LIFE_DAYS] [PLU_SHELF_LIFE_DAYS]
,[P].[GTIN] [PLU_GTIN]
,[P].[EAN13] [PLU_EAN13]
,[P].[ITF14] [PLU_ITF14]
,[P].[IS_CHECK_WEIGHT] [PLU_IS_CHECK_WEIGHT]
-- [DB_SCALES].[BUNDLES] | 30 - 35
,[BU].[UID] [BUNDLE_UID]
,[BU].[CREATE_DT] [BUNDLE_CREATE_DT]
,[BU].[CHANGE_DT] [BUNDLE_CHANGE_DT]
,[BU].[IS_MARKED] [BUNDLE_IS_MARKED]
,[BU].[NAME] [BUNDLE_NAME]
,[BU].[WEIGHT] [BUNDLE_WEIGHT]
-- [DB_SCALES].[BOXES] | 36 - 41
,[B].[UID] [BOX_UID]
,[B].[CREATE_DT] [BOX_CREATE_DT]
,[B].[CHANGE_DT] [BOX_CHANGE_DT]
,[B].[IS_MARKED] [BOX_IS_MARKED]
,[B].[NAME] [BOX_NAME]
,[B].[WEIGHT] [BOX_WEIGHT]
-- UID_1C | 42 - 44
,[P].[UID_1C] [PLU_UID_1C]
,[B].[UID_1C] [BOX_UID_1C]
,[BU].[UID_1C] [BUNDLE_UID_1C]
FROM [DB_SCALES].[PLUS_NESTING_FK] [PNFK]
LEFT JOIN [DB_SCALES].[PLUS_BUNDLES_FK] [PBFK] ON [PNFK].[PLU_BUNDLE_FK] = [PBFK].[UID]
LEFT JOIN [DB_SCALES].[PLUS] [P] ON [PBFK].[PLU_UID] = [P].[UID]
LEFT JOIN [DB_SCALES].[BUNDLES] [BU] ON [PBFK].[BUNDLE_UID] = [BU].[UID]
LEFT JOIN [DB_SCALES].[BOXES] [B] ON [PNFK].[BOX_UID] = [B].[UID]
{(isSetPluUid ? "WHERE [P].[UID] = :P_UID" : "")}
ORDER BY [P].[NUMBER];");
                
    #endregion
}
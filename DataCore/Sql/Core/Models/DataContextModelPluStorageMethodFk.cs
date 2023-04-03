// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace DataCore.Sql.Core.Models;

public partial class DataContextModel
{
    #region Public and private methods - PluStorageMethodFk

    /// <summary>
    /// Force update list PluStorageMethodFks.
    /// </summary>
    /// <param name="sqlCrudConfig"></param>
    public List<PluStorageMethodFkModel> UpdatePluStorageMethodFks(SqlCrudConfigModel sqlCrudConfig) => 
        PluStorageMethodsFks = GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig);

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <returns></returns>
    public PluStorageMethodModel GetPluStorageMethod(PluModel plu)
    {
        PluStorageMethodFkModel pluStorageMethodFk = new();
        if (PluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = PluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk.Method;
    }

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <returns></returns>
    public TemplateResourceModel GetPluStorageResource(PluModel plu)
    {
        PluStorageMethodFkModel pluStorageMethodFk = new();
        if (PluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = PluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk.Resource;
    }

    /// <summary>
    /// Get item PluStorageMethodFk by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <returns></returns>
    public PluStorageMethodFkModel GetPluStorageMethodFk(PluModel plu)
    {
        PluStorageMethodFkModel pluStorageMethodFk = new();
        if (PluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = PluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk;
    }

    #endregion
}
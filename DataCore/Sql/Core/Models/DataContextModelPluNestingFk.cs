// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.Core.Models;

public partial class DataContextModel
{
    #region Public and private methods - PluNestingFk

    /// <summary>
    /// Force update list PluStorageMethodFks.
    /// </summary>
    /// <param name="sqlCrudConfig"></param>
    public List<PluNestingFkModel> UpdatePluNestingFks(SqlCrudConfigModel sqlCrudConfig) => 
        PluNestingFks = GetListNotNullablePlusNestingFks(sqlCrudConfig);

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public PluNestingFkModel GetPluNestingFk(PluModel plu, BundleModel bundle, BoxModel box)
    {
        PluNestingFkModel pluNestingFk = PluNestingFks.Find(item => Equals(item.PluBundle.Plu, plu) && 
            Equals(item.PluBundle.Bundle, bundle) && Equals(item.Box, box));
        return pluNestingFk.IsExists ? pluNestingFk : new();
    }

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(PluModel plu, BundleModel bundle, BoxModel box) => 
        GetPluNestingFk(plu, bundle, box).BundleCount;

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFk"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(PluNestingFkModel pluNestingFk) => 
        pluNestingFk.BundleCount;

    #endregion
}
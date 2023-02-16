// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    /// <summary>
    /// Update the record in the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="importUid"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    private bool UpdateItemDb<T>(Response1cShortModel response, Guid importUid, T itemXml, T? itemDb, bool isCounter) where T : ISqlTable
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(importUid));
        }
        else
            AddResponse1cException(response, importUid, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    private bool UpdateBoxDb(Response1cShortModel response, PluModel pluXml, BoxModel? boxDb, bool isCounter)
    {
        if (boxDb is null || boxDb.IsNew) return false;
        boxDb.UpdateProperties(pluXml);
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(boxDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(pluXml.Uid1c));
        }
        else
            AddResponse1cException(response, pluXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    private bool UpdateBundleDb(Response1cShortModel response, PluModel pluXml, BundleModel? bundleDb, bool isCounter)
    {
        if (bundleDb is null || bundleDb.IsNew) return false;
        bundleDb.UpdateProperties(pluXml);
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(bundleDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(pluXml.Uid1c));
        }
        else
            AddResponse1cException(response, pluXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    private bool UpdateClipDb(Response1cShortModel response, PluModel pluXml, ClipModel? clipDb, bool isCounter)
    {
        if (clipDb is null || clipDb.IsNew) return false;
        clipDb.UpdateProperties(pluXml);
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(clipDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(pluXml.Uid1c));
        }
        else
            AddResponse1cException(response, pluXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Update the PLU record in the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <param name="pluDb"></param>
    /// <param name="isCounter"></param>
    private bool UpdatePluDb(Response1cShortModel response, PluModel pluXml, PluModel? pluDb, bool isCounter)
    {
        if (pluDb is null || pluDb.IsNew) return false;
        pluDb.Identity = pluXml.Identity;
        pluDb.UpdateProperties(pluXml);
        // Native update -> Be careful, good luck.
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.ExecQueryNative(
            SqlQueries.UpdatePlu, new List<SqlParameter>
            {
                new("uid", pluXml.IdentityValueUid),
                new("code", pluDb.Code),
                new("number", pluDb.Number),
            });
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(pluXml.IdentityValueUid));
        }
        else
        {
            AddResponse1cException(response, pluXml.IdentityValueUid, dbUpdate.Exception);
        }
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="importUid"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    private bool SaveItemDb<T>(Response1cShortModel response, Guid importUid, T item, bool isCounter) where T : ISqlTable
    {
        (bool IsOk, Exception? Exception) dbSave = DataContext.DataAccess.Save(item, item.Identity);
        // Add was success.
        if (dbSave.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(importUid));
        }
        else
            AddResponse1cException(response, importUid, dbSave.Exception);
        return dbSave.IsOk;
    }

    #endregion
}
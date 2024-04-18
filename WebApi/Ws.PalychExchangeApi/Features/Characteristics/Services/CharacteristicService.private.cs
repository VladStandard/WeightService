using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.PalychExchangeApi.Features.Characteristics.Services.Models;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services;

internal sealed partial class CharacteristicService
{
    #region Resolve uniques Db

    private void ResolveUniqueDb(List<GroupedCharacteristic> dtos)
    {
        HashSet<Guid> boxUids = dtos.Select(x => x.BoxUid).ToHashSet();
        HashSet<Guid> pluUids = dtos.Select(x => x.PluUid).ToHashSet();
        HashSet<Guid> characteristicUids = dtos.Select(x => x.Uid).ToHashSet();
        HashSet<short> bundleCounts = dtos.Select(x => x.BundleCount).ToHashSet();

        List<GroupedCharacteristic> existingPairs = DbContext.Characteristics
            .Where(i =>
                !characteristicUids.Contains(i.Id) &&
                boxUids.Contains(i.BoxId) &&
                bundleCounts.Contains(i.BundleCount) &&
                pluUids.Contains(i.PluId)
            ).Select(i =>
                new GroupedCharacteristic
                {
                    Uid = i.Id,
                    PluUid = i.PluId,
                    BoxUid = i.BoxId,
                    BundleCount = i.BundleCount,
                    IsDelete = false,
                    Name = ""
                })
            .ToList();


        dtos.RemoveAll(dto =>
        {
            if (!existingPairs.Any(uniq =>
                    dto.Uid == uniq.PluUid &&
                    dto.BoxUid == uniq.BoxUid &&
                    dto.BundleCount == uniq.BundleCount &&
                    dto.Uid != uniq.Uid
                )) return false;
            OutputDto.AddError(dto.Uid, "Характеристика - (Box, Plu, BundleCount) не уникальна (бд)");
            return true;
        });
    }

    private void ResolveIsWeightDb(List<GroupedCharacteristic> dtos)
    {
        HashSet<Guid> charUids = dtos.Select(x => x.Uid).ToHashSet();

        List<Guid> existingPairs = DbContext.Characteristics
            .Join(
                DbContext.Plus, characteristic => characteristic.PluId, plu => plu.Id,
                (characteristic, plu) => new { Characteristic = characteristic, Plu = plu }
            )
            .Where(pair => charUids.Contains(pair.Characteristic.Id) && pair.Plu.IsWeight == true)
            .Select(pair => pair.Characteristic.Id)
            .ToList();


        dtos.RemoveAll(dto =>
        {
            if (existingPairs.All(weightUid => dto.Uid != weightUid)) return false;
            OutputDto.AddError(dto.Uid, "Характеристика - принадлежит весовой плу");
            return true;
        });
    }

    #endregion

    private void SaveCharacteristics(IEnumerable<GroupedCharacteristic> dtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<CharacteristicEntity> characteristics = dtos.Select(i => i.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(characteristics, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
            });
            transaction.Commit();
            OutputDto.AddSuccess(characteristics.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(characteristics.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }

    private void DeleteCharacteristics(List<GroupedCharacteristic> dtos)
    {
        List<CharacteristicEntity> characteristicToDelete =
            dtos.Where(dto => dto.IsDelete)
                .Select(dto => dto.ToEntity(DateTime.MinValue))
                .ToList();

        if (characteristicToDelete.Count == 0) return;

        HashSet<Guid> deletedUid = characteristicToDelete.Select(i => i.Id).ToHashSet();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkDelete(characteristicToDelete);
            transaction.Commit();
            OutputDto.AddSuccess(deletedUid);
        }
        catch (Exception)
        {
            OutputDto.AddError(deletedUid, "Не предвиденная ошибка");
            transaction.Rollback();
        }
        finally
        {
            dtos.RemoveAll(brandDto => deletedUid.Contains(brandDto.Uid));
        }
    }
}
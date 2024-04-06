using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.PalychExchangeApi.Features.Characteristics.Services.Models;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services;


file record CharacteristicsUniqueFields
{
    public required Guid Uid { get; set; }
    public required Guid PluUid { get; set; }
    public required Guid BoxUid { get; set; }
    public required short BundleCount { get; set; }
}


internal sealed partial class CharacteristicService
{
    #region Resolve uniques Db

    private void ResolveUniqueDb(List<GroupedCharacteristic> dtos)
    {
        List<Guid> boxUids = dtos.Select(x => x.BoxUid).ToList();
        List<Guid> pluUids = dtos.Select(x => x.PluUid).ToList();
        List<Guid> characteristicUids = dtos.Select(x => x.Uid).ToList();
        List<short> bundleCounts = dtos.Select(x => x.BundleCount).ToList();


        List<CharacteristicsUniqueFields> existingPairs = dbContext.Characteristics
            .Where(i =>
                !characteristicUids.Contains(i.Id) &&
                boxUids.Contains(i.BoxId) &&
                bundleCounts.Contains(i.BundleCount) &&
                pluUids.Contains(i.PluId)
            ).Select(i =>
                new CharacteristicsUniqueFields
                {
                    Uid = i.Id,
                    PluUid = i.PluId,
                    BoxUid = i.BoxId,
                    BundleCount = i.BundleCount
                })
            .ToList();


        dtos.RemoveAll(characteristic =>
        {
            if (!existingPairs.Any(grouped =>
                    characteristic.Uid == grouped.PluUid &&
                    characteristic.BoxUid == grouped.BoxUid &&
                    characteristic.BundleCount == grouped.BundleCount &&
                    characteristic.Uid != grouped.Uid
                )) return false;
            OutputDto.AddError(characteristic.Uid, "Характеристика - (Box, Plu, BundleCount) не уникальна (бд)");
            return true;
        });
    }

    #endregion

    private void SaveCharacteristics(IEnumerable<GroupedCharacteristic> dtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<CharacteristicEntity> characteristics = dtos.Select(i => i.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(characteristics, options =>
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
}
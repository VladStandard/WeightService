using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;
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
    #region Resolve uniques local

    private void ResolveUniqueUidLocal(List<GroupedCharacteristic> dtos)
    {
        HashSet<Guid> duplicateUids = dtos
            .GroupBy(dto => dto.Uid)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToHashSet();

        dtos.RemoveAll(dto =>
        {
            if (!duplicateUids.Contains(dto.Uid))
                return false;
            OutputDto.AddError(dto.Uid, "PluUid - не уникален");
            return true;
        });
    }

    private void ResolveUniqueLocal(List<GroupedCharacteristic> dtos)
    {
        List<Guid> duplicates = dtos
            .GroupBy(dto => (dto.PluUid, dto.BoxUid, dto.BundleCount))
            .Where(group => group.Count() > 1)
            .SelectMany(group => group)
            .Select(dto => dto.Uid)
            .ToList();

        dtos.RemoveAll(dto =>
        {
            if (!duplicates.Contains(dto.Uid))
                return false;
            OutputDto.AddError(dto.Uid, "Характеристика - (Box, Plu, BundleCount) не уникальна");
            return true;
        });
    }

    #endregion

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

    #region Resolve exsists FK

    private void ResolveNotExistsBoxFkDb(List<GroupedCharacteristic> dtos)
    {
        HashSet<Guid> boxUidList = dtos.Select(i => i.BoxUid).ToHashSet();

        HashSet<Guid> existingBoxes = dbContext.Boxes
            .Where(box => boxUidList.Contains(box.Id))
            .Select(box => box.Id)
            .ToHashSet();


        dtos.RemoveAll(characteristic =>
        {
            if (existingBoxes.Contains(characteristic.BoxUid))
                return false;
            OutputDto.AddError(characteristic.Uid, "Коробка - не найдена");
            return true;
        });
    }

    #endregion

    #region Validation

    private IEnumerable<GroupedCharacteristic> FilterValidDtos(IEnumerable<GroupedCharacteristic> groupedCharacteristics) =>
        groupedCharacteristics.Where(IsCharacteristicDtoValid).ToList();

    private bool IsCharacteristicDtoValid(GroupedCharacteristic groupedCharacteristic)
    {
        ValidationResult validationResult = Validator.Validate(groupedCharacteristic);
        if (validationResult.IsValid) return true;

        OutputDto.AddError(groupedCharacteristic.Uid, validationResult.Errors.First().ErrorMessage);
        return false;
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
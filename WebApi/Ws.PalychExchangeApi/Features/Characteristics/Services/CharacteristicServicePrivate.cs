using FluentValidation.Results;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services;

internal sealed partial class CharacteristicService
{
    #region Resolve uniques local

    private void ResolveUniqueUidLocal(List<PluCharacteristicsDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];

        foreach (IGrouping<Guid, CharacteristicDto> dto in dtos.SelectMany(dto => dto.Characteristics).GroupBy(dto => dto.Uid))
        {
            if (dto.Count() <= 1) continue;
            duplicateUidSet.Add(dto.Key);
            OutputDto.AddError(dto.Select(i => i.Uid), "PluUid - не уникален");
        }

        dtos.ForEach(dto =>
            dto.Characteristics.RemoveAll(characteristic => !duplicateUidSet.Contains(characteristic.Uid))
        );
    }

    private void ResolveUniqueLocal(List<PluCharacteristicsDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];

        foreach (PluCharacteristicsDto plu in dtos)
        {
            foreach (IGrouping<(Guid, Guid, short), CharacteristicDto> dtoGroup in
                     plu.Characteristics.GroupBy(characteristic => (plu.Uid, characteristic.BoxUid, characteristic.BundleCount)))
            {
                if (dtoGroup.Count() <= 1) continue;
                duplicateUidSet.UnionWith(dtoGroup.Select(i => i.Uid));
            }
        }

        dtos.ForEach(dto =>
        {
            dto.Characteristics.RemoveAll(characteristic =>
            {
                if (!duplicateUidSet.Contains(characteristic.Uid)) return false;
                OutputDto.AddError(characteristic.Uid, "Характеристика - (Box, Plu, BundleCount) не уникальна");
                return true;
            });
        });
    }

    #endregion

    #region Resolve uniques Db

    //pass

    #endregion

    #region Resolve exsists FK

    private void ResolveNotExistsBoxFkDb(List<PluCharacteristicsDto> dtos)
    {
        HashSet<Guid> boxUidList = dtos
            .SelectMany(dto => dto.Characteristics.Select(dto2 => dto2.BoxUid))
            .ToHashSet();

        HashSet<Guid> existingBoxes = dbContext.Boxes
            .Where(box => boxUidList.Contains(box.Id))
            .Select(box => box.Id)
            .ToHashSet();

        List<CharacteristicDto> notExistsBoxesDto =
            dtos.SelectMany(dto => dto.Characteristics).Where(dto => !existingBoxes.Contains(dto.BoxUid)).ToList();
        OutputDto.AddError(notExistsBoxesDto.Select(i => i.Uid), "Коробка - не найдена");

        dtos.ForEach(dto =>
            dto.Characteristics.RemoveAll(
            characteristic => !existingBoxes.Contains(characteristic.BoxUid))
            );
    }

    #endregion

    #region Validation

    private IEnumerable<PluCharacteristicsDto> FilterValidDtos(IEnumerable<PluCharacteristicsDto> pluCharacteristicsDto)
    {
        return pluCharacteristicsDto.Select(dto =>
        {
            List<CharacteristicDto> validCharacteristics = dto.Characteristics.Where(IsCharacteristicDtoValid).ToList();
            return new PluCharacteristicsDto
            {
                Uid = dto.Uid,
                Characteristics = validCharacteristics.Any() ? validCharacteristics : []
            };
        }).Where(newDto => newDto.Characteristics.Any());
    }

    private bool IsCharacteristicDtoValid(CharacteristicDto characteristicDto)
    {
        ValidationResult validationResult = Validator.Validate(characteristicDto);
        if (validationResult.IsValid) return true;

        OutputDto.AddError(characteristicDto.Uid, validationResult.Errors.First().ErrorMessage);
        return false;
    }

    #endregion

    private void SaveCharacteristics(IEnumerable<PluCharacteristicsDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        // List<CharacteristicEntity> boxes = validDtos.Select(dto => dto.ToEntity(updateDt)).ToList();

        // using IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
        // try
        // {
        //     dbContext.BulkMerge(boxes, options =>
        //     {
        //         options.InsertIfNotExists = true;
        //         options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
        //         options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
        //     });
        //     transaction.Commit();
        //     OutputDto.AddSuccess(boxes.Select(i => i.Id).ToList());
        // }
        // catch (Exception)
        // {
        //     transaction.Rollback();
        //     OutputDto.AddError(boxes.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        // }
    }
}
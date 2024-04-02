using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.PalychExchangeApi.Features.Plus.Dto;

namespace Ws.PalychExchangeApi.Features.Plus.Services;

internal sealed partial class PluService
{
    #region Resolve uniques local

    private void ResolveUniqueNumberLocal(List<PluDto> dtos)
    {
        HashSet<short> duplicateNameSet = [];
        foreach (IGrouping<short, PluDto> dtoGroup in dtos.GroupBy(dto => dto.Number))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateNameSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Номер (внутри запроса) - не уникален");
        }
        dtos.RemoveAll(dto => duplicateNameSet.Contains(dto.Number));
    }

    private void ResolveUniqueUidLocal(List<PluDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];
        foreach (IGrouping<Guid, PluDto> dtoGroup in dtos.GroupBy(dto => dto.Uid))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateUidSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Uid (внутри запроса) - не уникален");
        }
        dtos.RemoveAll(brandDto => duplicateUidSet.Contains(brandDto.Uid));
    }

    #endregion

    #region Resolve uniques db

    private void ResolveNotExistClipFkDb(List<PluDto> dtos)
    {
        HashSet<Guid> bundleUidList = dtos.Select(dto => dto.BundleUid).ToHashSet();

        HashSet<Guid> existingBundles = dbContext.Bundles
            .Where(brand => bundleUidList.Contains(brand.Id))
            .Select(brand => brand.Id)
            .ToHashSet();

        List<PluDto> notExistsBundlesDto = dtos.Where(dto => !existingBundles.Contains(dto.Uid)).ToList();
        OutputDto.AddError(notExistsBundlesDto.Select(i => i.Uid), "Клипса - не найдена");

        dtos.RemoveAll(dto => !existingBundles.Contains(dto.BundleUid));
    }

    private void ResolveNotExistsBoxFkDb(List<PluDto> dtos)
    {
        HashSet<Guid> boxUidList = dtos.Select(dto => dto.BoxUid).ToHashSet();

        HashSet<Guid> existingBoxes = dbContext.Boxes
            .Where(box => boxUidList.Contains(box.Id))
            .Select(box => box.Id)
            .ToHashSet();

        List<PluDto> notExistsBoxesDto = dtos.Where(dto => !existingBoxes.Contains(dto.Uid)).ToList();
        OutputDto.AddError(notExistsBoxesDto.Select(i => i.Uid), "Коробка - не найдена");

        dtos.RemoveAll(dto => !existingBoxes.Contains(dto.BoxUid));
    }

    private void ResolveNotExistsBrandFkDb(List<PluDto> dtos)
    {
        HashSet<Guid> brandUidList = dtos.Select(dto => dto.BrandUid).ToHashSet();

        HashSet<Guid> existingBrands = dbContext.Brands
            .Where(brand => brandUidList.Contains(brand.Id))
            .Select(brand => brand.Id)
            .ToHashSet();

        List<PluDto> notExistsBrandsDto = dtos.Where(dto => !existingBrands.Contains(dto.Uid)).ToList();
        OutputDto.AddError(notExistsBrandsDto.Select(i => i.Uid), "Бренд - не найден");

        dtos.RemoveAll(dto => !existingBrands.Contains(dto.BrandUid));
    }

    private void ResolveNotExistsBundleFkDb(List<PluDto> dtos)
    {
        HashSet<Guid> bundleUidList = dtos.Select(dto => dto.BundleUid).ToHashSet();

        HashSet<Guid> existingBundles = dbContext.Bundles
            .Where(brand => bundleUidList.Contains(brand.Id))
            .Select(brand => brand.Id)
            .ToHashSet();

        List<PluDto> notExistsBundlesDto = dtos.Where(dto => !existingBundles.Contains(dto.Uid)).ToList();
        OutputDto.AddError(notExistsBundlesDto.Select(i => i.Uid), "Пакет - не найден");

        dtos.RemoveAll(dto => !existingBundles.Contains(dto.BundleUid));
    }

    #endregion

    private void SavePlus(IReadOnlyCollection<PluDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<PluEntity> plus = validDtos.Select(dto => dto.ToPluEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(plus, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
            });
            transaction.Commit();
            SaveNestings(validDtos);
            OutputDto.AddSuccess(plus.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(plus.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }

    private void SaveNestings(IEnumerable<PluDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<NestingEntity> nestings = validDtos.Select(dto => dto.ToNestingEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(nestings, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
                options.MergeKeepIdentity = true;
            });
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}
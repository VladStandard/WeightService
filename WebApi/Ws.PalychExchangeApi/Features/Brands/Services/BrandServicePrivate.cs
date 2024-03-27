using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands.Services;

internal partial class BrandService
{
    #region Resolve uniques local

    private void ResolveUniqueNameLocal(List<BrandDto> dtos)
    {
        HashSet<string> duplicateNameSet = [];
        foreach (IGrouping<string, BrandDto> dtoGroup in dtos.GroupBy(dto => dto.Name))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateNameSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Name не уникален");
        }
        dtos.RemoveAll(brandDto => duplicateNameSet.Contains(brandDto.Name));
    }

    private void ResolveUniqueUidLocal(List<BrandDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];
        foreach (IGrouping<Guid, BrandDto> dtoGroup in dtos.GroupBy(dto => dto.Uid))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateUidSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Uid не уникален");
        }
        dtos.RemoveAll(brandDto => duplicateUidSet.Contains(brandDto.Uid));
    }

    #endregion

    #region Resolve uniques db

    private void ResolveUniqueNameDb(List<BrandDto> dtos)
    {
        HashSet<string> namesList = dtos.Select(dto => dto.Name).ToHashSet();

        HashSet<string> existingNames = dbContext.Brands
            .Where(brand => namesList.Any(name => name.Equals(brand.Name)))
            .Select(brand => brand.Name)
            .ToHashSet();

        List<BrandDto> notUniqueBrandsDto = dtos.Where(dto => existingNames.Contains(dto.Name)).ToList();
        OutputDto.AddError(notUniqueBrandsDto.Select(i => i.Uid), "Нарушение уникальности в бд");

        dtos.RemoveAll(dto => existingNames.Contains(dto.Name));
    }

    #endregion

    private void SaveBrands(List<BrandDto> validDtos)
    {
        ResolveUniqueNameDb(validDtos);
        if (validDtos.Count == 0) return;

        List<BrandEntity> brands = validDtos.Select(dto => dto.ToEntity()).ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(brands);
            transaction.Commit();
            dbContext.SaveChanges();
            OutputDto.AddSuccess(brands.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(brands.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
    private void DeleteBrands(List<BrandDto> dtos)
    {
        List<BrandEntity> brandsToDelete = dtos.Where(brand => brand.IsDelete).Select(dto => dto.ToEntity()).ToList();
        HashSet<Guid> deletedUid = brandsToDelete.Select(brand => brand.Id).ToHashSet();

        if (brandsToDelete.Count == 0) return;

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkDelete(brandsToDelete);
            transaction.Commit();
            dbContext.SaveChanges();
            OutputDto.AddSuccess(brandsToDelete.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            OutputDto.AddError(brandsToDelete.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
            transaction.Rollback();
        }
        finally
        {
            dtos.RemoveAll(brandDto => deletedUid.Contains(brandDto.Uid));
        }
    }
}
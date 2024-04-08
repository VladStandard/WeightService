using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands.Services;

internal partial class BrandService
{
    #region Resolve uniques db

    private void ResolveUniqueNameDb(List<BrandDto> dtos)
    {
        HashSet<string> namesList = dtos.Select(dto => dto.Name).ToHashSet();

        HashSet<string> existingNames = DbContext.Brands
            .Where(brand => namesList.Any(name => name.Equals(brand.Name)))
            .Select(brand => brand.Name)
            .ToHashSet();

        List<BrandDto> notUniqueBrandsDto = dtos.Where(dto => existingNames.Contains(dto.Name)).ToList();
        OutputDto.AddError(notUniqueBrandsDto.Select(i => i.Uid), "Наименование не уникально (бд)");

        dtos.RemoveAll(dto => existingNames.Contains(dto.Name));
    }

    #endregion

    private void SaveBrands(IEnumerable<BrandDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<BrandEntity> brands = validDtos.Select(dto => dto.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(brands, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
            });
            transaction.Commit();
            OutputDto.AddSuccess(brands.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(brands.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}
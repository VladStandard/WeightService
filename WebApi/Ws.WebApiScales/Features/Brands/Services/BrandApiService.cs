using FluentValidation.Results;
using Ws.Database.Core.Utils;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Brand;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brands.Dto;
using Ws.WebApiScales.Features.Brands.Validators;

namespace Ws.WebApiScales.Features.Brands.Services;

internal sealed class BrandApiService(ResponseDto responseDto, IBrandService brandService) : IBrandApiService
{
    #region Private

    private void UpdateOrCreate(BrandDto brandDto)
    {
        BrandEntity brandDb = brandService.GetItemByUid1С(brandDto.Uid);

        brandDb = brandDto.AdaptTo(brandDb);
        
        SqlCoreHelper.SaveOrUpdate(brandDb);
        responseDto.AddSuccess(brandDto.Uid, brandDb.Name);
    }

    private void DeleteBrand(Guid uid)
    {
        BrandEntity brandDb = brandService.GetItemByUid1С(uid);

        if (brandDb.IsNew)
        {
            responseDto.AddSuccess(uid, "Бренд не найден для удаления");
            return;
        }
        brandService.Delete(brandDb);
        responseDto.AddSuccess(uid, $"{brandDb.Name} - удален");
    }

    #endregion

    public void Load(BrandsWrapper brandsWrapper)
    {
        foreach (BrandDto brandDto in brandsWrapper.Brands)
        {
            if (brandDto.IsDelete)
            {
                DeleteBrand(brandDto.Uid);
                continue;
            }

            ValidationResult validationResult = new ValidatorBrandDto().Validate(brandDto);

            if (validationResult.IsValid)
            {
                UpdateOrCreate(brandDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            responseDto.AddError(brandDto.Uid, string.Join(" | ", errors));
        }
    }
}
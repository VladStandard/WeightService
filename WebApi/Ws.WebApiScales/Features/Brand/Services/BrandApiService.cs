using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Brand;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand.Dto;
using Ws.WebApiScales.Features.Brand.Validators;

namespace Ws.WebApiScales.Features.Brand.Services;

internal sealed class BrandApiService(ResponseDto responseDto, IBrandService brandService) : IBrandApiService
{
    #region Private

    private void UpdateOrCreate(BrandDto brandDto)
    {
        BrandEntity brandDb = brandService.GetByUid1С(brandDto.Guid);
        
        brandDb = brandDto.AdaptTo(brandDb);
        
        SqlCoreHelper.Instance.SaveOrUpdate(brandDb);
        responseDto.AddSuccess(brandDto.Guid, brandDb.Name);
    }
    
    private void IsMarkedBrand(Guid uid)
    {
        BrandEntity brandDb = brandService.GetByUid1С(uid);
        
        if (brandDb.IsNew)
        {
            responseDto.AddSuccess(uid, "Бренд не найден для удаления");
            return;
        }
        SqlCoreHelper.Instance.Delete(brandDb);
        responseDto.AddSuccess(uid, $"{brandDb.Name} - удален");
    }

    #endregion
    
    public void Load(BrandsWrapper brandsWrapper)
    {
        foreach (BrandDto brandDto in brandsWrapper.Brands)
        {
            if (brandDto.IsMarked)
            {
                IsMarkedBrand(brandDto.Guid);
                continue;
            }
            
            ValidationResult validationResult = new ValidatorBrandDto().Validate(brandDto);
    
            if (validationResult.IsValid)
            {
                UpdateOrCreate(brandDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            responseDto.AddError(brandDto.Guid, string.Join(" | ", errors));
        }
    }
}
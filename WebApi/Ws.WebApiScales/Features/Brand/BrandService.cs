using FluentValidation.Results;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Brand.Dto;
using Ws.WebApiScales.Features.Brand.Validators;

namespace Ws.WebApiScales.Features.Brand;

public class BrandService(ResponseDto responseDto) : IBrandService
{
    #region Private

    private void UpdateOrCreate(BrandDto brandDto)
    {
        SqlBrandEntity brandDb = new SqlBrandRepository().GetItemByUid1C(brandDto.Guid);
        
        brandDb = brandDto.AdaptTo(brandDb);
        
        SqlCoreHelper.Instance.SaveOrUpdate(brandDb);
        responseDto.AddSuccess(brandDto.Guid, brandDb.Name);
    }
    
    private void IsMarkedBrand(Guid uid)
    {
        SqlBrandEntity brandDb = new SqlBrandRepository().GetItemByUid1C(uid);
        
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
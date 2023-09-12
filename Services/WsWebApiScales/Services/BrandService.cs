using FluentValidation.Results;
using WsStorageCore.Tables.TableScaleModels.Brands;
using WsWebApiScales.Dto.Brand;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Validators;

namespace WsWebApiScales.Services;

public class BrandService
{
    
    private readonly ResponseDto _responseDto;

    public BrandService(ResponseDto responseDto)
    {
        _responseDto = responseDto;
    }
    
    private void UpdateOrCreate(BrandDto brandDto)
    {
        WsSqlBrandModel brandDb = new WsSqlBrandRepository().GetItemByUid1C(brandDto.Guid);

        brandDb.Name = brandDto.Name;
        brandDb.Code = brandDto.Code;
        brandDb.IsMarked = brandDto.IsMarked;
        
        if (brandDb.IsNotExists)
        {
            brandDb.Uid1C = brandDto.Guid;
            WsServiceUtils.SqlCore.Save(brandDb);
        }
        else
        {
            WsServiceUtils.SqlCore.Update(brandDb);
        }
        _responseDto.AddSuccess(brandDto.Guid, $"Бренд - {brandDb.Name} - изменен");
    }
    
    private void IsMarkedBrand(BrandDto brandDto)
    {
        WsSqlBrandModel brandDb = new WsSqlBrandRepository().GetItemByUid1C(brandDto.Guid);
        
        if (brandDb.IsNotExists)
        {
            _responseDto.AddSuccess(brandDto.Guid, "Бренд не найден для удаления");
        }
        else
        {
            brandDb.IsMarked = brandDto.IsMarked;
            WsServiceUtils.SqlCore.Update(brandDb);
            _responseDto.AddSuccess(brandDto.Guid, $"Бренд - {brandDb.Name} - удален");
        }
    }
    
    public ResponseDto LoadBrands(BrandsDto brandsDto)
    {
        foreach (BrandDto brandDto in brandsDto.Brands)
        {
            if (brandDto.IsMarked)
            {
                IsMarkedBrand(brandDto);
                continue;
            }
            ValidationResult validationResult = new BrandDtoValidator().Validate(brandDto);
    
            if (validationResult.IsValid)
            {
                UpdateOrCreate(brandDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            _responseDto.AddError(brandDto.Guid, string.Join(" | ", errors));
        }

        return _responseDto;
    }
    
}
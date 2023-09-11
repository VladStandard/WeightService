using FluentValidation.Results;
using WsStorageCore.Tables.TableScaleModels.Brands;
using WsStorageCore.Tables.TableScaleModels.Plus;
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
        WsSqlBrandRepository brandRepository = new();
        
        WsSqlBrandModel brandDb = brandRepository.GetItemByUid1C(brandDto.Guid);

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
        
        _responseDto.AddSuccess(brandDto.Guid);
    }
    
    public ResponseDto LoadBrands(BrandsDto brandsDto)
    {
        foreach (BrandDto brandDto in brandsDto.Brands)
        {
            BrandDtoValidator validator = new();
            ValidationResult validationResult = validator.Validate(brandDto);

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
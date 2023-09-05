using FluentValidation.Results;
using WsStorageCore.Tables.TableScaleModels.Brands;
using WsWebApiScales.Dto.Brand;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Validators;

namespace WsWebApiScales.Services;

public class BrandService
{
    private static void SaveBrand(ResponseDto response, BrandDto brandDto)
    {
        WsSqlBrandModel brand = new()
        {
            Uid1C = brandDto.Guid,
            Name = brandDto.Name,
            Code = brandDto.Code,
            IsMarked = brandDto.IsMarkedAsBool,
        };
        response.AddSuccess(brandDto.Guid);
        WsServiceUtils.SqlCore.Save(brand);
    }
    
    private static bool UpdateBrandModel(ResponseDto response, WsSqlBrandModel? brandDb, BrandDto brandDto)
    {
        if (brandDb is null)
            return false;
        brandDb.Uid1C = brandDto.Guid;
        brandDb.Name = brandDto.Name;
        brandDb.Code = brandDto.Code;
        brandDb.IsMarked = brandDto.IsMarkedAsBool;
        response.AddSuccess(brandDto.Guid);
        WsServiceUtils.SqlCore.Update(brandDb);
        return true;
    }
    
    private static bool UpdateBrandIfExists(ResponseDto response, BrandDto brandDto)
    {
        WsSqlBrandModel? brandDb = WsServiceUtils.ContextCache.Brands.Find(item => Equals(item.Uid1C, brandDto.Guid));
        if (UpdateBrandModel(response, brandDb, brandDto)) return true;
        
        brandDb = WsServiceUtils.ContextCache.Brands.Find(item => Equals(item.Code, brandDto.Code));
        return UpdateBrandModel(response, brandDb, brandDto);
    }
    
    public ResponseDto LoadBrands(BrandsDto brandsDto)
    {

        ResponseDto response = new();
        
        WsServiceUtils.ContextCache.Load();
        
        foreach (BrandDto brandDto in brandsDto.Brands)
        {
            BrandDtoValidator validator = new();
            ValidationResult validationResult = validator.Validate(brandDto);

            if (validationResult.IsValid)
            {
                if (UpdateBrandIfExists(response, brandDto)) continue;
                SaveBrand(response, brandDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            response.AddError(brandDto.Guid, string.Join(" | ", errors));
        }
        WsServiceUtils.ContextCache.Load(WsSqlEnumTableName.Brands);

        return response;
    }
    
}
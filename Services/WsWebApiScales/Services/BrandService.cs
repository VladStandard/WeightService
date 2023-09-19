using FluentValidation.Results;
using WsStorageCore.Tables.TableDiagModels.LogsWebs;
using WsStorageCore.Tables.TableRef1cModels.Brands;
using WsWebApiScales.Dto.Brand;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Utils;
using WsWebApiScales.Validators;

namespace WsWebApiScales.Services;

public class BrandService
{
    
    private readonly ResponseDto _responseDto;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BrandService(ResponseDto responseDto, IHttpContextAccessor httpContextAccessor)
    {
        _responseDto = responseDto;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private void UpdateOrCreate(BrandDto brandDto)
    {
        WsSqlBrandModel brandDb = new WsSqlBrandRepository().GetItemByUid1C(brandDto.Guid);

        brandDb.Name = brandDto.Name;
        brandDb.Code = brandDto.Code;
        brandDb.IsMarked = brandDto.IsMarked;
        
        if (brandDb.IsNew)
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
        
        if (brandDb.IsNew)
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
        DateTime requestTime = DateTime.Now;
        string currentUrl = _httpContextAccessor?.HttpContext?.Request.Path ?? string.Empty; 
        
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

      
        new WsSqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(brandsDto),   
        XmlUtil.SerializeToXml(_responseDto), currentUrl, _responseDto.SuccessesCount, _responseDto.ErrorsCount);

        return _responseDto;
    }
    
}
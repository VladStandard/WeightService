using FluentValidation.Results;
using Ws.WebApiScales.Dto.Brand;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Validators;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.WebApiCore.Utils;

namespace Ws.WebApiScales.Services;

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
        SqlBrandEntity brandDb = new SqlBrandRepository().GetItemByUid1C(brandDto.Guid);

        brandDb.Name = brandDto.Name;
        brandDb.Code = brandDto.Code;
        brandDb.IsMarked = brandDto.IsMarked;
        
        if (brandDb.IsNew)
        {
            brandDb.Uid1C = brandDto.Guid;
            SqlCoreHelper.Instance.Save(brandDb);
        }
        else
        {
            SqlCoreHelper.Instance.Update(brandDb);
        }
        _responseDto.AddSuccess(brandDto.Guid, $"Бренд - {brandDb.Name} - изменен");
    }
    
    private void IsMarkedBrand(Guid uid)
    {
        SqlBrandEntity brandDb = new SqlBrandRepository().GetItemByUid1C(uid);
        
        if (brandDb.IsNew)
        {
            _responseDto.AddSuccess(uid, "Бренд не найден для удаления");
        }
        else
        {
            brandDb.IsMarked = true;
            SqlCoreHelper.Instance.Update(brandDb);
            _responseDto.AddSuccess(uid, $"Бренд - {brandDb.Name} - удален");
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
                IsMarkedBrand(brandDto.Guid);
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

      
        new SqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(brandsDto),   
        XmlUtil.SerializeToXml(_responseDto), currentUrl, _responseDto.SuccessesCount, _responseDto.ErrorsCount);

        return _responseDto;
    }
    
}
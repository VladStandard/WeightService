using FluentValidation.Results;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.WebApiScales.Dto.Brand;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Utils;

namespace Ws.WebApiScales.Services;

public class BrandService(ResponseDto responseDto, IHttpContextAccessor httpContextAccessor)
{
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
        brandDb.IsMarked = true;
        SqlCoreHelper.Instance.Update(brandDb);
        responseDto.AddSuccess(uid, $"Бренд - {brandDb.Name} - удален");
    }
    
    public ResponseDto LoadBrands(BrandsDto brandsDto)
    {
        DateTime requestTime = DateTime.Now;
        string currentUrl = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty; 
        
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
            responseDto.AddError(brandDto.Guid, string.Join(" | ", errors));
        }

      
        new SqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(brandsDto),   
        XmlUtil.SerializeToXml(responseDto), currentUrl, responseDto.SuccessesCount, responseDto.ErrorsCount);

        return responseDto;
    }
}
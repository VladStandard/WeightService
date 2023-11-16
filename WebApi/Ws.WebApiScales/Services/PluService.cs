using FluentValidation.Results;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;
using Ws.StorageCore.Entities.SchemaRef1c.Bundles;
using Ws.StorageCore.Entities.SchemaRef1c.Clips;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusFks;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.WebApiCore.Utils;
using Ws.WebApiScales.Dto.Plu;
using Ws.WebApiScales.Dto.Response;
using Ws.WebApiScales.Validators;

namespace Ws.WebApiScales.Services;


public class PluService
{ 
    private readonly ResponseDto _responseDto;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public PluService(ResponseDto responseDto, IHttpContextAccessor httpContextAccessor)
    {
        _responseDto = responseDto;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public ActionResult<ResponseDto> LoadPlu(PlusDto plusDto)
    {
        DateTime requestTime = DateTime.Now;
        string currentUrl = _httpContextAccessor?.HttpContext?.Request.Path ?? string.Empty; 

        
        foreach (PluDto pluDto in plusDto.plus.OrderBy(item=>item.PluNumber))
        {
            SqlPluEntity pluDb = new SqlPluRepository().GetItemByUid1C(pluDto.Uid);
            if (pluDto.IsMarked) SetPluIsMarked(pluDb);
            
            ValidationResult validationResult = new PluDtoValidator().Validate(pluDto);
           
            //SetPluIsMarked(pluDto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                _responseDto.AddError(pluDto.Uid, string.Join(" | ", errors));
                continue;
            }
            
            SaveOrUpdateClip(pluDto.ClipTypeGuid, pluDto.ClipTypeName, pluDto.ClipTypeWeight);
            SaveOrUpdateBox(pluDto.BoxTypeGuid, pluDto.BoxTypeName, pluDto.BoxTypeWeight);
            SaveOrUpdateBundle(pluDto.PackageTypeGuid, pluDto.PackageTypeName, pluDto.PackageTypeWeight);
            SaveOrUpdatePlu(pluDb, pluDto);
            SaveOrUpdatePluFk(pluDb, pluDto);
            SaveOrUpdatePluNesting(pluDb, pluDto);
            _responseDto.AddSuccess(pluDto.Uid, $"Номенклатура {pluDb.Number} обновлена");
        }
        
        new SqlLogWebRepository().Save(requestTime,   
        XmlUtil.SerializeToXml(plusDto),   
        XmlUtil.SerializeToXml(_responseDto), currentUrl, _responseDto.SuccessesCount, _responseDto.ErrorsCount);
        
        return _responseDto;
    }

    //TODO: REFACTOR
    #region REFACTORED

     private static void SaveOrUpdateClip(Guid clip1CUid, string clipName, decimal clipWeight)
    {
        SqlClipEntity clipDb = new SqlClipRepository().GetItemByUid1C(clip1CUid);

        if (clip1CUid == Guid.Empty)
        {
            clipName = "Без клипсы";
            clipWeight = 0;
        }
        
        clipDb.Name = clipName;
        clipDb.Weight = clipWeight;
        
        if (clipDb.IsNew)
        {
            clipDb.Uid1C = clip1CUid;
            SqlCoreHelper.Instance.Save(clipDb);
            return;
        }
        SqlCoreHelper.Instance.Update(clipDb);
    }
    private static void SaveOrUpdateBox(Guid box1CUid, string boxName, decimal boxWeight)
    {
        SqlBoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(box1CUid);

        if (box1CUid == Guid.Empty)
        {
            boxName = "Без коробки";
            boxWeight = 0;
        }
        
        boxDb.Name = boxName;
        boxDb.Weight = boxWeight;
        
        if (boxDb.IsNew)
        {
            boxDb.Uid1C = box1CUid;
            SqlCoreHelper.Instance.Save(boxDb);
            return;
        }
        SqlCoreHelper.Instance.Update(boxDb);
    }
    private static void SaveOrUpdateBundle(Guid bundle1CGuid, string bundleName, decimal bundleWeight)
    {
        SqlBundleEntity bundleDb = new SqlBundleRepository().GetItemByUid1C(bundle1CGuid);

        if (bundle1CGuid == Guid.Empty)
        {
            bundleName = "Без пакета";
            bundleWeight = 0;
        }
        
        bundleDb.Name = bundleName;
        bundleDb.Weight = bundleWeight;
        
        if (bundleDb.IsNew)
        {
            bundleDb.Uid1C = bundle1CGuid;
            SqlCoreHelper.Instance.Save(bundleDb);
            return;
        }
        SqlCoreHelper.Instance.Update(bundleDb);
    }
    private static void SaveOrUpdatePlu(SqlPluEntity plu, PluDto pluDto)
    {
        plu.Name = pluDto.Name;
        plu.FullName = pluDto.FullName;
        plu.Description = pluDto.Description;
        plu.IsMarked = pluDto.IsMarked;
        plu.IsGroup = pluDto.IsGroup;
        plu.Number = (short)pluDto.PluNumber;
        plu.ShelfLifeDays = (byte)pluDto.ShelfLife;
        plu.IsCheckWeight = pluDto.IsCheckWeight;
        plu.Bundle = new SqlBundleRepository().GetItemByUid1C(pluDto.PackageTypeGuid);
        plu.Brand = new SqlBrandRepository().GetItemByUid1C(pluDto.BrandGuid);
        plu.Code = pluDto.Code;
        plu.Ean13 = pluDto.Ean13;
        plu.Itf14 = pluDto.IsCheckWeight == false ? pluDto.Itf14 : "";
        plu.Gtin = pluDto.IsCheckWeight == false ? pluDto.Itf14 : "0" + pluDto.Ean13;
        
        if (plu.IsNew)
        {
            plu.Uid1C = pluDto.Uid;
            SqlCoreHelper.Instance.Save(plu);
            return;
        }
        SqlCoreHelper.Instance.Update(plu);
    }
    private static void SaveOrUpdatePluFk(SqlPluEntity plu, PluDto pluDto)
    {
        if (Equals(pluDto.ParentGroupGuid, Guid.Empty)) return;
        if (plu.IsNew) return;
        
        SqlPluEntity parentPluDb = new SqlPluRepository().GetItemByUid1C(pluDto.ParentGroupGuid);
        if (parentPluDb.IsNew) return;
        
        SqlPluEntity categoryDb = new SqlPluRepository().GetItemByUid1C(pluDto.CategoryGuid);

        SqlPluFkEntity pluFkDb = new SqlPluFkRepository().GetByPlu(plu);

        pluFkDb.Parent = parentPluDb;
        pluFkDb.Category = categoryDb.IsExists ? categoryDb : null;
        
        if (pluFkDb.IsNew)
        {
            pluFkDb.Plu = plu;
            SqlCoreHelper.Instance.Save(pluFkDb);
            return;
        }
        SqlCoreHelper.Instance.Update(pluFkDb);
    }
    private static void SetPluIsMarked(SqlPluEntity plu)
    {
        if (plu.IsNew) return;
        plu.IsMarked = true;
        SqlCoreHelper.Instance.Update(plu);
    }
    private static void SaveOrUpdatePluNesting(SqlPluEntity plu, PluDto pluDto)
    {
        SqlBoxEntity boxDb = new SqlBoxRepository().GetItemByUid1C(pluDto.BoxTypeGuid);
        if (boxDb.IsNew) return;

        SqlPluNestingFkEntity pluNestingDb = new SqlPluNestingFkRepository().GetDefaultByPlu(plu);
        
        pluNestingDb.IsDefault = true;
        pluNestingDb.BundleCount = pluDto.AttachmentsCount;
        pluNestingDb.Box = boxDb;
        
        if (pluNestingDb.IsNew)
        {
            pluNestingDb.Plu = plu;
            SqlCoreHelper.Instance.Save(pluNestingDb);
            return;
        }
        SqlCoreHelper.Instance.Update(pluNestingDb);
    }

     #endregion
   
}
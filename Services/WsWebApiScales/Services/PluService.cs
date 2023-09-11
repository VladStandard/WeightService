using FluentValidation.Results;
using WsStorageCore.Tables.TableScaleModels.Boxes;
using WsStorageCore.Tables.TableScaleModels.Bundles;
using WsStorageCore.Tables.TableScaleModels.Clips;
using WsStorageCore.Tables.TableScaleModels.Plus;
using WsWebApiScales.Dto.Plu;
using WsWebApiScales.Dto.Response;
using WsWebApiScales.Validators;


namespace WsWebApiScales.Services;


public class PluService
{ 
    private readonly ResponseDto _responseDto;
    
    public PluService(ResponseDto responseDto)
    {
        _responseDto = responseDto;
    }
    
    public ActionResult<ResponseDto> LoadPlu(PlusDto plusDto)
    {
        
        foreach (PluDto pluDto in plusDto.plus)
        {
            if (pluDto.IsMarked) SetPluIsMarked(pluDto);
            
            ValidationResult validationResult = new PluDtoValidator().Validate(pluDto);
           
            if (validationResult.IsValid)
            {
                SaveOrUpdateClip(pluDto.ClipTypeGuid, pluDto.ClipTypeName, pluDto.ClipTypeWeight);
                SaveOrUpdateBox(pluDto.BoxTypeGuid, pluDto.BoxTypeName, pluDto.BoxTypeWeight);
                SaveOrUpdateBundle(pluDto.PackageTypeGuid, pluDto.PackageTypeName, pluDto.PackageTypeWeight);
                SaveOrUpdatePlu(pluDto);
                // TODO: PluClipsSave
                continue;
            }
            SetPluIsMarked(pluDto);

            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            _responseDto.AddError(pluDto.Uid, string.Join(" | ", errors));

        }
        return _responseDto;
    }
    
    private static void SaveOrUpdateClip(Guid clip1CUid, string clipName, decimal clipWeight)
    {
        WsSqlClipModel clipDb = new WsSqlClipRepository().GetItemByUid1C(clip1CUid);

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
            WsServiceUtils.SqlCore.Save(clipDb);
            return;
        }
        WsServiceUtils.SqlCore.Update(clipDb);
    }
    private static void SaveOrUpdateBox(Guid box1CUid, string boxName, decimal boxWeight)
    {
        WsSqlBoxModel boxDb = new WsSqlBoxRepository().GetItemByUid1C(box1CUid);

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
            WsServiceUtils.SqlCore.Save(boxDb);
            return;
        }
        WsServiceUtils.SqlCore.Update(boxDb);
    }
    private static void SaveOrUpdateBundle(Guid bundle1CGuid, string bundleName, decimal bundleWeight)
    {
        WsSqlBundleModel bundleDb = new WsSqlBundleRepository().GetItemByUid1C(bundle1CGuid);

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
            WsServiceUtils.SqlCore.Save(bundleDb);
            return;
        }
        WsServiceUtils.SqlCore.Update(bundleDb);
    }
    private static void SaveOrUpdatePlu(PluDto pluDto)
    {
        WsSqlPluModel pluDb = new WsSqlPluRepository().GetItemByUid1C(pluDto.Uid);
        
        
        pluDb.Name = pluDto.Name;
        pluDb.FullName = pluDto.FullName;
        pluDb.Description = pluDto.Description;
        pluDb.IsMarked = pluDto.IsMarked;
        pluDb.IsGroup = pluDto.IsGroup;
        pluDb.Number = (short)pluDto.PluNumber;
        pluDb.ShelfLifeDays = (byte)pluDto.ShelfLife;
        pluDb.IsCheckWeight = pluDto.IsCheckWeight;
        pluDb.Bundle = new WsSqlBundleRepository().GetItemByUid1C(pluDto.PackageTypeGuid);
        pluDb.Code = pluDto.Code;
        pluDb.Ean13 = pluDto.Ean13;
        pluDb.Itf14 = pluDto.IsCheckWeight == false ? pluDto.Itf14 : "";
        pluDb.Gtin = pluDto.IsCheckWeight == false ? pluDto.Ean13 : "0" + pluDto.Ean13;
        
        if (pluDb.IsNew)
        {
            pluDb.Uid1C = pluDto.Uid;
            WsServiceUtils.SqlCore.Save(pluDb);
            return;
        }
        WsServiceUtils.SqlCore.Update(pluDb);
    }
    private static void SetPluIsMarked(PluDto pluDto)
    {
        WsSqlPluModel pluDb = new WsSqlPluRepository().GetItemByUid1C(pluDto.Uid);
        if (!pluDb.IsExists) return;
        pluDb.IsMarked = true;
        WsServiceUtils.SqlCore.Update(pluDb);

    }
}
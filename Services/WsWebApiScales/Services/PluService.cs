using FluentValidation.Results;
using WsStorageCore.Tables.TableScaleFkModels.PlusFks;
using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.Tables.TableScaleModels.Boxes;
using WsStorageCore.Tables.TableScaleModels.Brands;
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
        foreach (PluDto pluDto in plusDto.plus.OrderBy(item=>item.PluNumber))
        {
            WsSqlPluModel pluDb = new WsSqlPluRepository().GetItemByUid1C(pluDto.Uid);
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
        return _responseDto;
    }

    //TODO: REFACTOR
    #region REFACTORED

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
    private static void SaveOrUpdatePlu(WsSqlPluModel plu, PluDto pluDto)
    {
        plu.Name = pluDto.Name;
        plu.FullName = pluDto.FullName;
        plu.Description = pluDto.Description;
        plu.IsMarked = pluDto.IsMarked;
        plu.IsGroup = pluDto.IsGroup;
        plu.Number = (short)pluDto.PluNumber;
        plu.ShelfLifeDays = (byte)pluDto.ShelfLife;
        plu.IsCheckWeight = pluDto.IsCheckWeight;
        plu.Bundle = new WsSqlBundleRepository().GetItemByUid1C(pluDto.PackageTypeGuid);
        plu.Brand = new WsSqlBrandRepository().GetItemByUid1C(pluDto.BrandGuid);
        plu.Code = pluDto.Code;
        plu.Ean13 = pluDto.Ean13;
        plu.Itf14 = pluDto.IsCheckWeight == false ? pluDto.Itf14 : "";
        plu.Gtin = pluDto.IsCheckWeight == false ? pluDto.Ean13 : "0" + pluDto.Ean13;
        
        if (plu.IsNew)
        {
            plu.Uid1C = pluDto.Uid;
            WsServiceUtils.SqlCore.Save(plu);
            return;
        }
        WsServiceUtils.SqlCore.Update(plu);
    }
    private static void SaveOrUpdatePluFk(WsSqlPluModel plu, PluDto pluDto)
    {
        if (Equals(pluDto.ParentGroupGuid, Guid.Empty)) return;
        if (plu.IsNotExists) return;
        
        WsSqlPluModel parentPluDb = new WsSqlPluRepository().GetItemByUid1C(pluDto.ParentGroupGuid);
        if (parentPluDb.IsNotExists) return;
        
        WsSqlPluModel categoryDb = new WsSqlPluRepository().GetItemByUid1C(pluDto.CategoryGuid);

        WsSqlPluFkModel pluFkDb = new WsSqlPluFkRepository().GetByPlu(plu);

        pluFkDb.Parent = parentPluDb;
        pluFkDb.Category = categoryDb.IsExists ? categoryDb : null;
        
        if (pluFkDb.IsNew)
        {
            pluFkDb.Plu = plu;
            WsServiceUtils.SqlCore.Save(pluFkDb);
            return;
        }
        WsServiceUtils.SqlCore.Update(pluFkDb);
    }
    private static void SetPluIsMarked(WsSqlPluModel plu)
    {
        if (plu.IsNotExists) return;
        plu.IsMarked = true;
        WsServiceUtils.SqlCore.Update(plu);
    }
    private static void SaveOrUpdatePluNesting(WsSqlPluModel plu, PluDto pluDto)
    {
        WsSqlBoxModel boxDb = new WsSqlBoxRepository().GetItemByUid1C(pluDto.BoxTypeGuid);
        if (boxDb.IsNotExists) return;

        WsSqlPluNestingFkModel pluNestingDb = new WsSqlPluNestingFkRepository().GetDefaultByPlu(plu);
        
        pluNestingDb.IsDefault = true;
        pluNestingDb.BundleCount = pluDto.AttachmentsCount;
        pluNestingDb.Box = boxDb;
        
        if (pluNestingDb.IsNew)
        {
            pluNestingDb.Plu = plu;
            WsServiceUtils.SqlCore.Save(pluNestingDb);
            return;
        }
        WsServiceUtils.SqlCore.Update(pluNestingDb);
    }

     #endregion
   
}
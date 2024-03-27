// using FluentValidation.Results;
// using Ws.PalychExchangeApi.Features.Nestings.Dto;
// using Ws.PalychExchangeApi.Features.Nestings.Validators;
//
// namespace Ws.PalychExchangeApi.Features.Nestings.Services;
//
// internal sealed class PluCharacteristicApiService : IPluCharacteristicApiService
// {
//     public void Load(CharacteristicsWrapper characteristics)
//     {
//         foreach (PluCharacteristicDto pluCharacteristicDto in characteristics.PluCharacteristics)
//         {
//             PluEntity pluDb = pluService.GetItemByUid1С(pluCharacteristicDto.Uid);
//
//             if (pluDb.IsNew)
//             {
//                 responseDto.AddError(pluCharacteristicDto.Uid, "Номенклатура не найдена!");
//                 continue;
//             }
//
//             if (pluDb.IsCheckWeight)
//             {
//                 responseDto.AddError(pluCharacteristicDto.Uid, $"{pluDb.DisplayName} - весовая!");
//                 continue;
//             }
//
//             foreach (CharacteristicDto characteristic in pluCharacteristicDto.Characteristics)
//             {
//                 if (characteristic.IsDelete)
//                 {
//                     pluService.DeleteNestingByUid1C(pluDb, characteristic.Uid);
//                     responseDto.AddSuccess(characteristic.Uid,
//                     $"{pluDb.DisplayName} - вложенность {characteristic.BundleCount} удалена!");
//                     continue;
//                 }
//
//                 ValidationResult validationResult = new ValidatorCharacteristicDto().Validate(characteristic);
//
//                 if (!validationResult.IsValid)
//                 {
//                     List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
//                     responseDto.AddError(pluCharacteristicDto.Uid, string.Join(" | ", errors));
//                     continue;
//                 }
//
//                 PluNestingEntity nesting = pluService.GetNestingByUid1C(pluDb, characteristic.Uid);
//
//                 if (nesting.IsNew)
//                 {
//                     BoxEntity box = boxService.GetDefaultForCharacteristic();
//                     nesting.Box = box;
//                 }
//
//                 nesting.Plu = pluDb;
//                 nesting = characteristic.AdaptTo(nesting);
//                 SqlCoreHelper.SaveOrUpdate(nesting);
//
//                 responseDto.AddSuccess(pluCharacteristicDto.Uid,
//                 $"{pluDb.DisplayName} | Кол-во вложений: {nesting.BundleCount}");
//             }
//         }
//     }
// }
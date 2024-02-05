using FluentValidation.Results;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Bundle;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Bundles.Dto;
using Ws.WebApiScales.Features.Bundles.Validators;

namespace Ws.WebApiScales.Features.Bundles.Services;

internal sealed class BundleApiService(ResponseDto responseDto, IBundleService bundleService) : IBundleApiService
{
    private void UpdateOrCreate(BundleDto bundleDto)
    {
        BundleEntity bundleDb = bundleService.GetItemByUid1С(bundleDto.Uid);
        bundleDb = bundleDto.AdaptTo(bundleDb);
        SqlCoreHelper.Instance.SaveOrUpdate(bundleDb);
        responseDto.AddSuccess(bundleDb.Uid1C, bundleDb.Name);
    }
    
    public void Load(BundlesWrapper bundleWrapper)
    {
        foreach (BundleDto bundleDto in bundleWrapper.Bundles)
        {
            ValidationResult validationResult = new ValidatorBundleDto().Validate(bundleDto);
    
            if (validationResult.IsValid)
            {
                UpdateOrCreate(bundleDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            responseDto.AddError(bundleDto.Uid, string.Join(" | ", errors));
        }
    }
}
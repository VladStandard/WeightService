using FluentValidation.Results;
using Ws.Database.Core.Utils;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Clip;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Clips.Dto;
using Ws.WebApiScales.Features.Clips.Validators;

namespace Ws.WebApiScales.Features.Clips.Services;

internal sealed class ClipApiService(ResponseDto responseDto, IClipService clipService) : IClipApiService
{
    private void UpdateOrCreate(ClipDto clipDto)
    {
        ClipEntity clipDb = clipService.GetItemByUid1С(clipDto.Uid);
        clipDb = clipDto.AdaptTo(clipDb);
        SqlCoreHelper.SaveOrUpdate(clipDb);
        responseDto.AddSuccess(clipDb.Uid1C, clipDb.Name);
    }

    public void Load(ClipsWrapper clipsWrapper)
    {
        foreach (ClipDto clipDto in clipsWrapper.Clips)
        {
            ValidationResult validationResult = new ValidatorClipDto().Validate(clipDto);

            if (validationResult.IsValid)
            {
                UpdateOrCreate(clipDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            responseDto.AddError(clipDto.Uid, string.Join(" | ", errors));
        }
    }
}
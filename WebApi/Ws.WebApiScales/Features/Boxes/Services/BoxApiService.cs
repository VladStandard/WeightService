using FluentValidation.Results;
using Ws.Database.Core.Utils;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Box;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Boxes.Dto;
using Ws.WebApiScales.Features.Boxes.Validators;

namespace Ws.WebApiScales.Features.Boxes.Services;

internal sealed class BoxApiService(ResponseDto responseDto, IBoxService boxService) : IBoxApiService
{
    private void UpdateOrCreate(BoxDto boxDto)
    {
        BoxEntity boxDb = boxService.GetItemByUid1С(boxDto.Uid);
        boxDb = boxDto.AdaptTo(boxDb);
        SqlCoreHelper.SaveOrUpdate(boxDb);
        responseDto.AddSuccess(boxDb.Uid1C, boxDb.Name);
    }

    public void Load(BoxWrapper boxWrapper)
    {
        foreach (BoxDto boxDto in boxWrapper.Boxes)
        {
            ValidationResult validationResult = new ValidatorBoxDto().Validate(boxDto);

            if (validationResult.IsValid)
            {
                UpdateOrCreate(boxDto);
                continue;
            }
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            responseDto.AddError(boxDto.Uid, string.Join(" | ", errors));
        }
    }
}
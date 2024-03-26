using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;
using Ws.PalychExchangeApi.Features.Clips.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

internal class ClipService : IClipService
{
    private void SaveClip(ClipDto clipDto)
    {
        using var dbContext = new WsDbContext();
        using var transaction = dbContext.Database.BeginTransaction();

        try
        {
            // ClipEntity existingClip = dbContext.Clips.FirstOrDefault(b => b.Uid1C == clipDto.Uid) ?? new();
            //
            // existingClip.Name = clipDto.Name;
            // existingClip.Weight = clipDto.Weight;
            //
            // if (existingClip.IsNew)
            // {
            //     existingClip.Uid1C = clipDto.Uid;
            //     dbContext.Add(existingClip);
            // }
            // dbContext.SaveChanges();
            // transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }


    public ClipWrapper Load(ClipWrapper dto)
    {
        foreach (ClipDto clipDto in dto.Clips)
        {
            ValidationResult validationResult = new ClipDtoValidator().Validate(clipDto);

            if (!validationResult.IsValid)
                continue;
            SaveClip(clipDto);
        }
        return new();
    }
}
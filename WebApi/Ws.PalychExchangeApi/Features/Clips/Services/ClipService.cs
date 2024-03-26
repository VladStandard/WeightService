using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;
using Ws.PalychExchangeApi.Features.Clips.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

internal class ClipService(DbContext dbContext) : IClipService
{
    private void SaveClips(IEnumerable<ClipDto> clipsDto)
    {
        List<ClipEntity> clips = clipsDto.Select(boxDto => new ClipEntity(boxDto.Uid, boxDto.Name, boxDto.Weight))
            .ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(clips);
            transaction.Commit();
            dbContext.SaveChanges();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public ClipWrapper Load(ClipWrapper dto)
    {
        ClipDtoValidator clipValidator = new();
        HashSet<ClipDto> validDtoClips = [];

        foreach (ClipDto clipDto in dto.Clips)
        {
            ValidationResult validationResult = clipValidator.Validate(clipDto);
            if (validDtoClips.Any(clip => clip.Uid == clipDto.Uid))
                continue;
            if (!validationResult.IsValid)
                continue;
            validDtoClips.Add(clipDto);
        }
        SaveClips(validDtoClips);
        return new();
    }
}
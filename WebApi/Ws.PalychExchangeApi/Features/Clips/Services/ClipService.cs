using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;
using Ws.PalychExchangeApi.Features.Clips.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

internal class ClipService(DbContext dbContext) : IClipService
{
    private void SaveClips(IEnumerable<ClipDto> validDtos)
    {
        List<ClipEntity> clips = validDtos.Select(dto => dto.ToEntity()).ToList();

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

    public ClipWrapper Load(ClipWrapper dtoWrapper)
    {
        ClipDtoValidator validator = new();
        HashSet<ClipDto> validDtos = [];

        foreach (ClipDto dto in dtoWrapper.Clips)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (validDtos.Any(clip => clip.Uid == dto.Uid))
                continue;
            if (!validationResult.IsValid)
                continue;
            validDtos.Add(dto);
        }
        SaveClips(validDtos);
        return new();
    }
}
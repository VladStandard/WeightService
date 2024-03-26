using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

internal class BoxService : IBoxService
{
    private static void SaveBoxes(IEnumerable<BoxDto> boxesDto)
    {
        using var dbContext = new WsDbContext();

        List<BoxEntity> boxes = boxesDto.Select(boxDto => new BoxEntity(boxDto.Uid, boxDto.Name, boxDto.Weight))
            .ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(boxes);
            transaction.Commit();
            dbContext.SaveChanges();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public BoxWrapper Load(BoxWrapper dto)
    {
        BoxDtoValidator boxValidator = new();
        HashSet<BoxDto> validDtoBoxes = [];

        foreach (BoxDto boxDto in dto.Boxes)
        {
            ValidationResult validationResult = boxValidator.Validate(boxDto);
            if (validDtoBoxes.Any(box => box.Uid == boxDto.Uid))
                continue;
            if (!validationResult.IsValid)
                continue;
            validDtoBoxes.Add(boxDto);
        }
        SaveBoxes(validDtoBoxes);
        return new();
    }
}
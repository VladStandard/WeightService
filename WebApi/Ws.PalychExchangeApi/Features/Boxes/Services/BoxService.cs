using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

internal class BoxService(DbContext dbContext) : IBoxService
{
    private void SaveBoxes(IEnumerable<BoxDto> validDtos)
    {
        List<BoxEntity> boxes = validDtos.Select(dto => dto.ToEntity()).ToList();

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

    public BoxWrapper Load(BoxWrapper dtoWrapper)
    {
        BoxDtoValidator validator = new();
        HashSet<BoxDto> validDtos = [];

        foreach (BoxDto dto in dtoWrapper.Boxes)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (validDtos.Any(box => box.Uid == dto.Uid))
                continue;
            if (!validationResult.IsValid)
                continue;
            validDtos.Add(dto);
        }
        SaveBoxes(validDtos);
        return new();
    }
}
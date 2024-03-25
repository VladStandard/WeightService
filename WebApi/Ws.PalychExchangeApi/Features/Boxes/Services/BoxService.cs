using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

internal class BoxService : IBoxService
{
    private void SaveBox(BoxDto boxDto)
    {
        using var dbContext = new WsDbContext();
        using var transaction = dbContext.Database.BeginTransaction();

        try
        {
            BoxEntity existingBox = dbContext.Boxes.FirstOrDefault(b => b.Uid1C == boxDto.Uid) ?? new();

            existingBox.Name = boxDto.Name;
            existingBox.Weight = boxDto.Weight;

            if (existingBox.IsNew)
            {
                existingBox.Uid1C = boxDto.Uid;
                dbContext.Add(existingBox);
            }
            dbContext.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }


    public BoxWrapper Load(BoxWrapper dto)
    {
        foreach (BoxDto boxDto in dto.Boxes)
        {
            ValidationResult validationResult = new BoxDtoValidator().Validate(boxDto);

            if (!validationResult.IsValid)
                continue;
            SaveBox(boxDto);
        }
        return new();
    }
}
using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.PalletMen;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Validators;

public class PalletManUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<PalletManEntity, PalletManUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<PalletManEntity> dbSet, PalletManUpdateDto dto, Guid id)
    {
        Fio fio = new(dto.Surname, dto.Name, dto.Patronymic);
        UqPalletManProperties uqProperties = new(dto.Id1C, fio, dto.Password);
        PredicateField<PalletManEntity> idPredicate = new(i => i.Id == id, string.Empty);

        await ValidateProperties(new PalletManUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, PalletManExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}
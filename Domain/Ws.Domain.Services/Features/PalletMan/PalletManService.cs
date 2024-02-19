﻿using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.PalletMan;

public class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    [Session] public PalletManEntity GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);
    [Session] public IEnumerable<PalletManEntity> GetAll() => palletManRepo.GetAll();
}
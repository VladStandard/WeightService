﻿using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Line;

public interface ILineService : IGetItemByUid<LineEntity>, IGetAll<LineEntity>
{
    public LineEntity GetCurrentLine();
    public IEnumerable<PluEntity> GetLinePlus(LineEntity line);
    public IEnumerable<PluEntity> GetLineWeightPlus(LineEntity line);
    public IEnumerable<PluEntity> GetLinePiecePlus(LineEntity line);
    
    // TODO: Delete
    public IEnumerable<PluLineEntity> GetLinePlusFk(LineEntity line);
}
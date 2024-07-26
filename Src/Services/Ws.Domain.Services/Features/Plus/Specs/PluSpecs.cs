using ProjectionTools.Specifications;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Domain.Services.Features.Plus.Specs;

internal static class PluSpecs
{
    public static Specification<Plu> GetPiece() => new(x => !x.IsCheckWeight);
}
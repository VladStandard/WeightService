using ProjectionTools.Specifications;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Templates.Specs;

public static class TemplateSpec
{
    public static Specification<Template> GetForWeight => new (x => x.IsWeight);
    public static Specification<Template> GetForPiece => new (x => !x.IsWeight);
}
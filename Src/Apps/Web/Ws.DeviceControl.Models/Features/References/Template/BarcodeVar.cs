using System.Linq.Expressions;

namespace Ws.DeviceControl.Models.Features.References.Template;

public sealed record BarcodeVar
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(TypeJsonConverter))]
    public Type Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("length")]
    public short Length { get; set; }

    [JsonPropertyName("isRepeatable")]
    public bool IsRepeatable { get; set; }

    public BarcodeVar(Expression<Func<object>> expr, short length, bool isRepeatable = false)
    {
        switch (expr.Body)
        {
            case MemberExpression memberExpression:
                Type = memberExpression.Type;
                Name = memberExpression.Member.Name;
                break;
            case UnaryExpression { Operand: MemberExpression nestedMemberExpr }:
                Type = nestedMemberExpr.Type;
                Name = nestedMemberExpr.Member.Name;
                break;
            default:
                throw new ArgumentException("Expression must be a member expression.");
        }
        Length = length;
        IsRepeatable = isRepeatable;
    }
}
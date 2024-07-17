using System.Linq.Expressions;

namespace Ws.Labels.Service.Generate.Models;

public record BarcodeVariable
{
    public Type Type { get; }
    public string Name { get; }
    public short Length { get; }
    public bool IsRepeatable { get; }

    public BarcodeVariable(Expression<Func<object>> expr, short length, bool isRepeatable = false)
    {
        switch (expr.Body)
        {
            case MemberExpression memberExpression:
                Type = memberExpression.Type;
                Name = memberExpression.Member.Name;
                Length = length;
                IsRepeatable = isRepeatable;
                break;
            case UnaryExpression { Operand: MemberExpression nestedMemberExpr }:
                Type = nestedMemberExpr.Type;
                Name = nestedMemberExpr.Member.Name;
                Length = length;
                IsRepeatable = isRepeatable;
                break;
            default:
                throw new ArgumentException("Expression must be a member expression.");
        }
    }
}
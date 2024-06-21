using System.Linq.Expressions;

namespace Ws.Labels.Service.Generate.Models;

public class BarcodeVariable(MemberExpression memberExpr, short length, bool isRepeatable) {
    public short Length { get; } = length;
    public bool IsRepeatable { get; } = isRepeatable;
    private MemberExpression MemberExpr { get; } = memberExpr;

    public Type Type => MemberExpr.Type;
    public string Name => MemberExpr.Member.Name;

    public override bool Equals(object? obj)
    {
        if (obj is BarcodeVariable other)
            return Name == other.Name;
        return false;
    }

    public override int GetHashCode() => Name.GetHashCode();

    internal static BarcodeVariable Build<T>(Expression<Func<T>> expr, short length, bool isRepeatable=false)
    {
        if (expr.Body is MemberExpression memberExpression)
            return new(memberExpression, length, isRepeatable);
        throw new ArgumentException("Expression must be a member expression.");
    }
}
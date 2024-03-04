using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;
using Ws.Domain.Models.Common;
using Ws.Domain.Services.Exceptions;

namespace Ws.Domain.Services.Aspects;

[PSerializable]
internal class ValidateAttribute<TValidator> : OnMethodBoundaryAspect where TValidator : IValidator, new()
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Type? objectType = args.Arguments.GetArgument(0)?.GetType();

        if (objectType == null || !typeof(EntityBase).IsAssignableFrom(objectType))
            throw new ArgumentException("Method must have at least one argument of type object.");

        TValidator validator = new();

        MethodInfo? validateMethod = typeof(TValidator).GetMethod("Validate", [objectType]);

        if (validateMethod?.Invoke(validator, [args.Arguments.GetArgument(0)]) is ValidationResult
            {
                IsValid: false
            } validationResult)
        {
            throw new ValidateException(validationResult);
        }
    }
}
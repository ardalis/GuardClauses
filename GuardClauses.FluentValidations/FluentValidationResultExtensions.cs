
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using GuardClauses.FluentValidations;
namespace Ardalis.GuardClauses.FluentValidations;
public static class FluentValidationResultExtensions
{
    public static T ValidateOrThrow<T>(
        this IGuardClause guard,
        T? input) 
    {
        Guard.Against.Null(input);

        Assembly assembly = Assembly.GetExecutingAssembly();

        Type? validatorType = AssemblyScanner.FindValidatorsInAssembly(assembly)
            .Select(o => o.ValidatorType)
            .Where(o => o.IsSubclassOf(typeof(AbstractValidator<T>)))
            .FirstOrDefault();

        Guard.Against.Null(validatorType);    

        IValidator? validator = (IValidator?)Activator.CreateInstance(validatorType);
        var context = new ValidationContext<T>(input);
        var response = validator!.Validate(context);

        if (!response.IsValid)
        {
            throw new ArgumentException($"Input '{input.GetType().Name}' is not validated");
        }
        return input;
    }

}

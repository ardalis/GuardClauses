
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using GuardClauses.FluentValidations;
namespace Ardalis.GuardClauses.FluentValidations;

/// <summary>
/// FluentValidationExtensions
/// </summary>
public static class FluentValidationResultExtensions
{
    /// <summary>
    /// Validates and throws
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="guard"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T ValidateOrThrow<T>(
        this IGuardClause guard,
        T? input) 
    {

        Guard.Against.Null(input);

        Assembly assembly = GuardFluentValidation.GetAssembly() ?? Assembly.GetExecutingAssembly();
        
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

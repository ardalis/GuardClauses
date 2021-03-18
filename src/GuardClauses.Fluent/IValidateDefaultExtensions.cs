using System;
using System.Collections.Generic;
using System.Text;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// A collection of common IValidate clauses, implented as extensions.
    /// </summary>
    public static class IValidateBaseExtensions
    {
        public static T Default<T>([JetBrainsNotNull] this IValidate<T> validateClause, string? parameterName = null)
        {
            Guard.Against.Default(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input;
        }
    }
}

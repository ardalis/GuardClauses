using System;
using System.Collections.Generic;
using System.Text;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// A collection of common IValidate clauses, implented as extensions.
    /// </summary>
    public static class IValidateNullExtensions
    {
        public static T Null<T>([JetBrainsNotNull] this IValidate<T?> validateClause, string? parameterName = null) where T : struct
        {
            Guard.Against.Null(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input.Value;
        }

        public static T Null<T>([JetBrainsNotNull] this IValidate<T?> validateClause, string? parameterName = null) where T : class
        {
            Guard.Against.Null(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input;
        }
    }
}

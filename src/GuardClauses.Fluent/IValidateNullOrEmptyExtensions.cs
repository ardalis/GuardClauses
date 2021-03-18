using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Ardalis.GuardClauses.Fluent
{
    public static class IValidateNullOrEmptyExtensions
    {
        public static string NullOrWhiteSpace([JetBrainsNotNull] this IValidate<string?> validateClause, string? parameterName = null)
        {
            Guard.Against.NullOrWhiteSpace(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input;
        }

        public static string NullOrEmpty([JetBrainsNotNull] this IValidate<string?> validateClause, string? parameterName = null)
        {
            Guard.Against.NullOrEmpty(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input;
        }

        public static Guid NullOrEmpty([JetBrainsNotNull] this IValidate<Guid?> validateClause, string? parameterName = null)
        {
            Guard.Against.NullOrEmpty(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input.Value;
        }

        public static IEnumerable<T> NullOrEmpty<T>([JetBrainsNotNull] this IValidate<IEnumerable<T>?> validateClause, string? parameterName = null)
        {
            Guard.Against.NullOrEmpty(validateClause.Input, parameterName ?? validateClause.InputTypeName);

            return validateClause.Input;
        }
    }
}

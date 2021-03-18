using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses.Fluent
{
    /// <summary>
    /// An entry point to a set of validation methods defined as extension methods on IValidate.
    /// </summary>
    public static class ValidateForExtension
    {
        /// <summary>
        /// An entry point to a set of validation rules defined as extension methods on IValidate of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IValidate<T> ValidateFor<T>(this T input)
        {
            return new Validate<T>(input);
        }
    }
}

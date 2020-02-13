using System;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// Add to methods that check input for null and throw if the input is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute { }
}

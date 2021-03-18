using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses.Fluent
{
    /// <summary>
    /// Simple interface to provide a generic mechanism to build extension methods from.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidate<out T>
    {
        /// <summary>
        /// The object being extended.
        /// </summary>
        T Input { get; }

        /// <summary>
        /// The type name of the object being extended.
        /// </summary>
        string InputTypeName { get; }
    }
}

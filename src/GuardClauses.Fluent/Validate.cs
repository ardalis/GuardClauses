using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses
{
    /// <summary>
    /// Default implementation of IValidate of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Validate<T> : IValidate<T>
    {
        /// <summary>
        /// The object being extended.
        /// </summary>
        public T Input { get; private set; }

        /// <summary>
        /// The type name of the object being extended.
        /// </summary>
        public string InputTypeName => $"of type {typeof(T).Name}";

        public Validate(T input)
        {
            this.Input = input;
        }
    }
}

#if NETSTANDARD2_1

namespace System.Runtime.CompilerServices;

/// <summary>
/// This a special exception for backwards compatibility.
/// Please upgrade your dependent package to the latest supported netN.N version rather than using netstandard2.1
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
internal sealed class CallerArgumentExpressionAttribute : Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName)
    {
        ParameterName = parameterName;
    }

    public string ParameterName { get; }
}

#endif

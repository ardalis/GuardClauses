using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace Ardalis.GuardClauses;

/// <summary>
/// Chain of Delegates
/// </summary>
public static partial class GuardClauseExtensions
{  
    /// <summary>
    /// Throws <seealso cref="NullReferenceException"/>, if delegate's invocation list is empty
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Delegate[] AgainstChains(
        this IGuardClause _,
        [ValidatedNotNull][NotNull]Delegate? type)
    {
        Guard.Against.Null(type,"Delegate","Type of delegate is null");

        Delegate[] list = type.GetInvocationList();
        return list;    
    }


}

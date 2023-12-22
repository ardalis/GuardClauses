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


 /// <summary>
 /// Throws <seealso cref="ArgumentNullException"/> if delegate's type is null
 /// <br/> Param id : index
 /// </summary>
 /// <param name="guard"></param>
 /// <param name="id">index</param>
 /// <param name="type"></param>
 /// <returns></returns>
 public static Delegate AgainstChainSelection(
     this IGuardClause guard,
     [NotNull][ValidatedNotNull]Delegate? type,
     int id = 0)
 {

     guard.Null(type, "Delegate", "Provided delegate is null");

     try
     {
         return guard.Null(type.GetInvocationList()[id]);
     }
     catch(Exception)
     {
         throw new NotFoundException("id", "delegate");
     }
 }


}

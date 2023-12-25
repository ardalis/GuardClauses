using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GuardClauses.FluentValidations;
public class GuardFluentValidation
{
    private static Assembly? Assembly { get; set; }

    public static void Configure(Assembly assembly)
    {
        Assembly = assembly; 
    }

    public static Assembly? GetAssembly() => Assembly;

}

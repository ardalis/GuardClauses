using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace GuardClauses.FluentValidations;
public class Command
{
    public int Id { get; set; }
}
public class CommandWithoutValidator
{
    public int Id { get; set; }
}
public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(o => o.Id).GreaterThan(1);
    }
}

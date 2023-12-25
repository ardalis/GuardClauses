using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Xunit;
using GuardClauses;
using Ardalis.GuardClauses.FluentValidations;
using GuardClauses.FluentValidations;
namespace GuardClauses.UnitTests;
public class GuardAgainstValidationResult
{
    [Fact]
    public void ThrowsArgumentExceptionIfItIsNotValid()
    {
        var command = new Command()
        {
            Id = 1,
        };

        Assert.Throws<ArgumentException>(() => Guard.Against.ValidateOrThrow(command));
    }

    [Fact]
    public void SuccessfullyValidated()
    {
        var command = new Command()
        {
            Id = 2
        };

        var result = Guard.Against.ValidateOrThrow<Command>(command);
        Assert.NotNull(result);
        Assert.Equal(2, result.Id); 
    }

    [Fact]
    public void ThrowsArgumentNullExceptionValidatorCouldNotBeFound()
    {
        var command = new CommandWithoutValidator()
        {
            Id = 2
        };
        Assert.Throws<ArgumentNullException>(() => Guard.Against.ValidateOrThrow(command));
    }
}

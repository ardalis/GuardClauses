![logo](media/logotype%201024.png)

[![NuGet](https://img.shields.io/nuget/v/Ardalis.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.GuardClauses)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.GuardClauses)
![publish Ardalis.GuardClauses to nuget](https://github.com/ardalis/GuardClauses/workflows/publish%20Ardalis.GuardClauses%20to%20nuget/badge.svg)

[![Follow Ardalis](https://img.shields.io/twitter/follow/ardalis.svg?label=Follow%20@ardalis)](https://twitter.com/intent/follow?screen_name=ardalis)
[![Follow NimblePros](https://img.shields.io/twitter/follow/nimblepros.svg?label=Follow%20@nimblepros)](https://twitter.com/intent/follow?screen_name=nimblepros)

# Guard Clauses

A simple extensible package with guard clause extensions.

A [guard clause](https://deviq.com/design-patterns/guard-clause) is a software pattern that simplifies complex functions by "failing fast", checking for invalid inputs up front and immediately failing if any are found.

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Usage

```c#
public void ProcessOrder(Order order)
{
    Guard.Against.Null(order);

    // process order here
}

// OR

public class Order
{
    private string _name;
    private int _quantity;
    private long _max;
    private decimal _unitPrice;
    private DateTime _dateCreated;

    public Order(string name, int quantity, long max, decimal unitPrice, DateTime dateCreated)
    {
        _name = Guard.Against.NullOrWhiteSpace(name);
        _quantity = Guard.Against.NegativeOrZero(quantity);
        _max = Guard.Against.Zero(max);
        _unitPrice = Guard.Against.Negative(unitPrice);
        _dateCreated = Guard.Against.OutOfSQLDateRange(dateCreated, dateCreated);
    }
}
```

## Supported Guard Clauses

- **Guard.Against.Null** (throws if input is null)
- **Guard.Against.NullOrEmpty** (throws if string, guid or array input is null or empty)
- **Guard.Against.NullOrWhiteSpace** (throws if string input is null, empty or whitespace)
- **Guard.Against.OutOfRange** (throws if integer/DateTime/enum input is outside a provided range)
- **Guard.Against.EnumOutOfRange** (throws if an enum value is outside a provided Enum range)
- **Guard.Against.OutOfSQLDateRange** (throws if DateTime input is outside the valid range of SQL Server DateTime values)
- **Guard.Against.Zero** (throws if number input is zero)
- **Guard.Against.Expression** (use any expression you define)
- **Guard.Against.InvalidFormat** (define allowed format with a regular expression or func)
- **Guard.Against.NotFound** (similar to Null but for use with an id/key lookup; throws a `NotFoundException`)

## Extending with your own Guard Clauses

To extend your own guards, you can do the following:

```c#
// Using the same namespace will make sure your code picks up your 
// extensions no matter where they are in your codebase.
namespace Ardalis.GuardClauses
{
    public static class FooGuard
    {
        public static void Foo(this IGuardClause guardClause,
            string input, 
            [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input?.ToLower() == "foo")
                throw new ArgumentException("Should not have been foo!", parameterName);
        }
    }
}

// Usage
public void SomeMethod(string something)
{
    Guard.Against.Foo(something);
    Guard.Against.Foo(something, nameof(something)); // optional - provide parameter name
}
```

## YouTube Overview

[![Ardalis.GuardClauses on YouTube](http://img.youtube.com/vi/OkE2VeRM4mE/0.jpg)](http://www.youtube.com/watch?v=OkE2VeRM4mE "Improve Your Code with Ardalis.GuardClauses")

## Breaking Changes in v4

- OutOfRange for Enums now uses `EnumOutOfRange`
- Custom error messages now work more consistently, which may break some unit tests

## Nice Visualization of Refactoring to use Guard Clauses

https://user-images.githubusercontent.com/782127/234028498-96e206b0-9a70-4aa0-9c36-a62477ea0aa9.mp4

via [Nicolas Carlo](https://toot.legacycode.rocks/@nicoespeon/110226815487285845)

## References

- [Getting Started with Guard Clauses](https://blog.nimblepros.com/blogs/getting-started-with-guard-clauses/)
- [How to write clean validation clauses in .NET](https://www.youtube.com/watch?v=Tvx6DNarqDM) (Nick Chapsas, YouTube, 9 minutes)
- [Guard Clauses (podcast: 7 minutes)](http://www.weeklydevtips.com/004)
- [Guard Clause](http://deviq.com/guard-clause/)

## Commercial Support

If you require commercial support to include this library in your applications, contact [NimblePros](https://nimblepros.com/talk-to-us-1)

## Build Notes (for maintainers)

- Remember to update the PackageVersion in the csproj file and then a build on main should automatically publish the new package to nuget.org.
- Add a release with form `1.3.2` to GitHub Releases in order for the package to actually be published to Nuget. Otherwise it will claim to have been successful but is lying to you.



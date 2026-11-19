# Ardalis.GuardClauses

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

A simple extensible package with guard clause extensions.

A [guard clause](https://deviq.com/design-patterns/guard-clause) is a software pattern that simplifies complex functions by "failing fast", checking for invalid inputs up front and immediately failing if any are found.

## Release Notes

This is the last MIT-licensed release of this library (see above).

- Updated to latest supported .NET frameworks (.NET 10, .NET 11) with continued NetStandard2.0 support (#419)
  - **Breaking:** dropped the `net8.0` and `netstandard2.1` targets (.NET 8 and 9 are out of support). Projects that can't use .NET 10+ get the `netstandard2.0` build instead.
- Fixed `Guard.Against.Expression` so it works with reference types; the `struct` constraint was removed (#339, #406)
- Updated Microsoft.SourceLink.GitHub to 10.0.400 (#410)
- Package license is now declared as an MIT license expression, so it shows up on nuget.org and in license-scanning tools (#359)
- NuGet package now ships its own focused readme

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
        _dateCreated = Guard.Against.OutOfSQLDateRange(dateCreated);
    }
}
```

## Roadmap

This is our roadmap for this package:

### Version 6.x

This version. The last MIT-licensed package.

### Version 15.x

The next version. It will have minimal changes from 6.x but will have a dual license and a compile-time license check and any planned breaking changes will be marked `[Obsolete]` if possible to signal what's coming. It will also be released both as `Ardalis.GuardClauses` and as `NimbleTools.GuardClauses` but with identical behavior to make upgrades as easy as possible, both now and in the future.

Why the jump from 6 to 15? We want to consolidate the 3 Ardalis packages that are moving to commercial licensing (GuardClauses, SmartEnum, Specification) and, like Aspire, we want to keep our version numbers separated from .NET's version numbers. Starting at 15 for all 3 lets us do that.

### Version 16.x

This is where we expect to introduce potentially breaking changes as we rethink, refactor, and improve the implementation of the library. We will not break the API without a good reason, but there are some inconsistencies and places where we can improve the usability and sometimes the performance and this will be the time to make such changes.

Stay informed of changes by adding your email here: <https://nimbletools.io/updates>
